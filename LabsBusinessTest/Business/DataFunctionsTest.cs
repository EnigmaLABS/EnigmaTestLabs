using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ZmLabsObjects;
using ZmLabsBusiness.data;

namespace LabsBusinessTest
{
    [TestClass]
    public class DataFunctionsTest
    {
        [TestMethod]
        public void TestMasterDB_Fail_IncorrectServer()
        {
            Business_Data_Functions _datafunctionobject = new Business_Data_Functions();

            bool res = _datafunctionobject.TestMasterDB(@"XXX");

            Assert.IsFalse(res);
        }

        [TestMethod]
        public void TestMasterDB_OK()
        {
            Business_Data_Functions _datafunctionobject = new Business_Data_Functions();

            bool res = _datafunctionobject.TestMasterDB(@"DESKTOP-8R1RIRR\sqlserveri17");

            Assert.IsTrue(res);
        }

        [TestMethod]
        public void GetFilesPath_OK()
        {
            Business_Data_Functions _datafunctionobject = new Business_Data_Functions();

            List<DataDomain> res = _datafunctionobject.GetFilesPath(@"DESKTOP-8R1RIRR\sqlserveri17");

            Assert.AreEqual(res.Count, 2);
            Assert.AreEqual(res.Where(d => d.FileType == DataDomain.enumFileType.data).ToList().Count, 1);
            Assert.AreEqual(res.Where(d => d.FileType == DataDomain.enumFileType.log).ToList().Count, 1);
        }

        [TestMethod]
        public void GetFilesPath_Fail_IncorrectServer()
        {
            Business_Data_Functions _datafunctionobject = new Business_Data_Functions();

            List<DataDomain> res = _datafunctionobject.GetFilesPath(@"XXX");

            Assert.AreEqual(res.Count, 0);
        }
    }
}
