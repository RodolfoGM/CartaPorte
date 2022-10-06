using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CARTAPORTE
{
    public class Carta
    {

        public enum TipoAlerta { Info, Advertencia, Error }
        public static void MensajeAlerta(Panel panelMensajes, Label etiquetaMensaje, string mensaje, TipoAlerta tipoAlerta)
        {
            switch (tipoAlerta)
            {
                case TipoAlerta.Info:
                    panelMensajes.CssClass = "alert alert-success alert-dismissable fade in";
                    break;
                case TipoAlerta.Advertencia:
                    panelMensajes.CssClass = "alert alert-warning alert-dismissable fade in";
                    break;
                case TipoAlerta.Error:
                    panelMensajes.CssClass = "alert alert-danger alert-dismissable fade in";
                    break;
            }

            etiquetaMensaje.Text = mensaje;
            panelMensajes.Visible = true;
        }
    }
}