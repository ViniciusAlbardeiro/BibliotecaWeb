using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BibliotecaWeb
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                litMensagem.Text = "<div class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\">\r\n  <strong>Erro!</strong> Você deve preencher todos os campos.\r\n  <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Fechar\"></button>\r\n</div>\r\n";
            }
            else
            {
                UsuarioControlador usuarioControlador = new UsuarioControlador();
                Usuario usuario = usuarioControlador.Login(txtEmail.Text, txtSenha.Text);
                if (usuario != null)
                {
                    Sessao sessao = new Sessao();
                    sessao.CriarSessao(usuario);
                    Response.Redirect("index.aspx");
                }
                else
                {
                    var htmlBuilder = new StringBuilder();
                    htmlBuilder.Append("<div class=\"alert alert-danger alert-dismissible fade show\" role=\"alert\">\r\n  <strong>Erro!</strong> Dados Incorretos.\r\n  <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Fechar\"></button>\r\n</div>\r\n");
                    litMensagem.Text = htmlBuilder.ToString();
                }
            }
            
        }
    }
}