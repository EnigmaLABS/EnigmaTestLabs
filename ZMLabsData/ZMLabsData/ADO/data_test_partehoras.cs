using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZmLabsObjects.sqltests;

namespace ZMLabsData.ADO
{
    public class data_test_partehoras : contracts.IParteHorasRepository
    {
        private string str_cnx;

        public data_test_partehoras(string p_str_cnx)
        {
            str_cnx = p_str_cnx;
        }

        public bool InsertParteHorasAnualEF(List<ParteHorasDomain> _ParteAnual)
        {
            throw new NotImplementedException();
        }

        public bool InsertParteHorasAnualADO(DataTable _tblParteAnual)
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

            return true;
        }
    }
}
