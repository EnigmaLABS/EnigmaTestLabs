using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZmLabsObjects;
using ZMLabsData;
using ZMLabsData.repos;

namespace ZmLabsBusiness.test_info
{
    //ISOLATED
    class test_functions_ADO_ISOLATED : test_info.test_functions_base
    {
        private data_tests_info _datatestobj;

        /// <summary>
        /// Enlaza con el acceso a datos mediante ADO.NET
        /// </summary>
        /// <param name="TestObject"></param>
        public test_functions_ADO_ISOLATED() 
        {
            data.data_functions _df = new data.data_functions();
            
            _datatestobj = new data_tests_info(_df.GetLabsCnx());        
        }

        public override List<CategoriesDomain> getCategories()
        {
            List<CategoriesDomain> res = new List<CategoriesDomain>();

            try
            {
                res = _datatestobj.getCategories();

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
                res = _datatestobj.getTests();
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
                res = _datatestobj.insertTest(this.Test, this.Classname, this.Description, this.idCategorie);
            }
            catch (Exception)
            {
                return false;
            }

            return res;
        }

        public override bool InsertExecution(TestCaseExecutionsDomain _testCaseExec)
        {
            bool res;

            try
            {
                res = _datatestobj.InsertExecution(_testCaseExec);
            }
            catch (Exception ex)
            {
                return false;
            }

            return res;
        }

        public override TestCasesDomain insertTestCase(TestCasesDomain _testcase)
        {
            try
            {
                Int64 idTestCase = _datatestobj.insertTestCase(_testcase.idTest, _testcase.Function, _testcase.Description);

                if (idTestCase != 0)
                {
                    _testcase.id = idTestCase;
                }
            }
            catch (Exception)
            {

            }

            return _testcase;
        }
    }
}
