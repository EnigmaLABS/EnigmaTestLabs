using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZmLabsObjects;

namespace ZmLabsObjects.contracts
{
    public interface ITestFunctionsDomain
    {
        List<CategoriesDomain> getCategories(); 

        List<TestDomain> getTests();

        bool insertTest(TestDomain _test);

        TestCasesDomain insertTestCase(TestCasesDomain testCase);

        bool InsertExecution(TestCaseExecutionsDomain _testCaseExec);
       
    }
}
