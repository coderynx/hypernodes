namespace Hypernodes.Core.Transcoding;

/// <summary>
/// Audio codec mapping to string.
/// </summary>
public class AudioCodec
{
    private AudioCodec(string value) { _value = value; }
    
    public override string ToString() => _value;

    private readonly string _value;
    
    public static AudioCodec Aac => new("aac");
    public static AudioCodec Mp2 => new("mp2");
    public static AudioCodec Ac3 => new("ac3");
    public static AudioCodec Flac => new("flac");
    public static AudioCodec EAc3 => new("eac3");
    public static AudioCodec TrueHd => new("truehd");
    public static AudioCodec Dts => new("dts");

    // TODO: Handle other flags.
}