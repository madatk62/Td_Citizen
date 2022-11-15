using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Finbuckle.MultiTenant;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using TD.CitizenAPI.Application.Catalog.Areas;
using TD.CitizenAPI.Application.Catalog.Companies;
using TD.CitizenAPI.Application.Common.Caching;
using TD.CitizenAPI.Application.Common.Events;
using TD.CitizenAPI.Application.Common.Exceptions;
using TD.CitizenAPI.Application.Common.Exporters;
using TD.CitizenAPI.Application.Common.FileStorage;
using TD.CitizenAPI.Application.Common.Interfaces;
using TD.CitizenAPI.Application.Common.Mailing;
using TD.CitizenAPI.Application.Common.Models;
using TD.CitizenAPI.Application.Common.Persistence;
using TD.CitizenAPI.Application.Identity.Users;
using TD.CitizenAPI.Domain.Catalog;
using TD.CitizenAPI.Domain.Identity;
using TD.CitizenAPI.Infrastructure.Auth;
using TD.CitizenAPI.Infrastructure.Mailing;
using TD.CitizenAPI.Infrastructure.Persistence.Context;
using TD.CitizenAPI.Shared.Authorization;

namespace TD.CitizenAPI.Infrastructure.Identity;

internal partial class UserService : IUserService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    private readonly ApplicationDbContext _db;
    private readonly IStringLocalizer<UserService> _localizer;
    private readonly IJobService _jobService;
    private readonly IMailService _mailService;
    private readonly MailSettings _mailSettings;
    private readonly SecuritySettings _securitySettings;
    private readonly IEmailTemplateService _templateService;
    private readonly IFileStorageService _fileStorage;
    private readonly IEventPublisher _events;
    private readonly ICacheService _cache;
    private readonly ICacheKeyService _cacheKeys;
    private readonly ITenantInfo _currentTenant;
    private readonly IRepository<Area> _areaRepository;
    private readonly IExcelWriter _excelWriter;


    public UserService(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        ApplicationDbContext db,
        IStringLocalizer<UserService> localizer,
        IJobService jobService,
        IMailService mailService,
        IOptions<MailSettings> mailSettings,
        IEmailTemplateService templateService,
        IFileStorageService fileStorage,
        IEventPublisher events,
        ICacheService cache,
        ICacheKeyService cacheKeys,
        ITenantInfo currentTenant,
        IOptions<SecuritySettings> securitySettings,
        IExcelWriter excelWriter,
        IRepository<Area> areaRepository)
    {
        _excelWriter = excelWriter;
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _db = db;
        _localizer = localizer;
        _jobService = jobService;
        _mailService = mailService;
        _mailSettings = mailSettings.Value;
        _templateService = templateService;
        _fileStorage = fileStorage;
        _events = events;
        _cache = cache;
        _cacheKeys = cacheKeys;
        _currentTenant = currentTenant;
        _securitySettings = securitySettings.Value;
        _areaRepository = areaRepository;
    }


    public async Task<Stream> ExportAsync(UserListFilter filter, CancellationToken cancellationToken)
    {
        var spec = new UserListFilterSpec(filter);

        var users = await _userManager.Users
           .WithSpecification(spec)
           .ProjectToType<UserExportDto>()
           .ToListAsync(cancellationToken);

        return _excelWriter.WriteToStream(users);
    }

    public async Task<PaginationResponse<UserDto>> SearchAsync(UserListFilter filter, CancellationToken cancellationToken)
    {
        //var specc = new EntitiesByPaginationFilterSpec<ApplicationUser>(filter);

        var spec = new UserListFilterSpec(filter);
        var specPag = new UserListPaginationFilterSpec(filter);

        var users = await _userManager.Users
            .WithSpecification(specPag)
            .ProjectToType<UserDto>()
            .ToListAsync(cancellationToken);
        int count = await _userManager.Users
            .WithSpecification(spec)
            .CountAsync(cancellationToken);

        return new PaginationResponse<UserDto>(users, count, filter.PageNumber, filter.PageSize);
    }
    public async Task<int> GetCountFilterAsync(UserListFilter filter,CancellationToken cancellationToken)
    {
        var spec = new UserListFilterSpec(filter);
        int count = await _userManager.Users
           .WithSpecification(spec)
           .CountAsync(cancellationToken);
        return count;

    }





    public async Task<bool> ExistsWithNameAsync(string name)
    {
        EnsureValidTenant();
        return await _userManager.FindByNameAsync(name) is not null;
    }

    public async Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null)
    {
        EnsureValidTenant();
        return await _userManager.FindByEmailAsync(email.Normalize()) is ApplicationUser user && user.Id != exceptId;
    }

    public async Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null)
    {
        EnsureValidTenant();
        return await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber) is ApplicationUser user && user.Id != exceptId;
    }

    public async Task<bool> ExistsWithIdentityNumberAsync(string identityNumber, string? exceptId = null)
    {
        EnsureValidTenant();
        bool res = await _userManager.Users.FirstOrDefaultAsync(x => x.IdentityNumber == identityNumber) is ApplicationUser user && user.Id != exceptId;
        return res;
    }

    private void EnsureValidTenant()
    {
        if (string.IsNullOrWhiteSpace(_currentTenant?.Id))
        {
            throw new UnauthorizedException(_localizer["tenant.invalid"]);
        }
    }

    public async Task<List<UserDetailsDto>> GetListAsync(CancellationToken cancellationToken) =>
        (await _userManager.Users
                .AsNoTracking()
                .ToListAsync(cancellationToken))
            .Adapt<List<UserDetailsDto>>();

    public Task<int> GetCountAsync(CancellationToken cancellationToken) =>
        _userManager.Users.AsNoTracking().CountAsync(cancellationToken);


    public async Task<UserDetailsDto> GetAsyncByUserName(string userName, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .AsNoTracking()
            .Where(u => u.UserName == userName)
            .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        //return user.Adapt<UserDetailsDto>();5
        var tmp = user.Adapt<UserDetailsDto>();

        if (!string.IsNullOrEmpty(tmp.ProvinceId))
        {
            tmp.Province = await _areaRepository.GetBySpecAsync((ISpecification<Area, AreaDto>)new AreaByIdStringSpec(tmp.ProvinceId), cancellationToken);
        }

        if (!string.IsNullOrEmpty(tmp.DistrictId))
        {
            tmp.District = await _areaRepository.GetBySpecAsync((ISpecification<Area, AreaDto>)new AreaByIdStringSpec(tmp.DistrictId), cancellationToken);
        }

        if (!string.IsNullOrEmpty(tmp.CommuneId))
        {
            tmp.Commune = await _areaRepository.GetBySpecAsync((ISpecification<Area, AreaDto>)new AreaByIdStringSpec(tmp.CommuneId), cancellationToken);
        }

        return tmp;
    }

    public async Task<UserDetailsDto> GetAsyncByIdentityNumberName(string identityNumber, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .AsNoTracking()
            .Where(u => u.IdentityNumber == identityNumber)
            .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        var tmp = user.Adapt<UserDetailsDto>();

        return tmp;
    }
    public async Task<UserDetailsDto> GetAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        var tmp = user.Adapt<UserDetailsDto>();


        var company = await _db.Companies.FirstOrDefaultAsync(x => x.UserName == user.UserName);
        if (company != null)
        {
            tmp.CompanyId = company.Id;
            tmp.Company = company.Adapt<CompanyDto>();
        }

        if (!string.IsNullOrEmpty(tmp.ProvinceId))
        {
            tmp.Province = await _areaRepository.GetBySpecAsync((ISpecification<Area, AreaDto>)new AreaByIdStringSpec(tmp.ProvinceId), cancellationToken);

        }

        if (!string.IsNullOrEmpty(tmp.DistrictId))
        {
            tmp.District = await _areaRepository.GetBySpecAsync((ISpecification<Area, AreaDto>)new AreaByIdStringSpec(tmp.DistrictId), cancellationToken);
        }

        if (!string.IsNullOrEmpty(tmp.CommuneId))
        {
            tmp.Commune = await _areaRepository.GetBySpecAsync((ISpecification<Area, AreaDto>)new AreaByIdStringSpec(tmp.CommuneId), cancellationToken);
        }

        return tmp;
    }

    public async Task ToggleStatusAsync(ToggleUserStatusRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Where(u => u.Id == request.UserId).FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        bool isAdmin = await _userManager.IsInRoleAsync(user, FSHRoles.Admin);
        if (isAdmin)
        {
            throw new ConflictException(_localizer["Administrators Profile's Status cannot be toggled"]);
        }

        user.IsActive = request.ActivateUser;

        await _userManager.UpdateAsync(user);

        await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));
    }
    public async Task<bool> ToggleVerifiedAsync(ToggleUserStatusRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Where(u => u.Id == request.UserId).FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        bool isAdmin = await _userManager.IsInRoleAsync(user, FSHRoles.Admin);
        if (isAdmin)
        {
            throw new ConflictException(_localizer["Administrators Profile's Status cannot be toggled"]);
        }

        user.IsVerified = request.ActivateUser;

        await _userManager.UpdateAsync(user);

        await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));

        return true;
    }
}
