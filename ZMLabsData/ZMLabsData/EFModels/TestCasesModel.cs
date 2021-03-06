﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ZMLabsData.EFModels
{
    [Table("TestCases")]
    public class TestCasesModel
    {
        public Int64 id { get; set; }

        [Required]
        public int Orden { get; set; }

        [Required]
        [StringLength(255)]
        public string Function { get; set; }

        [StringLength(555)]
        public string Description { get; set; }

        [Required]
        [ForeignKey("Test")]
        public Int64 idTest { get; set; }

        public EFModels.TestsModel Test { get; set; }
    }
}
