using Banco_CodigoLimpio.BaseDeDatos;
using Banco_CodigoLimpio.Menus;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable CS8604 // Posible argumento de referencia nulo

namespace Banco_CodigoLimpio.Clases
{
    public class Usuario
    {
      
        public void ver_historial_movimientos(Usuario_DB Usuario_App)
        {
            Usuario_DB? Usuario = Usuario_DB.ObtenerUsuario_byName_DB(Usuario_App.Nombre);

            List<Movimiento_DB> Movimientos_Usuario = Usuario.Movimientos;

            foreach(Movimiento_DB movimiento in Movimientos_Usuario) 
            {
                Console.WriteLine("-----------------------------------------------");

                string fecha = movimiento.Fecha;
                float saldoActual_CuentaAhorroUsuario = movimiento.SaldoActual_CuentaAhorroUsuario;
                float saldoFinal_CuentaAhorroUsuario = movimiento.SaldoFinal_CuentaAhorroUsuario;
                float monto = movimiento.Monto;
                string tipoDeMovimiento = movimiento.TipoDeMovimiento;
                string grupo_asociado = movimiento.GrupoAsociado.Nombre;

                // Imprimiendo
                Console.WriteLine("Fecha: " + fecha);
                Console.WriteLine("Saldo Actual Cuenta Ahorro Usuario: " + saldoActual_CuentaAhorroUsuario);
                Console.WriteLine("Saldo Final Cuenta Ahorro Usuario: " + saldoFinal_CuentaAhorroUsuario);
                Console.WriteLine("Monto: " + monto);
                Console.WriteLine("Tipo de Movimiento: " + tipoDeMovimiento);
                Console.WriteLine("Grupo Asociado: " + grupo_asociado);

                Console.WriteLine("-----------------------------------------------");
            }

        }

        public void pagar_deudas(Usuario_DB Usuario_App)
        {
            Usuario_DB? Usuario = Usuario_DB.ObtenerUsuario_byName_DB(Usuario_App.Nombre);

            List<Movimiento_DB> Movimientos_Usuario = Usuario.Movimientos;

            // Asignando las variables necesarias:

            List<float> valores_movimientos = new List<float>();

            int numero_de_meses_a_pagar = 0;

            float deuda = 0;

            string hora_actual = DateTime.Now.ToString("HH:mm:ss");

            if (Movimientos_Usuario.Count != 0) 
            {
                foreach (Movimiento_DB movimiento in Movimientos_Usuario)
                {
                    if (movimiento.TipoDeMovimiento == "PrestamoGrupoPropioAhorro" || movimiento.TipoDeMovimiento == "PrestamoOtrosGruposAhorro")
                    { 
                        // Parsea las cadenas de tiempo en objetos DateTime
                        DateTime tiempo1 = DateTime.ParseExact(movimiento.Fecha, "HH:mm:ss", null);
                        DateTime tiempo2 = DateTime.ParseExact(hora_actual, "HH:mm:ss", null);

                        TimeSpan diferencia = tiempo2 - tiempo1;

                        int tiempo_transcurrido = diferencia.Minutes;

                        if (movimiento.TipoDeMovimiento == "PrestamoGrupoPropioAhorro")
                        {
                            int tasa_de_interes = Configuracion.tasa_de_interes_grupo_usuario;

                            float valorDecimal = (tasa_de_interes * tiempo_transcurrido) / 100;

                            float deuda_movimiento = movimiento.Monto * valorDecimal;

                            // Actualizando variables iniciales.

                            valores_movimientos.Add(deuda_movimiento);

                        }
                        if (movimiento.TipoDeMovimiento == "PrestamoOtrosGruposAhorro")
                        {
                            int tasa_de_interes = Configuracion.tasa_de_interes_otro_grupo;

                            float valorDecimal = (tasa_de_interes * tiempo_transcurrido) / 100;

                            float deuda_movimiento = movimiento.Monto * valorDecimal;

                            // Actualizando variables iniciales.

                            valores_movimientos.Add(deuda_movimiento);
                        }

                        numero_de_meses_a_pagar += tiempo_transcurrido;
                    }
                    else 
                    {
                        Console.WriteLine("El movimiento no es un prestamo.");
                    }
                }

                foreach (float valor_deuda in valores_movimientos)
                {
                    deuda += valor_deuda;
                }

                /////////////////////////////////////////////////

                Console.WriteLine("Tu deuda es: " + deuda);
                Console.WriteLine("El numero de meses a pagar es: " + numero_de_meses_a_pagar);
            }
            else
            {
                Console.WriteLine("No tienes prestamos a pagar.");
            }
        }

    }
}

