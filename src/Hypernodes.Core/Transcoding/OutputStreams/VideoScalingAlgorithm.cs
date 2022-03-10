namespace Hypernodes.Core.Transcoding.OutputStreams;

/// <summary>
/// Video scaling algorithm mapping to string.
/// </summary>
public class VideoScalingAlgorithm
{
    private VideoScalingAlgorithm(string value) { _value = value; }
    
    public override string ToString() => _value;

    private readonly string _value;
    
    public static VideoScalingAlgorithm None => new(string.Empty);
    public static VideoScalingAlgorithm FastBilinear => new("fast_bilinear");
    public static VideoScalingAlgorithm Bilinear => new("bilinear");
    public static VideoScalingAlgorithm Bicubic => new("bicubic");
    public static VideoScalingAlgorithm Experimental => new("experimental");
    public static VideoScalingAlgorithm Neighbor => new("neighbor");
    public static VideoScalingAlgorithm Area => new("area");
    public static VideoScalingAlgorithm BicubicBilinear => new("bicublin");
    public static VideoScalingAlgorithm Gauss => new("gauss");
    public static VideoScalingAlgorithm Sinc => new("sinc");
    public static VideoScalingAlgorithm Lanczos => new("lanczos");
    public static VideoScalingAlgorithm Spline => new("spline");
    
    // TODO: Handle other flags.
}