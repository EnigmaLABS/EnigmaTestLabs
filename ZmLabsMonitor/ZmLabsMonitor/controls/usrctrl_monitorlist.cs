using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using ZmLabsObjects;
using ZmLabsBusiness;
using ZmLabsBusiness.tests.objects;
using ZmLabsBusiness.tests;

namespace ZmLabsMonitor.controls
{
    public partial class usrctrl_monitorlist : UserControl
    {
        public static test_functions _testobject;
        public static test_types.enumEstadoProceso _estadoProceso;

        private usrctrl_testinfo _container;

        public usrctrl_monitorlist(usrctrl_testinfo p_container)
        {
            InitializeComponent();

            _container = p_container;
        }

        public void Activate(test_functions p_testobject)
        {
            _testobject = p_testobject;
            _estadoProceso = test_types.enumEstadoProceso.Parado;

            Thread _th = new Thread(() => HiloNegocio());
            _th.Start();

            this.Cursor = Cursors.WaitCursor;
            Thread.Sleep(3000);

            timerControl.Enabled = true;
        }

        private void timerControl_Tick(object sender, EventArgs e)
        {
            List<test_types.mensajes> lstMensajes = new List<test_types.mensajes>();

            test_exec execObject = (test_exec)_testobject.execution.OBJ;

            _estadoProceso = execObject.Estado;

            lstMensajes = execObject.Mensajes.Where(msg => msg.leido == false).ToList();

            foreach (var msg in lstMensajes)
            {
                execObject.SetMsgLeido(msg.id);
            }

            //_testobject.execution.OBJ = execObject;

            //switch (_testobject.execution.TestType)
            //{
            //    case enumTestTypes.test1_multithreading_vs_singlethreading:

            //        var obj2 = (test1_multithreading_vs_singlethreading)_testobject.execution.OBJ;

            //        _estadoProceso = obj2.Estado;
            //        lstMensajes = obj2.Mensajes.Where(msg => msg.leido == false).ToList();

            //        foreach (var msg in lstMensajes)
            //        {
            //            obj2.SetMsgLeido(msg.id);
            //        }

            //        _testobject.execution.OBJ = (test1_multithreading_vs_singlethreading)obj2;

            //        break;
            //}

            Application.DoEvents();

            if (_estadoProceso == test_types.enumEstadoProceso.Ejecutando)
            {
                foreach (var msg in lstMensajes)
                {
                    ListViewItem lstIt = new ListViewItem(msg.mensaje);
                    lstMonitor.Items.Add(lstIt);
                }

                lstMensajes.Clear();
            }
            else if (_estadoProceso == test_types.enumEstadoProceso.Finalizado)
            {
                timerControl.Enabled = false;

                //última lectura de mensajes
                foreach (var msg in lstMensajes)
                {
                    ListViewItem lstIt = new ListViewItem(msg.mensaje);
                    lstMonitor.Items.Add(lstIt);
                }

                lstMensajes.Clear();

                //volvemos a establecer el cursor por defecto
                _container.SetDefaultCursor();

                this.Cursor = Cursors.Default;
            }
        }

        public static void HiloNegocio()
        {
            var negobject = (test_exec)_testobject.execution.OBJ;
            negobject.Start();

            //switch (_testobject.execution.TestType)
            //{
            //    case enumTestTypes.test1_multithreading_vs_singlethreading:

            //        var obj2 = (test1_multithreading_vs_singlethreading)_testobject.execution.OBJ;
            //        obj2.Start();

            //        break;
            //}
        }
    }
}
