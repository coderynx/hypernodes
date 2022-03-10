namespace Hypernodes.Core.Transcoding;

public record TranscodingJobResult(DateTime StartTime, DateTime EndTime, TimeSpan RunningTime, string FullCommand);