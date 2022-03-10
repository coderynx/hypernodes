using Hypernodes.Core.GrainInterfaces;
using Hypernodes.Core.Transcoding;
using Hypernodes.FFMpeg.Engine.Services;
using Orleans;

namespace Hypernodes.Processing.Node.Grains;

public class TranscodingJobGrain : Grain, ITranscodingJobGrain
{
    public TranscodingJobGrain(ITranscodingService transcodingService, ILogger<TranscodingJobGrain> logger)
    {
        _transcodingService = transcodingService;
        _logger = logger;
    }

    private readonly ITranscodingService _transcodingService;
    private readonly ILogger<TranscodingJobGrain> _logger;

    public async Task<TranscodingJobResult> StartJobAsync(TranscodingJobConfiguration configuration,
        GrainCancellationToken token)
    {
        _logger.LogInformation("Started transcoding of job {Id}", this.GetPrimaryKey());

        var result = await _transcodingService.StartJobAsync(configuration, token.CancellationToken, test =>
        {
            _logger.LogInformation("Transcoding job {Id} progress: {Percentage}", this.GetPrimaryKey(),
                test.Percentage);
        });

        _logger.LogInformation("Completed transcoding job {Id} with a running time of {RunningTime}",
            this.GetPrimaryKey(), result.RunningTime);
        return result;
    }
}