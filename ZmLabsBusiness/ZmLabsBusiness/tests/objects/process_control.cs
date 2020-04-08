using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZmLabsBusiness.tests.objects
{
    public class process_control
    {
        public enum enumEstadoProceso { Ejecutando, Finalizado, Erroneo }

        public enumEstadoProceso Estado;
        public Thread Hilo;
    }
}
