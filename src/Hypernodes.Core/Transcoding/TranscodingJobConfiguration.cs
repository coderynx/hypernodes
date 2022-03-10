using Hypernodes.Core.Transcoding.OutputStreams;

namespace Hypernodes.Core.Transcoding;

public interface ISetInputMediaUri
{
    ISetOutputUriStage WithInputUri(string inputUri);
}

public interface ISetOutputUriStage
{
    IFinalStage WithOutputUri(string outputUri);
}

public interface IFinalStage
{
    IFinalStage AddOutputStream(IOutputStream outputStream);
    TranscodingJobConfiguration Build();
}

public class TranscodingJobConfiguration: ISetInputMediaUri, ISetOutputUriStage, IFinalStage
{
    private TranscodingJobConfiguration()
    {
        OutputStreams = new List<IOutputStream>();
    }

    /// <summary>
    /// The input media Uri.
    /// </summary>
    public string InputUri { get; set; }

    /// <summary>
    /// The output media Uri.
    /// </summary>
    public string OutputUri { get; set; }

    public List<IOutputStream> OutputStreams { get; set; }
    
    public static ISetInputMediaUri CreateBuilder() => new TranscodingJobConfiguration();
    
    public ISetOutputUriStage WithInputUri(string inputUri)
    {
        InputUri = inputUri;
        return this;
    }

    public IFinalStage WithOutputUri(string outputUri)
    {
        OutputUri = outputUri;
        return this;
    }

    public IFinalStage AddOutputStream(IOutputStream outputStream)
    {
        OutputStreams.Add(outputStream);
        return this;
    }

    public TranscodingJobConfiguration Build()
    {
        // Reorders the output streams.
        OutputStreams = OutputStreams.OrderBy(x => x.OutputStreamIndex).ToList();
        return this;
    }
}