using Hypernodes.Core.Models;
using Orleans;

namespace Hypernodes.Core.GrainInterfaces;

public interface IParserJobGrain : IGrainWithGuidKey
{
    Task<MediaObject> ParseMediaObject(string uri);
}