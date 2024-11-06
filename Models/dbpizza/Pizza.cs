using System;

namespace Proyecto4A.Models.dbpizza;

public class Pizza
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public double Precio { get; set; }

    public Pizza(int id, string nombre, double precio)
    {
        Id = id;
        Nombre = nombre;
        Precio = precio;
    }
}
