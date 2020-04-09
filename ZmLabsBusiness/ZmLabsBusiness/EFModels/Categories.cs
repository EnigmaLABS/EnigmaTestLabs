using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmLabsBusiness.EFModels
{
    public class Categories
    {
        public int id { get; set; }
        public string categorie { get; set; }

        public Categories categorie_dad { get; set; }
    }
}
