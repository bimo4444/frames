using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frames
{
    class ConfigFactory : IConfigFactory
    {
        public Config CreateConfig()
        {
            Config conf = new Config();
            conf.CreateFolder = false;
            conf.FolderName = "jpg";
            return conf;
        }
    }
}
