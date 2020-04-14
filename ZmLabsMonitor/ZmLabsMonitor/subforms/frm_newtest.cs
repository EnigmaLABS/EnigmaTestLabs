using System;
using System.Windows.Forms;

using ZmLabsObjects;
using ZmLabsBusiness.test_info;
using static ZmLabsObjects.data_object;

namespace ZmLabsMonitor.subforms
{
    public partial class frm_newtest : Form
    {
        private Categories _cat;
        private frmMonitor _container;
        private enumDataSystem _DataSystem;

        /// <summary>
        /// Formulario para la creación de un nuevo Test en BBDD
        /// </summary>
        /// <param name="p_cat"></param>
        /// <param name="p_container"></param>
        /// <param name="p_DataSystem"></param>
        public frm_newtest(Categories p_cat, frmMonitor p_container, enumDataSystem p_DataSystem)
        {
            InitializeComponent();

            _cat = p_cat;
            _container = p_container;
            _DataSystem = p_DataSystem;
        }

        private void picSave_Click(object sender, EventArgs e)
        {
            picSave.Enabled = false;

            test_functions_base _test = new test_functions_base(_DataSystem, null)
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
