using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMLabsData.EFModels
{
    public class Categories
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string Categorie { get; set; }

        public Categories Categorie_dad { get; set; }

    }
}
