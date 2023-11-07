using Banco_CodigoLimpio.BaseDeDatos;
using Banco_CodigoLimpio.Menus;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

#pragma warning disable CS8604 // Posible argumento de referencia nulo

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
            }

        }

        public static void grupos_ahorro_usuario_anadir(Usuario_DB Usuario_App)
        {
            // Utilizar el filtro para encontrar el usuario en la colección
            Usuario_DB? Usuario = Usuario_DB.ObtenerUsuario_byName_DB(Usuario_App.Nombre);

            List<GrupoAhorro_DB> grupos_ahorro_usuario = Usuario.GruposAhorro;

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

                foreach (GrupoAhorro_DB Grupo_ahorro in grupos_ahorro_usuario)
                {
                    Console.WriteLine($"{indice}. {Grupo_ahorro.Nombre}", indice++);

                }
                Console.WriteLine("-----------------------------------------------");

                Console.Write("Elige un grupo: ");
                string entrada = Console.ReadLine();

                int opcion_seleccionada = int.Parse(entrada);

                if (opcion_seleccionada <= grupos_ahorro_usuario.Count)
                {
                    GrupoAhorro_DB GrupoAhorro_usuario_deseado = grupos_ahorro_usuario[opcion_seleccionada - 1];
                    Console.WriteLine(GrupoAhorro_usuario_deseado.Nombre);

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
                        Console.WriteLine("El usuario ya está en el grupo.");
                    }

                    else
                    {
                        // Agregar el usuario al grupo
                        GrupoAhorro_DB.Actualizar_GrupoAhorro(usuario_deseado, GrupoAhorro_usuario_deseado);
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

                    // Obteniendo el grupo de ahorro actualizado de la DB.

                    GrupoAhorro_DB grupo_ahorro_actualizado = GrupoAhorro_DB.ObtenerGrupoAhorro_DB(GrupoAhorro_deseado);
                    float numero;
                    float NuevoSaldo_GrupoAhorro;

                    Console.Write("Ingrese un número: ");
                    string entrada_saldo = Console.ReadLine();

                    if (float.TryParse(entrada_saldo, out numero))
                    {
                        float comision_transferencia = numero * 0.001f;

                        float recibido_por_el_grupo = numero - comision_transferencia;

                        // Ingresando la comision en el banco.

                        Banco_DB banco = Banco_DB.Obtener_banco();
                        Banco_DB.Ingresar_Comision(banco, comision_transferencia);

                        Console.WriteLine("Antes de la operacion: " + grupo_ahorro_actualizado.CuentaAhorro.Saldo);
                        NuevoSaldo_GrupoAhorro = grupo_ahorro_actualizado.CuentaAhorro.Saldo + recibido_por_el_grupo;
                        Console.WriteLine("Despues de la operacion: " + NuevoSaldo_GrupoAhorro);
                    }
                    else
                    {
                        return;
                    }

                    GrupoAhorro_DB.ActualizarSaldo_GrupoAhorro(Usuario, grupo_ahorro_actualizado, NuevoSaldo_GrupoAhorro);
                    Console.WriteLine("Capital ingresado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Opcion no valida");
                }
            }

        }
        public static void realizar_prestamo(Usuario_DB Usuario_App, float Monto_a_pedir, int Cuotas)
        {
            // Utilizar el filtro para encontrar el usuario en la colección
            Usuario_DB? Usuario_ = Usuario_DB.ObtenerUsuario_byName_DB(Usuario_App.Nombre);

            List<GrupoAhorro_DB> grupos_usuario = Usuario_.GruposAhorro;

            if (Usuario_.GruposAhorro.Count == 0)
            {
                Console.WriteLine("No tienes grupos de ahorro.");
                return;
            }
            else
            {
                Console.WriteLine("Que grupo deseas escojer: ");
                Console.WriteLine("1. Mis grupos");
                Console.WriteLine("2. Otro grupos");
                Console.WriteLine("3. Cancelar");
                Console.Write("Ingrese la opción deseada: ");

                int opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        GrupoAhorro_DB GrupoAhorro_deseado = obtener_grupo_especifico_usuario(Usuario_, grupos_usuario);

                        float Monto_usuario = obtener_capital_ingresado_usuario(Usuario_App, GrupoAhorro_deseado);

                        if (Monto_a_pedir > Monto_usuario)
                        {
                            Console.WriteLine("El préstamo supera el monto total invertido por el usuario");
                            break;
                        }
                        else if(Cuotas < Configuracion.min_plazo_de_pago)
                        {
                            Console.WriteLine("El plazo de pago es inferior a dos meses");
                            break;
                        }

                        Movimiento.crear(Usuario_, GrupoAhorro_deseado, Monto_a_pedir, "PrestamoGrupoPropioAhorro");
                        break;

                    case 2:
                        GrupoAhorro_DB GrupoAhorro_deseado_otros_grupos = obtener_otros_grupos_usuario(Usuario_, grupos_usuario);
                        Movimiento.crear(Usuario_, GrupoAhorro_deseado_otros_grupos, Monto_a_pedir, "PrestamoOtrosGruposAhorro");

                        break;
                    case 3:
                        Menus.MenuClases.Menu_Usuario(Usuario_);
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, ingrese una opción válida.");
                        Console.WriteLine("-----------------------------------------------");
                        break;
                }
            }

        }

        public static float obtener_capital_ingresado_usuario(Usuario_DB Usuario_App, GrupoAhorro_DB GrupoAhorro_deseado)
        {
            List<Movimiento_DB> movimientos = Usuario_App.Movimientos;

            float monto_usuario = 0;

            foreach(Movimiento_DB movimiento in movimientos) 
            {

                if (movimiento.GrupoAsociado.Id == GrupoAhorro_deseado.Id) 
                {
                    monto_usuario += movimiento.Monto;
                    Console.WriteLine("if condicional: ", monto_usuario);
                }
            }

            return monto_usuario;

        }

        public static GrupoAhorro_DB obtener_grupo_especifico_usuario(Usuario_DB Usuario, List<GrupoAhorro_DB> grupos_usuario)
        {
            Console.WriteLine("-----------------------------------------------");

            Console.WriteLine("Los grupos de ahorro que tienes son:");
            int indice = 1;

            foreach (GrupoAhorro_DB Grupo_ahorro in grupos_usuario)
            {
                Console.WriteLine($"{indice}. {Grupo_ahorro.Nombre}", indice++);

            }
            Console.WriteLine("-----------------------------------------------");

            Console.Write("Elige el grupo que quieres pedir el prestamo: ");
            string entrada = Console.ReadLine();

            int opcion_seleccionada = int.Parse(entrada);

            if (opcion_seleccionada <= grupos_usuario.Count)
            {
                GrupoAhorro_DB GrupoAhorro_deseado = grupos_usuario[opcion_seleccionada - 1];

                return GrupoAhorro_deseado;
            }
            else
            {
                Console.WriteLine("Opcion no valida");
                return null;
            }
        }

        public static GrupoAhorro_DB obtener_otros_grupos_usuario(Usuario_DB Usuario_App, List<GrupoAhorro_DB> grupos_usuario)
        {
            List<Usuario_DB> Usuarios_asociados_grupos_ahorros_usuario = new List<Usuario_DB>();

            foreach (GrupoAhorro_DB grupo in grupos_usuario)
            {
                foreach (Usuario_DB Usuario in grupo.Usuarios)
                {
                    Usuarios_asociados_grupos_ahorros_usuario.Add(Usuario);
                }
            }

            List<GrupoAhorro_DB> grupos_ahorro_existentes = GrupoAhorro_DB.Obtener_GruposAhorro_DB();

            // "Eliminando" los grupos de ahorro que el usuario tiene, ya que solo se quiere visualizar el resto de grupos de ahorro existentes.
            List<GrupoAhorro_DB> grupos_ahorro_disponibles = grupos_ahorro_existentes.Except(grupos_usuario).ToList();

            // Creando una lista donde haya minimo 1 usuario asociado con el usuario principal.
            List<GrupoAhorro_DB> grupos_ahorro_disponibles_para_el_usuario = new List<GrupoAhorro_DB>();


            if (grupos_ahorro_disponibles.Count != 0)
            {
                foreach (GrupoAhorro_DB grupo in grupos_ahorro_disponibles)
                {
                    bool existe_algun_usuario_asociado = Usuarios_asociados_grupos_ahorros_usuario.Any(elemento => grupo.Usuarios.Contains(elemento));

                    if (existe_algun_usuario_asociado)
                    {
                        grupos_ahorro_disponibles_para_el_usuario.Add(grupo);
                    }
                }
            }
            else
            {
                Console.WriteLine("No existen grupos de ahorro que puedas pedir prestamo.");
                return null;
            }

            if (grupos_ahorro_disponibles_para_el_usuario.Count != 0) 
            {
                Console.WriteLine("Los grupos de ahorro que tienes son:");
                int indice = 1;

                foreach (GrupoAhorro_DB Grupo_ahorro in grupos_ahorro_disponibles_para_el_usuario)
                {
                    Console.WriteLine($"{indice}. {Grupo_ahorro.Nombre}", indice++);

                }
                Console.WriteLine("-----------------------------------------------");

                Console.Write("Elige el grupo que quieres pedir el prestamo: ");
                string entrada = Console.ReadLine();

                int opcion_seleccionada = int.Parse(entrada);

                if (opcion_seleccionada <= grupos_ahorro_disponibles_para_el_usuario.Count)
                {
                    GrupoAhorro_DB GrupoAhorro_deseado = grupos_ahorro_disponibles_para_el_usuario[opcion_seleccionada - 1];

                    return GrupoAhorro_deseado;
                }
                else
                {
                    Console.WriteLine("Opcion no valida");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("El usuario no cuenta con usuarios asociados a otros grupos.");
                return null;
            } 
            
        }
        public static void disolver(Usuario_DB Usuario_App)
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

                    // Obteniendo el grupo de ahorro actualizado de la DB.

                    GrupoAhorro_DB grupo_ahorro_actualizado = GrupoAhorro_DB.ObtenerGrupoAhorro_DB(GrupoAhorro_deseado);

                    // Obteniendo el porcentaje que cada usuario tiene el grupo de ahorros.

                    List<float> porcentajes_usuarios_grupo_ahorro = new List<float>();

                    float saldo_grupo_ahorro = grupo_ahorro_actualizado.CuentaAhorro.Saldo;

                    if (saldo_grupo_ahorro != 0)
                    {
                        float comision_grupo_ahorro = saldo_grupo_ahorro * 0.05f;

                        float saldo_con_comision_grupo_ahorro = saldo_grupo_ahorro - comision_grupo_ahorro;

                        // Obteniendo el porcentaje que representa cada usuario en la cuenta de ahorros del grupo.

                        foreach (Usuario_DB Usuario_grupo in grupo_ahorro_actualizado.Usuarios)
                        {
                            float monto_ingresado_por_usuario = obtener_capital_ingresado_usuario(Usuario_grupo, grupo_ahorro_actualizado);

                            float porcentaje_usuario = monto_ingresado_por_usuario / saldo_grupo_ahorro;

                            porcentajes_usuarios_grupo_ahorro.Add(porcentaje_usuario);
                        }

                        // Repartiendo cada porcentaje a cada usuario con lo restante.

                        foreach (Usuario_DB Usuario_grupo_ahorros in grupo_ahorro_actualizado.Usuarios)
                        {
                            foreach(float porcentaje in porcentajes_usuarios_grupo_ahorro)
                            {
                                float restante_recibido_usuario = saldo_con_comision_grupo_ahorro * porcentaje;

                                if(restante_recibido_usuario != 0)
                                {
                                    Console.WriteLine("El Usuario con nombre " + Usuario_grupo_ahorros.Nombre + "se le transfiere" + restante_recibido_usuario);
                                    CuentaAhorro.ingresar_capital_cuenta_ahorro_usuario(Usuario_grupo_ahorros, restante_recibido_usuario);
                                }
                                else
                                {
                                    Console.WriteLine("El Usuario con nombre " + Usuario_grupo_ahorros.Nombre + " no recibe nada.");
                                }
                            }
                        }

                        // Ingresando la comision en el banco.

                        Banco_DB banco = Banco_DB.Obtener_banco();
                        Banco_DB.Ingresar_Comision(banco, comision_grupo_ahorro);
                        
                    }
                    else 
                    {
                        Console.WriteLine("No se ha hecho ninguna transferencia de dinero al grupo de ahorro");
                    }

  
                    Console.WriteLine("Grupo de ahorro eliminado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Opcion no valida");
                }
            }
        }

        public static void calcular_ganancias(GrupoAhorro_DB grupo)
        {

        }
    }
}
