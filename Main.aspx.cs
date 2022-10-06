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

    public partial class Main : System.Web.UI.Page
    {
        //modifiquen por su path
        static private string path = @"C:\GACOSTA\CLOUD\DEVELOP\CARTAPORTE\";
        static string pathXML = path + @"Elxml.xml";

        static string pathCer = path + @"00001000000507105277.cer";
        static string pathKey = path + @"CSD_ALIEMTOS_KOWI_AKO971007558_20210415_142921.key";
        static string clavePrivada = "alimentoskowi97";

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
                        Da.Fill(Dt);


                        ListItem i;
                        foreach (DataRow r in Dt.Rows)
                        {
                            i = new ListItem(r["Nombre"].ToString(), r["ID"].ToString());
                            DropDownList1.Items.Add(i);
                            
                        }

                        conn.Close();
                    }
                }
            }




            resultado result = new resultado();
            //Obtener numero certificado------------------------------------------------------------


            //Obtenemos el numero
            string numeroCertificado, aa, b, c;
            SelloDigital.leerCER(pathCer, out aa, out b, out c, out numeroCertificado);



            Comprobante oComprobante = new Comprobante();
            oComprobante.Version = "3.3";
            oComprobante.Serie = "TALIME";
            oComprobante.Folio = "666";
            oComprobante.Fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            //oComprobante.Sello = "FALTANTE"; // siguiente video
            //oComprobante.FormaPago = "99";
            oComprobante.NoCertificado = numeroCertificado; // siguiente video
            //oComprobante.Certificado = ""; // siguiente video
            oComprobante.SubTotal = 0;
            oComprobante.Moneda = "XXX";
            oComprobante.TipoCambio = 1;
            oComprobante.Total = 0;
            oComprobante.TipoDeComprobante = c_TipoDeComprobante.T;
            //oComprobante.MetodoPago = c_MetodoPago.PUE;
            oComprobante.LugarExpedicion = "85230"; // codigo postal



            ComprobanteEmisor oEmisor = new ComprobanteEmisor();
            oEmisor.Rfc = "XIA190128J61";
            oEmisor.Nombre = "ALIMENTOS KOWI, S.A. DE C.V.";
            oEmisor.RegimenFiscal = c_RegimenFiscal.Item601;


            ComprobanteReceptor oReceptor = new ComprobanteReceptor();
            oReceptor.Rfc = "XAXX010101000";
            oReceptor.Nombre = "RUTA 5 OBREGON";
            oReceptor.ResidenciaFiscal = c_Pais.MEX;
            oReceptor.UsoCFDI = "P01";


            oComprobante.Emisor = oEmisor;
            oComprobante.Receptor = oReceptor;

            List<ComprobanteConcepto> lstConceptos = new List<ComprobanteConcepto>();
            ComprobanteConcepto oConcepto = new ComprobanteConcepto();

            oConcepto.ClaveProdServ = "50112008";
            oConcepto.Cantidad = 1;
            oConcepto.ClaveUnidad = "KGM"; //Manage Receivables Lookups>XXKW_FE CLAVE UOM SAT
            oConcepto.Descripcion = "CARNE DE CERDO"; // FALTA
            oConcepto.ValorUnitario = 1;
            oConcepto.Importe = 1;

            
            //oConcepto.Unidad = "KG"; //Manage Receivables Lookups>XXKW_FE CLAVE UOM SAT

            lstConceptos.Add(oConcepto);
            oComprobante.Conceptos = lstConceptos.ToArray();


            /// CARTA PORTE /// CARTA PORTE   /// CARTA PORTE   /// CARTA PORTE   /// CARTA PORTE   /// CARTA PORTE   
            CartaPorte cartaKowi = new CartaPorte();
           

            cartaKowi.Version = "1.0";
            cartaKowi.TranspInternac = CartaPorteTranspInternac.No;
            cartaKowi.TotalDistRecSpecified = true;
            cartaKowi.TotalDistRec = 600m;
            
            //cartaKowi.EntradaSalidaMercSpecified = true;
            //cartaKowi.EntradaSalidaMerc = CartaPorteEntradaSalidaMerc.Salida;


            /// CARTA PORTE UBICACIONES
            CartaPorteUbicacion oCartaPorteUbicacion = new CartaPorteUbicacion();
            CartaPorteUbicacion oCartaPorteUbicacion2 = new CartaPorteUbicacion();

            CartaPorteUbicacionOrigen oCartaPorteUbicacionOrigen = new CartaPorteUbicacionOrigen();
            CartaPorteUbicacionDestino oCartaPorteUbicacionDestino = new CartaPorteUbicacionDestino();

            
            oCartaPorteUbicacionOrigen.FechaHoraSalida = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            oCartaPorteUbicacionOrigen.RFCRemitente = "XIA190128J61";
            //oCartaPorteUbicacionOrigen.NumEstacionSpecified = true;
            //oCartaPorteUbicacionOrigen.NumEstacion = c_Estaciones.EF2138;
            oCartaPorteUbicacion.Origen = oCartaPorteUbicacionOrigen;            

            List<CartaPorteUbicacion> lstCartaPorteUbicacion = new List<CartaPorteUbicacion>();
            lstCartaPorteUbicacion.Add(oCartaPorteUbicacion);

            oCartaPorteUbicacionDestino.FechaHoraProgLlegada = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            oCartaPorteUbicacionDestino.RFCDestinatario = "XIA190128J61"; //"XAXX010101000";
            //oCartaPorteUbicacionDestino.NumEstacionSpecified = true;
            //oCartaPorteUbicacionDestino.NumEstacion = c_Estaciones.EF1102;
            oCartaPorteUbicacion2.Destino = oCartaPorteUbicacionDestino;

            lstCartaPorteUbicacion.Add(oCartaPorteUbicacion2);
            oCartaPorteUbicacion.DistanciaRecorridaSpecified = true;
            oCartaPorteUbicacion.DistanciaRecorrida = 600m;
            oCartaPorteUbicacion2.DistanciaRecorridaSpecified = true;
            oCartaPorteUbicacion2.DistanciaRecorrida = 600m;


            cartaKowi.Ubicaciones = lstCartaPorteUbicacion.ToArray();
            


            //// CARTAPORTE MERCANCIAS
            CartaPorteMercancias oCartaPorteMercancias = new CartaPorteMercancias();
            CartaPorteMercanciasMercancia oCartaPorteMercanciasMercancia = new CartaPorteMercanciasMercancia();

            oCartaPorteMercanciasMercancia.Cantidad = 1;
            
            oCartaPorteMercanciasMercancia.BienesTranspSpecified = true;
            oCartaPorteMercanciasMercancia.BienesTransp = "50112008";
            oCartaPorteMercanciasMercancia.ClaveUnidad = "KGM";
            oCartaPorteMercanciasMercancia.Descripcion = "CARNE DE COSHI";
            oCartaPorteMercanciasMercancia.PesoEnKg = 1;
            List<CartaPorteMercanciasMercancia> lstCartaPorteMercanciasMercancia = new List<CartaPorteMercanciasMercancia>();
            lstCartaPorteMercanciasMercancia.Add(oCartaPorteMercanciasMercancia);

            oCartaPorteMercancias.PesoBrutoTotal = 1;
            oCartaPorteMercancias.NumTotalMercancias = 1;
            oCartaPorteMercancias.Mercancia = lstCartaPorteMercanciasMercancia.ToArray();

            cartaKowi.Mercancias = oCartaPorteMercancias;

            /// AUTOTRANSPORTE FEDERAL
            CartaPorteMercanciasAutotransporteFederalIdentificacionVehicular oCartaIDentificacionvehicular = new CartaPorteMercanciasAutotransporteFederalIdentificacionVehicular();
            oCartaIDentificacionvehicular.ConfigVehicular = c_ConfigAutotransporte.C2;
            oCartaIDentificacionvehicular.PlacaVM = "gtld45";
            oCartaIDentificacionvehicular.AnioModeloVM = 2020;

            CartaPorteMercanciasAutotransporteFederal oCartaPorteMercanciasAutotransporteFederal = new CartaPorteMercanciasAutotransporteFederal();
            oCartaPorteMercanciasAutotransporteFederal.IdentificacionVehicular = oCartaIDentificacionvehicular;
            oCartaPorteMercanciasAutotransporteFederal.PermSCT = c_TipoPermiso.TPAF02;
            oCartaPorteMercanciasAutotransporteFederal.NumPermisoSCT = "6545465" ;
            oCartaPorteMercanciasAutotransporteFederal.NumPolizaSeguro = "DEMO";
            oCartaPorteMercanciasAutotransporteFederal.NombreAseg = "Alfredo Palacios";

            
            cartaKowi.Mercancias.AutotransporteFederal = oCartaPorteMercanciasAutotransporteFederal;



            ///// CARTAPORTE CartaPorteFiguraTransporte > OPERADORES
            CartaPorteFiguraTransporte oCartaPorteFiguraTransporte = new CartaPorteFiguraTransporte();
            CartaPorteFiguraTransporteOperadoresOperador oCartaPorteFiguraTransporteOperadoresOperador = new CartaPorteFiguraTransporteOperadoresOperador();
            CartaPorteFiguraTransporteOperadoresOperadorDomicilio oCartaPorteFiguraTransporteOperadoresOperadorDomicilio = new CartaPorteFiguraTransporteOperadoresOperadorDomicilio();

            oCartaPorteFiguraTransporteOperadoresOperadorDomicilio.Calle = "Carretera Fed Mexico-Nogales";
            oCartaPorteFiguraTransporteOperadoresOperadorDomicilio.CodigoPostal = "85230";
            oCartaPorteFiguraTransporteOperadoresOperadorDomicilio.Colonia = "1013"; //"Guaymitas";
            oCartaPorteFiguraTransporteOperadoresOperadorDomicilio.Estado = "SON";
            oCartaPorteFiguraTransporteOperadoresOperadorDomicilio.Municipio = "042";
            oCartaPorteFiguraTransporteOperadoresOperadorDomicilio.Pais = c_Pais.MEX ;
            
            oCartaPorteFiguraTransporteOperadoresOperador.RFCOperador = "CACX7605101P8";
            oCartaPorteFiguraTransporteOperadoresOperador.NumLicencia = "123456781234567";
            oCartaPorteFiguraTransporteOperadoresOperador.NombreOperador = "Jose Francisco Leyva Verdugo";
            oCartaPorteFiguraTransporteOperadoresOperador.ResidenciaFiscalOperadorSpecified = true;
            oCartaPorteFiguraTransporteOperadoresOperador.ResidenciaFiscalOperador = c_Pais.MEX;            
            oCartaPorteFiguraTransporteOperadoresOperador.Domicilio = oCartaPorteFiguraTransporteOperadoresOperadorDomicilio;

            List<CartaPorteFiguraTransporteOperadoresOperador> lstCartaPorteFiguraTransporteOperadoresOperador = new List<CartaPorteFiguraTransporteOperadoresOperador>();
            lstCartaPorteFiguraTransporteOperadoresOperador.Add(oCartaPorteFiguraTransporteOperadoresOperador);

            oCartaPorteFiguraTransporte.Operadores = lstCartaPorteFiguraTransporteOperadoresOperador.ToArray();

            oCartaPorteFiguraTransporte.CveTransporte = c_CveTransporte.Item01; // autotransporte federal

            cartaKowi.FiguraTransporte = oCartaPorteFiguraTransporte;

            ///  SERIALIZAMOS LA CARTA PORTE
            oComprobante.Complemento = new ComprobanteComplemento[1];
            oComprobante.Complemento[0] = new ComprobanteComplemento();

            XmlDocument docCarta = new XmlDocument();
            XmlSerializerNamespaces xmlNameSpaceCarta = new XmlSerializerNamespaces();
            xmlNameSpaceCarta.Add("cartaporte", "http://www.sat.gob.mx/CartaPorte");
            using (XmlWriter writer = docCarta.CreateNavigator().AppendChild())
            {
                new XmlSerializer(cartaKowi.GetType()).Serialize(writer, cartaKowi, xmlNameSpaceCarta);
            }
            oComprobante.Complemento[0].Any = new XmlElement[1];
            oComprobante.Complemento[0].Any[0] = docCarta.DocumentElement;

            // creamos el xml
            CreateXML(oComprobante);

            string cadenaOriginal = "";
            string pathxsl = path + @"cadenaoriginal_3_3.xslt";
            System.Xml.Xsl.XslCompiledTransform transformador = new System.Xml.Xsl.XslCompiledTransform(true);
            transformador.Load(pathxsl);

            using (StringWriter sw = new StringWriter())
            using (XmlWriter xwo = XmlWriter.Create(sw, transformador.OutputSettings))
            {

                transformador.Transform(pathXML, xwo);
                cadenaOriginal = sw.ToString();
            }

            SelloDigital oSelloDigital = new SelloDigital();
            oComprobante.Certificado = oSelloDigital.Certificado(pathCer);
            oComprobante.Sello = oSelloDigital.Sellar(cadenaOriginal, pathKey, clavePrivada);

            // creamos el xml CON EL SELLO
            CreateXML(oComprobante);
            result = TimbrarXML();
           
           
            TextBox1.Text = result.ErrorMessage;
            TextBox2.Text = result.ErrorCode;
        }

        private static void CreateXML(Comprobante oComprobante)
        {
            //SERIALIZAMOS.-------------------------------------------------

            XmlSerializerNamespaces xmlNameSpace = new XmlSerializerNamespaces();
            xmlNameSpace.Add("cfdi", "http://www.sat.gob.mx/cfd/3");
            xmlNameSpace.Add("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital"); 
            xmlNameSpace.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            xmlNameSpace.Add("cartaporte", "http://www.sat.gob.mx/CartaPorte"); // cartaporte
            xmlNameSpace.Add("xsd", "http://www.sat.gob.mx/sitio_internet/cfd/CartaPorte/CartaPorte.xsd"); // cartaporte


            XmlSerializer oXmlSerializar = new XmlSerializer(typeof(Comprobante));

            string sXml = "";

            using (var sww = new Utils.StringWriterWithEncoding(Encoding.UTF8))
            {

                using (XmlWriter writter = XmlWriter.Create(sww))
                {

                    oXmlSerializar.Serialize(writter, oComprobante, xmlNameSpace);
                    sXml = sww.ToString();
                }

            }

            //guardamos el string en un archivo
            System.IO.File.WriteAllText(pathXML, sXml);


        }


        //[Obsolete]
        private static resultado TimbrarXML()
        {

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
            //ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();


            byte[] cer = System.IO.File.ReadAllBytes(pathCer);
            string cer64 = Convert.ToBase64String(cer);

            byte[] key = System.IO.File.ReadAllBytes(pathKey);
            string key64 = Convert.ToBase64String(key);

            byte[] pass = System.Text.Encoding.UTF8.GetBytes(clavePrivada);
            string pass64 = Convert.ToBase64String(pass);

            byte[] elxml = System.IO.File.ReadAllBytes(pathXML);
            //byte[] elxml = System.IO.File.ReadAllBytes(@"C:\BORRAME\XML\Alimentos Kowi.xml");
            string xml64 = Convert.ToBase64String(elxml);


            DetecnoClient client = new DetecnoClient();
            resultado result = new resultado();

            result = client.ComprobanteGenerar33("i4ZoYqvJcyY4eytPFPZjzc8Bt5jOWKL-kOoe7AN8H7c=", cer64, key64, pass64, xml64);


            // Use the 'client' variable to call operations on the service.

            // Always close the client.
            client.Close();
            return result;

        }




    }

}