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
    public class Movimiento_DB
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("fecha")]
        public string Fecha { get; set; }

        [BsonElement("saldo_actual")]
        public int SaldoActual { get; set; }

        [BsonElement("tipo_de_movimiento")]
        public string TipoDeMovimiento { get; set; }

        [BsonElement("monto")]
        public int Monto { get; set; }

        [BsonElement("saldo_final")]
        public int SaldoFinal { get; set; }

        public Movimiento_DB(string fecha, int saldoActual, string tipoDeMovimiento, int monto, int saldoFinal)
        {
            Fecha = fecha;
            SaldoActual = saldoActual;
            TipoDeMovimiento = tipoDeMovimiento;
            Monto = monto;
            SaldoFinal = saldoFinal;
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
        public static void CrearMovimiento(string fecha, int saldoActual, string tipoDeMovimiento, int monto, int saldoFinal)
        {
            // Obteniendo la base de datos de la COLECCION.
            var Movimiento_Collection = Obtener_CollecionMovimiento();

            // Generando el movimiento en la base de datos.
            var movimiento = new Movimiento_DB(fecha,saldoActual,tipoDeMovimiento,monto,saldoFinal);

            Movimiento_Collection.InsertOne(movimiento);
        }

        public static List<Movimiento_DB> ObtenerMovimientos_usuario(Usuario_DB usuario_asociado)
        {
            List<Movimiento_DB> Movimientos_usuario = usuario_asociado.Movimientos;

            return Movimientos_usuario;
        }

    }
}
