using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frames
{
    [Serializable]
    class Config
    {
        public bool CreateFolder { get; set; }
        public string FolderName{ get; set; }
    }
}
