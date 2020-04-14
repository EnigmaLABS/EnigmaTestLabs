using System;
using System.Windows.Forms;

using ZmLabsBusiness.test_info;
using ZmLabsObjects;

namespace ZmLabsMonitor.subforms
{

    public partial class frm_newcase : Form
    {
        private test_functions_base _test;
        private controls.usrctrl_testinfo_detalles _container;

        /// <summary>
        /// Formulario para la creación de un nuevo TestCase asociado a un Test
        /// </summary>
        /// <param name="p_test"></param>
        /// <param name="p_container"></param>
        public frm_newcase(test_functions_base p_test, controls.usrctrl_testinfo_detalles p_container)
        {
            InitializeComponent();

            _test = p_test;
            _container = p_container;
        }

        private void frm_newcase_Load(object sender, EventArgs e)
        {
            txtFunctionName.Focus();
        }

        private void picSave_Click(object sender, EventArgs e)
        {
            if (txtFunctionName.Text.Trim().Length > 1)
            {
                TestCases res = _test.insertTestCase(txtFunctionName.Text.Trim(), txtDesc.Text.Trim());

                _container.AddTestCase(res);

                this.Close();
            }
            else
            {
                MessageBox.Show("Informe el nombre de la función");
            }
        }
    }
}
