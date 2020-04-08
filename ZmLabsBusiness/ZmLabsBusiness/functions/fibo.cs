using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmLabsBusiness.functions
{
    public static class fibo
    {
        public static void CalcFibo(int numelements)
        {
            Dictionary<int, Int64> lstelements = new Dictionary<int, Int64>();

            int cont = 0;

            Int64 anterior = 0;
            Int64 anterioranterior = 0;

            while (cont < numelements)
            {
                if (cont == 0)
                {
                    lstelements[cont] = 0;
                    anterioranterior = 0;
                }
                else if (cont == 1)
                {
                    lstelements[cont] = 1;
                    anterior = 1;
                }
                else
                {
                    Int64 newelem = anterioranterior + anterior;

                    anterioranterior = anterior;
                    anterior = newelem;

                    lstelements[cont] = newelem;
                }

                cont++;
            }
        }
    }
}
