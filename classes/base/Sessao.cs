using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class Sessao
{
    public void CriarSessao(Usuario usuario)
    {
        Dictionary <string, string>dados = new Dictionary<string, string>();
        dados.Add("cd_usuario", usuario.Codigo.ToString());
        dados.Add("nm_usuario", usuario.Nome);
        dados.Add("email", usuario.Email);
        dados.Add("usuarioLogado", "true");

    }

    public void DestruirSessao(Dictionary<string, string> dados)
    {
        if (dados.ContainsValue("true")) dados.Clear();
    }

    public Usuario GetSessao(Dictionary<string, string> dados)
    {
        Usuario usuario = new Usuario();
        if (dados.ContainsKey("cd_usuario")) usuario.Codigo = Convert.ToInt32(dados["cd_usuario"]);
        if (dados.ContainsKey("nm_usuario")) usuario.Nome = dados["nm_usuario"];
        if (dados.ContainsKey("email")) usuario.Email = dados["email"];
        return usuario;
    }
}