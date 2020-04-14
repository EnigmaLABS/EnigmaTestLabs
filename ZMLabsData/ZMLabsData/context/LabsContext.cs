using System;
using System.Data.Entity;
using System.Linq;

namespace ZMLabsData.context
{
    public class LabsContext : DbContext
    {
        public LabsContext(string cnx_str) : base(cnx_str)  {  }

        public DbSet<EFModels.Categories> Categories { get; set; }
        public DbSet<EFModels.Tests> Test { get; set; }
        public DbSet<EFModels.TestCases> TestCases { get; set; }
        public DbSet<EFModels.Executions> Executions { get; set; }
    }
}
