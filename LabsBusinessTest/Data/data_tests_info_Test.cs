using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZMLabsData.repos;

namespace LabsBusinessTest.Data
{
    [TestClass]
    public class data_tests_info_Test
    {
        private string cnx_str_OK = @"Persist Security Info=False;Integrated Security = true; Initial Catalog = EnigmaLABS_EF; Server=DESKTOP-8R1RIRR\sqlserveri17";
        private string cnx_str_Fail = "XXX";

        [TestMethod]
        public void getCategories_OK()
        {
            labs_repos _repo = new labs_repos(cnx_str_OK);

            var res = _repo.getCategories();

            Assert.IsTrue(res.Count > 0);
            Assert.AreEqual(res[0].Categorie, "SQL Server Tips");
        }

        [TestMethod]
        public void getCategories_BadCnxStr()
        {
            labs_repos _repo = new labs_repos(cnx_str_Fail);

            var res = _repo.getCategories();

            Assert.AreEqual(res.Count, 0);
        }
    }
}
