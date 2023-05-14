using AcmePublishing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AcmePublishing;

public class SubscriptionProcess {

    private readonly AcmeContext _context;
    private readonly ILogger<SubscriptionProcess> _logger;

    public SubscriptionProcess(AcmeContext context, ILogger<SubscriptionProcess> logger)
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
            var issue = $"{date.Month:00}/{date.Year:0000}";

            var distributor = subscription.Publication.Distributors.FirstOrDefault(x => x.CountryCode == subscription.Address.CountryCode);
            if (distributor == null)
            {
                _logger.LogError("Distributor not found for subscription {subscriptionId} and country {countryCode}", subscription.Id, subscription.Address.CountryCode);
                RecordIssueSend(date, subscription, issue, true);
                continue;
            }

            var distributionApi = DistributorApiFactory.Build(distributor);
            bool failed = false;
            try
            {
                await distributionApi.Publish(subscription.Customer, subscription.Address, subscription.Publication, issue);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Call to DistributionApi failed for subscription {subscriptionId}", subscription.Id);
                failed = false;
            }

            _logger.LogInformation("Issue {issue} sent to subscription {subscriptionId}", issue, subscription.Id);
            RecordIssueSend(date, subscription, issue, failed);
        }

        await _context.SaveChangesAsync();

        static void RecordIssueSend(DateOnly date, Subscription? subscription, string issue, bool failed)
        {
            subscription.IssuesSent.Add(new SubscriptionIssue(issue, date, failed, subscription.PublicationId));
        }
    }
}