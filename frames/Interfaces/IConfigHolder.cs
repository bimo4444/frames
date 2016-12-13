using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace frames
{
    interface IConfigHolder
    {
        object Read(string fileName);
        void Save<T>(T item, string fileName);
    }
}
