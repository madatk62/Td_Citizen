using TD.CitizenAPI.Application.Catalog.NhomHoSoDienTus;

namespace TD.CitizenAPI.Host.Controllers.Catalog;

public class NhomHoSoDienTusController : VersionedApiController
{
    //[ApiVersion("2.0")]
    [HttpPost("search")]
    [TenantIdHeader]
    [AllowAnonymous]
    //[MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    [OpenApiOperation("Danh sách danh mục nhóm hồ sơ điện tử.", "")]
    public Task<PaginationResponse<NhomHoSoDienTuDto>> SearchAsync(SearchNhomHoSoDienTusRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpGet("{id:guid}")]
    [TenantIdHeader]
    [AllowAnonymous]
    //[MustHavePermission(FSHAction.View, FSHResource.Brands)]
    [OpenApiOperation("Chi tiết danh mục nhóm hồ sơ điện tử.", "")]
    public Task<Result<NhomHoSoDienTuDetailDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetNhomHoSoDienTuRequest(id));
    }

    [HttpPost]
    //[MustHavePermission(FSHAction.Create, FSHResource.Brands)]
    [OpenApiOperation("Tạo mới danh mục nhóm hồ sơ điện tử.", "")]
    public Task<Result<Guid>> CreateAsync(CreateNhomHoSoDienTuRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    //[MustHavePermission(FSHAction.Update, FSHResource.Brands)]
    [OpenApiOperation("Cập nhật danh mục nhóm hồ sơ điện tử.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateNhomHoSoDienTuRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    //[MustHavePermission(FSHAction.Delete, FSHResource.Brands)]
    [OpenApiOperation("Xóa danh mục nhóm hồ sơ điện tử.", "")]
    public Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteNhomHoSoDienTuRequest(id));
    }
}