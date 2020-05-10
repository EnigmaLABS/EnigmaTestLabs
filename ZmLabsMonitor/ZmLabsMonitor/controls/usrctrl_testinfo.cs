using System;
using System.Windows.Forms;

using ZmLabsObjects;
using ZmLabsObjects.contracts;

using static ZmLabsObjects.DataDomain;

namespace ZmLabsMonitor.controls
{
    public partial class usrctrl_testinfo : UserControl
    {
        private TestDomain Test;
        private ITestFunctionsDomain TestFunctions;

        private usrctrl_testinfo_detalles _ctrl_test_info_details;
        private usrctrl_monitorlist _ctrl_test_exec_info;

        /// <summary>
        /// Formulario que muestra la información de un test seleccionado en el árbol del formulario principal
        /// Da acceso al historial de ejecuciones del test (pendiente desarrollo)
        /// ... y a la ejecución del mismo, que se desarrollaría en el control de usuario usrctrl_monitorlist
        /// </summary>
        /// <param name="p_testobject"></param>
        /// <param name="p_DataSystem"></param>
        public usrctrl_testinfo(TestDomain p_test, ITestFunctionsDomain p_TestFunctions)
        {
            InitializeComponent();

            Test = p_test;
            TestFunctions = p_TestFunctions;
        }

        private void usrctrl_testinfo_Load(object sender, EventArgs e)
        {
            txtTest.Text = Test.Test;
            txtClassName.Text = Test.Classname;

            _ctrl_test_info_details = new usrctrl_testinfo_detalles(Test, TestFunctions);
            _ctrl_test_exec_info = new usrctrl_monitorlist(this, Test);

            _ctrl_test_exec_info.Dock = DockStyle.Fill;
            panelDetalle.Controls.Add(_ctrl_test_exec_info);
            _ctrl_test_exec_info.Visible = false;

            panelDetalle.Controls.Add(_ctrl_test_info_details);
        }

        private void cmdPlay_Click(object sender, EventArgs e)
        {
            SetButtonsColor((Button)sender);

            _ctrl_test_info_details.Visible = false;
            _ctrl_test_exec_info.Visible = true;

            this.Cursor = Cursors.WaitCursor;

            _ctrl_test_exec_info.Activate();
        }

        private void cmdInfo_Click(object sender, EventArgs e)
        {
            _ctrl_test_info_details.Visible = true;
            _ctrl_test_exec_info.Visible = false;

            SetButtonsColor((Button)sender);
        }

        private void cmdHistorico_Click(object sender, EventArgs e)
        {
            SetButtonsColor((Button)sender);
        }

        private void SetButtonsColor(Button _activeButton)
        {
            cmdHistorico.BackColor = System.Drawing.Color.WhiteSmoke;
            cmdPlay.BackColor = System.Drawing.Color.WhiteSmoke;
            cmdInfo.BackColor = System.Drawing.Color.WhiteSmoke;

            _activeButton.BackColor = System.Drawing.Color.Coral;
        }

        public void SetDefaultCursor()
        {
            this.Cursor = Cursors.Default;
        }

    }
}
