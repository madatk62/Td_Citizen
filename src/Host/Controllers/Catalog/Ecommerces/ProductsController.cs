﻿using TD.CitizenAPI.Application.Catalog.Products;

namespace TD.CitizenAPI.Host.Controllers.Catalog;

public class ProductsController : VersionedApiController
{
    [HttpPost("search")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.Search, FSHResource.Products)]
    [OpenApiOperation("Danh sách sản phẩm.", "")]
    public Task<PaginationResponse<ProductDto>> SearchAsync(SearchProductsRequest request)
    {
        return Mediator.Send(request);
    }

    /*    [HttpGet("{id:guid}")]
        [MustHavePermission(FSHAction.View, FSHResource.Products)]
        [OpenApiOperation("Get product details.", "")]
        public Task<ProductDetailsDto> GetAsync(Guid id)
        {
            return Mediator.Send(new GetProductRequest(id));
        }*/
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [TenantIdHeader]
    //[MustHavePermission(FSHAction.View, FSHResource.Brands)]
    [OpenApiOperation("Chi tiết sản phẩm.", "")]
    public Task<Result<ProductDetailsDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetProductRequest(id));
    }

    [HttpGet("dapper")]
   // [MustHavePermission(FSHAction.View, FSHResource.Products)]
    [OpenApiOperation("Get product details via dapper.", "")]
    public Task<CategoriesInProduct> GetDapperAsync(Guid id)
    {
        return Mediator.Send(new GetProductViaDapperRequest(id));
    }

    [HttpPost]
    //[MustHavePermission(FSHAction.Create, FSHResource.Products)]
    [OpenApiOperation("Tạo mới sản phẩm.", "")]
    public Task<Guid> CreateAsync(CreateProductRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
   // [MustHavePermission(FSHAction.Update, FSHResource.Products)]
    [OpenApiOperation("Cập nhật sản phẩm.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateProductRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    //[MustHavePermission(FSHAction.Delete, FSHResource.Products)]
    [OpenApiOperation("Xóa sản phẩm.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteProductRequest(id));
    }

    [HttpPost("export")]
   // [MustHavePermission(FSHAction.Export, FSHResource.Products)]
    [OpenApiOperation("Export a products.", "")]
    public async Task<FileResult> ExportAsync(ExportProductsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "ProductExports");
    }

    [HttpPost("fetchdata")]
    //[MustHavePermission(FSHAction.Generate, FSHResource.Brands)]
    [OpenApiOperation("Fetch sản phẩm Tiki.", "")]
    public Task<string> FetchCategoriesAsync(GenerateProductsRequest request)
    {
        return Mediator.Send(request);
    }

}

