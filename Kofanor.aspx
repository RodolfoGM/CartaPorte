<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Kofanor.aspx.cs" Inherits="CARTAPORTE.Kofanor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
    <div class="text-center">
         <h2 >CARTA PORTE</h2>
    </div>
       

 
                <div class="row" >
                    
                        <div  class="col-md-4">
                             <asp:Label ID="lblEmpresa" runat="server" Text="EMPRESA"></asp:Label> 
                             <asp:DropDownList class="form-control" ID="DDLEmpresa" runat="server"></asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                             <asp:Label ID="lblTipodoc" runat="server" Text="TIPO DOCUMENTO"></asp:Label>    
                             <asp:DropDownList ID="DDLTipoDoc" class="form-control" runat="server"></asp:DropDownList>
                        </div>

                    <div class="col-md-4">   
                            <asp:Label ID="lblCliente" runat="server" Text="CLIENTE"></asp:Label>
                             <asp:TextBox ID="txtCliente" class="form-control"  placeholder="ID CLIENTE" runat="server"></asp:TextBox>
                            
                        </div >
                        
                </div>
    <br />
                    <div > 
                        <div class="col-md-4"> 
                            <asp:Button ID="BtnCliente" runat="server" Text="BUSCAR CLIENTE" class="btn btn-primary" OnClick="BtnCliente_Click" />  </div>
                                 
                    </div>

    <br />
    <br />
    <div class="row">
        <div class="col-md-4">
             ESTADO
             <asp:DropDownList ID="DDLEstado" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="myListDropDown_Change" runat="server"></asp:DropDownList>
        </div>
                <div class="col-md-4">
                     LOCALIDAD
            <asp:DropDownList  ID="DDLLocalida" class="form-control"  runat="server" OnSelectedIndexChanged="DDLLocalida_SelectedIndexChanged"></asp:DropDownList>

        </div>
                <div class="col-md-4">
                    MUNICIPIO
             <asp:DropDownList ID="DDLMunicipio" class="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DDLMunicipio_SelectedIndexChanged"></asp:DropDownList>
 
        </div>

    </div>

            COLONIA&nbsp;
            <asp:DropDownList ID="DDLColonias" class="form-control" runat="server"></asp:DropDownList>
            
            
            <br />
            <br />
    <div class="form-group">
             <div class="row" >
                 <div class="col-md-4">
                   RFC CLIENTE  <asp:TextBox ID="TxtRFCClien" class="form-control" placeholder="RFC CLIENTE" runat="server"></asp:TextBox>
                 </div>
                 <div class="col-md-4">
                   NOMBRE CLIENTE  <asp:TextBox ID="TxtNombreClien" class="form-control" placeholder="NOMBRE CLIENTE" runat="server"></asp:TextBox>
                 </div>
                 <div class="col-md-4">
                   USO DE CFDI  <asp:TextBox ID="TxtUso" class="form-control" placeholder="CFDI" runat="server"></asp:TextBox>
                 </div>
             </div>
        </div>


            PAIS <asp:TextBox ID="TxtPaisClient" class="form-control" placeholder="PAIS" runat="server"></asp:TextBox>
            <br />
            <br />
            <br />

        <div class="text-center">
         <h4 >DATOS CHOFER</h4>
         </div>
          
           
            <br />
            <br />
            <asp:DropDownList ID="DDLOperador" class="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DDLOperador_SelectedIndexChanged"></asp:DropDownList>
            <br />
            
    <div class="row">
        <div class="col-md-4">
            NUMERO DE LICENCIA<asp:TextBox class="form-control" ID="txtNumLic" placeholder="NUMERO DE LICENCIA" runat="server"></asp:TextBox>
        </div>

        <div class="col-md-4">
           CALLE OPERADOR<asp:TextBox ID="TxtCalleOp" class="form-control" placeholder="CALLE OPERADOR" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-4">
            RFC OPERADOR <asp:TextBox ID="TxtRFCOp" class="form-control" placeholder="RFC OPERADOR" runat="server"></asp:TextBox>
        </div>
    </div>
            
            <br />
            
        <div class="row">
            <div class="col-md-4">
               PAIS <asp:TextBox ID="TxtPaisOP" class="form-control"  placeholder="PAIS" runat="server"></asp:TextBox>
            </div>

            <div class="col-md-4">
                COLONIA <asp:TextBox ID="TxtColOp" class="form-control"  placeholder="COLONIA" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-4">
                NUMERO DE CASA<asp:TextBox ID="TxtNumOp" class="form-control"  placeholder="# DE CASA" runat="server"></asp:TextBox>
            </div>
         </div>


            <br />
           

    <div class="row">
        <div class="col-md-4">
             MUNICIPIO <asp:TextBox ID="TxtMunOP" class="form-control"  placeholder="MUNICIPIO" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-4">
             LOCALIDAD <asp:TextBox ID="TxtLocOP" class="form-control"  placeholder="LOCALIDAD" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-4">
             ESTADO
            <asp:TextBox ID="txtEstadoOP" class="form-control"  placeholder="ESTADO" runat="server"></asp:TextBox>
        </div>
    </div>
           
            <br />
          
    <div class="row">
        <div class="col-md-4">
           CODIGO POSTAL <asp:TextBox ID="txtCPOpera" class="form-control"  placeholder="CODIGO POSTAL" runat="server"></asp:TextBox>
        </div>
         <div class="col-md-4">
               FECHA LLEGADA <asp:TextBox textmode="Date" class="form-control" ID="TxtFechallegada" runat="server"></asp:TextBox>
        </div>
         <div class="col-md-4">
              VEHICULO
            <asp:DropDownList ID="DDLAuto" class="form-control" runat="server" >
            </asp:DropDownList>
        </div>
    </div>
    <br />
        <div class="row">
        <div class="col-md-4">
             DESCRIPCION DE VEHICULO<asp:DropDownList ID="DDLConfVehi" class="form-control" runat="server"></asp:DropDownList>
        </div>
         <div class="col-md-4">
              TIENE REMOLQUE
            <asp:DropDownList ID="DDLRemolqueSN" class="form-control" runat="server">
            </asp:DropDownList>
        </div>
         <div class="col-md-4">
              DISTANCIA RECORRIDA EN KM <asp:TextBox ID="TxtDisRec"  placeholder="DISTANCIA RECORRIDA" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>

           
            
          


            <br />
         <div class="text-center">
         <h4 >CONSULTAR INFORMACION DE PRODUCTOS</h4>
         </div>
            <br />

            <div class="row">
        <div class="col-md-4">
               CODIGO <asp:TextBox ID="txtCodigo" class="form-control" runat="server"></asp:TextBox>
        </div>
         <div class="col-md-4">
             DESCRIPCION 
                <asp:TextBox ID="TxtDescripcionA" class="form-control" runat="server"></asp:TextBox>
        </div>
         <div class="col-md-4">
              UNIDAD
                <asp:DropDownList ID="DDLUnidadA" class="form-control" runat="server">
                </asp:DropDownList>
        </div>
    </div>
    <br />
                <div class="row">
        <div class="col-md-4">
              CLAVE SAT
        <asp:TextBox ID="TxtClaveSatA" class="form-control" runat="server"></asp:TextBox>
        </div>
         <div class="col-md-4">
            CANTIDAD
        <asp:TextBox ID="txtCantidadA" class="form-control" runat="server"></asp:TextBox>
        </div>
         <div class="col-md-4">
             PRECIO UNITARIO<asp:TextBox ID="txtPrecioU" class="form-control" runat="server"></asp:TextBox>
        </div>
    </div>



        &nbsp;
&nbsp;&nbsp;
&nbsp;<br />
        IVA<asp:DropDownList ID="DDLIVA" class="form-control" runat="server">
        </asp:DropDownList>
        &nbsp;RETENCION<asp:DropDownList ID="DDLRETEN" class="form-control" runat="server">
        </asp:DropDownList>
    <br />
        <asp:Button ID="btnAgregar" class="btn btn-primary" runat="server" Text="AGREGAR PRODUCTO" Width="205px" OnClick="btnAgregar_Click" />
        <br />
        <br />

    <asp:GridView ID="GridView1" runat="server"  Height="121px" Width="625px"></asp:GridView>
        <br />
    
    <asp:Button ID="Button1" runat="server" type="button" class="btn btn-warning" Text="GENERAR TXT" OnClick="Button1_Click" Width="206px" />
   
    <asp:Button ID="Button2" CssClass="btn btn-warning" runat="server" Text="botton pruebas perronas" OnClick="Button2_Click" />

    <asp:DataGrid   id="data1" runat="server" >

    </asp:DataGrid>
</asp:Content>
