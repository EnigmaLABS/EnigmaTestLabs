using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZmLabsObjects;
using ZmLabsBusiness.test_info;

namespace ZmLabsMonitor.objects
{
    public enum enumElemType { Categorie, Test }

    public class treeElement
    {
        public enumElemType ElemType;
        public TestDomain TestObject;
        public CategoriesDomain Categorie;
    }
}
