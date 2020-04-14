using System;
using System.Collections.Generic;
using System.Linq;

using ZmLabsObjects;

namespace ZmLabsBusiness.test_info
{
    /// <summary>
    /// Clase base para la gestión de la información relacionada con los test: Tests, categorías, TestCses y Ejecuciones
    /// Heredan las clases derivadas test_functions_EF y test_functions_ADO que comparten los mismos métodos, cuyas funcionalidades son equivalentes
    /// pero en una clase de implementa el acceso a datos con ADO.NET y en la otra con Entity Framework
    /// </summary>
    public class test_functions_base : test_object
    {
        private test_functions_base neg_object;

        public test_functions_base(data_object.enumDataSystem DataSystem, test_object TestObject)
        {
            switch (DataSystem)
            {
                case data_object.enumDataSystem.ADO:

                    neg_object = new test_functions_ADO(TestObject); 
                    break;

                case data_object.enumDataSystem.EntityFramework:

                    neg_object = new test_functions_EF(TestObject);
                    break;
            }        
        }

        public virtual List<Categories> getCategories()
        {
            return neg_object.getCategories();
        }

        public virtual List<test_object> getTests()
        {
            return neg_object.getTests();
        }

        public virtual bool insertTest()
        {
            return neg_object.insertTest();
        }

        // TODO: Implementar con EF
        public virtual bool TestRecord(TestCases _testcase)
        {
            return true;
        }

        public virtual TestCases insertTestCase(string functionName, string Description)
        {
            TestCases res = new TestCases();
            return res;
        }


        //-->>
        #region TestObject

        public void SetTestObject(test_object _testobject)
        {
            this.id = _testobject.id;
            this.Description = _testobject.Description;
            this.Classname = _testobject.Classname;

            this.Url_blog = _testobject.Url_blog;
            this.Url_git = _testobject.Url_git;

            this.Categorie = _testobject.Categorie;
            this.Execution = _testobject.Execution;
            this.Test = _testobject.Test;
        }

        #endregion
    }
}
