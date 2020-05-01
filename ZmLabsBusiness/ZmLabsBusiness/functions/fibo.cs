using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmLabsBusiness.functions
{
    public class fibo : contracts.IFibo
    {
        public List<ulong> CalcFibo(int numelements)
        {
            List<ulong> lstelements = new List<ulong>();

            int cont = 0;

            ulong anterior = 0;
            ulong anterioranterior = 0;

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
                    ulong newelem = anterioranterior + anterior;

                    anterioranterior = anterior;
                    anterior = newelem;

                    lstelements[cont] = newelem;
                }

                cont++;
            }

            return lstelements;
        }
    }
}
