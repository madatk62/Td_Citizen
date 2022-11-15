using System.Linq;

namespace TD.CitizenAPI.Application.Catalog.EduDocuments;

public class EduDocumentsBySearchRequestSpec : EntitiesByPaginationFilterSpec<EduDocument, EduDocumentDto>
{
    public EduDocumentsBySearchRequestSpec(SearchEduDocumentsRequest request)
        : base(request) =>
        Query
        .Include(p => p.EduDocumentCategory)
        .Include(p => p.EduDocumentType)
        .Where(p => p.EduDocumentCategoryId.Equals(request.EduDocumentCategoryId!.Value), request.EduDocumentCategoryId.HasValue)
        .Where(p => p.EduDocumentTypeId.Equals(request.EduDocumentTypeId!.Value), request.EduDocumentTypeId.HasValue)
        ;

}