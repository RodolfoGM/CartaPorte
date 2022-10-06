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
namespace CARTAPORTE
{
    public partial class PorciKowi : System.Web.UI.Page
    {
        #region Variables
        //modifiquen por su path
        static private string path = @"C:\Users\desarrollador.oracle\Desktop\CARTAPORTE\";
        static string pathXML = path + @"Elxml.xml";

        static string pathCer = path + @"00001000000507105277.cer";
        static string pathKey = path + @"CSD_ALIEMTOS_KOWI_AKO971007558_20210415_142921.key";
        static string clavePrivada = "alimentoskowi97";

        static string Empresa = "6";
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


        static string ANOAUT;
        #endregion Variables


        protected void Page_Load(object sender, EventArgs e)
        {


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
                        Da.SelectCommand.Parameters.Add("@IDEmpresa", SqlDbType.VarChar).Value = Empresa;
                        Da.Fill(Dt);


                        ListItem i;
                        foreach (DataRow r in Dt.Rows)
                        {
                            i = new ListItem(r["Nombre"].ToString(), r["RFC"].ToString());
                            DDLEmpresa.Items.Add(i);

                            CalleEmi = r["CALLE"].ToString();
                            ColEmi = r["Colonia"].ToString();
                            MuniEmi = r["Municipio"].ToString();
                            EstEmi = r["Estado"].ToString();
                            PaisEmi = r["Pais"].ToString();
                            CPEmi = r["CP"].ToString();
                            SERIE = r["SERIE"].ToString();

                        }

                        conn.Close();
                    }


                }
                //Llena Si es traslado o ingreso
                ListItem x;
                x = new ListItem("Traslado", "T");
                DDLTipoDoc.Items.Add(x);
                x = new ListItem("Ingreso", "I");
                DDLTipoDoc.Items.Add(x);

                DDLRETEN.Items.Insert(0, new ListItem("Seleccione opcion", "0"));
                DDLRETEN.Items.Insert(1, new ListItem("Retencion 4%", "4"));

                DDLIVA.Items.Insert(0, new ListItem("Seleccione opcion", "0"));
                DDLIVA.Items.Insert(1, new ListItem("Retencion 16%", "16"));
                //  DDLRETEN.Items.Insert(2, new ListItem("No", "2"));
                DDLEstado.Items.Insert(0, new ListItem("Seleccione opcion", "0"));
                DDLMunicipio.Items.Insert(0, new ListItem("Seleccione opcion", "0"));
                DDLLocalida.Items.Insert(0, new ListItem("Seleccione opcion", "0"));


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
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
                {
                    List<string> lista = new List<string>();


                    SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                    DataTable Dt = new DataTable();
                    Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    Da.SelectCommand.Parameters.Add("@IDEmpresa", SqlDbType.VarChar).Value = Empresa;
                    Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 12;

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
                        z = new ListItem(r["Nombre"].ToString(), r["Clave unidad"].ToString());
                        DDLUnidadA.Items.Add(z);

                    }
                    DDLUnidadA.Items.Insert(0, new ListItem("Seleccione Unidad de Medida", "0"));
                    conn.Close();
                }
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
                    TxtRFCClien.Text = Dt.Rows[0]["RFC"].ToString();
                    TxtNombreClien.Text = Dt.Rows[0]["Nombre"].ToString();
                    TxtPaisClient.Text = Dt.Rows[0]["Pais"].ToString();


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
                    TxtPaisOP.Text = Dt.Rows[0]["Pais"].ToString();
                    TxtCalleOp.Text = Dt.Rows[0]["Calle"].ToString();
                    TxtNumOp.Text = Dt.Rows[0]["NumeroExterior"].ToString();
                    TxtMunOP.Text = Dt.Rows[0]["Municipio"].ToString();
                    TxtLocOP.Text = Dt.Rows[0]["Localidad"].ToString();
                    TxtColOp.Text = Dt.Rows[0]["Colonia"].ToString();
                    txtEstadoOP.Text = Dt.Rows[0]["Estado"].ToString();
                    txtCPOpera.Text = Dt.Rows[0]["CodigoPostal"].ToString();


                }


            }

        }

        protected void myListDropDown_Change(object sender, EventArgs e)
        {

            string estad = DDLEstado.SelectedValue.ToString();

            carga_Localidad(estad);
            carga_Municipio(estad);
        }

        public void carga_Localidad(string estado)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
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
        public void carga_Municipio(string estado)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
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
            //TextBox2.Text = "fsdfs";
        }

        protected void DDLLocalida_SelectedIndexChanged(object sender, EventArgs e)
        {

            string locali = DDLLocalida.SelectedValue.ToString();

            Cargar_Colonia(locali);
        }

        public void Cargar_Colonia(string estado)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
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
                    DDLColonias.Items.Add(i);


                    //PAIREC = r["c_Pais"].ToString();

                }
            }

        }

        protected void DDLMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            string codigoP = CODREC;


            Carga_Colonia(codigoP);
        }

        public void Carga_Colonia(string codigo_postal)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@codigo_postal", SqlDbType.VarChar).Value = codigo_postal;
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 15;
                Da.Fill(Dt);

                ListItem i;
                foreach (DataRow r in Dt.Rows)
                {
                    i = new ListItem(r["nombre de Asentamiento"].ToString(), r["c_colonia"].ToString());
                    DDLColonias.Items.Add(i);



                }
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
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
                    FOLIO = Dt.Rows[0].ToString();
                }
            }


            string path2 = @"C:\Users\desarrollador.oracle\Desktop\CARTAPORTE\";
            //  string texto = "Ha llegado hasta linea Xdsadadasd";
            string fechan, fechalllegada, cvetipotoc, tipodoc;

            cvetipotoc = DDLTipoDoc.SelectedValue.ToString();
            if (cvetipotoc == "T")
            {
                tipodoc = "7";
            }
            tipodoc = "1";



            fechan = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

            fechalllegada = Convert.ToDateTime(TxtFechallegada.Text).ToString("yyyy-MM-ddTHH:mm:ss");

            string[] contenido = { "E" };

            List<string> lista = new List<string>(contenido.ToList());

            if (DDLTipoDoc.SelectedValue.ToString() == "T")
            {
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
                lista.Add("NOTAS3 Cero Pesos 00/100 M.N.");
                lista.Add("USOCFDI G01");
                //EMISOR

                lista.Add("RFCEMI " + DDLEmpresa.SelectedValue.ToString());
                lista.Add("NOMEMI " + DDLEmpresa.SelectedItem.Text);
                lista.Add("CVEREGIMEN 622");
                lista.Add("CALEMI " + CalleEmi);
                lista.Add("COLEMI " + ColEmi);
                lista.Add("MUNEMI " + MuniEmi);
                lista.Add("ESTEMI " + EstEmi);
                lista.Add("PAIEMI " + PaisEmi);
                lista.Add("CODEMI " + CPEmi);
                lista.Add("");
                //RECEPTOR
                lista.Add("RFCREC " + RFCREC);
                lista.Add("NOMREC " + NOMREC);
                lista.Add("CALREC " + CALREC);
                lista.Add("NEXREC " + NEXREC);
                lista.Add("NINREC " + NINREC);
                lista.Add("COLREC " + COLREC);
                lista.Add("MUNREC " + MUNREC);
                lista.Add("ESTREC " + ESTREC);
                lista.Add("PAIREC " + PAIREC);
                lista.Add("CODREC " + CODREC);

                //AQUI VA LA PARTE DE LOS ARTICULOS

                lista.Add("D");
                int contador = 1;
                foreach (GridViewRow row in GridView1.Rows)
                {

                    //string itenm = row.Cells[1].Text;
                    lista.Add("CANTID " + row.Cells[1].Text);
                    lista.Add("NUMLIN " + contador);
                    lista.Add("CVEPRODSERV " + row.Cells[5].Text);
                    lista.Add("CVEUNIDAD ACT");
                    lista.Add("DESCRI " + row.Cells[3].Text);
                    lista.Add("UNIDAD " + row.Cells[6].Text);
                    lista.Add("CVEUNIDAD ACT");
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
                    lista.Add("TASIPE 16");
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
                lista.Add("TOTIVA 16");
                lista.Add("TOTPAG 0.00");
                lista.Add("IVATRA1 0.00");
                lista.Add("TOTIVA1 16");
                lista.Add("TOTTRA 0.00");

                //COMIENZA EL COMPLEMENTO CARTA PORTE 
                lista.Add("");
                lista.Add("COM_CPT_INICPT");
                lista.Add("COM_CPT_VERSIO	1.0");//Version del complemento carta porte
                lista.Add("COM_CPT_TRANINT No"); //indica si los articulos salen o no de pais 
                lista.Add("COM_CPT_ENTSAL");
                lista.Add("COM_CPT_VIAENTSAL");
                //lista.Add("COM_CPT_TOTDIST 0.02");  El campo solamente debe existir si existe la sección AutotransporteFederal
                lista.Add("COM_CPT_FT_CVETRANS");//Clave Transporte cuando sea transporte federal en caso de las demas empresas sera el valor 01 "COM_CPT_FT_CVETRANS 01"

                //INICIO PARA LA INFORMACION DE UBICACIONES
                lista.Add("");

                lista.Add("COM_CPT_INIUBI");
                //lista.Add("COM_CPT_TIPEST  02"); //tipo estaciones
                lista.Add("COM_CPT_DISTREC" + TxtDisRec.Text); //Distancia recorrida total
                lista.Add("COM_CPT_ORI_RFCREM " + DDLEmpresa.SelectedValue.ToString());//RFC del emitente
                lista.Add("COM_CPT_ORI_NOMREM " + DDLEmpresa.SelectedItem.Text);//nombre de emitente 
                lista.Add("COM_CPT_ORI_IDTRIBREM");//numero de identificacion fiscal este dato solo debe de ir sii el remitente es EXTRANJERO
                lista.Add("COM_CPT_ORI_NUMEST");//si es autotransposte fenderla este campo no existe
                lista.Add("COM_CPT_ORI_NOMEST");
                lista.Add("COM_CPT_ORI_NAVTRAF");
                lista.Add("COM_CPT_ORI_FECSAL   " + fechan); // fecha y hora de salid de la mercancia            
                lista.Add("COM_CPT_FINUBI"); //FIN PARA LAS INFORMACION DE UBICACIONES

                lista.Add("");

                lista.Add("COM_CPT_INIUBI");
                lista.Add("COM_CPT_DES_IDDES DE000000");
                lista.Add("COM_CPT_DES_RFCDES " + RFCREC);
                lista.Add("COM_CPT_DES_NOMDES " + NOMREC);
                lista.Add("COM_CPT_DES_RESFISCREM");
                lista.Add("COM_CPT_DES_NUMEST");
                lista.Add("COM_CPT_DES_NOMEST");
                lista.Add("COM_CPT_DES_NAVTRAF");
                lista.Add("COM_CPT_DES_FECLLEG " + fechalllegada);//Fecha estimada de llegada de la mercancia 
                lista.Add("COM_CPT_DOM_CAL " + CALREC);
                lista.Add("COM_CPT_DOM_NUMEXT " + NEXREC);
                lista.Add("COM_CPT_DOM_NUMINT " + NINREC);
                lista.Add("COM_CPT_DOM_COL " + DDLColonias.SelectedValue.ToString());
                lista.Add("COM_CPT_DOM_LOC " + DDLLocalida.SelectedValue.ToString());
                lista.Add("COM_CPT_DOM_REF ");
                lista.Add("COM_CPT_DOM_MUN " + DDLMunicipio.SelectedValue.ToString());
                lista.Add("COM_CPT_DOM_EST " + DDLEstado.SelectedValue.ToString());
                lista.Add("COM_CPT_DOM_PAI MEX");
                lista.Add("COM_CPT_DOM_CP " + CODREC);

                lista.Add("COM_CPT_FINUBI");

                lista.Add("");


                //INICIO DE SECCION DE MERCANCIAS

                lista.Add("COM_CPT_INIMERS ");

                lista.Add("COM_CPT_MER_PESBRU ");
                lista.Add("COM_CPT_MER_UNIPES ");
                lista.Add("COM_CPT_MER_PESONET");
                lista.Add("COM_CPT_MER_NUMTOT ");
                lista.Add("COM_CPT_MER_CARGO ");

                lista.Add("COM_CPT_INIMER");

                foreach (GridViewRow row in GridView1.Rows)
                {

                    lista.Add("COM_CPT_MER_BIENTRA " + row.Cells[7].Text);
                    lista.Add("COM_CPT_MER_BIENTRA " + row.Cells[6].Text);
                    lista.Add("COM_CPT_MER_DESCRI " + row.Cells[3].Text);
                    lista.Add("COM_CPT_MER_PKG " + row.Cells[1].Text);
                    lista.Add("");

                }

                //lista.Add("COM_CPT_INICANTRAN");
                //lista.Add("COM_CPT_CMER_CANTID " + GridView1.Rows.Count);

                // lista.Add("COM_CPT_FINCANTRAN");

                lista.Add("");
                lista.Add("COM_CPT_FINMER ");
                lista.Add("");
                lista.Add("COM_CPT_FINMERS ");
                lista.Add("");

                //

                lista.Add("COM_CPT_INIAUTO");
                lista.Add("COM_CPT_AUT_SCT	TPAF01");
                lista.Add("COM_CPT_AUT_PSCT"); //numero de permiso 
                lista.Add("COM_CPT_AUT_NOMASEG ");//nombre de aseguradora
                lista.Add("COM_CPT_AUT_NUMPOL");  //numero de pliza
                lista.Add("COM_CPT_AUT_CONVEH " + DDLConfVehi.SelectedValue.ToString());
                lista.Add("COM_CPT_AUT_PLACAV " + DDLAuto.SelectedValue.ToString());
                lista.Add("COM_CPT_AUT_ANIOV " + ANOAUT);
                // EN CASO DE LLEVAR REMOLQUES ESTA PARTE DE ABAJO SE TENDRIA QUE LLENAR 
                //COM_CPT_AUT_SUTIPREM1 CTR001
                //COM_CPT_AUT_PLACA1 XXXXX3
                //COM_CPT_AUT_SUTIPREM2 CTR002
                //COM_CPT_AUT_PLACA2 XXXXX5
                lista.Add("COM_CPT_FINAUTO");
                lista.Add("");

                //crear catalogo de choferes 
                lista.Add("COM_CPT_INIOPE");
                lista.Add("COM_CPT_OPE_RFCOPE " + TxtRFCOp.Text);
                lista.Add("COM_CPT_OPE_NUMLIC " + txtNumLic.Text);
                lista.Add("COM_CPT_OPE_NOMOPE " + DDLOperador.SelectedItem.Text);
                lista.Add("COM_CPT_OPE_IDTRIBOPE ");
                lista.Add("COM_CPT_OPE_RESFISCOPE " + TxtCalleOp.Text);
                lista.Add("COM_CPT_DOM_NUMEXT " + TxtNumOp.Text);
                lista.Add("COM_CPT_DOM_COL " + TxtColOp.Text);
                lista.Add("COM_CPT_DOM_LOC " + TxtLocOP.Text);
                lista.Add("COM_CPT_DOM_REF " + TxtColOp.Text);
                lista.Add("COM_CPT_DOM_MUN " + TxtMunOP.Text);
                lista.Add("COM_CPT_DOM_EST " + txtEstadoOP.Text);
                lista.Add("COM_CPT_DOM_PAI " + TxtPaisOP.Text);
                lista.Add("COM_CPT_DOM_CP " + txtCPOpera.Text);

                lista.Add("COM_CPT_FINOPE");



                contenido = lista.ToArray();

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(path2, "Nutri.txt")))
                {

                    foreach (string line in contenido)
                        outputFile.WriteLine(line);


                }

            }
            else            //COMIENZA EL ARMADO DEL TXT PARA TRASLADO//
            {
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
                lista.Add("NOTAS3 Cero Pesos 00/100 M.N.");
                lista.Add("USOCFDI G01");
                //EMISOR

                lista.Add("RFCEMI " + DDLEmpresa.SelectedValue.ToString());
                lista.Add("NOMEMI " + DDLEmpresa.SelectedItem.Text);
                lista.Add("CVEREGIMEN 601");
                lista.Add("CALEMI " + CalleEmi);
                lista.Add("COLEMI " + ColEmi);
                lista.Add("MUNEMI " + MuniEmi);
                lista.Add("ESTEMI " + EstEmi);
                lista.Add("PAIEMI " + PaisEmi);
                lista.Add("CODEMI " + CPEmi);
                lista.Add("");
                //RECEPTOR
                lista.Add("RFCREC " + RFCREC);
                lista.Add("NOMREC " + NOMREC);
                lista.Add("CALREC " + CALREC);
                lista.Add("NEXREC " + NEXREC);
                lista.Add("NINREC " + NINREC);
                lista.Add("COLREC " + COLREC);
                lista.Add("MUNREC " + MUNREC);
                lista.Add("ESTREC " + ESTREC);
                lista.Add("PAIREC " + PAIREC);
                lista.Add("CODREC " + CODREC);

                lista.Add("D");
                int contador = 1;
                int importe = 0;
                int ivas = 0;
                int impuestoret = 0;
                int totret = 0;
                int importeFinal = 0;
                int IvasFinal = 0;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    importe = (Convert.ToInt32(row.Cells[2].Text)) * Convert.ToInt32(row.Cells[4].Text);
                    ivas = ((Convert.ToInt32(row.Cells[2].Text)) * Convert.ToInt32(row.Cells[4].Text) * (Convert.ToInt32(DDLIVA.SelectedValue.ToString())) / 100);
                    //string itenm = row.Cells[1].Text;
                    lista.Add("CANTID " + row.Cells[2].Text);
                    lista.Add("NUMLIN " + contador);
                    lista.Add("CVEPRODSERV " + row.Cells[5].Text);//Clave SAT
                    lista.Add("CVEUNIDAD " + row.Cells[3].Text);//CVE UNIDAD EN ESTE MOMENTO SOLO ESTA EN KILOGRAMOS SI SE NECESITA CAMBIAR A PIEZA H87 SE TENDRA UE PEDIR ESTA INFORMACION AL USUARIO O A LA BASE DE DATOS
                    lista.Add("DESCRI " + row.Cells[1].Text);
                    lista.Add("UNIDAD " + row.Cells[3].Text);
                    lista.Add("VALUNI " + row.Cells[4].Text);
                    lista.Add("IMPORT ");//IMPORTE TOTAL DEL DOCUMENTO 
                    lista.Add("PBRUDE " + row.Cells[4].Text);//Valor unitario
                    lista.Add("IMPBRU " + (Convert.ToInt32(row.Cells[2].Text)) * Convert.ToInt32(row.Cells[4].Text));
                    lista.Add("IMPORTIPE");
                    lista.Add("TIPIPETR TASA");
                    lista.Add("TASIPE" + DDLIVA.SelectedValue.ToString());
                    lista.Add("FCTTASIPE " + (Convert.ToInt32(DDLIVA.SelectedValue.ToString())) / 100); //iva expresado como 0.16
                    lista.Add("MONIPE " + ((Convert.ToInt32(row.Cells[2].Text)) * Convert.ToInt32(row.Cells[4].Text) * (Convert.ToInt32(DDLIVA.SelectedValue.ToString())) / 100));

                    lista.Add("TASIEP 0.00");
                    lista.Add("FCTTASIEP 0.00");
                    lista.Add("MONIEP 0.00");
                    importeFinal = importeFinal + importe;
                    IvasFinal = IvasFinal + ivas;
                    if (DDLRETEN.SelectedValue.ToString() == "4")//Retencion solo aplica en los fletes
                    {

                        lista.Add("TIPIVARE TASA");
                        lista.Add("IVARET_P " + DDLRETEN.SelectedValue.ToString()); //iva retenido 
                        lista.Add("FCTIVARET_P " + Convert.ToInt32(DDLRETEN.SelectedValue.ToString()) / 100); //Tasa de iva retenido en valor 0.04
                        lista.Add("IVARET_D " + ((Convert.ToInt32(row.Cells[2].Text)) * Convert.ToInt32(row.Cells[4].Text) * (Convert.ToInt32(DDLRETEN.SelectedValue.ToString())) / 100));//Valor total de la linea por el valor de la retencion  300 * 0.04

                        impuestoret = ((Convert.ToInt32(row.Cells[2].Text)) * Convert.ToInt32(row.Cells[4].Text) * (Convert.ToInt32(DDLRETEN.SelectedValue.ToString())) / 100);
                    }

                    lista.Add("");

                    contador++;
                }


                //
                int immporteciva;
                immporteciva = (importe + IvasFinal);
                //IMPUESTOS
                lista.Add("R");
                lista.Add("SUBTBR " + importeFinal); // Total de las lineas sin impuestos
                //lista.Add("MONDET 0.00");
                //lista.Add("PRCDSG 0.00");
                //lista.Add("SUBTOT 0.00");
                //lista.Add("SUBTAI 0.00");
                //lista.Add("IVATRA 0.00");
                lista.Add("TOTIVA " + DDLIVA.SelectedValue.ToString());//Total de iva de la factura 
                lista.Add("TOTPAG " + (immporteciva - impuestoret));// restar impuestoret
                lista.Add("TOTTRA 16");
                lista.Add("TOTRET " + totret);

                //COMIENZA EL COMPLEMENTO CARTA PORTE 
                lista.Add("");
                lista.Add("COM_CPT_INICPT");
                lista.Add("COM_CPT_VERSIO	1.0");//Version del complemento carta porte
                lista.Add("COM_CPT_TRANINT NO"); //indica si los articulos salen o no de pais 
                lista.Add("COM_CPT_ENTSAL");
                lista.Add("COM_CPT_VIAENTSAL");
                //lista.Add("COM_CPT_TOTDIST 0.02");  El campo solamente debe existir si existe la sección AutotransporteFederal
                lista.Add("COM_CPT_FT_CVETRANS");//Clave Transporte cuando sea transporte federal en caso de las demas empresas sera el valor 01 "COM_CPT_FT_CVETRANS 01"

                //INICIO PARA LA INFORMACION DE UBICACIONES
                lista.Add("");

                lista.Add("COM_CPT_INIUBI");
                //lista.Add("COM_CPT_TIPEST  02"); //tipo estaciones
                lista.Add("COM_CPT_DISTREC" + TxtDisRec.Text); //Distancia recorrida total
                lista.Add("COM_CPT_ORI_RFCREM " + DDLEmpresa.SelectedValue.ToString());//RFC del emitente
                lista.Add("COM_CPT_ORI_NOMREM " + DDLEmpresa.SelectedItem.Text);//nombre de emitente 
                lista.Add("COM_CPT_ORI_IDTRIBREM");//numero de identificacion fiscal este dato solo debe de ir sii el remitente es EXTRANJERO
                lista.Add("COM_CPT_ORI_NUMEST");//si es autotransposte fenderla este campo no existe
                lista.Add("COM_CPT_ORI_NOMEST");
                lista.Add("COM_CPT_ORI_NAVTRAF");
                lista.Add("COM_CPT_ORI_FECSAL   " + fechan); // fecha y hora de salid de la mercancia            
                lista.Add("COM_CPT_FINUBI"); //FIN PARA LAS INFORMACION DE UBICACIONES

                lista.Add("");

                lista.Add("COM_CPT_INIUBI");
                lista.Add("COM_CPT_DES_IDDES DE000000");
                lista.Add("COM_CPT_DES_RFCDES " + RFCREC);
                lista.Add("COM_CPT_DES_NOMDES " + NOMREC);
                lista.Add("COM_CPT_DES_RESFISCREM");
                lista.Add("COM_CPT_DES_NUMEST");
                lista.Add("COM_CPT_DES_NOMEST");
                lista.Add("COM_CPT_DES_NAVTRAF");
                lista.Add("COM_CPT_DES_FECLLEG " + fechalllegada);//Fecha estimada de llegada de la mercancia 
                lista.Add("COM_CPT_DOM_CAL " + CALREC);
                lista.Add("COM_CPT_DOM_NUMEXT " + NEXREC);
                lista.Add("COM_CPT_DOM_NUMINT " + NINREC);
                lista.Add("COM_CPT_DOM_COL " + DDLColonias.SelectedValue.ToString());
                lista.Add("COM_CPT_DOM_LOC " + DDLLocalida.SelectedValue.ToString());
                lista.Add("COM_CPT_DOM_REF ");
                lista.Add("COM_CPT_DOM_MUN " + DDLMunicipio.SelectedValue.ToString());
                lista.Add("COM_CPT_DOM_EST " + DDLEstado.SelectedValue.ToString());
                lista.Add("COM_CPT_DOM_PAI MEX");
                lista.Add("COM_CPT_DOM_CP " + CODREC);

                lista.Add("COM_CPT_FINUBI");

                lista.Add("");


                //INICIO DE SECCION DE MERCANCIAS

                lista.Add("COM_CPT_INIMERS ");

                //lista.Add("COM_CPT_MER_PESBRU "); Peso bruto pero si es transporte ferroviario o maritimo 
                lista.Add("COM_CPT_MER_UNIPES ");
                lista.Add("COM_CPT_MER_PESONET");
                lista.Add("COM_CPT_MER_NUMTOT " + contador);
                lista.Add("COM_CPT_MER_CARGO ");

                lista.Add("COM_CPT_INIMER");

                foreach (GridViewRow row in GridView1.Rows)
                {

                    lista.Add("COM_CPT_MER_BIENTRA " + row.Cells[5].Text);
                    lista.Add("COM_CPT_MER_DESCRI " + row.Cells[1].Text);
                    lista.Add("COM_CPT_MER_PKG " + row.Cells[2].Text);
                    lista.Add("");

                }

                //lista.Add("COM_CPT_INICANTRAN");
                //lista.Add("COM_CPT_CMER_CANTID " + GridView1.Rows.Count);

                // lista.Add("COM_CPT_FINCANTRAN");

                lista.Add("");
                lista.Add("COM_CPT_FINMER ");
                lista.Add("");
                lista.Add("COM_CPT_FINMERS ");
                lista.Add("");

                //

                lista.Add("COM_CPT_INIAUTO");
                lista.Add("COM_CPT_AUT_SCT	TPAF01");
                lista.Add("COM_CPT_AUT_PSCT"); //numero de permiso 
                lista.Add("COM_CPT_AUT_NOMASEG ");//nombre de aseguradora
                lista.Add("COM_CPT_AUT_NUMPOL");  //numero de pliza
                lista.Add("COM_CPT_AUT_CONVEH " + DDLConfVehi.SelectedValue.ToString());
                lista.Add("COM_CPT_AUT_PLACAV " + DDLAuto.SelectedValue.ToString());
                lista.Add("COM_CPT_AUT_ANIOV " + ANOAUT);
                // EN CASO DE LLEVAR REMOLQUES ESTA PARTE DE ABAJO SE TENDRIA QUE LLENAR 
                //COM_CPT_AUT_SUTIPREM1 CTR001
                //COM_CPT_AUT_PLACA1 XXXXX3
                //COM_CPT_AUT_SUTIPREM2 CTR002
                //COM_CPT_AUT_PLACA2 XXXXX5
                lista.Add("COM_CPT_FINAUTO");
                lista.Add("");

                //crear catalogo de choferes 
                lista.Add("COM_CPT_INIOPE");
                lista.Add("COM_CPT_OPE_RFCOPE " + TxtRFCOp.Text);
                lista.Add("COM_CPT_OPE_NUMLIC " + txtNumLic.Text);
                lista.Add("COM_CPT_OPE_NOMOPE " + DDLOperador.SelectedItem.Text);
                lista.Add("COM_CPT_OPE_IDTRIBOPE ");
                lista.Add("COM_CPT_OPE_RESFISCOPE " + TxtCalleOp.Text);
                lista.Add("COM_CPT_DOM_NUMEXT " + TxtNumOp.Text);
                lista.Add("COM_CPT_DOM_COL " + TxtColOp.Text);
                lista.Add("COM_CPT_DOM_LOC " + TxtLocOP.Text);
                lista.Add("COM_CPT_DOM_REF " + TxtColOp.Text);
                lista.Add("COM_CPT_DOM_MUN " + TxtMunOP.Text);
                lista.Add("COM_CPT_DOM_EST " + txtEstadoOP.Text);
                lista.Add("COM_CPT_DOM_PAI " + TxtPaisOP.Text);
                lista.Add("COM_CPT_DOM_CP " + txtCPOpera.Text);

                lista.Add("COM_CPT_FINOPE");

                contenido = lista.ToArray();

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(path2, "Nutri.txt")))
                {

                    foreach (string line in contenido)
                        outputFile.WriteLine(line);


                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {


            DataTable dt1 = new DataTable();
            DataRow row;
            // dt1 = new DataTable();

            if (GridView1.Rows.Count > 0)
            {
                dt1 = (DataTable)GridView1.DataSource;

            }
            else
            {
                dt1 = new DataTable();

            }


            dt1.Columns.Add("Codigo");
            dt1.Columns.Add("Descripcion");
            dt1.Columns.Add("Cantidad");
            dt1.Columns.Add("Unidad");
            dt1.Columns.Add("Precio Unitario");
            dt1.Columns.Add("Calve SAT");

            row = dt1.NewRow();

            row["Codigo"] = txtCodigo.Text;
            row["Descripcion"] = TxtDescripcionA.Text;
            row["Cantidad"] = txtCantidadA.Text;
            row["Unidad"] = DDLUnidadA.SelectedValue.ToString();
            row["Precio Unitario"] = txtPrecioU.Text;
            row["Calve SAT"] = TxtClaveSatA.Text;
            dt1.Rows.Add(row);

            GridView1.DataSource = dt1;
            GridView1.DataBind();

        }

        protected void DDLEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string rfcempresa = DDLEmpresa.SelectedValue.ToString();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                List<string> lista = new List<string>();


                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@IDEmpresa", SqlDbType.VarChar).Value = rfcempresa;
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 17;

                Da.Fill(Dt);

                string idempresa = Dt.Rows[0]["ID"].ToString();

                Cargar_Vechiculos(idempresa);

            }
        }

        public void Cargar_Vechiculos(string idempresa)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                List<string> lista = new List<string>();


                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;
                Da.SelectCommand.Parameters.Add("@IDEmpresa", SqlDbType.VarChar).Value = idempresa;
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 12;

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

    }
}