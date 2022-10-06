<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="CARTAPORTE.Clientes" %>
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
            <div class="checkbox checkbox-material-grey">
        <asp:Label ID="modifica" runat="server" CssClass="c-white normal f-11 m-b-15" AssociatedControlID="Checkbox1">
            <input type="checkbox" runat="server" id="Checkbox1" class="md-checkbox"  onclick="activa(this.id)" />
            MODIFICAR 
        </asp:Label>
    </div>
</div>

        <div class="col-md-3">   
                          
                                                   <div class="input-group">   
                            <asp:TextBox ID="txtCliente" class="form-control"  placeholder="ID-CLIENTE" runat="server"> </asp:TextBox>
                          <span class="input-group-btn">
                        <button id="Buscar_Cliente"  class="form-control" data-toggle="modal" data-target="#exampleModalCenter" type="button" > 
                         <span class="material-icons">person_search</span>
                        </button>
                         </span>
                        </div>

                        </div > 

 <asp:Button ID="BtnElminar" runat="server" Text="Eliminar" class="btn btn-danger" Enabled="false" OnClick="BtnElminar_Click"    />
    </div> 

 <!--   <div class="row">
                        <div class="col-md-4">
            <div class="checkbox checkbox-material-grey">
      <asp:Label ID="Label1" runat="server" CssClass="c-white normal f-11 m-b-15" AssociatedControlID="Checkbox2">
            <input type="checkbox" runat="server" id="Checkbox2" class="md-checkbox"  />
            NUEVO 
        </asp:Label>
    </div>
</div>
    </div>
-->

    <br />
       <hr />

            <div class="row" >

                <div class="col-md-4">
                     <asp:Label Text="ID CLIENTE" runat="server" />
                      <asp:TextBox class="form-control" readonly = 'true' ID="txtIDCliente" runat="server"></asp:TextBox>
                </div>
        <div class="col-md-4">
            <asp:Label Text="NOMBRE CLIENTE" runat="server" />
            
            <asp:TextBox class="form-control" ID="txtNombre" placeholder="NOMBRE"  runat="server"></asp:TextBox>
                    <asp:requiredfieldvalidator id="RequiredFieldValidator1"
      controltovalidate="txtNombre"
      validationgroup="Grupo1"
      errormessage="SE NECESITA NOMBRE"
      runat="Server">
    </asp:requiredfieldvalidator>
              </div>
        <div class="col-md-4">
            RFC
         <asp:TextBox class="form-control" ID="txtRFC" placeholder="RFC" runat="server"></asp:TextBox>  
                 <asp:requiredfieldvalidator id="RequiredFieldValidator2"
      controltovalidate="txtRFC"
      validationgroup="Grupo1"
      errormessage="SE NECESITA RFC"
      runat="Server">
    </asp:requiredfieldvalidator>
        </div>

        </div>
            <br />


 
            <div class="row">

    
           

        <div class="col-md-4">
           ESTADO
            <asp:DropDownList class="form-control" ID="DDLEstado"  AutoPostBack="true"  runat="server" OnSelectedIndexChanged="DDLEstado_SelectedIndexChanged"></asp:DropDownList>
        </div>

        <div class="col-md-4">

   
            <asp:Label ID="Label8" runat="server" Text="MUNICIPIO"></asp:Label><asp:DropDownList class="form-control" ID="DDLMunicipio" runat="server"></asp:DropDownList>
     </div>
        
            


        <div class="col-md-4">

       
        
            <asp:Label ID="Label9" runat="server" Text="LOCALIDAD"></asp:Label>
            <asp:DropDownList class="form-control" ID="DDLLocalida" runat="server"></asp:DropDownList>
             </div>

     </div>
        <br />
        <div class="row">

                    <div class="col-md-4">

        

            <asp:Label ID="Label6" runat="server" Text="CALLE"></asp:Label>
            <asp:TextBox class="form-control" placeholder="CALLE" ID="txtCalle" runat="server"></asp:TextBox>
                             <asp:requiredfieldvalidator id="RequiredFieldValidator5"
      controltovalidate="txtCalle"
      validationgroup="Grupo1"
      errormessage="SE NECESITA CALLE"
      runat="Server">
    </asp:requiredfieldvalidator>
            </div>
        <div class="col-md-4">
            <asp:Label ID="Label5" runat="server" Text="CODIGO POSTAL"></asp:Label>
            <asp:TextBox class="form-control" placeholder="CODIGO POSTAL" ID="txtCP" AutoPostBack="true" runat="server" OnTextChanged="txtCP_TextChanged"></asp:TextBox>
                 <asp:requiredfieldvalidator id="RequiredFieldValidator4"
      controltovalidate="txtCP"
      validationgroup="Grupo1"
      errormessage="SE NECESITA CODIGO POSTAL"
      runat="Server">
    </asp:requiredfieldvalidator>
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
    <div class="row"  >
         <div class="col-md-4">

        
            NUMERO EXTERIOR CASA/DEPARTAMENTO
            <asp:TextBox class="form-control" ID="TxtNumExt" placeholder="NUMERO EXTERIOR"  runat="server"></asp:TextBox>
                  <asp:requiredfieldvalidator id="RequiredFieldValidator6"
      controltovalidate="TxtNumExt"
      validationgroup="Grupo1"
      errormessage="SE NECESITA NUMERO DE CASA"
      runat="Server">
    </asp:requiredfieldvalidator>
            </div>


   </div>



    <br />





    



     <asp:Button ID="Btn_Guardar"  class="btn btn-success" runat="server" ValidationGroup="Grupo1" Text="GUARDAR CLIENTE" OnClick="Btn_Guardar_Click" />


    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLongTitle">Busqueda De Cliente</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
            <asp:TextBox ID="TxtClienteModal" class="form-control"  placeholder="RFC" runat="server"> </asp:TextBox>
          <br />
           <asp:TextBox ID="TxtNombClienmodal" class="form-control"  placeholder="Nombre Cliente" runat="server"> </asp:TextBox>
          <br />

          <asp:GridView ID="GridModal"  class="table table-bordered table-condensed table-hover" runat="server" >
                  <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="chkSelect" runat="server"  />
            </ItemTemplate>
        </asp:TemplateField>
        
    </Columns>

          </asp:GridView>
                    <br />
          <asp:Button id="BuscarModal" type="button"  class="btn btn-primary" Text="Buscar Cliente"  OnClick="BuscarModal_Click"   runat="server" />
          



          <asp:Label Text="" ID="lblMessage" runat="server" />
                                      </ContentTemplate>
          </asp:UpdatePanel>
      </div>
      <div class="modal-footer">
          <asp:Button Text="Cerrar" runat="server" id="Cerrar_Modal"  class="btn btn-primary" OnClick="Cerrar_Modal_Click"   />
           <asp:Button Text="Aceptar" runat="server" id="Aceptar_Modal" class="btn btn-primary" OnClick="Aceptar_Modal_Click" />

      </div>
    </div>
  </div>
</div>
</asp:Content>
