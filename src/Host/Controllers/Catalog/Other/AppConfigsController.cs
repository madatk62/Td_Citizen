using TD.CitizenAPI.Application.Catalog.AppConfigs;
using TD.CitizenAPI.Application.Catalog.AppConfigs;

namespace TD.CitizenAPI.Host.Controllers.Catalog;

public class AppConfigsController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    [OpenApiOperation("Danh sách cấu hình hệ thống.", "")]
    public Task<PaginationResponse<AppConfigDto>> SearchAsync(SearchAppConfigsRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.View, FSHResource.Brands)]
    [OpenApiOperation("Chi tiết cấu hình hệ thống.", "")]
    public Task<Result<AppConfigDetailsDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetAppConfigRequest(id));
    }

    [HttpPost]
    //[MustHavePermission(FSHAction.Create, FSHResource.Brands)]
    [OpenApiOperation("Tạo mới cấu hình hệ thống.", "")]
    public Task<Result<Guid>> CreateAsync(CreateAppConfigRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    //[MustHavePermission(FSHAction.Update, FSHResource.Brands)]
    [OpenApiOperation("Cập nhật cấu hình hệ thống.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateAppConfigRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    //[MustHavePermission(FSHAction.Delete, FSHResource.Brands)]
    [OpenApiOperation("Xóa cấu hình hệ thống.", "")]
    public Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteAppConfigRequest(id));
    }
}