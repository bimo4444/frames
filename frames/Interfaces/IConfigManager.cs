using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frames
{
    interface IConfigManager
    {
        Config InitConfig();
        void Save<T>(T conf);
    }
}
