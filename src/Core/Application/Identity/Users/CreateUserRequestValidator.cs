using FluentValidation;

namespace TD.CitizenAPI.Application.Identity.Users;

public class CreateUserRequestValidator : CustomValidator<CreateUserRequest>
{
    public CreateUserRequestValidator(IUserService userService, IStringLocalizer<CreateUserRequestValidator> localizer)
    {
        //RuleFor(u => u.Email).Cascade(CascadeMode.Stop)
        //    .EmailAddress()
        //    .When(u => !string.IsNullOrWhiteSpace(u.Email))
        //        .WithMessage(localizer["Email không đúng định dạng."])
        //    .MustAsync(async (email, _) => !await userService.ExistsWithEmailAsync(email))
        //        .WithMessage((_, email) => string.Format(localizer["Email {0} đã được đăng ký."], email))
        //    .Unless(u => string.IsNullOrWhiteSpace(u.Email));
        RuleFor(u => u.UserName).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(6)
            .MustAsync(async (name, _) => !await userService.ExistsWithNameAsync(name))
                .WithMessage((_, name) => string.Format(localizer["Tài khoản {0} đã tồn tại trong hệ thống."], name));

        RuleFor(u => u.PhoneNumber).Cascade(CascadeMode.Stop)
            .MustAsync(async (phone, _) => !await userService.ExistsWithPhoneNumberAsync(phone!))
                .WithMessage((_, phone) => string.Format(localizer["Số điện thoại {0} đã tồn tại trong hệ thống."], phone))
                .Unless(u => string.IsNullOrWhiteSpace(u.PhoneNumber));
        /*RuleFor(u => u.IdentityNumber).Cascade(CascadeMode.Stop)
           .MustAsync(async (identityNumber, _) => !await userService.ExistsWithIdentityNumberAsync(identityNumber!))
               .WithMessage((_, identityNumber) => string.Format(localizer["Identity number {0} is already registered."], identityNumber))
               .Unless(u => string.IsNullOrWhiteSpace(u.IdentityNumber));*/
        RuleFor(u => u.IdentityNumber).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(string.Format(localizer["Số CCCD/CMND không được để trống."]))
            .MustAsync(async(identityNumber,_) => !await userService.ExistsWithIdentityNumberAsync(identityNumber!))
            .WithMessage((_, identityNumber) => string.Format(localizer["Số CCCD/CMND {0} đã tồn tại trong hệ thống."], identityNumber))
            .Unless(u => string.IsNullOrWhiteSpace(u.IdentityNumber));
        RuleFor(p => p.FullName).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(localizer["Họ tên không được để trống."]);

        RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(p => p.ConfirmPassword).Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Equal(p => p.Password);
    }
}