using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proveedoresAPP.DAO;
using proveedoresAPP.Data;
using proveedoresAPP.Enums;
using proveedoresAPP.Helpers;
using proveedoresAPP.Models;

//                                                          //AUTHOR: Towa (EFA-Eliakim Flores).
//                                                          //CO-AUTHOR: Towa ().
//                                                          //DATE: May 07, 2024

namespace proveedoresAPP.Controllers
{
    //==================================================================================================================
    public class ProveedorController : Controller
    {
        //--------------------------------------------------------------------------------------------------------------
        /*INSTANCE VARIABLES*/
        private readonly ProveedoresDbContext context_Z;
        private readonly ILogger<ProveedorController> logger_Z;
        private readonly ProveedoresDao proveedoresDao_Z;

        //--------------------------------------------------------------------------------------------------------------
        /*OBJECT CONSTRUCTORS*/

        //--------------------------------------------------------------------------------------------------------------
        public ProveedorController(
            ILogger<ProveedorController> logger_I,
            ProveedoresDbContext context_I,
            ProveedoresDao proveedoresDao_I
            )
        {
            this.context_Z = context_I;
            this.logger_Z = logger_I;
            this.proveedoresDao_Z = proveedoresDao_I;
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<IActionResult> Index(
            //                                              //GET: Proveedores
            )
        {
            List<proventityProveedorEntityDB?> listProventities = null;
            try
            {
                listProventities = this.proveedoresDao_Z.GetAllRecords();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Log the exception using your logger_Z
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProveedorController),
                $"Error ocurrido al obtener proveedores: {ex.Message}", "*");
                ViewBag.ErrorMessage = "Error al obtener proveedores. Consulte con el administrador.";
                return View(listProventities ?? new List<proventityProveedorEntityDB?>());
            }
            catch (DbUpdateException ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProveedoresDao),
                    $"Error ocurrido al obtener proveedores: {ex.Message}", "*");
                ViewBag.ErrorMessage = "Error al obtener proveedores. Consulte con el administrador.";
                return View(listProventities ?? new List<proventityProveedorEntityDB?>());
            }
            catch (Exception ex)
            {
                // Log the exception using your logger_Z
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProveedorController),
                $"Error ocurrido al obtener proveedores: {ex.Message}", "*");

                // Handle the error gracefully within the view
                ViewBag.ErrorMessage = "Error al obtener proveedores. Consulte con el administrador."; 
                return View(listProventities ?? new List<proventityProveedorEntityDB?>()); 
            }

            return View(listProventities ?? new List<proventityProveedorEntityDB?>()); 
        }

        public IActionResult Create()
        {
            return View();
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create(
            //                                              // POST: Proveedor/Create
            proventityProveedorEntityDB proventityProveedorEntityDB)
        {
            RcdoperEnum rcdoperEnum = RcdoperEnum.Z_NULL;
            try
            {
                string strMensajeCustomizado = " ";
                this.proveedoresDao_Z.subAddProveedor(proventityProveedorEntityDB, out rcdoperEnum, out strMensajeCustomizado);
                
                switch (rcdoperEnum)
                {
                    case RcdoperEnum.SUCESS:
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    case RcdoperEnum.MISSING_FIELDS:
                        {
                            ModelState.AddModelError("", strMensajeCustomizado);
                            return View(proventityProveedorEntityDB);
                            break;
                        }
                    case RcdoperEnum.CODE_ALREADY_EXISTS:
                        {
                            ModelState.AddModelError("", strMensajeCustomizado);
                            return View(proventityProveedorEntityDB);
                            break;
                        }
                    case RcdoperEnum.RFC_INVALID:
                        {
                            ModelState.AddModelError("", strMensajeCustomizado);
                            return View(proventityProveedorEntityDB);
                            break;
                        }
                    default: {
                            ViewBag.ErrorMessage = "Hay problemas al guardar el proveedor. Intentelo mas tarde";
                            return View(proventityProveedorEntityDB);
                            break;
                        }
                }
                
            }
            catch (Exception ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProveedorController),
                    $"$Error ocurrido: {ex.Message}", "*");
                rcdoperEnum = RcdoperEnum.UNKNOWN_ERROR;
                ModelState.AddModelError("", "Ocurrió un error al guardar el proveedor, intentalo " +
                    "mas tarde.");
                return View(proventityProveedorEntityDB);
            }
        }
        
        public async Task<IActionResult> Edit(
            //                                              //Formulario de edicion proveedor 
            int? id
            )
        {
            RcdoperEnum rcdoperEnum = RcdoperEnum.Z_NULL;
            string strMensajeCustomizado = "";
            if (!id.HasValue)
            {
                ModelState.AddModelError("", "Ocurrió un error al editar el proveedor, intentalo " +
                            "mas tarde.");
                return RedirectToAction(nameof(Index));
            }
            proventityProveedorEntityDB? proventity =
                this.proveedoresDao_Z.proventityGetRecordById(id, out rcdoperEnum, out strMensajeCustomizado);

            if (
                proventity != null)
            {                                   //Vista de editado
                    
                return View(proventity);

            }
            else
            {
                //                                          //No es posible editar 
                ModelState.AddModelError("", "Ocurrió un error al editar el proveedor, intentalo " +
                            "mas tarde.");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            int id, 
            proventityProveedorEntityDB proventityProveedorEntityDB)
        {
            RcdoperEnum rcdoperEnum = RcdoperEnum.Z_NULL;
            string strMensajeCustomizado = " ";

            try 
            {
                this.proveedoresDao_Z.subEditProveedor(id, proventityProveedorEntityDB, out rcdoperEnum, out strMensajeCustomizado);

                switch (rcdoperEnum)
                {
                    case RcdoperEnum.SUCESS:
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    case RcdoperEnum.MISSING_FIELDS:
                        {
                            ModelState.AddModelError("", strMensajeCustomizado);
                            return View(proventityProveedorEntityDB);
                            break;
                        }
                    case RcdoperEnum.CODE_ALREADY_EXISTS:
                        {
                            ModelState.AddModelError("", strMensajeCustomizado);
                            return View(proventityProveedorEntityDB);
                            break;
                        }
                    case RcdoperEnum.RFC_INVALID:
                        {
                            ModelState.AddModelError("", strMensajeCustomizado);
                            return View(proventityProveedorEntityDB);
                            break;
                        }
                    default:
                        {
                            ModelState.AddModelError("", "Ocurrió un error al guardar el proveeedor. " +
                                "Por favor intentalo mas tarde.");
                            return View(proventityProveedorEntityDB);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProveedorController),
                    $"$Error ocurrido: {ex.Message}", "*");
                rcdoperEnum = RcdoperEnum.UNKNOWN_ERROR;
                ModelState.AddModelError("", "Ocurrió un error al editar el proveedor, intentalo " +
                    "mas tarde.");
                return View(proventityProveedorEntityDB);
            }
        }

        public async Task<IActionResult> Delete(
            //                                              //Borrado de proveedor automatico o mediante confirmacion 
            int? id
            )
        {
            RcdoperEnum rcdoperEnum = RcdoperEnum.Z_NULL;
            string strMensajeCustomizado = "";
            proventityProveedorEntityDB? proventity = 
                this.proveedoresDao_Z.proventityGetRecordById(id, out rcdoperEnum, out strMensajeCustomizado);
            
            if (
                proventity != null)
            {
                if (
                    //                                      //El proveedor tiene productos?
                    proventity.Productos.Any()
                    )
                {
                    //                                      //Vista de borrado para confirmar
                    return View(proventity); 
                }
                else
                {
                    //                                        //Borrado automatico 
                    this.proveedoresDao_Z.subDeleteProveedor(proventity, out rcdoperEnum, out strMensajeCustomizado);
                    if (
                        rcdoperEnum != RcdoperEnum.SUCESS
                        )
                    {
                        this.logger_Z.LogError($"[{nameof(ProveedoresDao)} - {nameof(Create)}] - " +
                        $"{strMensajeCustomizado}");
                        rcdoperEnum = RcdoperEnum.UNKNOWN_ERROR;
                        ModelState.AddModelError("", "Ocurrió un error al borrar el proveedor, intentalo " +
                            "mas tarde.");
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                //                                          //No es posible borrar 
                ModelState.AddModelError("", "Ocurrió un error al borrar el proveedor, intentalo " +
                            "mas tarde.");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(
            //                                              //Confirmar borrado
            int id
            )
        {
            RcdoperEnum rcdoperEnum = RcdoperEnum.Z_NULL;
            string strMensajeCustomizado = "";
            proventityProveedorEntityDB? proventity =
                this.proveedoresDao_Z.proventityGetRecordById(id, out rcdoperEnum, out strMensajeCustomizado);

            if (
                proventity != null)
            {
                
                //                                        //Borrado automatico 
                this.proveedoresDao_Z.subDeleteProveedor(proventity, out rcdoperEnum, out strMensajeCustomizado);
                if (
                    rcdoperEnum != RcdoperEnum.SUCESS
                    )
                {
                    this.logger_Z.LogError($"[{nameof(ProveedoresDao)} - {nameof(Create)}] - " +
                    $"{strMensajeCustomizado}");
                    rcdoperEnum = RcdoperEnum.UNKNOWN_ERROR;
                    ModelState.AddModelError("", "Ocurrió un error al borrar el proveedor, intentalo " +
                        "mas tarde.");
                }
                return RedirectToAction(nameof(Index));
                
            }
            else
            {
                //                                          //No es posible borrar 
                ModelState.AddModelError("", "Ocurrió un error al borrar el proveedor, intentalo " +
                            "mas tarde.");
                return RedirectToAction(nameof(Index));
            }
        }

        private bool proventityProveedorEntityDBExists(int id)
        {
          return (context_Z.Proveedores?.Any(e => e.IdProveedor == id)).GetValueOrDefault();
        }

        //--------------------------------------------------------------------------------------------------------------
    }
    //==================================================================================================================
}
/*END-TASK*/
