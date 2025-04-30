using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Presupuestos.Models;
using Presupuestos.Servicios;

namespace Presupuestos.Controllers
{
    public class TiposCuentasController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IRepositorioTiposCuentas repositorioTiposCuentas;

        public TiposCuentasController(IConfiguration _configuration, IRepositorioTiposCuentas repositorioTiposCuentas)
        {
            configuration = _configuration;
            this.repositorioTiposCuentas = repositorioTiposCuentas;
        }

        public async Task<IActionResult> Index() 
        {
            var usuarioId = 1; //aqui se debe obtener el id del usuario logueado    
            var tiposCuentas = await repositorioTiposCuentas.Obtener(usuarioId);
            return View(tiposCuentas);  
            
        }

        public IActionResult Crear()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(TipoCuenta tipoCuenta)
        {

            if (!ModelState.IsValid)
            {
                return View(tipoCuenta);
            }

            tipoCuenta.UsuarioId = 1;

            var yaExiste = await repositorioTiposCuentas.Existe(tipoCuenta.Nombre, tipoCuenta.UsuarioId);

            if (yaExiste)
            {
                ModelState.AddModelError(nameof(tipoCuenta.Nombre), $"El nombre {tipoCuenta.Nombre} ya existe");
                return View(tipoCuenta);
            }


            await repositorioTiposCuentas.Crear(tipoCuenta);
            return RedirectToAction("Index");   
        }

        //metodo para verificar que el nombre de la cuenta no exista 
        //en la base de datos, mediante una llamada ajax con el atributo Remote en el modelo
        [HttpGet]
        public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre) 
        {
            var usuarioId = 1;
            var yaExisteTipoCuenta = await repositorioTiposCuentas.Existe(nombre,usuarioId);

            if (yaExisteTipoCuenta) 
            {
                return Json($"El nombre {nombre} ya existe");
            }
            return Json(true);
        
        }
    }
}
