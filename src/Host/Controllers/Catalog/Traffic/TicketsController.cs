using TD.CitizenAPI.Application.Catalog.Categories;
using TD.CitizenAPI.Application.Catalog.PlaceTypes;
using TD.CitizenAPI.Application.Catalog.Tickets;

namespace TD.CitizenAPI.Host.Controllers.Catalog;

public class TicketsController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.Search, FSHResource.Brands)]
    [OpenApiOperation("Danh sách vé.", "")]
    public Task<PaginationResponse<TicketDto>> SearchAsync(SearchTicketsRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.View, FSHResource.Brands)]
    [OpenApiOperation("Chi tiết vé.", "")]
    public Task<Result<TicketDetailsDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetTicketRequest(id));
    }

    [HttpPost]
    //[MustHavePermission(FSHAction.Create, FSHResource.Brands)]
    [OpenApiOperation("Tạo mới vé.", "")]
    public Task<Result<Guid>> CreateAsync(CreateTicketRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    //[MustHavePermission(FSHAction.Update, FSHResource.Brands)]
    [OpenApiOperation("Cập nhật vé.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateTicketRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    //[MustHavePermission(FSHAction.Delete, FSHResource.Brands)]
    [OpenApiOperation("Xóa vé.", "")]
    public Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteTicketRequest(id));
    }

}