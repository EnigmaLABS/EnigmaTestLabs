using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmLabsBusiness.functions.contracts
{
    public interface IFibo
    {
        List<ulong> CalcFibo(int numelements);
    }
}
