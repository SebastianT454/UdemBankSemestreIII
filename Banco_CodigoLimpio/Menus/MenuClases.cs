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
            Console.WriteLine("2. Invitar usuario");
            Console.WriteLine("3. Mi cuenta de ahorro");
            Console.WriteLine("4. Mis grupos de ahorro");
            Console.WriteLine("5. Pedir prestamo");
            Console.WriteLine("6. Mis prestamos");
            Console.WriteLine("7. Volver al menu principal.");
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
                    GrupoAhorro.mostrar_grupos_ahorro(Usuario);
                    Console.WriteLine("-----------------------------------------------");
                    break;
                case 3:
                    Console.WriteLine("Espera un momento");
                    Menu_cuenta_de_ahorro(Usuario);
                    break;

                case 4:
                    Console.WriteLine("Espera un momento");
                    Menu_Grupo_De_Ahorro(Usuario);
                    break;

                case 5:
                    Console.WriteLine("Espera un momento");
                    Menu_pedir_prestamo(Usuario);
                    break;

                case 6:
                    Console.WriteLine("Espera un momento");
                    Menu_mis_prestamos(Usuario);
                    break;

                case 7:
                    Console.WriteLine("-----------------------------------------------");
                    MenuGrafico.Menu_Principal(Usuario);
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

            }
        }

        public static void Menu_Grupo_De_Ahorro(Usuario_DB Usuario)
        {
            Console.WriteLine("-- TUS GRUPOS DE AHORRO --");
            var gruposAhorro = Usuario.GruposAhorro;

            for (int i = 0; i < gruposAhorro.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Grupo de ahorro #{i + 1} - {gruposAhorro[i].Nombre}");
            }

            Console.WriteLine($"{gruposAhorro.Count + 1}. Crear nuevo grupo de ahorro");
            Console.WriteLine($"{gruposAhorro.Count + 2}. Volver al menú principal.");

            int opcion = int.Parse(Console.ReadLine());

            if (opcion >= 1 && opcion <= gruposAhorro.Count)
            {
                var grupoSeleccionado = gruposAhorro[opcion - 1];
                MenuGrupoDeAhorro(Usuario, grupoSeleccionado); // Llama al método con el grupo seleccionado
            }
            else if (opcion == gruposAhorro.Count + 2)
            {
                Console.WriteLine("-----------------------------------------------");
                Menu_Usuario(Usuario); // Vuelve al menú principal
            }
            else
            {
                Console.WriteLine("Opción no válida.");
            }
        }

        public static void MenuGrupoDeAhorro(Usuario_DB Usuario, GrupoAhorro_DB grupoAhorro)
        {
            Console.WriteLine($"-- GRUPO DE AHORRO: {grupoAhorro.Nombre} --");
            Console.WriteLine("1. Ingresar capital a grupo de ahorro");
            Console.WriteLine("2. Invitar a un usuario al grupo");
            Console.WriteLine("3. Volver al menú principal.");

            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    // Lógica para ingresar capital al grupo de ahorro
                    break;

                case 2:
                    // Lógica para invitar a un usuario al grupo de ahorro
                    break;

                case 3:
                    Console.WriteLine("-----------------------------------------------");
                    Menu_Grupo_De_Ahorro(Usuario); // Vuelve al menú de grupos de ahorro
                    break;

                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
        public static void Menu_grupo_de_ahorro_1(Usuario_DB Usuario)
        {
            Console.WriteLine("-- TU GRUPO DE AHORRO --");
            Console.WriteLine("1. Ingresar capital a grupo de ahorro");
            Console.WriteLine("2. invitar a un usuario al grupo");
            Console.WriteLine("3. Volver al menu principal.");

            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("-----------------------------------------------");
                    //aqui va la logica pero no puedo ver donde esta capital
                    break;

                case 2:
                    Console.WriteLine("-----------------------------------------------");
                    //aqui va la logica pero esto es con database
                    break;

                case 3:
                    Console.WriteLine("-----------------------------------------------");
                    Menu_Usuario(Usuario);
                    break;
            }
        }

        public static void Menu_grupo_de_ahorro_2(Usuario_DB Usuario)
        {
            Console.WriteLine("-- TU GRUPO DE AHORRO --");
            Console.WriteLine("1. Ingresar capital a grupo de ahorro");
            Console.WriteLine("2. invitar a un usuario al grupo");
            Console.WriteLine("3. Volver al menu principal.");

            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("-----------------------------------------------");
                    //aqui va la logica pero no puedo ver donde esta capital
                    break;

                case 2:
                    Console.WriteLine("-----------------------------------------------");
                    //aqui va la logica pero esto es con database
                    break;

                case 3:
                    Console.WriteLine("-----------------------------------------------");
                    Menu_Usuario(Usuario);
                    break;
            }
        }

        public static void Menu_grupo_de_ahorro_3(Usuario_DB Usuario)
        {
            Console.WriteLine("-- TU GRUPO DE AHORRO --");
            Console.WriteLine("1. Ingresar capital a grupo de ahorro");
            Console.WriteLine("2. invitar a un usuario al grupo");
            Console.WriteLine("3. Volver al menu principal.");

            int opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    Console.WriteLine("-----------------------------------------------");
                    //aqui va la logica pero no puedo ver donde esta capital
                    break;

                case 2:
                    Console.WriteLine("-----------------------------------------------");
                    //aqui va la logica pero esto es con database
                    break;

                case 3:
                    Console.WriteLine("-----------------------------------------------");
                    Menu_Usuario(Usuario);
                    break;
            }
        }


        public static void Menu_pedir_prestamo(Usuario_DB Usuario)
        {

            Console.WriteLine("-- PEDIR UN PRESTAMO --");
            Console.WriteLine("Selecciona uno de los siguientes grupos de ahorro para pedir el prestamo");
            //aqui va la logica pa la vueltica de mostarlos grupos de ahorro disponible
            int Grupo_a_pedir = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese la cantidad de dinero que quiere pedir a prestamo");
            try
            {
                int Monto_a_pedir = int.Parse(Console.ReadLine());
            }

            catch (FormatException)
            {
                Console.WriteLine("El valor no es un número válido.");
                Menu_pedir_prestamo(Usuario);
            }

            Console.WriteLine("Ingrese a cuantas cueotas desea pagar el prestamo");

            try
            {
                int Cuotas = int.Parse(Console.ReadLine());
            }

            catch (FormatException)
            {
                Console.WriteLine("El valor no es un número válido.");
                Menu_pedir_prestamo(Usuario);
            }
        }

        public static void Menu_mis_prestamos(Usuario_DB Usuario)
        {

            Console.WriteLine("-- MIS PRESTAMOS --");
            Console.WriteLine("1. Ver mis prestamos");
            Console.WriteLine("2. Pagar prestamo");
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