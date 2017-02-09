using SoftBabymartVn.Infractstructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftBabymartVn.App_Code
{
    public class App_Start
    {
        public static void AppInitialize()
        {
            Mappers.Boot();
        }
    }
}