using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZmLabsObjects;

namespace ZMLabsData.contracts
{
    public interface ITestRepository
    {
        List<CategoriesDomain> getCategories();

        List<TestDomain> getTests();

        bool insertTest(TestDomain _test);

        bool insertTestCase(ref TestCasesDomain testCase);

        bool InsertExecution(TestCaseExecutionsDomain _testCaseExec);
    }
}
