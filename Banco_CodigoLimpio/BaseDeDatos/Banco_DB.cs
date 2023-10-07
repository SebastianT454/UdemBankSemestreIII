using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_CodigoLimpio.BaseDeDatos
{
    public class Banco_DB
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nombre_banco")]
        public string Nombre_Banco { get; set; }

        [BsonElement("comision")]
        public float Comision { get; set; }

        public Banco_DB()
        {
            Nombre_Banco = "UdemBank";
            Comision = 0;
        }

        public static IMongoCollection<Banco_DB> Obtener_CollecionBanco()
        {
            // Obteniendo la base de datos.
            var database = BaseDeDatos.BaseDeDatos_Gestor.ObtenerBaseDeDatos();

            // Obteniendo la base de datos de la COLECCION.
            var Banco_Collection = database.GetCollection<Banco_DB>("Banco");

            return Banco_Collection;
        }

        public static Banco_DB CrearBanco()
        {
            // Obteniendo la base de datos de la COLECCION.
            var Banco_Collection = Obtener_CollecionBanco();

            // Generando el banco en la base de datos.
            var banco = new Banco_DB();

            Banco_Collection.InsertOne(banco);

            return banco;
        }

        public static float ObtenerComisionTotal(Banco_DB banco)
        {
            float ComisionTotal = banco.Comision;

            return ComisionTotal;
        }

        public static void Ingresar_Comision(Banco_DB banco, float comision)
        {
            // Obteniendo la base de datos de la COLECCION.
            var Banco_Collection = Obtener_CollecionBanco();

            // Actualizando la lista de usuarios en el grupo de ahorro.
            var filter = Builders<Banco_DB>.Filter.Eq(b => b.Id, banco.Id);
            var update = Builders<Banco_DB>.Update.Set(b => b.Comision, comision);

            var result = Banco_Collection.UpdateOne(filter, update);

        }

        public static Banco_DB Obtener_banco()
        {
            // Obteniendo la base de datos de la COLECCION.
            var Banco_Collection = Obtener_CollecionBanco();

            List<Banco_DB> lista_Banco = Banco_Collection.Find(d => true).ToList();

            // verificando existencia.

            if(lista_Banco.Count == 0)
            {
                Banco_DB banco = CrearBanco();

                return banco;
            }
            else
            {
                return lista_Banco[0];
            }

        }

    }
}
