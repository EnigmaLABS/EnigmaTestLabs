﻿using System;

using ZmLabsObjects;

namespace ZmLabsBusiness.tests
{
    public class test2_basicos_concatstrings : objects.test_exec
    {
        public test2_basicos_concatstrings(test_info.test_functions_base p_testobject) : base(p_testobject) { }

        public override void Start()
        {
            //inicia test
            this.InitTest();

            //recorre y ejecuta testcases
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

            //finaliza test
            this.EndTest();
        }

        public TestCases Concat_PlusOperator(TestCases _test)
        {
            TestCaseExecutions _testexec = new TestCaseExecutions() { idTestCase = _test.id };

            //registra inicio
            _testexec.dtBegin = DateTime.Now;
            InitTestCase(_test.Function, _testexec.dtBegin);

            //ejecuta testcase
            functions.quijote quijoteObject = new functions.quijote();
            quijoteObject.ConcatQuijotePlusOperator();

            //registra fin
            _testexec.dtEnd = DateTime.Now;
            EndTestCase(_test.Function, _testexec);

            return _test;
        }

        public TestCases Concat_StringBuilder(TestCases _test)
        {
            TestCaseExecutions _testexec = new TestCaseExecutions() { idTestCase = _test.id };

            //registra inicio
            _testexec.dtBegin = DateTime.Now;
            InitTestCase(_test.Function, _testexec.dtBegin);

            //ejecuta testcase
            functions.quijote quijoteObject = new functions.quijote();
            quijoteObject.ConcatQuijoteStringBuilder();

            //registra fin
            _testexec.dtEnd = DateTime.Now;
            EndTestCase(_test.Function, _testexec);

            return _test;
        }
    }
}
