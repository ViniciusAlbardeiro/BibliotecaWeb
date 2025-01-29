﻿using System;
using System.Collections.Generic;
using System.Text;
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
                        <a href=""livro.aspx?c={livro.Codigo}"">
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