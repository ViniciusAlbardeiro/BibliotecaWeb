<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="BibliotecaWeb.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</head>
<body class="bg-light">

    <form id="form1" runat="server">
        <div class="d-flex justify-content-center align-items-center vh-100">
            <div class="card p-4 shadow-lg rounded" style="width: 350px;">
                <div class="card-body text-center">
                    <h2 class="text-primary">Login</h2>
                    <asp:Literal ID="litMensagem" runat="server" />
                    <div class="mb-3">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                    </div>
                    <div class="mb-3">
                        <asp:TextBox ID="txtSenha" runat="server" CssClass="form-control" TextMode="Password" placeholder="Senha"></asp:TextBox>
                    </div>
                    <asp:LinkButton ID="btnLogin" runat="server" CssClass="btn btn-primary w-100" OnClick="btnLogin_Click">Entrar</asp:LinkButton>
                    

                    <p><a href="#" class="link-dark">É novo aqui? Cadastre-se</a></p>
                </div>
            </div>
        </div>
    </form>

</body>
</html>
