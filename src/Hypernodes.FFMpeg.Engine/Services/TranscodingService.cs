using System.Text;
using System.Text.RegularExpressions;
using CliWrap;
using Hypernodes.Core.Models.Streams;
using Hypernodes.Core.Transcoding;
using Hypernodes.Core.Transcoding.OutputStreams;

namespace Hypernodes.FFMpeg.Engine.Services;

public class TranscodingProcessEventArgs : EventArgs
{
    public TranscodingProcessEventArgs(int percentage, TimeSpan currentTime, TimeSpan totalTime)
    {
        Percentage = percentage;
        CurrentTime = currentTime;
        TotalTime = totalTime;
    }

    public int Percentage { get; }
    public TimeSpan CurrentTime { get; }
    public TimeSpan TotalTime { get; }
}

public class TranscodingService : ITranscodingService
{
    public TranscodingService(IProbeService probeService)
    {
        _probeService = probeService;
    }

    private readonly IProbeService _probeService;

    private static StringBuilder BuildStreamString(IOutputStream outputStream, StringBuilder arguments)
    {
        arguments.Append($"-map 0:{outputStream.InputStreamIndex} ");

        // Gets stream kind.
        switch (outputStream.Kind)
        {
            case StreamKind.Video:
                var videoOutputStream = (VideoOutputStream)outputStream;

                // Setups video codec.
                arguments.Append($"-c:v {videoOutputStream.Codec} ");

                // Setups video bitrate.
                if (videoOutputStream.Bitrate is not null)
                    arguments.Append($"-b:v {videoOutputStream.Bitrate} ");

                // Setups constant rate factor.
                if (videoOutputStream.ConstantRateFactor is not null)
                    arguments.Append($"-crf {videoOutputStream.ConstantRateFactor} ");

                // Setups video scaling.
                if (videoOutputStream.Width is not null && videoOutputStream.Height is not null)
                {
                    // Setups video resolution.
                    arguments.Append($"-vf scale={videoOutputStream.Width}:{videoOutputStream.Height} ");

                    // Setups scaling algorithm.
                    if (!videoOutputStream.ScalingAlgorithm.ToString().Equals(string.Empty))
                        arguments.Append($"-sws_flags {videoOutputStream.ScalingAlgorithm} ");
                }

                break;
            case StreamKind.Audio:
                var audioOutputStream = (AudioOutputStream)outputStream;

                // Setups audio codec.
                arguments.Append($"-c:a {audioOutputStream.Codec} ");

                // Sets audio sample rate.
                if (audioOutputStream.SampleRate is not null)
                    arguments.Append($"-ar {audioOutputStream.SampleRate} ");
                
                // Setups audio bitrate.
                if (audioOutputStream.Bitrate is not null)
                    arguments.Append($"-b:a {audioOutputStream.Bitrate} ");

                // Setups number of channels.
                if (audioOutputStream.NumberOfChannels is not null)
                    arguments.Append($"-ac {audioOutputStream.NumberOfChannels} ");

                break;
            case StreamKind.Subtitle:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return arguments;
    }

    public async Task<TranscodingJobResult> StartJobAsync(TranscodingJobConfiguration jobConfiguration, CancellationToken token,
        Action<TranscodingJobProgress> action)
    {
        // Setups ffmpeg input URI.
        var arguments = new StringBuilder();
        arguments.Append($"-y -progress - -nostats -i \"{jobConfiguration.InputUri}\" ");

        // Setups stream mapping and transcoding parameters
        arguments = jobConfiguration.OutputStreams.Aggregate(arguments,
            (current, outputStream) => BuildStreamString(outputStream, current));

        arguments.Append($"-strict -2 \"{jobConfiguration.OutputUri}\" ");

        var inputInfo = await _probeService.ProbeMediaPackage(jobConfiguration.InputUri);
        var duration = TimeSpan.FromSeconds(Math.Round(inputInfo.Duration));
        
        var result = await Cli.Wrap("ffmpeg")
            .WithArguments(arguments.ToString())
            .WithStandardOutputPipe(PipeTarget.ToDelegate(output =>
            {
                // Checks if the progress is complete.
                var progress = Regex.Match(output, "(?<=progress=).*").ToString();
                if (progress.Equals("complete")) return;

                var match = Regex.Match(output, "(?<=out_time=).*").ToString();
                if (match.Equals(string.Empty)) return;

                var outTime = TimeSpan.Parse(match);
                var percentage = (int)Math.Round(outTime.TotalSeconds / duration.TotalSeconds * 100);

                action.Invoke(new TranscodingJobProgress(duration, outTime, percentage));
                
            }))
            .ExecuteAsync(token);

        var fullCommand = $"ffmpeg {arguments}";
        return new TranscodingJobResult(result.StartTime.DateTime, result.ExitTime.DateTime, result.RunTime, fullCommand);
    }
}