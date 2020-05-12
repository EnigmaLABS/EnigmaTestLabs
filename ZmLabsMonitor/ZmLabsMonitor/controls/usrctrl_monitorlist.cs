using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using ZmLabsBusiness;
using ZmLabsBusiness.tests.objects;

using ZmLabsObjects;

namespace ZmLabsMonitor.controls
{
    public partial class usrctrl_monitorlist : UserControl
    {
        public static TestDomain Test;

        public static test_types.enumEstadoProceso _estadoProceso;

        private usrctrl_testinfo _container;

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Control de usuario que invoca la ejecución de un Test y va mostrando los resultados de la ejecución en pantalla
        /// </summary>
        /// <param name="p_container"></param>
        public usrctrl_monitorlist(usrctrl_testinfo p_container, TestDomain p_test)
        {
            InitializeComponent();

            _container = p_container;
            Test = p_test;
        }

        public void Activate()
        {
            _estadoProceso = test_types.enumEstadoProceso.Parado;

            Thread _th = new Thread(() => HiloNegocio());
            _th.Start();

            this.Cursor = Cursors.WaitCursor;
            Thread.Sleep(3000);

            timerControl.Interval = 5500;
            timerControl.Enabled = true;
        }

        private void timerControl_Tick(object sender, EventArgs e)
        {
            List<test_types.mensajes> lstMensajes = new List<test_types.mensajes>();

            test_base execObject = (test_base)Test.Execution.TESTOBJ;

            _estadoProceso = execObject.Estado;

            try
            {
                lstMensajes = execObject.Mensajes.Where(msg => msg.leido == false).ToList();
            }
            catch (InvalidOperationException)
            {
                _logger.Warn("Colección modificada");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al leer los mensajes - timerControl_Tick ");
            }

            foreach (var msg in lstMensajes)
            {
                execObject.SetMsgLeido(msg.id);
            }

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

                //volvemos a establecer el cursor por defecto
                _container.SetDefaultCursor();

                this.Cursor = Cursors.Default;
            }
        }

        public static void HiloNegocio()
        {
            var negobject = (test_base)Test.Execution.TESTOBJ;
            negobject.Start();
        }
    }
}
