using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

using ZmLabsObjects;

namespace ZMLabsData
{
    public static class data_labs
    {
        public static bool Test(string cnx_str)
        {
            try
            {
                SqlConnection cnx = new SqlConnection(cnx_str);

                cnx.Open();
                cnx.Close();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static List<DataDomain> GetFilesPath(string cnx_str)
        {
            List<DataDomain> res = new List<DataDomain>();

            SqlConnection cnx = new SqlConnection(cnx_str);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = cnx;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_helpfile";

            cnx.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DataDomain _do = new DataDomain();

                if (reader["usage"].ToString() == "data only")
                {
                    _do.FileType = DataDomain.enumFileType.data;
                }
                else if (reader["usage"].ToString() == "log only")
                {
                    _do.FileType = DataDomain.enumFileType.log;
                }

                _do.Path = reader["filename"].ToString();

                res.Add(_do);
            }

            cnx.Close();

            return res;
        }

        public static bool ExecScript(string scriptText, string cnx_str)
        {
            try
            {
                SqlConnection cnx = new SqlConnection(cnx_str);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cnx;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = scriptText;

                cnx.Open();
                cmd.ExecuteNonQuery();
                cnx.Close();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static bool InitializeTables(string cnx_str)
        {
            try
            {
                SqlConnection cnx = new SqlConnection(cnx_str);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InitializeTables";

                cnx.Open();
                cmd.ExecuteNonQuery();
                cnx.Close();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
