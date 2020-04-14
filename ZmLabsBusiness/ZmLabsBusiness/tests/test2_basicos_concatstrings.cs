using System;

using ZmLabsObjects;

namespace ZmLabsBusiness.tests
{
    public class test2_basicos_concatstrings : objects.test_exec
    {
        public test2_basicos_concatstrings(test_info.test_functions_base p_testobject) : base(p_testobject) { }

        public override void Start()
        {
            this.Estado = test_types.enumEstadoProceso.Ejecutando;

            SetMsg("- - - - -");
            SetMsg("test2_basicos_concatstrings iniciado a las " + DateTime.Now.ToLongTimeString());

            int cont = 0;

            while (cont < _testobject.Execution.testcases.Count)
            {
                TestCases _test = _testobject.Execution.testcases[cont];

                switch (_test.Function)
                {
                    case "Concat_PlusOperator":

                        _testobject.Execution.testcases[cont] = Concat_PlusOperator(_test);
                        break;

                    case "Concat_StringBuilder":

                        _testobject.Execution.testcases[cont] = Concat_StringBuilder(_test);
                        break;
                }

                cont++;
            }

            SetMsg("- - - - -");
            SetMsg("test2_basicos_concatstrings finalizado a las " + DateTime.Now.ToLongTimeString());


            this.Estado = test_types.enumEstadoProceso.Finalizado;
        }

        public TestCases Concat_PlusOperator(TestCases _test)
        {
            //registra inicio
            _test.dtBegin = DateTime.Now;

            SetMsg("- - - - -");
            SetMsg("Concat_PlusOperator Case iniciado a las " + _test.dtBegin);

            functions.quijote quijoteObject = new functions.quijote();
            quijoteObject.ConcatQuijotePlusOperator();

            //registra fin
            _test.dtEnd = DateTime.Now;

            TimeSpan _ts = _test.dtEnd - _test.dtBegin;

            SetMsg("Concat_PlusOperator Case finalizado a las " + _test.dtEnd);
            SetMsg("Concat_PlusOperator Case ejecutado en " + _ts.TotalMilliseconds + " milisegundos");

            _testobject.TestRecord(_test);

            return _test;
        }

        public TestCases Concat_StringBuilder(TestCases _test)
        {
            //registra inicio
            _test.dtBegin = DateTime.Now;

            SetMsg("- - - - -");
            SetMsg("Concat_StringBuilder Case iniciado a las " + _test.dtBegin);

            functions.quijote quijoteObject = new functions.quijote();
            quijoteObject.ConcatQuijoteStringBuilder();

            //registra fin
            _test.dtEnd = DateTime.Now;

            TimeSpan _ts = _test.dtEnd - _test.dtBegin;

            SetMsg("Concat_StringBuilder Case finalizado a las " + _test.dtEnd);
            SetMsg("Concat_StringBuilder Case ejecutado en " + _ts.TotalMilliseconds + " milisegundos");

            _testobject.TestRecord(_test);

            return _test;
        }
    }
}
