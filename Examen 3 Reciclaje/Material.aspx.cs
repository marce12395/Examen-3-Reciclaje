using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Examen_3_Reciclaje
{
    public static class JavaScriptHelper
    {
        public static void MostrarAlerta(Page page, string message)
        {
            string script = $"<script type='text/javascript'>alert('{message}');</script>";
            ClientScriptManager cs = page.ClientScript;
            cs.RegisterStartupScript(page.GetType(), "AlertScript", script);
        }
    }

    public partial class Material : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrid();
            }
        }


        protected void LlenarGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["conexionmam"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM MaterialReciclable", con))
            {
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    GridView1.DataSource = rdr;
                    GridView1.DataBind();
                }
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexionmam"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            using (SqlCommand comando = new SqlCommand(
                "INSERT INTO MaterialReciclable (Tipo, NombreMaterial, PuntosPorUnidad) " +
                "VALUES (@Tipo, @NombreMaterial, @PuntosPorUnidad)", conexion))
            {

                comando.Parameters.AddWithValue("@Tipo", tipo.Text.Trim());
                comando.Parameters.AddWithValue("@NombreMaterial", tmateria.Text.Trim());
                comando.Parameters.AddWithValue("@PuntosPorUnidad", tpuntos.Text.Trim());


                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    LlenarGrid();
                    JavaScriptHelper.MostrarAlerta(this, "Datos del material ingresado correctamente.");
                }
                catch (Exception ex)
                {
                    JavaScriptHelper.MostrarAlerta(this, "Error al ingresar datos :/" + ex.Message);

                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexionmam"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            using (SqlCommand comando = new SqlCommand("UPDATE MaterialReciclable SET PuntosPorUnidad = @PuntosPorUnidad, NombreMaterial=@NombreMaterial, Tipo=@Tipo WHERE NombreMaterial=@NombreMaterial", conexion))
            {
                comando.Parameters.AddWithValue("@NombreMaterial", tmateria.Text.Trim());
                comando.Parameters.AddWithValue("@PuntosPorUnidad", tpuntos.Text.Trim());
                comando.Parameters.AddWithValue("@Tipo", tipo.Text.Trim());

                try
                {
                    conexion.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();
                    LlenarGrid();

                    if (filasAfectadas > 0)
                        JavaScriptHelper.MostrarAlerta(this, "Datos de Materias Modificados correctamente.");
                    else
                        JavaScriptHelper.MostrarAlerta(this, "No se encontró ese material.");
                }
                catch (Exception ex)
                {
                    JavaScriptHelper.MostrarAlerta(this, "Error al modificar  " + ex.Message);
                }
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexionmam"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            using (SqlCommand comando = new SqlCommand("SELECT * FROM MaterialReciclable WHERE NombreMaterial = @NombreMaterial", conexion))
            {
                comando.Parameters.AddWithValue("@NombreMaterial", tmateria.Text.Trim());

                try
                {
                    conexion.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comando);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        JavaScriptHelper.MostrarAlerta(this, "Resultado encontrado.");
                    }
                    else
                    {
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        JavaScriptHelper.MostrarAlerta(this, "No se encontró ningún material con ese nombre.");
                    }
                }
                catch (Exception ex)
                {
                    JavaScriptHelper.MostrarAlerta(this, "Error al buscar: " + ex.Message);
                }
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexionmam"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            using (SqlCommand comando = new SqlCommand("DELETE FROM MaterialReciclable WHERE NombreMaterial = @NombreMaterial", conexion))
            {
                comando.Parameters.AddWithValue("@NombreMaterial", tmateria.Text.Trim());

                try
                {
                    conexion.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();
                    LlenarGrid();

                    if (filasAfectadas > 0)
                        JavaScriptHelper.MostrarAlerta(this, "datos de material eliminados correctamente.");
                    else
                        JavaScriptHelper.MostrarAlerta(this, "No se encontró el material con ese nombre.");
                }
                catch (Exception ex)
                {
                    JavaScriptHelper.MostrarAlerta(this, "Error al eliminar  " + ex.Message);
                }
            }
        }
    }
}