using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace ZMLabsData.context
{
    public class LabsContext : DbContext
    {
        public LabsContext(string cnx_str) : base(cnx_str)  {  }

        public DbSet<EFModels.Categories> Categories { get; set; }
        public DbSet<EFModels.Tests> Test { get; set; }
        public DbSet<EFModels.TestCases> TestCases { get; set; }

    }
}
