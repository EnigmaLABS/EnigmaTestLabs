using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMLabsData.ADO
{
    public class data_test_sql
    {
        private string str_cnx;

        public data_test_sql(string p_str_cnx)
        {
            str_cnx = p_str_cnx;
        }

        public bool InsertParteHorasAnual(DataTable _tblParteAnual)
        {
            try
            {
                SqlConnection cnx = new SqlConnection(str_cnx);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "test.insertParteHoras";

                cmd.Parameters.AddWithValue("@ParteHoras", _tblParteAnual);

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
