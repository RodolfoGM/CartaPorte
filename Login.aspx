<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CARTAPORTE.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/js/bootstrap.bundle.min.js" ></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js" ></script>
    <link href="Estilos.css" rel="stylesheet" />
    <title></title>
</head>
<body class="bg-light">
    <div class="wrapper">
        <div class="formcontent">
            <form id="login_Form" runat="server">
                <br />
                <br />
                <br />
                <br />
                <div class="form-control">
                    <div class=" text-center mb-5">
                        <asp:Label cass="h2" ID="lblBienvendida" runat="server" Text="CFDI DE TRASLADO CON COMPLEMENTO CARTA PORTE"></asp:Label>

                    </div>

                    <div>
                        <asp:Label ID="lblUsuario" runat="server" Text="Usuario:"></asp:Label>
                        <asp:TextBox ID="tbUsuario" CssClass="form-control" placeholder="Nombre Usuario" runat="server"></asp:TextBox>
                    </div>

                    <div>
                        <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                        <asp:TextBox ID="tbPassword" CssClass="form-control" TextMode="Password" placeholder="Password" runat="server"></asp:TextBox>

                    </div>
                    <div class="row">
                    <asp:Label  runat="server"  CssClass="alert-danger" ID="lblError"  ></asp:Label>

                    </div>

                    <hr />
                    
                    
                    <div class="row">
                        <asp:Button ID="btnEntrar" runat="server" CssClass="btn btn-primary btn-dark" Text="Ingresar" OnClick="btnEntrar_Click" />

                    </div>
                </div>

         </form>
        
        </div>

    </div>

</body>

