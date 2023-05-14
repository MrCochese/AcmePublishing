namespace AcmePublishing.Data;

public class Customer 
{
    public int Id { get; set; }
    public string FamilyName { get; set; }
    public string GivenName { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; }
}
