using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Utilities;
using Proyecto4A.Models;

namespace Proyecto4A.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Login()
        {
            GestorBDTienda gestor = new GestorBDTienda();
            List<Usuario> lstusuarios = gestor.listarusuarios();
            Usuario primerUsuario = lstusuarios.FirstOrDefault();

            if (primerUsuario == null)
            {
                ViewBag.Message = "No hay usuarios registrados. Registre uno para empezar.";
            }

            return View(primerUsuario);
        }

        public IActionResult Verificar(string usuario, string clave)
        {
            GestorBDTienda gestor = new GestorBDTienda();
            string msg;

            if (gestor.verificarUsuario(usuario, clave))
            {
                msg = "Usuario verificado.";
                return listarUsuarios(msg);
            }
            else
            {
                return View("Error");
            }
        }
        public IActionResult Registro()
        {
            return View("Registro");
        }

        [HttpPost]
        public IActionResult Registrar(string usuario, string clave, int nivel)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(clave) || nivel == 0)
            {
                ViewBag.Message = "Todos los campos son obligatorios.";
                return View("Registro");
            }

            GestorBDTienda gestor = new GestorBDTienda();
            if (gestor.consultarUsuarioPorUsuario(usuario))
            {
                ViewBag.Message = "El usuario ya está registrado. Intente con otro.";
                return View("Registro");
            }
            else
            {
                gestor.registrarUsuario(usuario, clave, nivel);
                ViewBag.Message = "Registro exitoso. (" + usuario + ")";
                return View("Registro");
            }
        }

        public IActionResult Editar(string codigo)
        {
            GestorBDTienda gestor = new GestorBDTienda();
            Usuario usuario = gestor.obtenerUsuarioPorCodigo(codigo);

            if (usuario == null)
            {
                ViewBag.Message = "Usuario no encontrado.";
                return RedirectToAction("Login");
            }

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Actualizar(string codigo, string usuario, string clave, int nivel)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(clave) || nivel == 0)
            {
                ViewBag.Message = "Todos los campos son obligatorios.";
                return View("Editar", codigo);
            }

            GestorBDTienda gestor = new GestorBDTienda();
            Usuario usuarioActualizado = new Usuario();
            usuarioActualizado.codigo = Convert.ToInt32(codigo);
            usuarioActualizado.usuario = usuario;
            usuarioActualizado.clave = clave;
            usuarioActualizado.nivel = nivel;

            gestor.actualizarUsuario(usuarioActualizado);
            ViewBag.Message = "Actualización exitosa.";

            return RedirectToAction("Login");
        }


        public IActionResult Eliminar(string codigo)
        {
            string msg;
            GestorBDTienda gestor = new GestorBDTienda();

            if (gestor.consultarusuarioPorId(codigo))
            {
                msg = "Usuario " + gestor.obtenerUsuarioPorCodigo(codigo).usuario + " eliminado";
                gestor.eliminarUsuarioPorId(codigo);
                return listarUsuarios(msg);
            }
            else
            {
                msg = "Usuario con ID (" + codigo + ") no encontrado";
                return listarUsuarios(msg);
            }
        }


        private IActionResult listarUsuarios(string msg = null)
        {
            GestorBDTienda gestor = new GestorBDTienda();
            List<Usuario> lstusuarios = gestor.listarusuarios();
            ViewBag.Message = msg;
            return View("Sistema", lstusuarios);
        }

        
    }
}
