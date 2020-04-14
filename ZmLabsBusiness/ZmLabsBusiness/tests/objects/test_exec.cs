using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using ZmLabsObjects;
using ZmLabsBusiness.test_info;
//using ZmLabsBusiness.tests;

namespace ZmLabsBusiness.tests.objects
{
    /// <summary>
    /// Clase base de un Test específico
    /// Heredan las clases específicas de cada uno de los test: test1_x , test2_x etc...
    /// Gestiona la ejecución del test y la lectura/escritura que los test van generando, así como el estado de la ejecución
    /// </summary>
    public class test_exec
    {
        public test_types.enumEstadoProceso Estado;

        public List<test_types.mensajes> Mensajes = new List<test_types.mensajes>();
        public test_functions_base _testobject;

        public test_exec(test_functions_base p_testobject)
        {
            _testobject = p_testobject;
        }

        /// <summary>
        /// Inicia la ejecución del Test seleccionado
        /// Sobreescrito en cada una de las clases de los test: test1_x , test2_x etc...
        /// </summary>
        public virtual void Start() {  }

        public void SetMsgLeido(Guid id)
        {
            Mensajes.Where(idx => idx.id == id).First().leido = true;
        }

        public void SetMsg(string Msg)
        {
            this.Mensajes.Add(new test_types.mensajes()
            {
                id = Guid.NewGuid(),
                mensaje = Msg,
                leido = false
            });
        }
    }
}
