using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZmLabsObjects;
using ZmLabsBusiness.data;

using ZMLabsData.repos;

namespace LabsBusinessTest
{
    [TestClass]
    public class EFFunctionsTest
    {
        [TestMethod]
        public void getCategoriesTest_CNX_Fail()
        {
            labs_repos repos = new labs_repos("");
            Business_Test_Functions_EF clsObj = new Business_Test_Functions_EF(repos);

            List<CategoriesDomain> res = clsObj.getCategories();

            Assert.IsTrue(res.Count == 0);
        }

        [TestMethod]
        public void getCategoriesTest_CNX_OK()
        {
            labs_repos repos = new labs_repos(@"Persist Security Info=False;Integrated Security=true;Initial Catalog=EnigmaLABS_EF;Server=DESKTOP-8R1RIRR\sqlserveri17");
            Business_Test_Functions_EF clsObj = new Business_Test_Functions_EF(repos);

            List<CategoriesDomain> res = clsObj.getCategories();

            Assert.IsTrue(res.Count > 0);
        }
    }
}
