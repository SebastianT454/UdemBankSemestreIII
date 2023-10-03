using Banco_CodigoLimpio.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8604 // Posible argumento de referencia nulo

namespace Banco_CodigoLimpio.Menus
{
    public static class MenuGrafico
    {
        public static void Menu_Login()
        {
            Console.WriteLine("Que deseas hacer: ");
            Console.WriteLine("1. Registrarse");
            Console.WriteLine("2. Iniciar Sesion");
            Console.WriteLine("3. Salir");
            Console.Write("Ingrese la opción deseada: ");

            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("-----------------------------------------------");
                    Acceso_Gestor.registrarse();
                    break;
                case 2:
                    Console.WriteLine("-----------------------------------------------");
                    Acceso_Gestor.iniciar_sesion();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Opción no válida. Por favor, ingrese una opción válida.");
                    Console.WriteLine("-----------------------------------------------");
                    break;
            }
        }

        public static (string? nombre, string? contraseña) Menu_Registar_Usuario()
        {
            string? nombre;
            string? contraseña;

            do
            {
                Console.WriteLine("Ingresar nombre: ");
                nombre = Console.ReadLine();

                Console.WriteLine("-----------------------------------------------");

                Console.WriteLine("Ingresar contraseña: ");
                contraseña = Console.ReadLine();

                Console.WriteLine("-----------------------------------------------");
            }
            while (nombre == "" || contraseña == "");

            return (nombre, contraseña);
        }

        public static (string? nombre, string? contraseña) Menu_Iniciar_Sesion()
        {
            string? nombre;
            string? contraseña;

            do
            {
                Console.WriteLine("Ingresar nombre: ");
                nombre = Console.ReadLine();

                Console.WriteLine("-----------------------------------------------");

                Console.WriteLine("Ingresar contraseña: ");
                contraseña = Console.ReadLine();

                Console.WriteLine("-----------------------------------------------");
            }
            while (nombre == "" || contraseña == "");

            return (nombre, contraseña);
        }
    }
}
