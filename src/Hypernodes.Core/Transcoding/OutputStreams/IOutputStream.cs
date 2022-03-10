using Hypernodes.Core.Models.Streams;

namespace Hypernodes.Core.Transcoding.OutputStreams;

public interface IOutputStream
{
    public StreamKind Kind { get; set; }
    public int InputStreamIndex { get; set; }
    public int OutputStreamIndex { get; set; }
}