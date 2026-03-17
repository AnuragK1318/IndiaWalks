using AutoMapper;
using IndiaWalks.APi.Domain;
using IndiaWalks.APi.DTOs;

namespace IndiaWalks.APi.Mapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() 
        {
            #region RegionDomain
            //Map Domain to DTO and revers
            CreateMap<Region,RegionDto>().ReverseMap();
            CreateMap<Region,AddRegionRequestDto>().ReverseMap();
            CreateMap<Region,UpdateRegionRequestDto>().ReverseMap();
            #endregion
        }
    }
}
