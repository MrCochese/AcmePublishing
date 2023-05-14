using AcmePublishing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AcmePublishing;

// var configuration =  new ConfigurationBuilder()
//      .AddJsonFile($"appsettings.json");
            
// var config = configuration.Build();
// var connectionString = config.GetConnectionString("ConnectionString");

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddLogging();
        services.AddTransient<PublishProcess>();
        services.AddDbContext<AcmeContext>(x => x.UseSqlServer("Data Source=192.168.20.79,5434;Initial Catalog=Acme;User id=sa;Password=Pass@word;Persist Security Info=True;encrypt=False;"));
    })
    .Build();

await host.Services.GetService<PublishProcess>().Execute();

await host.RunAsync();