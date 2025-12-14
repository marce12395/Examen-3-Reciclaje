using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Examen_3_Reciclaje.Repositorio
{
    // HERENCIA: clase base para repositorios
    public abstract class RepositorioBase
    {
        protected readonly string _connectionString;

        public RepositorioBase()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["conexionmam"].ConnectionString;
        }

        protected SqlConnection CrearConexion()
        {
            return new SqlConnection(_connectionString);
        }

        // POLIMORFISMO: cada repositorio implementa estos métodos de forma distinta
        public abstract void Insertar(object entidad);
        public abstract bool Eliminar(string clave);
        public abstract bool Modificar(object entidad);
        public abstract object Buscar(string criterio);
        public abstract object ObtenerTodos();
    }
}