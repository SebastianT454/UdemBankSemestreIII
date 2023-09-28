﻿using Banco_CodigoLimpio.BaseDeDatos;
using Banco_CodigoLimpio.Menus;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Banco_CodigoLimpio.Clases
{
    public class Usuario
    {

        public void ingresar_capital(Usuario_DB Usuario_App)
        {
            // Utilizar el filtro para encontrar el usuario en la colección
            Usuario_DB? Usuario = Usuario_DB.ObtenerUsuario_byName_DB(Usuario_App.Nombre);

            float numero;
            float NuevoSaldo_CuentaAhorro_Usuario;

            Console.Write("Ingrese un número: ");
            string entrada = Console.ReadLine();

            if (float.TryParse(entrada, out numero))
            {
                NuevoSaldo_CuentaAhorro_Usuario = Usuario.CuentaAhorro.Saldo + numero;
            }
            else
            {
                return;
            }

            Usuario_DB.ActualizarSaldo_Usuario(Usuario, NuevoSaldo_CuentaAhorro_Usuario);
        }

        public void ver_historial_movimientos()
        {

        }

        public void pagar_deudas()
        {

        }

    }

}
