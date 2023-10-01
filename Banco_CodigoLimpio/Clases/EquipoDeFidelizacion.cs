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
        //public GrupoAhorro EquipoConMayorGanancias { get; set; }
        public List<Usuario> UsuariosConMayorAportes { get; set; }

        public EquipoDeFidelizacion(List<Usuario> usuariosConMayorAportes)
        {
            //EquipoConMayorGanancias = equipoConMayorGanancias;
            UsuariosConMayorAportes = usuariosConMayorAportes;
        }

        public static void PremiarEquipoMasGanancias()
        {
            // Obtener todos los grupos de ahorro desde la base de datos
            /*var gruposDeAhorro = GrupoAhorro_DB.Obtener_GruposAhorro_DB();

            GrupoAhorro_DB equipoMasGanancias = null;
            decimal maxGanancias = decimal.MinValue;

            foreach (var grupoAhorro in gruposDeAhorro)
            {
                decimal totalGanancias = grupoAhorro.CalcularTotalGanancias(); // Implementa este método en tu clase CuentaAhorro_DB_GrupoAhorro

                if (totalGanancias > maxGanancias)
                {
                    maxGanancias = totalGanancias;
                    equipoMasGanancias = grupoAhorro;
                }
            }

            if (equipoMasGanancias != null)
            {
                decimal premio = equipoMasGanancias.ObtenerSaldoActual() * 0.10m;
                equipoMasGanancias.AgregarSaldo(premio); // Implementa este método en tu clase CuentaAhorro_DB_GrupoAhorro
            }*/
        }

        public static void PremiarUsuariosQueMasAportan()
        {
            // Obtener todos los grupos de ahorro desde la base de datos
            /*var gruposDeAhorro = Obtener_GruposAhorro_DB();

            foreach (var grupoAhorro in gruposDeAhorro)
            {
                List<Usuario_DB> usuariosQueMasAportan = grupoAhorro.ObtenerUsuariosQueMasAportan(); // Implementa este método en tu clase CuentaAhorro_DB_GrupoAhorro

                foreach (var usuario in usuariosQueMasAportan)
                {
                    usuario.ReducirComision(0.01m); // Implementa este método en tu clase Usuario_DB
                }
            }*/
        }
    }
}
