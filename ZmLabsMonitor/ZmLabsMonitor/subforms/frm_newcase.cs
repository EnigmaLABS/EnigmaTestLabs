using System;
using System.Windows.Forms;

using ZmLabsObjects;
using ZmLabsObjects.contracts;

namespace ZmLabsMonitor.subforms
{

    public partial class frm_newcase : Form
    {
        private TestDomain Test;
        private ITestFunctionsDomain TestFunctions;

        private controls.usrctrl_testinfo_detalles _container;

        /// <summary>
        /// Formulario para la creación de un nuevo TestCase asociado a un Test
        /// </summary>
        /// <param name="p_test"></param>
        /// <param name="p_container"></param>
        public frm_newcase(TestDomain p_test,
                           ITestFunctionsDomain p_TestFunctions, 
                           controls.usrctrl_testinfo_detalles p_container)
        {
            InitializeComponent();

            TestFunctions = p_TestFunctions;
            _container = p_container;
            Test = p_test;
        }

        private void frm_newcase_Load(object sender, EventArgs e)
        {
            txtFunctionName.Focus();
        }

        private void picSave_Click(object sender, EventArgs e)
        {
            if (txtFunctionName.Text.Trim().Length > 1)
            {
                TestCasesDomain _tc = new TestCasesDomain(TestFunctions)
                {
                    idTest = Test.id,
                    Function = txtFunctionName.Text.Trim(),
                    Description = txtDesc.Text.Trim(),

                    Test = Test
                };

                _tc = TestFunctions.insertTestCase(_tc);

                _container.AddTestCase(_tc);

                this.Close();
            }
            else
            {
                MessageBox.Show("Informe el nombre de la función");
            }
        }
    }
}
