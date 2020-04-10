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
        public Int64 id;
        public string Test;
        public string Description;
        public string Classname;
        public string Url_blog;
        public string Url_git;

        public Categories Categorie = new Categories();
        public ExecutionProperties Execution = new ExecutionProperties();
    }

    public class Categories
    {
        public int id;
        public string Categorie;

        public Categories Categorie_dad;
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
