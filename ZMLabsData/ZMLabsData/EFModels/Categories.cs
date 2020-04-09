using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string categorie { get; set; }

        public Categories categorie_dad { get; set; }
    }
}
