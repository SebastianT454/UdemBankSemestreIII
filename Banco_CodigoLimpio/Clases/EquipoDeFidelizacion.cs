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

        public void IdentificarEquipoMayorGanancias()
        {

        }

        public void IdentificarUsuarioMayorAportantes()
        {

        }

        public void AplicarDescuentoUsuarios()
        {

        }

        public void InyectarSaldoEquipoMayorGanancias()
        {

        }
    }
}
