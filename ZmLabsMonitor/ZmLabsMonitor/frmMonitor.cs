using System;
using System.Configuration;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using static ZmLabsObjects.data_object;
using ZmLabsObjects;

using ZmLabsBusiness;
using ZmLabsBusiness.data;
using ZmLabsBusiness.test_info;
using ZmLabsBusiness.registry;


namespace ZmLabsMonitor
{
    public partial class frmMonitor : Form
    {
        private enum enumPantalla { MonitorList, TestInfo }

        private test_functions_base _test_functions;
        private List<test_object> _lst_tests = new List<test_object>(); //-->> para el treeview

        private controls.usrctrl_testinfo _ctrl_test_info;
        private enumDataSystem _DataSystem;

        /// <summary>
        /// Formulario principal que puestra el árbol de categorías y tests de cada categoría
        /// </summary>
        public frmMonitor()
        {
            InitializeComponent();

            string strDataSystem = ConfigurationManager.AppSettings["DBStrategy"].ToString();
            _DataSystem = (enumDataSystem)Enum.Parse(typeof(enumDataSystem), strDataSystem);

            switch (_DataSystem)
            {
                case enumDataSystem.ADO:

                    _test_functions = new test_functions_ADO();
                    break;

                case enumDataSystem.EF:

                    _test_functions = new test_functions_EF();
                    break;
            }
        }

        private void frmMonitor_Load(object sender, EventArgs e)
        {
            registry_functions _reg = new registry_functions();

            bool existeBBDD = _reg.ExisteBBDD();

            //primero comprobamos si existe la BBDD
            if (!existeBBDD)
            {
                frmStart _frm = new frmStart(this, _DataSystem);
                _frm.ShowDialog();
            }
            else
            {
                data_functions _df = new data_functions();
                _df.UpdateDatabaseEF(_reg.GetRegisteredServer());

                GetCategories();
            }
        }

        //-->> Acceso a la información de un test
        //     Acceso a la creación de un nuevo test (colgando de una categoría secundaria)
        #region Menú

        private void treeCatalogo_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeCatalogo.SelectedNode != null)
            {
                objects.treeElement _treeelem = (objects.treeElement)treeCatalogo.SelectedNode.Tag;

                if (_treeelem.ElemType == objects.enumElemType.Test)
                {
                    _test_functions.SetTestObject(_treeelem.TestObject);

                    try
                    {
                        enumTestTypes _type = (enumTestTypes)Enum.Parse(typeof(enumTestTypes),
                                                                        _treeelem.TestObject.Classname);
                         
                        _treeelem.TestObject.Execution.TestType = _type;

                        _treeelem.TestObject.Execution.OBJ = test_types.GetObject(_test_functions, _type);

                        _test_functions.SetTestObject(_treeelem.TestObject);

                        _ctrl_test_info = new controls.usrctrl_testinfo(_test_functions);

                        splitContainer.Panel2.Controls.Clear();
                        splitContainer.Panel2.Controls.Add(_ctrl_test_info);
                    }
                    catch (Exception ex)
                    {
                        splitContainer.Panel2.Controls.Clear();
                    }
                }
                else
                {
                    splitContainer.Panel2.Controls.Clear();
                }
            }
        }

        private void contextMenuArbol_Opening(object sender, CancelEventArgs e)
        {
            if (treeCatalogo.SelectedNode != null)
            {
                objects.treeElement _treeelem = (objects.treeElement)treeCatalogo.SelectedNode.Tag;

                if (_treeelem.ElemType == objects.enumElemType.Categorie && _treeelem.Categorie.Categorie_dad != null)
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void nnuevoTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objects.treeElement _treeelem = (objects.treeElement)treeCatalogo.SelectedNode.Tag;

            subforms.frm_newtest _frm = new subforms.frm_newtest(_treeelem.Categorie, this, _test_functions);
            _frm.ShowDialog();
        }
        
        #endregion


        //-->> Carga recursivamente el árbol de categorías y tests
        #region carga info

        public void GetCategories()
        {
            _lst_tests = _test_functions.getTests();
            treeCatalogo.Nodes.Clear();

            List<Categories> _lstcat = _test_functions.getCategories();

            //rellenamos el tree view
            foreach (Categories _cat in _lstcat.Where(ct => ct.Categorie_dad == null))
            {
                TreeNode _trprincipal = new TreeNode(_cat.Categorie);

                //cargamos los hijos
                foreach (Categories _catsub in _lstcat.Where(c => c.Categorie_dad != null && c.Categorie_dad.id == _cat.id))
                {
                    GetCategorieChildrensAndTests(ref _trprincipal, _catsub, _lstcat);
                }

                //no cargamos tests en las categorías principales (pq no debe haber)
                //si fuera necesario lo haríamos aquí

                objects.treeElement _treeElem = new objects.treeElement()
                {
                    ElemType = objects.enumElemType.Categorie,
                    Categorie = _cat
                };

                _trprincipal.Tag = _treeElem;
                treeCatalogo.Nodes.Add(_trprincipal);
            };

            treeCatalogo.ExpandAll();
        }

        public void GetCategorieChildrensAndTests(ref TreeNode _trprincipal, Categories _catsub, List<Categories> _lstcat)
        {
            TreeNode _hijo = new TreeNode(_catsub.Categorie);

            objects.treeElement _treeElem = new objects.treeElement()
            {
                ElemType = objects.enumElemType.Categorie,
                Categorie = _catsub
            };

            _hijo.Tag = _treeElem;

            //cargamos los hijos
            foreach (Categories _catsubsub in _lstcat.Where(c => c.Categorie_dad != null && c.Categorie_dad.id == _catsub.id))
            {
                GetCategorieChildrensAndTests(ref _hijo, _catsubsub, _lstcat);
            }

            //cargamos los test de la categoría actual
            foreach (test_object _test in _lst_tests.Where(ct => ct.Categorie.id == _catsub.id))
            {
                objects.treeElement _treeTestElem = new objects.treeElement()
                {
                    ElemType = objects.enumElemType.Test,
                    TestObject = _test
                };

                TreeNode _treetest = new TreeNode(_test.Test);
                _treetest.Tag = _treeTestElem;
                _hijo.Nodes.Add(_treetest);
            }

            _trprincipal.Nodes.Add(_hijo);
        }

        #endregion

    }
}
