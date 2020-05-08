using System;
using System.Windows.Forms;

using ZmLabsBusiness.test_info;
using ZmLabsObjects;
using static ZmLabsObjects.DataDomain;

namespace ZmLabsMonitor.controls
{
    public partial class usrctrl_testinfo : UserControl
    {
        private test_functions_base _testobject;
        private usrctrl_testinfo_detalles _ctrl_test_info_details;
        private usrctrl_monitorlist _ctrl_test_exec_info;

        /// <summary>
        /// Formulario que muestra la información de un test seleccionado en el árbol del formulario principal
        /// Da acceso al historial de ejecuciones del test (pendiente desarrollo)
        /// ... y a la ejecución del mismo, que se desarrollaría en el control de usuario usrctrl_monitorlist
        /// </summary>
        /// <param name="p_testobject"></param>
        /// <param name="p_DataSystem"></param>
        public usrctrl_testinfo(test_functions_base p_testobject)
        {
            InitializeComponent();

            _testobject = p_testobject;
        }

        private void usrctrl_testinfo_Load(object sender, EventArgs e)
        {
            txtTest.Text = _testobject.Test;
            txtClassName.Text = _testobject.Classname;

            _ctrl_test_info_details = new usrctrl_testinfo_detalles(_testobject);
            _ctrl_test_exec_info = new usrctrl_monitorlist(this);

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

            _ctrl_test_exec_info.Activate(_testobject);
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
