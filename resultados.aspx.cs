using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BibliotecaWeb
{
    public partial class resultados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica se o parâmetro de busca existe
            if (Request["f"] == null)
            {
                Response.Redirect("");
                Response.End();
                return;
            }

            // Verifica se o parâmetro de busca não está vazio

            if (!IsPostBack)
            {
                CarregarCategorias();
                CarregarPesquisaLivro();
            }
           
            

        }

        // Evento de clique do botão de pesquisa
        protected void pesquisar_Click(object sender, EventArgs e)
        {
            string urlcaminho = $@"resultados.aspx?f={txtFiltro.Text}&c={ddlCategorias.SelectedItem.Value.ToString()}";
            Response.Redirect(urlcaminho);
            Response.End();
        }
        private void CarregarPesquisaLivro()
        {
            if (Request["f"] == null)
            {
                Response.Redirect("");
                Response.End();
                return;
            }
            if (Request["c"] == null)
            {
                Response.Redirect("");
                Response.End();
                return;
            }

            string codigoCategoria = Request["c"].ToString();
            string paramBusca = Request["f"].ToString();

            var livroController = new LivroController();
            var resultados = livroController.PesquisarLivros(paramBusca, codigoCategoria);
 
            if (resultados.Count >= 1)
            {
                var htmlBuilder = new StringBuilder();
                foreach (var livro in resultados)
                {
                    var autores = ObterNomesAutores(livro.Autores);
                    htmlBuilder.AppendLine(CriarCardLivro(livro, autores));
                }
                litListaLivro.Text = htmlBuilder.ToString();
            }
            else
            {
                NoneResultados();
            }

        }

        // Carrega as categorias no dropdown
        private void CarregarCategorias()
        {
            var controller = new CategoriaController();
            var categorias = controller.Listar();

            ddlCategorias.Items.Clear();
            ddlCategorias.Items.Add(new ListItem("Todos", "-1"));

            foreach (var categoria in categorias)
            {
                ddlCategorias.Items.Add(new ListItem(
                    categoria.Nome,
                    categoria.Codigo.ToString()
                ));
            }
        }

        // Executa a pesquisa de livros


        // Exibe mensagem de nenhum resultado
        private void NoneResultados()
        {
            litListaLivro.Text = $@"<h2>Não encontramos nenhum livro relacionado à pesquisa</h2>";
        }

        // Obtém nomes dos autores formatados
        private string ObterNomesAutores(List<Autor> autores)
        {
            var nomesAutores = new List<string>();
            foreach (var autor in autores)
            {
                nomesAutores.Add(autor.Nome);
            }
            return string.Join(", ", nomesAutores);
        }

        // Cria HTML do card do livro
        private string CriarCardLivro(Livro livro, string autores)
        {
            return $@"<div class=""livro"">
                        <a href=""livro.aspx?cl={livro.Codigo}"">
                            <div class=""box-livro sombra"">
                                <img src=""images/{livro.Codigo}.jpg"" alt=""Capa do livro {livro.Nome}""/>
                                <div class=""dados-livro"">
                                    <p class=""titulo-livro"">{livro.Nome}</p>
                                    <p class=""autor-livro"">{autores}</p>
                                </div>
                            </div>
                        </a>
                    </div>";
        }
    }
}