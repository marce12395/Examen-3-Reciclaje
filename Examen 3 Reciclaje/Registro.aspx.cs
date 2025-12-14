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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarUsuarios();
                LlenarMateriales();
                LlenarGrid();
            }

        }
        public static class JavaScriptHelper
        {
            public static void MostrarAlerta(Page page, string message)
            {
                string script = $"<script type='text/javascript'>alert('{message}');</script>";
                ClientScriptManager cs = page.ClientScript;
                cs.RegisterStartupScript(page.GetType(), "AlertScript", script);
            }
        }

        protected void LlenarGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["conexionmam"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM RegistroReciclaje", con))
            {
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    GridView1.DataSource = rdr;
                    GridView1.DataBind();
                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexionmam"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
 
                decimal puntosUnitarios = 0;
                using (SqlCommand cmdPuntos = new SqlCommand("SELECT PuntosPorUnidad FROM MaterialReciclable WHERE MaterialID = @MaterialID", conexion))
                {
                    cmdPuntos.Parameters.AddWithValue("@MaterialID", ddlMaterial.SelectedValue);
                    conexion.Open();
                    object result = cmdPuntos.ExecuteScalar();
                    conexion.Close();

                    if (result != null)
                        puntosUnitarios = Convert.ToDecimal(result);
                    else
                    {
                        JavaScriptHelper.MostrarAlerta(this, "No se encontró el material seleccionado.");
                        return;
                    }
                }

                if (!decimal.TryParse(tcantidad.Text.Trim(), out decimal cantidad))
                {
                    JavaScriptHelper.MostrarAlerta(this, "Ingrese un valor numérico válido para la cantidad.");
                    return;
                }

                decimal puntosDelRegistro = cantidad * puntosUnitarios;
                using (SqlCommand comando = new SqlCommand(
                    "UPDATE RegistroReciclaje " +
                    "SET Cantidad = @Cantidad, PuntosUnitarios = @PuntosUnitarios, PuntosTotales = @PuntosTotales " +
                    "WHERE RegistroID = @RegistroID", conexion))
                {
                    comando.Parameters.AddWithValue("@Cantidad", cantidad);
                    comando.Parameters.AddWithValue("@PuntosUnitarios", puntosUnitarios);
                    comando.Parameters.AddWithValue("@PuntosTotales", puntosDelRegistro);

                    
                    comando.Parameters.AddWithValue("@RegistroID", tidregistro.Text.Trim());

                    try
                    {
                        conexion.Open();
                        int filas = comando.ExecuteNonQuery();
                        conexion.Close();

                        if (filas > 0)
                        {
                            LlenarGrid();
                            JavaScriptHelper.MostrarAlerta(this, "Registro modificado correctamente.");
                        }
                        else
                        {
                            JavaScriptHelper.MostrarAlerta(this, "No se encontró el registro a modificar.");
                        }
                    }
                    catch (Exception ex)
                    {
                        JavaScriptHelper.MostrarAlerta(this, "Error al modificar registro: " + ex.Message);
                    }
                }
            }


        }

        




        private void LlenarUsuarios()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexionmam"].ConnectionString;
            using (SqlConnection conexion = new SqlConnection(connectionString))
            using (SqlCommand comando = new SqlCommand("SELECT UsuarioID, Nombre FROM Hogar", conexion))
            {
                conexion.Open();
                SqlDataReader rdr = comando.ExecuteReader();
                ddlUsuario.DataSource = rdr;
                ddlUsuario.DataTextField = "Nombre";       
                ddlUsuario.DataValueField = "UsuarioID";   
                ddlUsuario.DataBind();
            }
        }

        private void LlenarMateriales()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexionmam"].ConnectionString;
            using (SqlConnection conexion = new SqlConnection(connectionString))
            using (SqlCommand comando = new SqlCommand("SELECT MaterialID, NombreMaterial FROM MaterialReciclable", conexion))
            {
                conexion.Open();
                SqlDataReader rdr = comando.ExecuteReader();
                ddlMaterial.DataSource = rdr;
                ddlMaterial.DataTextField = "NombreMaterial";   
                ddlMaterial.DataValueField = "MaterialID";      
                ddlMaterial.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexionmam"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
     
                decimal puntosUnitarios = 0;
                using (SqlCommand cmdPuntos = new SqlCommand("SELECT PuntosPorUnidad FROM MaterialReciclable WHERE MaterialID = @MaterialID", conexion))
                {
                    cmdPuntos.Parameters.AddWithValue("@MaterialID", ddlMaterial.SelectedValue);
                    conexion.Open();
                    object result = cmdPuntos.ExecuteScalar();
                    conexion.Close();

                    if (result != null)
                        puntosUnitarios = Convert.ToDecimal(result);
                    else
                    {
                        JavaScriptHelper.MostrarAlerta(this, "No se encontró el material seleccionado.");
                        return;
                    }
                }

                if (!decimal.TryParse(tcantidad.Text.Trim(), out decimal cantidad))
                {
                    JavaScriptHelper.MostrarAlerta(this, "Ingrese un valor numérico válido para la cantidad.");
                    return;
                }

                decimal puntosDelRegistro = cantidad * puntosUnitarios;
                using (SqlCommand comando = new SqlCommand(
                    "INSERT INTO RegistroReciclaje (UsuarioID, MaterialID, Cantidad, PuntosUnitarios, PuntosTotales) " +
                    "VALUES (@UsuarioID, @MaterialID, @Cantidad, @PuntosUnitarios, @PuntosTotales)", conexion))
                {
                    comando.Parameters.AddWithValue("@UsuarioID", ddlUsuario.SelectedValue);
                    comando.Parameters.AddWithValue("@MaterialID", ddlMaterial.SelectedValue);
                    comando.Parameters.AddWithValue("@Cantidad", cantidad);
                    comando.Parameters.AddWithValue("@PuntosUnitarios", puntosUnitarios);
                    comando.Parameters.AddWithValue("@PuntosTotales", puntosDelRegistro);

                    try
                    {
                        conexion.Open();
                        comando.ExecuteNonQuery();
                        LlenarGrid();
                        JavaScriptHelper.MostrarAlerta(this, "Registro ingresado correctamente.");
                    }
                    catch (Exception ex)
                    {
                        JavaScriptHelper.MostrarAlerta(this, "Error al ingresar registro: " + ex.Message);
                    }
                }
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["conexionmam"].ConnectionString;

            using (SqlConnection conexion = new SqlConnection(connectionString))
            using (SqlCommand comando = new SqlCommand("SELECT * FROM RegistroReciclaje WHERE RegistroID = @RegistroID", conexion))
            {
                comando.Parameters.AddWithValue("@RegistroID", tidregistro.Text.Trim());

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
            using (SqlCommand comando = new SqlCommand("DELETE FROM RegistroReciclaje WHERE RegistroID = @RegistroID", conexion))
            {
                comando.Parameters.AddWithValue("@RegistroID", tidregistro.Text.Trim());

                try
                {
                    conexion.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();
                    LlenarGrid();

                    if (filasAfectadas > 0)
                        JavaScriptHelper.MostrarAlerta(this, "datos del registro eliminados correctamente.");
                    else
                        JavaScriptHelper.MostrarAlerta(this, "No se encontró el registro con ese nombre.");
                }
                catch (Exception ex)
                {
                    JavaScriptHelper.MostrarAlerta(this, "Error al eliminar  " + ex.Message);
                }
            }
        }
    }
}