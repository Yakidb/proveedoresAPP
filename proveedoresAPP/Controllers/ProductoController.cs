using System;
using System.Collections.Generic;
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
    public class ProductoController : Controller
    {
        private readonly ProveedoresDbContext context_Z;
        private readonly ILogger<ProductoController> logger_Z;
        private readonly ProductosDao productosDao_Z;

        public ProductoController(
            ProveedoresDbContext context_I,
            ILogger<ProductoController> loogger_I,
            ProductosDao productosDao_I
            )
        {
            this.context_Z = context_I;
            this.logger_Z = loogger_I;
            this.productosDao_Z = productosDao_I;
        }

        //--------------------------------------------------------------------------------------------------------------
        public async Task<IActionResult> Index()
        {
            List<prodentityProductoEntityDB?> listProdentities = null;
            RcdoperEnum rcdoperEnum = RcdoperEnum.Z_NULL;
            try
            {
                listProdentities = this.productosDao_Z.GetAllRecords();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProductoController),
                    $"Exepcion de concurrencia atrapada: {ex.Message}", "*");
                ViewBag.ErrorMessage = "Hay problemas al obtener registros sobre los productos. " +
                    "Intentelo mas tarde";
                rcdoperEnum = RcdoperEnum.CONCURRENCY_ISSUE;
                return View(listProdentities ?? new List<prodentityProductoEntityDB?>());
            }
            catch (DbUpdateException ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProductoController),
                    $"Error al guardar registro en la base de datos: {ex.Message}", "*");
                ViewBag.ErrorMessage = "Hay problemas al obtener registros sobre los productos. " +
                    "Intentelo mas tarde";
                rcdoperEnum = RcdoperEnum.FAILURE;
                return View(listProdentities ?? new List<prodentityProductoEntityDB?>());
            }
            catch (Exception ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProductoController),
                    $"$Error ocurrido: {ex.Message}", "*");
                ViewBag.ErrorMessage = "Hay problemas al obtener registros sobre los productos. " +
                    "Intentelo mas tarde";
                rcdoperEnum = RcdoperEnum.UNKNOWN_ERROR;
                return View(listProdentities ?? new List<prodentityProductoEntityDB?>());
            }

            return View(listProdentities ?? new List<prodentityProductoEntityDB?>());
        }

        //--------------------------------------------------------------------------------------------------------------
        public IActionResult Create(
            //                                              //Formulario para guardar un producto
            )
        {
            RcdoperEnum rcdoperEnum = RcdoperEnum.Z_NULL;
            try
            {

                ViewData["IdProveedor"] = new SelectList(context_Z.Proveedores, "IdProveedor", "IdProveedor");
                return View();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProductoController),
                    $"Exepcion de concurrencia atrapada: {ex.Message}", "*");
                ViewBag.ErrorMessage = "Hay un problema al guardar un producto. Consulte al" +
                    " administrador";
                rcdoperEnum = RcdoperEnum.CONCURRENCY_ISSUE;
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProductoController),
                    $"Error al guardar registro en la base de datos: {ex.Message}", "*");
                ViewBag.ErrorMessage = "Hay un problema al guardar un producto. Consulte al " +
                    "administrador";
                rcdoperEnum = RcdoperEnum.FAILURE;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProductoController),
                    $"$Error ocurrido: {ex.Message}", "*");
                ViewBag.ErrorMessage = "Hay un problema al guardar un producto. Consulte al administrador";
                rcdoperEnum = RcdoperEnum.UNKNOWN_ERROR;
                return RedirectToAction(nameof(Index));
            }
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpPost]
        public async Task<IActionResult> Create(
            prodentityProductoEntityDB prodentityProductoEntityDB
            )
        {
            RcdoperEnum rcdoperEnum = RcdoperEnum.Z_NULL;
            try
            {
                string strMensajeCustomizado = " ";
                this.productosDao_Z.subAddProducto(prodentityProductoEntityDB, out rcdoperEnum, out strMensajeCustomizado);

                switch (rcdoperEnum)
                {
                    case RcdoperEnum.SUCESS:
                        {
                            return RedirectToAction(nameof(Index));
                        }
                    case RcdoperEnum.MISSING_FIELDS:
                        {
                            ModelState.AddModelError("", strMensajeCustomizado);
                            ViewData["IdProveedor"] = 
                                new SelectList(context_Z.Proveedores, "IdProveedor", "IdProveedor", prodentityProductoEntityDB.IdProveedor);
                            return View(prodentityProductoEntityDB);
                            break;
                        }
                    case RcdoperEnum.FIELD_INVALID:
                        {
                            ModelState.AddModelError("", strMensajeCustomizado);
                            ViewData["IdProveedor"] = 
                                new SelectList(context_Z.Proveedores, "IdProveedor", "IdProveedor", prodentityProductoEntityDB.IdProveedor);
                            return View(prodentityProductoEntityDB);
                            break;
                        }
                    default:
                        {
                            ViewBag.ErrorMessage = "Hay problemas al guardar el producto. Intentelo mas tarde";
                            ViewData["IdProveedor"] = 
                                new SelectList(context_Z.Proveedores, "IdProveedor", "IdProveedor", prodentityProductoEntityDB.IdProveedor);
                            return View(prodentityProductoEntityDB);
                            break;
                        }
                }

            }
            catch (DbUpdateConcurrencyException ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProductoController),
                    $"Exepcion de concurrencia atrapada: {ex.Message}", "*");
                ViewBag.ErrorMessage = "Hay un problema al guardar un producto. Consulte al administrador";
                rcdoperEnum = RcdoperEnum.CONCURRENCY_ISSUE;
                return View(prodentityProductoEntityDB);
            }
            catch (DbUpdateException ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProductoController),
                    $"Error al guardar registro en la base de datos: {ex.Message}", "*");
                ViewBag.ErrorMessage = "Hay un problema al guardar un producto. Consulte al administrador";
                rcdoperEnum = RcdoperEnum.FAILURE;
                return View(prodentityProductoEntityDB);
            }
            catch (Exception ex)
            {
                this.logger_Z.LogErrorWrapped(BorderEnum.BorderBoth, typeof(ProductoController),
                    $"$Error ocurrido: {ex.Message}", "*");
                ViewBag.ErrorMessage = "Hay un problema al guardar un producto. Consulte al administrador";
                rcdoperEnum = RcdoperEnum.UNKNOWN_ERROR;
                return View(prodentityProductoEntityDB);
            }
        }
    }
}
