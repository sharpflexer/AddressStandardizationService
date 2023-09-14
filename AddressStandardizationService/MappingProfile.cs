using AutoMapper;

namespace AddressStandardizationService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddressRequestModel, DadataRequestModel>(); // Зависит от структуры Dadata API запроса
            CreateMap<DadataResponseModel, AddressResponseModel>();
        }
    }
}
