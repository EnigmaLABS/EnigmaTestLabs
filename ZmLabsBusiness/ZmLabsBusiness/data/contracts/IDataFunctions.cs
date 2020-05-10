using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZmLabsObjects;

namespace ZmLabsBusiness.data.contracts
{
    public interface IDataFunctions
    {
        bool TestMasterDB(string Server);
        List<DataDomain> GetFilesPath(string Server);
        bool CreateDatabase(string Server, List<DataDomain> Files);
        bool CreateDatabaseEF(string Server);
        bool UpdateDatabaseEF(string Server);
        string GetLabsCnx();

    }
}
