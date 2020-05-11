using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmLabsBusiness.functions
{
    public static class fibo_functions 
    {
        public static List<ulong> CalcFibo(int numelements)
        {
            List<ulong> lstelements = new List<ulong>();

            int cont = 0;

            ulong anterior = 0;
            ulong anterioranterior = 0;

            while (cont < numelements)
            {
                if (cont == 0)
                {
                    lstelements.Add(0);
                    anterioranterior = 0;
                }
                else if (cont == 1)
                {
                    lstelements.Add(1);
                    anterior = 1;
                }
                else
                {
                    ulong newelem = anterioranterior + anterior;

                    anterioranterior = anterior;
                    anterior = newelem;

                    lstelements.Add(newelem);
                }

                cont++;
            }

            return lstelements;
        }

        public static List<ulong> CalcFiboLinq(int numelements)
        {
            List<ulong> fibonacciNumbers = new List<ulong>();

            Enumerable.Range(0, numelements)
                .ToList()
                .ForEach(k => fibonacciNumbers.Add(k <= 1 ? 1 : fibonacciNumbers[k - 2] + fibonacciNumbers[k - 1]));

            return fibonacciNumbers.Take(numelements).ToList();
        }
    }
}
