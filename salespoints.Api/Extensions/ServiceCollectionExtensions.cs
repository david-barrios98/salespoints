using FluentValidation;
using salespoints.Application.Ports.Inbound;
using salespoints.Application.Ports.Outbound;
using salespoints.Application.UseCases;
using salespoints.Application.Validators;
using salespoints.Infrastructure.ExternalServices;
using salespoints.Infrastructure.Persistence.Repositories;
using salespoints.Infrastructure.Security;

namespace salespoints.Api.Extensions;

/// <summary>
/// Extensiones de servicios para inyecci�n de dependencias
/// Organiza los registros siguiendo arquitectura hexagonal
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registra los casos de uso (Puertos de entrada)
    /// </summary>
    public static IServiceCollection AddApplicationUseCases(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticateUserUseCase, AuthenticateUserUseCase>();
        return services;
    }

    /// <summary>
    /// Registra los puertos de salida (Repositorios y Servicios)
    /// </summary>
    public static IServiceCollection AddApplicationPorts(this IServiceCollection services)
    {
        services.AddScoped<ILoginRepository, LoginRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IFailedLoginAttemptService, FailedLoginAttemptService>();
        services.AddScoped<IHashPasswordService, HashPasswordService>();
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IGetCriticalInventoryUseCase, GetCriticalInventoryUseCase>();

        return services;
    }

    /// <summary>
    /// Registra los validadores (FluentValidation)
    /// </summary>
    public static IServiceCollection AddApplicationValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
        return services;
    }
}