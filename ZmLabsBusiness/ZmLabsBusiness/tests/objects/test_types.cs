using System;

using ZmLabsObjects;
using ZmLabsObjects.contracts;

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

        private static ITestFunctionsDomain FunctionDomain;

        public static test_base GetObject(TestDomain Test, enumTestTypes _type, ITestFunctionsDomain p_FunctionDomain)
        {
            FunctionDomain = p_FunctionDomain;
            test_base res = new test_base(Test, FunctionDomain);

            switch (_type)
            {
                case enumTestTypes.test1_multithreading_vs_singlethreading:

                    res = new test1_multithreading_vs_singlethreading(Test, FunctionDomain);
                    break;

                case enumTestTypes.test2_basicos_concatstrings:

                    res = new test2_basicos_concatstrings(Test, FunctionDomain);
                    break;

                case enumTestTypes.test3_sql_loaddata:

                    res = new test3_sql_loaddata(Test,
                                                    FunctionDomain,
                                                    new data.Business_Data_Functions());
                                                    
                    break;

            }

            return res;
        }
    }
}
