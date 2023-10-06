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

        // Configuracion Prestamos.
        public static int min_plazo_de_pago = 2;

        public static int tasa_de_interes_grupo_usuario = 3;
        public static int tasa_de_interes_otro_grupo = 5;

    }
}
