using TD.CitizenAPI.Application.Catalog.LoginLogs;

namespace TD.CitizenAPI.Host.Controllers.Catalog;

public class LoginLogsController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    [OpenApiOperation("Danh sách cấu hình hệ thống.", "")]
    public Task<PaginationResponse<LoginLogDto>> SearchAsync(SearchLoginLogsRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.View, FSHResource.Brands)]
    [OpenApiOperation("Chi tiết cấu hình hệ thống.", "")]
    public Task<Result<LoginLogDetailsDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetLoginLogRequest(id));
    }

    [HttpPost]
    //[MustHavePermission(FSHAction.Create, FSHResource.Brands)]
    [OpenApiOperation("Tạo mới cấu hình hệ thống.", "")]
    public Task<Result<Guid>> CreateAsync(CreateLoginLogRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("{id:guid}")]
    //[MustHavePermission(FSHAction.Delete, FSHResource.Brands)]
    [OpenApiOperation("Xóa cấu hình hệ thống.", "")]
    public Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteLoginLogRequest(id));
    }
}