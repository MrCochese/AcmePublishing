using AcmePublishing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AcmePublishing;

var configuration =  new ConfigurationBuilder()
     .AddEnvironmentVariables()
     .AddUserSecrets("c7519472-b5c7-4869-b28a-358f57e65517")
     .Build();

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddLogging();
        services.AddTransient<SubscriptionProcess>();
        services.AddDbContext<AcmeContext>(x => x.UseSqlServer(configuration.GetConnectionString("SqlServer")));
    })
    .Build();

await host.Services.GetService<SubscriptionProcess>().Execute();

await host.RunAsync();