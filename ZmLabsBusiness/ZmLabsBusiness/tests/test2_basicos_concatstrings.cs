﻿using System;

using ZmLabsObjects;
using ZmLabsBusiness.functions.contracts;

namespace ZmLabsBusiness.tests
{
    public class test2_basicos_concatstrings : objects.test_base
    {
        private IQuijote QuijoteFunctions;
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private static objects.test_base _testexecbase;

        public test2_basicos_concatstrings(test_info.test_functions_base p_testobject, IQuijote p_QuijoteFunctions) : base(p_testobject)
        {
            QuijoteFunctions = p_QuijoteFunctions;
            _testexecbase = this;
        }

        public override void Start()
        {
            //inicia test
            this.InitTest();

            //recorre y ejecuta testcases
            int cont = 0;

            while (cont < _testobject.TestCases.Count)
            {
                TestCasesDomain _test = _testobject.TestCases[cont];

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

        public TestCasesDomain Concat_PlusOperator(TestCasesDomain _test)
        {
            TestCaseExecutionsDomain _testexec = new TestCaseExecutionsDomain() { idTestCase = _test.id };

            try
            {
                //registra inicio
                _testexec.dtBegin = DateTime.Now;
                InitTestCase(_test.Function, _testexec.dtBegin);

                //ejecuta testcase
                QuijoteFunctions.ConcatQuijotePlusOperator();

                //registra fin
                _testexec.dtEnd = DateTime.Now;
                EndTestCase(_test.Function, _testexec);
            }
            catch (Exception ex)
            {
                _testexecbase.SetMsg("Error ejecutando Concat_PlusOperator");
                _logger.Error(ex, "Error ejecutando Concat_PlusOperator");
            }

            return _test;
        }

        public TestCasesDomain Concat_StringBuilder(TestCasesDomain _test)
        {
            TestCaseExecutionsDomain _testexec = new TestCaseExecutionsDomain() { idTestCase = _test.id };

            try
            {
                //registra inicio
                _testexec.dtBegin = DateTime.Now;
                InitTestCase(_test.Function, _testexec.dtBegin);

                //ejecuta testcase
                QuijoteFunctions.ConcatQuijoteStringBuilder();

                //registra fin
                _testexec.dtEnd = DateTime.Now;
                EndTestCase(_test.Function, _testexec);
            }
            catch (Exception ex)
            {
                _testexecbase.SetMsg("Error ejecutando Concat_StringBuilder");
                _logger.Error(ex, "Error ejecutando Concat_StringBuilder");
            }

            return _test;
        }
    }
}
