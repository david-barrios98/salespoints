using salespoints.Application.DTOs.Auth;

namespace salespoints.Application.Ports.Outbound
{
    public interface ILoginRepository
    {
        Task<LoginResponseDTO?> GetLoginUserAsync(LoginRequestDTO request);
    }
}
