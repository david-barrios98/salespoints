using FluentValidation;
using Microsoft.Extensions.Logging;
using salespoints.Application.DTOs.Auth;
using salespoints.Application.DTOs.Common;
using salespoints.Application.Ports.Inbound;
using salespoints.Application.Ports.Outbound;
using salespoints.Core.Application.DTOs;

namespace salespoints.Application.UseCases;

/// <summary>
/// Caso de uso: Autenticación de usuario
/// Implementa validación, seguridad y logging auditivo
/// </summary>
public class AuthenticateUserUseCase : IAuthenticateUserUseCase
{
    private readonly ILoginRepository _loginRepository;
    private readonly ITokenService _tokenService;
    private readonly IValidator<LoginRequestDTO> _validator;
    private readonly ILogger<AuthenticateUserUseCase> _logger;
    private readonly IFailedLoginAttemptService _failedLoginService;
    private readonly IHashPasswordService _hashPasswordService;

    public AuthenticateUserUseCase(
        ILoginRepository loginRepository,
        ITokenService tokenService,
        IValidator<LoginRequestDTO> validator,
        ILogger<AuthenticateUserUseCase> logger,
        IFailedLoginAttemptService failedLoginService,
        IHashPasswordService hashPasswordService)
    {
        _loginRepository = loginRepository;
        _tokenService = tokenService;
        _validator = validator;
        _logger = logger;
        _failedLoginService = failedLoginService;
        _hashPasswordService = hashPasswordService;
    }

    public async Task<ApiResponse<LoginResponseDTO>> ExecuteAsync(LoginRequestDTO request)
    {
        try
        {
            // 1. VALIDACIÓN DE ENTRADA
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(x => x.Key, x => x.Select(e => e.ErrorMessage).ToArray());

                _logger.LogWarning("Validación fallida para login de usuario: {username}", request.username);
                return ApiResponse<LoginResponseDTO>.Error("Validación fallida" +  errors);
            }

            // 2. VERIFICAR INTENTOS FALLIDOS
            if (await _failedLoginService.IsAccountLockedAsync(request.username))
            {
                _logger.LogWarning("Intento de login en cuenta bloqueada: {username}", request.username);
                return ApiResponse<LoginResponseDTO>.Error(
                    "Cuenta bloqueada por múltiples intentos fallidos");
            }

            // 3. OBTENER USUARIO
            var user = await _loginRepository.GetLoginUserAsync(request);
            if (user == null)
            {
                await _failedLoginService.RecordFailedAttemptAsync(request.username);
                _logger.LogWarning("Usuario no encontrado: {username}", request.username);
                return ApiResponse<LoginResponseDTO>.Error("Credenciales inválidas");
            }

            // 4. VALIDAR CONTRASEÑA
            if (!_hashPasswordService.Verify(request.password, user.password))
            {
                await _failedLoginService.RecordFailedAttemptAsync(request.username);
                _logger.LogWarning("Contraseña incorrecta para usuario: {username}", request.username);
                return ApiResponse<LoginResponseDTO>.Error("Credenciales inválidas");
            }

            // 5. LIMPIAR INTENTOS FALLIDOS
            await _failedLoginService.ClearFailedAttemptsAsync(request.username);

            // 6. GENERAR TOKENS
            var jwtUser = new JwtUserDto
            {
                user_id = user.user_id,
                username = request.username
            };

            var accessToken = _tokenService.GenerateAccessToken(jwtUser);
            var expiresIn = _tokenService.GetAccessTokenExpirationSeconds();

            // 7. PREPARAR RESPUESTA (SIN DATOS SENSIBLES)
            var response = new LoginResponseDTO
            {
                user_id = user.user_id,
                username = user.username,
                email = user.email,
                first_name = user.first_name,
                last_name = user.last_name,
                access_token = accessToken,
                expires_in = expiresIn,
                issued_at = DateTime.UtcNow
            };

            _logger.LogInformation("Usuario autenticado exitosamente: {username} | TraceId: {TraceId}",
                request.username, System.Diagnostics.Activity.Current?.Id ?? "N/A");

            return ApiResponse<LoginResponseDTO>.Success(response, "Autenticación exitosa");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado en AuthenticateUserUseCase para usuario: {username}",
                request.username);
            return ApiResponse<LoginResponseDTO>.Error(
                "Ha ocurrido un error en el servidor");
        }
    }
}
