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
    public partial class WebForm1 : System.Web.UI.Page
    {
        static int result;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuariologueado"] != null)
            {
                string usuariologueado = Session["usuariologueado"].ToString();

            }
            else
            {
                Response.Redirect("login.aspx");
            }

            if (Checkbox1.Checked==true)
            {  
                BtnElminar.Enabled = true;
                DDLChoferes.Enabled = true;
                DDLEmpresa2.Enabled = true;
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
                    DDLEmpresa.Items.Insert(0, new ListItem("Empresa", "0"));
                    DDLChoferes.Items.Insert(0, new ListItem("CHOFERES DISPONIBLES ", "0"));
                    DDLMunicipio.Items.Insert(0, new ListItem("SELECCIONE MUNICIPIO", "0"));
                    DDLLocalida.Items.Insert(0, new ListItem("SELECCIONE LOCALIDAD", "0"));
                }


                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {
                    SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 27;
                    Da.Fill(Dt);


                    ListItem i;
                    foreach (DataRow r in Dt.Rows)
                    {
                        i = new ListItem(r["Nombre"].ToString(), r["id"].ToString());
                        DDLEmpresa.Items.Add(i);
                        DDLEmpresa2.Items.Add(i);

                        //PAIREC = r["c_Pais"].ToString();

                    }

                    DDLEmpresa2.Items.Insert(0, new ListItem("EMPRESAS DEL GRUPO", "0"));
                }

            }
          
        }

        protected void carga_localidad(string estado) {
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

        protected void Colonia_Chofer(string Colonia)
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


        protected void buscaColonia_Click(object sender, EventArgs e) 
        {

            Colonia_Chofer(txtCP.Text);



        }

        public void carga_Choferes(string id_empresa)

        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                DDLChoferes.Items.Clear();
                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@IDEmpresa", SqlDbType.VarChar).Value = id_empresa;
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 28;
                Da.Fill(Dt);

                ListItem i;
                foreach (DataRow r in Dt.Rows)
                {
                    i = new ListItem(r["NombreOperador"].ToString(), r["RFCOperador"].ToString());
                    DDLChoferes.Items.Add(i);


                    //PAIREC = r["c_Pais"].ToString();

                }
                DDLChoferes.Items.Insert(0, new ListItem("CHOFERES DISPONIBLES ", "0"));
            }
            //TextBox2.Text = "fs

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            

            if (Checkbox1.Checked == true)
            {
                string Emp = DDLEmpresa.SelectedValue.ToString();
                if (Emp == "0")
                {
                    Response.Write("<script>alert('Seleccione Empresa');</script>");
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                    {

                        SqlDataAdapter Da = new SqlDataAdapter("CPSP06", conn);
                        DataTable Dt = new DataTable();
                        Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                        Da.SelectCommand.Parameters.Clear();
                        Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 1;
                        Da.SelectCommand.Parameters.Add("@RFC", SqlDbType.VarChar).Value = txtRFC.Text;
                        Da.SelectCommand.Parameters.Add("@LICENCIA", SqlDbType.VarChar).Value = txtLicencia.Text;
                        Da.SelectCommand.Parameters.Add("@NOMBREOPE", SqlDbType.VarChar).Value = txtNombre.Text;
                        Da.SelectCommand.Parameters.Add("@CALLE", SqlDbType.VarChar).Value = txtCalle.Text;
                        Da.SelectCommand.Parameters.Add("@CODIGOPOSTAL", SqlDbType.VarChar).Value = txtCP.Text;
                        Da.SelectCommand.Parameters.Add("@NUMEROEXT", SqlDbType.VarChar).Value = TxtNumExt.Text;
                        Da.SelectCommand.Parameters.Add("@COLONIA", SqlDbType.VarChar).Value = DDLColonia.SelectedValue.ToString();
                        Da.SelectCommand.Parameters.Add("@ESTADO", SqlDbType.VarChar).Value = DDLEstado.SelectedValue.ToString();
                        Da.SelectCommand.Parameters.Add("@MUNUCIPIO", SqlDbType.VarChar).Value = DDLMunicipio.SelectedValue.ToString();
                        Da.SelectCommand.Parameters.Add("@PAIS", SqlDbType.VarChar).Value = "MEX";
                        Da.SelectCommand.Parameters.Add("@LOCALIDAD", SqlDbType.VarChar).Value = DDLLocalida.SelectedValue.ToString();
                        Da.SelectCommand.Parameters.Add("@EMPRESA", SqlDbType.Int).Value = DDLEmpresa.SelectedValue.ToString();
                        Da.SelectCommand.Parameters.Add("@EMPRESA2", SqlDbType.Int).Value = DDLEmpresa2.SelectedValue.ToString();
                        Da.Fill(Dt);


                        conn.Close();
                    }
                    txtRFC.Text = "";
                    txtLicencia.Text = "";
                    txtNombre.Text = "";
                    txtCP.Text = "";
                    txtCalle.Text = "";
                    TxtNumExt.Text = "";
                }
               



            }
            else
            {
                //Valida que el mismo RFC no este dado de alta para la misma empresa

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {

                    SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    Da.SelectCommand.Parameters.Clear();
                    Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 30;
                    Da.SelectCommand.Parameters.Add("@rfcOperador", SqlDbType.VarChar).Value = txtRFC.Text;
                    Da.SelectCommand.Parameters.Add("@IDEmpresa", SqlDbType.Int).Value = DDLEmpresa.SelectedValue.ToString();

                    Da.Fill(Dt);

                    
                   result  = Dt.Rows.Count;

                    conn.Close();
                }

                if (result==0)
                {
                    string Emp = DDLEmpresa.SelectedValue.ToString();
                    if (Emp == "0")
                    {
                        Response.Write("<script>alert('Seleccione Empresa');</script>");
                    }
                    else
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                        {

                            SqlDataAdapter Da = new SqlDataAdapter("CPSP05", conn);
                            DataTable Dt = new DataTable();
                            Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                            Da.SelectCommand.Parameters.Clear();
                            Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 1;
                            Da.SelectCommand.Parameters.Add("@RFC", SqlDbType.VarChar).Value = txtRFC.Text;
                            Da.SelectCommand.Parameters.Add("@LICENCIA", SqlDbType.VarChar).Value = txtLicencia.Text;
                            Da.SelectCommand.Parameters.Add("@NOMBREOPE", SqlDbType.VarChar).Value = txtNombre.Text;
                            Da.SelectCommand.Parameters.Add("@CALLE", SqlDbType.VarChar).Value = txtCalle.Text;
                            Da.SelectCommand.Parameters.Add("@CODIGOPOSTAL", SqlDbType.VarChar).Value = txtCP.Text;
                            Da.SelectCommand.Parameters.Add("@NUMEROEXT", SqlDbType.VarChar).Value = TxtNumExt.Text;
                            Da.SelectCommand.Parameters.Add("@COLONIA", SqlDbType.VarChar).Value = DDLColonia.SelectedValue.ToString();
                            Da.SelectCommand.Parameters.Add("@ESTADO", SqlDbType.VarChar).Value = DDLEstado.SelectedValue.ToString();
                            Da.SelectCommand.Parameters.Add("@MUNUCIPIO", SqlDbType.VarChar).Value = DDLMunicipio.SelectedValue.ToString();
                            Da.SelectCommand.Parameters.Add("@PAIS", SqlDbType.VarChar).Value = "MEX";
                            Da.SelectCommand.Parameters.Add("@LOCALIDAD", SqlDbType.VarChar).Value = DDLLocalida.SelectedValue.ToString();
                            Da.SelectCommand.Parameters.Add("@EMPRESA", SqlDbType.Int).Value = DDLEmpresa.SelectedValue.ToString();

                            Da.Fill(Dt);


                            conn.Close();
                        }
                        txtRFC.Text = "";
                        txtLicencia.Text = "";
                        txtNombre.Text = "";
                        txtCP.Text = "";
                        txtCalle.Text = "";
                        TxtNumExt.Text = "";

                    }
                }
                else
                {
                    Response.Write("<script>alert('Este Operador ya esta dado de alta para esta empresa');</script>");
                }

                /////





          


            }

          
        }

        protected void DDLChoferes_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {

                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@rfcOperador", SqlDbType.VarChar).Value = DDLChoferes.SelectedValue.ToString();
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 10;
                Da.Fill(Dt);


                try
                {

                txtRFC.Text = Dt.Rows[0]["rfcoperador"].ToString();
                txtLicencia.Text = Dt.Rows[0]["numlicencia"].ToString();
                txtNombre.Text = Dt.Rows[0]["nombreoperador"].ToString();
                txtCP.Text = Dt.Rows[0]["CodigoPostal"].ToString();
                txtCalle.Text = Dt.Rows[0]["Calle"].ToString();
                TxtNumExt.Text = Dt.Rows[0]["NumeroExterior"].ToString();

                DDLEstado.SelectedValue = Dt.Rows[0]["Estado"].ToString();

                carga_localidad(Dt.Rows[0]["Estado"].ToString());
                carga_municipio(Dt.Rows[0]["Estado"].ToString());


                DDLLocalida.SelectedValue = Dt.Rows[0]["Localidad"].ToString();
                DDLMunicipio.SelectedValue = Dt.Rows[0]["Municipio"].ToString();

                Colonia_Chofer(Dt.Rows[0]["CodigoPostal"].ToString());

                DDLColonia.SelectedValue = Dt.Rows[0]["Colonia"].ToString();

                }
                catch (Exception ex)
                {

                    Response.Write("<script>alert('Operador Mal Dado de alta comuniquese con sistemas "+ex+"');</script>");
                }
              
                DDLEmpresa.SelectedValue = Dt.Rows[0]["Id_Empresa"].ToString();

            }
 
        }

        protected void DDLEmpresa2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id_empresa = DDLEmpresa2.SelectedValue.ToString();

            carga_Choferes(id_empresa);
        }


        protected void Elminar_Click(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {

                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                Da.SelectCommand.Parameters.Clear();
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 29;
                Da.SelectCommand.Parameters.Add("@rfcOperador", SqlDbType.VarChar).Value = DDLChoferes.SelectedValue.ToString();
               
                Da.Fill(Dt);


                conn.Close();
            }

        }

        protected void txtCP_TextChanged(object sender, EventArgs e)
        {
            if (txtCP.Text.Length == 5)
            {
                Colonia_Chofer(txtCP.Text);
            }
        }
    }
}