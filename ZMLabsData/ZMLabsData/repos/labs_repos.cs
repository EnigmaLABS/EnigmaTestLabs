using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMLabsData.repos
{
    public class labs_repos
    {
        public List<EFModels.Categories> getCategories()
        {
            using (var db = new context.LabsContext())
            {
                return db.Categories.ToList();
            }
        }
    }
}
