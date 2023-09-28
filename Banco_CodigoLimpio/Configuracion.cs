using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_CodigoLimpio
{
    public static class Configuracion
    {
        // Configuracion Base de datos.
        public static string localhost_DB = "mongodb://localhost:27017";
        public static string BaseDeDatos = "UdemBank";

        // Configuracion Grupos de Ahorro.
        public static int max_grupos_ahorro_por_usuario = 3;
    }
}
