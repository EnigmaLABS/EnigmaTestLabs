using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZmLabsObjects;

namespace ZmLabsObjects.functions
{
    //ISOLATED
    class test_functions_ADO_ISOLATED : contracts.ITestFunctionsDomain
    {
        private contracts.ITestFunctionsDomain TestFunctions;

        public test_functions_ADO_ISOLATED(contracts.ITestFunctionsDomain p_TestFunctions) 
        { 
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
            catch (Exception)
            {
                return false;
            }

            return res;
        }

        public bool InsertExecution(TestCaseExecutionsDomain _testCaseExec)
        {
            bool res;

            try
            {
                res = TestFunctions.InsertExecution(_testCaseExec);
            }
            catch (Exception ex)
            {
                return false;
            }

            return res;
        }

        public TestCasesDomain insertTestCase(TestCasesDomain _testcase)
        {
            try
            {
                _testcase = TestFunctions.insertTestCase(_testcase);
            }
            catch (Exception)
            {

            }

            return _testcase;
        }
    }
}
