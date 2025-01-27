using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace BibliotecaWeb
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoriaController controller = new CategoriaController();
            List<Categoria> lista = controller.Listar();
            ddlCategorias.Items.Add(new ListItem("Todos", "-1"));
            //for (int i = 0; i < lista.Count; i++)
            //{
            //    ddlCategorias.Items.Add(new ListItem(lista[i].Nome, lista[i].Codigo.ToString()));
            //}

            foreach (Categoria c in lista)
            {
                ddlCategorias.Items.Add(new ListItem(c.Nome, c.Codigo.ToString()));
            }


            /* LIVRO */
            LivroController livroController = new LivroController();
            List<Livro> livros = livroController.Listar();


            foreach (Livro livro in livros)
            {
                string nomesAutores = "";
                foreach (Autor autor in livro.Autores)
                {
                    if (nomesAutores == "")
                    {
                        nomesAutores += autor.Nome;
                    }
                    else
                    {
                        nomesAutores += ", " + autor.Nome;
                    }
                }

                litListaLivro.Text += $@"<div class=""livro"">
                <a href=""livro.html"">
                 <div class=""box-livro sombra"">
                  <img src=""images/{livro.Codigo}.jpg""/>
                  <div class=""dados-livro"">
                   <p class=""titulo-livro"">{livro.Nome}</p>
                   <p class=""autor-livro"">{nomesAutores}</p>
                  </div>
                 </div>
                </a>
               </div>";
            }
        }
    }
    }
