using System.Collections.Generic;
using MySql.Data.MySqlClient;
public class LivroController
{
    public LivroController()
    {
    }

    public List<Livro> Listar()
    {
        List<Livro> lista = new List<Livro>();
        Banco banco = new Banco();
        banco.Conectar();
        MySqlDataReader dados = banco.Consultar($@"SELECT 
	                                                    l.cd_livro, l.nm_livro, group_concat(a.nm_autor separator ',') as autores
                                                        FROM livro l 
                                                        JOIN editora e ON (l.cd_editora = e.cd_editora)
                                                        JOIN livro_autor la ON (l.cd_livro = la.cd_livro)
                                                        JOIN autor a ON (a.cd_autor = la.cd_autor)
                                                        group by l.cd_livro");
        while (dados.Read())
        {
            List<Autor> autoresLivro = new List<Autor>();
            string nomesAutores = dados.GetString("autores");
            string[] autores = nomesAutores.Split(',');
            foreach (string autor in autores)
            {
                Autor a = new Autor
                {
                    Nome = autor
                };
                autoresLivro.Add(a);
            }

            Livro livro = new Livro
            {
                Codigo = dados.GetInt32("cd_livro"),
                Nome = dados.GetString("nm_livro"),
                Autores = autoresLivro
            };

            lista.Add(livro);
        }
        dados.Close();
        banco.Desconectar();
        return lista;
    }
}
