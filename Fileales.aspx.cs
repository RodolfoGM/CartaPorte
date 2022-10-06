using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class Nutri : System.Web.UI.Page
    {
        #region Variables
        //modifiquen por su path
        static private string path = @"C:\Users\desarrollador.oracle\Desktop\CARTAPORTE\";
        static string pathXML = path + @"Elxml.xml";

        static string pathCer = path + @"00001000000507105277.cer";
        static string pathKey = path + @"CSD_ALIEMTOS_KOWI_AKO971007558_20210415_142921.key";
        static string clavePrivada = "alimentoskowi97";

        static string Empresa = "";
        string vers = "VERSIO 3.3";

        static string CalleEmi;
        static string ColEmi;
        static string MuniEmi;
        static string EstEmi;
        static string PaisEmi;
        static string CPEmi;
        static string SERIE;
        static string FOLIO;

        static string RFCREC;
        static string NOMREC;
        static string CALREC;
        static string NEXREC;
        static string NINREC;
        static string COLREC;
        static string MUNREC;
        static string LOCREC;
        static string ESTREC;
        static string PAIREC;
        static string CODREC;
        static string POLIZA;
        static string PERMISOSCT;
        static string NOMBREASEGU;

        static string SAT_COL;
        static string SAT_EST;
        static string SAT_PAIS;
        static string SAT_MUN;
        static string SAT_LOCAL;

        static string ANOAUT;
        // <summary>
        static string CPEMPRESA;
        static string RFCOPERADOR;
        static string NUMEROLICENCIA;
        static string PAISOPERADOR;
        static string CALLEOPERADOR;
        static string NUMEROPERADOR;
        static string MUNICIPIOOPERADOR;
        static string LOCALIDADOPERADOR;
        static string COLONIAOPERADOR;
        static string ESTADOPERADOR;
        static string CPOPERADOR;
        static string REGIMEN;
        static string idempresa;
        static string RFCEmpresaGlob;
        static string BU;

        static string input;
        static int PESOBRUTO;
        static int MERCANCIAS;

        string version = "2.0";

        #endregion Variables


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

            if (!this.IsPostBack)
            {

                {



                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                    {


                        List<string> lista = new List<string>();


                        SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                        DataTable Dt = new DataTable();
                        Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        //Da.SelectCommand.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = TxtNombre.Text;
                        Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 4;
                        //Da.SelectCommand.Parameters.Add("@IDEmpresa", SqlDbType.VarChar).Value = Empresa;
                        Da.Fill(Dt);


                        ListItem i;
                        foreach (DataRow r in Dt.Rows)
                        {
                            i = new ListItem(r["Nombre"].ToString(), r["ID"].ToString());
                            //DDLEmpresa.Items.Add(i);
                            //Empresa = r["ID"].ToString();
                            //CalleEmi = r["CALLE"].ToString();
                            //ColEmi = r["Colonia"].ToString();
                            //MuniEmi = r["Municipio"].ToString();
                            //EstEmi = r["Estado"].ToString();
                            //PaisEmi = r["Pais"].ToString();
                            //CPEmi = r["CP"].ToString();
                            //SERIE = r["SERIE"].ToString();
                            DDLEmpresa.Items.Add(i);
                        }
                        DDLEmpresa.Items.Insert(0, new ListItem("Seleccione Empresa", "0"));
                        conn.Close();
                    }


                }
                //Llena Si es traslado o ingreso
                ListItem x;
                x = new ListItem("Traslado", "T");
                DDLTipoDoc.Items.Add(x);
                x = new ListItem("Ingreso", "I");
                DDLTipoDoc.Items.Add(x);

                ddlremolque1.Visible = false;
                ddlremolque2.Visible = false;
                TXTPLACA1.Visible = false;
                txtplac2.Visible = false;
                lblremolque.Visible = false;
                lblremolque2.Visible = false;
                lblplaca1.Visible = false;
                lblplaca2.Visible = false;


                //DDLRETEN.Items.Insert(0, new ListItem("Seleccione opcion", "0"));
                //DDLRETEN.Items.Insert(1, new ListItem("Retencion 4%", "4"));

                //DDLIVA.Items.Insert(0, new ListItem("Seleccione opcion", "0"));
                //DDLIVA.Items.Insert(1, new ListItem("Retencion 16%", "16"));
                //  DDLRETEN.Items.Insert(2, new ListItem("No", "2"));

               

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {
                    List<string> lista = new List<string>();


                    SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 9;

                    Da.Fill(Dt);


                    ListItem z;
                    foreach (DataRow r in Dt.Rows)
                    {
                        z = new ListItem(r["NombreOperador"].ToString(), r["Rfcoperador"].ToString());
                        DDLOperador.Items.Add(z);

                    }
                    DDLOperador.Items.Insert(0, new ListItem("Seleccionar Chofer", "0"));
                    conn.Close();
                }

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {
                    List<string> lista = new List<string>();


                    SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 20;

                    Da.Fill(Dt);


                    ListItem z;
                    foreach (DataRow r in Dt.Rows)
                    {
                        z = new ListItem(r["Descripcion"].ToString(), r["Clave"].ToString());
                        DDLTipoPermiso.Items.Add(z);

                    }
                    DDLTipoPermiso.Items.Insert(0, new ListItem("Selecciona un permiso", "0"));
                    conn.Close();
                }

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {
                    List<string> lista = new List<string>();


                    SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 11;

                    Da.Fill(Dt);


                    ListItem z;
                    foreach (DataRow r in Dt.Rows)
                    {
                        z = new ListItem(r["Descripcion"].ToString(), r["Clave"].ToString());
                        DDLConfVehi.Items.Add(z);

                    }
                    DDLConfVehi.Items.Insert(0, new ListItem("Seleccione Tipo Vehiculo", "0"));
                    conn.Close();
                }


                DDLRemolqueSN.Items.Insert(0, new ListItem("Seleccione opcion", "0"));
                DDLRemolqueSN.Items.Insert(1, new ListItem("Si", "1"));
                DDLRemolqueSN.Items.Insert(2, new ListItem("No", "2"));



                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {
                    List<string> lista = new List<string>();


                    SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 16;

                    Da.Fill(Dt);


                    ListItem z;
                    foreach (DataRow r in Dt.Rows)
                    {
                        z = new ListItem(r["Nombre"].ToString(), r["Clave"].ToString());
                        DDLUnidadA.Items.Add(z);

                    }
                    DDLUnidadA.Items.Insert(0, new ListItem("Seleccione Unidad de Medida", "0"));
                    conn.Close();
                }



            }

        }

        
            public void cargar_choferes(string idempresa,string id_vales)
        {
           

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {
                    List<string> lista = new List<string>();


                    SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 9;
                    Da.SelectCommand.Parameters.Add("@idempresa", SqlDbType.Int).Value = idempresa;
                    Da.SelectCommand.Parameters.Add("@ID_VALES", SqlDbType.Int).Value = id_vales; 
                    Da.Fill(Dt);


                    ListItem z;
                    foreach (DataRow r in Dt.Rows)
                    {
                        z = new ListItem(r["NombreOperador"].ToString(), r["Rfcoperador"].ToString());
                        DDLOperador.Items.Add(z);

                    }
                    DDLOperador.Items.Insert(0, new ListItem("Seleccionar Chofer", "0"));
                    conn.Close();
                }

        }


        public void Cargar_Vechiculos(string idempresa)
        {
           using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                List<string> lista = new List<string>();

                DDLAuto.Items.Clear();

                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@IDEmpresa", SqlDbType.VarChar).Value = idempresa;
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 13;

                Da.Fill(Dt);


                ListItem z;
                foreach (DataRow r in Dt.Rows)
                {
                    z = new ListItem(r["unidad"].ToString() + " - " + r["placas"].ToString(), r["placas"].ToString());
                    DDLAuto.Items.Add(z);

                }
                DDLAuto.Items.Insert(0, new ListItem("Seleccione Automovil", "0"));
                conn.Close();
            }

        }

        protected void BtnCliente_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {

                //List<string> lista = new List<string>();


                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@AliCliente", SqlDbType.VarChar).Value = txtCliente.Text;
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 6;
                Da.Fill(Dt);

                if (Dt.Rows.Count == 0)
                {

                }
                else
                {
                    //TxtUso.Text = Dt.Columns[3].ToString();



                    RFCREC = Dt.Rows[0]["RFC"].ToString();
                    NOMREC = Dt.Rows[0]["Nombre"].ToString();
                    CALREC = Dt.Rows[0]["CALLE"].ToString();

                    NEXREC = Dt.Rows[0]["NUMEXT"].ToString();
                    NINREC = Dt.Rows[0]["NUMINT"].ToString();
                    COLREC = Dt.Rows[0]["COLONIA"].ToString();
                    MUNREC = Dt.Rows[0]["CIUDAD"].ToString();
                    ESTREC = Dt.Rows[0]["ESTADO"].ToString();
                    PAIREC = Dt.Rows[0]["Pais"].ToString();
                    CODREC = Dt.Rows[0]["CODIGO_POSTAL"].ToString();


                }

                conn.Close();
            }



        }

        protected void DDLOperador_SelectedIndexChanged(object sender, EventArgs e)
        {

            string rfcO = DDLOperador.SelectedValue.ToString();


            Carga_Chofer(rfcO);

        }

        public void Carga_Chofer(string rfc)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@rfcOperador", SqlDbType.VarChar).Value = rfc;
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 10;
                Da.Fill(Dt);



                if (Dt.Rows.Count == 0)
                {

                }
                else
                {



                    TxtRFCOp.Text = Dt.Rows[0]["RFCOperador"].ToString();
                    txtNumLic.Text = Dt.Rows[0]["NumLicencia"].ToString();

                    RFCOPERADOR = Dt.Rows[0]["RFCOperador"].ToString();
                    NUMEROLICENCIA = Dt.Rows[0]["NumLicencia"].ToString();
                    PAISOPERADOR = Dt.Rows[0]["Pais"].ToString();
                    CALLEOPERADOR = Dt.Rows[0]["Calle"].ToString();
                    NUMEROPERADOR = Dt.Rows[0]["NumeroExterior"].ToString();
                    MUNICIPIOOPERADOR = Dt.Rows[0]["Municipio"].ToString();
                    LOCALIDADOPERADOR = Dt.Rows[0]["Localidad"].ToString();
                    COLONIAOPERADOR = Dt.Rows[0]["Colonia"].ToString();
                    ESTADOPERADOR = Dt.Rows[0]["Estado"].ToString();
                    CPOPERADOR = Dt.Rows[0]["CodigoPostal"].ToString();



                }


            }

        }

       



        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime t = Convert.ToDateTime(TxtFechallegada.Text);
                if (t >= DateTime.Now.Date)
                {

                    PanelMensajes.Controls.Clear();
                    PanelMensajes.CssClass = default;


          



            if (GridView2.Rows.Count==0 || GVClientes.Rows.Count==0)
            {
                limpiar();


            }
            else
            {

   
           
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@Placas", SqlDbType.VarChar).Value = DDLAuto.SelectedValue.ToString();
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 12;
                Da.Fill(Dt);

                if (Dt.Rows.Count == 0)
                {

                }
                else
                {
                    ANOAUT = Dt.Rows[0]["MODELO"].ToString();
                }
            }

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 1;
                Da.Fill(Dt);

                if (Dt.Rows.Count == 0)
                {

                }
                else
                {
                    FOLIO = Dt.Rows[0][0].ToString();
                }
              }

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                string empresa;
                empresa = DDLEmpresa.SelectedValue.ToString();
                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                 DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //Da.SelectCommand.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = TxtNombre.Text;
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 17;
                //en lugar de rfcempresa sera id de empresa 
                Da.SelectCommand.Parameters.Add("@IDEmpresa", SqlDbType.VarChar).Value = empresa;
                Da.Fill(Dt);



                Empresa = Dt.Rows[0]["ID_Vales"].ToString();

                CalleEmi = Dt.Rows[0]["CALLE"].ToString();
                ColEmi = Dt.Rows[0]["COLONIA"].ToString();
                MuniEmi = Dt.Rows[0]["MUNICIPIO"].ToString();
                EstEmi = Dt.Rows[0]["ESTADO"].ToString();
                PaisEmi = Dt.Rows[0]["PAIS"].ToString();
                CPEmi = Dt.Rows[0]["CP"].ToString();
                SERIE = Dt.Rows[0]["SERIE"].ToString();
                REGIMEN = Dt.Rows[0]["REGIMENFISCAL"].ToString();

                SAT_COL = Dt.Rows[0]["SAT_COL"].ToString();
                SAT_EST = Dt.Rows[0]["SAT_EST"].ToString();
                SAT_PAIS = Dt.Rows[0]["SAT_PAIS"].ToString();
                SAT_MUN = Dt.Rows[0]["SAT_MUN"].ToString();
                SAT_LOCAL = Dt.Rows[0]["SAT_LOCAL"].ToString();



                conn.Close();
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {

                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //Da.SelectCommand.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = TxtNombre.Text;
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 19;
                Da.SelectCommand.Parameters.Add("@Placas", SqlDbType.VarChar).Value = DDLAuto.SelectedValue.ToString();
                Da.Fill(Dt);


                POLIZA = Dt.Rows[0]["NUMERO_POLIZA"].ToString();
                NOMBREASEGU= Dt.Rows[0]["NOMBRE_ASEGURADORA"].ToString();
                PERMISOSCT = Dt.Rows[0]["NUMERO_PERMISOSCT"].ToString();
                      if (PERMISOSCT=="")
                       {
                        PERMISOSCT = "000000";
                       }
                conn.Close();
            }


            //string path2 = @"C:\Users\desarrollador.oracle\Desktop\CARTAPORTE\";
              string path2 = @"E:\inetpub\wwwroot\CARTAPORTE\FACTURAS\MASFAC_ESC\DATOS";
            //  string texto = "Ha llegado hasta linea Xdsadadasd";
            string fechan, fechalllegada, cvetipotoc, tipodoc,fecha3;

                        DateTime Fecha2;            
            cvetipotoc = DDLTipoDoc.SelectedValue.ToString();
             if (cvetipotoc == "T")
            {
                tipodoc = "7";
            }
            else
            {
                tipodoc = "1";
            }
            



            fechan = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

           // fechalllegada = Convert.ToDateTime(TxtFechallegada.Text).ToString("yyyy-MM-ddTHH:mm:ss");

                        Fecha2 = Convert.ToDateTime(TxtFechallegada.Text).AddHours(22);
                        fechalllegada = Fecha2.ToString("yyyy-MM-ddTHH:mm:ss");

            string[] contenido = { "E" };

            List<string> lista = new List<string>(contenido.ToList());

     
                //Traslado
                lista.Add(vers);
                lista.Add("SELLO");
                lista.Add("FECEXP   " + fechan);
                lista.Add("SERFOL " + SERIE);
                lista.Add("NUMFOL " + FOLIO);
                lista.Add("CVETIPDOC " + cvetipotoc);
                lista.Add("TIPDOC " + tipodoc);
                lista.Add("TIPMON XXX");
                lista.Add("TIPCAM 1");
                lista.Add("USOCFDI P01");
                        //EMISOR
                input = DDLEmpresa.SelectedItem.Text;
                input = input.Substring(0, input.IndexOf("CV")+2);
                lista.Add("RFCEMI " + RFCEmpresaGlob);
                lista.Add("NOMEMI " + input);
                lista.Add("CVEREGIMEN " + REGIMEN);
                lista.Add("CALEMI " + CalleEmi);
                lista.Add("COLEMI " + ColEmi);
                lista.Add("MUNEMI " + MuniEmi);
                lista.Add("ESTEMI " + EstEmi);
                lista.Add("PAIEMI " + PaisEmi);
                lista.Add("CODEMI " + CPEmi);
                lista.Add("TIPCOM T");
                lista.Add("");
                //RECEPTOR

                lista.Add("RFCREC " + RFCEmpresaGlob);
                lista.Add("NOMREC " + input);
                lista.Add("CALREC " + CalleEmi);
                // lista.Add("NEXREC " + NEXREC);
                //lista.Add("NINREC " + NINREC);
                lista.Add("COLREC " + ColEmi);
                lista.Add("MUNREC " + MuniEmi);
                lista.Add("ESTREC " + EstEmi);
                lista.Add("PAIREC " + PaisEmi);
                lista.Add("CODREC " + CPEmi);

                lista.Add("CODLUGEXP " + CPEmi);

                //AQUI VA LA PARTE DE LOS ARTICULOS

                lista.Add("D");
                int contador = 1;
                foreach (GridViewRow row in GridView2.Rows)
                {

                    //string itenm = row.Cells[1].Text;
                    lista.Add("CANTID " + row.Cells[1].Text);
                    lista.Add("NUMLIN " + contador);
                    lista.Add("CVEPRODSERV " + row.Cells[4].Text);
                    lista.Add("CVEUNIDAD " + row.Cells[2].Text);
                    lista.Add("DESCRI " + row.Cells[0].Text);
                    lista.Add("UNIDAD " + row.Cells[2].Text);
                    //lista.Add("CVEUNIDAD ACT");
                    lista.Add("VALUNI 0.00");
                    lista.Add("IMPORT 0.00");
                    lista.Add("PBRUDE 0.00");
                    lista.Add("IMPBRU 0.00");
                    lista.Add("TDECON 0.00");
                    lista.Add("MDECON 0.00");
                    lista.Add("TDECON0 0.00");
                    lista.Add("MDECON0 0.00");
                    lista.Add("TDECON1 0.00");
                    lista.Add("MDECON1 0.00");
                    lista.Add("TASIPE 0.00");
                    lista.Add("TASIEP 0.00");
                    lista.Add("MONIPE 0.00");
                    lista.Add("MONIEP 0.00");
                    lista.Add("");

                    contador++;
                }




                //

                //IMPUESTOS
                lista.Add("R");
                lista.Add("SUBTBR 0.00");
                lista.Add("MONDET 0.00");
                lista.Add("PRCDSG 0.00");
                lista.Add("SUBTOT 0.00");
                lista.Add("SUBTAI 0.00");
                lista.Add("IVATRA 0.00");
                lista.Add("TOTIVA 0");
                lista.Add("TOTPAG 0.00");
                lista.Add("IVATRA1 0.00");
                lista.Add("TOTIVA1 0.00");
                lista.Add("TOTTRA 0.00");

                //COMIENZA EL COMPLEMENTO CARTA PORTE 
                lista.Add("");
                lista.Add("COM_CPT_INICPT");
                lista.Add("COM_CPT_VERSIO "+ version);//Version del complemento carta porte
                lista.Add("COM_CPT_TRANINT No"); //indica si los articulos salen o no de pais 
                lista.Add("COM_CPT_ENTSAL");
                lista.Add("COM_CPT_VIAENTSAL");
                lista.Add("COM_CPT_TOTDIST " + TxtDisRec.Text);  //El campo solamente debe existir si existe la sección AutotransporteFederal
                lista.Add("COM_CPT_FT_CVETRANS 01");//Clave Transporte cuando sea transporte federal en caso de las demas empresas sera el valor 01 "COM_CPT_FT_CVETRANS 01"

                //INICIO PARA LA INFORMACION DE UBICACIONES
                lista.Add("");
                //UBICACION ORIGEN
                lista.Add("COM_CPT_INIUBI");
                lista.Add("COM_CPT_UBI_TIPUBI Origen");
                lista.Add("COM_CPT_UBI_IDUBI OR000001");
                //lista.Add("COM_CPT_TIPEST  02"); //tipo estaciones
                lista.Add("COM_CPT_DISTREC " + TxtDisRec.Text); //Distancia recorrida total
                lista.Add("COM_CPT_UBI_RFC " + RFCEmpresaGlob);//RFC del emitente
                lista.Add("COM_CPT_ORI_NOMREM " + DDLEmpresa.SelectedItem.Text);//nombre de emitente 
                lista.Add("COM_CPT_UBI_FECHA   " + fechan); // fecha y hora de salid de la mercancia

                lista.Add("COM_CPT_DOM_CAL " +CalleEmi );
                lista.Add("COM_CPT_DOM_PAI MEX");
               // lista.Add("COM_CPT_DOM_REF EDIFICIO EMPRESA GRUPOKOWI");


                lista.Add("COM_CPT_DOM_COL " + SAT_COL);
                lista.Add("COM_CPT_DOM_LOC ");//+ SAT_LOCAL);
                lista.Add("COM_CPT_DOM_MUN " + SAT_MUN);
                lista.Add("COM_CPT_DOM_EST " + SAT_EST);
                lista.Add("COM_CPT_DOM_PAI MEX");
                lista.Add("COM_CPT_DOM_CP " + CPEmi);

                lista.Add("COM_CPT_FINUBI"); //FIN PARA LAS INFORMACION DE UBICACIONES

                lista.Add("");
                //UBICACION DESTINO
                int j =0; 
                for (int i = 1; i <= GVClientes.Rows.Count; i++)
                 {

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                    {

                        // List<string> lista = new List<string>();


                        SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                        DataTable Dt = new DataTable();

                        GridViewRow row = GVClientes.Rows[j];

                        Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                        Da.SelectCommand.Parameters.Add("@ID_CLIENTE", SqlDbType.VarChar).Value = row.Cells[1].Text.ToString(); 
                        Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 6;
                        Da.Fill(Dt);




                                decimal distancia = Convert.ToDecimal(TxtDisRec.Text);
                                decimal clientes = Convert.ToDecimal(GVClientes.Rows.Count);
                                decimal distanciaxclient = distancia / clientes;
                                if (Dt.Rows.Count > 0)
                        {
                            lista.Add("COM_CPT_INIUBI");
                            lista.Add("COM_CPT_UBI_TIPUBI Destino");
                            lista.Add("COM_CPT_UBI_IDUBI DE00000" + i);
                            lista.Add("COM_CPT_UBI_RFC " + Dt.Rows[0]["RFC"].ToString());
                            // lista.Add("COM_CPT_DES_NOMDES " + NOMREC);
                            lista.Add("COM_CPT_UBI_FECHA " + fechalllegada);//Fecha estimada de llegada de la mercancia 
                            lista.Add("COM_CPT_UBI_DISTREC " + distanciaxclient);
                            lista.Add("COM_CPT_DOM_CAL " + Dt.Rows[0]["CALLE"].ToString());
                            lista.Add("COM_CPT_DOM_PAI MEX");
                           // lista.Add("COM_CPT_DOM_REF "+ input);
                            lista.Add("COM_CPT_DOM_NUMEXT " + Dt.Rows[0]["numero_ext"].ToString());// 1350
                            lista.Add("COM_CPT_DOM_COL " + row.Cells[5].Text.ToString());//
                            lista.Add("COM_CPT_DOM_LOC ");// + row.Cells[3].Text.ToString()
                            lista.Add("COM_CPT_DOM_MUN " + row.Cells[4].Text.ToString());
                            lista.Add("COM_CPT_DOM_EST " + row.Cells[2].Text.ToString());
                            lista.Add("COM_CPT_DOM_CP " + Dt.Rows[0]["CODIGO_POSTAL"].ToString());

                            lista.Add("COM_CPT_FINUBI");
                            lista.Add("");
                        }
                                else
                                {
                                    Response.Write("<script>alert('No Existe el cliente : " + row.Cells[1].Text.ToString() + "');</script>");
                                }

                        conn.Close();
                    }
                    j++;
                }

            


                //INICIO DE SECCION DE MERCANCIAS

                lista.Add("COM_CPT_INIMERS ");
              
                int x = 0;
                
                int y = 0;
                MERCANCIAS = 0;
                PESOBRUTO = 0;
                foreach (GridViewRow row in GridView2.Rows)
                {

                    MERCANCIAS++;

                    y = Int32.Parse(row.Cells[3].Text);
                PESOBRUTO = PESOBRUTO + y;

                }

                lista.Add("COM_CPT_MER_PESBRU "+ PESOBRUTO);//OBLIGATORIO DESDE LA VERSION 2.0
                lista.Add("COM_CPT_MER_UNIPES KGM");//OBLIGATORIO DESDE LA VERSION 2.0
               // lista.Add("COM_CPT_MER_PESONET");
                lista.Add("COM_CPT_MER_NUMTOT "+ MERCANCIAS); //OBLIGATORIO DESDE LA VERSION 2.0
                                                     // lista.Add("COM_CPT_MER_CARGO ");
              
               
               

                lista.Add("COM_CPT_INIMER");

                foreach (GridViewRow row in GridView2.Rows)
                {
                            lista.Add("COM_CPT_INIMER");
                            lista.Add("COM_CPT_MER_BIENTRA " + row.Cells[4].Text);
                            lista.Add("COM_CPT_MER_DESCRI " + row.Cells[0].Text);
                            lista.Add("COM_CPT_MER_CANTID " + row.Cells[1].Text);
                            lista.Add("COM_CPT_MER_CVUNID " + row.Cells[2].Text);
                            lista.Add("COM_CPT_MER_PKG " + row.Cells[3].Text);
                            lista.Add("COM_CPT_FINMER ");
                            lista.Add("");

                            lista.Add("COM_CPT_INICANTRAN");
                            lista.Add("COM_CPT_CMER_CANTID " + row.Cells[1].Text);
                            //se tendra que definir bien los destinos 
                            lista.Add("COM_CPT_CMER_IDORI OR000001");
                            lista.Add("COM_CPT_CMER_IDDES DE000001");
                            lista.Add("COM_CPT_CMER_CVETRAN");
                            lista.Add("COM_CPT_FINCANTRAN");
                        }

                //lista.Add("COM_CPT_INICANTRAN");


                // lista.Add("COM_CPT_FINCANTRAN");

                
               
                lista.Add("");
                lista.Add("COM_CPT_FINMERS ");
                lista.Add("");

                

                lista.Add("COM_CPT_INIAUTO");
                lista.Add("COM_CPT_AUT_SCT " + DDLTipoPermiso.SelectedValue.ToString());
                lista.Add("COM_CPT_AUT_PSCT " + PERMISOSCT); //numero de permiso
                        


                lista.Add("COM_CPT_AUT_ASEGRESP " + NOMBREASEGU);//nombre de aseguradora
                lista.Add("COM_CPT_AUT_POLIRESP " + POLIZA);  //numero de pliza
                lista.Add("COM_CPT_AUT_CONVEH " + DDLConfVehi.SelectedValue.ToString());
                lista.Add("COM_CPT_AUT_PLACAV " + DDLAuto.SelectedValue.ToString());
                lista.Add("COM_CPT_AUT_ANIOV " + ANOAUT);

                        string varrem1 = DDLRemolqueSN.SelectedValue.ToString();

                        string remolq2 = ddlremolque2.SelectedValue.ToString();
                 if (varrem1 == "1")
                  {
                   //EN CASO DE LLEVAR REMOLQUES ESTA PARTE DE ABAJO SE TENDRIA QUE LLENAR 
                   lista.Add("COM_CPT_AUT_SUTIPREM1 " + ddlremolque1.SelectedValue.ToString());
                   lista.Add("COM_CPT_AUT_PLACA1 " + TXTPLACA1.Text);
                            if (remolq2=="0")
                            {

                            }
                            else
                            {
                                lista.Add("COM_CPT_AUT_SUTIPREM2 " + ddlremolque2.SelectedValue.ToString());
                                lista.Add("COM_CPT_AUT_PLACA2 " + txtplac2.Text);
                            }
                

                 }


                        lista.Add("COM_CPT_FINAUTO");
                lista.Add("");

                

                lista.Add("COM_CPT_INIFIGTRA");




                lista.Add("COM_CPT_FIG_TIPFIG 01");
                lista.Add("COM_CPT_FIG_RFCFIG " + TxtRFCOp.Text);
                lista.Add("COM_CPT_FIG_NUMLIC " + txtNumLic.Text);
                lista.Add("COM_CPT_FIG_NOMFIG " + DDLOperador.SelectedItem.Text);
                lista.Add("");
                lista.Add("COM_CPT_INIPARTRAN");
                lista.Add("COM_CPT_FIG_PARTRANS 02");
                lista.Add("COM_CPT_DOM_CAL " + CALLEOPERADOR);
                lista.Add("COM_CPT_DOM_NUMEXT  " + NUMEROPERADOR);
                lista.Add("COM_CPT_DOM_COL  " + COLONIAOPERADOR);
                lista.Add("COM_CPT_DOM_LOC ");// + LOCALIDADOPERADOR);
                lista.Add("COM_CPT_DOM_REF CASA PARTICULAR");
                lista.Add("COM_CPT_DOM_MUN " + MUNICIPIOOPERADOR);
                lista.Add("COM_CPT_DOM_EST " + ESTADOPERADOR);
                lista.Add("COM_CPT_DOM_PAI " + PAISOPERADOR);
                lista.Add("COM_CPT_DOM_CP " + CPOPERADOR);
                lista.Add("");
                lista.Add("COM_CPT_FINPARTRAN");
                lista.Add("");
                 lista.Add("COM_CPT_FINFIGTRA");
                lista.Add("");
                lista.Add("COM_CPT_FINCPT");



                contenido = lista.ToArray();




                // INSERTA ENCABEZADO A LA TABLA MASTER
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {

                    SqlDataAdapter Da = new SqlDataAdapter("CPSP02", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.Clear();

                    Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 1;
                    Da.SelectCommand.Parameters.Add("@FOLIO", SqlDbType.VarChar).Value = FOLIO;
                    Da.SelectCommand.Parameters.Add("@VERSION", SqlDbType.VarChar).Value = version;
                    Da.SelectCommand.Parameters.Add("@SERIE", SqlDbType.VarChar).Value = SERIE;
                    Da.SelectCommand.Parameters.Add("@RFCEMPRE", SqlDbType.VarChar).Value = RFCEmpresaGlob;
                    Da.SelectCommand.Parameters.Add("@NOMBRE_EMPRE", SqlDbType.VarChar).Value = DDLEmpresa.SelectedItem.Text;
                    Da.SelectCommand.Parameters.Add("@TIPODOC", SqlDbType.VarChar).Value = DDLTipoDoc.SelectedValue.ToString();
                    Da.SelectCommand.Parameters.Add("@NOMBRE_CHOF", SqlDbType.VarChar).Value = DDLOperador.SelectedItem.Text;
                    Da.SelectCommand.Parameters.Add("@LIC_CHOF", SqlDbType.VarChar).Value = txtNumLic.Text;
                    Da.SelectCommand.Parameters.Add("@CALLE_CHOF", SqlDbType.VarChar).Value = CALLEOPERADOR;
                    Da.SelectCommand.Parameters.Add("@RFC_CHOFER", SqlDbType.VarChar).Value = TxtRFCOp.Text;
                    Da.SelectCommand.Parameters.Add("@PAIS_CHOFER", SqlDbType.VarChar).Value = PAISOPERADOR;
                    Da.SelectCommand.Parameters.Add("@COL_CHOF", SqlDbType.VarChar).Value = COLONIAOPERADOR;
                    Da.SelectCommand.Parameters.Add("@NUM_C_CHOFER", SqlDbType.VarChar).Value = NUMEROPERADOR;
                    Da.SelectCommand.Parameters.Add("@MUNICIP_CHOF", SqlDbType.VarChar).Value = MUNICIPIOOPERADOR;
                    Da.SelectCommand.Parameters.Add("@LOCAL_CHOF", SqlDbType.VarChar).Value = "";// LOCALIDADOPERADOR;
                    Da.SelectCommand.Parameters.Add("@ESTAD_CHOF", SqlDbType.VarChar).Value = ESTADOPERADOR;
                    Da.SelectCommand.Parameters.Add("@CP_CHOF", SqlDbType.VarChar).Value = CPOPERADOR;
                    Da.SelectCommand.Parameters.Add("@FECHA_LLEGADA", SqlDbType.VarChar).Value = fechalllegada;
                    Da.SelectCommand.Parameters.Add("@PLACAS_VEHIC", SqlDbType.VarChar).Value = DDLAuto.SelectedValue.ToString();
                    Da.SelectCommand.Parameters.Add("@TIP_VEHIC", SqlDbType.VarChar).Value = DDLConfVehi.SelectedValue.ToString();
                    Da.SelectCommand.Parameters.Add("@REMOLQ", SqlDbType.VarChar).Value = DDLRemolqueSN.SelectedValue.ToString();
                    Da.SelectCommand.Parameters.Add("@DISTANC_REC", SqlDbType.VarChar).Value = TxtDisRec.Text;

                    Da.SelectCommand.Parameters.Add("@TIPOFIGURA", SqlDbType.VarChar).Value = "01";
                    
                    Da.SelectCommand.Parameters.Add("@PARTETRANSP", SqlDbType.VarChar).Value = "02";
                    Da.SelectCommand.Parameters.Add("@PERMSCT", SqlDbType.VarChar).Value = DDLTipoPermiso.SelectedValue.ToString();
                    Da.SelectCommand.Parameters.Add("@MODELOV", SqlDbType.VarChar).Value = ANOAUT;
                    Da.SelectCommand.Parameters.Add("@ASEGURADORA", SqlDbType.VarChar).Value = NOMBREASEGU;
                    Da.SelectCommand.Parameters.Add("@POLIZA", SqlDbType.VarChar).Value = POLIZA;

                    Da.SelectCommand.Parameters.Add("@PESOBRUTO", SqlDbType.VarChar).Value = PESOBRUTO;
                    Da.SelectCommand.Parameters.Add("@NUMTOTMERC", SqlDbType.VarChar).Value = MERCANCIAS;
                    Da.SelectCommand.Parameters.Add("@PESOTOTKILOS", SqlDbType.VarChar).Value = PESOBRUTO;

                    Da.SelectCommand.Parameters.Add("@NUM_PERMISO", SqlDbType.VarChar).Value = PERMISOSCT;
                    
                    string varrem2 = DDLRemolqueSN.SelectedValue.ToString();
                                string varremolq2 = ddlremolque2.SelectedValue.ToString();

                                if (varrem2 == "1")
                                {
                                    Da.SelectCommand.Parameters.Add("@TIPOREM1", SqlDbType.VarChar).Value = ddlremolque1.SelectedValue.ToString();
                                    Da.SelectCommand.Parameters.Add("@PLACAREM1", SqlDbType.VarChar).Value = TXTPLACA1.Text;
                                // DESCRIPCION DEL REMOLQUE NUMERO 1
                                Da.SelectCommand.Parameters.Add("@DESCRIPREMOLQ1", SqlDbType.VarChar).Value = ddlremolque1.SelectedItem.Text;

                                if (varremolq2=="0")
                                    {
                                        Da.SelectCommand.Parameters.Add("@TIPOREM2", SqlDbType.VarChar).Value = "";
                                        Da.SelectCommand.Parameters.Add("@PLACAREM2", SqlDbType.VarChar).Value = "";
                                        Da.SelectCommand.Parameters.Add("@DESCRIPREMOLQ2", SqlDbType.VarChar).Value = "";


                                }
                                    else
                                    {
                                    
                                    Da.SelectCommand.Parameters.Add("@TIPOREM2", SqlDbType.VarChar).Value = ddlremolque2.SelectedValue.ToString();
                                    Da.SelectCommand.Parameters.Add("@PLACAREM2", SqlDbType.VarChar).Value = txtplac2.Text;
                                    //DESCRIPCION DE REMOLQUE NUMERO 2
                                     Da.SelectCommand.Parameters.Add("@DESCRIPREMOLQ2", SqlDbType.VarChar).Value = ddlremolque2.SelectedItem.Text;
                                }

                            }
                                else
                                {
                                    Da.SelectCommand.Parameters.Add("@TIPOREM1", SqlDbType.VarChar).Value = "";
                                    Da.SelectCommand.Parameters.Add("@TIPOREM2", SqlDbType.VarChar).Value = "";
                                    Da.SelectCommand.Parameters.Add("@PLACAREM1", SqlDbType.VarChar).Value = "";
                                    Da.SelectCommand.Parameters.Add("@PLACAREM2", SqlDbType.VarChar).Value = "";
                                ///EN CASO DE ESTAR VACIO 
                                Da.SelectCommand.Parameters.Add("@DESCRIPREMOLQ1", SqlDbType.VarChar).Value = "";
                                Da.SelectCommand.Parameters.Add("@DESCRIPREMOLQ2", SqlDbType.VarChar).Value = "";
                            }
                                
                                //DESCRIPCION SOLICITADA POR EL AREA DE FISCAL
                            Da.SelectCommand.Parameters.Add("@DESCRIPPERMISO", SqlDbType.VarChar).Value = DDLTipoPermiso.SelectedItem.Text ;
                            Da.SelectCommand.Parameters.Add("@DESCRPVEHICULO", SqlDbType.VarChar).Value = DDLConfVehi.SelectedItem.Text ;
                            Da.SelectCommand.Parameters.Add("@DESCRIPOPERA", SqlDbType.VarChar).Value = "OPERADOR";

                            Da.Fill(Dt);

                    conn.Close();
                }

                //INSERTA LINEAS A LA TABLA DETALLE
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {

                    SqlDataAdapter Da = new SqlDataAdapter("CPSP03", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;



                    foreach (GridViewRow row in GridView2.Rows)
                    {
                        Da.SelectCommand.Parameters.Clear();
                        Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 1;
                        Da.SelectCommand.Parameters.Add("@FOLIO", SqlDbType.VarChar).Value = FOLIO;
                        Da.SelectCommand.Parameters.Add("@DESCRIP_PROD", SqlDbType.VarChar).Value = row.Cells[0].Text;
                        Da.SelectCommand.Parameters.Add("@UNID_PROD", SqlDbType.VarChar).Value = row.Cells[2].Text;
                        Da.SelectCommand.Parameters.Add("@CLAVE_PROD", SqlDbType.VarChar).Value = row.Cells[4].Text;
                        Da.SelectCommand.Parameters.Add("@CANTID_PROD", SqlDbType.VarChar).Value = row.Cells[1].Text;
                        Da.SelectCommand.Parameters.Add("@PRECUNI_PROD", SqlDbType.VarChar).Value = row.Cells[3].Text;
                        Da.SelectCommand.Parameters.Add("@PESO_N_KILOS", SqlDbType.VarChar).Value = row.Cells[3].Text;

                         Da.Fill(Dt);

                    }

                    conn.Close();
                }
                //INSERTA LINEAS EN LA TABLA DE CLIENTES 
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {

                    SqlDataAdapter Da = new SqlDataAdapter("CPSP04", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;



                    foreach (GridViewRow row in GVClientes.Rows)
                    {
                        Da.SelectCommand.Parameters.Clear();
                        Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 1;
                        Da.SelectCommand.Parameters.Add("@FOLIO", SqlDbType.VarChar).Value = FOLIO;
                        Da.SelectCommand.Parameters.Add("@Tipo_Ubi", SqlDbType.VarChar).Value = "Destino";
                        Da.SelectCommand.Parameters.Add("@NOMBRE_CLIEN", SqlDbType.VarChar).Value = row.Cells[1].Text;
                        Da.SelectCommand.Parameters.Add("@ESTADO_CLIEN", SqlDbType.VarChar).Value = row.Cells[2].Text;
                        Da.SelectCommand.Parameters.Add("@LOCALI_CLIEN", SqlDbType.VarChar).Value ="";
                        Da.SelectCommand.Parameters.Add("@MUNICIP_CLIEN", SqlDbType.VarChar).Value = row.Cells[4].Text;
                        Da.SelectCommand.Parameters.Add("@COL_CLIEN", SqlDbType.VarChar).Value = row.Cells[5].Text;
                        Da.SelectCommand.Parameters.Add("@CODIGO_POSTAL", SqlDbType.VarChar).Value = row.Cells[6].Text;
                        Da.SelectCommand.Parameters.Add("@PAIS_CLIEN", SqlDbType.VarChar).Value = "MEX";
                        Da.SelectCommand.Parameters.Add("@CALLE", SqlDbType.VarChar).Value = row.Cells[7].Text;


                                Da.Fill(Dt);
                    }

                    conn.Close();
                }
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                            {

                                SqlDataAdapter Da = new SqlDataAdapter("CPSP04", conn);
                                DataTable Dt = new DataTable();
                                Da.SelectCommand.CommandType = CommandType.StoredProcedure;


                                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 1;
                                Da.SelectCommand.Parameters.Add("@FOLIO", SqlDbType.VarChar).Value = FOLIO;
                                Da.SelectCommand.Parameters.Add("@Tipo_Ubi", SqlDbType.VarChar).Value = "Origen";
                                Da.SelectCommand.Parameters.Add("@NOMBRE_CLIEN", SqlDbType.VarChar).Value = SERIE;
                                Da.SelectCommand.Parameters.Add("@ESTADO_CLIEN", SqlDbType.VarChar).Value = SAT_EST;
                                Da.SelectCommand.Parameters.Add("@LOCALI_CLIEN", SqlDbType.VarChar).Value = "";
                                Da.SelectCommand.Parameters.Add("@MUNICIP_CLIEN", SqlDbType.VarChar).Value = SAT_MUN;
                                Da.SelectCommand.Parameters.Add("@COL_CLIEN", SqlDbType.VarChar).Value = SAT_COL;
                                Da.SelectCommand.Parameters.Add("@CODIGO_POSTAL", SqlDbType.VarChar).Value = CPEMPRESA;
                                Da.SelectCommand.Parameters.Add("@PAIS_CLIEN", SqlDbType.VarChar).Value = "MEX";
                                Da.SelectCommand.Parameters.Add("@CALLE", SqlDbType.VarChar).Value = CalleEmi;

                                Da.Fill(Dt);
                             

                                conn.Close();
                            }



                        if (tipodoc == "1")
                        {
                            //inserta en apex
                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {

                    SqlDataAdapter Da = new SqlDataAdapter("INSERT_CP_APEX", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    Da.SelectCommand.Parameters.Add("@FOLIO", SqlDbType.VarChar).Value = FOLIO;


                      Da.Fill(Dt);
                      conn.Close();

                                Response.Write("<script>alert('Se Genero Correctamente con el folio: " + FOLIO + "');</script>");
                            }

                            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                            {

                                SqlDataAdapter Da = new SqlDataAdapter("INSERT_CP_APEX_TEST", conn);
                                DataTable Dt = new DataTable();
                                Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                                Da.SelectCommand.Parameters.Add("@FOLIO", SqlDbType.VarChar).Value = FOLIO;


                                Da.Fill(Dt);
                                conn.Close();

                                
                            }

                        }   
            else
            {
                //CREA EL DOCUMENTO
                using (StreamWriter outputFile = new StreamWriter( Path.Combine(path2, SERIE + FOLIO + ".txt"),false,Encoding.GetEncoding(1252)))
                
                            {

                    foreach (string line in contenido)
                        outputFile.WriteLine(line);
                             

                }

                            Response.Write("<script>alert('Se Genero Correctamente con el folio: "+FOLIO+"');</script>");
                            limpiar();
                          


                        }

                limpiar();

                    }
                }
                else
                {
                 
                    limpiar();
                    Response.Write("<script>alert('La fecha de llegada tiene  que ser mayor a la fecha actual');</script>");


                }
            }
            catch (Exception ex)
            {


                Carta.MensajeAlerta(this.PanelMensajes, this.EtiquetaMensajes, "Error: " + ex.Message, Carta.TipoAlerta.Error);

            }





        }

        protected void limpiar() {
            txtNumLic.Text = "";
            TxtRFCOp.Text = "";
            TxtDisRec.Text = "";
           // txtCodigo.Text = "";
            TxtDescripcionA.Text = "";
            TxtClaveSatA.Text = "";
            txtCantidadA.Text = "";
            txtPESO.Text = "";

            PanelMensajes.Controls.Clear();

            GridView2.DataSource = null;
            GridView2.DataBind();

            GVClientes.DataSource = null;
            GVClientes.DataBind();

        }

        DataTable dt1 = new DataTable();
        DataRow row;
        protected void btnAgregar_Click(object sender, EventArgs e)
        {


            if (GridView2.Rows.Count > 0)
            {
               

                DataTable dt = (DataTable)ViewState["datatable"];

                DataRow row = dt.NewRow();

               // row["Codigo"] = txtCodigo.Text;
                row["Descripcion"] = TxtDescripcionA.Text;
                row["Cantidad"] = txtCantidadA.Text;
                row["Unidad"] = DDLUnidadA.SelectedValue.ToString();
                row["Peso"] = txtPESO.Text;
                row["Calve SAT"] = TxtClaveSatA.Text;
                dt.Rows.Add(row);

                GridView2.DataSource = dt;

                GridView2.DataBind();
               
            
            }
            else
            {
                dt1 = new DataTable();

               // dt1.Columns.Add("Codigo");
                dt1.Columns.Add("Descripcion");
                dt1.Columns.Add("Cantidad");
                dt1.Columns.Add("Unidad");
                dt1.Columns.Add("Peso");
                dt1.Columns.Add("Calve SAT");

                row = dt1.NewRow();

              //  row["Codigo"] = txtCodigo.Text;
                row["Descripcion"] = TxtDescripcionA.Text;
                row["Cantidad"] = txtCantidadA.Text;
                row["Unidad"] = DDLUnidadA.SelectedValue.ToString();
                row["Peso"] = txtPESO.Text;
                row["Calve SAT"] = TxtClaveSatA.Text;
                dt1.Rows.Add(row);

                ViewState["datatable"] = dt1;
                GridView2.DataSource = dt1;
                GridView2.DataBind();

            }


        }
    

        protected void DDLEmpresa_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string id_empresa = DDLEmpresa.SelectedValue.ToString();
            string nombreempre = DDLEmpresa.SelectedItem.ToString();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                List<string> lista = new List<string>();


                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@IDEmpresa", SqlDbType.VarChar).Value = id_empresa;
               // Da.SelectCommand.Parameters.Add("@NombreEmpresa", SqlDbType.VarChar).Value = nombreempre; 
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 24;

                Da.Fill(Dt);

                RFCEmpresaGlob = Dt.Rows[0]["RFC"].ToString();
                idempresa= Dt.Rows[0]["ID_Vales"].ToString();
                CPEMPRESA = Dt.Rows[0]["CP"].ToString();
                BU = Dt.Rows[0]["IDENTI_ORAC"].ToString();

                DDLOperador.Items.Clear();
                DDLAuto.Items.Clear();

                Cargar_Vechiculos(idempresa);

                cargar_choferes(id_empresa, idempresa);


            }


        }


        protected void BtnDelet_Click(object sender, EventArgs e)
        {
            GridView2.DataSource = null;
            GridView2.DataBind();

        }

        protected void BindData()
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@RFCCliente", SqlDbType.VarChar).Value = TxtClienteModal.Text;
                Da.SelectCommand.Parameters.Add("@BU_EMPRESSA", SqlDbType.VarChar).Value = BU;
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 22;
                Da.Fill(Dt);


                GridModal.DataSource = Dt;
                GridModal.DataBind();



                conn.Close();
            }
        }
        protected void BuscarModal_Click(object sender, EventArgs e)
        {
            if (TxtClienteModal.Text=="")
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {
                    SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.Add("@NombreCliente", SqlDbType.VarChar).Value = TxtNombClienmodal.Text;
                    Da.SelectCommand.Parameters.Add("@BU_EMPRESSA", SqlDbType.VarChar).Value = BU;
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

        protected void GridModal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            this.DataBind();
        }

        protected void Cerrar_Modal_Click(object sender, EventArgs e)
        {

            GridModal.DataSource = "";
            GridModal.DataBind();

            TxtClienteModal.Text = "";
            TxtNombClienmodal.Text = "";
            lblMessage.Text = "";

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

            GridModal.DataSource = "";
            GridModal.DataBind();

            TxtClienteModal.Text = "";
            TxtNombClienmodal.Text = "";
            lblMessage.Text = "";


        }

        protected void Add_Client_Click(object sender, EventArgs e)
        {
            string calle, estado, localidad, municipio, colonia, codigopostal;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@ID_CLIENTE", SqlDbType.VarChar).Value = txtCliente.Text;
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 6;
                Da.Fill(Dt);

                calle = Dt.Rows[0]["calle"].ToString();
                estado = Dt.Rows[0]["estado"].ToString();
                localidad = Dt.Rows[0]["localidad"].ToString();
                municipio = Dt.Rows[0]["municipio"].ToString();
                colonia = Dt.Rows[0]["col"].ToString();
                codigopostal = Dt.Rows[0]["codigo_postal"].ToString();



                conn.Close();
            }


                if (GVClientes.Rows.Count > 0)
            {


                DataTable dt = (DataTable)ViewState["datatable2"];

                DataRow row = dt.NewRow();

                row["ID_Cliente"] = txtCliente.Text;
                row["Estado"] = estado;
                row["Localidad"] = localidad;
                row["Municipio"] = municipio;
                row["Colonia"] = colonia;
                row["Codigo_Postal"] = codigopostal;
                row["Calle"] = calle;
                dt.Rows.Add(row);

                GVClientes.DataSource = dt;

                GVClientes.DataBind();


            }
            else
            {
                dt1 = new DataTable();

                dt1.Columns.Add("ID_Cliente");
                dt1.Columns.Add("Estado");
                dt1.Columns.Add("Localidad");
                dt1.Columns.Add("Municipio");
                dt1.Columns.Add("Colonia");
                dt1.Columns.Add("Codigo_Postal");
                dt1.Columns.Add("Calle");

                    row = dt1.NewRow();

                row["ID_Cliente"] = txtCliente.Text;
                row["Estado"] = estado;
                row["Localidad"] = localidad;
                row["Municipio"] = municipio;
                row["Colonia"] = colonia;
                row["Codigo_Postal"] = codigopostal;
                row["Calle"] = calle;
                dt1.Rows.Add(row);

                ViewState["datatable2"] = dt1;
                GVClientes.DataSource = dt1;
                GVClientes.DataBind();

            }
           

           
        }

        protected void ElimClienGrid_Click(object sender, EventArgs e)
        {

            System.Web.UI.WebControls.Button btn = (System.Web.UI.WebControls.Button)sender;
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            int index = gvr.RowIndex;

            DataTable dt = (DataTable)ViewState["datatable2"];
            dt.Rows.RemoveAt(index);

            GVClientes.DataSource = dt;
            GVClientes.DataBind();

        }

        protected void DDLRemolqueSN_SelectedIndexChanged(object sender, EventArgs e)
        {
            string remolque = DDLRemolqueSN.SelectedValue.ToString();
            if (remolque=="1")
            {
               
                ddlremolque1.Visible = true;
                ddlremolque2.Visible = true;
                TXTPLACA1.Visible = true;
                txtplac2.Visible = true;
                lblremolque.Visible = true;
                lblremolque2.Visible = true;
                lblplaca1.Visible = true;
                lblplaca2.Visible = true;


                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {



                    SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 21;
                    Da.Fill(Dt);


                    ListItem i;
                    foreach (DataRow r in Dt.Rows)
                    {
                        i = new ListItem(r["Nombre"].ToString(), r["Clave"].ToString());
                        ddlremolque1.Items.Add(i);
                        ddlremolque2.Items.Add(i);
                    }

                    ddlremolque1.Items.Insert(0, new ListItem("Seleccione Remolque", "0"));
                    ddlremolque2.Items.Insert(0, new ListItem("Seleccione Remolque", "0"));
                    conn.Close();
                }
            }
            else
            {
                //escodne el txt
                ddlremolque1.Visible = false;
                ddlremolque2.Visible = false;
                TXTPLACA1.Visible = false;
                txtplac2.Visible = false;
                lblremolque.Visible = false;
                lblremolque2.Visible = false;
                lblplaca1.Visible = false;
                lblplaca2.Visible = false;


            }

        }

    }
}