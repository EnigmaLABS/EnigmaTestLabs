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

        public override List<Categories> getCategories()
        {
            List<Categories> res = new List<Categories>();

            try
            {
                res = _datatestrepository.getCategories();

            }
            catch (Exception ex)
            {

            }

            return res;
        }

        public override List<test_object> getTests()
        {
            List<test_object> res = new List<test_object>();

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

        public override TestCases insertTestCase(TestCases _testcase)
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

        public override bool InsertExecution(TestCaseExecutions _testCaseExec)
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
