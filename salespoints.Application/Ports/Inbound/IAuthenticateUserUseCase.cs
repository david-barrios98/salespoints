using salespoints.Application.DTOs.Auth;
using salespoints.Application.DTOs.Common;

namespace salespoints.Application.Ports.Inbound;

/// <summary>
/// Puerto de entrada: Caso de uso para autenticar usuario
/// </summary>
public interface IAuthenticateUserUseCase
{
    Task<ApiResponse<LoginResponseDTO>> ExecuteAsync(LoginRequestDTO request);
}
