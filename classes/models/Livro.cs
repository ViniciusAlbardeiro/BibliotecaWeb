﻿

using System.Collections.Generic;

public class Livro
{
    public int Codigo { get; set; }

    public string Isbn { get; set; }

    public string Nome { get; set; }

    public int AnoEdicao { get; set; }

    public string Sinopse { get; set; }

    public Editora Editora { get; set; }

    public List<Autor> Autores { get; set; }

    public Livro() { }

    public Livro( int codigo, string isbn, string nome, int ano, string sinopse, Editora editora, List<Autor> autores)
    {
        this.Codigo = codigo;
        this.Isbn = isbn;
        this.Nome = nome;
        this.AnoEdicao = ano;
        this.Sinopse = sinopse;
        this.Editora = editora;
        this.Autores = autores;

    }
}