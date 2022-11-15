using TD.CitizenAPI.Application.Catalog.LoaiHoSoDienTus;

namespace TD.CitizenAPI.Host.Controllers.Catalog;

public class LoaiHoSoDienTusController : VersionedApiController
{
    //[ApiVersion("2.0")]
    [HttpPost("search")]
    [TenantIdHeader]
    [AllowAnonymous]
    //[MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    [OpenApiOperation("Danh sách danh mục loại hồ sơ điện tử.", "")]
    public Task<PaginationResponse<LoaiHoSoDienTuDto>> SearchAsync(SearchLoaiHoSoDienTusRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpGet("{id:guid}")]
    [TenantIdHeader]
    [AllowAnonymous]
    //[MustHavePermission(FSHAction.View, FSHResource.Brands)]
    [OpenApiOperation("Chi tiết danh mục loại hồ sơ điện tử.", "")]
    public Task<Result<LoaiHoSoDienTuDetailDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetLoaiHoSoDienTuRequest(id));
    }

    [HttpPost]
    //[MustHavePermission(FSHAction.Create, FSHResource.Brands)]
    [OpenApiOperation("Tạo mới danh mục loại hồ sơ điện tử.", "")]
    public Task<Result<Guid>> CreateAsync(CreateLoaiHoSoDienTuRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    //[MustHavePermission(FSHAction.Update, FSHResource.Brands)]
    [OpenApiOperation("Cập nhật danh mục loại hồ sơ điện tử.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateLoaiHoSoDienTuRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    //[MustHavePermission(FSHAction.Delete, FSHResource.Brands)]
    [OpenApiOperation("Xóa danh mục loại hồ sơ điện tử.", "")]
    public Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteLoaiHoSoDienTuRequest(id));
    }
}