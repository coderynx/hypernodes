using AutoMapper;
using Hypernodes.Core.Models.Streams.AudioStream;
using Hypernodes.Core.Models.Streams.VideoStream;
using Hypernodes.FFMpeg.Engine.Probe;

namespace Hypernodes.FFMpeg.Engine.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProbeStream, VideoStream>();
        CreateMap<ProbeStream, AudioStream>();
    }
}