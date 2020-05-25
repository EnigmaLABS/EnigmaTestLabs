using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZmLabsObjects.DTO;
using ZmLabsObjects.sqltests;

namespace ZMLabsData.contracts
{
    public interface IParteHorasRepository
    {
        bool InsertParteHorasAnualADO(DataTable _tblParteAnual);
        bool InsertParteHorasAnualEF(List<ParteHorasDomain> _ParteAnual);

        List<InformeAbsentismoDTO> GetInformeAbsentismoAnual(int anho);
    }
}
