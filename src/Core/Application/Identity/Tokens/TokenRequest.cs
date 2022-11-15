namespace TD.CitizenAPI.Application.Identity.Tokens;

public record TokenRequest(string UserName, string Password);

public class TokenRequestValidator : CustomValidator<TokenRequest>
{
    public TokenRequestValidator()
    {
        RuleFor(p => p.UserName).Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Invalid Username.");

        RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
            .NotEmpty();
    }
}