using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ZMLabsData.EFModels
{
    public class Tests
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

        [Required]
        public Categories Categorie { get; set; }
    }
}
