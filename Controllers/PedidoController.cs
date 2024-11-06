using Microsoft.AspNetCore.Mvc;
using Proyecto4A.Models.dbpizza;

namespace Proyecto4A.Controllers
{
    public class PedidoController : Controller
    {
        private readonly GestorDBPizza gestor = new GestorDBPizza();

        [HttpGet]
        public ActionResult Index(int clienteId, int estado = 0)
        {
            var pedidos = gestor.ObtenerPedidosPorCliente(clienteId)
                                .Where(p => estado == 0 || p.Estado == estado)
                                .ToList();

            var cliente = gestor.ObtenerClientePorId(clienteId);
            if (cliente == null)
            {
                return NotFound("Cliente no encontrado " + clienteId + ".");
            }

            ViewBag.ClienteUsuario = cliente.Usuario;
            ViewBag.ClienteId = clienteId;
            ViewBag.Estado = estado;

            double totalPedidos = 0;
            if (estado == 0)
            {
                totalPedidos = pedidos.Sum(p => p.Total);
            }
            ViewBag.TotalPedidos = totalPedidos;

            return View(pedidos);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            ViewBag.Clientes = gestor.ObtenerClientes();
            ViewBag.Pizzas = gestor.ObtenerPizzas();
            return View();
        }

        [HttpPost]
        public ActionResult Guardar(int cliente, int pizza, int cantidad)
        {
            if (cantidad < 1)
            {
                ModelState.AddModelError("cantidad", "La cantidad debe ser al menos 1.");
            }

            var gestor = new GestorDBPizza();
            var pizzaSeleccionada = gestor.ObtenerPizzaPorId(pizza);
            if (pizzaSeleccionada == null)
            {
                ModelState.AddModelError("pizza", "La pizza seleccionada no es válida.");
            }

            // Verificar que el cliente existe
            var clienteExistente = gestor.ObtenerClientePorId(cliente);
            if (clienteExistente == null)
            {
                ModelState.AddModelError("cliente", "El cliente no existe.");
            }

            if (!ModelState.IsValid)
            {
                // Si hay errores, recarga los datos y vuelve a mostrar la vista de crear
                ViewBag.Clientes = gestor.ObtenerClientes();
                ViewBag.Pizzas = gestor.ObtenerPizzas();
                return View("Crear");
            }

            // Si todo es válido, crear el pedido
            gestor.CrearPedido(cliente, pizza, cantidad);

            // Redirigir a la lista de pedidos
            return RedirectToAction("Index", new { clienteId = cliente });
        }

        [HttpPost]
        public ActionResult Actualizar(int idPedido, int clienteId, int nuevoEstado)
        {
            if (idPedido <= 0 || clienteId <= 0 || nuevoEstado <= 0){
                return NotFound("No se puede actualizar el pedido, Datos invalidos"); 
            }

            Pedido pedidoActual = gestor.ObtenerPedidoPorId(idPedido);

            if (pedidoActual != null && pedidoActual.GetEstado() != nuevoEstado) {
                gestor.ActualizarEstadoPedido(idPedido, nuevoEstado);
                return RedirectToAction("Index", new { clienteId = clienteId, estado = pedidoActual.Estado });
            }
            
            return RedirectToAction("Index", new { clienteId = clienteId, estado = pedidoActual.Estado });
        }

        [HttpGet]
        public ActionResult Cierre()
        {
            List<Pizza> pizzasList = gestor.ObtenerPizzas();

            double totalVentas = 0;
            int totalCantidadVendidas = 0;
            for (int i = 0; i < pizzasList.Count; i++)
            {
                totalCantidadVendidas += gestor.ObtenerCantidadVentasPorPizza(pizzasList[i].Id);
                totalVentas += gestor.ObtenerTotalVentasPorPizza(pizzasList[i].Id);
            }

            ViewBag.TotalCantidadVendidas = totalCantidadVendidas;
            ViewBag.TotalVentas = totalVentas;

            return View(pizzasList);
        }

    }
}
