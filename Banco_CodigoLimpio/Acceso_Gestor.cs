using Banco_CodigoLimpio.BaseDeDatos;
using Banco_CodigoLimpio.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CS8604 // Posible argumento de referencia nulo

namespace Banco_CodigoLimpio
{
    public static class Acceso_Gestor
    {
        public static void registrarse()
        {
            List<Usuario_DB> Usuarios_existentes = Usuario_DB.Obtener_Usuarios();

            (string? nombre, string? contraseña) resultado = Menus.MenuGrafico.Menu_Registar_Usuario();

            foreach (Usuario_DB Usuario in Usuarios_existentes)
            {
                if(Usuario.Nombre == resultado.nombre)
                {
                    Console.WriteLine("El usuario ya existe en la base de datos.");
                    Console.WriteLine("-----------------------------------------------");
                    return;
                }
            }

            Usuario_DB Usuario_Generado = Usuario_DB.CrearUsuario(resultado.nombre, resultado.contraseña);
            App.App_Main_Menu(Usuario_Generado);

        }

        public static void iniciar_sesion()
        {
            (string? nombre, string? contraseña) resultado = Menus.MenuGrafico.Menu_Iniciar_Sesion();

            Usuario_DB? Usuario = Usuario_DB.ObtenerUsuario_DB(resultado.nombre, resultado.contraseña);

            if (Usuario == null)
            {
                Console.WriteLine("No existe el usuario.");
                Console.WriteLine("-----------------------------------------------");
            }
            else
            {
                App.App_Main_Menu(Usuario);
            }

        }
    }
}
