using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banco_CodigoLimpio.BaseDeDatos;
using Banco_CodigoLimpio.Menus;

// MongoDB

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Banco_CodigoLimpio
{
    public static class App
    {
        public static void App_Start() 
        { 
            while (true)
            {
               Menus.MenuGrafico.Menu_Login();
            }
        }
        public static void App_Main_Menu(Usuario_DB Usuario)
        {
            while (true)
            {
                Menus.MenuClases.Menu_Usuario(Usuario);
            }

        }

    }

}
