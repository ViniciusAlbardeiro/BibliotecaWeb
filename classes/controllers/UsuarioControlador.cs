using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

public class UsuarioControlador
{
    private Banco banco = new Banco();

    public UsuarioControlador()
    {
    }

    public bool VerificarDadosRepetido(string email)
    {
        string comando = $@"SELECT * FROM usuarios WHERE email = {email}";
        MySqlDataReader reader = banco.Consultar(comando);
        if (reader.HasRows)
            return true;
        
        else
            return false;
    }

    public Usuario DadosUsuario(string codigo)
    {
        string comando = $@"select * from usuarios where cd_usuario = {codigo}";
        MySqlDataReader reader = banco.Consultar(comando);
        if (reader.HasRows)
        {
            Usuario usuario = new Usuario(reader.GetString("cd_usuario"), reader.GetString("nm_usuario"), reader.GetString("email"));
            reader.Close();
            banco.Desconectar();
            return usuario;
        }
        else
        {
            reader.Close();
            banco.Desconectar();
            return null;
        }
    }
    public void Cadastrar(Usuario usuario)
    {
        string comando = $@"INSERT INTO usuarios (nm_usuario, email, senha) VALUES ({usuario.Nome.ToString()}, {usuario.Email.ToString()}, {usuario.Senha.ToString()})";
        banco.Executar(comando);
        banco.Desconectar();
    }

    public void Atualizar(Usuario usuario)
    {
        string comando = $@"UPDATE usuarios SET nm_usuario = {usuario.Nome.ToString()}, email = {usuario.Email.ToString()}, senha = {usuario.Senha.ToString()} WHERE cd_usuario = {usuario.Codigo}";
        banco.Executar(comando);
        banco.Desconectar();
    }

    public void Excluir(int codigo)
    {
        string comando = $@"DELETE FROM usuarios WHERE cd_usuario = {codigo}";
        banco.Executar(comando);
        banco.Desconectar();
    }

    public Usuario Login(string email, string senha)
    {
        Usuario usuario = new Usuario();
        string comando = $@"SELECT * FROM usuarios WHERE (email = ""{email}"" AND senha = ""{senha}"")";
        MySqlDataReader reader = banco.Consultar(comando);

        if (reader.HasRows)
        {
            while (reader.Read())
            { 
                usuario = new Usuario(reader.GetInt32("cd_usuario"), reader.GetString("nm_usuario"), reader.GetString("email"), reader.GetString("senha"));
            }
            
        
            reader.Close();
            banco.Desconectar();
        }


        else
        {
            usuario = null;
            reader.Close();
            banco.Desconectar();
            
        }

        return usuario;
    }
}
 