using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZmLabsObjects;
using ZMLabsData;

namespace ZmLabsBusiness
{
    public class test_functions : test_object 
    {
        private data_tests _datatestobj;

        public test_functions()
        {
            data.data_functions _df = new data.data_functions();

            _datatestobj = new data_tests(_df.GetLabsCnx());
        }

        public List<Categories> getCategories()
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

        public bool TestRecord(TestCases _testcase)
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

        public List<test_object> getTests()
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

        public bool insertTest()
        {
            bool res;

            try
            {
                res = _datatestobj.insertTest(this.test, this.classname, this.description, this.categorie.id);
            }
            catch (Exception)
            {
                return false;
            }

            return res;
        }

        public TestCases insertTestCase(string functionName, string Description)
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

        #region TestObject

        public void SetTestObject(test_object _testobject)
        {
            this.id = _testobject.id;
            this.description = _testobject.description;
            this.classname = _testobject.classname;

            this.url_blog = _testobject.url_blog;
            this.url_git = _testobject.url_git;

            this.categorie = _testobject.categorie;
            this.execution = _testobject.execution;
            this.test = _testobject.test;
        }

        #endregion
    }
}
