using AutoMapper;
using TRMDesktopUI.Library.Models;
using TRMWebUI_NET4._7._2.Models;

namespace TRMWebUI_NET4._7._2.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ProductModel, ProductViewModel>();
        }
    }
}