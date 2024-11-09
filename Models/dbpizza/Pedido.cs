using System;
using Proyecto4A.Models.dbpizza;

namespace Proyecto4A.Models.dbpizza;
public class Pedido
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public int PizzaId { get; set; }
    public int Cantidad { get; set; }
    public double Total { get; set; }
    public int Estado { get; set; }

    public Pedido(int id, int clienteId, int pizzaId, int cantidad, int estado = 0)
    {
        Id = id;
        ClienteId = clienteId;
        PizzaId = pizzaId;
        Cantidad = cantidad;
        Total = GetTotal();

        if (estado == 0)
        {
            Estado = GetEstado().Id;
        }
        else
        {
            Estado = estado;
        }

        // Estado = GetEstado();
    }

    public double GetTotal()
    {
        GestorDBPizza gestor = new GestorDBPizza();
        double precioPizza = gestor.ObtenerPrecioPizza(PizzaId);
        return precioPizza * Cantidad;
    }

    public Estado GetEstado()
    {
        GestorDBPizza gestor = new GestorDBPizza();
        var estado = gestor.ObtenerEstadoPorId(Estado);
        return estado ?? new Estado(0, "Estado desconocido", "#000000");
    }

    public string GetNombrePizza()
    {
        GestorDBPizza gestor = new GestorDBPizza();
        var pizza = gestor.ObtenerPizzaPorId(PizzaId);
        return pizza != null ? pizza.Nombre : "Pizza no encontrada";
    }

    public double GetPrecioUnidad()
    {
        GestorDBPizza gestor = new GestorDBPizza();
        var precioUnidad = gestor.ObtenerPrecioPizza(PizzaId);
        return precioUnidad;
    }
}
