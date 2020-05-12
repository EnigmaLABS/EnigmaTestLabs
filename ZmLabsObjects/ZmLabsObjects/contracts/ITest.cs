using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmLabsObjects.contracts
{
    public interface ITest
    {
        void Start();

        void InitTest();

        void EndTest();

        void InitTestCase(string casename, DateTime dtBegin);

        void EndTestCase(string casename, TestCaseExecutionsDomain _testexec);


        //gestión de mensajes
        void SetMsgLeido(Guid id);

        void SetMsg(string Msg);
    }
}
