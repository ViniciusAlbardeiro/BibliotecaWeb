﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="resultados.aspx.cs" Inherits="BibliotecaWeb.resultados" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Resultados da Busca</title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@48,400,0,0" />
    <link rel="stylesheet" type="text/css" href="css/estilo.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <header class="sombra">
                <div class="id-site">
                    Biblioteca
                </div>
                
                <section class="area-busca sombra">
                    <asp:DropDownList ID="ddlCategorias" runat="server"></asp:DropDownList>
                    <asp:TextBox ID="txtFiltro" runat="server"></asp:TextBox>
                    <asp:LinkButton ID="pesquisar"  runat="server" OnClick="pesquisar_Click">
                        <img src="images/Buscar.png" />
                    </asp:LinkButton>
                </section>
                
                <section class="area-user">
                    <div class="id-usuario">
                        <p>
                            Olá, bem vindo(a)!<br />
                            <span class="link-login">Entre ou cadastre-se</span>
                        </p>
                        <span class="material-symbols-outlined icone">account_circle</span>
                    </div>
                    <div class="area-icones">
                        <span class="material-symbols-outlined icone icone-favoritos">collections_bookmark</span>
                        <span class="material-symbols-outlined icone icone-notificacao">notifications</span>
                    </div>
                </section>
            </header>

            <main>
                <section>
                    <h1 class="titulo-secao">Nosso acervo</h1>
                    <section class="livros">
                        <asp:Literal ID="litListaLivro" runat="server"></asp:Literal>
                    </section>
                </section>
            </main>
        </div>
    </form>
</body>
</html>