using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ZmLabsBusiness.registry;
using ZmLabsBusiness.data;
using ZmLabsObjects;

namespace ZmLabsMonitor
{
    public partial class frmStart : Form
    {
        private frmMonitor _container;
        private data_object.enumDataSystem _DataSystem;

        public frmStart(frmMonitor p_container, data_object.enumDataSystem p_DataSystem)
        {
            InitializeComponent();

            _container = p_container;
            _DataSystem = p_DataSystem;
        }

        private void frmStart_Load(object sender, EventArgs e)
        {
            txtServer.Focus();
        }

        private void cmdEmpezar_Click(object sender, EventArgs e)
        {
            cmdCancelar.Enabled = false;
            cmdEmpezar.Enabled = false;

            this.Cursor = Cursors.WaitCursor;

            switch (_DataSystem)
            {
                case data_object.enumDataSystem.ADO:

                    CrearMedianteScripts();
                    break;

                case data_object.enumDataSystem.EntityFramework:

                    CrearMedianteEF();
                    break;
            }

            cmdCancelar.Enabled = true;
            cmdEmpezar.Enabled = true;

            this.Cursor = Cursors.Default;
        }

        private void CrearMedianteEF()
        {
            if (txtServer.Text.Trim().Length > 2)
            {
                data_functions _df = new data_functions();

                if (_df.TestMasterDB(txtServer.Text.Trim()))
                {
                    if (_df.CreateDatabaseEF(txtServer.Text.Trim()))
                    {
                        registry_functions _reg = new registry_functions();

                        if (_reg.SetBBDDCreada(txtServer.Text.Trim()))
                        {
                            _container.GetCategories();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Error al registrar");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error al crear la base de datos");
                    }
                }
                else
                {
                    MessageBox.Show("El servidor no responde");
                }
            }
            else
            {
                MessageBox.Show("Introduzca el nombre del servidor SQL Server");
            }
        }

        private void CrearMedianteScripts()
        {
            if (txtServer.Text.Trim().Length > 2)
            {
                data_functions _df = new data_functions();

                if (_df.TestMasterDB(txtServer.Text.Trim()))
                {
                    List<data_object> _files = _df.GetFilesPath(txtServer.Text.Trim());

                    if (_files.Count >= 2)
                    {
                        if (_df.CreateDatabase(txtServer.Text.Trim(), _files))
                        {
                            registry_functions _reg = new registry_functions();

                            if (_reg.SetBBDDCreada(txtServer.Text.Trim()))
                            {
                                _container.GetCategories();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Error al registrar");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No se ha podido crear la base de datos");
                        }
                    }
                    else
                    {
                        MessageBox.Show("conexión OK. No se encuentran los ficheros de datos");
                    }
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
            else
            {
                MessageBox.Show("Introduzca el nombre del servidor SQL Server");
            }
        }

        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
