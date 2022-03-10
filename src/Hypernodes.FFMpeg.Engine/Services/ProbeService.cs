using AutoMapper;
using CliWrap;
using CliWrap.Buffered;
using Hypernodes.Core.Models;
using Hypernodes.Core.Models.Streams;
using Hypernodes.Core.Models.Streams.AudioStream;
using Hypernodes.Core.Models.Streams.VideoStream;
using Hypernodes.FFMpeg.Engine.Probe;
using Newtonsoft.Json;

namespace Hypernodes.FFMpeg.Engine.Services;

/// <summary>
/// Probe engine.
/// </summary>
public class ProbeService : IProbeService
{
    public ProbeService(IMapper mapper)
    {
        _mapper = mapper;
    }

    private readonly IMapper _mapper;
    
    private IStream ParseStream(ProbeStream probeStream)
    { 
        IStream stream = null;
        switch (probeStream.CodecType)
        {
            // StreamKind is audio.
            case "video":
            {
                stream = _mapper.Map<ProbeStream, VideoStream>(probeStream);
                stream.Kind = StreamKind.Video;
                break;
            }
                
            // StreamKind is video.
            case "audio":
            {
                stream = _mapper.Map<ProbeStream, AudioStream>(probeStream);
                stream.Kind = StreamKind.Subtitle;
                break;
            }
                
            // StreamKind is subtitle.
            case "subtitle":
                break;
        }

        return stream;
    }
    
    public async Task<MediaObject> ProbeMediaPackage(string uri)
    {
        // Run FFProbe on requested URI.
        var executable = await Cli.Wrap("ffprobe")
            .WithArguments($"-v quiet -print_format json -show_format -show_streams \"{uri}\" ")
            .ExecuteBufferedAsync();

        // Deserialized media info.
        var result = JsonConvert.DeserializeObject<ProbeMediaInfo>(executable.StandardOutput);

        // Creates the media package.
        return new MediaObject
        {
            Container = result.Format.FormatName,
            Duration = result.Format.Duration,
            Streams = result.Streams.Select(ParseStream).ToList(),
            Uri = uri
        };
    }
}