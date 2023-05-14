using AcmePublishing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AcmePublishing;

public class PublishProcess {

    private readonly AcmeContext _context;
    private readonly ILogger<PublishProcess> _logger;

    public PublishProcess(AcmeContext context, ILogger<PublishProcess> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Execute()
    {    
        _context.Database.EnsureCreated();

        var subscriptions = await _context.Subscription
            .Where(x => x.Active)
            .Include(x => x.Address)
            .Include(x => x.Customer)
            .Include(x => x.Publication)
            .ThenInclude(x => x.Distributors)
            .ToListAsync();

        var date = DateOnly.FromDateTime(DateTime.Today);

        foreach (var subscription in subscriptions)
        {
            var distributor = subscription.Publication.Distributors.FirstOrDefault(x => x.CountryCode == subscription.Address.CountryCode);
            if (distributor == null)
            {
                _logger.LogError("Distributor not found for subscription {subscriptionId} and country {countryCode}", subscription.Id, subscription.Address.CountryCode);
                subscription.IssuesSent.Add(new SubscriptionIssue((byte)date.Month, date, true, subscription.PublicationId));
                continue;
            }

            var distributionApi = DistributorApiFactory.Build(distributor);
            bool failed = false;
            try {
                await distributionApi.Publish(subscription.Customer, subscription.Address, subscription.Publication, date.Month);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Call to DistributionApi failed for subscription {subscriptionId}", subscription.Id);
                failed = false;
            }

            subscription.IssuesSent.Add(new SubscriptionIssue((byte)date.Month, date, failed, subscription.PublicationId));
        }

        await _context.SaveChangesAsync();
    }
}