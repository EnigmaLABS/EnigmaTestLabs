using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmLabsObjects.DTO
{
    public class InformeAbsentismoDTO
    {
        public Guid Trabajador { get; set; }

        public int TotalLaborales
        {
            get => conteo_registros * 8;
        }

        public int DiasRegistrados
        {
            get => conteo_registros;
        }

        public int HorasTrabajadas
        {
            get => suma_horas;
        }

        public int HorasNoTrabajadas
        {
            get => (conteo_registros * 8) - suma_horas;
        }

        public int DiasNoTrabajados
        {
            get => HorasNoTrabajadas / 8;
        }

        public int PctAbsentismo
        {
            get => ((TotalLaborales - HorasTrabajadas) * 100) / TotalLaborales;
        }

        //resultados parciales obtenidos de BBDD
        public int conteo_registros { get; set; }
        public int suma_horas { get; set; }
    }
}
