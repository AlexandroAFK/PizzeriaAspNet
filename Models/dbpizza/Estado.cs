using System;

namespace Proyecto4A.Models.dbpizza;

public class Estado
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public string Color { get; set; }

    public Estado(int id, string nombre, string color)
    {
        Id = id;
        Nombre = nombre;
        Color = color;
    }
}
