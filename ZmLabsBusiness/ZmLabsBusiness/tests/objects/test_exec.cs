using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using ZmLabsBusiness.test_info;
using ZmLabsObjects;
using ZMLabsData.repos;

using NLog;

namespace ZmLabsBusiness.tests.objects
{
    /// <summary>
    /// Clase base de un Test específico
    /// Heredan las clases específicas de cada uno de los test: test1_x , test2_x etc...
    /// Gestiona la ejecución del test y la lectura/escritura que los test van generando, así como el estado de la ejecución
    /// </summary>
    public class test_base
    {
        public test_types.enumEstadoProceso Estado;

        public List<test_types.mensajes> Mensajes = new List<test_types.mensajes>();

        private static int intentos_mensajes = 0;

        public test_functions_base _testobject;

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public test_base(test_functions_base p_testobject)
        {
            _testobject = p_testobject;
        }

        /// <summary>
        /// Inicia la ejecución del Test seleccionado
        /// Sobreescrito en cada una de las clases de los test: test1_x , test2_x etc...
        /// </summary>
        public virtual void Start() {  }

        public void InitTest()
        {
            this.Estado = test_types.enumEstadoProceso.Ejecutando;

            SetMsg("- - - - -");
            SetMsg(_testobject.Test + " iniciado a las " + DateTime.Now.ToLongTimeString());
        }

        public void EndTest()
        {
            SetMsg("- - - - -");
            SetMsg(_testobject.Test + " finalizado a las " + DateTime.Now.ToLongTimeString());

            this.Estado = test_types.enumEstadoProceso.Finalizado;
        }

        public void InitTestCase(string casename, DateTime dtBegin)
        {
            SetMsg("- - - - -");
            SetMsg(casename + " Case iniciado a las " + dtBegin.ToShortTimeString() + " " + dtBegin.ToLongTimeString());
        }

        public void EndTestCase(string casename, TestCaseExecutionsDomain _testexec)
        {
            try
            {
                _testobject.InsertExecution(_testexec);
            }
            catch (Exception ex)
            {
                SetMsg("Ha habido un problema al grabar los resultados en BBDD. Revise el fichero de erores");

                _logger.Error(ex, "Error en EndTestCase(" + casename + ")");
            }
            finally
            {
                SetMsg(casename + " Case finalizado a las " + _testexec.dtEnd);
                SetMsg(casename + " Case ejecutado en " + _testexec.Miliseconds + " milisegundos");
            }
        }

        public void SetMsgLeido(Guid id)
        {
            reintenta:
            try
            {
                this.Mensajes.Where(idx => idx.id == id).First().leido = true;
            }
            catch (InvalidOperationException ex_inv)
            {
                intentos_mensajes++;
                _logger.Warn(ex_inv, "Lista modificada en SetMsgLeido - Intento  " + intentos_mensajes.ToString());
                
                Thread.Sleep(100);

                if (intentos_mensajes < 8539)
                {
                    goto reintenta;
                }
            }
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
