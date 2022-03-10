using Hypernodes.FFMpeg.Engine.Mapping;
using Hypernodes.FFMpeg.Engine.Services;
using Orleans;
using Orleans.Hosting;
using Serilog;

var host = Host.CreateDefaultBuilder(args)

    .ConfigureServices(services =>
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IProbeService, ProbeService>();
        services.AddScoped<ITranscodingService, TranscodingService>();
    })
    
    // Serilog configuration.
    .UseSerilog((hostingContext, _, loggerConfiguration) => loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext())
    
    // Orleans configuration.
    .UseOrleans((host, builder) =>
    {
        builder
            .AddAdoNetGrainStorage("storage", options =>
            {
                options.Invariant = "Npgsql";
                options.ConnectionString = host.Configuration.GetConnectionString("OrleansStorage");
            })
            .UseLocalhostClustering()
            .UseDashboard();
    })
    .Build();

await host.RunAsync();