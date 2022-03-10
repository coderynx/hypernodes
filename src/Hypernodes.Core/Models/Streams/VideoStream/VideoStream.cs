namespace Hypernodes.Core.Models.Streams.VideoStream;

/// <summary>
/// Video stream in a media package.
/// </summary>
public class VideoStream : BaseEntity, IStream
{
    /// <summary>
    /// The kind of the stream.
    /// </summary>
    public StreamKind Kind { get; set; }

    /// <summary>
    /// The index of the video stream.
    /// </summary>
    public int Index { get; set; }
    
    /// <summary>
    /// The codec of the video stream.
    /// </summary>
    public string Codec { get; set; }

    /// <summary>
    /// The aspect ratio of the video stream.
    /// </summary>
    public string AspectRatio { get; set; }

    /// <summary>
    /// The field order 
    /// </summary>
    public string FieldOrder { get; set; }
    
    /// <summary>
    /// The width of the video resolution.
    /// </summary>
    public int Width { get; set; }
    
    /// <summary>
    /// The height of the video resolution.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// The bitrate of the video stream.
    /// </summary>
    public int BitRate { get; set; }

    /// <summary>
    /// The duration of the video stream.
    /// </summary>
    public double Duration { get; set; }
}