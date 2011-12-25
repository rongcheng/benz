using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;

namespace QJVRMS.Common
{
    /// <summary>
    ///  Author: SuNan
    ///  Date: 2007.09.14 
    ///  对象序列化工厂
    /// </summary>
    public class SerializeObjectFactory : ISerializeFactory
    {

        /// <summary>
        /// 序列化为二进制文件
        /// </summary>
        /// <param name="o">序列化的对象</param>
        /// <param name="fileFullPath">二进制文件完整路径</param>
        public void SerializeToBinary(object o, string fileFullPath)
        {
            using (FileStream fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, o);
            }
        }

        /// <summary>
        /// 从二进制文件反序列化对象
        /// </summary>
        /// <param name="fileFullPath">二进制文件完整路径</param>
        /// <returns>Object类型</returns>
        public object DeserializeFromBinary(string fileFullPath)
        {
            object o = null;
            using (FileStream fs = new FileStream(fileFullPath, FileMode.Open))
            {
                fs.Seek(0, SeekOrigin.Begin);
                BinaryFormatter formatter = new BinaryFormatter();
                o = formatter.Deserialize(fs);
            }

            return o;
        }

        /// <summary>
        /// 序列化为Xml文件
        /// </summary>
        /// <param name="o">序列化对象</param>
        /// <param name="fileFullPath">Xml文件完整路径</param>
        /// <param name="t">对象类型</param>
        public void SerializeToXml(object o, string fileFullPath, Type t)
        {
            using (FileStream fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                XmlSerializer formatter = new XmlSerializer(t);
                formatter.Serialize(fs, o);
            }
        }

        /// <summary>
        /// 从Xml文件反序列化对象
        /// </summary>
        /// <param name="fileFullPath">文件完整路径</param>
        /// <param name="t">对象类型</param>
        /// <returns>Object类型</returns>
        public object DeserializeFromXml(string fileFullPath, Type t)
        {
            object o = null;
            using (FileStream sr = new FileStream(fileFullPath, FileMode.Open))
            {
                sr.Seek(0, SeekOrigin.Begin);
                XmlSerializer formatter = new XmlSerializer(t);
                o = formatter.Deserialize(sr);
            }

            return o;
        }


        /// <summary>
        /// 将对象序列化为Base64字符串
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public   string SerializeToBase64(object o)
        {
            BinaryFormatter format = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            format.Serialize(ms, o);

            byte[] objectStream = ms.ToArray();

            return Convert.ToBase64String(objectStream);
        }

        /// <summary>
        /// 将Base64字符串序列化为对象
        /// </summary>
        /// <param name="base64Str"></param>
        /// <returns></returns>
        public   object DesializeFromBase64(string base64Str)
        {
            BinaryFormatter format = new BinaryFormatter();


            byte[] objectBytes = Convert.FromBase64String(base64Str);

            MemoryStream ms = new MemoryStream(objectBytes);

            return format.Deserialize(ms);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(object obj)
        {
            // 首先，当然是JSON序列化
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());

            // 定义一个stream用来存发序列化之后的内容
            Stream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);

            // 从头到尾将stream读取成一个字符串形式的数据，并且返回
            stream.Position = 0;
            StreamReader streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }
    }
}
