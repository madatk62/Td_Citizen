﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using System.Security.Claims;
using TD.CitizenAPI.Application.Common.Exceptions;
using TD.CitizenAPI.Application.Common.Mailing;
using TD.CitizenAPI.Application.Identity.Users;
using TD.CitizenAPI.Domain.Catalog;
using TD.CitizenAPI.Domain.Identity;
using TD.CitizenAPI.Shared.Authorization;

namespace TD.CitizenAPI.Infrastructure.Identity;

internal partial class UserService
{
    /// <summary>
    /// This is used when authenticating with AzureAd.
    /// The local user is retrieved using the objectidentifier claim present in the ClaimsPrincipal.
    /// If no such claim is found, an InternalServerException is thrown.
    /// If no user is found with that ObjectId, a new one is created and populated with the values from the ClaimsPrincipal.
    /// If a role claim is present in the principal, and the user is not yet in that roll, then the user is added to that role.
    /// </summary>
    public async Task<string> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal)
    {
        string? objectId = principal.GetObjectId();
        if (string.IsNullOrWhiteSpace(objectId))
        {
            throw new InternalServerException(_localizer["Invalid objectId"]);
        }

        var user = await _userManager.Users.Where(u => u.ObjectId == objectId).FirstOrDefaultAsync()
            ?? await CreateOrUpdateFromPrincipalAsync(principal);

        if (principal.FindFirstValue(ClaimTypes.Role) is string role &&
            await _roleManager.RoleExistsAsync(role) &&
            !await _userManager.IsInRoleAsync(user, role))
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        return user.Id;
    }

    private async Task<ApplicationUser> CreateOrUpdateFromPrincipalAsync(ClaimsPrincipal principal)
    {
        string? email = principal.FindFirstValue(ClaimTypes.Upn);
        string? username = principal.GetDisplayName();
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username))
        {
            throw new InternalServerException(string.Format(_localizer["Username or Email not valid."]));
        }

        var user = await _userManager.FindByNameAsync(username);
        if (user is not null && !string.IsNullOrWhiteSpace(user.ObjectId))
        {
            throw new InternalServerException(string.Format(_localizer["Username {0} is already taken."], username));
        }

        if (user is null)
        {
            user = await _userManager.FindByEmailAsync(email);
            if (user is not null && !string.IsNullOrWhiteSpace(user.ObjectId))
            {
                throw new InternalServerException(string.Format(_localizer["Email {0} is already taken."], email));
            }
        }

        IdentityResult? result;
        if (user is not null)
        {
            user.ObjectId = principal.GetObjectId();
            result = await _userManager.UpdateAsync(user);

            await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));
        }
        else
        {
            user = new ApplicationUser
            {
                ObjectId = principal.GetObjectId(),
                FirstName = principal.FindFirstValue(ClaimTypes.GivenName),
                LastName = principal.FindFirstValue(ClaimTypes.Surname),
                Email = email,
                NormalizedEmail = email.ToUpperInvariant(),
                UserName = username,
                NormalizedUserName = username.ToUpperInvariant(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
                CreatedOn = DateTime.UtcNow,
            };
            result = await _userManager.CreateAsync(user);

            await _events.PublishAsync(new ApplicationUserCreatedEvent(user.Id));
        }

        if (!result.Succeeded)
        {
            throw new InternalServerException(_localizer["Validation Errors Occurred."], result.GetErrors(_localizer));
        }

        return user;
    }

    public async Task<string> CreateAsync(CreateUserRequest request, string origin)
    {
        var user = new ApplicationUser
        {
            FullName = request.FullName,
            Email = request.Email,
            DateOfBirth = request.DateOfBirth,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber,
            IsActive = true,
            Gender = request.Gender,
            Type = request.Type,
            IsSync = request.IsSync,
            IdentityDate = request.IdentityDate,
            IdentityNumber = request.IdentityNumber,
            IdentityPlace = request.IdentityPlace,
            ProvinceId = request.ProvinceId,
            DistrictId = request.DistrictId,
            CommuneId = request.CommuneId,
            TechnicalId = request.TechnicalId,
            Address = request.Address,
            ImageUrl = request.ImageUrl,
            CreatedOn = DateTime.UtcNow,
        };

        if (!string.IsNullOrWhiteSpace(request.IdentityNumber))
        {
            bool tmp = await ExistsWithIdentityNumberAsync(request.IdentityNumber!);

            if (tmp)
            {
                string mess = string.Format(_localizer[$"Số CMND/CCCD {request.IdentityNumber} đã được đăng ký."]);
                throw new InternalServerException(mess);
            }
        }

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new InternalServerException(_localizer["Validation Errors Occurred."], result.GetErrors(_localizer));
        }

        await _userManager.AddToRoleAsync(user, FSHRoles.Basic);

        var messages = new List<string> { string.Format(_localizer["Tài khoản {0} được tạo thành công."], user.UserName) };

        //if (_securitySettings.RequireConfirmedAccount && !string.IsNullOrEmpty(user.Email))
        //{
        //    // send verification email
        //    string emailVerificationUri = await GetEmailVerificationUriAsync(user, origin);
        //    RegisterUserEmailModel eMailModel = new RegisterUserEmailModel()
        //    {
        //        Email = user.Email,
        //        UserName = user.UserName,
        //        Url = emailVerificationUri
        //    };
        //    var mailRequest = new MailRequest(
        //        new List<string> { user.Email },
        //        _localizer["Confirm Registration"],
        //        _templateService.GenerateEmailTemplate("email-confirmation", eMailModel));
        //    _jobService.Enqueue(() => _mailService.SendAsync(mailRequest, CancellationToken.None));
        //    messages.Add(_localizer[$"Please check {user.Email} to verify your account!"]);
        //}

        await _events.PublishAsync(new ApplicationUserCreatedEvent(user.Id));

        return string.Join(Environment.NewLine, messages);
    }

    public async Task UpdateAvatar(IFormFile? request, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        try
        {
            var file = await _fileStorage.UploadFileAsync<Attachment>(request, default);
            user.ImageUrl = file.Url;
        }
        catch
        {

        }

        var result = await _userManager.UpdateAsync(user);

        await _signInManager.RefreshSignInAsync(user);

        await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));

        if (!result.Succeeded)
        {
            throw new InternalServerException(_localizer["Update avatar failed"], result.GetErrors(_localizer));
        }
    }

    public async Task UpdateAsync(UpdateUserRequest request, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        /*string currentImage = user.ImageUrl ?? string.Empty;
        if (request.Image != null || request.DeleteCurrentImage)
        {
            user.ImageUrl = await _fileStorage.UploadAsync<ApplicationUser>(request.Image, FileType.Image);
            if (request.DeleteCurrentImage && !string.IsNullOrEmpty(currentImage))
            {
                string root = Directory.GetCurrentDirectory();
                _fileStorage.Remove(Path.Combine(root, currentImage));
            }
        }*/


        user.Gender = request.Gender;
        user.ImageUrl = request.ImageUrl;

        if (!user.IsVerified)
        {
            user.FullName = request.FullName;
            user.DateOfBirth = request.DateOfBirth;
            user.IdentityNumber = request.IdentityNumber;
            user.IdentityPlace = request.IdentityPlace;
            user.IdentityDate = request.IdentityDate;
            user.PlaceOfOrigin = request.PlaceOfOrigin;
            user.PlaceOfDestination = request.PlaceOfDestination;
            user.Nationality = request.Nationality;
        }

        user.ProvinceId = request.ProvinceId;
        user.DistrictId = request.DistrictId;
        user.CommuneId = request.CommuneId;
        user.Address = request.Address;


        /*user.PhoneNumber = request.PhoneNumber;
        string phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        if (request.PhoneNumber != phoneNumber)
        {
            await _userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
        }*/

        var result = await _userManager.UpdateAsync(user);

        await _signInManager.RefreshSignInAsync(user);

        await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));

        if (!result.Succeeded)
        {
            throw new InternalServerException(_localizer["Update profile failed"], result.GetErrors(_localizer));
        }
    }


    public async Task UpdateLocationAsync(UpdateUserLocationRequest request, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        user.Latitude = request.Latitude;
        user.Longitude = request.Longitude;

        var result = await _userManager.UpdateAsync(user);

        await _signInManager.RefreshSignInAsync(user);

        await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));

        if (!result.Succeeded)
        {
            throw new InternalServerException(_localizer["Update profile failed"], result.GetErrors(_localizer));
        }
    }


    public async Task<bool> DeleteAsyncByUserName( string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        await _userManager.DeleteAsync(user);

        return true;

    }

    public async Task<bool> UpdateAsyncByUserName(UpdateUserRequest request, string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);


        user.Gender = request.Gender;
        user.ImageUrl = request.ImageUrl;

        if (!user.IsVerified)
        {
            user.FullName = request.FullName;
            user.DateOfBirth = request.DateOfBirth;
            user.IdentityNumber = request.IdentityNumber;
            user.IdentityPlace = request.IdentityPlace;
            user.IdentityDate = request.IdentityDate;
            user.PlaceOfOrigin = request.PlaceOfOrigin;
            user.PlaceOfDestination = request.PlaceOfDestination;
            user.Nationality = request.Nationality;
        }

        user.ProvinceId = request.ProvinceId;
        user.DistrictId = request.DistrictId;
        user.CommuneId = request.CommuneId;
        user.Address = request.Address;


        var result = await _userManager.UpdateAsync(user);

        await _signInManager.RefreshSignInAsync(user);

        await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));

        if (!result.Succeeded)
        {
            throw new InternalServerException(_localizer["Update profile failed"], result.GetErrors(_localizer));
        }
        return true;

    }

    public async Task<bool> VerifyAsyncByUserName(VerifyUserRequest request, string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);


        user.Gender = request.Gender;
        user.IsVerified = true;

        user.FullName = request.FullName ?? user.FullName;
        user.DateOfBirth = request.DateOfBirth ?? user.DateOfBirth;
        user.IdentityNumber = request.IdentityNumber ?? user.IdentityNumber;
        user.IdentityPlace = request.IdentityPlace ?? user.IdentityPlace;
        user.IdentityDate = request.IdentityDate ?? user.IdentityDate;
        user.PlaceOfOrigin = request.PlaceOfOrigin ?? user.PlaceOfOrigin;
        user.PlaceOfDestination = request.PlaceOfDestination ?? user.PlaceOfDestination;
        user.Nationality = request.Nationality ?? user.Nationality;

        var result = await _userManager.UpdateAsync(user);

        // await _signInManager.RefreshSignInAsync(user);

        await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));

        if (!result.Succeeded)
        {
            throw new InternalServerException(_localizer["Update profile failed"], result.GetErrors(_localizer));
        }

        return true;

    }
}
