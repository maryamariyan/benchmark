using hwapp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ConsoleApp6
{
    [Serializable]
    public class DataItem
    {
        public DataItem() { }

        public int Key;
        public int Value;

        public DataItem(int key, int value)
        {
            Key = key;
            Value = value;
        }
    }
}
