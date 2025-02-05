using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Usuario
{
	public int Codigo { get; set; }
	public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }

    public Usuario() { }

    public Usuario(string nome, string email, string senha)
    {
        this.Nome = nome;
        this.Email = email;
        this.Senha = senha;
    }
    public Usuario(int codigo, string nome, string email, string senha)
    {
        this.Codigo = codigo;
        this.Nome = nome;
        this.Email = email;
        this.Senha = senha;
    }

}
