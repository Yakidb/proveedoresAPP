using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using proveedoresAPP.Data;
using proveedoresAPP.DTO;
using proveedoresAPP.Enums;
using proveedoresAPP.Helpers;
using proveedoresAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//                                                          //AUTHOR: Towa (EFA-Eliakim Flores).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: May 7, 2023. 

namespace proveedoresAPP.DAO
{
    //==================================================================================================================
    public class ProductosDao
    {
		//--------------------------------------------------------------------------------------------------------------
		//                                                  //This is a Data Access Object (DAO)

		//--------------------------------------------------------------------------------------------------------------
		/*CONSTANTS*/

		//--------------------------------------------------------------------------------------------------------------
		/*INITIALIZER*/

		//--------------------------------------------------------------------------------------------------------------
        /*INSTANCE VARIABLES*/
        
        private readonly ProveedoresDbContext proveedoresDbContext_Z;
        private readonly IMapper mapper_Z;
        private readonly ILogger<ProductosDao> logger_Z;

        //--------------------------------------------------------------------------------------------------------------
        /*COMPUTED VARIABLES*/

        //--------------------------------------------------------------------------------------------------------------
        /*METHODS TO SUPPORT COMPUTED VARIABLES*/

        //-------------------------------------------------------------------------------------------------------------
        /*OBJECT CONSTRUCTORS*/

        //-------------------------------------------------------------------------------------------------------------
        public ProductosDao(
            ProveedoresDbContext provDbContext_I, 
            IMapper mapper_I,
            ILogger<ProductosDao> logger_I
            )
        {
            this.proveedoresDbContext_Z = provDbContext_I;
            this.mapper_Z = mapper_I;
            this.logger_Z = logger_I;
        }

        //-------------------------------------------------------------------------------------------------------------
        /*TRANSFORMATION METHODS*/

        //-------------------------------------------------------------------------------------------------------------
        public void subAddProducto(
            //                                              //Agregar producto a base de datos
            prodentityProductoEntityDB prodEntity_I,
            out RcdoperEnum rcdoperEnum_IO,
            out string strMensajeCustomizado_IO
            )
        {
            rcdoperEnum_IO = RcdoperEnum.Z_NULL;
            strMensajeCustomizado_IO = " ";
            
                try
                {
                    if (
                        //                                      //Validar que el costo sea decimal  
                        this.boolIsValidEntity(prodEntity_I, out rcdoperEnum_IO, out strMensajeCustomizado_IO)
                        )
                    {
                        this.proveedoresDbContext_Z.Add(prodEntity_I);
                        this.proveedoresDbContext_Z.SaveChanges();
                        rcdoperEnum_IO = RcdoperEnum.SUCESS;
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProductosDao), 
                        $"Exepcion de concurrencia atrapada: {ex.Message}", "*");
                    rcdoperEnum_IO = RcdoperEnum.CONCURRENCY_ISSUE;
                }
                catch (DbUpdateException ex)
                {
                    this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProductosDao), 
                        $"Error al guardar registro en la base de datos: {ex.Message}", "*");
                    rcdoperEnum_IO = RcdoperEnum.FAILURE;
                }
                catch (Exception ex)
                {
                    this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProductosDao), 
                        $"Error desconocido ocurrido: {ex.Message}", 
                        "*");
                    //dbTransaction.Rollback();
                    rcdoperEnum_IO = RcdoperEnum.UNKNOWN_ERROR;
                }
            
        }

        //--------------------------------------------------------------------------------------------------------------
        public bool boolIsValidEntity(
            //                                              //Validar campos de la entity mediante reglas de negocio
            prodentityProductoEntityDB prodEntity_I,
            out RcdoperEnum rcdoperEnum_I,
            out string strMensajeCustomizado_IO
            )
        {
            strMensajeCustomizado_IO = " "; 
            rcdoperEnum_I = RcdoperEnum.Z_NULL;
            proventityProveedorEntityDB? provEntityExistente = null;

            if (!decimal.TryParse(prodEntity_I.Costo.ToString(), out _))
            {
                strMensajeCustomizado_IO = "El costo debe ser un valor decimal.";
                this.logger_Z.LogWarningWrapped(BorderEnum.BorderBoth, typeof(ProductosDao),
                    strMensajeCustomizado_IO, "*");
                rcdoperEnum_I = RcdoperEnum.FIELD_INVALID;
                return false;
            }

            return true;
        }

        //--------------------------------------------------------------------------------------------------------------
        //                                                  //ACCESS METHODS.

        //--------------------------------------------------------------------------------------------------------------
        public List<prodentityProductoEntityDB> GetAllRecords()
        {
            return this.proveedoresDbContext_Z.Productos.Include(p => p.ProventityProveedorEntity).ToList();
        }                                       
        
        
        //-------------------------------------------------------------------------------------------------------------
    }
    //==================================================================================================================
}
/*END-TASK*/

