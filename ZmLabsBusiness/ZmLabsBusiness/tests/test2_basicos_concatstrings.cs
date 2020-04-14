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

            while (cont < _testobject.TestCases.Count)
            {
                TestCases _test = _testobject.TestCases[cont];

                switch (_test.Function)
                {
                    case "Concat_PlusOperator":

                        _testobject.TestCases[cont] = Concat_PlusOperator(_test);
                        break;

                    case "Concat_StringBuilder":

                        _testobject.TestCases[cont] = Concat_StringBuilder(_test);
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
            TestCaseExecutions _testexec = new TestCaseExecutions() { idTestCase = _test.id };

            //registra inicio
            _testexec.dtBegin = DateTime.Now;

            SetMsg("- - - - -");
            SetMsg("Concat_PlusOperator Case iniciado a las " + _testexec.dtBegin);

            functions.quijote quijoteObject = new functions.quijote();
            quijoteObject.ConcatQuijotePlusOperator();

            //registra fin
            _testexec.dtEnd = DateTime.Now;

            SetMsg("Concat_PlusOperator Case finalizado a las " + _testexec.dtEnd);
            SetMsg("Concat_PlusOperator Case ejecutado en " + _testexec.Miliseconds + " milisegundos");

            _testobject.InsertExecution(_testexec);

            return _test;
        }

        public TestCases Concat_StringBuilder(TestCases _test)
        {
            TestCaseExecutions _testexec = new TestCaseExecutions() { idTestCase = _test.id };

            //registra inicio
            _testexec.dtBegin = DateTime.Now;

            SetMsg("- - - - -");
            SetMsg("Concat_StringBuilder Case iniciado a las " + _testexec.dtBegin);

            functions.quijote quijoteObject = new functions.quijote();
            quijoteObject.ConcatQuijoteStringBuilder();

            //registra fin
            _testexec.dtEnd = DateTime.Now;

            SetMsg("Concat_StringBuilder Case finalizado a las " + _testexec.dtEnd);
            SetMsg("Concat_StringBuilder Case ejecutado en " + _testexec.Miliseconds + " milisegundos");

            _testobject.InsertExecution(_testexec);

            return _test;
        }
    }
}
