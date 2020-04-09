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
        public LabsContext() : base("cnxLABS_DB_STR_EF") {  }

        public DbSet<EFModels.Categories> Categories { get; set; }

    }
}
