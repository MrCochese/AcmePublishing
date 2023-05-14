using AcmePublishing.Data;

namespace AcmePublishing;

public class DistributorApiFactory 
{
    public static IDistributionApi Build(PrintDistributor distributor)
    {
        return new FakeDistributionApi();
    }
}
