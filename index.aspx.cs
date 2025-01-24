using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BibliotecaWeb
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoriaController controller = new CategoriaController();
            List<Categoria> lista = controller.Listar();
            for (int i = 0; i < lista.Count; i++)
            {
                ddlCategorias.Items.Add(new ListItem(lista[i].Nome, lista[i].Codigo.ToString()));
            }

            //ddlCategorias.DataSource = lista;
            //ddlCategorias.DataTextField = "Nome";
            //ddlCategorias.DataValueField = "Codigo";
            //ddlCategorias.DataBind();

        }
    }
}