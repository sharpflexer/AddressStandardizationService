using AddressStandardizationService.Models;
using AutoMapper;

namespace AddressStandardizationService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, ShortAddress>();
            CreateMap<Address, GeoData>();
        }
    }
}
