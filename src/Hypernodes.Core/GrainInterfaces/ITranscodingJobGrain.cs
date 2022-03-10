using Hypernodes.Core.Transcoding;
using Orleans;

namespace Hypernodes.Core.GrainInterfaces;

public interface ITranscodingJobGrain : IGrainWithGuidKey
{
    Task<TranscodingJobResult> StartJobAsync(TranscodingJobConfiguration configuration, GrainCancellationToken token);
}