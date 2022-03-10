using Hypernodes.Core.Models;

namespace HyperNodes.DataAccess.Repositories;

public interface IMediaObjectsRepository
{
    Task<List<MediaObject>> GetMediaObjectsAsync();
    Task<MediaObject?> GetMediaObjectAsync(Guid id);
    Task<MediaObject> SaveMediaObjectAsync(MediaObject mediaObject);
    Task<MediaObject> UpdateMediaObjectAsync(Guid id, MediaObject mediaObject);
    Task RemoveMediaObjectAsync(Guid id);
}