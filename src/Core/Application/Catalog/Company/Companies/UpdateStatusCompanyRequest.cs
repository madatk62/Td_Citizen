namespace TD.CitizenAPI.Application.Catalog.Companies;

public class UpdateStatusCompanyRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
 
    public int Status { get; set; }
}



public class UpdateStatusCompanyRequestHandler : IRequestHandler<UpdateStatusCompanyRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Company> _repository;
    private readonly IRepositoryWithEvents<CompanyIndustry> _companyIndustryRepository;

    private readonly IStringLocalizer<UpdateStatusCompanyRequestHandler> _localizer;

    public UpdateStatusCompanyRequestHandler(IRepositoryWithEvents<Company> repository, IRepositoryWithEvents<CompanyIndustry> companyIndustryRepository, IStringLocalizer<UpdateStatusCompanyRequestHandler> localizer) =>
        (_repository, _companyIndustryRepository, _localizer) = (repository, companyIndustryRepository, localizer);

    public async Task<Result<Guid>> Handle(UpdateStatusCompanyRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = item ?? throw new NotFoundException(string.Format(_localizer["company.notfound"], request.Id));

        item.UpdateStatus(request.Status);

        await _repository.UpdateAsync(item, cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}