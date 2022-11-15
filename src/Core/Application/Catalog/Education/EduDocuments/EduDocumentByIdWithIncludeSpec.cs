namespace TD.CitizenAPI.Application.Catalog.EduDocuments;

public class EduDocumentByIdWithIncludeSpec : Specification<EduDocument, EduDocumentDetailsDto>, ISingleResultSpecification
{
    public EduDocumentByIdWithIncludeSpec(Guid id) =>
        Query
            .Where(p => p.Id == id)
            .Include(p => p.EduDocumentCategory)
            .Include(p => p.EduDocumentCategory).ThenInclude(c => c.EduDocumentCatalogue)
            .Include(p => p.EduDocumentType)
            ;
}