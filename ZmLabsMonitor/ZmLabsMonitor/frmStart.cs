﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ZmLabsBusiness.registry;
using ZmLabsBusiness.data;
using ZmLabsObjects;

namespace ZmLabsMonitor
{
    public partial class frmStart : Form
    {
        private frmMonitor _container;

        public frmStart(frmMonitor p_container)
        {
            InitializeComponent();
            _container = p_container;
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

            cmdCancelar.Enabled = true;
            cmdEmpezar.Enabled = true;

            this.Cursor = Cursors.Default;
        }

        private void cmdCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}