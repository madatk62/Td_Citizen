using TD.CitizenAPI.Application.Catalog.CarPolicies;
using TD.CitizenAPI.Application.Catalog.GovNews;

namespace TD.CitizenAPI.Host.Controllers.Catalog;

public class GovNewsController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    [OpenApiOperation("Danh sách chính sách đi xe.", "")]
    public Task<PaginationResponse<GovNewDto>> SearchAsync(SearchGovNewsRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.View, FSHResource.Brands)]
    [OpenApiOperation("Chi tiết chính sách đi xe.", "")]
    public Task<Result<GovNewDetailsDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetGovNewRequest(id));
    }

    [HttpPost]
    //[MustHavePermission(FSHAction.Create, FSHResource.Brands)]
    [OpenApiOperation("Tạo mới chính sách đi xe.", "")]
    public Task<Result<Guid>> CreateAsync(CreateGovNewRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    //[MustHavePermission(FSHAction.Update, FSHResource.Brands)]
    [OpenApiOperation("Cập nhật chính sách đi xe.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateGovNewRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    //[MustHavePermission(FSHAction.Delete, FSHResource.Brands)]
    [OpenApiOperation("Xóa chính sách đi xe.", "")]
    public Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteGovNewRequest(id));
    }

}