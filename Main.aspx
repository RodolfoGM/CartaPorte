<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CARTAPORTE._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">



    <div class="row">
        <div class="col-md-4">
            <h2>Carta Porte</h2>
            <p>
               Carta porte porte de porte

            </p>
            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
            <p>
                <a class="btn btn-default" >Generar XML &raquo;</a>
            </p>
            <asp:Button ID="Button1" runat="server" Text="Button" />
        </div>


    </div>

</asp:Content>