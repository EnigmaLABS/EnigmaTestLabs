using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ZmLabsObjects;
using ZmLabsObjects.contracts;

namespace ZmLabsMonitor.controls
{
    public partial class usrctrl_testinfo_detalles : UserControl
    {
        private TestDomain _test;
        private ITestFunctionsDomain TestFunctions;

        public usrctrl_testinfo_detalles(TestDomain p_test, ITestFunctionsDomain p_TestFunctions)
        {
            InitializeComponent();

            _test = p_test;
            TestFunctions = p_TestFunctions;
        }

        private void usrctrl_testinfo_detalles_Load(object sender, EventArgs e)
        {
            txtDesc2.Text = _test.Description;

            ShowTestCases();
        }

        private void lstCases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCases.SelectedItems.Count == 1)
            {
                TestCasesDomain _testcase = (TestCasesDomain)lstCases.SelectedItems[0].Tag;

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

            foreach (TestCasesDomain _tc in _test.TestCases.OrderBy(ord => ord.Orden))
            {
                ListViewItem lstIt = new ListViewItem(_tc.Function);
                lstIt.Tag = _tc;

                lstCases.Items.Add(lstIt);
            }
        }

        public void AddTestCase(TestCasesDomain _testcase)
        {
            _test.TestCases.Add(_testcase);
            ShowTestCases();
        }

        private void picNewTestCase_Click(object sender, EventArgs e)
        {
            subforms.frm_newcase _frm = new subforms.frm_newcase(_test, TestFunctions, this);
            _frm.ShowDialog();
        }
    }
}
