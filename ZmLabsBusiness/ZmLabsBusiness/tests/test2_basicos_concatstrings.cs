using System;
using System.Linq;

using ZmLabsObjects;
using ZmLabsObjects.contracts;

using ZmLabsBusiness.functions;

namespace ZmLabsBusiness.tests
{
    public class test2_basicos_concatstrings : objects.test_base
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private static objects.test_base _testexecbase;

        public test2_basicos_concatstrings(TestDomain p_test,
                                           ITestFunctionsDomain p_DomainFunctions) : base(p_test, p_DomainFunctions)
        {
            _testexecbase = this;
        }

        public override void Start()
        {
            try
            {
                //inicia test
                this.InitTest();

                //recorre y ejecuta testcases
                int cont = 0;

                while (cont < Test.TestCases.Count)
                {
                    TestCasesDomain _testcase = Test.TestCases.Where(ord => ord.Orden == cont + 1).First();

                    switch (_testcase.Function)
                    {
                        case "Concat_PlusOperator":

                            Concat_PlusOperator(ref _testcase);
                            break;

                        case "Concat_StringBuilder":

                            Concat_StringBuilder(ref _testcase);
                            break;
                    }

                    cont++;
                }

                //finaliza test
                this.EndTest();
            }
            catch (Exception ex)
            {
                _testexecbase.SetMsg("Error ejecutando test2_basicos_concatstrings - Start");
                _logger.Error(ex, "Error ejecutando test2_basicos_concatstrings - Start");
            }
        }

        private void Concat_PlusOperator(ref TestCasesDomain _testcase)
        {
            _testcase.TestCaseExecution.idTestCase = _testcase.id;

            try
            {
                //registra inicio

                _testcase.TestCaseExecution.dtBegin = DateTime.Now;
                InitTestCase(_testcase.Function, _testcase.TestCaseExecution.dtBegin);

                //ejecuta testcase
                quijote_functions.ConcatQuijotePlusOperator();

                //registra fin
                _testcase.TestCaseExecution.dtEnd = DateTime.Now;
                EndTestCase(_testcase.Function, _testcase.TestCaseExecution);
            }
            catch (Exception ex)
            {
                _testexecbase.SetMsg("Error ejecutando Concat_PlusOperator");
                _logger.Error(ex, "Error ejecutando Concat_PlusOperator");
            }
        }

        private void Concat_StringBuilder(ref TestCasesDomain _testcase)
        {
            _testcase.TestCaseExecution.idTestCase = _testcase.id;

            try
            {
                //registra inicio
                _testcase.TestCaseExecution.dtBegin = DateTime.Now;
                InitTestCase(_testcase.Function, _testcase.TestCaseExecution.dtBegin);

                //ejecuta testcase
                quijote_functions.ConcatQuijoteStringBuilder();

                //registra fin
                _testcase.TestCaseExecution.dtEnd = DateTime.Now;
                EndTestCase(_testcase.Function, _testcase.TestCaseExecution);
            }
            catch (Exception ex)
            {
                _testexecbase.SetMsg("Error ejecutando Concat_StringBuilder");
                _logger.Error(ex, "Error ejecutando Concat_StringBuilder");
            }
        }
    }
}
