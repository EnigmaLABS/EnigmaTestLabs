﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ZMLabsData.EFModels
{
    [Table("Tests")]
    public class TestsModel
    {
        public Int64 id { get; set; }

        [Required]
        [StringLength(255)]
        public string Test { get; set; }

        [StringLength(555)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Classname { get; set; }

        [StringLength(555)]
        public string Url_Blog { get; set; }

        [StringLength(555)]
        public string Url_Git { get; set; }

        [StringLength(555)]
        public string Url_Stackoverflow { get; set; }

        public CategoriesModel Categorie { get; set; }

        [Required]
        [ForeignKey("Categorie")]
        public int idCategorie { get; set; }

        public List<TestCasesModel> TestCases { get; set; }
    }
}
