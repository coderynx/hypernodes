using Hypernodes.Core.Models.Streams;

namespace Hypernodes.Core.Models;

public class MediaObject : BaseEntity
{
    /// <summary>
    /// The uri of the media object.
    /// </summary>
    public string Uri { get; set; }

    /// <summary>
    /// The name of the media object.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The duration of the media object.
    /// </summary>
    public double Duration { get; set; }

    /// <summary>
    /// The container of the media object.
    /// </summary>
    public string Container { get; set; }
    
    /// <summary>
    /// The streams contained in the media object.
    /// </summary>
    public ICollection<IStream> Streams { get; set; }
}