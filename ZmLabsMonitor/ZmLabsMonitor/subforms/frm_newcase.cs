using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ZmLabsBusiness;
using ZmLabsObjects;

namespace ZmLabsMonitor.subforms
{

    public partial class frm_newcase : Form
    {
        private test_functions _test;
        private controls.usrctrl_testinfo_detalles _container;

        public frm_newcase(test_functions p_test, controls.usrctrl_testinfo_detalles p_container)
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
