using AcmePublishing.Data;

namespace AcmePublishing;

internal class FakeDistributionApi : IDistributionApi
{
    public Task Publish(Customer customer, Address address, Publication publication, string issue)
    {
        return Task.CompletedTask;
    }
}