using System.Reflection;
using HyperNodes.DataAccess;
using HyperNodes.DataAccess.Repositories;
using Hypernodes.Web.Api.Endpoints;
using Hypernodes.Web.Api.MappingProfiles;
using Microsoft.EntityFrameworkCore;
using Orleans;
using tusdotnet;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;
using tusdotnet.Models.Expiration;
using tusdotnet.Stores;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(kestrel => { kestrel.Limits.MaxRequestBodySize = null; });

builder.Services.AddSingleton(new ClientBuilder()
        .UseLocalhostClustering()
        .Build());

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ApplicationConnection")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IMediaObjectsRepository, MediaObjectsRepository>();
var app = builder.Build();

app.UseCors(builder => builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .WithExposedHeaders(tusdotnet.Helpers.CorsHelper.GetExposedHeaders())
);
var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

app.Use(async (httpContext, next) =>
{
    var requestTimeout = TimeSpan.FromSeconds(60);
    using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(httpContext.RequestAborted);
    timeoutCts.CancelAfter(requestTimeout);
    httpContext.RequestAborted = timeoutCts.Token;
    await next();
});

app.UseTus(ctx => new DefaultTusConfiguration
{
    Store = new TusDiskStore(Path.Combine(path, "files")),
    UrlPath = "/files",
    Expiration = new AbsoluteExpiration(TimeSpan.FromMinutes(5)),
    Events = new Events
    {
        OnFileCompleteAsync = async eventContext =>
        {
            var file = await eventContext.GetFileAsync();

            // Gets file path.
            var metadata = await file.GetMetadataAsync(CancellationToken.None);
            var metadataName = metadata.GetValueOrDefault("name").ToString();
            var filePath = Path.Combine(path, "files", metadataName);

            // Writes file to disk.
            var content = await file.GetContentAsync(eventContext.CancellationToken);
            await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            await content.CopyToAsync(fileStream);

            // Begins media analysis.
            var client = app.Services.GetService<IClusterClient>();
        },
        OnCreateCompleteAsync = ctx =>
        {
            Console.WriteLine($"Created file {ctx.FileId} using {ctx.Store.GetType().FullName}");
            return Task.CompletedTask;
        }
    }
});

app.MapMediaObjectsApi();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();