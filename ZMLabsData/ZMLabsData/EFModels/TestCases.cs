using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMLabsData.EFModels
{
    public class TestCases
    {
        public Int64 id { get; set; }

        [Required]
        [StringLength(255)]
        public string Function { get; set; }

        [StringLength(555)]
        public string Description { get; set; }

        [Required]
        public EFModels.Tests Test { get; set; }
    }
}
