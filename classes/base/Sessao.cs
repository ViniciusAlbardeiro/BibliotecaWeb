using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public class Sessao
{
    public void CriarSessao(HttpSessionStateBase session, Usuario usuario)
    {
        session["user"] = usuario;
    }

    public void DestruirSessao(HttpSessionStateBase session)
    {
        if (session["user"] != null)
            session.Remove("user");
    }

    public Usuario GetSessao(HttpSessionStateBase session)
    {
        if (session["user"] != null)
        {     
            Usuario usuario = (Usuario)session["user"];

            return usuario;
        }
        else
            return null;
    }
}