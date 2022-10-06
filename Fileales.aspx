<%@ Page Title="-" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  Inherits="CARTAPORTE.Nutri" %>




<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />  

    <script type="text/javascript">            
    function ValidNumeric() {    
    
    var charCode = (event.which) ? event.which : event.keyCode;    
    if (charCode >= 48 && charCode <= 57)    
    { return true; }    
    else    
        { return false; }
        }


    </script>

      <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">


                <asp:Panel ID="PanelMensajes" runat="server">
                    <asp:Label ID="EtiquetaMensajes" runat="server" ></asp:Label>
    </asp:Panel>

                <div class="row" >
                    
                        <div  class="col-md-4">
                            
                             <asp:DropDownList class="form-control" ID="DDLEmpresa" AutoPostBack="true"  runat="server" OnSelectedIndexChanged="DDLEmpresa_SelectedIndexChanged1" ></asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                             
                             <asp:DropDownList ID="DDLTipoDoc" class="form-control" runat="server"></asp:DropDownList>
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
                                   <asp:requiredfieldvalidator id="RequiredFieldValidator6"
                                      controltovalidate="txtCliente"
                                      validationgroup="Grupo2"
                                      errormessage="se necesita IDCliente"
                                      runat="Server">
                                   </asp:requiredfieldvalidator>
                        </div > 

                <div class="col-auto">
          
            <asp:Button  runat="server" class="btn btn-primary" CausesValidation="true" ValidationGroup="Grupo2" Text="+" style="font-weight:bold;" OnClick="Add_Client_Click"/>
          
                       
        </div>
                </div>
    <br />



    <asp:GridView ID="GVClientes" class="table table-bordered table-condensed table-hover" runat="server">
                                  <Columns>

        <asp:TemplateField HeaderText="del">
                <ItemTemplate>
                    <asp:Button ID="ElimClienGrid" runat="server" onclick="ElimClienGrid_Click" Text="Eliminar" />
                </ItemTemplate>
            </asp:TemplateField>
    </Columns>

    </asp:GridView>


        <div class="text-center">
         <h4 >DATOS CHOFER</h4>
         </div>


    <div class="row">
                <div class="col-md-4">
           OPERADOR <asp:DropDownList ID="DDLOperador" class="form-control" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DDLOperador_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="col-md-4">
            NUMERO DE LICENCIA<asp:TextBox class="form-control" ID="txtNumLic" placeholder="NUMERO DE LICENCIA" runat="server"></asp:TextBox>
        </div>

        <div class="col-md-4">
            RFC OPERADOR <asp:TextBox ID="TxtRFCOp" class="form-control" placeholder="RFC OPERADOR" runat="server"></asp:TextBox>
        </div>



    </div>



            <br />

          
    <div class="row">

                         <div class="col-md-4">
               FECHA LLEGADA <asp:TextBox  type="date" class="form-control" id="TxtFechallegada"  runat="server" onchange="ValidDate()"></asp:TextBox>
        </div>
                 <div class="col-md-4">
              TIPO PERMISO SCT
            <asp:DropDownList ID="DDLTipoPermiso" AutoPostBack="true" class="form-control" runat="server" >
            </asp:DropDownList>
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
             
            TIENE REMOLQUE <asp:DropDownList  id="DDLRemolqueSN" class="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDLRemolqueSN_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
         <div class="col-md-4">
              DISTANCIA RECORRIDA EN KM <asp:TextBox ID="TxtDisRec"  placeholder="DISTANCIA RECORRIDA" class="form-control" runat="server"  onkeypress="return ValidNumeric()"></asp:TextBox>
        </div>
    </div>
    <br />

           
            
          


     
     <div class="row"  >
        <div class="col-md-3">
        <asp:Label ID="lblremolque" runat="server"   Text="REMOLQUE 1"></asp:Label> <asp:DropDownList ID="ddlremolque1"  runat="server" AutoPostBack="true"  class="form-control"></asp:DropDownList>
        </div>

        <div class="col-md-3">
        <asp:Label ID="lblplaca1" runat="server"  Text="PLACA 1"></asp:Label>  <asp:TextBox id="TXTPLACA1" name="TXTPLACA 1" runat="server" class="form-control"></asp:TextBox>
        </div>

        <div class="col-md-3">
        <asp:Label ID="lblremolque2" runat="server"   Text="REMOLQUE 2"></asp:Label> <asp:DropDownList ID="ddlremolque2" runat="server" AutoPostBack="true" class="form-control" ></asp:DropDownList>
        </div>

         <div class="col-md-3">
        <asp:Label ID="lblplaca2" runat="server" Text="PLACA 2"></asp:Label> <asp:TextBox id="txtplac2"  class="form-control" name="TXTPLACA 2"  runat="server" ></asp:TextBox>
        </div>

    </div>

           
            
          


            <br />
         <div class="text-center">
         <h4 >CONSULTAR INFORMACION DE PRODUCTOS</h4>
         </div>
            <br />

            <div class="row">

         <div class="col-md-4">
             DESCRIPCION 
                <asp:TextBox ID="TxtDescripcionA" class="form-control" runat="server"  ></asp:TextBox>
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
        <asp:TextBox ID="TxtClaveSatA" class="form-control" runat="server" onkeypress="return ValidNumeric()"></asp:TextBox>
        </div>
         <div class="col-md-4">
            CANTIDAD
        <asp:TextBox ID="txtCantidadA" class="form-control" runat="server"  onkeypress="return ValidNumeric()"></asp:TextBox>
        </div>
 <div class="col-md-4">
             PESO TOTAL
             <asp:TextBox ID="txtPESO" class="form-control" runat="server" onkeypress="return ValidNumeric()"></asp:TextBox>
        </div>
    </div>
    <br />
    <div class="row">
       
    </div>
             
    <asp:requiredfieldvalidator id="RequiredFieldValidator1"
      controltovalidate="txtPESO"
      validationgroup="Grupo1"
      errormessage="se necesita peso"
      runat="Server">
    </asp:requiredfieldvalidator>

     <asp:requiredfieldvalidator id="RequiredFieldValidator2"
      controltovalidate="txtCantidadA"
      validationgroup="Grupo1"
      errormessage="se necesita cantidad"
      runat="Server">
    </asp:requiredfieldvalidator>

     <asp:requiredfieldvalidator id="RequiredFieldValidator3"
      controltovalidate="TxtClaveSatA"
      validationgroup="Grupo1"
      errormessage="se necesita Clave SAT"
      runat="Server">
    </asp:requiredfieldvalidator>

     <asp:requiredfieldvalidator id="RequiredFieldValidator4"
      controltovalidate="TxtDescripcionA"
      validationgroup="Grupo1"
      errormessage="se necesita Descripcion"
      runat="Server">
    </asp:requiredfieldvalidator>


        &nbsp;<br />



        <asp:GridView ID="GridView2" class="table table-bordered table-condensed table-hover" runat="server" >
    </asp:GridView>

      <div class="row">
          <div class="auto">
           &nbsp;&nbsp;&nbsp;&nbsp;
           <asp:Button ID="btnAgregar" class="btn btn-primary" runat="server" CausesValidation="true" ValidationGroup="Grupo1" Text="AGREGAR PRODUCTO" Width="205px" OnClick="btnAgregar_Click" />
          <asp:Button ID="BtnDelet" class="btn btn-danger" runat="server" Text="BORRAR" Width="205px" OnClick="BtnDelet_Click"  />
          </div>
       
    </div>


        <br />

    
    <asp:Button ID="Button1" runat="server" type="button" class="btn btn-success" Text="GENERAR DOCUMENTO" OnClick="Button1_Click" Width="206px" />


        
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
          <asp:Button id="BuscarModal" type="button"  class="btn btn-primary" Text="Buscar Cliente"  OnClick="BuscarModal_Click" runat="server" />
          



          <asp:Label Text="" ID="lblMessage" runat="server" />
                                      </ContentTemplate>
          </asp:UpdatePanel>
      </div>
      <div class="modal-footer">
          <asp:Button Text="Cerrar" runat="server" id="Cerrar_Modal"  class="btn btn-primary"   OnClick="Cerrar_Modal_Click" />
          <asp:Button Text="Aceptar" runat="server" id="Aceptar_Modal" class="btn btn-primary" OnClick="Aceptar_Modal_Click" />

      </div>
    </div>
  </div>
</div>


</asp:Content>
