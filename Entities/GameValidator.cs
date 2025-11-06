using FluentValidation;

namespace ASPNETDemo1.Entities;

public class GameValidator : AbstractValidator<Game>
{
    public GameValidator()
    {
        RuleFor(g => g.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(2, 100);
        RuleFor(g => g.Genre)
            .NotEmpty().WithMessage("Genre is required")
            .Length(5, 50);
        RuleFor(g => g.Price)
            .GreaterThan(0)
            .LessThanOrEqualTo(500);
        RuleFor(g => g.ReleaseDate)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage("Date must be in the future");
        RuleFor(g => g.ImageUri)
            .NotEmpty()
            .Must(uri => Uri.TryCreate(uri,UriKind.Absolute,out _))
            .WithMessage("Invalid image uri");
    }
    
}