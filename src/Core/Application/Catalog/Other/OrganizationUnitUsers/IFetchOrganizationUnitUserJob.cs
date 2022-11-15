using System.ComponentModel;

namespace TD.CitizenAPI.Application.Catalog.OrganizationUnitUsers;

public interface IFetchOrganizationUnitUserJob : IScopedService
{
    [DisplayName("Fetch OrganizationUnit")]
    Task FetchOrganizationUnitUserAsync(CancellationToken cancellationToken);
}