using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using ZMLabsData;
using ZmLabsObjects;

namespace ZmLabsBusiness.data
{
    public class Business_Data_Functions : contracts.IDataFunctions
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        const string DBMaster = "master";
        private string DBLabs;

        /// <summary>
        /// Lógica de negocio para el proceso inicial de creación de base de datos del usuario
        /// A desencadenarse en la primera ejecución de la solución
        /// </summary>
        public Business_Data_Functions()
        {
            DBLabs = ConfigurationManager.AppSettings["DBLABS"].ToString();
        }

        private List<string> lstSPs = new List<string>() { "getCategories",
                                                           "getTests", "insertTest",
                                                           "getTestCases", "insertTestCase",
                                                           "insertExecution", "getExecutions",
                                                           "insertParteHoras", "GetEstadisticasAbsentismo"
        };

        /// <summary>
        /// Comprueba la conexión contra la base de datos master
        /// </summary>
        /// <param name="Server"></param>
        /// <returns></returns>
        public bool TestMasterDB(string Server)
        {
            bool res = true;

            try
            {
                string cnxstr = GetCnx(Server, DBMaster);

                res = data_labs.Test(cnxstr);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error ejecutando TestMasterDB");
                return false;
            }

            return res;
        }

        /// <summary>
        /// Obtiene los ficheros mdf y ldf del servidor SQL Server especificado por parámetro
        /// </summary>
        /// <param name="Server"></param>
        /// <returns></returns>
        public List<DataDomain> GetFilesPath(string Server)
        {
            string cnx_str = ""; 
            List<DataDomain> _files = new List<DataDomain>();

            try
            {
                cnx_str = GetCnx(Server, "master");

                _files = data_labs.GetFilesPath(cnx_str);

                foreach (DataDomain _do in _files)
                {
                    char chr = '\\';
                    int index = _do.Path.LastIndexOf(chr);

                    string ruta = _do.Path.Substring(0, index + 1);

                    _do.Path = ruta;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error ejecutando GetFilesPath");
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
        public bool CreateDatabase(string Server, List<DataDomain> Files)
        {
            bool res = true;
            bool res2 = true;

            try
            {
                string cnx_str_master = GetCnx(Server, DBMaster);
                string cnx_str_labs = GetCnx(Server, DBLabs);

                TextReader tr = new StreamReader(@"sqlfiles\createdatabase.txt");
                TextReader tr2 = new StreamReader(@"sqlfiles\createschema_test.txt");

                string scriptCreateDatabase = tr.ReadToEnd();
                string scriptCreateSchemaTest = tr2.ReadToEnd();

                string rutaDatos = Files.Where(tp => tp.FileType == DataDomain.enumFileType.data).First().Path;
                string rutaLog = Files.Where(tp => tp.FileType == DataDomain.enumFileType.log).First().Path;

                scriptCreateDatabase = scriptCreateDatabase.Replace("##RUTADATOS##", rutaDatos).Replace("##RUTALOG##", rutaLog).Replace("##DATABASENAME##", DBLabs);

                res = data_labs.ExecScript(scriptCreateDatabase, cnx_str_master);
                res2 = data_labs.ExecScript(scriptCreateSchemaTest, cnx_str_labs);

                if (res && res2)
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
                _logger.Error(ex, "Error ejecutando CreateDatabase");

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

                //Crea el tipo -tipo tabla- definido por el usuario
                TextReader txtType = new StreamReader(@"sqlfiles\tblParteHoras.txt");
                string scriptType = txtType.ReadToEnd();

                bool resType = data_labs.ExecScript(scriptType, GetCnxEF(Server));

                if (resType)
                {
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
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error ejecutando CreateDatabaseEF");
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
                _logger.Error(ex, "Error ejecutando UpdateDatabaseEF");
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


            string cnx_str = GetCnx(Server, DBLabs);

            TextReader tr = new StreamReader(@"sqlfiles\InitializeTables.txt");
            string script = tr.ReadToEnd();

            res = data_labs.ExecScript(script, cnx_str);

            if (res)
            {
                res = data_labs.InitializeTables(cnx_str);
            }

            return res;
        }

        //-->>
        #region Get Connections

        public string GetLabsCnx()
        {
            string res = "";
            try
            {
                string server = registry.registry_functions.GetRegisteredServer();
                res = GetCnx(server, DBLabs);

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error ejecutando GetLabsCnx");
            }

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

