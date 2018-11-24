using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UILogic.Interfaces
{
    public interface ISerializable
    {
        void Serialize<T>(T _object, string path);
        T Deserialize<T>(string path);
    }
}
