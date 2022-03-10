namespace Hypernodes.Core.Transcoding;

/// <summary>
/// Video codec mapping to string.
/// </summary>
public class VideoCodec
{
    private VideoCodec(string value) { _value = value; }
    
    public override string ToString() => _value;

    private readonly string _value;
    
    public static VideoCodec H264 => new("h264");
    public static VideoCodec Av1 => new("av1");
    public static VideoCodec Hevc => new("hevc");
    public static VideoCodec Mpeg2Video => new("mpeg2video");

    // TODO: Handle other flags.
}