using System;
using System.Collections.Generic;
using System.Linq;

//using ZmLabsBusiness;
using ZmLabsBusiness.tests;
using ZmLabsBusiness.tests.objects;

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

        public static Object GetObject(test_info.test_functions_base _functions, ZmLabsObjects.enumTestTypes _type)
        {
            test_exec res = new test_exec(_functions);

            switch (_type)
            {
                case ZmLabsObjects.enumTestTypes.test1_multithreading_vs_singlethreading:

                    res = new test1_multithreading_vs_singlethreading(_functions);
                    break;

                case ZmLabsObjects.enumTestTypes.test2_basicos_concatstrings:

                    res = new test2_basicos_concatstrings(_functions);
                    break;

                case ZmLabsObjects.enumTestTypes.test3_sql_loaddata:

                    res = new test3_sql_loaddata(_functions);
                    break;

            }

            return res;
        }
    }
}
