namespace Hypernodes.Core.Transcoding;

public record TranscodingJobProgress(TimeSpan Duration, TimeSpan OutTime, int Percentage);