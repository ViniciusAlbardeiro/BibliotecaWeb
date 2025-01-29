using MySql.Data.MySqlClient;
using System;
using System.Data;

public class Banco
{
    MySqlConnection conexao = new MySqlConnection();

    public void Conectar()
    {

        try
        {
            string linhaConexao = "SERVER=localhost;USER=root;PASSWORD=Sql201007*;DATABASE=biblioteca";
            conexao = new MySqlConnection(linhaConexao);
            conexao.Open();
        }
        catch
        {
            throw new Exception("Erro ao conectar ao Servidor");
        }
    }

    public void Desconectar()
    { 
        if (conexao != null)
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }
    }

    public MySqlDataReader Consultar(string comando)
    {
        try
        {
            Conectar();
            MySqlCommand cSQL = new MySqlCommand(comando, conexao);
            return cSQL.ExecuteReader();
        }
        catch
        {
            throw new Exception("Erro na consulta");
        }
    }

    public void Executar(string comando)
    { 
        try
        {
            Conectar();
            MySqlCommand cSQL = new MySqlCommand(comando, conexao);
            cSQL.ExecuteNonQuery();
        }
        catch
        {
            throw new Exception("Erro na execucao");
        }
    }
}

