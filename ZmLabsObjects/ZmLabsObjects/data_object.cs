using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmLabsObjects
{
    public class data_object
    {
        public enum enumFileType { data, log }
        public enum enumDataSystem { ADO, EntityFramework }

        public enumFileType FileType;
        public string Path;

        public enumDataSystem DataSystem;
    }
}
