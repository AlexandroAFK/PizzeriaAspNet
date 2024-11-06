using System;

namespace Proyecto4A.Models.dbpizza;

public class Estado
{
    public int Id { get; set; }
    public string Nombre { get; set; }

    public Estado(int id, string nombre)
    {
        Id = id;
        Nombre = nombre;
    }
}
