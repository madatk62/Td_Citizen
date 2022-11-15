namespace TD.CitizenAPI.Domain.Catalog;

public class Feedback : AuditableEntity, IAggregateRoot
{
    public string UserName { get; set; }
    public int Rate { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }
    public int Status { get; set; }
    public string? Type { get; set; }
    public Guid? DocId { get; set; }

    public Feedback(string userName, int rate, string? description, string? content, int status, string? type, Guid? docId)
    {
        UserName = userName;
        Rate = rate;
        Description = description;
        Content = content;
        Status = status;
        Type = type;
        DocId = docId;
    }

    public Feedback UpdateStatus(int status)
    {
        Status = status;
        return this;
    }
}