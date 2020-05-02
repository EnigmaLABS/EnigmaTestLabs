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
            catch (Exception)
            {
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
            catch (Exception)
            {

            }

            return _testcase;
        }

        public override bool InsertExecution(TestCaseExecutionsDomain _testCaseExec)
        {
            bool res;

            try
            {
                res = _datatestrepository.InsertExecution(_testCaseExec);
            }
            catch (Exception)
            {
                return false;
            }

            return res;
        }
    }
}
