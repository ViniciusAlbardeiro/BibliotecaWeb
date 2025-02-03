using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace BibliotecaWeb
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CarregarCategorias();
            CarregarLivros();
        }
        protected void pesquisar_Click(object sender, EventArgs e)
        {
            string urlcaminho = "resultados.aspx?busca=" + HttpUtility.UrlEncode(txtFiltro.Text);

            int codigoCategoria;
            if (int.TryParse(ddlCategorias.SelectedValue, out codigoCategoria) && codigoCategoria != -1)
            {
                urlcaminho += "&categoria=" + codigoCategoria;
            }

            if (!string.IsNullOrWhiteSpace(txtFiltro.Text))
            {
                Response.Redirect(urlcaminho);
                Response.End();
            }
        }


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

        private void CarregarLivros()
        {
            var livroController = new LivroController();
            var livros = livroController.Listar();

            if (livros == null || livros.Count == 0)
            {
                litListaLivro.Text = "<p>Nenhum livro encontrado.</p>";
                return;
            }

            var htmlBuilder = new StringBuilder();
            foreach (var livro in livros)
            {
                var autores = ObterNomesAutores(livro.Autores);
                htmlBuilder.AppendLine(CriarCardLivro(livro, autores));
            }

            litListaLivro.Text = htmlBuilder.ToString();
        }


        private string ObterNomesAutores(List<Autor> autores)
        {
            if (autores == null || autores.Count == 0)
            {
                return "Autor desconhecido";
            }

            var nomesAutores = new List<string>();
            foreach (var autor in autores)
            {
                nomesAutores.Add(autor.Nome);
            }
            return string.Join(", ", nomesAutores);
        }


        private string CriarCardLivro(Livro livro, string autores)
        {
            string nomeLivro = HttpUtility.HtmlEncode(livro.Nome);
            string nomeAutores = HttpUtility.HtmlEncode(autores);

            return $@"<div class=""livro"">
                <a href=""livro.aspx?c={livro.Codigo}"">
                    <div class=""box-livro sombra"">
                        <img src=""images/{livro.Codigo}.jpg"" alt=""Capa do livro {nomeLivro}""/>
                        <div class=""dados-livro"">
                            <p class=""titulo-livro"">{nomeLivro}</p>
                            <p class=""autor-livro"">{nomeAutores}</p>
                        </div>
                    </div>
                </a>
            </div>";
        }
    }
}