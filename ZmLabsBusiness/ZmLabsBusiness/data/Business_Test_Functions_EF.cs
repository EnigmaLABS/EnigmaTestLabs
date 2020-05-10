using System;
using System.Collections.Generic;
using System.Linq;

using ZmLabsObjects;

using ZMLabsData.contracts;

namespace ZmLabsBusiness.data
{
    public class Business_Test_Functions_EF : Business_Test_Functions_base
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public Business_Test_Functions_EF(ITestRepository p_data_repository) :base(p_data_repository) {  }

        public override List<CategoriesDomain> getCategories()
        {
            List<CategoriesDomain> res = new List<CategoriesDomain>();

            try
            {
                res = TestRepos.getCategories();

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en getCategories");
            }

            return res;
        }

        public override List<TestDomain> getTests()
        {
            List<TestDomain> res = new List<TestDomain>();

            try
            {
                res = TestRepos.getTests();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en getTests");
            }

            return res;
        }

        public override bool insertTest(TestDomain _test)
        {
            bool res;

            try
            {
                res = TestRepos.insertTest(_test);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en insertTest");

                //TODO - Guardar test de contexto en JSON

                return false;
            }

            return res;
        }

        public override TestCasesDomain insertTestCase(TestCasesDomain _testcase)
        {
            bool res;

            try
            {
                int maxorden = ((from x in _testcase.Test.TestCases select x.Orden).Max()) + 1;

                _testcase.Orden = maxorden;

                res = TestRepos.insertTestCase(ref _testcase);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en insertTestCase");

                //TODO - Guardar testcase de contexto en JSON
            }

            return _testcase;
        }

        public override bool InsertExecution(TestCaseExecutionsDomain _testCaseExec)
        {
            bool res = TestRepos.InsertExecution(_testCaseExec);
            return res;
        }
    }
}
