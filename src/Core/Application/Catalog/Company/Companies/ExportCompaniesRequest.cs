using TD.CitizenAPI.Application.Common.Exporters;

namespace TD.CitizenAPI.Application.Catalog.Companies;

public class ExportCompaniesRequest : BaseFilter, IRequest<Stream>
{
    public int? Status { get; set; }
    public string? UserName { get; set; }
    public Guid? ProvinceId { get; set; }
    public Guid? DistrictId { get; set; }
    public Guid? CommuneId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class ExportProductsRequestHandler : IRequestHandler<ExportCompaniesRequest, Stream>
{
    private readonly IReadRepository<Company> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportProductsRequestHandler(IReadRepository<Company> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportCompaniesRequest request, CancellationToken cancellationToken)
    {
        var spec = new CompaniesAllBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}


