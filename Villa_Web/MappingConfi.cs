using AutoMapper;
using Villa_Web.Models.DTO;

namespace Villa_Web
{
    public class MappingConfi : Profile
    {
        public MappingConfi()
        {
            CreateMap<VillaDTO, VillaUpdateDTO>();
            CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdate>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdate>().ReverseMap();           
        }
    }
}
