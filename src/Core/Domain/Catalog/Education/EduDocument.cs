namespace TD.CitizenAPI.Domain.Catalog;

public class EduDocument : AuditableEntity, IAggregateRoot
{

    public string Name { get; set; }
    public string? Image { get; set; }
    public string? File { get; set; }
    public string? Tags { get; set; }
    public string? Description { get; set; }
    public int? ViewQuantity { get; set; }
    public int? DownloadQuantity { get; set; }
    public bool? IsStar { get; set; }
    public bool? IsPublic { get; set; }


    public Guid? EduDocumentCategoryId { get; set; }
    public Guid? EduDocumentTypeId { get; set; }
    public virtual EduDocumentCategory? EduDocumentCategory { get; set; }
    public virtual EduDocumentType? EduDocumentType { get; set; }

    public EduDocument(string name, string? image, string? file, string? tags, string? description, int? viewQuantity, int? downloadQuantity, bool? isStar, bool? isPublic, Guid? eduDocumentCategoryId, Guid? eduDocumentTypeId)
    {
        Name = name;
        Image = image;
        File = file;
        Tags = tags;
        Description = description;
        ViewQuantity = viewQuantity;
        DownloadQuantity = downloadQuantity;
        IsStar = isStar;
        IsPublic = isPublic;
        EduDocumentCategoryId = eduDocumentCategoryId;
        EduDocumentTypeId = eduDocumentTypeId;
    }

    public EduDocument Update(string? name, string? image, string? file, string? tags, string? description, bool? isStar, bool? isPublic, Guid? eduDocumentCategoryId, Guid? eduDocumentTypeId)
    {
        if (name is not null && Name?.Equals(name) is not true)
        {
            Name = name;
        }

        if (file is not null && File?.Equals(file) is not true)
        {
            File = file;
        }

        if (tags is not null && Tags?.Equals(tags) is not true)
        {
            Tags = tags;
        }

        if (image is not null && Image?.Equals(image) is not true)
        {
            Image = image;
        }

        if (description is not null && Description?.Equals(description) is not true)
        {
            Description = description;
        }

        if (isStar.HasValue && IsStar != isStar)
        {
            IsStar = isStar.Value;
        }

        if (isPublic.HasValue && IsPublic != isPublic)
        {
            IsPublic = isPublic.Value;
        }

        /*if (eduDocumentCategoryId.HasValue && eduDocumentCategoryId.Value != Guid.Empty && !EduDocumentCategoryId.Equals(eduDocumentCategoryId.Value)) EduDocumentCategoryId = eduDocumentCategoryId.Value;

        if (eduDocumentTypeId.HasValue && eduDocumentTypeId.Value != Guid.Empty && !EduDocumentTypeId.Equals(eduDocumentTypeId.Value)) EduDocumentTypeId = eduDocumentTypeId.Value;*/

        EduDocumentCategoryId = eduDocumentCategoryId;

        EduDocumentTypeId = eduDocumentTypeId;

        return this;
    }

    public EduDocument UpdateViewQuantity()
    {
        ViewQuantity += 1;
        return this;
    }

    public EduDocument UpdateDownloadQuantity()
    {
        DownloadQuantity += 1;
        return this;
    }
}