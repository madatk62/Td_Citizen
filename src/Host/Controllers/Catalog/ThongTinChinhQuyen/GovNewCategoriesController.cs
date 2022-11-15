using TD.CitizenAPI.Application.Catalog.GovNewCategories;

namespace TD.CitizenAPI.Host.Controllers.Catalog;

public class GovNewCategoriesController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    [OpenApiOperation("Danh sách chính sách đi xe.", "")]
    public Task<PaginationResponse<GovNewCategoryDto>> SearchAsync(SearchGovNewCategoriesRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.View, FSHResource.Brands)]
    [OpenApiOperation("Chi tiết chính sách đi xe.", "")]
    public Task<Result<GovNewCategoryDetailsDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetGovNewCategoryRequest(id));
    }

    [HttpPost]
    //[MustHavePermission(FSHAction.Create, FSHResource.Brands)]
    [OpenApiOperation("Tạo mới chính sách đi xe.", "")]
    public Task<Result<Guid>> CreateAsync(CreateGovNewCategoryRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    //[MustHavePermission(FSHAction.Update, FSHResource.Brands)]
    [OpenApiOperation("Cập nhật chính sách đi xe.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateGovNewCategoryRequest request, Guid id)
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
        return Mediator.Send(new DeleteGovNewCategoryRequest(id));
    }

}