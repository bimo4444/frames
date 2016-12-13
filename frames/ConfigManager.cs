using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace frames
{
    class ConfigManager : IConfigManager
    {
        IConfigHolder configHolder;
        IConfigFactory configFactory;
        const string fileName = "conf.dat";
        public ConfigManager(IConfigFactory configFactory, IConfigHolder configHolder)
        {
            this.configFactory = configFactory;
            this.configHolder  = configHolder;
        }
        public Config InitConfig()
        {
            object obj = configHolder.Read(fileName);
            Config conf = obj as Config;
            if (conf != null)
                return conf;
            return configFactory.CreateConfig();
        }
        public void Save<T>(T conf)
        {
            configHolder.Save(conf, fileName);
        }

    }
}
