using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace CARTAPORTE
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

           
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {

                SqlDataAdapter Da = new SqlDataAdapter("CPSP01", conn);
                DataTable Dt = new DataTable();
                Da.SelectCommand.CommandType = CommandType.StoredProcedure;

                Da.SelectCommand.Parameters.Clear();
                Da.SelectCommand.Parameters.Add("@Accion", SqlDbType.Int).Value = 36;
                Da.SelectCommand.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = tbUsuario.Text;
                Da.SelectCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = tbPassword.Text;

                Da.Fill(Dt);

                if (Dt.Rows.Count>0)
                {
                    Session["usuariologueado"] = tbUsuario.Text;

                    Session.Timeout = 15;

                    Response.Redirect("Default.aspx");
                }
                else
                {
                    lblError.Text = "Error de Usuario o Contraseña";

                }
        

                conn.Close();
            }
        }
    }
}