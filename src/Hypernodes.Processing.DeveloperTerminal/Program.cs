using Cocona;
using Hypernodes.Core.GrainInterfaces;
using Hypernodes.Core.Transcoding;
using Hypernodes.Core.Transcoding.OutputStreams;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Spectre.Console;

void PrintHeader()
{
    AnsiConsole.Write(new FigletText("MediaBound").Color(Color.Red).Centered());
    AnsiConsole.Write(new Markup("[yellow]Developer Terminal[/]").Centered());
    AnsiConsole.Write(new Markup("[gray]Internal use only[/]"));
}

PrintHeader();

var builder = CoconaApp.CreateBuilder();

// Sets up Microsoft Orleans client.
var client = new ClientBuilder()
    .UseLocalhostClustering()
    .Build();

await client.Connect();
AnsiConsole.MarkupLine("[green]Successfully[/] connected to cluster");

builder.Services.AddSingleton(client);

var app = builder.Build();

// Creates a new job.
app.AddCommand("new-job", async (IClusterClient clusterClient) =>
{
    // Generates a new job grain.
    var id = Guid.NewGuid();
    var jobGrain = clusterClient.GetGrain<ITranscodingJobGrain>(id);
    
    // Starts the job.
    const string uri = "/Users/coderynx/Downloads/test.mp4";
    const string outUri = "/Users/coderynx/Downloads/test.mkv";
    // var result = await probeService.ProbeMediaPackage(uri);

    var videoOutputStream = VideoOutputStream.CreateBuilder()
        .WithInputStreamIndex(0)
        .WithOutputStreamIndex(0)
        .WithCodec(VideoCodec.H264)
        .WithConstantRateFactor(18)
        .WithScaling(VideoScalingAlgorithm.Lanczos, 1280, 720)
        .Build();

    var audioOutputStream = AudioOutputStream.CreateBuilder()
        .WithInputStreamIndex(1)
        .WithOutputStreamIndex(1)
        .WithCodec(AudioCodec.TrueHd)
        .WithNumberOfChannels(2)
        .Build();

    var jobConfiguration = TranscodingJobConfiguration.CreateBuilder()
        .WithInputUri(uri)
        .WithOutputUri(outUri)
        .AddOutputStream(videoOutputStream)
        .AddOutputStream(audioOutputStream)
        .Build();

    var tokenSource = new GrainCancellationTokenSource();
    var result = await jobGrain.StartJobAsync(jobConfiguration, tokenSource.Token);
});

app.Run();