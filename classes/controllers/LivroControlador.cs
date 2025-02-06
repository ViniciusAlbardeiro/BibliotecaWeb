using System.Collections.Generic;
using System.EnterpriseServices;
using System.Runtime.Remoting.Messaging;
using BibliotecaWeb;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
public class LivroControlador
{

    private Banco banco = new Banco();
    private List<Livro> listaLivros = new List<Livro>();


    private List<Autor> NomesAutores(string dados)
    {
        List<Autor> autoresLivro = new List<Autor>();
        string nomesAutores = dados;
        string[] autores = nomesAutores.Split(',');
        foreach (string autor in autores)
        {
            Autor a = new Autor(autor);
            autoresLivro.Add(a);
        }

        return autoresLivro;
    }

    private List<Livro> Livros(string query)
    {
        MySqlDataReader dados = banco.Consultar(query);
        while (dados.Read())
        {


            Livro livro = new Livro
            {
                Codigo = dados.GetInt32("cd_livro"),
                Nome = dados.GetString("nm_livro"),
                Autores = NomesAutores(dados.GetString("autores"))
            };

            listaLivros.Add(livro);
        }
        dados.Close();
        banco.Desconectar();
        return listaLivros;
    }

    public List<Livro> Listar()
    {
        string query = $@"SELECT 
        l.cd_livro, l.nm_livro, group_concat(a.nm_autor separator ',') as autores
        FROM livro l 
        JOIN editora e ON (l.cd_editora = e.cd_editora)
        JOIN livro_autor la ON (l.cd_livro = la.cd_livro)
        JOIN autor a ON (a.cd_autor = la.cd_autor)
        group by l.cd_livro";





        List<Livro> livrosCadastrados = Livros(query);
        
        return livrosCadastrados;
    }

    public List<Livro> PesquisarLivros(string param, string codigoCategoria)
    {

        if (codigoCategoria == "-1" )
        { 
            if (!string.IsNullOrWhiteSpace(param))
            {
                string query = $@"SELECT 
                            l.cd_livro, l.nm_livro, l.cd_ISBN,
                            l.ds_sinopse, e.nm_editora,
                            group_concat(a.nm_autor separator ',') as autores
                        FROM livro l 
                        JOIN editora e ON (l.cd_editora = e.cd_editora)
                        JOIN livro_autor la ON (l.cd_livro = la.cd_livro)
                        JOIN autor a ON (a.cd_autor = la.cd_autor)
                        WHERE l.nm_livro like '%{param}%' 
                            or
                            a.nm_autor like '%{param}%'
                            or
                            e.nm_editora like '%{param}%'
                            or
                            l.ds_sinopse like '%{param}%'
                        group by l.cd_livro";
                List<Livro> resultadoPesquisaLivro = Livros(query);
                return resultadoPesquisaLivro;
            }
            else
            {
                return Listar();
            }
        

        }

        else
        {
            if (string.IsNullOrWhiteSpace(param))
            {
                string query = $@"SELECT 
                                l.cd_livro, 
                                l.nm_livro, 
                                l.cd_ISBN, 
                                l.ds_sinopse, 
                                e.nm_editora,
                                lc.cd_categoria,
                                GROUP_CONCAT(DISTINCT a.nm_autor SEPARATOR ', ') AS autores
                            FROM livro l
                            JOIN livro_categoria lc ON l.cd_livro = lc.cd_livro
                            JOIN categoria c ON c.cd_categoria = lc.cd_categoria
                            JOIN editora e ON l.cd_editora = e.cd_editora
                            JOIN livro_autor la ON l.cd_livro = la.cd_livro
                            JOIN autor a ON a.cd_autor = la.cd_autor
                            WHERE lc.cd_categoria = {codigoCategoria}
                            GROUP BY l.cd_livro, l.nm_livro, l.cd_ISBN, l.ds_sinopse, e.nm_editora;
                            ";
                List<Livro> resultadoPesquisaLivro = Livros(query);
                return resultadoPesquisaLivro;
            }
            else
            {
                string query = $@"SELECT 
	                                    l.cd_livro, 
	                                    l.nm_livro, 
	                                    l.cd_ISBN, 
	                                    l.ds_sinopse, 
	                                    e.nm_editora,
	                                    lc.cd_categoria,
	                                    GROUP_CONCAT(a.nm_autor SEPARATOR ', ') AS autores
                                    FROM livro l
                                    JOIN livro_categoria lc ON l.cd_livro = lc.cd_livro
                                    JOIN categoria c ON c.cd_categoria = lc.cd_categoria
                                    JOIN editora e ON l.cd_editora = e.cd_editora
                                    JOIN livro_autor la ON l.cd_livro = la.cd_livro
                                    JOIN autor a ON a.cd_autor = la.cd_autor
                                    WHERE lc.cd_categoria = {codigoCategoria} 
                                    AND (
                                        l.nm_livro LIKE '%{param}%' 
                                        OR a.nm_autor LIKE '%{param}%'
                                        OR l.ds_sinopse LIKE '%{param}%'
                                    )
                                    GROUP BY l.cd_livro;
                                    ";



                List<Livro> resultadoPesquisaLivro = Livros(query);
                return resultadoPesquisaLivro;

            }
        }
            


    }

    public Livro DetalhesLivro(int codigo)
    {
        
        Livro livro = new Livro();

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
            livro.Codigo = dados.GetInt32("cd_livro");
            livro.Nome = dados.GetString("nm_livro");
            livro.Isbn = dados.GetString("cd_ISBN");
            livro.Sinopse = dados.GetString("ds_sinopse");
            livro.AnoEdicao = dados.GetInt32("aa_edicao");
            livro.Editora = new Editora(dados.GetString("nm_editora"));
            livro.Autores = NomesAutores(dados.GetString("autores"));
            
        }
        dados.Close();
        banco.Desconectar();
        return livro;
    }

    
}
