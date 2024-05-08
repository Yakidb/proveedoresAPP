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
//                                                          //DATE: October 12, 2023. 

namespace proveedoresAPP.DAO
{
    //==================================================================================================================
    public class ProveedoresDao
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
        private readonly ILogger<ProveedoresDao> logger_Z;

        //--------------------------------------------------------------------------------------------------------------
        /*COMPUTED VARIABLES*/

        //--------------------------------------------------------------------------------------------------------------
        /*METHODS TO SUPPORT COMPUTED VARIABLES*/

        //-------------------------------------------------------------------------------------------------------------
        /*OBJECT CONSTRUCTORS*/

        //-------------------------------------------------------------------------------------------------------------
        public ProveedoresDao(
            ProveedoresDbContext provDbContext_I, 
            IMapper mapper_I,
            ILogger<ProveedoresDao> logger_I
            )
        {
            this.proveedoresDbContext_Z = provDbContext_I;
            this.mapper_Z = mapper_I;
            this.logger_Z = logger_I;
        }

        //--------------------------------------------------------------------------------------------------------------
        /*METHODS TO SUPPORT CONSTRUCTORS*/

        //-------------------------------------------------------------------------------------------------------------
        /*TRANSFORMATION METHODS*/

        //-------------------------------------------------------------------------------------------------------------
        public void subAddProveedor(
            //                                              //Agregar proveedor a base de datos
            proventityProveedorEntityDB provEntity_I,
            out RcdoperEnum rcdoperEnum_IO,
            out string strMensajeCustomizado_IO
            )
        {
            rcdoperEnum_IO = RcdoperEnum.Z_NULL;
            strMensajeCustomizado_IO = " ";
            
                try
                {
                    if (
                        //                                      //Si el codigo no está en uso 
                        this.boolIsValidEntity(provEntity_I, out rcdoperEnum_IO, out strMensajeCustomizado_IO)
                        )
                    {
                        this.proveedoresDbContext_Z.Proveedores.Add(provEntity_I);
                        this.proveedoresDbContext_Z.SaveChanges();
                        rcdoperEnum_IO = RcdoperEnum.SUCESS;
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao), 
                        $"Exepcion de concurrencia atrapada: {ex.Message}", "*");
                    //dbTransaction.Rollback();
                    rcdoperEnum_IO = RcdoperEnum.CONCURRENCY_ISSUE;
                }
                catch (DbUpdateException ex)
                {
                    this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao), 
                        $"Error al guardar registro en la base de datos: {ex.Message}", "*");
                    //.Rollback();
                    rcdoperEnum_IO = RcdoperEnum.FAILURE;
                }
                catch (Exception ex)
                {
                    this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao), $"Error desconocido ocurrido: {ex.Message}", 
                        "*");
                    //dbTransaction.Rollback();
                    rcdoperEnum_IO = RcdoperEnum.UNKNOWN_ERROR;
                }
            
        }

        //-------------------------------------------------------------------------------------------------------------
        public void subEditProveedor(
            //                                              //Agregar proveedor a base de datos
            int id_I,
            proventityProveedorEntityDB provEntity_I,
            out RcdoperEnum rcdoperEnum_IO,
            out string strMensajeCustomizado_IO
            )
        {
            rcdoperEnum_IO = RcdoperEnum.Z_NULL;
            strMensajeCustomizado_IO = " ";

            try
            {
                if (
                    //                                      //Si el codigo no está en uso 
                    this.boolIsValidEntityInEdit(id_I,provEntity_I, out rcdoperEnum_IO, out strMensajeCustomizado_IO)
                    )
                {
                    this.proveedoresDbContext_Z.Update(provEntity_I);
                    this.proveedoresDbContext_Z.SaveChanges();
                    rcdoperEnum_IO = RcdoperEnum.SUCESS;
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao),
                    $"Exepcion de concurrencia atrapada: {ex.Message}", "*");
                //dbTransaction.Rollback();
                rcdoperEnum_IO = RcdoperEnum.CONCURRENCY_ISSUE;
            }
            catch (DbUpdateException ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao),
                    $"Error al guardar registro en la base de datos: {ex.Message}", "*");
                //.Rollback();
                rcdoperEnum_IO = RcdoperEnum.FAILURE;
            }
            catch (Exception ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao), $"Error desconocido ocurrido: {ex.Message}",
                    "*");
                //dbTransaction.Rollback();
                rcdoperEnum_IO = RcdoperEnum.UNKNOWN_ERROR;
            }

        }

        //--------------------------------------------------------------------------------------------------------------
        public bool boolIsValidEntityInEdit(
            //                                              //Validar campos de la entity mediante reglas de negocio
            int id_I,
            proventityProveedorEntityDB proveEntity_I,
            out RcdoperEnum rcdoperEnum_I,
            out string strMensajeCustomizado_IO
            )
        {
            strMensajeCustomizado_IO = " ";
            rcdoperEnum_I = RcdoperEnum.Z_NULL;
            proventityProveedorEntityDB? provEntityExistente = null;

            if (
                //                                          //Codigo es requerido
                proveEntity_I.Codigo.IsNullOrEmpty()
                )
            {
                strMensajeCustomizado_IO = $"Codigo no fue escrito para el proveedor";
                this.logger_Z.LogWarningWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao), strMensajeCustomizado_IO, "*");
                rcdoperEnum_I = RcdoperEnum.MISSING_FIELDS;
                return false;
            }
            else
            {
                provEntityExistente = this.proventityGetRecordByCode(proveEntity_I.Codigo, out rcdoperEnum_I);
                if(provEntityExistente != null)
                    this.proveedoresDbContext_Z.Entry(provEntityExistente).State = EntityState.Detached; // Desasociar entidad recuperada
            }

            if (
                //                                          //Razon social es requerida
                proveEntity_I.RazonSocial.IsNullOrEmpty()
                )
            {
                strMensajeCustomizado_IO = $"La razon social no fue escrita para el proveedor: {proveEntity_I.Codigo}";
                this.logger_Z.LogWarningWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao), strMensajeCustomizado_IO, "*");
                rcdoperEnum_I = RcdoperEnum.MISSING_FIELDS;
                return false;
            }

            if(
                //                                          //Se debe editar el proveedor elegido
                proveEntity_I.IdProveedor != id_I
                )
            {
                strMensajeCustomizado_IO = $"Estas editando un proveedor equivocado: {proveEntity_I.Codigo}";
                this.logger_Z.LogWarningWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao), strMensajeCustomizado_IO, "*");
                rcdoperEnum_I = RcdoperEnum.FAILURE;
                return false;
            }

            if (
                //                                          //No es valido que haya un proveedor con el mismo codigo al editar
                provEntityExistente != null &&
                provEntityExistente.IdProveedor != id_I
                )
            {
                strMensajeCustomizado_IO = $"Codigo actualmente está en uso: {proveEntity_I.Codigo}";
                this.logger_Z.LogWarningWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao), strMensajeCustomizado_IO, "*");
                rcdoperEnum_I = RcdoperEnum.CODE_ALREADY_EXISTS;
                return false;
            }

            if (
                //                                          //Validar: El RFC del proveedor debe tener
                //                                          //13 caracteres (4 letras + 6 dígitos + 1 letra + 2 dígitos)
                !boolIsValidRFC(proveEntity_I.RFC)
                )
            {
                strMensajeCustomizado_IO = $"El RFC es invalido: {proveEntity_I.RFC}";
                this.logger_Z.LogWarningWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao), strMensajeCustomizado_IO, "*");
                rcdoperEnum_I = RcdoperEnum.RFC_INVALID;
                return false;
            }
            return true;
        }

        //--------------------------------------------------------------------------------------------------------------
        public bool boolIsValidEntity(
            //                                              //Validar campos de la entity mediante reglas de negocio
            proventityProveedorEntityDB proveEntity_I,
            out RcdoperEnum rcdoperEnum_I,
            out string strMensajeCustomizado_IO
            )
        {
            strMensajeCustomizado_IO = " "; 
            rcdoperEnum_I = RcdoperEnum.Z_NULL;
            proventityProveedorEntityDB? provEntityExistente = null;

            if (
                //                                          //Codigo es requerido
                proveEntity_I.Codigo.IsNullOrEmpty()
                )
            {
                strMensajeCustomizado_IO = $"Codigo no fue escrito para el proveedor";
                this.logger_Z.LogWarningWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao), strMensajeCustomizado_IO, "*");
                rcdoperEnum_I = RcdoperEnum.MISSING_FIELDS;
                return false;
            }
            else
            {
                provEntityExistente = this.proventityGetRecordByCode(proveEntity_I.Codigo, out rcdoperEnum_I);
            }

            if (
                //                                          //No es valido que haya un proveedor con el mismo codigo
                provEntityExistente != null
                )
            {
                strMensajeCustomizado_IO = $"Codigo actualmente está en uso: {proveEntity_I.Codigo}";
                this.logger_Z.LogWarningWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao), strMensajeCustomizado_IO, "*");
                rcdoperEnum_I = RcdoperEnum.CODE_ALREADY_EXISTS;
                return false;
            }

            if (
                //                                          //Validar: El RFC del proveedor debe tener
                //                                          //13 caracteres (4 letras + 6 dígitos + 1 letra + 2 dígitos)
                !boolIsValidRFC(proveEntity_I.RFC)
                )
            {
                strMensajeCustomizado_IO = $"El RFC es invalido: {proveEntity_I.RFC}";
                this.logger_Z.LogWarningWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao), strMensajeCustomizado_IO, "*");
                rcdoperEnum_I = RcdoperEnum.RFC_INVALID;
                return false;
            }
            return true;
        }

        //--------------------------------------------------------------------------------------------------------------
        private bool boolIsValidRFC(string strRFC_I)
        {
            const string rfcRegex = @"^[A-Z]{4}[0-9]{6}[A-Z]{1}[0-9]{2}$";
            return Regex.IsMatch(strRFC_I, rfcRegex);
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subDeleteProveedor(
            proventityProveedorEntityDB proventity_I,
            out RcdoperEnum rcdoperEnum_IO,
            out string strMensajeCustomizado_IO
            )
        {
            rcdoperEnum_IO = RcdoperEnum.Z_NULL;
            strMensajeCustomizado_IO = " ";

            try
            {
                this.proveedoresDbContext_Z.Remove(proventity_I);
                this.proveedoresDbContext_Z.SaveChanges();
                strMensajeCustomizado_IO = $"Proveedor borrado exitosamente: {strMensajeCustomizado_IO}";
                rcdoperEnum_IO = RcdoperEnum.SUCESS;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao),
                    $"Exepcion de concurrencia atrapada: {ex.Message}", "*");
                //dbTransaction.Rollback();
                rcdoperEnum_IO = RcdoperEnum.CONCURRENCY_ISSUE;
            }
            catch (Exception ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao),
                    $"Error desconocido ocurrido: {ex.Message}",
                    "*");
                //dbTransaction.Rollback();
                rcdoperEnum_IO = RcdoperEnum.UNKNOWN_ERROR;
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        //                                                  //ACCESS METHODS.

        //--------------------------------------------------------------------------------------------------------------
        public proventityProveedorEntityDB? proventityGetRecordByCode(
            //                                              //Encontrar proveedor por coddigo
            String strCodigoProveedor_I,
            out RcdoperEnum rcdoperEnum_IO
            )
        {
            try
            {
                rcdoperEnum_IO = RcdoperEnum.Z_NULL;
                proventityProveedorEntityDB proventity = this.proveedoresDbContext_Z.Proveedores
                    .Include(b => b.Productos)
                    .FirstOrDefault(t => t.Codigo == strCodigoProveedor_I);
                return proventity;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.logger_Z.LogWarning($"[{nameof(ProveedoresDao)} - {nameof(proventityGetRecordByCode)}] - " +
                    $"Exepcion de concurrencia atrapada: {ex.Message}");
                rcdoperEnum_IO = RcdoperEnum.CONCURRENCY_ISSUE;
                return null;
            }
            catch (Exception ex)
            {
                this.logger_Z.LogError($"[{nameof(ProveedoresDao)} - {nameof(proventityGetRecordByCode)}] - " +
                    $"Error desconocido ocurrido: {ex.Message}");
                rcdoperEnum_IO = RcdoperEnum.UNKNOWN_ERROR;
                return null;
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        public proventityProveedorEntityDB? proventityGetRecordById(
            //                                              //Encontrar proveedor por coddigo
            int? id_I,
            out RcdoperEnum rcdoperEnum_IO,
            out string strMensajeCustomizado_IO
            )
        {
            proventityProveedorEntityDB proventity = null;
            try
            {
                rcdoperEnum_IO = RcdoperEnum.Z_NULL;
                strMensajeCustomizado_IO = " ";
                 
                if (
                    id_I.Value != null
                    )
                {
                    proventity = this.proveedoresDbContext_Z.Proveedores
                    .Include(b => b.Productos)
                    .FirstOrDefault(t => t.IdProveedor == id_I);
                    return proventity;
                }
                else
                {
                    strMensajeCustomizado_IO = "Error al encontrar el proveedor a borrar";
                    this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao), strMensajeCustomizado_IO,
                        "*");
                    rcdoperEnum_IO = RcdoperEnum.FAILURE;
                    return proventity;
                }

            }
            catch (DbUpdateConcurrencyException ex)
            {
                strMensajeCustomizado_IO = $"Exepcion de concurrencia atrapada: {ex.Message}";
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao),
                    strMensajeCustomizado_IO, "*");
                rcdoperEnum_IO = RcdoperEnum.CONCURRENCY_ISSUE;
                return proventity;
            }
            catch (Exception ex)
            {
                strMensajeCustomizado_IO = $"Exepcion de concurrencia atrapada: {ex.Message}";
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao),
                    strMensajeCustomizado_IO, "*");
                rcdoperEnum_IO = RcdoperEnum.UNKNOWN_ERROR;
                return proventity;
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        public List<proventityProveedorEntityDB> GetAllRecords()
        {
            return this.proveedoresDbContext_Z.Proveedores.ToList();
        }                                       
        
        
        //-------------------------------------------------------------------------------------------------------------
    }
    //==================================================================================================================
}
/*END-TASK*/

