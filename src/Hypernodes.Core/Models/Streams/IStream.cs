namespace Hypernodes.Core.Models.Streams;

public enum StreamKind
{
    Video,
    Audio,
    Subtitle
}

/// <summary>
/// StreamKind contained in a media package.
/// </summary>
public interface IStream
{
    public StreamKind Kind { get; set; }
    public int Index { get; set; }
    public double Duration { get; set; }
}