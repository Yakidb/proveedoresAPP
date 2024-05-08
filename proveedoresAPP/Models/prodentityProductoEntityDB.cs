using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//                                                          //AUTHOR: Towa (EFA-Eliakim Flores).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: May 07, 2024.

namespace proveedoresAPP.Models
{
    [Table("Productos")]
    //==================================================================================================================
    public class prodentityProductoEntityDB
    {
        //--------------------------------------------------------------------------------------------------------------
        /*PRIMARY KEY*/
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProducto { get; set; }

        //--------------------------------------------------------------------------------------------------------------
        /*COLUMNS*/
        public string? Codigo { get; set; }

        public string? Descripcion { get; set; }

        public string? Unidad { get; set; }

        public decimal? Costo { get; set; }

        //--------------------------------------------------------------------------------------------------------------
        /*FOREIGN KEYS*/
        [ForeignKey("IdProveedor")]
        public int IdProveedor { get; set; }
        public virtual proventityProveedorEntityDB? ProventityProveedorEntity { get; set; }

        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
}
/*END-TASK*/

