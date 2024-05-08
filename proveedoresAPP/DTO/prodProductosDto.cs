using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//                                                          //AUTHOR: Towa (EFA-Eliakim Flores).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: May 7, 2024. 

namespace proveedoresAPP.DTO
{
    //==================================================================================================================
    public class ProdProductosDto
    {
        //--------------------------------------------------------------------------------------------------------------
        //                                                  //This is a Data Transfer Object (DTO)

        //--------------------------------------------------------------------------------------------------------------
        /*INSTANCE VARIABLES*/
        public int IdProducto { get; set; }
        public int IdProveedor { get; set; }
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public string? Unidad { get; set; }
        public decimal? Costo { get; set; }

        //--------------------------------------------------------------------------------------------------------------
    }
    //==================================================================================================================
}
/*END-TASK*/
