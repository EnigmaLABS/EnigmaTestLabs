using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmLabsObjects
{
    public enum enumTestTypes
    {
        test1_multithreading_vs_singlethreading
    }

    public class test_object
    {
        public Int64 id;
        public string test;
        public string description;
        public string classname;
        public string url_blog;
        public string url_git;

        public Categories categorie = new Categories();
        public ExecutionProperties execution = new ExecutionProperties();
    }

    public class Categories
    {
        public int id;
        public string categorie;

        public Categories categorie_dad;
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
