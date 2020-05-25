using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZmLabsObjects.DTO;
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

        public List<InformeAbsentismoDTO> GetInformeAbsentismoAnual(int anho)
        {
            List<InformeAbsentismoDTO> res = new List<InformeAbsentismoDTO>();

            SqlConnection cnx = new SqlConnection(str_cnx);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = cnx;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "test.GetEstadisticasAbsentismo";

            cmd.Parameters.AddWithValue("@anho", anho);

            cnx.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                InformeAbsentismoDTO _infline = new InformeAbsentismoDTO()
                {

                    Trabajador = Guid.Parse(reader["Trabajador"].ToString()),
                    conteo_registros = int.Parse(reader["conteo_registros"].ToString()),
                    suma_horas = int.Parse(reader["suma_horas"].ToString())
                };

                res.Add(_infline);
            }
            cnx.Close();

            return res;
        }
    }
}
