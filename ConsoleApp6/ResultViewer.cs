using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyBenchmarks;
using static MyBenchmarks.IntroIParam;

namespace hwapp
{
    public class ResultViewer
    {

        private static readonly IEnumerable<string> s_64bit = new string[]
        {
            "95e85f8e-67a3-4367-974f-dd24d8bb2ca2",
            "eb3d6fe9-de64-43a9-8f58-bddea727b1ca"
        };

        private static readonly IEnumerable<string> s_32bit = new string[]
        {
            "25b1f130-7517-48e3-96b0-9da44e8bfe0e",
            "ba5a3625-bc38-4bf1-a707-a3cfe2158bae"
        };

        private static void GHI()
        {
            string[] chained = (Environment.Is64BitProcess ? s_64bit : s_32bit).ToArray();
            var dictionary = new DD<string, string>(11);
            string a;
            HashSet<string> other = new HashSet<string>();

            Console.WriteLine($"1 dictionary.EnsureCapacity(0) {dictionary.EnsureCapacity(0)}");
            int m = 0;
            do
            {
                a = Guid.NewGuid().ToString();
                if (!other.Contains(a) && string.Compare(a, chained[0]) != 0 && string.Compare(a, chained[1]) != 0)
                {
                    other.Add(a);
                    dictionary.Add(a, a);
                    m++;
                }
            } while (m < 3);
            Console.WriteLine($"2 dictionary.EnsureCapacity(0) {dictionary.EnsureCapacity(0)}");
            dictionary.Add(chained[0], chained[0]);
            dictionary.Add(chained[1], chained[1]);
            Console.WriteLine($"3 dictionary.EnsureCapacity(0) {dictionary.EnsureCapacity(0)}");

            m = 0;
            do
            {
                a = Guid.NewGuid().ToString();
                if (!other.Contains(a) && string.Compare(a, chained[0]) != 0 && string.Compare(a, chained[1]) != 0)
                {
                    other.Add(a);
                    dictionary.Add(a, a);
                    m++;
                }
            } while (m < 5);
            Console.WriteLine($"4 dictionary.EnsureCapacity(0) {dictionary.EnsureCapacity(0)}");

            foreach (var item in other.Take(2))
            {
                dictionary.Remove(item);
            }
            Console.WriteLine($"5 dictionary.EnsureCapacity(0) {dictionary.EnsureCapacity(0)}");

            for (int i = 0; i < 138; i++)
            {
                a = Guid.NewGuid().ToString();
                other.Add(a);
                dictionary.Add(a, a);
            }
            Console.WriteLine($"6 dictionary.EnsureCapacity(0) {dictionary.EnsureCapacity(0)}");

            string val;
            Console.WriteLine(dictionary.TryGetValue(chained[1], out val));

            if (string.Compare(chained[1], val) != 0)
            {
                Console.WriteLine("heres the bug");
            }

            Console.WriteLine(dictionary.TryGetValue(chained[0], out val));

            if (string.Compare(chained[0], val) != 0)
            {
                Console.WriteLine("heres the bug");
            }
        }

        private static void DEF()
        {
            var dictionary = new DD<string, string>(41);
            string a;
            HashSet<string> other = new HashSet<string>();
            for (int i = 0; i < 38; i++)
            {
                a = Guid.NewGuid().ToString();
                other.Add(a);
                dictionary.Add(a, a);
            }
            string[] chained = (Environment.Is64BitProcess ? s_64bit : s_32bit).ToArray();
            dictionary.Add(chained[0], chained[0]);
            dictionary.Add(chained[1], chained[1]);

            foreach (var item in other)
            {
                dictionary.Remove(item);
            }

            Console.WriteLine($"before trim, dictionary.EnsureCapacity(0)={dictionary.EnsureCapacity(0)}");
            dictionary.TrimExcess(3);

            Console.WriteLine($"after trim, dictionary.Count={dictionary.Count}");
            Console.WriteLine($"after trim, dictionary.EnsureCapacity(0)={dictionary.EnsureCapacity(0)}");

            string val;
            try
            {
                dictionary.TryGetValue(chained[0], out val);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine($"Was expecting exception e: {e.GetType()}, {e.Message}");
            }
        }

        private static void TestUseCaseC()
        {
            var _generator = new CustomizableInputGenerator(1000000);

            var rand = new Random(42);
            var generator = new CustomizableInputGenerator(1000000);
            int[] counts = { 50000 };//,10000,1000 };
            var count = counts[0];
            float[] initCapacityPercentages = { 0.0f };//, 1.0f, 2.0f };//, 1.0f};
            DD<int, int> diff;
            CC<int, int> dict;
            ResizeInputElements inputElement;

            int initCount = HashHelpers.ExpandPrime(count);
            int removeCount = 0;//count;
            int initCapacity = count;

            diff = generator.ZombiesAreScatteredDiff(rand, initCount, removeCount, initCapacity);
            dict = generator.ZombiesAreScattered(rand, initCount, removeCount, initCapacity);
            Console.WriteLine($"capacity became {dict.EnsureCapacity(0)} and should be equal to {HashHelpers.ExpandPrime(initCount)}");

            int resizeTo = HashHelpers.ExpandPrime(dict.EnsureCapacity(0));//dict.Count;
            inputElement = new ResizeInputElements(
                GetName(nameof(generator.ZombiesAreScattered), initCount, dict.EnsureCapacity(0), dict.Count, initCapacity),
                Serializer.SerializeJobData(diff),
                Serializer.SerializeJobData(dict),
                initCount,
                resizeTo);//, 4 * HashHelpers.ExpandPrime(count));

            Console.WriteLine($"side note, HashHelpers.ExpandPrime(131) ={ HashHelpers.ExpandPrime(131)} and HashHelpers.GetPrime(131)={HashHelpers.GetPrime(131)}");
            Console.WriteLine($"side note, HashHelpers.ExpandPrime(100) ={ HashHelpers.ExpandPrime(100)} and HashHelpers.GetPrime(100)={HashHelpers.GetPrime(100)}");

            Console.WriteLine($"old plan: wanted resize to GetPrime({ 2 * HashHelpers.ExpandPrime(count)}) which is  {HashHelpers.GetPrime(4 * HashHelpers.ExpandPrime(count))}");
            Console.WriteLine($"wanna resize to dict.Count={dict.Count} which should be {initCount - removeCount}");
            Console.WriteLine($"initial capacity requested was {initCapacity} I probably got {HashHelpers.GetPrime(initCapacity)}");
            Console.WriteLine($"added {initCount} then removed {removeCount}");
            var jumpedCapacity = HashHelpers.GetPrime(initCapacity);
            for (int i = 0; i < initCount; i++)
            {
                if (jumpedCapacity == i)
                {
                    jumpedCapacity = HashHelpers.ExpandPrime(i);
                }
            }
            Console.WriteLine($"I assume the capacity jumped to {HashHelpers.ExpandPrime(HashHelpers.GetPrime(HashHelpers.ExpandPrime(count)))} or more realistically to {jumpedCapacity}");
            Console.WriteLine($"removed {removeCount}");
            Console.WriteLine($"for dict {GetName(nameof(generator.ZombiesAreScattered), HashHelpers.ExpandPrime(count), dict.EnsureCapacity(0), dict.Count, count)}");
            Console.WriteLine($"for diff {GetName(nameof(generator.ZombiesAreScattered), HashHelpers.ExpandPrime(count), diff.EnsureCapacity(0), diff.Count, count)}");
            try
            {
                if (_generator.TrimWillResize(dict, resizeTo) && _generator.TrimWillResize(diff, resizeTo))
                {
                    dict.Resize(resizeTo, false);
                    Console.WriteLine("good");
                    diff.Resize(resizeTo, false);
                    Console.WriteLine("good");
                }
                else
                {
                    dict.Resize(resizeTo, false);
                    Console.WriteLine("bad");
                    diff.Resize(resizeTo, false);
                    Console.WriteLine("bad");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            System.Threading.Thread.Sleep(20000);
        }

        /// <summary>
        /// Call input and test Resize won't change dictionary
        /// Compare contents after Resize (count and enumerate and compare)
        /// </summary>
        private void TestBothResizeApisOnDefaultInput()
        {
            CC<int, int> _dictionary;
            DD<int, int> _dictionarydiff;
            var iparams = new IntroIParam().InParameters();
            foreach (var inp in iparams)
            {
                Console.WriteLine(inp.name);
                _dictionary = Serializer.DeserializeData(inp.dictstring);
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

                _dictionary = Serializer.DeserializeData(inp.dictstring);
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

                _dictionarydiff = Serializer.DeserializeDataDiff(inp.diffstring);
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

                _dictionarydiff = Serializer.DeserializeDataDiff(inp.diffstring);
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
            CC<int, int> dictionary)
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
        public static void TestBothDictionaryHaveSameEnumeration(CC<int, int> _dictionary, CC<int, int> dict2)
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
