namespace Hypernodes.Core.Models.Streams.AudioStream;


public class AudioStream : BaseEntity, IStream
{
    public StreamKind Kind { get; set; }
    public int Index { get; set; }
    public double Duration { get; set; }
    public string Codec { get; set; }
    public string SampleFmt { get; set; }
    public int SampleRate { get; set; }
    public int NumberOfChannels { get; set; }
    public string ChannelLayout { get; set; }
    public int Bitrate { get; set; }
}