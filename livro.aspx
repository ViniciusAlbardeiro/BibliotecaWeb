<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="livro.aspx.cs" Inherits="BibliotecaWeb.livro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta charset="utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1"/>
<title>Biblioteca</title>
<link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@48,400,0,0" />
<link rel="stylesheet" type="text/css" href="css/estilo.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <header class="sombra">
	<div class="id-site">
		Biblioteca
	</div>
	<section class="area-busca sombra">
		 <asp:DropDownList ID="ddlCategorias" runat="server" ></asp:DropDownList>
		<asp:TextBox ID="txtFiltro" runat="server"></asp:TextBox>
<asp:LinkButton ID="pesquisar"  runat="server" OnClick="pesquisar_Click">
    <img src="images/Buscar.png" />
</asp:LinkButton>
	</section>
	<section class="area-user">
		<div class="id-usuario">
			<p>Olá, bem vindo(a)!<br/><span class="link-login">Entre ou cadastre-se</span></p>
			<span class="material-symbols-outlined icone">account_circle</span>
		</div>
		<div class="area-icones">
			<span class="material-symbols-outlined icone icone-favoritos">collections_bookmark</span>
			<span class="material-symbols-outlined icone icone-notificacao">notifications
			</span>
		</div>
	</section>
</header>

<main>
	<section class="area-livro">
		<div class="local-capa">
			<asp:Literal ID="litCapa" runat="server"></asp:Literal>
			
		</div>
		<div class="local-dados-livro">
			<asp:Literal ID="litNome" runat="server"></asp:Literal>
			<asp:Literal ID="litDetalhes" runat="server"></asp:Literal>
			<asp:Literal ID="litSinopse" runat="server"></asp:Literal>
		</div>
	</section>
</main>
        </div>
    </form>
</body>
</html>
