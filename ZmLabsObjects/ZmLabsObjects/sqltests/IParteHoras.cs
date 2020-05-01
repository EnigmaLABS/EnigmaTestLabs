using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmLabsObjects.sqltests
{
    public interface IParteHoras
    {
        //enum enumTipoJornada;

        Guid Trabajador { get; set; }
        DateTime Fecha { get; set; }

        Int16 Horas { get; set; }

        ParteHoras.enumTipoJornada TipoJornada { get; set; }

        //Métodos
        List<IParteHoras> Generate(int numTrabajadores, int Anho);

        void Clear();
    }
}
