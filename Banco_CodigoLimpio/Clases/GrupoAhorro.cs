using Banco_CodigoLimpio.BaseDeDatos;
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
    public static class GrupoAhorro
    {
        public static void crear(Usuario_DB Usuario)
        {
            GrupoAhorro_DB.CrearGrupoAhorro(Usuario);
            Console.WriteLine("Grupo de Ahorro generado.");
        }
        public static void mostrar_mis_grupos_ahorro(Usuario_DB Usuario_App)
        {
            // Utilizar el filtro para encontrar el usuario en la colección
            Usuario_DB? Usuario = Usuario_DB.ObtenerUsuario_byName_DB(Usuario_App.Nombre);

            List<GrupoAhorro_DB> grupos_ahorro_usuario_deseado = Usuario.GruposAhorro;

            if (Usuario.GruposAhorro.Count == 0)
            {
                Console.WriteLine("No tienes grupos de ahorro.");
                return;
            }
            else
            {
                Console.WriteLine("Los grupos de ahorro que tienes son:");
                int indice = 1;

                foreach (GrupoAhorro_DB Grupo_ahorro in grupos_ahorro_usuario_deseado)
                {
                    Console.WriteLine($"{indice}. {Grupo_ahorro.Nombre}", indice++);

                }
                Console.WriteLine("-----------------------------------------------");
            }

        }

        public static void grupos_ahorro_usuario_anadir(Usuario_DB Usuario_App)
        {
            // Utilizar el filtro para encontrar el usuario en la colección
            Usuario_DB? Usuario = Usuario_DB.ObtenerUsuario_byName_DB(Usuario_App.Nombre);

            List<GrupoAhorro_DB> grupos_ahorro_usuario_deseado = Usuario.GruposAhorro;

            if (Usuario.GruposAhorro.Count == 0)
            {
                Console.WriteLine("No tienes grupos de ahorro.");
                return;
            }
            else
            {
                Console.WriteLine("Ingrese el nombre del usuario que deseas añadir al grupo de ahorro:");
                string Nombre_Usuario_deseado = Console.ReadLine();

                Console.WriteLine("-----------------------------------------------");

                Console.WriteLine("Los grupos de ahorro que tienes son:");
                int indice = 1;

                foreach (GrupoAhorro_DB Grupo_ahorro in grupos_ahorro_usuario_deseado)
                {
                    Console.WriteLine($"{indice}. {Grupo_ahorro.Nombre}", indice++);

                }
                Console.WriteLine("-----------------------------------------------");

                Console.Write("Elige un grupo: ");
                string entrada = Console.ReadLine();

                int opcion_seleccionada = int.Parse(entrada);

                if (opcion_seleccionada <= grupos_ahorro_usuario_deseado.Count)
                {
                    GrupoAhorro_DB GrupoAhorro_usuario_deseado = grupos_ahorro_usuario_deseado[opcion_seleccionada - 1];
                    anadir_usuarios(Nombre_Usuario_deseado, GrupoAhorro_usuario_deseado);

                    Console.WriteLine("Usuario invitado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Opcion no valida");
                }

            }

        }


        public static void anadir_usuarios(string Usuario_deseado, GrupoAhorro_DB GrupoAhorro_usuario_deseado)
        {
            // Verificar si New_Usuario ya existe en la lista de usuarios
            Usuario_DB? usuario_deseado = Usuario_DB.ObtenerUsuario_byName_DB(Usuario_deseado);

            if (usuario_deseado != null)
            {
                // Verificar si el grupo de ahorro ya tiene 2 usuarios invitados
                if (GrupoAhorro_usuario_deseado.Usuarios.Count < 2)
                {
                    // Verificar si el usuario ya está en el grupo
                    if (GrupoAhorro_usuario_deseado.Usuarios.Contains(usuario_deseado))
                    {
                        // Agregar el usuario al grupo
                        GrupoAhorro_DB.Actualizar_GrupoAhorro(usuario_deseado, GrupoAhorro_usuario_deseado);
                    }

                    else
                    {
                        Console.WriteLine("El usuario ya está en el grupo.");
                    }
                }
                else
                {
                    Console.WriteLine("El grupo de ahorro ya tiene el maximo de usuarios.");
                }
            }
            else
            {
                Console.WriteLine("Usuario no encontrado.");
            }
        }

        public static void ingresar_capital_grupo_ahorro(Usuario_DB Usuario_App)
        {
            // Utilizar el filtro para encontrar el usuario en la colección
            Usuario_DB? Usuario = Usuario_DB.ObtenerUsuario_byName_DB(Usuario_App.Nombre);

            List<GrupoAhorro_DB> grupos_usuario = Usuario.GruposAhorro;

            if (Usuario.GruposAhorro.Count == 0)
            {
                Console.WriteLine("No tienes grupos de ahorro.");
                return;
            }
            else
            {

                Console.WriteLine("-----------------------------------------------");

                Console.WriteLine("Los grupos de ahorro que tienes son:");
                int indice = 1;

                foreach (GrupoAhorro_DB Grupo_ahorro in grupos_usuario)
                {
                    Console.WriteLine($"{indice}. {Grupo_ahorro.Nombre}", indice++);

                }
                Console.WriteLine("-----------------------------------------------");

                Console.Write("Elige el grupo que quieras añadir capital: ");
                string entrada = Console.ReadLine();

                int opcion_seleccionada = int.Parse(entrada);

                if (opcion_seleccionada <= grupos_usuario.Count)
                {
                    GrupoAhorro_DB GrupoAhorro_deseado = grupos_usuario[opcion_seleccionada - 1];
                    float numero;
                    float NuevoSaldo_GrupoAhorro;

                    Console.Write("Ingrese un número: ");
                    string entrada_saldo = Console.ReadLine();

                    if (float.TryParse(entrada_saldo, out numero))
                    {
                        NuevoSaldo_GrupoAhorro = Usuario.CuentaAhorro.Saldo + numero;
                    }
                    else
                    {
                        return;
                    }

                    GrupoAhorro_DB.ActualizarSaldo_GrupoAhorro(GrupoAhorro_deseado, NuevoSaldo_GrupoAhorro);
                    Console.WriteLine("Capital ingresado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Opcion no valida");
                }
            }

        }
        public static void pagar_prestamo()
        {

        }

        public static void disolver()
        {

        }

    }
}
