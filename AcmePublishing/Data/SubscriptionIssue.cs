namespace AcmePublishing.Data;

public class SubscriptionIssue
{
    protected SubscriptionIssue()
    {

    }

    public SubscriptionIssue(byte month, DateOnly date, bool failedToSend, int publicationId)
    {
        Month = month;
        Date = date;
        FailedToSend = failedToSend;
        PublicationId = publicationId;
    }

    public int Id { get; set; }
    public byte Month { get; set; }
    public DateOnly Date { get; }
    public bool FailedToSend { get; set; }
    public int PublicationId { get; }
}
