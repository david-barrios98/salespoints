using Microsoft.AspNetCore.Mvc;
using salespoints.Application.DTOs.Auth;
using salespoints.Application.DTOs.Common;
using salespoints.Application.Ports.Inbound;

namespace salespoints.Api.Controllers.Auth;

/// <summary>
/// Controlador de Autenticaci�n
/// Adaptador HTTP que orquesta casos de uso
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticateUserUseCase _authenticateUserUseCase;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        IAuthenticateUserUseCase authenticateUserUseCase,
        ILogger<AuthController> logger)
    {
        _authenticateUserUseCase = authenticateUserUseCase;
        _logger = logger;
    }

    /// <summary>
    /// Endpoint: Login
    /// Autentica un usuario y retorna Access Token
    /// </summary>
    /// <remarks>
    /// POST /api/v1/auth/login
    /// {
    ///   "username": "usuario@ejemplo.com",
    ///   "password": "SecurePass123!"
    /// }
    /// </remarks>
    /// <param name="request">Credenciales del usuario</param>
    /// <returns>Token de acceso y datos del usuario</returns>
    /// <response code="200">Autenticaci�n exitosa</response>
    /// <response code="400">Validaci�n fallida</response>
    /// <response code="401">Credenciales inv�lidas</response>
    /// <response code="429">Cuenta bloqueada por intentos fallidos</response>
    /// <response code="500">Error interno del servidor</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<LoginResponseDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<object>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiResponse<object>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<object>))]
    public async Task<ActionResult<ApiResponse<LoginResponseDTO>>> Login([FromBody] LoginRequestDTO request)
    {
        _logger.LogInformation("Intento de login para usuario: {username}", request.username);

        var result = await _authenticateUserUseCase.ExecuteAsync(request);

        if (!result.IsSuccess)
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }
}