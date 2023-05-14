using System.ComponentModel.DataAnnotations;

namespace AcmePublishing.Data;

public class SubscriptionIssue
{
    protected SubscriptionIssue()
    {

    }

    public SubscriptionIssue(string issue, DateOnly date, bool failedToSend, int publicationId)
    {
        Issue = issue;
        Date = date;
        FailedToSend = failedToSend;
        PublicationId = publicationId;
    }

    public int Id { get; set; }
    [MaxLength(7)]
    public string Issue { get; set; }
    public DateOnly Date { get; }
    public bool FailedToSend { get; set; }
    public int PublicationId { get; }
}
