namespace AcmePublishing.Data;

public class Publication
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<PrintDistributor> Distributors { get; set;}
}
