using AutoMapper;
using Hypernodes.Core.Models;
using Hypernodes.Web.Api.Dtos;

namespace Hypernodes.Web.Api.MappingProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MediaObject, GetMediaObjectDto>();
    }
}
