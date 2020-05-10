using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ZmLabsObjects;

namespace ZmLabsObjects.functions
{
    public class Domain_Test_Functions_EF : contracts.ITestFunctionsDomain
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private contracts.ITestFunctionsDomain TestFunctions;

        /// <summary>
        /// Enlaza con el acceso a datos mediante Entity Framework
        /// </summary>
        /// <param name="TestObject"></param>
        public Domain_Test_Functions_EF(contracts.ITestFunctionsDomain p_TestFunctions)
        {
            //data.data_functions _df = new data.data_functions();
            //_datatestrepository = new labs_repos(_df.GetLabsCnx());

            TestFunctions = p_TestFunctions;
        }

        public List<CategoriesDomain> getCategories()
        {
            List<CategoriesDomain> res = new List<CategoriesDomain>();

            try
            {
                res = TestFunctions.getCategories();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en getCategories");
            }

            return res;
        }

        public List<TestDomain> getTests()
        {
            List<TestDomain> res = new List<TestDomain>();

            try
            {
                res = TestFunctions.getTests();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en getTests");
            }

            return res;
        }

        public bool insertTest(TestDomain _test)
        {
            bool res;

            try
            {
                res = TestFunctions.insertTest(_test);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en insertTest");

                //TODO - Guardar test de contexto en JSON

                return false;
            }

            return res;
        }

        public TestCasesDomain insertTestCase(TestCasesDomain _testcase)
        {
            TestCasesDomain res;

            try
            {
                int maxorden = ((from x in _testcase.Test.TestCases select x.Orden).Max())+1;

                _testcase.Orden = maxorden;

                res = TestFunctions.insertTestCase(_testcase);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en insertTestCase");

                //TODO - Guardar testcase de contexto en JSON
            }

            return _testcase;
        }

        public bool InsertExecution(TestCaseExecutionsDomain _testCaseExec)
        {
            bool res = TestFunctions.InsertExecution(_testCaseExec);
            return res;
        }

    }
}
