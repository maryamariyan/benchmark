using hwapp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MyBenchmarks
{
    public static class Serializer
    {

        #region serializing

        public static string SerializeJobData(CC<int, int> myDictionary)
        {
            return ToBase64String(myDictionary);
        }

        public static CC<int, int> DeserializeData(string RawData)
        {
            CC<int, int> myDictionary = (CC<int, int>)FromBase64String(RawData);
            return myDictionary;
        }

        public static string SerializeJobData(DD<int, int> myDictionary)
        {
            return ToBase64String(myDictionary);
        }

        public static DD<int, int> DeserializeDataDiff(string RawData)
        {
            DD<int, int> myDictionary = (DD<int, int>)FromBase64String(RawData);
            return myDictionary;
        }

        public static byte[] ToByteArray(object obj,
            FormatterAssemblyStyle assemblyStyle = FormatterAssemblyStyle.Full)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.AssemblyFormat = assemblyStyle;
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static string ToBase64String(object obj,
            FormatterAssemblyStyle assemblyStyle = FormatterAssemblyStyle.Full)
        {
            byte[] raw = ToByteArray(obj, assemblyStyle);
            return Convert.ToBase64String(raw);
        }

        public static object FromByteArray(byte[] raw,
            FormatterAssemblyStyle assemblyStyle = FormatterAssemblyStyle.Full)
        {
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.AssemblyFormat = assemblyStyle;
            using (var serializedStream = new MemoryStream(raw))
            {
                return binaryFormatter.Deserialize(serializedStream);
            }
        }

        public static object FromBase64String(string base64Str,
            FormatterAssemblyStyle assemblyStyle = FormatterAssemblyStyle.Full)
        {
            byte[] raw = Convert.FromBase64String(base64Str);
            return FromByteArray(raw, assemblyStyle);
        }
        #endregion
    }
}
