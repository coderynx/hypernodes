using Hypernodes.Core.Models.Streams;

namespace Hypernodes.Core.Transcoding.OutputStreams;

public interface IAudioOutputStreamSetInputStreamIndexStage
{
    IAudioOutputStreamSetOutputStreamIndexStage WithInputStreamIndex(int index);
}

public interface IAudioOutputStreamSetOutputStreamIndexStage
{
    IAudioOutputStreamSetCodecStage WithOutputStreamIndex(int index);
}

public interface IAudioOutputStreamSetCodecStage
{
    IAudioOutputStreamFinalStage WithCodec(AudioCodec codec);
}

public interface IAudioOutputStreamFinalStage
{
    IAudioOutputStreamFinalStage WithNumberOfChannels(int number);
    IAudioOutputStreamFinalStage WithSampleRate(int sampleRate);
    IAudioOutputStreamFinalStage WithBitrate(int bitrate);
    AudioOutputStream Build();
}

public class AudioOutputStream : IOutputStream, IAudioOutputStreamSetInputStreamIndexStage, IAudioOutputStreamSetOutputStreamIndexStage,
    IAudioOutputStreamSetCodecStage, IAudioOutputStreamFinalStage
{
    private AudioOutputStream()
    {
        Kind = StreamKind.Audio;
    }
    
    public StreamKind Kind { get; set; }
    public int InputStreamIndex { get; set; }
    public int OutputStreamIndex { get; set; }
    public AudioCodec Codec { get; set; }
    public int? NumberOfChannels { get; set; }
    public int? SampleRate { get; set; }
    public int? Bitrate { get; set; }

    public static IAudioOutputStreamSetInputStreamIndexStage CreateBuilder() => new AudioOutputStream();

    IAudioOutputStreamSetOutputStreamIndexStage IAudioOutputStreamSetInputStreamIndexStage.WithInputStreamIndex(int index)
    {
        InputStreamIndex = index;
        return this;
    }

    public IAudioOutputStreamSetCodecStage WithOutputStreamIndex(int index)
    {
        OutputStreamIndex = index;
        return this;
    }

    public IAudioOutputStreamFinalStage WithCodec(AudioCodec codec)
    {
        Codec = codec;
        return this;
    }

    public IAudioOutputStreamFinalStage WithNumberOfChannels(int number)
    {
        NumberOfChannels = number;
        return this;
    }

    public IAudioOutputStreamFinalStage WithSampleRate(int sampleRate)
    {
        SampleRate = sampleRate;
        return this;
    }

    public IAudioOutputStreamFinalStage WithBitrate(int bitrate)
    {
        Bitrate = bitrate;
        return this;
    }

    public AudioOutputStream Build() => this;
}