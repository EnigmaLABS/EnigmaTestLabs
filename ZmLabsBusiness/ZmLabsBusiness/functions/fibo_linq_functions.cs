using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmLabsBusiness.functions
{
    public class fibo_linq_functions : contracts.IFibo
    {
        public List<ulong> CalcFibo(int numelements)
        {
            List<ulong> fibonacciNumbers = new List<ulong>();

            Enumerable.Range(0, numelements)
                .ToList()
                .ForEach(k => fibonacciNumbers.Add(k <= 1 ? 1 : fibonacciNumbers[k - 2] + fibonacciNumbers[k - 1]));

            return fibonacciNumbers.Take(numelements).ToList();
        }
    }
}

//-->> CÓDIGO PARA BLOG :

