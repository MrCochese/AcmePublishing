using System.ComponentModel.DataAnnotations;

namespace AcmePublishing.Data;

public class Customer 
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string FamilyName { get; set; }
    [MaxLength(50)]
    public string GivenName { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; }
}
