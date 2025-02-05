using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

public class UsuarioController
{
    private Banco banco = new Banco();

    public UsuarioController()
    {
    }
    public void Cadastrar(Usuario usuario)
    {
        string comando = $@"INSERT INTO usuarios (nm_usuario, email, senha) VALUES ({usuario.Nome}, {usuario.Email}, {usuario.Senha})";
        banco.Executar(comando);
        banco.Desconectar();
    }


}
