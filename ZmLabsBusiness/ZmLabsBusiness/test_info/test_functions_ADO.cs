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
    public class test_functions_ADO : test_info.test_functions_base
    {
        private data_tests _datatestobj;

        /// <summary>
        /// Enlaza con el acceso a datos mediante ADO.NET
        /// </summary>
        /// <param name="TestObject"></param>
        public test_functions_ADO(test_object TestObject) : base(data_object.enumDataSystem.ADO, TestObject)
        {
            data.data_functions _df = new data.data_functions();
            
            _datatestobj = new data_tests(_df.GetLabsCnx());        
        }

        public override List<Categories> getCategories()
        {
            List<Categories> res = new List<Categories>();

            try
            {
                res = _datatestobj.getCategories();

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
                res = _datatestobj.insertTest(this.Test, this.Classname, this.Description, this.Categorie.id);
            }
            catch (Exception)
            {
                return false;
            }

            return res;
        }


        //-->> TODO: Implementar en EF

        public override bool TestRecord(TestCases _testcase)
        {
            bool res;

            try
            {
                res = _datatestobj.InsertExecution(_testcase);
            }
            catch (Exception ex)
            {
                return false;
            }

            return res;
        }

        public override TestCases insertTestCase(string functionName, string Description)
        {
            TestCases res = new TestCases();

            try
            {
                Int64 idTestCase = _datatestobj.insertTestCase(this.id, functionName, Description);

                if (idTestCase != 0)
                {
                    res.id = idTestCase;
                    res.Function = functionName;
                    res.Description = Description;
                }
            }
            catch (Exception)
            {

            }

            return res;
        }


    }
}
