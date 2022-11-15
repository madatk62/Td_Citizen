namespace TD.CitizenAPI.Application.Catalog.OrganizationUnits;

public class FetchOrganizationUnitRequest : IRequest<string>
{
}

public class FetchMarketProductHandler : IRequestHandler<FetchOrganizationUnitRequest, string>
{
    private readonly IJobService _jobService;

    public FetchMarketProductHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(FetchOrganizationUnitRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Enqueue<IFetchOrganizationUnitJob>(x => x.FetchOrganizationUnitAsync(default));
        return Task.FromResult(jobId);
    }
}