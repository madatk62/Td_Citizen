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
using TD.CitizenAPI.Application.Catalog.MarketProducts;
using RestSharp;
using Newtonsoft.Json.Linq;
using TD.CitizenAPI.Application.Catalog.OrganizationUnits;
using Newtonsoft.Json;

namespace TD.CitizenAPI.Infrastructure.Catalog;

public class FetchOrganizationUnitJob : IFetchOrganizationUnitJob
{
    private readonly ILogger<FetchOrganizationUnitJob> _logger;
    private readonly ISender _mediator;
    private readonly IReadRepository<MarketProduct> _repository;

    private readonly IProgressBarFactory _progressBar;
    private readonly PerformingContext _performingContext;
    private readonly INotificationSender _notifications;
    private readonly ICurrentUser _currentUser;
    private readonly IProgressBar _progress;

    public FetchOrganizationUnitJob(
        ILogger<FetchOrganizationUnitJob> logger,
        ISender mediator,
        IReadRepository<MarketProduct> repository,
        IProgressBarFactory progressBar,
        PerformingContext performingContext,
        INotificationSender notifications,
        ICurrentUser currentUser)
    {
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
    public async Task FetchOrganizationUnitAsync(CancellationToken cancellationToken)
    {
        await NotifyAsync("FetchProductAsync processing has started", 0, cancellationToken);

        var client = new RestClient();
        var cancellationTokenSource = new CancellationTokenSource();


        var request_ = new RestRequest("https://api.bacgiang.gov.vn/danhmuc/GetAllGroup", Method.Get);
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
                    await _mediator.Send(
                        new CreateOrganizationUnitRequest
                        {
                           Name = item.GroupName,
                           Description = "",
                           Code = item.GroupCode,
                           FullCode = item.FullCode,
                           ParentCode = item?.OfGroup?.GroupCode,
                           Type = item.Type,
                        },
                        cancellationToken);
                }
                catch (Exception)
                {

                }
            }
        }
          
        await NotifyAsync("FetchOrganizationUnitAsync successfully completed", 0, cancellationToken);
    }

    public class Datum
    {
        public string? FullCode { get; set; }
        public string? GroupCode { get; set; }
        public string? GroupName { get; set; }
        public int? GroupOrder { get; set; }
        public OfGroup? OfGroup { get; set; }
        public string? Type { get; set; }
    }

    public class OfGroup
    {
        public string? FullCode { get; set; }
        public string? GroupCode { get; set; }
        public string? GroupName { get; set; }
    }

    public class Root
    {
        public List<Datum>? data { get; set; }
    }


}



