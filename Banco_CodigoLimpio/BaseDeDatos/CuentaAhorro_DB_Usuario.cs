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
    public class CuentaAhorro_DB_Usuario 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("saldo")]
        public float Saldo { get; set; }

        public CuentaAhorro_DB_Usuario()
        {
            Saldo = new Random().Next(10000);
        }

        public static IMongoCollection<CuentaAhorro_DB_Usuario> Obtener_CollecionCuentaAhorro()
        {
            // Obteniendo la base de datos.
            var database = BaseDeDatos.BaseDeDatos_Gestor.ObtenerBaseDeDatos();

            // Obteniendo la base de datos de la COLECCION.
            var CuentaAhorro_Collection = database.GetCollection<CuentaAhorro_DB_Usuario>("CuentaAhorro");

            return CuentaAhorro_Collection;
        }

        public static List<CuentaAhorro_DB_Usuario> Obtener_CuentasAhorro()
        {
            // Obteniendo la base de datos de la COLECCION.
            var CuentaAhorro_Collection = Obtener_CollecionCuentaAhorro();

            // Obteniendo todos las cuentas de ahorro en la coleccion.

            List<CuentaAhorro_DB_Usuario> lista_CuentaAhorro = CuentaAhorro_Collection.Find(d => true).ToList();

            return lista_CuentaAhorro;
        }
        public static void CrearCuentaAhorro_Usuario(Usuario_DB usuario_propietario)
        {
            // Obteniendo la base de datos de la COLECCION de las cuenta de Ahorro.
            var CuentaAhorro_Collection = Obtener_CollecionCuentaAhorro();

            // Obteniendo la base de datos de la COLECCION de los usuarios.
            var Usuarios_Collection = Usuario_DB.Obtener_CollecionUsuarios();

            // Generando la cuenta de ahorro en la base de datos.
            var CuentaAhorro_usuario = new CuentaAhorro_DB_Usuario();

            CuentaAhorro_Collection.InsertOne(CuentaAhorro_usuario);

            var filter_usuario = Builders<Usuario_DB>.Filter.Eq(u => u.Id, usuario_propietario.Id);
            var update_usuario = Builders<Usuario_DB>.Update.Set(u => u.CuentaAhorro, CuentaAhorro_usuario);

            var result_usuario = Usuarios_Collection.UpdateOne(filter_usuario, update_usuario);

        }

        public static CuentaAhorro_DB_Usuario ObtenerCuentaAhorro_Usuario(Usuario_DB usuario_asociado)
        {
            CuentaAhorro_DB_Usuario CuentaDeAhorro = usuario_asociado.CuentaAhorro;

            return CuentaDeAhorro;
        }

        public static string ObtenerIdCuentaAhorro(Usuario_DB usuario_asociado)
        {
            CuentaAhorro_DB_Usuario CuentaDeAhorro = usuario_asociado.CuentaAhorro;

            if (CuentaDeAhorro != null)
            {
                return CuentaDeAhorro.Id;
            }
            else
            {
                return null; // Opcional: Puedes manejar el caso en el que no haya cuenta de ahorro.
            }
        }

        public static float ObtenerCapitalCuentaAhorro(Usuario_DB usuario_asociado)
        {
            CuentaAhorro_DB_Usuario CuentaDeAhorro = usuario_asociado.CuentaAhorro;

            if (CuentaDeAhorro != null)
            {
                return CuentaDeAhorro.Saldo;
            }
            else
            {
                return 0.0f; // Opcional: Puedes manejar el caso en el que no haya cuenta de ahorro.
            }
        }

        public static string ObtenerNombreUsuario(Usuario_DB usuario_asociado)
        {
            return usuario_asociado.Nombre;
        }

    }
}
