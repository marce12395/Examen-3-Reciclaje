using Examen_3_Reciclaje.Repositorio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using HogarEntidad = Examen_3_Reciclaje.Modelo.Hogar;

namespace Examen_3_Reciclaje
{
    public partial class Hogar : System.Web.UI.Page
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
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Hogar", con))
            {
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    GridView1.DataSource = rdr;
                    GridView1.DataBind();
                }
            }
        }

        protected void bnombre_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexionmam"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            using (SqlCommand comando = new SqlCommand(
                "INSERT INTO Hogar (Nombre, Correo, NumeroCasa) " +
                "VALUES (@Nombre, @Correo, @NumeroCasa)", conexion))
            {

                comando.Parameters.AddWithValue("@Nombre", tnombre.Text.Trim());
                comando.Parameters.AddWithValue("@Correo", tcorreo.Text.Trim());
                comando.Parameters.AddWithValue("@NumeroCasa", tubicacion.Text.Trim());


                try
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    LlenarGrid();
                    JavaScriptHelper.MostrarAlerta(this, "Datos del hogar ingresado correctamente.");
                }
                catch (Exception ex)
                {
                    JavaScriptHelper.MostrarAlerta(this, "Error al ingresar datos :/" + ex.Message);

                }
            }
        }



        protected void beliminar_Click(object sender, EventArgs e)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["conexionmam"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            using (SqlCommand comando = new SqlCommand("DELETE FROM Hogar WHERE NumeroCasa = @NumeroCasa", conexion))
            {
                comando.Parameters.AddWithValue("@NumeroCasa", tubicacion.Text.Trim());

                try
                {
                    conexion.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();
                    LlenarGrid();

                    if (filasAfectadas > 0)
                        JavaScriptHelper.MostrarAlerta(this, "datos de hogar eliminados correctamente.");
                    else
                        JavaScriptHelper.MostrarAlerta(this, "No se encontró la casa con ese número.");
                }
                catch (Exception ex)
                {
                    JavaScriptHelper.MostrarAlerta(this, "Error al eliminar  " + ex.Message);
                }
            }


        }

        protected void bmodificar_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexionmam"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            using (SqlCommand comando = new SqlCommand("UPDATE Hogar SET NumeroCasa = @NumeroCasa, Nombre=@Nombre, Correo=@Correo WHERE Nombre=@Nombre", conexion))
            {
                comando.Parameters.AddWithValue("@Nombre", tnombre.Text.Trim());
                comando.Parameters.AddWithValue("@Correo", tcorreo.Text.Trim());
                comando.Parameters.AddWithValue("@NumeroCasa", tubicacion.Text.Trim());

                try
                {
                    conexion.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();
                    LlenarGrid();

                    if (filasAfectadas > 0)
                        JavaScriptHelper.MostrarAlerta(this, "Datos de hogar Modificados correctamente.");
                    else
                        JavaScriptHelper.MostrarAlerta(this, "No se encontró la persona con ese nombre.");
                }
                catch (Exception ex)
                {
                    JavaScriptHelper.MostrarAlerta(this, "Error al modificar  " + ex.Message);
                }
            }

        }

        protected void bbuscar_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexionmam"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            using (SqlCommand comando = new SqlCommand("SELECT * FROM Hogar WHERE Nombre = @Nombre", conexion))
            {
                comando.Parameters.AddWithValue("@Nombre", tnombre.Text.Trim());

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
                        JavaScriptHelper.MostrarAlerta(this, "No se encontró ningún hogar con ese nombre.");
                    }
                }
                catch (Exception ex)
                {
                    JavaScriptHelper.MostrarAlerta(this, "Error al buscar: " + ex.Message);
                }
            }
        }
    }
}