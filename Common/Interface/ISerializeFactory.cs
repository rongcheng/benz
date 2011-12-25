using System;
using System.Collections.Generic;
using System.Text;

namespace QJVRMS.Common
{
    public interface ISerializeFactory
    {
        void SerializeToBinary(object o, string fileFullPath);
        object DeserializeFromBinary(string fileFullPath);
        void SerializeToXml(object o, string fileFullPath, Type t);
        object DeserializeFromXml(string fileFullPath, Type t);


        string SerializeToBase64(object o);
        object DesializeFromBase64(string base64Str);
    }
}
