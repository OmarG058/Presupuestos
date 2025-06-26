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
        private readonly IServicioUsuarios servicioUsuarios;

        public TiposCuentasController(IConfiguration _configuration, IRepositorioTiposCuentas repositorioTiposCuentas, IServicioUsuarios servicioUsuarios) 
        {
            configuration = _configuration;
            this.repositorioTiposCuentas = repositorioTiposCuentas;
            this.servicioUsuarios = servicioUsuarios;
        }

        public async Task<IActionResult> Index() 
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId(); //aqui se debe obtener el id del usuario logueado    
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

            tipoCuenta.UsuarioId = servicioUsuarios.ObtenerUsuarioId();

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
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var yaExisteTipoCuenta = await repositorioTiposCuentas.Existe(nombre,usuarioId);

            if (yaExisteTipoCuenta) 
            {
                return Json($"El nombre {nombre} ya existe");
            }
            return Json(true);
        
        }

        [HttpGet]
        public async Task<ActionResult> Editar(int id)
        {
            var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipoCuenta = await repositorioTiposCuentas.ObtenerPorId(id, usuarioId);

            if (tipoCuenta == null)
            {
                return RedirectToAction("Index");
            }  
            
            return View(tipoCuenta);
              
        }

        [HttpPost]
        public async Task<ActionResult> Editar(TipoCuenta tipoCuenta) 
        {
             var usuarioId = servicioUsuarios.ObtenerUsuarioId();
            var tipocuentaExiste = await repositorioTiposCuentas.ObtenerPorId(tipoCuenta.Id, usuarioId);

            if(tipocuentaExiste is null)
            { 
                return RedirectToAction("NoEncontrado","Home");
            
            }
            await repositorioTiposCuentas.Actualizar(tipoCuenta);
            return RedirectToAction("Index");   

        }


    }
}
