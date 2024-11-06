using System;

namespace Proyecto4A.Models.dbpizza;

public class Cliente
{
    public int Id { get; set; }
    public string Usuario { get; set; }
    public int Nivel { get; set; }

    public Cliente(int id, string usuario, int nivel)
    {
        Id = id;
        Usuario = usuario;
        Nivel = nivel;
    }
}