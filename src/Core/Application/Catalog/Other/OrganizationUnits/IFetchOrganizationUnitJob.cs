using System.ComponentModel;

namespace TD.CitizenAPI.Application.Catalog.OrganizationUnits;

public interface IFetchOrganizationUnitJob : IScopedService
{
    [DisplayName("Fetch OrganizationUnit")]
    Task FetchOrganizationUnitAsync(CancellationToken cancellationToken);
}