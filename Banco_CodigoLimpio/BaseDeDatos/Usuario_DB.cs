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
    public class Usuario_DB
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("password")]
        public string Contraseña { get; set; }

        [BsonElement("cuenta_de_ahorro")]
        public CuentaAhorro_DB_Usuario? CuentaAhorro { get; set; }

        [BsonElement("grupos_de_ahorro")]
        public List<GrupoAhorro_DB> GruposAhorro { get; set; }

        [BsonElement("Movimientos")]
        public List<Movimiento_DB> Movimientos { get; set; }

        public Usuario_DB(string nombre, string contraseña)
        {
            Nombre = nombre;
            Contraseña = contraseña;
            GruposAhorro = new List<GrupoAhorro_DB>();
            Movimientos = new List<Movimiento_DB>();
        }

        public static IMongoCollection<Usuario_DB> Obtener_CollecionUsuarios()
        {
            // Obteniendo la base de datos.
            var database = BaseDeDatos.BaseDeDatos_Gestor.ObtenerBaseDeDatos();

            // Obteniendo la base de datos de la COLECCION.
            var Usuarios_Collection = database.GetCollection<Usuario_DB>("Usuario");

            return Usuarios_Collection;
        }
        public static List<Usuario_DB> Obtener_Usuarios()
        {
            // Obteniendo la base de datos de la COLECCION.
            var Usuarios_Collection = Obtener_CollecionUsuarios();

            // Obteniendo todos los usuarios en la coleccion.

            List<Usuario_DB> lista_usuarios = Usuarios_Collection.Find(d => true).ToList();

            return lista_usuarios;

        }
        public static Usuario_DB CrearUsuario(string nombre, string contraseña)
        {
            // Obteniendo la base de datos de la COLECCION.
            var Usuarios_Collection = Obtener_CollecionUsuarios();

            // Generando el usuario en la base de datos.
            var Usuario = new Usuario_DB(nombre, contraseña);

            Usuarios_Collection.InsertOne(Usuario);

            CuentaAhorro_DB_Usuario.CrearCuentaAhorro_Usuario(Usuario);

            return Usuario;
        }

        public static Usuario_DB? ObtenerUsuario_DB(string nombre, string contraseña)
        {
            // Obteniendo la base de datos de la COLECCION.
            List<Usuario_DB> Usuarios = Obtener_Usuarios();

            // Verificando si existe el usuario.

            foreach (var usuario in Usuarios)
            {
                if (usuario.Nombre == nombre || usuario.Contraseña == contraseña)
                {
                    return usuario;
                }
            }

            return null;
        }

        public static Usuario_DB? ObtenerUsuario_byName_DB(string nombre)
        {
            // Obteniendo la base de datos de la COLECCION.
            List<Usuario_DB> Usuarios = Obtener_Usuarios();

            // Verificando si existe el usuario.

            foreach (var usuario in Usuarios)
            {
                if (usuario.Nombre == nombre)
                {
                    return usuario;
                }
            }

            return null;
        }

        public static List<GrupoAhorro_DB> ObtenerGruposAhorro_usuario(string nombre)
        {
            // Obteniendo la base de datos de la COLECCION.
            List<Usuario_DB> Usuarios = Obtener_Usuarios();

            Usuario_DB? usuario_asociado = null;

            // Verificando si existe el usuario.

            foreach (var usuario in Usuarios)
            {
                if (usuario.Nombre == nombre)
                {
                    usuario_asociado = usuario;
                    break;
                }
            }

            if (usuario_asociado != null)
            {
                List<GrupoAhorro_DB> GruposDeAhorro_usuario = usuario_asociado.GruposAhorro;

                return GruposDeAhorro_usuario;
            }
            else
            {
                Console.WriteLine("No se encontro el usuario.");
                return null;
            }

        }

        public static void ActualizarSaldo_Usuario(Usuario_DB usuario, float Saldo)
        {
            // Obteniendo la base de datos de la COLECCION.
            var CuentaAhorro_Collection = CuentaAhorro_DB_Usuario.Obtener_CollecionCuentaAhorro();

            // Obteniendo la cuenta de ahorros del usuario.

            CuentaAhorro_DB_Usuario CuentaAhorrosUsuario = usuario.CuentaAhorro;

            // Actualizando la lista de grupos de ahorro del usuario.
            var filter = Builders<CuentaAhorro_DB_Usuario>.Filter.Eq(c => c.Id, CuentaAhorrosUsuario.Id);
            var update = Builders<CuentaAhorro_DB_Usuario>.Update.Set(c => c.Saldo, Saldo);

            var result = CuentaAhorro_Collection.UpdateOne(filter, update);

        }

    }
}
