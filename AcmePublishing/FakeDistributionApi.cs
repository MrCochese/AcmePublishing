using AcmePublishing.Data;

namespace AcmePublishing;

internal class FakeDistributionApi : IDistributionApi
{
    public Task Publish(Customer customer, Address address, Publication publication, int month)
    {
        return Task.CompletedTask;
    }
}