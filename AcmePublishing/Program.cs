using AcmePublishing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// var configuration =  new ConfigurationBuilder()
//      .AddJsonFile($"appsettings.json");
            
// var config = configuration.Build();
// var connectionString = config.GetConnectionString("ConnectionString");

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddDbContext<AcmeContext>(x => x.UseSqlServer("Data Source=192.168.20.79,5434;Initial Catalog=Acme;User id=sa;Password=Pass@word;Persist Security Info=True;encrypt=False;"));
    })
    .Build();

var context = host.Services.GetService<AcmeContext>();
context.Database.EnsureCreated();

var subscriptions = await context.Subscription.Where(x => x.Active).Include(x => x.Address).Include(x => x.Customer).Include(x => x.Publication).ThenInclude(x => x.Distributors).ToListAsync();

foreach (var subscription in subscriptions)
{
    var distributor = subscription.Publication.Distributors.FirstOrDefault(x => x.CountryCode == subscription.Address.CountryCode);
    if (distributor == null)
    {
        // log distributor for subscription not found
        continue;
    }

    var distributionApi = DistributorApiFactory.Build(distributor);
    await distributionApi.Publish(subscription.Customer, subscription.Address, subscription.Publication);
}

await host.RunAsync();