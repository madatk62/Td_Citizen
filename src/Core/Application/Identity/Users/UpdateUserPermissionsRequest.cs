namespace TD.CitizenAPI.Application.Identity.Users;

public class UpdateUserPermissionsRequest
{
    public string UserName { get; set; } = default!;
    public List<string>? Permissions { get; set; } = default!;
}

public class UpdateUserPermissionsRequestValidator : CustomValidator<UpdateUserPermissionsRequest>
{
    public UpdateUserPermissionsRequestValidator()
    {
        RuleFor(r => r.UserName)
            .NotEmpty();
       
    }
}