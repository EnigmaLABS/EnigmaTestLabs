using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ZmLabsObjects;
using ZmLabsBusiness;

namespace ZmLabsMonitor.controls
{
    public partial class usrctrl_testinfo_detalles : UserControl
    {
        test_object _testobject;

        public usrctrl_testinfo_detalles(test_object p_testobject)
        {
            InitializeComponent();
            _testobject = p_testobject;
        }

        private void usrctrl_testinfo_detalles_Load(object sender, EventArgs e)
        {
            txtDesc2.Text = _testobject.description;

            ShowTestCases();
        }

        private void lstCases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCases.SelectedItems.Count == 1)
            {
                TestCases _testcase = (TestCases)lstCases.SelectedItems[0].Tag;

                txtCaseDesc.Text = _testcase.Description;
            }
            else
            {
                txtCaseDesc.Text = "";
            }
        }

        private void ShowTestCases()
        {
            lstCases.Items.Clear();

            foreach (TestCases _tc in _testobject.execution.testcases)
            {
                ListViewItem lstIt = new ListViewItem(_tc.Function);
                lstIt.Tag = _tc;

                lstCases.Items.Add(lstIt);
            }
        }

        public void AddTestCase(TestCases _testcase)
        {
            _testobject.execution.testcases.Add(_testcase);
            ShowTestCases();
        }

        private void picNewTestCase_Click(object sender, EventArgs e)
        {
            test_functions _functions = new test_functions();
            _functions.SetTestObject(_testobject);

            subforms.frm_newcase _frm = new subforms.frm_newcase(_functions, this);
            _frm.ShowDialog();
        }
    }
}
