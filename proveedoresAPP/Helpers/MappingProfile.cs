using AutoMapper;
using proveedoresAPP.DTO;
using proveedoresAPP.Models;

//                                                          //AUTHOR: Towa (EFA-Eliakim Flores).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: May 7, 2024.

namespace proveedoresAPP.Helpers
{
    //==================================================================================================================
    public class MappingProfile : Profile
    {
        //--------------------------------------------------------------------------------------------------------------
        /*CONSTRUCTORS*/

        //--------------------------------------------------------------------------------------------------------------
        public MappingProfile()
        {
            CreateMap<prodentityProductoEntityDB, ProdProductosDto>().ReverseMap();
            CreateMap<proventityProveedorEntityDB, provProveedoresDto>().ReverseMap();
        }

        //--------------------------------------------------------------------------------------------------------------
    }
    //==================================================================================================================
}
/*END-TASK*/
