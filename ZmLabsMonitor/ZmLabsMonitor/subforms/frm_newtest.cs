using System;
using System.Windows.Forms;

using ZmLabsObjects;
using ZmLabsBusiness.test_info;
using static ZmLabsObjects.DataDomain;

namespace ZmLabsMonitor.subforms
{
    public partial class frm_newtest : Form
    {
        private CategoriesDomain _cat;
        private frmMonitor _container;
        private test_functions_base _testObject;

        /// <summary>
        /// Formulario para la creación de un nuevo Test en BBDD
        /// </summary>
        /// <param name="p_cat"></param>
        /// <param name="p_container"></param>
        /// <param name="p_DataSystem"></param>
        public frm_newtest(CategoriesDomain p_cat, frmMonitor p_container, test_functions_base p_testObject)
        {
            InitializeComponent();

            _cat = p_cat;
            _container = p_container;

            _testObject = p_testObject;
        }

        private void picSave_Click(object sender, EventArgs e)
        {
            picSave.Enabled = false;

            _testObject.Test = txtTest.Text;
            _testObject.Classname = txtClassName.Text;
            _testObject.Description = txtDesc.Text;

            _testObject.idCategorie = _cat.id;
            _testObject.Categorie = null;

            if (_testObject.insertTest())
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
