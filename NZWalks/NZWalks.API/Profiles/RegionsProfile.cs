using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class RegionsProfile : Profile
    {

        public RegionsProfile()
        {
            CreateMap<Models.Region, DTO.Region>();
        }
    }
}
