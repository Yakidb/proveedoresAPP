//                                                          //AUTHOR: Towa (EFA-Eliakim Flores).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: May 7, 2024. 

namespace proveedoresAPP.DTO
{
    //==================================================================================================================
    public class provProveedoresDto
    {
        //--------------------------------------------------------------------------------------------------------------
        //                                                  //This is a Data Transfer Object (DTO)

        //--------------------------------------------------------------------------------------------------------------
        /*INSTANCE VARIABLES*/
        public int IdProveedor { get; set; }
        public string? Codigo { get; set; }
        public string? RazonSocial { get; set; }
        public string? RFC { get; set; }
        public IList<ProdProductosDto> ProductosDto { get; set; }
        //--------------------------------------------------------------------------------------------------------------
    }
    //==================================================================================================================
}
/*END-TASK*/
