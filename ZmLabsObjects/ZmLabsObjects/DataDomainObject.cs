using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmLabsObjects
{
    public class DataDomain
    {
        public enum enumFileType { data, log }

        //public enum enumDataSystem { ADO, EF }

        public enumFileType FileType;
        public string Path;

        //public enumDataSystem DataSystem;
    }
}
