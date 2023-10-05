using Banco_CodigoLimpio.BaseDeDatos;
using Banco_CodigoLimpio.Menus;
using Microsoft.VisualBasic;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Banco_CodigoLimpio.Clases
{
    public class Movimiento
    {
        public static void crear(Usuario_DB Usuario, GrupoAhorro_DB GrupoAhorro, float Monto, string TipoDeMovimiento)
        {
            // Obteniendo la base de datos de la COLECCION de los movimientos.
            var Movimiento_Collection = Movimiento_DB.Obtener_CollecionMovimiento();

            // Obteniendo la base de datos de la COLECCION de los usuarios.
            var Usuarios_Collection = Usuario_DB.Obtener_CollecionUsuarios();

            // Determinando cada valor para el movimiento.

            string fecha = DateTime.Now.ToString("HH:mm:ss");
            float saldoActual_CuentaAhorroUsuario = Usuario.CuentaAhorro.Saldo;
            float saldoFinal_CuentaAhorroUsuario = Usuario.CuentaAhorro.Saldo + Monto;
            float monto = Monto;
            string tipoDeMovimiento = TipoDeMovimiento;
            GrupoAhorro_DB grupo_asociado = GrupoAhorro;

            // Generando el movimiento en la base de datos.
            var movimiento = Movimiento_DB.CrearMovimiento(fecha, saldoActual_CuentaAhorroUsuario, saldoFinal_CuentaAhorroUsuario, monto, tipoDeMovimiento, grupo_asociado);

            var filter_usuario = Builders<Usuario_DB>.Filter.Eq(u => u.Id, Usuario.Id);
            var update_usuario = Builders<Usuario_DB>.Update.Push(u => u.Movimientos, movimiento);

            var result_usuario = Usuarios_Collection.UpdateOne(filter_usuario, update_usuario);

            Console.WriteLine("Movimiento generado.");
        }
        public void ImprimirMovimiento()
        {

        }
    }
}
