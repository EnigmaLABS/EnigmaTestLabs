using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMLabsData.repos
{
    public class labs_repos
    {
        public List<EFModels.Categories> getCategories(string cnx_str)
        {
            using (var db = new context.LabsContext(cnx_str))
            {
                return db.Categories.ToList();
            }
        }
    }
}
