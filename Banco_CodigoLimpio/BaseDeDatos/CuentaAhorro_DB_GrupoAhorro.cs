using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banco_CodigoLimpio.Clases;

// MongoDB

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Banco_CodigoLimpio.BaseDeDatos
{
    public class CuentaAhorro_DB_GrupoAhorro
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("saldo")]
        public float Saldo { get; set; }

        public CuentaAhorro_DB_GrupoAhorro()
        {
            Saldo = 0;
        }

        public static IMongoCollection<CuentaAhorro_DB_GrupoAhorro> Obtener_CollecionCuentaAhorro()
        {
            // Obteniendo la base de datos.
            var database = BaseDeDatos.BaseDeDatos_Gestor.ObtenerBaseDeDatos();

            // Obteniendo la base de datos de la COLECCION.
            var CuentaAhorro_Collection = database.GetCollection<CuentaAhorro_DB_GrupoAhorro>("CuentaAhorro");

            return CuentaAhorro_Collection;
        }

        public static List<CuentaAhorro_DB_GrupoAhorro> Obtener_CuentasAhorro()
        {
            // Obteniendo la base de datos de la COLECCION.
            var CuentaAhorro_Collection = Obtener_CollecionCuentaAhorro();

            // Obteniendo todos las cuentas de ahorro en la coleccion.

            List<CuentaAhorro_DB_GrupoAhorro> lista_CuentaAhorro = CuentaAhorro_Collection.Find(d => true).ToList();

            return lista_CuentaAhorro;
        }
        public static void CrearCuentaAhorro_GrupoAhorro(GrupoAhorro_DB grupo_ahorro)
        {
            // Obteniendo la base de datos de la COLECCION de las cuenta de Ahorro.
            var CuentaAhorro_Collection = Obtener_CollecionCuentaAhorro();

            // Obteniendo la base de datos de la COLECCION de los grupos de Ahorro.
            var GrupoAhorro_Collection = GrupoAhorro_DB.Obtener_CollecionGrupoAhorro();

            // Generando la cuenta de ahorro en la base de datos.
            var CuentaAhorro = new CuentaAhorro_DB_GrupoAhorro();

            CuentaAhorro_Collection.InsertOne(CuentaAhorro);

            // Actualizando la lista de cuentas de ahorro del usuario.
            var filter = Builders<GrupoAhorro_DB>.Filter.Eq(g => g.Id, grupo_ahorro.Id);
            var update = Builders<GrupoAhorro_DB>.Update.Set(g => g.CuentaAhorro, CuentaAhorro);

            var result = GrupoAhorro_Collection.UpdateOne(filter, update);

        }
        public static CuentaAhorro_DB_GrupoAhorro? ObtenerCuentaAhorro_GrupoAhorro(GrupoAhorro_DB Grupo_Ahorro)
        {
            CuentaAhorro_DB_GrupoAhorro CuentaDeAhorro = Grupo_Ahorro.CuentaAhorro;

            return CuentaDeAhorro;
        }

        public void CalcularTotalGanancias()
        {

        }

    }
}
