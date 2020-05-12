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

        public Domain_Test_Functions_EF(contracts.ITestFunctionsDomain p_TestFunctions)
        {
            //data.data_functions _df = new data.data_functions();
            //_datatestrepository = new labs_repos(_df.GetLabsCnx());

            TestFunctions = p_TestFunctions;
        }

        public List<CategoriesDomain> getCategories()
        {
            List<CategoriesDomain> res = TestFunctions.getCategories();
            return res;
        }

        public List<TestDomain> getTests()
        {
            List<TestDomain> res = new List<TestDomain>();
            res = TestFunctions.getTests();

            return res;
        }

        public bool insertTest(TestDomain _test)
        {
            bool res = TestFunctions.insertTest(_test);
            return res;
        }

        public TestCasesDomain insertTestCase(TestCasesDomain _testcase)
        {
            TestCasesDomain res = TestFunctions.insertTestCase(_testcase);
            return _testcase;
        }

        public bool InsertExecution(TestCaseExecutionsDomain _testCaseExec)
        {
            bool res = TestFunctions.InsertExecution(_testCaseExec);
            return res;
        }

    }
}
