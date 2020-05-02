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
    public class test_functions_base : TestDomain
    {
        //private test_functions_base neg_object;

        public test_functions_base()  {  }

        public virtual List<CategoriesDomain> getCategories() { return new List<CategoriesDomain>(); }

        public virtual List<TestDomain> getTests() { return new List<TestDomain>(); }

        public virtual bool insertTest() { return false; }

        public virtual TestCasesDomain insertTestCase(TestCasesDomain testCase) { return new TestCasesDomain(); }

        public virtual bool InsertExecution(TestCaseExecutionsDomain _testCaseExec) { return true; }


        //-->>
        #region TestObject

        public void SetTestObject(TestDomain _testobject)
        {
            this.id = _testobject.id;
            this.Description = _testobject.Description;
            this.Classname = _testobject.Classname;

            this.Url_blog = _testobject.Url_blog;
            this.Url_git = _testobject.Url_git;

            this.Categorie = _testobject.Categorie;
            this.Execution = _testobject.Execution;
            this.TestCases = _testobject.TestCases;
            this.Test = _testobject.Test;
        }

        public TestDomain GetTestObject()
        {
            TestDomain res = new TestDomain()
            {
                id = this.id,
                Description = this.Description,
                Classname = this.Classname,

                Url_blog = this.Url_blog,
                Url_git = this.Url_git,

                Categorie = this.Categorie,
                Execution = this.Execution,
                Test = this.Test,

                TestCases = this.TestCases
            };

            return res;
        }

        #endregion
    }
}
