using FluentValidation;
using salespoints.Application.DTOs.Auth;

namespace salespoints.Application.Validators;

/// <summary>
/// Validador de flujo: LoginRequest
/// Implementa reglas de negocio de seguridad
/// </summary>
public class LoginRequestValidator : AbstractValidator<LoginRequestDTO>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.username)
            .NotEmpty().WithMessage("El nombre de usuario es requerido")
            .MinimumLength(3).WithMessage("El usuario debe tener m�nimo 3 caracteres")
            .MaximumLength(50).WithMessage("El usuario no puede exceder 50 caracteres")
            .Matches("^[a-zA-Z0-9._@-]+$").WithMessage("El usuario contiene caracteres inv�lidos");

        //RuleFor(x => x.password)
        //    .NotEmpty().WithMessage("La contrase�a es requerida")
        //    .MinimumLength(2).WithMessage("La contrase�a debe tener m�nimo 2 caracteres")
        //    .MaximumLength(100).WithMessage("La contrase�a no puede exceder 100 caracteres")
        //    .Matches("[A-Z]").WithMessage("La contrase�a debe contener al menos una may�scula")
        //    .Matches("[a-z]").WithMessage("La contrase�a debe contener al menos una min�scula")
        //    .Matches("[0-9]").WithMessage("La contrase�a debe contener al menos un n�mero");

        RuleFor(x => x.password)
            .NotEmpty().WithMessage("La contrase�a es requerida")
            .MinimumLength(2).WithMessage("La contrase�a debe tener m�nimo 2 caracteres");
    }
}