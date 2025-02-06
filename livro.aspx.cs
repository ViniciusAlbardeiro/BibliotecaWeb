using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Org.BouncyCastle.Asn1.Ocsp;

namespace BibliotecaWeb
{
    public partial class livro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request["cl"] == null)
            {
                Response.Redirect("");
                Response.End();
                return;
            }

            if (String.IsNullOrEmpty(Request["cl"].ToString()))
            {
                Response.Redirect("");
                Response.End();
                return;
            }


            
            int codigo = Convert.ToInt32(Request["cl"].ToString());


            CategoriaControlador categoriaController = new CategoriaControlador();
            List<Categoria> lista = categoriaController.Listar();
            ddlCategorias.Items.Add(new ListItem("Todos", "-1"));

            foreach (Categoria c in lista)
            {
                ddlCategorias.Items.Add(new ListItem(c.Nome, c.Codigo.ToString()));
            }



            /* livro */
            LivroControlador livroController = new LivroControlador();
            Livro livro = livroController.DetalhesLivro(codigo);

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

            litCapa.Text = $@"<img src=""images/{codigo}.jpg""/>";
            litNome.Text = $@"<h1>{livro.Nome}</h1>";
            litDetalhes.Text = $@"<h2>{nomesAutores} - {livro.Editora} - {livro.AnoEdicao} - ISBN: {livro.Isbn}</h2>";
            litSinopse.Text = $@"<p>{livro.Sinopse}</p>";
            
            }

        protected void pesquisar_Click(object sender, EventArgs e)
        {
            string urlcaminho = $@"resultados.aspx?f={txtFiltro.Text}&c={ddlCategorias.SelectedItem.Value.ToString()}";
            Response.Redirect(urlcaminho);
            Response.End();
        }








    }
}
    
