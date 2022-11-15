namespace TD.CitizenAPI.Domain.Catalog;

//Thong tin tu chinh quyen
public class GovNew : AuditableEntity, IAggregateRoot
{
    public string Title { get; set; }
    public string? Actor { get; set; }
    public string? Description { get; set; }

    public string? Content { get; set; }
    public DateTime? Date { get; set; }
    public string? Image { get; set; }
    public string? Files { get; set; }
    public string? Source { get; set; }
    public int? ViewQuantity { get; set; }
    public int? Level { get; set; }

    public bool? IsStar { get; set; }
    public bool? IsPublic { get; set; }
    public bool? IsNotification { get; set; }


    public Guid? GovNewCategoryId { get; set; }
    public Guid? ProvinceId { get; set; }
    public Guid? DistrictId { get; set; }
    public Guid? CommuneId { get; set; }
    public virtual GovNewCategory? GovNewCategory { get; set; }

    public virtual Area? Province { get; set; }
    public virtual Area? District { get; set; }
    public virtual Area? Commune { get; set; }

    public GovNew(string title, string? actor, string? description, string? content, DateTime? date, string? image, string? files, string? source, int? viewQuantity, int? level, bool? isStar, bool? isPublic, bool? isNotification, Guid? govNewCategoryId, Guid? provinceId, Guid? districtId, Guid? communeId)
    {
        Title = title;
        Actor = actor;
        Description = description;
        Content = content;
        Date = date;
        Image = image;
        Files = files;
        Source = source;
        ViewQuantity = viewQuantity;
        Level = level;
        IsStar = isStar;
        IsPublic = isPublic;
        IsNotification = isNotification;
        GovNewCategoryId = govNewCategoryId;
        ProvinceId = provinceId;
        DistrictId = districtId;
        CommuneId = communeId;
    }

    public GovNew UpdateViewQuantity()
    {
        ViewQuantity += 1;
        return this;
    }

    public GovNew Update(string? title, string? actor, string? description, string? content, DateTime? date, string? image, string? files, string? source,  int? level, bool? isStar, bool? isPublic, bool? isNotification, Guid? govNewCategoryId, Guid? provinceId, Guid? districtId, Guid? communeId)
    {
        if (title is not null && Title?.Equals(title) is not true)
        {
            Title = title;
        }

        if (actor is not null && Actor?.Equals(actor) is not true)
        {
            Actor = actor;
        }

        if (description is not null && Description?.Equals(description) is not true)
        {
            Description = description;
        }

        if (files is not null && Files?.Equals(files) is not true)
        {
            Files = files;
        }

        if (content is not null && Content?.Equals(content) is not true)
        {
            Content = content;
        }

        if (image is not null && Image?.Equals(image) is not true)
        {
            Image = image;
        }

        if (source is not null && Source?.Equals(source) is not true)
        {
            Source = source;
        }

        if (date.HasValue && !Date.Equals(date.Value))
        {
            Date = date.Value;
        }

       

        if (level.HasValue && Level != level)
        {
            Level = level.Value;
        }

        if (isPublic.HasValue && IsPublic != isPublic)
        {
            IsPublic = isPublic.Value;
        }

        if (isStar.HasValue && IsStar != isStar)
        {
            IsStar = isStar.Value;
        }

        if (isNotification.HasValue && IsNotification != isNotification)
        {
            IsNotification = isNotification.Value;
        }

        if (govNewCategoryId.HasValue && govNewCategoryId.Value != Guid.Empty && !GovNewCategoryId.Equals(govNewCategoryId.Value))
        {
            GovNewCategoryId = govNewCategoryId.Value;
        }

        if (provinceId.HasValue && provinceId.Value != Guid.Empty && !ProvinceId.Equals(provinceId.Value))
        {
            ProvinceId = provinceId.Value;
        }

        if (districtId.HasValue && districtId.Value != Guid.Empty && !DistrictId.Equals(districtId.Value))
        {
            DistrictId = districtId.Value;
        }

        if (communeId.HasValue && communeId.Value != Guid.Empty && !CommuneId.Equals(communeId.Value))
        {
            CommuneId = communeId.Value;
        }

        return this;
    }


    public GovNew Update(int? viewQuantity)
    {
        if (viewQuantity.HasValue && ViewQuantity != viewQuantity)
        {
            ViewQuantity = viewQuantity.Value;
        }

        return this;
    }

}