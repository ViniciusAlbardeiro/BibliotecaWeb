﻿using System;
using System.Collections.Generic;
using System.Linq;
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

            if (Request["c"] == null)
            {
                Response.Redirect("");
                Response.End();
                return;
            }

            if (String.IsNullOrEmpty(Request["c"].ToString()))
            {
                Response.Redirect("");
                Response.End();
                return;
            }


            
            int codigo = Convert.ToInt32(Request["c"].ToString());


            CategoriaController categoriaController = new CategoriaController();
            List<Categoria> lista = categoriaController.Listar();
            ddlCategorias.Items.Add(new ListItem("Todos", "-1"));

            foreach (Categoria c in lista)
            {
                ddlCategorias.Items.Add(new ListItem(c.Nome, c.Codigo.ToString()));
            }



            /* livro */
            LivroController livroController = new LivroController();
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

    }
}