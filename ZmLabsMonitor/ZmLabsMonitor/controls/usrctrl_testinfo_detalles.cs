using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ZmLabsObjects;
using ZmLabsBusiness.test_info;
using static ZmLabsObjects.data_object;

namespace ZmLabsMonitor.controls
{
    public partial class usrctrl_testinfo_detalles : UserControl
    {
        private test_object _testobject;
        private enumDataSystem _DataSystem;

        public usrctrl_testinfo_detalles(test_object p_testobject, enumDataSystem p_DataSystem)
        {
            InitializeComponent();

            _testobject = p_testobject;
            _DataSystem = p_DataSystem;
        }

        private void usrctrl_testinfo_detalles_Load(object sender, EventArgs e)
        {
            txtDesc2.Text = _testobject.Description;

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

            foreach (TestCases _tc in _testobject.Execution.testcases)
            {
                ListViewItem lstIt = new ListViewItem(_tc.Function);
                lstIt.Tag = _tc;

                lstCases.Items.Add(lstIt);
            }
        }

        public void AddTestCase(TestCases _testcase)
        {
            _testobject.Execution.testcases.Add(_testcase);
            ShowTestCases();
        }

        private void picNewTestCase_Click(object sender, EventArgs e)
        {
            test_functions_base _functions = new test_functions_base(_DataSystem, _testobject);
            _functions.SetTestObject(_testobject);

            subforms.frm_newcase _frm = new subforms.frm_newcase(_functions, this);
            _frm.ShowDialog();
        }
    }
}
