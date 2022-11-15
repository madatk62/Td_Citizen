using TD.CitizenAPI.Application.Catalog.OrganizationUnits;

namespace TD.CitizenAPI.Host.Controllers.Catalog;

public class OrganizationUnitsController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    [OpenApiOperation("Danh sách cấu hình hệ thống.", "")]
    public Task<PaginationResponse<OrganizationUnitDto>> SearchAsync(SearchOrganizationUnitsRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.View, FSHResource.Brands)]
    [OpenApiOperation("Chi tiết cấu hình hệ thống.", "")]
    public Task<Result<OrganizationUnitDetailsDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetOrganizationUnitRequest(id));
    }

    [HttpPost]
    //[MustHavePermission(FSHAction.Create, FSHResource.Brands)]
    [OpenApiOperation("Tạo mới cấu hình hệ thống.", "")]
    public Task<Result<Guid>> CreateAsync(CreateOrganizationUnitRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    //[MustHavePermission(FSHAction.Update, FSHResource.Brands)]
    [OpenApiOperation("Cập nhật cấu hình hệ thống.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateOrganizationUnitRequest request, Guid id)
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
        return Mediator.Send(new DeleteOrganizationUnitRequest(id));
    }

    [HttpPost("fetchdata")]
    //[MustHavePermission(FSHAction.Generate, FSHResource.Brands)]
    [OpenApiOperation("Dong bo du lieu don vi.", "")]
    public Task<string> FetchOrganizationUnitAsync(FetchOrganizationUnitRequest request)
    {
        return Mediator.Send(request);
    }
}