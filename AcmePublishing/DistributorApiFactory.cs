using AcmePublishing.Data;

namespace AcmePublishing;

public class DistributorApiFactory 
{
    public static IDistributionApi Build(PrintDistributor distributor)
    {
        return new FakeDistributionApi();
    }
}

public interface IDistributionApi
{
    Task Publish(Customer customer, Address address, Publication publication, int month);
}