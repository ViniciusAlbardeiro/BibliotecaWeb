using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;





public class CategoriaController
{
    public CategoriaController()
    {
    }

    public List<Categoria> Listar()
    {
        List<Categoria> lista = new List<Categoria>();
        Banco banco = new Banco();
        banco.Conectar();
        MySqlDataReader reader = banco.Consultar("SELECT * FROM categoria");
        while (reader.Read())
        {
            Categoria categoria = new Categoria
            {
                Codigo = reader.GetInt32("cd_categoria"),
                Nome = reader.GetString("nm_categoria")
            };
            lista.Add(categoria);
        }
        banco.Desconectar();


        return lista;
    }






        //public List<Categoria> Listar()
        //{
        //    List<Categoria> lista = new List<Categoria>();
        //    Categoria c = new Categoria(1, "Ação");
        //    lista.Add(c);
        //    c = new Categoria(2, "Aventura");
        //    lista.Add(c);
        //    c = new Categoria(3, "Romance");
        //    lista.Add(c);
        //    c = new Categoria(4, "Ficção Científica");
        //    lista.Add(c);
        //    c = new Categoria(5, "Terror");
        //    lista.Add(c);
        //    return lista;
        //}
    }
