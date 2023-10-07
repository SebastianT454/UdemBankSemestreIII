using Banco_CodigoLimpio.BaseDeDatos;
using Banco_CodigoLimpio.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8604 // Posible argumento de referencia nulo

namespace Banco_CodigoLimpio.Menus
{
    public static class MenuClases
    {
        public static void Menu_Usuario(Usuario_DB Usuario)
        {
            Console.WriteLine("-- OPCIONES USUARIO --");
            Console.WriteLine("1. Crear Grupo de Ahorro");
            Console.WriteLine("2. Invitar Usuario al Grupo de Ahorro");
            Console.WriteLine("3. Mi cuenta de ahorro");
            Console.WriteLine("4. Mis grupos de ahorro");
            Console.WriteLine("5. Ingresar capital Grupo de Ahorro");
            Console.WriteLine("6. Pedir prestamo");
            Console.WriteLine("7. Mis prestamos");
            Console.WriteLine("8. Salir del menu.");
            Console.Write("Ingrese la opción deseada: ");

            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("-----------------------------------------------");
                    GrupoAhorro.crear(Usuario);
                    Console.WriteLine("-----------------------------------------------");
                    break;

                case 2:
                    Console.WriteLine("-----------------------------------------------");
                    GrupoAhorro.grupos_ahorro_usuario_anadir(Usuario);
                    Console.WriteLine("-----------------------------------------------");
                    break;
                case 3:
                    Console.WriteLine("-----------------------------------------------");
                    Menu_cuenta_de_ahorro(Usuario);
                    Console.WriteLine("-----------------------------------------------");
                    break;

                case 4:
                    Console.WriteLine("-----------------------------------------------");
                    GrupoAhorro.mostrar_mis_grupos_ahorro(Usuario);
                    Console.WriteLine("-----------------------------------------------");
                    break;

                case 5:
                    Console.WriteLine("-----------------------------------------------");
                    MenuGrupoDeAhorro(Usuario);
                    Console.WriteLine("-----------------------------------------------");
                    break;

                case 6:
                    Console.WriteLine("-----------------------------------------------");
                    Menu_pedir_prestamo(Usuario);
                    Console.WriteLine("-----------------------------------------------");
                    break;

                case 7:
                    Console.WriteLine("-----------------------------------------------");
                    Menu_mis_prestamos(Usuario);
                    Console.WriteLine("-----------------------------------------------");
                    break;
                case 8:
                    Console.WriteLine("-----------------------------------------------");
                    Environment.Exit(0);
                    Console.WriteLine("-----------------------------------------------");
                    break;

                default:
                    Console.WriteLine("Opción no válida. Por favor, ingrese una opción válida.");
                    Console.WriteLine("-----------------------------------------------");
                    break;
            }
        }

        public static void Menu_cuenta_de_ahorro(Usuario_DB Usuario)
        {
            Console.WriteLine("-- BIENVENIDO A TU CUENTA DE AHORRO --");
            Console.WriteLine(" ID de tu cuenta ");
            Console.WriteLine(CuentaAhorro_DB_Usuario.ObtenerIdCuentaAhorro(Usuario));

            Console.WriteLine(" Capital de la cuenta ");
            Console.WriteLine(CuentaAhorro_DB_Usuario.ObtenerCapitalCuentaAhorro(Usuario)); 

            Console.WriteLine(" Usuario de la cuenta ");
            Console.WriteLine(CuentaAhorro_DB_Usuario.ObtenerNombreUsuario(Usuario));

            Console.WriteLine(" Ingresa 1 para volver al menu de usuario");
            
            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("-----------------------------------------------");
                    Menu_Usuario(Usuario);
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    Console.WriteLine("-----------------------------------------------");
                    break;

            }
        }

        public static void MenuGrupoDeAhorro(Usuario_DB Usuario)
        {
            Console.WriteLine($"-- GRUPO DE AHORRO --");
            Console.WriteLine("1. Ingresar capital a grupo de ahorro");
            Console.WriteLine("2. Disolver Grupo de Ahorro");
            Console.WriteLine("3. Volver al menú principal.");
            Console.Write("Ingrese la opción deseada: ");

            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    GrupoAhorro.ingresar_capital_grupo_ahorro(Usuario);
                    break;

                case 2:
                    GrupoAhorro.disolver(Usuario);
                    break;

                case 3:
                    Console.WriteLine("-----------------------------------------------");
                    Menu_Usuario(Usuario); // Vuelve al menú principal
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }

        public static void Menu_pedir_prestamo(Usuario_DB Usuario)
        {
            float Monto_a_pedir = 0;
            int Cuotas = 0;

            Console.WriteLine("-- PEDIR UN PRESTAMO --");

            Console.WriteLine("Ingrese la cantidad de dinero que quiere pedir a prestamo");
            try
            {
                Monto_a_pedir = int.Parse(Console.ReadLine());
            }

            catch (FormatException)
            {
                Console.WriteLine("El valor no es un número válido.");
                Menu_pedir_prestamo(Usuario);
            }

            Console.WriteLine("Ingrese a cuantas cuotas desea pagar el prestamo");

            try
            {
                Cuotas = int.Parse(Console.ReadLine());
            }

            catch (FormatException)
            {
                Console.WriteLine("El valor no es un número válido.");
                Menu_pedir_prestamo(Usuario);
            }

            GrupoAhorro.realizar_prestamo(Usuario, Monto_a_pedir, Cuotas);

        }

        public static void Menu_mis_prestamos(Usuario_DB Usuario)
        {

            Console.WriteLine("-- MIS PRESTAMOS --");
            Console.WriteLine("1. Ver mis prestamos");
            Console.WriteLine("2. Pagar deudas");
            Console.WriteLine("3. Salir");

            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Menu_ver_mis_prestamos(Usuario);
                    break;

                case 2:
                    Menu_pagar_prestamo(Usuario);
                    break;

                case 3:
                    Menu_Usuario(Usuario);
                    break;
            }
        }

        public static void Menu_ver_mis_prestamos(Usuario_DB Usuario)
        {
            Console.WriteLine("TUS PRESTAMOS");
            //metodo que muestre los prestamos
        }

        public static void Menu_pagar_prestamo(Usuario_DB Usuario)
        {

        }
    }
}