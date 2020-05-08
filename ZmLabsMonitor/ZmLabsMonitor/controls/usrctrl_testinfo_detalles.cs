using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ZmLabsObjects;
using ZmLabsBusiness.test_info;
using static ZmLabsObjects.DataDomain;

namespace ZmLabsMonitor.controls
{
    public partial class usrctrl_testinfo_detalles : UserControl
    {
        private test_functions_base _testobject;

        public usrctrl_testinfo_detalles(test_functions_base p_testobject)
        {
            InitializeComponent();

            _testobject = p_testobject;
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

            foreach (TestCasesDomain _tc in _testobject.TestCases.OrderBy(ord => ord.Orden))
            {
                ListViewItem lstIt = new ListViewItem(_tc.Function);
                lstIt.Tag = _tc;

                lstCases.Items.Add(lstIt);
            }
        }

        public void AddTestCase(TestCasesDomain _testcase)
        {
            _testobject.TestCases.Add(_testcase);
            ShowTestCases();
        }

        private void picNewTestCase_Click(object sender, EventArgs e)
        {
            subforms.frm_newcase _frm = new subforms.frm_newcase(_testobject, this);
            _frm.ShowDialog();
        }
    }
}
