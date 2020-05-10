using System;
using System.Windows.Forms;

using ZmLabsObjects;
using static ZmLabsObjects.DataDomain;

using ZmLabsObjects.contracts;

namespace ZmLabsMonitor.subforms
{
    public partial class frm_newtest : Form
    {
        private CategoriesDomain _cat;
        private frmMonitor _container;
        private ITestFunctionsDomain _test_functions;

        /// <summary>
        /// Formulario para la creación de un nuevo Test en BBDD
        /// </summary>
        /// <param name="p_cat"></param>
        /// <param name="p_container"></param>
        /// <param name="p_DataSystem"></param>
        public frm_newtest(CategoriesDomain p_cat, frmMonitor p_container, ITestFunctionsDomain p_test_functions)
        {
            InitializeComponent();

            _container = p_container;

            _cat = p_cat;
            _test_functions = p_test_functions;
        }

        private void picSave_Click(object sender, EventArgs e)
        {
            picSave.Enabled = false;

            TestDomain _test = new TestDomain(_test_functions)
            {
                Test = txtTest.Text,
                Classname = txtClassName.Text,
                Description = txtDesc.Text,

                idCategorie = _cat.id,
                Categorie = null
            };

            if (_test_functions.insertTest(_test))
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
