using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ZmLabsObjects;
using ZMLabsData.repos;

namespace ZmLabsBusiness.test_info
{
    public class test_functions_EF : test_info.test_functions_base
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private labs_repos _datatestrepository;

        /// <summary>
        /// Enlaza con el acceso a datos mediante Entity Framework
        /// </summary>
        /// <param name="TestObject"></param>
        public test_functions_EF() 
        {
            data.data_functions _df = new data.data_functions();

            _datatestrepository = new labs_repos(_df.GetLabsCnx());
        }

        public override List<CategoriesDomain> getCategories()
        {
            List<CategoriesDomain> res = new List<CategoriesDomain>();

            try
            {
                res = _datatestrepository.getCategories();

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
                res = _datatestrepository.getTests();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en getTests");
            }

            return res;
        }

        public override bool insertTest()
        {
            bool res;

            try
            {
                res = _datatestrepository.insertTest(this);
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
                res = _datatestrepository.insertTestCase(ref _testcase);
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
            bool res = _datatestrepository.InsertExecution(_testCaseExec);
            return res;
        }

    }
}
