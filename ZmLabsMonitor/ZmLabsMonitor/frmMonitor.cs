using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ZmLabsObjects;
using ZmLabsObjects.functions;
using static ZmLabsObjects.DataDomain;

using ZmLabsBusiness;
using ZmLabsBusiness.data;
using ZmLabsBusiness.registry;

using ZMLabsData.repos;

namespace ZmLabsMonitor
{
    public partial class frmMonitor : Form
    {
        private enum enumPantalla { MonitorList, TestInfo }

        //private Business_Test_Functions_base _test_functions;
        private List<TestDomain> _lst_tests = new List<TestDomain>(); //-->> para el treeview

        private controls.usrctrl_testinfo _ctrl_test_info;

        private labs_repos Repository;
        private Business_Test_Functions_EF BusinessFunctions;
        private Domain_Test_Functions_EF DomainFunctions;
        private Business_Data_Functions DataFunctions;

        /// <summary>
        /// Formulario principal que puestra el árbol de categorías y tests de cada categoría
        /// </summary>
        public frmMonitor()
        {
            InitializeComponent();

            //08/05/2020 - Suprimimos compatibilidad con ADO

            //switch (_DataSystem)
            //{
            //    case enumDataSystem.ADO:

            //        _test_functions = new test_functions_ADO();
            //        break;

            //    case enumDataSystem.EF:

            //        _test_functions = new test_functions_EF();
            //        break;
            //}

            //Dependencias
            DataFunctions = new Business_Data_Functions();
            Repository = new labs_repos(DataFunctions.GetLabsCnx());
            BusinessFunctions = new Business_Test_Functions_EF(Repository);
            DomainFunctions = new Domain_Test_Functions_EF(BusinessFunctions);
        }

        private void frmMonitor_Load(object sender, EventArgs e)
        {
            bool existeBBDD = registry_functions.ExisteBBDD();

            //primero comprobamos si existe la BBDD
            if (!existeBBDD)
            {
                frmStart _frm = new frmStart(this);
                _frm.ShowDialog();
            }
            else
            {
                DataFunctions.UpdateDatabaseEF(registry_functions.GetRegisteredServer());
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
                    try
                    {
                        enumTestTypes _type = (enumTestTypes)Enum.Parse(typeof(enumTestTypes),
                                                                        _treeelem.TestObject.Classname);
                         
                        _treeelem.TestObject.Execution.TestType = _type;

                        _treeelem.TestObject.Execution.OBJ = test_types.GetObject(_treeelem.TestObject, _type, DomainFunctions);

                        _ctrl_test_info = new controls.usrctrl_testinfo(_treeelem.TestObject, BusinessFunctions);

                        splitContainer.Panel2.Controls.Clear();

                        _ctrl_test_info.Dock = DockStyle.Fill;
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

            subforms.frm_newtest _frm = new subforms.frm_newtest(_treeelem.Categorie, this, DomainFunctions);
            _frm.ShowDialog();
        }
        
        #endregion


        //-->> Carga recursivamente el árbol de categorías y tests
        #region carga info

        public void GetCategories()
        {
            _lst_tests = BusinessFunctions.getTests();
            treeCatalogo.Nodes.Clear();

            List<CategoriesDomain> _lstcat = BusinessFunctions.getCategories();

            //rellenamos el tree view
            foreach (CategoriesDomain _cat in _lstcat.Where(ct => ct.Categorie_dad == null))
            {
                TreeNode _trprincipal = new TreeNode(_cat.Categorie);

                //cargamos los hijos
                foreach (CategoriesDomain _catsub in _lstcat.Where(c => c.Categorie_dad != null && c.Categorie_dad.id == _cat.id))
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

        public void GetCategorieChildrensAndTests(ref TreeNode _trprincipal, CategoriesDomain _catsub, List<CategoriesDomain> _lstcat)
        {
            TreeNode _hijo = new TreeNode(_catsub.Categorie);

            objects.treeElement _treeElem = new objects.treeElement()
            {
                ElemType = objects.enumElemType.Categorie,
                Categorie = _catsub
            };

            _hijo.Tag = _treeElem;

            //cargamos los hijos
            foreach (CategoriesDomain _catsubsub in _lstcat.Where(c => c.Categorie_dad != null && c.Categorie_dad.id == _catsub.id))
            {
                GetCategorieChildrensAndTests(ref _hijo, _catsubsub, _lstcat);
            }

            //cargamos los test de la categoría actual
            foreach (TestDomain _test in _lst_tests.Where(ct => ct.Categorie.id == _catsub.id))
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
