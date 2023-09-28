using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

// MongoDB

using MongoDB.Driver;

namespace Banco_CodigoLimpio.BaseDeDatos
{
    public static class BaseDeDatos_Gestor
    {
        public static IMongoDatabase ObtenerBaseDeDatos()
        {
            // Obteniendo la conexión.
            var localhost = new MongoClient(Configuracion.localhost_DB);
            var database = localhost.GetDatabase(Configuracion.BaseDeDatos);

            return database;
        }

    }
}
