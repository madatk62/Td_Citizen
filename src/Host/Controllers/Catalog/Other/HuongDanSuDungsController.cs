using TD.CitizenAPI.Application.Catalog.HuongDanSuDungs;

namespace TD.CitizenAPI.Host.Controllers.Catalog;

public class HuongDanSuDungsController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    [OpenApiOperation("Danh sách tài liệu hướng dẫn sử dụng.", "")]
    public Task<PaginationResponse<HuongDanSuDungDto>> SearchAsync(SearchHuongDanSuDungsRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.View, FSHResource.Brands)]
    [OpenApiOperation("Chi tiết hướng dẫn sử dụng.", "")]
    public Task<Result<HuongDanSuDungDetailsDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetHuongDanSuDungRequest(id));
    }

    [HttpPost]
    //[MustHavePermission(FSHAction.Create, FSHResource.Brands)]
    [OpenApiOperation("Tạo mới hướng dẫn sử dụng.", "")]
    public Task<Result<Guid>> CreateAsync(CreateHuongDanSuDungRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    //[MustHavePermission(FSHAction.Update, FSHResource.Brands)]
    [OpenApiOperation("Cập nhật hướng dẫn sử dụng.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateHuongDanSuDungRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    //[MustHavePermission(FSHAction.Delete, FSHResource.Brands)]
    [OpenApiOperation("Xóa Hướng dẫn sử dụng.", "")]
    public Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteHuongDanSuDungRequest(id));
    }
}