using Examen_3_Reciclaje.Interfaz;
using Examen_3_Reciclaje.Modelo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using static Examen_3_Reciclaje.Modelo.Hogar;

namespace Examen_3_Reciclaje.Repositorio
{
    // HERENCIA: repositoriohogar hereda de RepositorioBase
    public class repositoriohogar : RepositorioBase
    {
        public override void Insertar(object entidad)
        {
            var hogar = (Hogar)entidad;
            using (var con = CrearConexion())
            using (var cmd = new SqlCommand("INSERT INTO Hogar (Nombre, Correo, NumeroCasa) VALUES (@Nombre, @Correo, @NumeroCasa)", con))
            {
                cmd.Parameters.AddWithValue("@Nombre", hogar.Nombre);
                cmd.Parameters.AddWithValue("@Correo", hogar.Correo);
                cmd.Parameters.AddWithValue("@NumeroCasa", hogar.NumeroCasa);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public override bool Eliminar(string numeroCasa)
        {
            using (var con = CrearConexion())
            using (var cmd = new SqlCommand("DELETE FROM Hogar WHERE NumeroCasa=@NumeroCasa", con))
            {
                cmd.Parameters.AddWithValue("@NumeroCasa", numeroCasa);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public override bool Modificar(object entidad)
        {
            var hogar = (Hogar)entidad;
            using (var con = CrearConexion())
            using (var cmd = new SqlCommand("UPDATE Hogar SET Correo=@Correo WHERE NumeroCasa=@NumeroCasa", con))
            {
                cmd.Parameters.AddWithValue("@Correo", hogar.Correo);
                cmd.Parameters.AddWithValue("@NumeroCasa", hogar.NumeroCasa);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public override object Buscar(string nombre)
        {
            using (var con = CrearConexion())
            using (var cmd = new SqlCommand("SELECT * FROM Hogar WHERE Nombre=@Nombre", con))
            {
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                con.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return new Hogar
                        {
                            Nombre = rdr["Nombre"].ToString(),
                            Correo = rdr["Correo"].ToString(),
                            NumeroCasa = rdr["NumeroCasa"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public override object ObtenerTodos()
        {
            var hogares = new List<Hogar>();
            using (var con = CrearConexion())
            using (var cmd = new SqlCommand("SELECT * FROM Hogar", con))
            {
                con.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        hogares.Add(new Hogar
                        {
                            Nombre = rdr["Nombre"].ToString(),
                            Correo = rdr["Correo"].ToString(),
                            NumeroCasa = rdr["NumeroCasa"].ToString()
                        });
                    }
                }
            }
            return hogares;
        }
    }
}