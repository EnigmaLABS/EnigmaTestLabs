using System;
using System.Collections.Generic;
using System.Linq;

namespace ZmLabsObjects
{
    public enum enumTestTypes
    {
        test1_multithreading_vs_singlethreading,
        test2_basicos_concatstrings,
        test3_sql_loaddata
    }

    public class TestDomain
    {
        private contracts.ITestFunctionsDomain TestFunctions;

        //constructores
        public TestDomain() {  }

        public TestDomain(contracts.ITestFunctionsDomain p_TestFunctions)
        {
            TestFunctions = p_TestFunctions;
        }

        //propiedades
        public Int64 id { get; set; }
        public string Test { get; set; }
        public string Description { get; set; }
        public string Classname { get; set; }
        public string Url_blog { get; set; }
        public string Url_git { get; set; }
        public int idCategorie { get; set; }

        public CategoriesDomain Categorie { get; set; }
        public ExecutionPropertiesDomain Execution { get; set; } = new ExecutionPropertiesDomain();
        public List<TestCasesDomain> TestCases { get; set; }

        //métodos
        public List<TestDomain> Get()
        {
            return TestFunctions.getTests();
        }

        public bool Insert()
        {
            return TestFunctions.insertTest(this);
        }
    }

    public class CategoriesDomain
    {
        private contracts.ITestFunctionsDomain TestFunctions;

        //constructores
        public CategoriesDomain() {  }

        public CategoriesDomain(contracts.ITestFunctionsDomain p_TestFunctions)
        {
            TestFunctions = p_TestFunctions;
        }

        //propiedades
        public int id { get; set; }
        public string Categorie { get; set; } = "";

        public CategoriesDomain Categorie_dad { get; set; }

        //métodos
        public List<CategoriesDomain> Get(contracts.ITestFunctionsDomain p_TestFunctions)
        {
            TestFunctions = p_TestFunctions;
            return TestFunctions.getCategories();
        }
    }

    public class ExecutionPropertiesDomain
    {
        public ExecutionPropertiesDomain() {  }

        public enumTestTypes TestType;

        public contracts.ITest OBJ;
        //public Object OBJ;
    }

    public class TestCasesDomain
    {
        private static contracts.ITestFunctionsDomain MyTestFunctions;

        //constructores
        public TestCasesDomain() {  }

        public TestCasesDomain(contracts.ITestFunctionsDomain p_TestFunctions)
        {
            MyTestFunctions = p_TestFunctions;
        }

        //propiedades
        public Int64 id { get; set; }
        public int Orden { get; set; }
        public string Function { get; set; }
        public string Description { get; set; }

        public TestDomain Test { get; set; }
        public TestCaseExecutionsDomain TestCaseExecution { get; set; } = new TestCaseExecutionsDomain(MyTestFunctions);

        public Int64 idTest { get; set; }

        //métodos
        public TestCasesDomain Insert()
        {
            return MyTestFunctions.insertTestCase(this);
        }
    }

    public class TestCaseExecutionsDomain
    {
        private contracts.ITestFunctionsDomain TestFunctions;

        //constructores
        public TestCaseExecutionsDomain() {  }

        public TestCaseExecutionsDomain(contracts.ITestFunctionsDomain p_TestFunctions)
        {
            TestFunctions = p_TestFunctions;
        }

        //propiedades
        public Int64 id { get; set; }
        public Int64 idTestCase { get; set; } 

        public DateTime dtBegin { get; set; }
        public DateTime dtEnd { get; set; }

        public double Miliseconds
        {
            get
            {
                TimeSpan _ts = this.dtEnd - this.dtBegin;
                return _ts.TotalMilliseconds;
            }
        }

        //métodos
        public bool Insert(contracts.ITestFunctionsDomain p_TestFunctions)
        {
            TestFunctions = p_TestFunctions;
            return TestFunctions.InsertExecution(this);
        }
    }
}
