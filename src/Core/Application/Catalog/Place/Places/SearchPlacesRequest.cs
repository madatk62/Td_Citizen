using TD.CitizenAPI.Application.Catalog.PlaceTypes;

namespace TD.CitizenAPI.Application.Catalog.Places;

public class SearchPlacesRequest : PaginationFilter, IRequest<PaginationResponse<PlaceDto>>
{
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? Range { get; set; }
    public string? PlaceTypeIds { get; set; }
    public string? PlaceTypeCode { get; set; }
    public Guid? AreaId { get; set; }
    public Guid? ProvinceId { get; set; }
    public Guid? DistrictId { get; set; }
    public Guid? CommuneId { get; set; }

}

public class SearchCategoriesRequestHandler : IRequestHandler<SearchPlacesRequest, PaginationResponse<PlaceDto>>
{
    private readonly IReadRepository<Place> _repository;
    private readonly IReadRepository<PlaceType> _placeTypeRepository;

    public SearchCategoriesRequestHandler(IReadRepository<Place> repository, IReadRepository<PlaceType> placeTypeRepository) => (_repository, _placeTypeRepository) = (repository, placeTypeRepository);

    public async Task<PaginationResponse<PlaceDto>> Handle(SearchPlacesRequest request, CancellationToken cancellationToken)
    {

       if (!string.IsNullOrEmpty(request.PlaceTypeCode))
        {
            var placeType = await _placeTypeRepository.GetBySpecAsync(new PlaceTypeByCodeSpec(request.PlaceTypeCode), cancellationToken) ?? throw new NotFoundException("PlaceTypeCode.notfound");
            request.PlaceTypeIds = placeType.Id.ToString();
        }

        var spec = new PlacesBySearchRequestSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);
        int count = await _repository.CountAsync(spec, cancellationToken);

        return new PaginationResponse<PlaceDto>(list, count, request.PageNumber, request.PageSize);
    }
}