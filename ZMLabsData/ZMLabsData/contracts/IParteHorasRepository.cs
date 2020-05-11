using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZmLabsObjects.sqltests;

namespace ZMLabsData.contracts
{
    public interface IParteHorasRepository
    {
        bool InsertParteHorasAnualADO(DataTable _tblParteAnual);
        bool InsertParteHorasAnual(List<ParteHorasDomain> _ParteAnual);

    }
}
