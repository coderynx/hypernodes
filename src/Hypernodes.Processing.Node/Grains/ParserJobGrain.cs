using Hypernodes.Core.GrainInterfaces;
using Hypernodes.Core.Models;
using Hypernodes.FFMpeg.Engine.Services;
using Orleans;

namespace Hypernodes.Processing.Node.Grains;

public class ParserJobGrain : Grain, IParserJobGrain
{
    public ParserJobGrain(IProbeService probeService)
    {
        _probeService = probeService;
    }

    private readonly IProbeService _probeService;
    
    public async Task<MediaObject> ParseMediaObject(string uri)
    {
        return await _probeService.ProbeMediaPackage(uri);
    }
}