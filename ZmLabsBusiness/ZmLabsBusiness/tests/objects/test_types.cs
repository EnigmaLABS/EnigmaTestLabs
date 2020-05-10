using System;
using System.Collections.Generic;
using System.Linq;

using ZmLabsObjects;
using ZmLabsObjects.sqltests;

using ZmLabsBusiness.functions;
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

        private static ZmLabsObjects.contracts.ITestFunctionsDomain FunctionDomain;

        public static Object GetObject(TestDomain Test, ZmLabsObjects.enumTestTypes _type, ZmLabsObjects.contracts.ITestFunctionsDomain p_FunctionDomain)
        {
            FunctionDomain = p_FunctionDomain;
            test_base res = new test_base(Test, FunctionDomain);

            switch (_type)
            {
                case ZmLabsObjects.enumTestTypes.test1_multithreading_vs_singlethreading:

                    res = new test1_multithreading_vs_singlethreading(Test, FunctionDomain, new fibo_functions());
                    break;

                case ZmLabsObjects.enumTestTypes.test2_basicos_concatstrings:

                    res = new test2_basicos_concatstrings(Test, FunctionDomain, new quijote());
                    break;

                case ZmLabsObjects.enumTestTypes.test3_sql_loaddata:

                    res = new test3_sql_loaddata(Test,
                                                    FunctionDomain,
                                                    new data.Business_Data_Functions(),
                                                    new parte_horas_functions(new List<IParteHoras>()));
                                                    
                    break;

            }

            return res;
        }
    }
}
