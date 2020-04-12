using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using ZmLabsObjects;
using ZmLabsBusiness;

namespace ZmLabsMonitor.subforms
{
    public partial class frm_newtest : Form
    {
        private Categories _cat;
        private frmMonitor _container;

        public frm_newtest(Categories p_cat, frmMonitor p_container)
        {
            InitializeComponent();

            _cat = p_cat;
            _container = p_container;
        }

        private void picSave_Click(object sender, EventArgs e)
        {
            picSave.Enabled = false;

            test_functions _test = new test_functions()
            {
                Test = txtTest.Text,
                Classname = txtClassName.Text,
                Description = txtDesc.Text,

                idCategorie = _cat.id
            };

            if (_test.insertTest())
            {
                _container.GetCategories();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error");
                picSave.Enabled = true;
            }
        }
    }
}
