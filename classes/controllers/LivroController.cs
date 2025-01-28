using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using BibliotecaWeb;
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

    public Livro Dados(int codigo)
    {
        Banco banco = new Banco();
        Livro livro = new Livro();
        banco.Conectar();
        MySqlDataReader dados = banco.Consultar($@"SELECT 
                                                        l.cd_livro, l.nm_livro, l.cd_ISBN, l.ds_sinopse,
                                                        l.aa_edicao, e.cd_editora, e.nm_editora,
                                                        group_concat(a.nm_autor separator ',') as autores
                                                    FROM livro l 
                                                    JOIN editora e ON (l.cd_editora = e.cd_editora)
                                                    JOIN livro_autor la ON (l.cd_livro = la.cd_livro)
                                                    JOIN autor a ON (a.cd_autor = la.cd_autor)
                                                    WHERE l.cd_livro = {codigo}
                                                    group by l.cd_livro");
        if (dados.Read())
        {
            
            List<Autor> autoresLivro = new List<Autor>();
            string nomesAutores = dados.GetString("autores");
            string[] autores = nomesAutores.Split(',');
            foreach (string autor in autores)
            {
                Autor a = new Autor(autor);
                autoresLivro.Add(a);
            }

            livro.Codigo = dados.GetInt32("cd_livro");
            livro.Nome = dados.GetString("nm_livro");
            livro.Isbn = dados.GetString("cd_ISBN");
            livro.Sinopse = dados.GetString("ds_sinopse");
            livro.AnoEdicao = dados.GetInt32("aa_edicao");
            livro.Editora = new Editora(dados.GetString("nm_editora"));
            livro.Autores = autoresLivro;
            
        }
        dados.Close();
        banco.Desconectar();
        return livro;
    }
        
}
