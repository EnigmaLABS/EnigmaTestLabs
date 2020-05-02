using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZMLabsData.EFModels.testModels
{
    [Table("ParteHoras", Schema = "test")]
    public class ParteHorasModel
    {
        public Int64 id { get; set; }

        [Required]
        public Guid Trabajador { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public Int16 Horas { get; set; }
    }
}
