using TD.CitizenAPI.Application.Catalog.HoSoDienTus;

namespace TD.CitizenAPI.Host.Controllers.Catalog;

public class GiayToHoSoDienTusController : VersionedApiController
{
    //[ApiVersion("2.0")]
    [HttpPost("search")]
    [TenantIdHeader]
    [AllowAnonymous]
    //[MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    [OpenApiOperation("Danh sách danh mục giấy tờ hồ sơ điện tử.", "")]
    public Task<PaginationResponse<GiayToHoSoDienTuDto>> SearchAsync(SearchGiayToHoSoDienTusRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpGet("{id:guid}")]
    [TenantIdHeader]
    [AllowAnonymous]
    //[MustHavePermission(FSHAction.View, FSHResource.Brands)]
    [OpenApiOperation("Chi tiết danh mục giấy tờ hồ sơ điện tử.", "")]
    public Task<Result<GiayToHoSoDienTuDetailsDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetGiayToHoSoDienTuRequest(id));
    }

    [HttpPost]
    //[MustHavePermission(FSHAction.Create, FSHResource.Brands)]
    [OpenApiOperation("Tạo mới danh mục giấy tờ hồ sơ điện tử.", "")]
    public Task<Result<Guid>> CreateAsync(CreateGiayToHoSoDienTuRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    //[MustHavePermission(FSHAction.Update, FSHResource.Brands)]
    [OpenApiOperation("Cập nhật danh mục giấy tờ hồ sơ điện tử.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateGiayToHoSoDienTuRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    //[MustHavePermission(FSHAction.Delete, FSHResource.Brands)]
    [OpenApiOperation("Xóa danh mục giấy tờ hồ sơ điện tử.", "")]
    public Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteGiayToHoSoDienTuRequest(id));
    }
}