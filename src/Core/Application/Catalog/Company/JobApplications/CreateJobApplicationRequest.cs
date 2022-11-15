namespace TD.CitizenAPI.Application.Catalog.JobApplications;

public class CreateJobApplicationRequest : IRequest<Result<Guid>>
{
    public string? Name { get; set; }
    public string? CVFile { get; set; }
    public string? Image { get; set; }
    //Vi tri hien tai
    public Guid? CurrentPositionId { get; set; }
    //Vi tri mong muon
    public Guid? PositionId { get; set; }
    public Guid? JobNameId { get; set; }

    //Trinh do hoc van
    public Guid? DegreeId { get; set; }
    //Tong so nam Kinh nghiem
    public Guid? ExperienceId { get; set; }

    //Mong muon muc luong toi thieu
    public int? MinExpectedSalary { get; set; }
    //Dia diem lam viec
    public string? Address { get; set; }
    //Hinh thuc lam viec
    public Guid? JobTypeId { get; set; }
    //Cho phep nguoi khac tim kiem thong tin
    public int? IsSearchAllowed { get; set; }
}

public class CreateJobApplicationRequestValidator : CustomValidator<CreateJobApplicationRequest>
{
    public CreateJobApplicationRequestValidator(IReadRepository<JobApplication> repository, IStringLocalizer<CreateJobApplicationRequestValidator> localizer) =>
        RuleFor(p => p.Name).NotEmpty();
}

public class CreateJobApplicationRequestHandler : IRequestHandler<CreateJobApplicationRequest, Result<Guid>>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<JobApplication> _repository;
    private readonly ICurrentUser _currentUser;

    public CreateJobApplicationRequestHandler(IRepositoryWithEvents<JobApplication> repository, ICurrentUser currentUser) => (_repository, _currentUser) = (repository, currentUser);

    public async Task<Result<Guid>> Handle(CreateJobApplicationRequest request, CancellationToken cancellationToken)
    {
        var item = new JobApplication(_currentUser.GetUserName(), request.Name, request.CVFile, request.Image, request.CurrentPositionId, request.PositionId, request.JobNameId, request.DegreeId, request.ExperienceId, request.MinExpectedSalary, request.Address, request.JobTypeId, request.IsSearchAllowed);
        await _repository.AddAsync(item, cancellationToken);
        return Result<Guid>.Success(item.Id);
    }
}