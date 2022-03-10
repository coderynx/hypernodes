using Hypernodes.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HyperNodes.DataAccess.Repositories;

public class MediaObjectsRepository : IMediaObjectsRepository
{
    public MediaObjectsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private readonly ApplicationDbContext _dbContext;

    public async Task<List<MediaObject>> GetMediaObjectsAsync()
    {
        var result = await _dbContext.MediaObjects.ToListAsync();
        return result;
    }
    
    public async Task<MediaObject?> GetMediaObjectAsync(Guid id)
    {
        var result = await _dbContext.MediaObjects.FindAsync(id);
        return result;
    }

    public async Task<MediaObject> SaveMediaObjectAsync(MediaObject mediaObject)
    {
        var result = await _dbContext.MediaObjects.AddAsync(mediaObject);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<MediaObject> UpdateMediaObjectAsync(Guid id, MediaObject mediaObject)
    {
        if (id != mediaObject.Id) throw new ArgumentException("The given media objects ids are not the same");

        var entity = await _dbContext.MediaObjects.FindAsync(id);
        if (entity is null) throw new NullReferenceException("Cannot find a media object with the give id");
        
        entity = mediaObject;
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task RemoveMediaObjectAsync(Guid id)
    {
        var entity = await _dbContext.MediaObjects.FindAsync(id);
        if (entity is null) throw new NullReferenceException("Cannot find a media object with the give id");

        _dbContext.MediaObjects.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}