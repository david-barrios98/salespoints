using Microsoft.Extensions.Configuration;
using salespoints.Application.DTOs.Auth;
using salespoints.Application.Ports.Outbound;
using salespoints.Infrastructure.Constants;
using salespoints.Infrastructure.Extensions;
using salespoints.Infrastructure.Persistence.Adapters;
using System.Data;

namespace salespoints.Infrastructure.Persistence.Repositories
{
    public class LoginRepository : SqlConfigServer, ILoginRepository
    {
        private readonly salespointsDbContext _context;

        public LoginRepository(IConfiguration configuration, salespointsDbContext context) : base(configuration)
        {
            _context = context;
        }

        public async Task<LoginResponseDTO?> GetLoginUserAsync(LoginRequestDTO request)
        {

            var parameters = new[]
            {
                CreateParameter("@username", request.username, SqlDbType.VarChar)
            };
            return await ExecuteStoredProcedureSingleAsync(
                StoredProcedures.Auth.sp_login_user,
                parameters,
                reader => SqlDataReaderMapper.MapToDto<LoginResponseDTO>(reader));
        }
    }
}
