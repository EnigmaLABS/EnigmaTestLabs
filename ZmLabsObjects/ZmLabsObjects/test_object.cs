using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmLabsObjects
{
    public enum enumTestTypes
    {
        test1_multithreading_vs_singlethreading,
        test2_basicos_concatstrings
    }

    public class test_object
    {
        public Int64 id { get; set; }
        public string Test { get; set; }
        public string Description { get; set; }
        public string Classname { get; set; }
        public string Url_blog { get; set; }
        public string Url_git { get; set; }
        public int idCategorie { get; set; }

        public Categories Categorie { get; set; }
        public ExecutionProperties Execution { get; set; } = new ExecutionProperties();
    }

    public class Categories
    {
        public int id { get; set; }
        public string Categorie { get; set; } = "";

        public Categories Categorie_dad { get; set; }
    }

    public class ExecutionProperties
    {
        public DateTime dtBegin;
        public DateTime dtEnd;

        public List<TestCases> testcases = new List<TestCases>();

        public List<string> log;

        public string ClassName;
        public enumTestTypes TestType;
        public Object OBJ;
    }

    public class TestCases
    {
        public Int64 id;
        public string Function;
        public string Description;
        public DateTime dtBegin;
        public DateTime dtEnd;
    }


}
