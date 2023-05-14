namespace AcmePublishing.Data;

public class PublicationIssue
{
    public int Id { get; }
    public DateOnly PublishDate { get; }
    public int PublicationId { get; }
}
