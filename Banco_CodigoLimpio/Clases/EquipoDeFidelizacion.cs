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
    public class EquipoDeFidelizacion
    {
        public List<Usuario_DB> ObtenerUsuariosConMayorContribucionPorGrupo()
        {
            List<GrupoAhorro_DB> gruposDeAhorro = GrupoAhorro_DB.Obtener_GruposAhorro_DB();
            List<Usuario_DB> usuariosConMayorContribucionPorGrupo = new List<Usuario_DB>();

            foreach (GrupoAhorro_DB grupo in gruposDeAhorro)
            {
                Usuario_DB usuarioConMayorContribucion = ObtenerUsuarioConMayorContribucion(grupo);

                if (usuarioConMayorContribucion != null)
                {
                    usuariosConMayorContribucionPorGrupo.Add(usuarioConMayorContribucion);
                }
            }

            return usuariosConMayorContribucionPorGrupo;
        }


        public Usuario_DB ObtenerUsuarioConMayorContribucion(GrupoAhorro_DB grupoAhorro)
        {
            List<Usuario_DB> usuariosDelGrupo = grupoAhorro.Usuarios;

            if (usuariosDelGrupo.Count == 0)
            {
                return null; // No hay usuarios en el grupo.
            }

            Usuario_DB usuarioMayorContribucion = null;
            float mayorContribucion = 0;

            foreach (Usuario_DB usuario in usuariosDelGrupo)
            {
                float contribucionUsuario = GrupoAhorro.obtener_capital_ingresado_usuario(usuario, grupoAhorro);

                if (contribucionUsuario > mayorContribucion)
                {
                    mayorContribucion = contribucionUsuario;
                    usuarioMayorContribucion = usuario;
                }
            }

            return usuarioMayorContribucion;
        }

        public static void PremiarEquipoConMasGanancias()
        {
            // Obtener todos los grupos de ahorro
            List<GrupoAhorro_DB> gruposAhorro = GrupoAhorro_DB.Obtener_GruposAhorro_DB();

            float maxGanancias = 0;
            GrupoAhorro_DB equipoGanador = null;

            foreach (GrupoAhorro_DB grupo in gruposAhorro)
            {
                // Calcular las ganancias actuales del grupo
                float ganancias = grupo.CuentaAhorro.Saldo - grupo.CuentaAhorro.SaldoInicial;

                if (ganancias > maxGanancias)
                {
                    maxGanancias = ganancias;
                    equipoGanador = grupo;
                }
            }

            if (equipoGanador != null)
            {
                // Inyectar el 10% de su saldo actual
                float premio = equipoGanador.CuentaAhorro.Saldo * 0.10f;
                GrupoAhorro.ingresar_capital_grupo_ahorro(equipoGanador, premio);
                Console.WriteLine($"El equipo ganador {equipoGanador.Nombre} ha recibido un premio de {premio}.");
            }
            else
            {
                Console.WriteLine("No hay equipos de ahorro registrados.");
            }
        }

    }
}
