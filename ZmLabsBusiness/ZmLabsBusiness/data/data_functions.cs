using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using ZMLabsData;
using ZmLabsObjects;

namespace ZmLabsBusiness.data
{
    public class data_functions
    {
        const string DBMaster = "master";
        private string DBLabs;

        /// <summary>
        /// Lógica de negocio para el proceso inicial de creación de base de datos del usuario
        /// A desencadenarse en la primera ejecución de la solución
        /// </summary>
        public data_functions()
        {
            DBLabs = ConfigurationManager.AppSettings["DBLABS"].ToString();
        }

        private List<string> lstSPs = new List<string>() { "getCategories", "getTests", "insertTest", "getTestCases", "insertTestCase", "insertExecution", "getExecutions" };

        /// <summary>
        /// Comprueba la conexión contra la base de datos master
        /// </summary>
        /// <param name="Server"></param>
        /// <returns></returns>
        public bool TestMasterDB(string Server)
        {
            string cnxstr = GetCnx(Server, DBMaster);

            bool res = data_labs.Test(cnxstr);

            return res;
        }

        /// <summary>
        /// Obtiene los ficheros mdf y ldf del servidor SQL Server especificado por parámetro
        /// </summary>
        /// <param name="Server"></param>
        /// <returns></returns>
        public List<data_object> GetFilesPath(string Server)
        {
            string cnx_str = GetCnx(Server, "master");

            List<data_object> _files = data_labs.GetFilesPath(cnx_str);

            foreach (data_object _do in _files)
            {
                char chr = '\\';
                int index = _do.Path.LastIndexOf(chr);

                string ruta = _do.Path.Substring(0, index+1);

                _do.Path = ruta;
            }

            return _files;
        }

        /// <summary>
        /// Crea la base de datos en el servidor dado, crea tablas con datos y procedimientos almacenados
        /// Todo ello mediante Scripts SQL
        /// </summary>
        /// <param name="Server"></param>
        /// <param name="Files"></param>
        /// <returns></returns>
        public bool CreateDatabase(string Server, List<data_object> Files)
        {
            bool res = true;

            try
            {
                string cnx_str_master = GetCnx(Server, DBMaster);
                string cnx_str_labs = GetCnx(Server, DBLabs);

                TextReader tr = new StreamReader(@"sqlfiles\createdatabase.txt");
                string script = tr.ReadToEnd();

                string rutaDatos = Files.Where(tp => tp.FileType == data_object.enumFileType.data).First().Path;
                string rutaLog = Files.Where(tp => tp.FileType == data_object.enumFileType.log).First().Path;

                script = script.Replace("##RUTADATOS##", rutaDatos).Replace("##RUTALOG##", rutaLog).Replace("##DATABASENAME##", DBLabs);

                res = data_labs.ExecScript(script, cnx_str_master);

                if (res)
                {
                    res = InitializeTables(Server);

                    if (res)
                    {
                        bool resProcedimientos = true;

                        foreach (string _file in lstSPs)
                        {
                            TextReader txtProcedure = new StreamReader(@"sqlfiles\" + _file + ".txt");
                            string scriptProcedure = txtProcedure.ReadToEnd();

                            resProcedimientos = data_labs.ExecScript(scriptProcedure, cnx_str_labs);

                            if (!resProcedimientos)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                res = false;
            }

            return res;
        }

        /// <summary>
        /// Crea la base de datos en el servidor dado, crea tablas con datos y procedimientos almacenados
        /// Todo ello mediante Entity Framewok Code First
        /// </summary>
        /// <param name="Server"></param>
        /// <returns></returns>
        public bool CreateDatabaseEF(string Server)
        {
            bool res = true;

            try
            {
                ZMLabsData.Migrations.Configuration _confDB = new ZMLabsData.Migrations.Configuration();
                _confDB.CreateOrUpdateDataBase(false, GetCnxEF(Server));

                //Crea los procedimientos almacenados
                bool resProcedimientos;

                foreach (string _file in lstSPs)
                {
                    TextReader txtProcedure = new StreamReader(@"sqlfiles\" + _file + ".txt");
                    string scriptProcedure = txtProcedure.ReadToEnd();

                    resProcedimientos = data_labs.ExecScript(scriptProcedure, GetCnxEF(Server));

                    if (!resProcedimientos)
                    {
                        return false;
                    }
                }
            }

            catch (Exception ex)
            {
                res = false;
            }

            return res;
        }

        public bool UpdateDatabaseEF(string Server)
        {
            try
            {
                ZMLabsData.Migrations.Configuration _confDB = new ZMLabsData.Migrations.Configuration();
                _confDB.CreateOrUpdateDataBase(true, GetCnxEF(Server));
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        //-->> Privados

        /// <summary>
        /// Ejecuta el script SQL InitializeTables que crea la estructura de tablas y las alimenta de datos
        /// Enmarcado en la estrategia de creación mediante Scripts
        /// </summary>
        /// <param name="Server"></param>
        /// <returns></returns>
        private bool InitializeTables(string Server)
        {
            bool res = true;

            try
            {
                string cnx_str = GetCnx(Server, DBLabs);

                TextReader tr = new StreamReader(@"sqlfiles\InitializeTables.txt");
                string script = tr.ReadToEnd();

                res = data_labs.ExecScript(script, cnx_str);

                if (res)
                {
                    res = data_labs.InitializeTables(cnx_str);
                }
            }

            catch (Exception ex)
            {
                res = false;
            }

            return res;
        }

        //-->>
        #region Get Connections

        public string GetLabsCnx()
        {
            registry.registry_functions _regfunc = new registry.registry_functions();

            string server = _regfunc.GetRegisteredServer();
            string res = GetCnx(server, DBLabs);

            return res;
        }

        private string GetCnx(string Server, string DB)
        {
            string cnx = ConfigurationManager.ConnectionStrings["cnxLABS_DB_STR"].ConnectionString;

            cnx = cnx.Replace("##DB##", DB).Replace("##SERVER##", Server);

            return cnx;
        }

        private string GetCnxEF(string Server)
        {
            string cnx = ConfigurationManager.ConnectionStrings["cnxLABS_DB_STR_EF"].ConnectionString;

            cnx = cnx.Replace("##SERVER##", Server);

            return cnx;
        }

        #endregion

    }
}

