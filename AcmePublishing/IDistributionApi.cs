using AcmePublishing.Data;

namespace AcmePublishing;

public interface IDistributionApi
{
    Task Publish(Customer customer, Address address, Publication publication, string issue);
}