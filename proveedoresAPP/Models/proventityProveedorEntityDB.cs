using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//                                                          //AUTHOR: Towa (EFA-Eliakim Flores).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: May 07, 2024.

namespace proveedoresAPP.Models
{ 

    [Table("Proveedores")]
    //==================================================================================================================
    public class proventityProveedorEntityDB
    {
        //--------------------------------------------------------------------------------------------------------------
        /*PRIMARY KEY*/
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProveedor { get; set; }

        //--------------------------------------------------------------------------------------------------------------
        /*COLUMNS*/
        public string? Codigo { get; set; }

        public string? RazonSocial { get; set; }

        public string? RFC { get; set; }

        public virtual ICollection<prodentityProductoEntityDB> Productos { get; set; }
        //--------------------------------------------------------------------------------------------------------------
    }

    //==================================================================================================================
}
/*END-TASK*/
