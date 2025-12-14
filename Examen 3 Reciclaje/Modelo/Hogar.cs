using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Examen_3_Reciclaje.Modelo
{
   public class Hogar
    {
        public abstract class RepositorioBase
        {
            public string Nombre { get; set; }
            public string Correo { get; set; }
            public string NumeroCasa { get; set; }
        }
    }
}