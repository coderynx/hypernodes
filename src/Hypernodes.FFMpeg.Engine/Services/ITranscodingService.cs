using Hypernodes.Core.Transcoding;

namespace Hypernodes.FFMpeg.Engine.Services;

public interface ITranscodingService
{
    Task<TranscodingJobResult> StartJobAsync(TranscodingJobConfiguration jobConfiguration, CancellationToken token,
        Action<TranscodingJobProgress> action);
}