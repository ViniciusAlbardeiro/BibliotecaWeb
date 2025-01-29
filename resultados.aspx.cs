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
            if (Request["busca"] == null)
            {
                Response.Redirect("");
                Response.End();
                return;
            }

            if (String.IsNullOrEmpty(Request["busca"].ToString()))
            {
                Response.Redirect("");
                Response.End();
                return;
            }



            CarregarCategorias();
            CarregarPesquisaLivro();

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

        private void CarregarPesquisaLivro()
        {
            string paramBusca = Request["busca"].ToString();
            var livroController = new LivroController();
            var resultados = livroController.PesquisarLivros(paramBusca);

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
                NoneResultados(paramBusca);


        }
        
        private string NoneResultados(string paramBusca)
        {
            return $@"<p>Não encontramos nenhum livro relacionado a {paramBusca}</p>";
        }

        private string ObterNomesAutores(List<Autor> autores)
        {
            var nomesAutores = new List<string>();
            foreach (var autor in autores)
            {
                nomesAutores.Add(autor.Nome);
            }
            return string.Join(", ", nomesAutores);
        }

        private string CriarCardLivro(Livro livro, string autores)
        {

            return $@"<div class=""livro"">
                        <a href=""livro.aspx?busca={livro.Codigo}"">
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