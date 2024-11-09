using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Proyecto4A.Models.dbpizza
{
    public class GestorDBPizza
    {
        public void CrearPedido(int clienteId, int pizzaId, int cantidad)
        {
            MySqlConnection con = null;
            MySqlCommand cmd = null;
            
            string sql = "INSERT INTO pedidos (cliente_id, pizza_id, cantidad, total, estado) VALUES (@cliente_id, @pizza_id, @cantidad, @total, @estado)";

            try
            {
                con = ConectaBDPizza.Abrir();
                double precioPizza = ObtenerPrecioPizza(pizzaId);
                double total = precioPizza * cantidad;

                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@cliente_id", clienteId);
                cmd.Parameters.AddWithValue("@pizza_id", pizzaId);
                cmd.Parameters.AddWithValue("@cantidad", cantidad);
                cmd.Parameters.AddWithValue("@total", total);
                cmd.Parameters.AddWithValue("@estado", 1); // Estado por defecto: 1 (Creado)

                cmd.ExecuteNonQuery();
                Console.WriteLine("Pedido creado exitosamente. Total: " + total);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al crear el pedido: " + e.Message);
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Close();
            }
        }

        public List<Cliente> ObtenerClientes()
        {
            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            List<Cliente> listaClientes = new List<Cliente>();

            string sql = "SELECT * FROM cliente";

            try
            {
                con = ConectaBDPizza.Abrir();
                cmd = new MySqlCommand(sql, con);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    string usuario = reader.GetString("usuario");
                    int nivel = reader.GetInt32("nivel");

                    listaClientes.Add(new Cliente(id, usuario, nivel));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al obtener los clientes: " + e.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Close();
            }

            return listaClientes;
        }
        
        public Cliente ObtenerClientePorId(int clienteId)
        {
            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            Cliente cliente = null;

            string sql = "SELECT * FROM cliente WHERE id = @cliente_id";

            try
            {
                con = ConectaBDPizza.Abrir();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@cliente_id", clienteId);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    string usuario = reader.GetString("usuario");
                    int nivel = reader.GetInt32("nivel");

                    cliente = new Cliente(id, usuario, nivel);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al obtener el cliente: " + e.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Close();
            }

            return cliente;
        }

        public List<Pizza> ObtenerPizzas()
        {
            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            List<Pizza> listaPizzas = new List<Pizza>();

            string sql = "SELECT * FROM pizza";

            try
            {
                con = ConectaBDPizza.Abrir();
                cmd = new MySqlCommand(sql, con);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    string nombre = reader.GetString("nombre");
                    double precio = reader.GetDouble("precio");

                    listaPizzas.Add(new Pizza(id, nombre, precio));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al obtener las pizzas: " + e.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Close();
            }

            return listaPizzas;
        }

        public double ObtenerPrecioPizza(int pizzaId)
        {
            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            double precio = 0.0;

            string sql = "SELECT precio FROM pizza WHERE id = @pizza_id";

            try
            {
                con = ConectaBDPizza.Abrir();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@pizza_id", pizzaId);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    precio = reader.GetDouble("precio");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al obtener el precio de la pizza: " + e.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Close();
            }

            return precio;
        }

        public List<Pedido> ObtenerPedidosPorCliente(int clienteId)
        {
            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            List<Pedido> listaPedidos = new List<Pedido>();

            string sql = "SELECT * FROM pedidos WHERE cliente_id = @cliente_id";

            try
            {
                con = ConectaBDPizza.Abrir();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@cliente_id", clienteId);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    int pizzaId = reader.GetInt32("pizza_id");
                    int cantidad = reader.GetInt32("cantidad");
                    int estado = reader.GetInt32("estado");

                    listaPedidos.Add(new Pedido(id, clienteId, pizzaId, cantidad, estado));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al obtener los pedidos del cliente: " + e.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Close();
            }

            return listaPedidos;
        }

        public Estado ObtenerEstadoPorId(int estadoId)
        {
            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            Estado estado = null;

            string sql = "SELECT * FROM estado WHERE id = @estado_id";

            try
            {
                con = ConectaBDPizza.Abrir();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@estado_id", estadoId);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    string nombre = reader.GetString("nombre");
                    string color = reader.IsDBNull(reader.GetOrdinal("color")) ? "" : reader.GetString("color");

                    estado = new Estado(id, nombre, color);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al obtener el estado: " + e.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Close();
            }

            if (estado == null)
            {
                throw new Exception("Estado no encontrado: " + estadoId);
            }

            return estado;
        }

        public Pizza ObtenerPizzaPorId(int pizzaId)
        {
            MySqlConnection con = new MySqlConnection();
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader reader = null;
            Pizza pizza = null;

            string sql = "SELECT * FROM pizza WHERE id = @pizza_id";

            try
            {
                con = ConectaBDPizza.Abrir();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@pizza_id", pizzaId);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    string nombre = reader.GetString("nombre");
                    double precio = reader.GetDouble("precio");
                    pizza = new Pizza(id, nombre, precio);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al obtener la pizza: " + e.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Close();
            }

            return pizza;
        }

        public Pedido ObtenerPedidoPorId(int idPedido)
        {
            MySqlConnection con = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            Pedido pedido = null;

            string sql = "SELECT * FROM pedidos WHERE id = @id_pedido";

            try
            {
                con = ConectaBDPizza.Abrir();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id_pedido", idPedido);
                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int clienteId = reader.GetInt32("cliente_id");
                    int pizzaId = reader.GetInt32("pizza_id");
                    int cantidad = reader.GetInt32("cantidad");
                    int estado = reader.GetInt32("estado");
                    double total = reader.GetDouble("total");

                    pedido = new Pedido(idPedido, clienteId, pizzaId, cantidad, estado);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al obtener el pedido: " + e.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Close();
            }

            return pedido;
        }

        public void ActualizarEstadoPedido(int idPedido, int nuevoEstado)
        {
            MySqlConnection con = null;
            MySqlCommand cmd = null;

            string sql = "UPDATE pedidos SET estado = @nuevo_estado WHERE id = @id_pedido";

            try
            {
                con = ConectaBDPizza.Abrir();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@nuevo_estado", nuevoEstado);
                cmd.Parameters.AddWithValue("@id_pedido", idPedido);
                
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Pedido actualizado correctamente.");
                }
                else
                {
                    Console.WriteLine("No se pudo actualizar el pedido.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al actualizar el estado del pedido: " + e.Message);
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Close();
            }
        }
        public double ObtenerTotalVentasPorPizza(int pizzaId, int estadoFiltrado = 0, int estadoExcluido = 0)
        {
            MySqlConnection con = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            double totalVentas = 0.0;

            // Construcción de la consulta con las condiciones de estadoFiltrado y estadoExcluido
            string sql = "SELECT SUM(total) AS total_ventas FROM pedidos WHERE pizza_id = @pizza_id";
            
            // Filtrar por estadoFiltrado si se pasa
            if (estadoFiltrado != 0)
            {
                sql += " AND estado = @estadoFiltrado";
            }

            // Excluir estadoExcluido si se pasa
            if (estadoExcluido != 0)
            {
                sql += " AND estado != @estadoExcluido";
            }

            try
            {
                con = ConectaBDPizza.Abrir();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@pizza_id", pizzaId);

                // Añadir el parámetro estadoFiltrado si se pasa
                if (estadoFiltrado != 0)
                {
                    cmd.Parameters.AddWithValue("@estadoFiltrado", estadoFiltrado);
                }

                // Añadir el parámetro estadoExcluido si se pasa
                if (estadoExcluido != 0)
                {
                    cmd.Parameters.AddWithValue("@estadoExcluido", estadoExcluido);
                }

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    totalVentas = reader.IsDBNull(reader.GetOrdinal("total_ventas")) ? 0.0 : reader.GetDouble("total_ventas");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al obtener el total de ventas de pedidos por pizza: " + e.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Close();
            }

            return totalVentas;
        }

        public int ObtenerCantidadVentasPorPizza(int pizzaId, int estadoFiltrado = 0, int estadoExcluido = 0)
        {
            MySqlConnection con = null;
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            int totalCantidad = 0;

            // Construcción de la consulta con las condiciones de estadoFiltrado y estadoExcluido
            string sql = "SELECT SUM(cantidad) AS total FROM pedidos WHERE pizza_id = @pizza_id";
            
            // Filtrar por estadoFiltrado si se pasa
            if (estadoFiltrado != 0)
            {
                sql += " AND estado = @estadoFiltrado";
            }

            // Excluir estadoExcluido si se pasa
            if (estadoExcluido != 0)
            {
                sql += " AND estado != @estadoExcluido";
            }

            try
            {
                con = ConectaBDPizza.Abrir();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@pizza_id", pizzaId);

                // Añadir el parámetro estadoFiltrado si se pasa
                if (estadoFiltrado != 0)
                {
                    cmd.Parameters.AddWithValue("@estadoFiltrado", estadoFiltrado);
                }

                // Añadir el parámetro estadoExcluido si se pasa
                if (estadoExcluido != 0)
                {
                    cmd.Parameters.AddWithValue("@estadoExcluido", estadoExcluido);
                }

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    totalCantidad = reader.IsDBNull(reader.GetOrdinal("total")) ? 0 : reader.GetInt32("total");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al obtener la cantidad de pizzas por ID de pizza y estado: " + e.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Close();
            }

            return totalCantidad;
        }
    }
}
