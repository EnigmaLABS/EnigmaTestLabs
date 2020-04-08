using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZmLabsObjects;
using ZmLabsBusiness;
using ZmLabsBusiness.tests;

namespace ZmLabsBusiness.tests.objects
{
    public class test_exec
    {
        public test_types.enumEstadoProceso Estado;

        public List<test_types.mensajes> Mensajes = new List<test_types.mensajes>();
        public test_functions _testobject;

        public test_exec(test_functions p_testobject)
        {
            _testobject = p_testobject;
        }

        public void SetMsgLeido(Guid id)
        {
            Mensajes.Where(idx => idx.id == id).First().leido = true;
        }
    }
}
