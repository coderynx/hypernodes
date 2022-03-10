using Hypernodes.Core.Models.Streams;

namespace Hypernodes.Core.Transcoding.OutputStreams;

public interface IOutputVideoStreamSetInputStreamIndexStage
{
    IOutputVideoStreamSetOutputIndexStage WithInputStreamIndex(int index);
}

public interface IOutputVideoStreamSetOutputIndexStage
{
    IOutputVideoStreamSetCodecStage WithOutputStreamIndex(int index);
}

public interface IOutputVideoStreamSetCodecStage
{
    IOutputVideoStreamFinalStage WithCodec(VideoCodec codec);
}

public interface IOutputVideoStreamFinalStage
{
    IOutputVideoStreamFinalStage WithBitrate(int bitrate);
    IOutputVideoStreamFinalStage WithConstantRateFactor(int crf);
    IOutputVideoStreamFinalStage WithScaling(VideoScalingAlgorithm scalingAlgorithm, int width, int height);
    VideoOutputStream Build();
}

/// <summary>
/// Video transcoding definition.
/// </summary>
public class VideoOutputStream : IOutputStream, IOutputVideoStreamSetInputStreamIndexStage, IOutputVideoStreamSetOutputIndexStage,
    IOutputVideoStreamSetCodecStage, IOutputVideoStreamFinalStage
{
    private VideoOutputStream()
    {
        Kind = StreamKind.Video;
        ScalingAlgorithm = VideoScalingAlgorithm.None;
    }

    /// <summary>
    /// The kind of stream.
    /// </summary>
    public StreamKind Kind { get; set; }

    /// <summary>
    /// The input stream index.
    /// </summary>
    public int InputStreamIndex { get; set; }

    /// <summary>
    /// The output stream index.
    /// </summary>
    public int OutputStreamIndex { get; set; }

    /// <summary>
    /// Output video codec.
    /// </summary>
    public VideoCodec Codec { get; set; }

    /// <summary>
    /// Vide codec constant rate factor.
    /// </summary>
    public int? ConstantRateFactor { get; set; }

    /// <summary>
    /// Video stream resolution width.
    /// </summary>
    public int? Width { get; set; }

    /// <summary>
    /// Video stream resolution height.
    /// </summary>
    public int? Height { get; set; }

    /// <summary>
    /// Video output scaling.
    /// </summary>
    public VideoScalingAlgorithm ScalingAlgorithm { get; set; }

    /// <summary>
    /// Video output bitrate.
    /// </summary>
    public int? Bitrate { get; set; }

    public static IOutputVideoStreamSetInputStreamIndexStage CreateBuilder() => new VideoOutputStream();
    
    public IOutputVideoStreamSetOutputIndexStage WithInputStreamIndex(int index)
    {
        InputStreamIndex = index;
        return this;
    }

    public IOutputVideoStreamSetCodecStage WithOutputStreamIndex(int index)
    {
        OutputStreamIndex = index;
        return this;
    }
    
    public IOutputVideoStreamFinalStage WithCodec(VideoCodec codec)
    {
        Codec = codec;
        return this;
    }

    public IOutputVideoStreamFinalStage WithConstantRateFactor(int crf)
    {
        ConstantRateFactor = crf;
        return this;
    }
    
    public IOutputVideoStreamFinalStage WithBitrate(int bitrate)
    {
        Bitrate = bitrate;
        return this;
    }

    public IOutputVideoStreamFinalStage WithScaling(VideoScalingAlgorithm scalingAlgorithm, int width, int height)
    {
        ScalingAlgorithm = scalingAlgorithm;
        Width = width;
        Height = height;
        return this;
    }
    
    public VideoOutputStream Build()
    {
        return this;
    }
}