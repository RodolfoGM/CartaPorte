using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using CARTAPORTE.Utils;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using CARTAPORTE.ServiceReferenceFC;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Windows.Forms;

namespace CARTAPORTE
{
    public partial class Clientes : System.Web.UI.Page
    {
        static int result;
        static string mun;
        static string calle;
        static string local;
        static int bandera1;
        static int Variable;

        protected void Page_Load(object sender, EventArgs e)
        {
           Session.Timeout = 15;

            if (Session["usuariologueado"] != null)
            {
                string usuariologueado = Session["usuariologueado"].ToString();

            }
            else
            {
                Response.Redirect("login.aspx");
            }

            if (Checkbox1.Checked == true)
            {
                BtnElminar.Enabled = true;
              
            }
            if (!this.IsPostBack)
            {


                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {
                    SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    // Da.SelectCommand.Parameters.Add("@Estado", SqlDbType.VarChar).Value = ESTREC;
                    Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 5;
                    Da.Fill(Dt);


                    ListItem i;
                    foreach (DataRow r in Dt.Rows)
                    {
                        i = new ListItem(r["Nombre"].ToString(), r["c_Estado"].ToString());
                        DDLEstado.Items.Add(i);


                        //PAIREC = r["c_Pais"].ToString();

                    }
                    DDLEstado.Items.Insert(0, new ListItem("SELECCIONE ESTADO", "0"));
                    DDLColonia.Items.Insert(0, new ListItem("SELECCIONE COLONIA", "0"));
                   
                   
                    DDLMunicipio.Items.Insert(0, new ListItem("SELECCIONE MUNICIPIO", "0"));
                    DDLLocalida.Items.Insert(0, new ListItem("SELECCIONE LOCALIDAD", "0"));
                    conn.Close();
                }


             

            }

        }

        protected void carga_localidad(string estado)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                DDLLocalida.Items.Clear();
                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@Estado", SqlDbType.VarChar).Value = estado;
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 8;
                Da.Fill(Dt);

                ListItem i;
                foreach (DataRow r in Dt.Rows)
                {
                    i = new ListItem(r["descripción"].ToString(), r["c_Localidad"].ToString());
                    DDLLocalida.Items.Add(i);


                    //PAIREC = r["c_Pais"].ToString();

                }
            }

        }
        protected void carga_municipio(string estado)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                DDLMunicipio.Items.Clear();
                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@Estado", SqlDbType.VarChar).Value = estado;
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 14;
                Da.Fill(Dt);

                ListItem i;
                foreach (DataRow r in Dt.Rows)
                {
                    i = new ListItem(r["descripción"].ToString(), r["c_Municipio"].ToString());
                    DDLMunicipio.Items.Add(i);


                    //PAIREC = r["c_Pais"].ToString();

                }
            }
            //TextBox2.Text = "fs

        }


        protected void DDLEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string estado = DDLEstado.SelectedValue.ToString();

            carga_localidad(estado);
            carga_municipio(estado);
        }

        protected void Colonia_Clientes(string Colonia)
        {
            DDLColonia.Items.Clear();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@codigo_postal", SqlDbType.VarChar).Value = Colonia;
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 15;
                Da.Fill(Dt);

                ListItem i;
                foreach (DataRow r in Dt.Rows)
                {
                    i = new ListItem(r["Nombre_de_Asentamiento"].ToString(), r["c_colonia"].ToString());
                    DDLColonia.Items.Add(i);


                    //PAIREC = r["c_Pais"].ToString();

                }
            }
        }

        protected void buscaColonia_Click(object sender, ImageClickEventArgs e)
        {
            Colonia_Clientes(txtCP.Text);
        }

       

        protected void DDLCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }


        protected void Btn_Guardar_Click(object sender, EventArgs e)
        {
            bandera1 = 0;
            if (Checkbox1.Checked == true)
            {

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                    {

                        SqlDataAdapter Da = new SqlDataAdapter("CPSP07", conn);
                        DataTable Dt = new DataTable();
                        Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                        Da.SelectCommand.Parameters.Clear();
                        Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 1;
                        Da.SelectCommand.Parameters.Add("@IDCLIENTE", SqlDbType.VarChar).Value = txtIDCliente.Text;
                        Da.SelectCommand.Parameters.Add("@RFC", SqlDbType.VarChar).Value = txtRFC.Text;
                        Da.SelectCommand.Parameters.Add("@NombreCliente", SqlDbType.VarChar).Value = txtNombre.Text;
                        Da.SelectCommand.Parameters.Add("@CALLE", SqlDbType.VarChar).Value = txtCalle.Text;
                        Da.SelectCommand.Parameters.Add("@CODIGOPOSTAL", SqlDbType.VarChar).Value = txtCP.Text;
                        Da.SelectCommand.Parameters.Add("@NUMEROEXT", SqlDbType.VarChar).Value = TxtNumExt.Text;
                        Da.SelectCommand.Parameters.Add("@COLONIA", SqlDbType.VarChar).Value = DDLColonia.SelectedValue.ToString();
                        Da.SelectCommand.Parameters.Add("@ESTADO", SqlDbType.VarChar).Value = DDLEstado.SelectedValue.ToString();
                        Da.SelectCommand.Parameters.Add("@MUNUCIPIO", SqlDbType.VarChar).Value = DDLMunicipio.SelectedValue.ToString();
                        Da.SelectCommand.Parameters.Add("@PAIS", SqlDbType.VarChar).Value = "MEX";
                        Da.SelectCommand.Parameters.Add("@LOCALIDAD", SqlDbType.VarChar).Value = DDLLocalida.SelectedValue.ToString();
                       // Da.SelectCommand.Parameters.Add("@EMPRESA", SqlDbType.Int).Value = DDLEmpresa.SelectedValue.ToString();
                        Da.Fill(Dt);


                        conn.Close();
                    }
                limpia_Campos();
                limpia_Modal();


                Response.Write("<script>alert('SE MODIFICO EL CLIENTE CORRECTAMENTE ');</script>");


            }
            else
            {
                //Valida si el cliente tiene varias direcciones que  no se agregue nuevamente una repetida, los campos que se validan no sean iguales es municipio,localidad y calle

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {

                    SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    Da.SelectCommand.Parameters.Clear();
                    Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 32;
                    Da.SelectCommand.Parameters.Add("@RFCCliente", SqlDbType.VarChar).Value = txtRFC.Text;
                    

                    Da.Fill(Dt);
                    result = Dt.Rows.Count;
                   
                    mun = DDLMunicipio.SelectedValue.ToString();
                    local = DDLLocalida.SelectedValue.ToString();
                    calle = txtCalle.Text;


                    foreach (DataRow r in Dt.Rows)
                    {
                       
                        if ((mun == r["MUNICIPIO"].ToString()) && (local==r["LOCALIDAD"].ToString()) &&(calle==r["CALLE"].ToString()))
                        {
                            bandera1 = 1;
                            break;
                        }

                    }

                   

                    conn.Close();
                }

                if (bandera1==0)
                {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                        {

                            SqlDataAdapter Da = new SqlDataAdapter("CPSP08", conn);
                            DataTable Dt = new DataTable();
                            Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                            Da.SelectCommand.Parameters.Clear();
                            Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 1;
                            Da.SelectCommand.Parameters.Add("@RFC", SqlDbType.VarChar).Value = txtRFC.Text;
                            //Da.SelectCommand.Parameters.Add("@LICENCIA", SqlDbType.VarChar).Value = txtLicencia.Text;
                            Da.SelectCommand.Parameters.Add("@NOMBRE", SqlDbType.VarChar).Value = txtNombre.Text;
                            Da.SelectCommand.Parameters.Add("@CALLE", SqlDbType.VarChar).Value = txtCalle.Text;
                            Da.SelectCommand.Parameters.Add("@CODIGOPOSTAL", SqlDbType.VarChar).Value = txtCP.Text;
                            Da.SelectCommand.Parameters.Add("@NUMEROEXT", SqlDbType.VarChar).Value = TxtNumExt.Text;
                            Da.SelectCommand.Parameters.Add("@COLONIA", SqlDbType.VarChar).Value = DDLColonia.SelectedValue.ToString();
                            Da.SelectCommand.Parameters.Add("@ESTADO", SqlDbType.VarChar).Value = DDLEstado.SelectedValue.ToString();
                            Da.SelectCommand.Parameters.Add("@MUNUCIPIO", SqlDbType.VarChar).Value = DDLMunicipio.SelectedValue.ToString();
                            Da.SelectCommand.Parameters.Add("@PAIS", SqlDbType.VarChar).Value = "MEX";
                            Da.SelectCommand.Parameters.Add("@LOCALIDAD", SqlDbType.VarChar).Value = DDLLocalida.SelectedValue.ToString();
                           // Da.SelectCommand.Parameters.Add("@EMPRESA", SqlDbType.Int).Value = DDLEmpresa.SelectedValue.ToString();

                            Da.Fill(Dt);


                            conn.Close();
                        }
                    limpia_Campos();
                    limpia_Modal();

                        Response.Write("<script>alert('SE CREO EL CLIENTE CORRECTAMENTE');</script>");

                }
                else
                {
                    Response.Write("<script>alert('YA EXISTE ESTE CLIENTE DADO DE ALTA');</script>");
                }

                     



            }
        }
        protected void BindData()
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@RFCCliente", SqlDbType.VarChar).Value = TxtClienteModal.Text;
               
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 22;
                Da.Fill(Dt);


                GridModal.DataSource = Dt;
                GridModal.DataBind();



                conn.Close();
            }
        }

        protected void Aceptar_Modal_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (GridViewRow row in GridModal.Rows)
            {
                System.Web.UI.WebControls.CheckBox ch = (System.Web.UI.WebControls.CheckBox)row.FindControl("chkSelect");
                if (ch.Checked)
                {
                    count++;
                    if (count > 1)
                    {
                        lblMessage.Text = "Seleccione solo 1 cliente";
                        return;
                    }
                }
            }

            string valor1;
            foreach (GridViewRow row in GridModal.Rows)
            {
                System.Web.UI.WebControls.CheckBox ch = (System.Web.UI.WebControls.CheckBox)row.FindControl("chkSelect");
                if (ch.Checked)
                {
                    valor1 = row.Cells[1].Text;

                    txtCliente.Text = valor1;


                }
            }

            if (txtCliente.Text == "")
            {

            }
            else
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {

                    SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.Add("@ID_CLIENTE", SqlDbType.VarChar).Value = txtCliente.Text;
                    Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 34;
                    Da.Fill(Dt);


                    try
                    {

                        txtRFC.Text = Dt.Rows[0]["rfc"].ToString();
                        // txtLicencia.Text = Dt.Rows[0]["numlicencia"].ToString();
                        txtNombre.Text = Dt.Rows[0]["nombre"].ToString();
                        txtCP.Text = Dt.Rows[0]["Codigo_Postal"].ToString();
                        txtCalle.Text = Dt.Rows[0]["Calle"].ToString();
                        TxtNumExt.Text = Dt.Rows[0]["Numero_EXT"].ToString();
                        txtIDCliente.Text = Dt.Rows[0]["ID"].ToString();

                        DDLEstado.SelectedValue = Dt.Rows[0]["Estado"].ToString();

                        carga_localidad(Dt.Rows[0]["Estado"].ToString());
                        carga_municipio(Dt.Rows[0]["Estado"].ToString());


                        DDLLocalida.SelectedValue = Dt.Rows[0]["Localidad"].ToString();
                        DDLMunicipio.SelectedValue = Dt.Rows[0]["Municipio"].ToString();

                        Colonia_Clientes(Dt.Rows[0]["Codigo_Postal"].ToString());

                        DDLColonia.SelectedValue = Dt.Rows[0]["Colonia"].ToString();

                    }
                    catch (Exception ex)
                    {

                        Response.Write("<script>alert('cliente Mal Dado de alta comuniquese con sistemas " + ex + "');</script>");
                    }

                    
                }
            }


            limpia_Modal();



        }

        protected void BuscarModal_Click(object sender, EventArgs e)
        {
            if (TxtClienteModal.Text == "")
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {
                    SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.Add("@NombreCliente", SqlDbType.VarChar).Value = TxtNombClienmodal.Text;                  
                    Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 23;
                    Da.Fill(Dt);


                    GridModal.DataSource = Dt;
                    GridModal.DataBind();



                    conn.Close();
                }
            }
            else
            {
                BindData();
            }
        }
        protected void Cerrar_Modal_Click(object sender, EventArgs e)
        {
            limpia_Modal();


        }

        protected void BtnElminar_Click(object sender, EventArgs e)
        {
            if (Checkbox1.Checked == true)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {
                    SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.Add("@ID_CLIENTE", SqlDbType.VarChar).Value = txtCliente.Text;
                    Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 35;
                    Da.Fill(Dt);


                    GridModal.DataSource = Dt;
                    GridModal.DataBind();



                    conn.Close();
                }
                limpia_Campos();
                limpia_Modal();

            }
        }

        private void limpia_Modal() {

            GridModal.DataSource = "";
            GridModal.DataBind();
            //limpia modal
            TxtClienteModal.Text = "";
            TxtNombClienmodal.Text = "";
            lblMessage.Text = "";

        }

        private void limpia_Campos() {
            txtCliente.Text = "";
            txtIDCliente.Text = "";
            txtNombre.Text = "";
            txtRFC.Text = "";
            txtCalle.Text = "";
            txtCP.Text = "";
            TxtNumExt.Text   = "";

        }


        protected void txtCP_TextChanged(object sender, EventArgs e)
        {
            if (txtCP.Text.Length == 5)
            {
                Colonia_Clientes(txtCP.Text);
            }
        }
    }
}