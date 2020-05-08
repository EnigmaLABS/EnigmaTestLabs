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
    }

    public class CategoriesDomain
    {
        public int id { get; set; }
        public string Categorie { get; set; } = "";

        public CategoriesDomain Categorie_dad { get; set; }
    }

    public class ExecutionPropertiesDomain
    {
        public enumTestTypes TestType;

        public DateTime dtBegin;
        public DateTime dtEnd;

        public Object OBJ;
    }

    public class TestCasesDomain
    {
        public Int64 id { get; set; }
        public int Orden { get; set; }
        public string Function { get; set; }
        public string Description { get; set; }

        public Int64 idTest { get; set; }
    }

    public class TestCaseExecutionsDomain
    {
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
    }

}
