﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZmLabsObjects.contracts;

namespace ZmLabsObjects.sqltests
{
    public class ParteHorasDomain 
    {
        public enum enumTipoJornada { Normal, Baja, Incidencia };

        public Guid Trabajador { get; set; }
        public DateTime Fecha { get; set; }

        public Int16 _horas;
        public Int16 Horas
        {
            get => _horas;
            set
            {
                _horas = value;

                if (Horas == 0)
                {
                    this.TipoJornada = enumTipoJornada.Baja;
                }
                else if (Horas == 8)
                {
                    this.TipoJornada = enumTipoJornada.Normal;
                }
                else
                {
                    this.TipoJornada = enumTipoJornada.Incidencia;
                }
            }
        }

        public int Anho
        {
            get => Fecha.Year;
        }

        public enumTipoJornada TipoJornada { get; set; }
    }
}
