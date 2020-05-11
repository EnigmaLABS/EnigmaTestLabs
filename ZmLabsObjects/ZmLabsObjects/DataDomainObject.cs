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

        public enumFileType FileType;
        public string Path;
    }
}
