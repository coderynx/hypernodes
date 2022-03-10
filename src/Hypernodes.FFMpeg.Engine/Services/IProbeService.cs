using Hypernodes.Core.Models;

namespace Hypernodes.FFMpeg.Engine.Services;

public interface IProbeService
{
    Task<MediaObject> ProbeMediaPackage(string uri);
}