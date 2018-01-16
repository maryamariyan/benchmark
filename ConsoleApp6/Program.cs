using System;
using BenchmarkDotNet.Running;
using MyBenchmarks;
using BenchmarkDotNet.Attributes;
using hwapp;
using System.Collections.Generic;
using BenchmarkDotNet.Code;
using static MyBenchmarks.IntroIParam;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ConsoleApp6;

namespace hwapp
{
    public class Program
    {
        static void Main(string[] args)
        {
            // new Resize1VsResize2().TestBothResizeApisOnDefaultInput();

            //check implemented resize no worse on any input


            var rand = new Random(42);
            var generator = new CustomizableInputGenerator();
            var dict = generator.WithPercentageAsZombiesAtRandom(rand, 10000, 0.5f);
            string rawdata = SerializeJobData(dict);
            Console.WriteLine($"rawdata {rawdata}");
            var dict2 = DeserializeData(rawdata);
            TestBothDictionaryHaveSameEnumeration(dict, dict2);

            string rawdata2 = ToBase64String(new DataItem[] { new DataItem(123, 321), new DataItem(123, 321) });
            Console.WriteLine($"rawdata on dataitem {rawdata}");
            var item = (DataItem[])FromBase64String(rawdata2);
            Console.WriteLine($"rawdata back {item[0]} and {item[1]}");

            //var summary = BenchmarkRunner.Run<IntroIParam>();
        }

        private static void AssertDictionaryEnumerateAndCountRemainsUnchanged(
            List<int> orderedKeys,
            List<int> orderedValues,
            CustomDictionary<int, int> dictionary)
        {
            if (dictionary.Count != orderedKeys.Count) throw new Exception("wrong _dictionary.Count != orderedKeys.Count");
            if (dictionary.Count != orderedValues.Count) throw new Exception("wrong _dictionary.Count != orderedValues.Count");

            var keys = orderedKeys.GetEnumerator();
            var values = orderedValues.GetEnumerator();
            foreach (var item in dictionary)
            {
                keys.MoveNext();
                values.MoveNext();
                if (keys.Current != item.Key) throw new Exception($"wrong ki.Current != item.Key");
                if (values.Current != item.Value) throw new Exception($"wrong if (vi.Current != item.Value)");
            }
        }

        /// <summary>
        /// Call input and test Resize won't change dictionary
        /// Compare contents after Resize (count and enumerate and compare)
        /// </summary>
        public static void TestBothDictionaryHaveSameEnumeration(CustomDictionary<int, int> _dictionary, CustomDictionary<int, int> dict2)
        {
            var orderedKeys = new List<int>();
            var orderedValues = new List<int>();
            foreach (var item in _dictionary)
            {
                orderedKeys.Add(item.Key);
                orderedValues.Add(item.Value);
            }
            AssertDictionaryEnumerateAndCountRemainsUnchanged(orderedKeys, orderedValues, _dictionary);

            _dictionary = dict2;
            AssertDictionaryEnumerateAndCountRemainsUnchanged(orderedKeys, orderedValues, _dictionary);
        }
    }
}

// Benchmark docs: 
// [Params], [IterationTime]

namespace MyBenchmarks
{
    public class IntroIParam
    {
        public struct InputElements
        {
            public readonly string dictstring;
            public readonly int origSize;
            public readonly int trimSize;

            public InputElements(string dictionarystring, int orig, int trim)
            {
                Console.WriteLine(dictionarystring);
                dictstring = dictionarystring;

                // deserialize from string
                //dict = (CustomDictionary<int,int>)CustomDictionary<int,int>.FromBase64String(dictionarystring);
                origSize = orig;
                trimSize = trim;
            }

        }

        public class CustomParam : IParam
        {
            private readonly InputElements value;

            public CustomParam(InputElements value) => this.value = value;

            public object Value => value;

            public string DisplayText => $"({value.dictstring},{value.origSize},{value.trimSize})";

            // serializes my object to string
            public string ToSourceCode() => $"new InputElements({value.dictstring}, {value.origSize}, {value.trimSize})";
        }

        [ParamsSource(nameof(Parameters))]
        public InputElements Field;

        public IEnumerable<IParam> Parameters()
        {
            var rand = new Random(42);
            var generator = new CustomizableInputGenerator();
            var dict = generator.WithPercentageAsZombiesAtRandom(rand, 10000, 0.5f);
            string rawdata = SerializeJobData(dict);

            var inputElement = new InputElements(rawdata, 10000, 7000);
            yield return new CustomParam(inputElement);
            //yield return new CustomParam(new VeryCustomStruct(100, 10));
            //yield return new CustomParam(new VeryCustomStruct(100, 20));
            //yield return new CustomParam(new VeryCustomStruct(200, 10));
            //yield return new CustomParam(new VeryCustomStruct(200, 20));
        }

        public static string SerializeJobData(CustomDictionary<int, int> myDictionary)
        {
            Console.WriteLine("a");
            var tempdataitems = new DataItem[myDictionary.Count];

            int i = 0;
            foreach (int key in myDictionary.Keys)
            {
                tempdataitems[i] = new DataItem(key, myDictionary[key]);
                i++;
            }

            return ToBase64String(tempdataitems);
            //XmlSerializer serializer = new XmlSerializer(typeof(DataItem[]));
            //StringWriter sw = new StringWriter();
            //XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //Console.WriteLine("b");

            //ns.Add("", "");
            //serializer.Serialize(sw, tempdataitems, ns);
            //return sw.ToString();

        }

        public static CustomDictionary<int,int> DeserializeData(string RawData)
        {
            Console.WriteLine("c");
            CustomDictionary<int, int> myDictionary = new CustomDictionary<int, int>();
            var templist = (DataItem[])FromBase64String(RawData);

            //XmlSerializer xs = new XmlSerializer(typeof(DataItem[]));
            //StringReader sr = new StringReader(RawData);
            //DataItem[] templist = (DataItem[])xs.Deserialize(sr);
            foreach (DataItem di in templist)
            {
                myDictionary.Add(di.Key, di.Value);
            }
            Console.WriteLine("d");
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

        [Benchmark]
        public void Benchmark() => DeserializeData(Field.dictstring).Resize2(Field.trimSize, false);

        [Benchmark]
        public void Benchmark2() => DeserializeData(Field.dictstring).ResizeX(Field.trimSize, false);
    }

    /*
    public class Resize1VsResize2
    {   
        /// <summary>
        /// Write code to gen those dictionary
        /// </summary>
        public IEnumerable<InputElements> GetInput()
        {
            var rand = new Random(42);
            var generator = new CustomizableInputGenerator();

            Console.WriteLine($"(int)(0.65f * 10000) {(int)(0.65f * 10000)}");
            yield return new InputElements(generator.WithPercentageAsZombiesAtRandom(rand, 10000, 0.5f), 10000, (int)(0.65f * 10000), 7000);
            Console.WriteLine($"(int)(0.65f * 10000) = {(int)(0.65f * 10000)}");
            // p.M Large
            // p.M Small
            var customParams = new[] {
               new { M = 10000, N = 5000},
               //new { M = 200, N = 12},
            };

            foreach (var p in customParams)
            {
                // With a lot of zombies, no bias in order of zombies in array
                //yield return new InputElements(WithPercentageAsZombiesAtRandom(rand, p.N, 0.9f), p.N, (int)0.9f, (int)0.95f);

                //// With a half entries as zombies, no bias in order of zombies in array
                //yield return new InputElements(WithPercentageAsZombiesAtRandom(rand, p.N, 0.5f), p.N, (int)0.5f, (int)0.55f);

                //// With not a lot of zombies
                //yield return new InputElements(WithPercentageAsZombiesAtRandom(rand, p.N, 0.1f), p.N, (int)0.01f, (int)0.15f);

                //// With zombies at end
                //yield return new InputElements(WithZombiesAtEnd(rand, p.N, p.M), p.N, p.M, p.M);
                //yield return new InputElements(WithZombiesAtEnd(rand, p.N, p.M), p.N, p.M, p.M + 3);

                //// With zombies at front
                //yield return new InputElements(WithZombiesAtFront(rand, p.N, p.M), p.N, p.M, p.M);
                //yield return new InputElements(WithZombiesAtFront(rand, p.N, p.M), p.N, p.M, p.M + 3);

                //yield return new InputElements(WithDictionaryAllEntriesRemovedAddAgain(rand, p.N, 0), p.N, 0, p.M);
                //yield return new InputElements(WithDictionaryAllEntriesRemovedAddAgain(rand, p.N, 0), p.N, 0, 0);
                //yield return new InputElements(WithDictionaryAllEntriesRemovedAddAgain(rand, p.N, 0), p.N, 0, 3);

                //yield return new InputElements(WithDictionaryAllEntriesRemovedAddAgain(rand, p.N, 1), p.N, 1, 1);
                //yield return new InputElements(WithDictionaryAllEntriesRemovedAddAgain(rand, p.N, 1), p.N, 1, 3);

                //yield return new InputElements(WithDictionaryAllEntriesRemovedAddAgain(rand, p.N, 1), p.N, 3, 3);

                //yield return new InputElements(WithDictionaryFull(rand, p.N), p.N, p.N, p.N);
            }

        }

        [ParamsSource(nameof(GetInput))]
        public InputElements A { get; set; }

//        [Benchmark]
        public void Resize1() => A.dict.ResizeX(A.trimSize, false);

//        [Benchmark]
        public void Resize2() => A.dict.Resize2(A.trimSize, false);
        
        private void AssertDictionaryEnumerateAndCountRemainsUnchanged(
            List<int> orderedKeys,
            List<int> orderedValues,
            CustomDictionary<int, int> dictionary)
        {
            if (dictionary.Count != orderedKeys.Count) throw new Exception("wrong _dictionary.Count != orderedKeys.Count");
            if (dictionary.Count != orderedValues.Count) throw new Exception("wrong _dictionary.Count != orderedValues.Count");

            var keys = orderedKeys.GetEnumerator();
            var values = orderedValues.GetEnumerator();
            foreach (var item in dictionary)
            {
                keys.MoveNext();
                values.MoveNext();
                if (keys.Current != item.Key) throw new Exception($"wrong ki.Current != item.Key");
                if (values.Current != item.Value) throw new Exception($"wrong if (vi.Current != item.Value)");
            }
        }

        /// <summary>
        /// Call input and test Resize won't change dictionary
        /// Compare contents after Resize (count and enumerate and compare)
        /// </summary>
        public void TestBothResizeApisOnDefaultInput()
        {
            CustomDictionary<int,int> _dictionary;
            foreach (var inp in GetInput())
            {
                _dictionary = inp.dict;
                var orderedKeys = new List<int>();
                var orderedValues = new List<int>();
                foreach (var item in _dictionary)
                {
                    orderedKeys.Add(item.Key);
                    orderedValues.Add(item.Value);
                }
                AssertDictionaryEnumerateAndCountRemainsUnchanged(orderedKeys, orderedValues, _dictionary);

                _dictionary.ResizeX(inp.nextSize, false);
                AssertDictionaryEnumerateAndCountRemainsUnchanged(orderedKeys, orderedValues, _dictionary);

                _dictionary = inp.dict;
                orderedKeys = new List<int>();
                orderedValues = new List<int>();
                foreach (var item in _dictionary)
                {
                    orderedKeys.Add(item.Key);
                    orderedValues.Add(item.Value);
                }
                AssertDictionaryEnumerateAndCountRemainsUnchanged(orderedKeys, orderedValues, _dictionary);

                _dictionary.Resize2(inp.nextSize, false);
                AssertDictionaryEnumerateAndCountRemainsUnchanged(orderedKeys, orderedValues, _dictionary);
            }
        }
    }

    */
}