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
    ///  �������л�����
    /// </summary>
    public class SerializeObjectFactory : ISerializeFactory
    {

        /// <summary>
        /// ���л�Ϊ�������ļ�
        /// </summary>
        /// <param name="o">���л��Ķ���</param>
        /// <param name="fileFullPath">�������ļ�����·��</param>
        public void SerializeToBinary(object o, string fileFullPath)
        {
            using (FileStream fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, o);
            }
        }

        /// <summary>
        /// �Ӷ������ļ������л�����
        /// </summary>
        /// <param name="fileFullPath">�������ļ�����·��</param>
        /// <returns>Object����</returns>
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
        /// ���л�ΪXml�ļ�
        /// </summary>
        /// <param name="o">���л�����</param>
        /// <param name="fileFullPath">Xml�ļ�����·��</param>
        /// <param name="t">��������</param>
        public void SerializeToXml(object o, string fileFullPath, Type t)
        {
            using (FileStream fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                XmlSerializer formatter = new XmlSerializer(t);
                formatter.Serialize(fs, o);
            }
        }

        /// <summary>
        /// ��Xml�ļ������л�����
        /// </summary>
        /// <param name="fileFullPath">�ļ�����·��</param>
        /// <param name="t">��������</param>
        /// <returns>Object����</returns>
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
        /// ���������л�ΪBase64�ַ���
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
        /// ��Base64�ַ������л�Ϊ����
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
            // ���ȣ���Ȼ��JSON���л�
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());

            // ����һ��stream�����淢���л�֮�������
            Stream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);

            // ��ͷ��β��stream��ȡ��һ���ַ�����ʽ�����ݣ����ҷ���
            stream.Position = 0;
            StreamReader streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }
    }
}
