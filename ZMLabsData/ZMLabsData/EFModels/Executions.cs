﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZMLabsData.EFModels
{
    public class Executions
    {
        public Int64 id { get; set; }

        public DateTime dtBegin { get; set; }
        public DateTime dtEnd { get; set; }

        [Required]
        [ForeignKey("TestCase")]
        public Int64 idTestCase { get; set; }

        public EFModels.TestCases TestCase { get; set; }
    }
}
