namespace TD.CitizenAPI.Domain.Catalog;

public class EduDocumentCatalogue : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string? Icon { get; set; }
    public string? Image { get; set; }
    public string? CoverImage { get; set; }
    public string? Description { get; set; }
    public int? Order { get; set; }

    public EduDocumentCatalogue(string name, string code, string? icon, string? image, string? coverImage, string? description, int? order)
    {
        Name = name;
        Code = code;
        Icon = icon;
        Image = image;
        CoverImage = coverImage;
        Description = description;
        Order = order;
    }

    public EduDocumentCatalogue Update(string? name, string? code, string? icon, string? image, string? coverImage, string? description, int? order)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (code is not null && Code?.Equals(code) is not true) Code = code;
        if (icon is not null && Icon?.Equals(icon) is not true) Icon = icon;
        if (image is not null && Image?.Equals(image) is not true) Image = image;
        if (coverImage is not null && CoverImage?.Equals(coverImage) is not true) CoverImage = coverImage;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (order.HasValue && Order != order) Order = order.Value;

        return this;
    }
}