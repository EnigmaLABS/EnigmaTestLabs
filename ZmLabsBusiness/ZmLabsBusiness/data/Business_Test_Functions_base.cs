using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZmLabsObjects;
using ZmLabsObjects.contracts;

using ZMLabsData.contracts;

namespace ZmLabsBusiness.data
{
    public class Business_Test_Functions_base : ITestFunctionsDomain
    {
        public ITestRepository TestRepos;

        public Business_Test_Functions_base(ITestRepository p_data_repository)
        {
            TestRepos = p_data_repository;
        }

        public virtual List<CategoriesDomain> getCategories() { return new List<CategoriesDomain>(); }

        public virtual List<TestDomain> getTests() { return new List<TestDomain>(); }

        public virtual bool insertTest(TestDomain _test) { return false; }

        public virtual TestCasesDomain insertTestCase(TestCasesDomain testCase) { return new TestCasesDomain(); }

        public virtual bool InsertExecution(TestCaseExecutionsDomain _testCaseExec) { return true; }
    }
}
