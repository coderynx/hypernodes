using System.Reflection;
using AutoMapper;
using Hypernodes.Core.Models;
using HyperNodes.DataAccess.Repositories;
using Hypernodes.Web.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Hypernodes.Web.Api.Endpoints;

public static class MediaObjectsApi
{
    public static void MapMediaObjectsApi(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/mediaobjects",
                async ([FromServices] IMediaObjectsRepository repository, [FromServices] IMapper mapper) =>
                {
                    var result = await repository.GetMediaObjectsAsync();
                    return Results.Ok(mapper.Map<List<GetMediaObjectDto>>(result));
                })
            .WithName("GetMediaObjects")
            .WithDisplayName("Get media objects");

        app.MapGet("/api/mediaobjects/{id}",
                async ([FromServices] IMediaObjectsRepository repository, [FromServices] IMapper mapper,
                    [FromQuery] Guid id) =>
                {
                    var result = await repository.GetMediaObjectAsync(id);
                    return Results.Ok(mapper.Map<GetMediaObjectDto>(result));
                })
            .WithName("GetMediaObject")
            .WithDisplayName("Get media object");

        app.MapPost("/api/mediaobjects/{id}",
                async ([FromServices] IMediaObjectsRepository repository, [FromServices] IMapper mapper, HttpRequest httpRequest) =>
                {
                    if (!httpRequest.HasFormContentType)
                        return Results.BadRequest();

                    var form = await httpRequest.ReadFormAsync();
                    var formFile = form.Files["file"];

                    if (formFile is null || formFile.Length == 0)
                        return Results.BadRequest();

                    // TODO: Implement S3 storage.
                    // Writes form file to disk.
                    var applicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    var filePath = Path.Combine(applicationPath, formFile.FileName);
                    await using var fileStream = new FileStream(filePath, FileMode.Create);
                    await formFile.CopyToAsync(fileStream);

                    var mediaObject = new MediaObject
                    {
                        Name = Path.GetFileNameWithoutExtension(formFile.Name),
                        Uri = applicationPath
                    };

                    var dto = mapper.Map<GetMediaObjectDto>(mediaObject);
                    return Results.Ok(dto);
                })
            .WithName("PostMediaObject")
            .WithDisplayName("Post media object");

        app.MapDelete("/api/mediaobjects/{id}", async ([FromServices] IMediaObjectsRepository repository,
                [FromServices] IMapper mapper,
                [FromQuery] Guid id) =>
            {
                await repository.RemoveMediaObjectAsync(id);
                return Results.Ok();
            })
            .WithName("DeleteMediaObject")
            .WithDisplayName("Delete media object");
    }
}