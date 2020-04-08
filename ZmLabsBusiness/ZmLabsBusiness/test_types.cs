using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZmLabsBusiness;
using ZmLabsBusiness.tests;

namespace ZmLabsBusiness
{
    public static class test_types
    {
        public class mensajes
        {
            public Guid id;
            public string mensaje;
            public bool leido;
        }

        public enum enumEstadoProceso { Parado, Ejecutando, Finalizado, Erroneo }

        public static Object GetObject(test_functions _functions, ZmLabsObjects.enumTestTypes _type)
        {
            Object res = new Object();

            switch (_type)
            {
                case ZmLabsObjects.enumTestTypes.test1_multithreading_vs_singlethreading:

                    res = new test1_multithreading_vs_singlethreading(_functions);
                    break;

            }

            return res;
        }
    }
}
