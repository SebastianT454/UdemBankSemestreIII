using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Banco_CodigoLimpio.BaseDeDatos
{
    public class Movimiento_DB
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("fecha")]
        public string Fecha { get; set; }

        [BsonElement("saldo_actual")]
        public float SaldoActual_CuentaAhorroUsuario { get; set; }

        [BsonElement("saldo_final")]
        public float SaldoFinal_CuentaAhorroUsuario { get; set; }

        [BsonElement("monto")]
        public float Monto { get; set; }

        [BsonElement("tipo_de_movimiento")]
        public string TipoDeMovimiento { get; set; }

        [BsonElement("grupo_asociado")]
        public GrupoAhorro_DB GrupoAsociado { get; set; }

        public Movimiento_DB(string fecha, float saldoActual_CuentaAhorroUsuario, float saldoFinal_CuentaAhorroUsuario, float monto, string tipoDeMovimiento, GrupoAhorro_DB grupo_asociado)
        {
            Fecha = fecha;
            SaldoActual_CuentaAhorroUsuario = saldoActual_CuentaAhorroUsuario;
            SaldoFinal_CuentaAhorroUsuario = saldoFinal_CuentaAhorroUsuario;
            TipoDeMovimiento = tipoDeMovimiento;
            Monto = monto;
            GrupoAsociado = grupo_asociado;
        }

        public static IMongoCollection<Movimiento_DB> Obtener_CollecionMovimiento()
        {
            // Obteniendo la base de datos.
            var database = BaseDeDatos.BaseDeDatos_Gestor.ObtenerBaseDeDatos();

            // Obteniendo la base de datos de la COLECCION.
            var Movimiento_Collection = database.GetCollection<Movimiento_DB>("Movimiento");

            return Movimiento_Collection;
        }
        public static List<Movimiento_DB> Obtener_Movimientos()
        {
            // Obteniendo la base de datos de la COLECCION.
            var Movimiento_Collection = Obtener_CollecionMovimiento();

            // Obteniendo todos los movimientos de la coleccion.

            List<Movimiento_DB> lista_movimientos = Movimiento_Collection.Find(d => true).ToList();

            return lista_movimientos;

        }
        public static Movimiento_DB CrearMovimiento(string fecha, float saldoActual_CuentaAhorroUsuario, float saldoFinal_CuentaAhorroUsuario, float monto, string tipoDeMovimiento, GrupoAhorro_DB grupo_asociado)
        {
            // Obteniendo la base de datos de la COLECCION.
            var Movimiento_Collection = Obtener_CollecionMovimiento();

            // Generando el movimiento en la base de datos.
            var movimiento = new Movimiento_DB(fecha, saldoActual_CuentaAhorroUsuario, saldoFinal_CuentaAhorroUsuario, monto, tipoDeMovimiento, grupo_asociado);

            Movimiento_Collection.InsertOne(movimiento);

            return movimiento;
        }

        public static List<Movimiento_DB> ObtenerMovimientos_usuario(Usuario_DB usuario_asociado)
        {
            List<Movimiento_DB> Movimientos_usuario = usuario_asociado.Movimientos;

            return Movimientos_usuario;
        }

    }
}
