using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                test = txtTest.Text,
                classname = txtClassName.Text,
                description = txtDesc.Text,

                categorie = new Categories()
                {
                    id = _cat.id
                }
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
