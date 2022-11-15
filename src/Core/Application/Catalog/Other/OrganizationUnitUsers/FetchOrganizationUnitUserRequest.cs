namespace TD.CitizenAPI.Application.Catalog.OrganizationUnitUsers;

public class FetchOrganizationUnitUserRequest : IRequest<string>
{
}

public class FetchOrganizationUnitUserHandler : IRequestHandler<FetchOrganizationUnitUserRequest, string>
{
    private readonly IJobService _jobService;

    public FetchOrganizationUnitUserHandler(IJobService jobService) => _jobService = jobService;

    public Task<string> Handle(FetchOrganizationUnitUserRequest request, CancellationToken cancellationToken)
    {
        string jobId = _jobService.Enqueue<IFetchOrganizationUnitUserJob>(x => x.FetchOrganizationUnitUserAsync(default));
        return Task.FromResult(jobId);
    }
}