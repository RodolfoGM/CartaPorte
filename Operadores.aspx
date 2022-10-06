<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Operadores.aspx.cs" Inherits="CARTAPORTE.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
          <br />
            <br />
    <script>    
    function myFunction() {
        var x = document.getElementById("ocultar-y-mostrar");
    if (x.style.display === "none") {
        x.style.display = "block";
    } else {
        x.style.display = "none";
    }
        }



        function activa(checkbox) {
            document.getElementById('<%= DDLEmpresa2.ClientID %>').disabled = !document.getElementById(checkbox).checked;

            document.getElementById('<%= DDLChoferes.ClientID %>').disabled = !document.getElementById(checkbox).checked;

            document.getElementById('<%= BtnElminar.ClientID %>').disabled = !document.getElementById(checkbox).checked;
        }




        function muestra_oculta(id) {
            if (document.getElementById) { //se obtiene el id
                var el = document.getElementById(id); //se define la variable "el" igual a nuestro div
                el.style.display = (el.style.display == 'none') ? 'block' : 'none'; //damos un atributo display:none que oculta el div
            }
        }


    </script>

     <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">

    <div class="row">

        <div class="col-md-4">
            <asp:Label ID="Label1" runat="server" Text="SELECCIONE EMPRESA"></asp:Label>
            <asp:DropDownList ID="DDLEmpresa2" runat="server" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="DDLEmpresa2_SelectedIndexChanged"></asp:DropDownList>
        </div>
            <div class="col-md-4">
            <asp:Label ID="Label2" runat="server" Text="SELECCIONE CHOFER"></asp:Label>
                <br />
            <asp:DropDownList ID="DDLChoferes" runat="server" AutoPostBack="true" Enabled="false" OnSelectedIndexChanged="DDLChoferes_SelectedIndexChanged"></asp:DropDownList>
        </div>

        <div class="col-md-4">
            <div class="checkbox checkbox-material-grey">
        <asp:Label ID="modifica" runat="server" CssClass="c-white normal f-11 m-b-15" AssociatedControlID="Checkbox1">
            <input type="checkbox" runat="server" id="Checkbox1" class="md-checkbox"  onclick="activa(this.id)" />
            MODIFICAR 
        </asp:Label>
    </div>
</div>
 <asp:Button ID="BtnElminar" runat="server" Text="Eliminar" class="btn btn-danger" Enabled="false"  OnClick="Elminar_Click"  />
    </div>    
    <br />
   
    <hr />
    <br />

    <div id='ocultar-y-mostrar' > 

    
    <div class="row" >

        <div class="col-md-4">
            NOMBRE COMPLETO
            <asp:TextBox class="form-control" ID="txtNombre" placeholder="NOMBRE"  runat="server"></asp:TextBox>
              </div>
        <div class="col-md-4">
            RFC
         <asp:TextBox class="form-control" ID="txtRFC" placeholder="RFC" runat="server"></asp:TextBox>  

        </div>
        <div class="col-md-4">
            EMPRESA A LA QUE IRA ASIGNADO
            <asp:DropDownList class="form-control" ID="DDLEmpresa" runat="server"></asp:DropDownList>
               </div>

    </div>    
       


            <br />
    <div class="row" >
            <br />
        <div class="col-md-4"> <asp:Label ID="Label4" runat="server" Text="LICENCIA"></asp:Label>
            <asp:TextBox class="form-control" placeholder="LICENCIA"  ID="txtLicencia" runat="server"></asp:TextBox>
            </div> 
        <div class="col-md-4">
            <asp:Label ID="Label5" runat="server" Text="CODIGO POSTAL"></asp:Label>
            <asp:TextBox class="form-control" placeholder="CODIGO POSTAL" ID="txtCP" AutoPostBack="true" runat="server" OnTextChanged="txtCP_TextChanged"></asp:TextBox>
            </div>
        <div class="col-md-4">

        
            <asp:Label ID="Label6" runat="server" Text="CALLE"></asp:Label>
            <asp:TextBox class="form-control" placeholder="CALLE" ID="txtCalle" runat="server"></asp:TextBox>
            </div>
        </div>
    
    <br />

            <br />
    <div class="row"  >

    
           <div class="col-md-4">

        
            NUMERO EXTERIOR CASA/DEPARTAMENTO
            <asp:TextBox class="form-control" ID="TxtNumExt" placeholder="NUMERO EXTERIOR"  runat="server"></asp:TextBox>
            </div>

        <div class="col-md-4">
           ESTADO
            <asp:DropDownList class="form-control" ID="DDLEstado"  AutoPostBack="true"  runat="server" OnSelectedIndexChanged="DDLEstado_SelectedIndexChanged"></asp:DropDownList>
        </div>

                <div class="col-md-3">
                    COLONIA
                              <div class="input-group">   
                             <asp:DropDownList class="form-control" ID="DDLColonia" AutoPostBack="true"  runat="server"></asp:DropDownList>
                          <span class="input-group-btn">
                             
                              <asp:ImageButton ImageUrl="imagen/magnify.png" runat="server" OnClick="buscaColonia_Click" />

                         
                 
                         </span>
                        </div>

            
        </div>
     </div>
                <br />
    <br />
    <div class="row"  >
        <div class="col-md-4">

   
            <asp:Label ID="Label8" runat="server" Text="MUNICIPIO"></asp:Label><asp:DropDownList class="form-control" ID="DDLMunicipio" runat="server"></asp:DropDownList>
     </div>
        
            


        <div class="col-md-4">

       
        
            <asp:Label ID="Label9" runat="server" Text="LOCALIDAD"></asp:Label>
            <asp:DropDownList class="form-control" ID="DDLLocalida" runat="server"></asp:DropDownList>
             </div>
        <div class="col-md-4">
            
            </div>
   </div>
        
             <asp:requiredfieldvalidator id="RequiredFieldValidator1"
      controltovalidate="txtNombre"
      validationgroup="Grupo1"
      errormessage="SE NECESITA NOMBRE"
      runat="Server">
    </asp:requiredfieldvalidator>

     <asp:requiredfieldvalidator id="RequiredFieldValidator2"
      controltovalidate="txtRFC"
      validationgroup="Grupo1"
      errormessage="SE NECESITA RFC"
      runat="Server">
    </asp:requiredfieldvalidator>

     <asp:requiredfieldvalidator id="RequiredFieldValidator3"
      controltovalidate="txtLicencia"
      validationgroup="Grupo1"
      errormessage="SE NECESITA LICENCIA"
      runat="Server">
    </asp:requiredfieldvalidator>

     <asp:requiredfieldvalidator id="RequiredFieldValidator4"
      controltovalidate="txtCP"
      validationgroup="Grupo1"
      errormessage="SE NECEISTA CODIGO POSTAL"
      runat="Server">
    </asp:requiredfieldvalidator>
    
     <asp:requiredfieldvalidator id="RequiredFieldValidator5"
      controltovalidate="txtCalle"
      validationgroup="Grupo1"
      errormessage="SE NECEISTA CALLE"
      runat="Server">
    </asp:requiredfieldvalidator>

     <asp:requiredfieldvalidator id="RequiredFieldValidator6"
      controltovalidate="TxtNumExt"
      validationgroup="Grupo1"
      errormessage="SE NECEISTA NUMERO DE CASA"
      runat="Server">
    </asp:requiredfieldvalidator>
    <div class="row"  >
        <div class="col-md-4">
            <br />


            <asp:Button ID="Button1"  class="btn btn-success" runat="server" Text="GUARDAR OPERADOR" ValidationGroup="Grupo1" OnClick="Button1_Click" />
        </div>
    </div>
        </div>
    <br />


</asp:Content>
