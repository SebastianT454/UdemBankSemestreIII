using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banco_CodigoLimpio.Clases;

namespace Banco_CodigoLimpio.BaseDeDatos
{
    public class GrupoAhorro_DB
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("usuarios")]
        public List<Usuario_DB> Usuarios { get; set; }

        [BsonElement("cuenta_ahorro")]
        public CuentaAhorro_DB_GrupoAhorro? CuentaAhorro { get; set; }

        public GrupoAhorro_DB(string nombre)
        {
            Nombre = nombre;
            Usuarios = new List<Usuario_DB>();
        }

        public static IMongoCollection<GrupoAhorro_DB> Obtener_CollecionGrupoAhorro()
        {
            // Obteniendo la base de datos.
            var database = BaseDeDatos.BaseDeDatos_Gestor.ObtenerBaseDeDatos();

            // Obteniendo la base de datos de la COLECCION.
            var GrupoAhorro_Collection = database.GetCollection<GrupoAhorro_DB>("GrupoAhorro");

            return GrupoAhorro_Collection;
        }
        public static List<GrupoAhorro_DB> Obtener_GruposAhorro_DB()
        {
            // Obteniendo la base de datos de la COLECCION.
            var GrupoAhorro_Collection = Obtener_CollecionGrupoAhorro();

            // Obteniendo todos los grupos de ahorro en la coleccion.

            List<GrupoAhorro_DB> lista_GrupoAhorro = GrupoAhorro_Collection.Find(d => true).ToList();

            return lista_GrupoAhorro;

        }

        public static GrupoAhorro_DB ObtenerGrupoAhorro_DB(GrupoAhorro_DB grupo_ahorro_actualizar)
        {
            // Obteniendo la base de datos de la COLECCION.
            List<GrupoAhorro_DB> GruposAhorro = Obtener_GruposAhorro_DB();

            // Verificando si existe el usuario.

            foreach (var GrupoAhorro in GruposAhorro)
            {
                if (GrupoAhorro.Id == grupo_ahorro_actualizar.Id)
                {
                    return GrupoAhorro;
                }
            }

            return null;
        }
        public static void CrearGrupoAhorro(Usuario_DB usuario)
        {
            // Verificando si el usuario puede crear el grupo.

            Boolean usuario_puede_generar_grupo = VerificarGruposAhorro_Usuario(usuario);

            if (usuario_puede_generar_grupo == true)
            {
                // Obteniendo el nombre del gurpo de ahorro.
                string? Nombre_GrupoAhorro;

                do
                {
                    Console.WriteLine("Ingrese el nombre del grupo de ahorro: ");
                    Nombre_GrupoAhorro = Console.ReadLine();

                    Console.WriteLine("-----------------------------------------------");
                }
                while (Nombre_GrupoAhorro == "");

                // Obteniendo la base de datos de la COLECCION de los grupos de Ahorro.
                var GrupoAhorro_Collection = Obtener_CollecionGrupoAhorro();

                // Generando la cuenta de ahorro en la base de datos.
                var Grupo_Ahorro = new GrupoAhorro_DB(Nombre_GrupoAhorro);

                // Añadiendo primer usuario al grupo de ahorro.
                Grupo_Ahorro.Usuarios.Add(usuario);

                GrupoAhorro_Collection.InsertOne(Grupo_Ahorro);

                CuentaAhorro_DB_GrupoAhorro.CrearCuentaAhorro_GrupoAhorro(Grupo_Ahorro, usuario);

            }
            else
            {
                Console.WriteLine("Error: Maximo grupo de ahorros alcanzado.");
            }
        }
        public static Boolean VerificarGruposAhorro_Usuario(Usuario_DB usuario_asociado)
        {
            // Obteniendo la base de datos de la COLECCION.
            var Usuarios_Collection = Usuario_DB.Obtener_CollecionUsuarios();

            // Crear un filtro para buscar el usuario por su Id
            var filtro = Builders<Usuario_DB>.Filter.Eq(u => u.Id, usuario_asociado.Id);

            // Utilizar el filtro para encontrar el usuario en la colección
            Usuario_DB usuario_actualizado = Usuarios_Collection.Find(filtro).FirstOrDefault();

            // Obteniendo la información actualizada del usuario.

            List<GrupoAhorro_DB> GruposDeAhorro_usuario = usuario_actualizado.GruposAhorro;

            /*
            foreach (GrupoAhorro_DB Grupo_ahorro in usuario_actualizado.GruposAhorro)
            {
                Console.WriteLine(Grupo_ahorro.Nombre);
            }
            */

            if (usuario_actualizado != null && GruposDeAhorro_usuario.Count < Configuracion.max_grupos_ahorro_por_usuario)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void Actualizar_GrupoAhorro(Usuario_DB usuario, GrupoAhorro_DB Grupo_Ahorro)
        {
            // Obteniendo la base de datos de la COLECCION de los grupos de Ahorro.
            var GrupoAhorro_Collection = Obtener_CollecionGrupoAhorro();

            // Obteniendo la base de datos de la COLECCION de los usuarios.
            var Usuarios_Collection = Usuario_DB.Obtener_CollecionUsuarios();

            // Actualizando la lista de usuarios en el grupo de ahorro.
            var filter_grupoAhorro = Builders<GrupoAhorro_DB>.Filter.Eq(g => g.Id, Grupo_Ahorro.Id);
            var update_grupoAhorro = Builders<GrupoAhorro_DB>.Update.Push(g => g.Usuarios, usuario);

            var result_grupoAhorro = GrupoAhorro_Collection.UpdateOne(filter_grupoAhorro, update_grupoAhorro);

            // Actualizando la lista de grupos de ahorro del usuario.
            var filter_user = Builders<Usuario_DB>.Filter.Eq(u => u.Id, usuario.Id);
            var update_user = Builders<Usuario_DB>.Update.Push(u => u.GruposAhorro, Grupo_Ahorro);

            var result_user = Usuarios_Collection.UpdateOne(filter_user, update_user);

        }

        public static void ActualizarSaldo_GrupoAhorro(Usuario_DB usuario, GrupoAhorro_DB grupo_ahorro, float Saldo)
        {
            // Obteniendo la base de datos de la COLECCION.
            var CuentaAhorro_Collection = CuentaAhorro_DB_GrupoAhorro.Obtener_CollecionCuentaAhorro();

            // Obteniendo la base de datos de la COLECCION de los grupos de Ahorro.
            var GrupoAhorro_Collection = Obtener_CollecionGrupoAhorro();

            // Obteniendo la cuenta de ahorros del usuario.

            CuentaAhorro_DB_GrupoAhorro CuentaAhorroGrupo = grupo_ahorro.CuentaAhorro;

            // Actualizando la cuenta de ahorro del grupo.
            var filter_CuentaAhorro = Builders<CuentaAhorro_DB_GrupoAhorro>.Filter.Eq(c => c.Id, CuentaAhorroGrupo.Id);
            var update_CuentaAhorro = Builders<CuentaAhorro_DB_GrupoAhorro>.Update.Set(c => c.Saldo, Saldo);

            var result_CuentaAhorro = CuentaAhorro_Collection.UpdateOne(filter_CuentaAhorro, update_CuentaAhorro);

            // Actualizando la cuenta de ahorro en el grupo.
            var filter_CuentaAhorroGrupo = Builders<GrupoAhorro_DB>.Filter.Eq(g => g.Id, grupo_ahorro.Id);
            var update_CuentaAhorroGrupo = Builders<GrupoAhorro_DB>.Update.Set(g => g.CuentaAhorro.Saldo, Saldo);

            var result_CuentaAhorroGrupo = GrupoAhorro_Collection.UpdateOne(filter_CuentaAhorroGrupo, update_CuentaAhorroGrupo);

            // Creando el movimiento generado de esta accion.

            Movimiento.crear(usuario, grupo_ahorro, Saldo, "Transferencia");

        }

    }
}