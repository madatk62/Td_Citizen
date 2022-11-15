using Ardalis.Specification;
using TD.CitizenAPI.Application.Catalog.Brands;
using TD.CitizenAPI.Application.Common.Interfaces;
using TD.CitizenAPI.Application.Common.Persistence;
using TD.CitizenAPI.Domain.Catalog;
using TD.CitizenAPI.Shared.Notifications;
using Hangfire;
using Hangfire.Console.Extensions;
using Hangfire.Console.Progress;
using Hangfire.Server;
using MediatR;
using Microsoft.Extensions.Logging;
using RestSharp;
using Newtonsoft.Json.Linq;
using TD.CitizenAPI.Application.Catalog.OrganizationUnits;
using Newtonsoft.Json;
using TD.CitizenAPI.Application.Catalog.OrganizationUnitUsers;
using TD.CitizenAPI.Application.Identity.Users;

namespace TD.CitizenAPI.Infrastructure.Catalog;

public class FetchOrganizationUnitUserJob : IFetchOrganizationUnitUserJob
{
    private readonly ILogger<FetchOrganizationUnitJob> _logger;
    private readonly ISender _mediator;
    private readonly IReadRepository<MarketProduct> _repository;

    private readonly IProgressBarFactory _progressBar;
    private readonly PerformingContext _performingContext;
    private readonly INotificationSender _notifications;
    private readonly ICurrentUser _currentUser;
    private readonly IProgressBar _progress;
    private readonly IUserService _userService;


    public FetchOrganizationUnitUserJob(
        IUserService userService,
        ILogger<FetchOrganizationUnitJob> logger,
        ISender mediator,
        IReadRepository<MarketProduct> repository,
        IProgressBarFactory progressBar,
        PerformingContext performingContext,
        INotificationSender notifications,
        ICurrentUser currentUser)
    {
        _userService = userService;
        _logger = logger;
        _mediator = mediator;
        _repository = repository;
        _progressBar = progressBar;
        _performingContext = performingContext;
        _notifications = notifications;
        _currentUser = currentUser;
        _progress = _progressBar.Create();
    }

    private async Task NotifyAsync(string message, int progress, CancellationToken cancellationToken)
    {
        _progress.SetValue(progress);
        await _notifications.SendToUserAsync(
            new JobNotification()
            {
                JobId = _performingContext.BackgroundJob.Id,
                Message = message,
                Progress = progress
            },
            _currentUser.GetUserId().ToString(),
            cancellationToken);
    }

    [Queue("notdefault")]
    public async Task FetchOrganizationUnitUserAsync(CancellationToken cancellationToken)
    {
        await NotifyAsync("FetchProductAsync processing has started", 0, cancellationToken);

        var client = new RestClient();
        var cancellationTokenSource = new CancellationTokenSource();


        var request_ = new RestRequest("https://api.bacgiang.gov.vn/danhmuc/GetAllUser?top=100000", Method.Get);
        request_.AddHeader("Authorization", "Bearer 83d4c566-9fb6-3da8-a496-d688d3f1694b");

        var restResponse =
            await client.ExecuteAsync(request_, cancellationTokenSource.Token);

        string? content = restResponse.Content;

        Root dataProduct = JsonConvert.DeserializeObject<Root>(content);

        if (dataProduct.data != null)
        {
            foreach (var item in dataProduct.data)
            {
                try
                {

                    var request = new CreateUserRequest() { FullName = item?.UserProfile?.FullName, Email = item?.UserProfile?.Email ??(item?.UserProfile?.Account + "@bacgiang.gov.vn"), UserName = item?.UserProfile?.Account, Gender = item?.UserProfile?.Sex, Type = 1, IsSync = true};

                    await _userService.CreateAsync(request, "");

                }
                catch (Exception)
                {

                }
            }
        }
          
        await NotifyAsync("FetchOrganizationUnitAsync successfully completed", 0, cancellationToken);
    }


    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Datum
    {
        public Group? Group { get; set; }
        public int? ID { get; set; }
        public Position? Position { get; set; }
        public UserOffice? UserOffice { get; set; }
        public UserProfile? UserProfile { get; set; }
    }


    public class Group
    {
        public string? GroupCode { get; set; }
        public string? GroupName { get; set; }
    }

    public class Position
    {
        public string? Name { get; set; }
    }

    public class Root
    {
        public List<Datum>? data { get; set; }
    }

    public class UserOffice
    {
        public string? GroupCode { get; set; }
        public string? GroupName { get; set; }
    }

    public class UserProfile
    {
        public string? Account { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Sex { get; set; }
    }



}



