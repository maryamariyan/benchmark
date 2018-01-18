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

namespace hwapp
{
    public class Program
    {
        static void Main(string[] args)
        {
            new Program().TestBothResizeApisOnDefaultInput();
            //var summary = BenchmarkRunner.Run<IntroIParam>();
        }

        /// <summary>
        /// Call input and test Resize won't change dictionary
        /// Compare contents after Resize (count and enumerate and compare)
        /// </summary>
        private void TestBothResizeApisOnDefaultInput()
        {
            CustomDictionary<int, int> _dictionary;
            DifferentDictionary<int, int> _dictionarydiff;
            var iparams = new IntroIParam().InParameters();
            foreach (var inp in iparams)
            {
                Console.WriteLine(inp.name);
                _dictionary = DeserializeData(inp.dictstring);
                var orderedKeys = new List<int>();
                var orderedValues = new List<int>();
                foreach (var item in _dictionary)
                {
                    orderedKeys.Add(item.Key);
                    orderedValues.Add(item.Value);
                }
                AssertDictionaryEnumerateAndCountRemainsUnchanged(orderedKeys, orderedValues, _dictionary);
                Console.WriteLine(inp.addOrResizeSize);
                _dictionary.Resize(inp.addOrResizeSize, false);
                AssertDictionaryEnumerateAndCountRemainsUnchanged(orderedKeys, orderedValues, _dictionary);

                _dictionary = DeserializeData(inp.dictstring);
                orderedKeys = new List<int>();
                orderedValues = new List<int>();
                foreach (var item in _dictionary)
                {
                    orderedKeys.Add(item.Key);
                    orderedValues.Add(item.Value);
                }
                AssertDictionaryEnumerateAndCountRemainsUnchanged(orderedKeys, orderedValues, _dictionary);
                Console.WriteLine(inp.addOrResizeSize);
                _dictionary.Resize(inp.addOrResizeSize, false);
                AssertDictionaryEnumerateAndCountRemainsUnchanged(orderedKeys, orderedValues, _dictionary);

                _dictionarydiff = DeserializeDataDiff(inp.diffstring);
                orderedKeys = new List<int>();
                orderedValues = new List<int>();
                foreach (var item in _dictionary)
                {
                    orderedKeys.Add(item.Key);
                    orderedValues.Add(item.Value);
                }
                AssertDictionaryEnumerateAndCountRemainsUnchanged(orderedKeys, orderedValues, _dictionary);
                Console.WriteLine(inp.addOrResizeSize);
                _dictionary.Resize(inp.addOrResizeSize, false);
                AssertDictionaryEnumerateAndCountRemainsUnchanged(orderedKeys, orderedValues, _dictionary);

                _dictionarydiff = DeserializeDataDiff(inp.diffstring);
                orderedKeys = new List<int>();
                orderedValues = new List<int>();
                foreach (var item in _dictionary)
                {
                    orderedKeys.Add(item.Key);
                    orderedValues.Add(item.Value);
                }
                AssertDictionaryEnumerateAndCountRemainsUnchanged(orderedKeys, orderedValues, _dictionary);
                Console.WriteLine(inp.addOrResizeSize);
                _dictionary.Resize(inp.addOrResizeSize, false);
                AssertDictionaryEnumerateAndCountRemainsUnchanged(orderedKeys, orderedValues, _dictionary);
            }
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
// [IterationTime]

namespace MyBenchmarks
{
    public class IntroIParam
    {
        public IntroIParam()
        {
            _generator = new CustomizableInputGenerator(1000000);
        }

        public struct ResizeInputElements
        {
            public readonly string dictstring;
            public readonly string diffstring;
            public readonly string name;
            public readonly int origSize;
            public readonly int addOrResizeSize;

            public ResizeInputElements(string nam, string diffdictstring, string dictionarystring, int orig, int trim)
            {
                name = nam;
                diffstring = diffdictstring;
                dictstring = dictionarystring;
                origSize = orig;
                addOrResizeSize = trim;
            }

        }

        public class CustomParam : IParam
        {
            private readonly ResizeInputElements value;

            public CustomParam(ResizeInputElements value) => this.value = value;

            public object Value => value;

            public string DisplayText => $"({value.name}{value.addOrResizeSize})";

            // serializes my object to string
            public string ToSourceCode() => $"new ResizeInputElements(\"{value.name}\", \"{value.diffstring}\", \"{value.dictstring}\", {value.origSize}, {value.addOrResizeSize})";
        }

        [ParamsSource(nameof(Parameters))]
        public ResizeInputElements Field;

        public IEnumerable<IParam> Parameters()
        {
            foreach (var inputElement in InParameters())
            {
                yield return new CustomParam(inputElement);
            }
        }

        public IEnumerable<ResizeInputElements> InParameters()
        {
            var rand = new Random(42);
            var generator = new CustomizableInputGenerator(1000000);
            int[] counts = { 100000,10000 };//,10000,1000 };
            float[] initCapacityPercentages = { 0.0f };//, 1.0f, 2.0f };//, 1.0f};
            DifferentDictionary<int,int> diff;
            CustomDictionary<int, int> dict;
            ResizeInputElements inputElement;

            foreach (var perc in initCapacityPercentages)
            {
                foreach (var count in counts)
                {
                    //diff = generator.ZombiesAtEndDiff(rand, count, (int)(0.5 * count), perc);
                    //dict = generator.ZombiesAtEnd(rand, count, (int)(0.5 * count), perc);
                    //inputElement = new ResizeInputElements(GetName(nameof(generator.ZombiesAtEnd), count, dict.EnsureCapacity(0), dict.Count, perc), SerializeJobData(diff), SerializeJobData(dict), count, dict.Count);
                    //if (_generator.TrimWillResize(dict, dict.EnsureCapacity(0) -2) && _generator.TrimWillResize(diff, diff.EnsureCapacity(0) -2)) yield return inputElement;

                    //diff = generator.ZombiesAtFrontDiff(rand, count, (int)(0.5 * count), perc);
                    //dict = generator.ZombiesAtFront(rand, count, (int)(0.5 * count), perc);
                    //inputElement = new ResizeInputElements(GetName(nameof(generator.ZombiesAtFront), count, dict.EnsureCapacity(0), dict.Count, perc), SerializeJobData(diff), SerializeJobData(dict), count, dict.Count);
                    //if (_generator.TrimWillResize(dict, dict.EnsureCapacity(0) - 2) && _generator.TrimWillResize(diff, diff.EnsureCapacity(0) - 2)) yield return inputElement;

                    //diff = generator.ZombiesAreScatteredDiff(rand, count, 0.5f, perc);
                    //dict = generator.ZombiesAreScattered(rand, count, 0.5f, perc);
                    //inputElement = new ResizeInputElements(GetName(nameof(generator.ZombiesAreScattered), count, dict.EnsureCapacity(0), dict.Count, perc), SerializeJobData(diff), SerializeJobData(dict), count, dict.Count);
                    //if (_generator.TrimWillResize(dict, dict.EnsureCapacity(0) - 2) && _generator.TrimWillResize(diff, diff.EnsureCapacity(0) - 2)) yield return inputElement;

                    // UC C
                    diff = generator.ZombiesAreScatteredChangeCapDiff(rand, HashHelpers.ExpandPrime(count), count, count, 3 * HashHelpers.ExpandPrime(count));
                    dict = generator.ZombiesAreScatteredChangeCap(rand, HashHelpers.ExpandPrime(count), count, count, 3 * HashHelpers.ExpandPrime(count));
                    inputElement = new ResizeInputElements(GetName(nameof(generator.ZombiesAreScattered), HashHelpers.ExpandPrime(count), dict.EnsureCapacity(0), dict.Count, count), SerializeJobData(diff), SerializeJobData(dict), count, 2 * HashHelpers.ExpandPrime(count));
                    if (_generator.TrimWillResize(dict, 2 * HashHelpers.ExpandPrime(count)) && _generator.TrimWillResize(diff, 2 * HashHelpers.ExpandPrime(count))) yield return inputElement;

                    //UC B
                    //diff = generator.ZombiesAreScatteredDiff(rand, count, (int)(0.5 * count), 2 * HashHelpers.ExpandPrime(count));
                    //dict = generator.ZombiesAreScattered(rand, count, (int)(0.5 * count), 2 * HashHelpers.ExpandPrime(count));
                    //inputElement = new ResizeInputElements(GetName(nameof(generator.ZombiesAreScattered), count, dict.EnsureCapacity(0), dict.Count, 2 * HashHelpers.ExpandPrime(count)), SerializeJobData(diff), SerializeJobData(dict), count, HashHelpers.ExpandPrime(count));
                    //if (_generator.TrimWillResize(dict, HashHelpers.ExpandPrime(count)) && _generator.TrimWillResize(diff, HashHelpers.ExpandPrime(count))) yield return inputElement;

                    //diff = generator.ZombiesAreScatteredDiff(rand, count, 0.1f, perc);
                    //dict = generator.ZombiesAreScattered(rand, count, 0.1f, perc);
                    //inputElement = new ResizeInputElements(GetName(nameof(generator.ZombiesAreScattered), count, dict.EnsureCapacity(0), dict.Count, perc), SerializeJobData(diff), SerializeJobData(dict), count, dict.Count);
                    //if (_generator.TrimWillResize(dict, dict.EnsureCapacity(0) - 2) && _generator.TrimWillResize(diff, diff.EnsureCapacity(0) - 2)) yield return inputElement;

                    //diff = generator.ZombiesAreScatteredDiff(rand, count, 0.9f, perc);
                    //dict = generator.ZombiesAreScattered(rand, count, 0.9f, perc);
                    //inputElement = new ResizeInputElements(GetName(nameof(generator.ZombiesAreScattered), count, dict.EnsureCapacity(0), dict.Count, perc), SerializeJobData(diff), SerializeJobData(dict), count, dict.Count);
                    //if (_generator.TrimWillResize(dict, dict.EnsureCapacity(0) - 2) && _generator.TrimWillResize(diff, diff.EnsureCapacity(0) - 2)) yield return inputElement;

                    //UC A
                    //diff = generator.DictionaryFullDiff(rand, count, 2*HashHelpers.ExpandPrime(count));
                    //dict = generator.DictionaryFull(rand, count, 2*HashHelpers.ExpandPrime(count));
                    //inputElement = new ResizeInputElements(GetName(nameof(generator.DictionaryFull), count, dict.EnsureCapacity(0), dict.Count, 2* HashHelpers.ExpandPrime(count)), SerializeJobData(diff), SerializeJobData(dict), count, HashHelpers.ExpandPrime(dict.Count));
                    //if (_generator.TrimWillResize(dict, HashHelpers.ExpandPrime(dict.Count)) && _generator.TrimWillResize(diff, HashHelpers.ExpandPrime(diff.Count))) yield return inputElement;

                    //diff = generator.DictionaryAllEntriesRemovedAddAgainDiff(rand, count, 2 * count, perc);
                    //dict = generator.DictionaryAllEntriesRemovedAddAgain(rand, count, 2 * count, perc);
                    //inputElement = new ResizeInputElements(GetName(nameof(generator.DictionaryAllEntriesRemovedAddAgain), count, dict.EnsureCapacity(0), dict.Count, perc), SerializeJobData(diff), SerializeJobData(dict), count, dict.Count);
                    //if (_generator.TrimWillResize(dict, dict.EnsureCapacity(0) - 2) && _generator.TrimWillResize(diff, diff.EnsureCapacity(0) - 2)) yield return inputElement;

                    //diff = generator.DictionaryAllEntriesRemovedAddAgainDiff(rand, count, count, perc);
                    //dict = generator.DictionaryAllEntriesRemovedAddAgain(rand, count, count, perc);
                    //inputElement = new ResizeInputElements(GetName(nameof(generator.DictionaryAllEntriesRemovedAddAgain), count, dict.EnsureCapacity(0), dict.Count, perc), SerializeJobData(diff), SerializeJobData(dict), count, dict.Count);
                    //if (_generator.TrimWillResize(dict, dict.EnsureCapacity(0) - 2) && _generator.TrimWillResize(diff, diff.EnsureCapacity(0) - 2)) yield return inputElement;

                    //diff = generator.DictionaryAllEntriesRemovedAddAgainDiff(rand, count, (int)(0.5 * count), perc);
                    //dict = generator.DictionaryAllEntriesRemovedAddAgain(rand, count, (int)(0.5 * count), perc);
                    //inputElement = new ResizeInputElements(GetName(nameof(generator.DictionaryAllEntriesRemovedAddAgain), count, dict.EnsureCapacity(0), dict.Count, perc), SerializeJobData(diff), SerializeJobData(dict), count, HashHelpers.GetPrime(dict.EnsureCapacity(0) - 10000));
                    //if (_generator.TrimWillResize(dict, dict.EnsureCapacity(0) - 10000) && _generator.TrimWillResize(diff, diff.EnsureCapacity(0) - 10000)) yield return inputElement;

                    //diff = generator.DictionaryAllEntriesRemovedAddAgainDiff(rand, size: HashHelpers.GetPrime(count), addAgainCount: count, initCapacity: HashHelpers.GetPrime(count));
                    //dict = generator.DictionaryAllEntriesRemovedAddAgain(rand, size: HashHelpers.GetPrime(count), addAgainCount: count, initCapacity: HashHelpers.GetPrime(count));
                    //inputElement = new ResizeInputElements(GetName(nameof(generator.DictionaryAllEntriesRemovedAddAgain), HashHelpers.GetPrime(count), dict.EnsureCapacity(0), dict.Count, HashHelpers.GetPrime(count)), SerializeJobData(diff), SerializeJobData(dict), count, HashHelpers.GetPrime(count));
                    //if (_generator.TrimWillResize(dict, HashHelpers.GetPrime(count)) && _generator.TrimWillResize(diff, HashHelpers.GetPrime(count))) yield return inputElement;

                    //diff = generator.DictionaryAllEntriesRemovedAddAgainDiff(rand, count, 10, perc);
                    //dict = generator.DictionaryAllEntriesRemovedAddAgain(rand, count, 10, perc);
                    //inputElement = new ResizeInputElements(GetName(nameof(generator.DictionaryAllEntriesRemovedAddAgain), count, dict.EnsureCapacity(0), dict.Count, perc), SerializeJobData(diff), SerializeJobData(dict), count, dict.Count);
                    //if (_generator.TrimWillResize(dict, dict.EnsureCapacity(0) - 2) && _generator.TrimWillResize(diff, diff.EnsureCapacity(0) - 2)) yield return inputElement;

                    //diff = generator.DictionaryAllEntriesRemovedAddAgainDiff(rand, count, 3, perc);
                    //dict = generator.DictionaryAllEntriesRemovedAddAgain(rand, count, 3, perc);
                    //inputElement = new ResizeInputElements(GetName(nameof(generator.DictionaryAllEntriesRemovedAddAgain), count, dict.EnsureCapacity(0), dict.Count, perc), SerializeJobData(diff), SerializeJobData(dict), count, dict.Count);
                    //if (_generator.TrimWillResize(dict, dict.EnsureCapacity(0) - 2) && _generator.TrimWillResize(diff, diff.EnsureCapacity(0) - 2)) yield return inputElement;

                    //diff = generator.DictionaryAllEntriesRemovedAddAgainDiff(rand, count, 1, perc);
                    //dict = generator.DictionaryAllEntriesRemovedAddAgain(rand, count, 1, perc);
                    //inputElement = new ResizeInputElements(GetName(nameof(generator.DictionaryAllEntriesRemovedAddAgain), count, dict.EnsureCapacity(0), dict.Count, perc), SerializeJobData(diff), SerializeJobData(dict), count, dict.Count);
                    //if (_generator.TrimWillResize(dict, dict.EnsureCapacity(0) - 2) && _generator.TrimWillResize(diff, diff.EnsureCapacity(0) - 2)) yield return inputElement;
                }
            }
        }

        private string GetName(string name, int originalSize, int curCapacity, int newSize, int startCapacity)
        { return $"{name} startCapacity:{startCapacity}, originalSize:{originalSize}, curCapacity:{curCapacity}, newSize:{newSize}, ResizeTo:"; }

        private CustomizableInputGenerator _generator;

        #region serializing

        public static string SerializeJobData(CustomDictionary<int, int> myDictionary)
        {
            return ToBase64String(myDictionary);
        }

        public static CustomDictionary<int, int> DeserializeData(string RawData)
        {
            CustomDictionary<int, int> myDictionary = (CustomDictionary<int, int>)FromBase64String(RawData);
            return myDictionary;
        }
        
        public static string SerializeJobData(DifferentDictionary<int, int> myDictionary)
        {
            return ToBase64String(myDictionary);
        }

        public static DifferentDictionary<int, int> DeserializeDataDiff(string RawData)
        {
            DifferentDictionary<int, int> myDictionary = (DifferentDictionary<int, int>)FromBase64String(RawData);
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
        
        [Benchmark]
        public void ResizeNew()
        {
            var d = DeserializeDataDiff(Field.diffstring);
            d.Resize(HashHelpers.GetPrime(Field.addOrResizeSize), false);
        }

        [Benchmark]
        public void ResizeOld()
        {
            var d= DeserializeData(Field.dictstring);
            d.Resize(HashHelpers.GetPrime(Field.addOrResizeSize), false);
        }

        //[Benchmark]
        public void AddOnceNew()
        {
            Random rand = new Random(42);
            var d = DeserializeDataDiff(Field.diffstring);

            _generator.PickNumbers(d);

            _generator.AddThenRemoveAtRandom(d, rand, 1, 0);
        }

        //[Benchmark]
        public void AddOnceOld()
        {
            Random rand = new Random(42);
            var d = DeserializeData(Field.dictstring);

            _generator.PickNumbers(d);

            _generator.AddThenRemoveAtRandom(d, rand, 1, 0);
        }

        //[Benchmark]
        public void RemoveOnceNew()
        {
            Random rand = new Random(42);
            var d = DeserializeDataDiff(Field.diffstring);

            _generator.PickNumbers(d);

            _generator.AddThenRemoveAtRandom(d, rand, 1, 0);
        }

        //[Benchmark]
        public void RemoveOnceOld()
        {
            Random rand = new Random(42);
            var d = DeserializeData(Field.dictstring);

            _generator.PickNumbers(d);

            _generator.AddThenRemoveAtRandom(d, rand, 1, 0);
        }

        //[Benchmark]
        public void AddNew()
        {
            Random rand = new Random(42);
            var d = DeserializeDataDiff(Field.diffstring);

            _generator.PickNumbers(d);

            _generator.AddThenRemoveAtRandom(d, rand, Field.addOrResizeSize, 0);
        }

        //[Benchmark]
        public void AddOld()
        {
            Random rand = new Random(42);
            var d = DeserializeData(Field.dictstring);

            _generator.PickNumbers(d);

            _generator.AddThenRemoveAtRandom(d, rand, Field.addOrResizeSize, 0);
        }

       // [Benchmark]
        public void AddAndRemoveNew()
        {
            Random rand = new Random(42);
            var d = DeserializeDataDiff(Field.diffstring);

            _generator.PickNumbers(d);

            _generator.AddThenRemoveAtRandom(d, rand, Field.origSize, Field.addOrResizeSize);
        }

        //[Benchmark]
        public void AddAndRemoveOld()
        {
            Random rand = new Random(42);
            var d = DeserializeData(Field.dictstring);

            _generator.PickNumbers(d);

            _generator.AddThenRemoveAtRandom(d, rand, Field.origSize, Field.addOrResizeSize);
        }
    }

    /*
    public class Resize1VsResize2
    {   
        /// <summary>
        /// Write code to gen those dictionary
        /// </summary>
        public IEnumerable<ResizeInputElements> GetInput()
        {
            var rand = new Random(42);
            var generator = new CustomizableInputGenerator();

            Console.WriteLine($"(int)(0.65f * 10000) {(int)(0.65f * 10000)}");
            yield return new ResizeInputElements(generator.ZombiesAreScattered(rand, 10000, 0.5f), 10000, (int)(0.65f * 10000), 7000);
            Console.WriteLine($"(int)(0.65f * 10000) = {(int)(0.65f * 10000)}");
            // p.M Large
            // p.M Small
            var customParams = new[] {
               new { M = 10000, N = 5000},
               //new { M = 200, N = 12},
            };

            foreach (var p in customParams)
            {
                //  a lot of zombies, no bias in order of zombies in array
                //yield return new ResizeInputElements(ZombiesAreScattered(rand, p.N, 0.9f), p.N, (int)0.9f, (int)0.95f);

                ////  a half entries as zombies, no bias in order of zombies in array
                //yield return new ResizeInputElements(ZombiesAreScattered(rand, p.N, 0.5f), p.N, (int)0.5f, (int)0.55f);

                ////  not a lot of zombies
                //yield return new ResizeInputElements(ZombiesAreScattered(rand, p.N, 0.1f), p.N, (int)0.01f, (int)0.15f);

                ////  zombies at end
                //yield return new ResizeInputElements(ZombiesAtEnd(rand, p.N, p.M), p.N, p.M, p.M);
                //yield return new ResizeInputElements(ZombiesAtEnd(rand, p.N, p.M), p.N, p.M, p.M + 3);

                ////  zombies at front
                //yield return new ResizeInputElements(ZombiesAtFront(rand, p.N, p.M), p.N, p.M, p.M);
                //yield return new ResizeInputElements(ZombiesAtFront(rand, p.N, p.M), p.N, p.M, p.M + 3);

                //yield return new ResizeInputElements(DictionaryAllEntriesRemovedAddAgain(rand, p.N, 0), p.N, 0, p.M);
                //yield return new ResizeInputElements(DictionaryAllEntriesRemovedAddAgain(rand, p.N, 0), p.N, 0, 0);
                //yield return new ResizeInputElements(DictionaryAllEntriesRemovedAddAgain(rand, p.N, 0), p.N, 0, 3);

                //yield return new ResizeInputElements(DictionaryAllEntriesRemovedAddAgain(rand, p.N, 1), p.N, 1, 1);
                //yield return new ResizeInputElements(DictionaryAllEntriesRemovedAddAgain(rand, p.N, 1), p.N, 1, 3);

                //yield return new ResizeInputElements(DictionaryAllEntriesRemovedAddAgain(rand, p.N, 1), p.N, 3, 3);

                //yield return new ResizeInputElements(DictionaryFull(rand, p.N), p.N, p.N, p.N);
            }

        }

        [ParamsSource(nameof(GetInput))]
        public ResizeInputElements A { get; set; }

//        [Benchmark]
        public void Resize1() => A.dict.ResizeX(A.addOrResizeSize, false);

//        [Benchmark]
        public void Resize2() => A.dict.Resize2(A.addOrResizeSize, false);
        
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