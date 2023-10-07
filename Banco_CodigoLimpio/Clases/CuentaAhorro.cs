using Banco_CodigoLimpio.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco_CodigoLimpio.Clases
{
    public static class CuentaAhorro
    {

        public static void ingresar_capital_cuenta_ahorro_usuario(Usuario_DB Usuario, float numero)
        {
            float NuevoSaldo_CuentaAhorro_Usuario = Usuario.CuentaAhorro.Saldo + numero;

            Usuario_DB.ActualizarSaldo_Usuario(Usuario, NuevoSaldo_CuentaAhorro_Usuario);
        }
        

        public static void realizar_transferencia()
        {

        }

    }
}
