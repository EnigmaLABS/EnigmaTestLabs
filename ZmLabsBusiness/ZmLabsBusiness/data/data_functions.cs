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

        public data_functions()
        {
            DBLabs = ConfigurationManager.AppSettings["DBLABS"].ToString();
        }

        private List<string> lstFicheros = new List<string>() { "getCategories",
                                                                "getExecutions",
                                                                "getTestCases",
                                                                "getTests",
                                                                "insertExecution",
                                                                "insertTest",
                                                                "insertTestCase" };

        public string GetLabsCnx()
        {
            registry.registry_functions _regfunc = new registry.registry_functions();

            string server = _regfunc.GetRegisteredServer();
            string res = GetCnx(server, DBLabs);

            return res;
        }

        public bool TestMasterDB(string Server)
        {
            string cnxstr = GetCnx(Server, DBMaster);

            bool res = data_labs.Test(cnxstr);

            return res;
        }

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

                script = script.Replace("##RUTADATOS##", rutaDatos).Replace("##RUTALOG##", rutaLog);

                res = data_labs.ExecScript(script, cnx_str_master);

                if (res)
                {
                    res = InitializeTables(Server);

                    if (res)
                    {
                        bool resProcedimientos = true;

                        foreach (string _file in lstFicheros)
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

        //-->> Privados

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

        private string GetCnx(string Server, string DB)
        {
            string cnx = ConfigurationManager.ConnectionStrings["cnxLABS_DB_STR"].ConnectionString;

            cnx = cnx.Replace("##DB##", DB).Replace("##SERVER##", Server);

            return cnx;
        }
    }

}

