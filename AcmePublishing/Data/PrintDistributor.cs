namespace AcmePublishing.Data;

public class PrintDistributor 
{
    public int Id { get; set;}
    public string Name { get; set; }
    public string CountryCode { get; set;}
    public ICollection<Publication> Publications { get; set; }
}
