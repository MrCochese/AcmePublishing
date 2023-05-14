namespace AcmePublishing.Data;

public class Subscription
{
    public int Id { get; set; }
    public int PublicationId { get; set; }
    public Publication Publication { get; set; }
    public int AddressId {get; set;}
    public Address Address { get; set; }
    public int CustomerId { get; set;}
    public Customer Customer { get; set;}
    public bool Active { get; set; }
}
