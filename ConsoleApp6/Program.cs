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
using System.Linq;

namespace hwapp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //ABC();
            //DEF();
            //new Program().TestBothResizeApisOnDefaultInput();
            try
            {
                var summary = BenchmarkRunner.Run<IntroIParam>();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //TestUseCaseC();

            //int a = 0;
            //do
            //{
            //    int.TryParse(Console.ReadLine(), out a);
            //    Console.WriteLine($"HashHelpers.GetPrime(a)={HashHelpers.GetPrime(a)}");
            //} while (a != 0);
        }

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

        private static void ABC()
        {
            var dictionary = new DD<string, string>(41);

            var chainedItems1 = new StringsMatchingNonRandomizedHashCode().Data.Skip(0).Take(20).ToArray();
            var chainedItems2 = new StringsMatchingNonRandomizedHashCode().Data.Skip(20).Take(20).ToArray();

            foreach (var item in chainedItems1)
            {
                dictionary.Add(item, item);
            }

            foreach (var item in chainedItems2)
            {
                dictionary.Add(item, item);
            }

            foreach (var item in chainedItems1)
            {
                dictionary.Remove(item);
            }

            Console.WriteLine($"before trim, dictionary.EnsureCapacity(0)={dictionary.EnsureCapacity(0)}");
            dictionary.TrimExcess(23);

            Console.WriteLine($"after trim, dictionary.Count={dictionary.Count}");
            Console.WriteLine($"after trim, dictionary.EnsureCapacity(0)={dictionary.EnsureCapacity(0)}");

            string val;
            foreach (var item in chainedItems1)
            {
                if (dictionary.TryGetValue(item, out val))
                {
                    Console.WriteLine("oh damn 1");
                }
            }
            foreach (var item in chainedItems2)
            {
                if (!dictionary.TryGetValue(item, out val))
                {
                    Console.WriteLine("oh damn2");
                }
            }
        }

        private static void TestUseCaseC()
        {
            var _generator = new CustomizableInputGenerator(1000000);

            var rand = new Random(42);
            var generator = new CustomizableInputGenerator(1000000);
            int[] counts = { 1000 };//,10000,1000 };
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
                SerializeJobData(diff),
                SerializeJobData(dict),
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

    internal class StringsMatchingNonRandomizedHashCode
    {
        /// <summary>
        /// A list of strings that all have the same hashcode (-1038491930) using the legacy non randomized string hash code algorithm
        /// </summary>
        internal IEnumerable<string> Data => Environment.Is64BitProcess ? s_64bit : s_32bit;

        private static readonly IEnumerable<string> s_64bit = new string[]
        {
            "95e85f8e-67a3-4367-974f-dd24d8bb2ca2",
            "eb3d6fe9-de64-43a9-8f58-bddea727b1ca",
            "9b9bc0c2-5d25-4292-880c-d54bb48c2f37",
            "f18b7e1a-8096-4346-93fa-e3364b6b0733",
            "60b03506-0199-4b8e-8c67-6537334dca0e",
            "cce5ede8-63f9-484f-b4e7-b99dc2d91443",
            "2325db9d-ab54-44fb-a9c2-176c5d288d4f",
            "46cf9c95-ecef-4b8b-92f7-7bee4fa27e46",
            "b5bd9e71-47ba-487d-b8a6-9215a4fecf7f",
            "ad9959d7-65ac-4411-84cd-716a029c87e5",
            "b55428aa-ef81-4ad5-9e37-6b5a8ca9c0e1",
            "5158d6d2-7d4e-4daa-a360-2b4b26756b90",
            "da014f41-88fb-410c-953a-a9b1dfa3a757",
            "5aec72a1-1df3-4e82-a062-7a75f92541d2",
            "9302fa63-19dc-4a9d-9a4d-7157206f022e",
            "59597f29-523d-4fe9-93ed-1bdfb9b08cc9",
            "187fc738-b8de-4df6-ad74-a4f80a75c342",
            "94dd8708-078b-4aee-9f6a-9e0fbf92b80a",
            "470b67ee-2064-4514-9e27-625f8b56b4da",
            "7306a445-8cec-439b-815e-1c19eb508321",
            "e4965b74-5f66-4d7c-9466-3e8a1877c3ec",
            "b8496759-d960-4ba8-9730-579d1d0ed239",
            "536632d6-459e-4ce2-be87-f21a918733fe",
            "e50e0ea6-beab-4fa5-a9f4-9c96b12c139e",
            "5d9c533b-2b3b-4e95-a1c7-fe69c4327618",
            "1b9f655b-09ea-4cf0-91a5-cda8766f13e4",
            "66cc34f5-b87c-45a9-b113-87d6c9d25cd5",
            "4a9b743f-e055-4be4-9662-05af93470538",
            "e0577593-fd31-4898-a88c-3950d0b19b72",
            "ce1d0443-b80b-492e-8b3f-27e98f5e3e9a",
            "05720b31-5ff1-4724-823f-77922031ac01",
            "68286f1b-db7a-4e3d-957c-63f2568235cd",
            "49e291eb-f684-4925-93c4-6c7185366ab8",
            "4de12acb-b914-46b3-9512-ab98691d63d3",
            "55ff0365-5569-4735-a277-2c33250f0f07",
            "720f0ffb-db80-484a-a595-9010ae049af6",
            "e42648ac-35a9-47eb-aab8-ae2b83cf589a",
            "913148cc-d3f1-481e-a79e-2c7d7df8ea89",
            "02ff9719-1b0b-40d1-bc54-d73db9adbf75",
            "bd58c566-8770-4418-8764-3e1297465a02",
            "5ceac45b-32de-45cd-8b0f-e1c2f682de68",
            "5b89bcca-92db-4223-852b-799ea656a2b5",
            "5386efa9-dce3-434a-9ec0-f04c68893ea2",
            "b9b8baf8-91c3-45e9-b80e-7d2826832332",
            "07206b0c-d1b7-4608-b391-dd9697224d04",
            "851ad5d2-8a2e-459f-ac71-5b1267de7fb7",
            "71ca867c-2caa-48d4-86b2-388cc882e214",
            "e4abda6d-0240-4ead-8738-b86fa8f10737",
            "31ec579b-ac0b-4c18-9b19-3275dbab095d",
            "160a8f1e-72fb-41e1-a5ee-51b7d8b862b9",
            "4c2c3a69-0e15-4e9f-9c4f-46b4d5ba51cc",
            "2adfa71a-23dd-4f2b-b37f-32a45047f97f",
            "997e2a2f-fb2f-4195-9a3e-e32b9757f66f",
            "eca9216f-fc27-4c50-9d8e-4f36fadb141c",
            "f2e1bc76-d0d6-4767-8677-3de239ccb369",
            "19d66436-f554-4514-810c-917831d42bae",
            "0740442c-13d0-4ccc-8471-40519ab592c8",
            "71a10371-f9cb-4db1-9b34-33de5a5fdf6f",
            "4b2802ce-6a90-41be-80da-ca5d74db3dee",
            "14e333d5-9296-4ac1-bf0b-b2da050b8218",
            "8a8f0c91-b73c-41e9-b0e2-36c4dc44c272",
            "e578a3af-c8a8-4677-a518-2c55c55311c3",
            "7a1bece9-edac-4ef9-b60d-ea044e6d5213",
            "80e4c8f3-6fbc-49fc-a78f-68a2c008a5ee",
            "5a92d9a1-ccc2-40ad-b16d-fa76792e6136",
            "ef7041ef-768c-4120-b939-d51faf825037",
            "b7e72bbf-d3f1-4e81-9e05-f37a461cb1ca",
            "afd217a5-c324-4b6c-88cb-5ddfe44b71ba",
            "aa497d73-e054-4803-a3a1-c2628f2cda1c",
            "a85fc2b5-b3b4-4448-ab26-f22fc611a2f6",
            "6369c1a4-e037-4813-8cf6-721ba190e216",
            "5f465559-9cef-4bb7-b6c9-776b19832367",
            "9e30427d-da1f-41d9-bc5f-be7bf905b13d",
            "17f75a2e-2c24-410f-bb9c-45b0190eb71d",
            "22d28aee-bb8d-46a4-8da6-da530aba66b2",
            "e23f24d0-a49a-43a5-98dd-9aa47935776b",
            "9bb540e7-d2a5-429d-86c9-4e22704d1adc",
            "30294db6-4fda-4d98-bbdd-b6fe0903196a",
            "68cb69a7-c672-4076-ad56-1ba635bfe923",
            "67ba1ecc-a33c-4eca-82f5-2fa3c5c63b95",
            "2e65193c-0040-4bea-8984-9fadcc16f673",
            "a2ddd2d6-0d3c-46d7-990d-6fd545e687ae",
            "0775d627-55f6-427a-a1e0-8711d00cf186",
            "bac81b76-4166-4c2d-8fda-81ea89ab98a4",
            "24c8fe6b-379a-401f-bda7-2d6fb0224195",
            "e628506c-a2d6-49b9-b8ac-d44a505d7bd2",
            "d4098a97-f749-4e80-bbd9-5a3ee2799150",
            "1dd4c357-6b4b-4c79-8265-61c7ed933c29",
            "204bd760-cb3b-42e1-9229-1ba75e64210a",
            "dac72303-82ba-43b0-a702-116556aeaa67",
            "b0a93178-586f-436d-b73e-daf631de81b5",
            "60792704-3eab-4d80-873f-44a58ca45dd1",
            "602c80ce-6d82-4368-8099-8be4beabe628",
            "1c0a59a4-1f5b-4c8b-ae44-0d2642016eba",
            "0fd9cee5-3ebf-4bea-b171-5aa9faac4973",
            "6f51bd07-2985-4077-ba11-dfd6032aff39",
            "5c0d55c8-8641-4796-8781-12a0bed9ef9d",
            "69098dfe-e5a6-41bd-913c-bb43b5a94661",
            "7d1f34de-1368-4cd8-939c-9a273a5ac89b",
            "70c65496-16b8-42b2-b600-f5848990fe89",
            "899dd758-8f74-4fa7-860b-e70deb15f736",
            "bd8c8608-4056-414c-9100-6591a7d97f7f",
        };

        private static readonly IEnumerable<string> s_32bit = new string[]
        {
            "25b1f130-7517-48e3-96b0-9da44e8bfe0e",
            "ba5a3625-bc38-4bf1-a707-a3cfe2158bae",
            "de8aa2bd-8e3e-4c32-aa2d-903529dd4c25",
            "1b609ebd-557c-4ae4-8cc1-6810fa84804f",
            "92ef9659-3b0e-4076-9cbe-699121181b76",
            "5ef3f71b-e809-44d0-8444-3ce743cc318b",
            "3e2585e2-4433-46a1-b230-c31d8fa59c04",
            "458256ac-db2d-4eb3-96cd-395f1503ac4f",
            "56e70194-dbff-4f6f-bccf-be946c5b95c1",
            "8b74f5a4-da7a-41a4-9fda-ac061db060ac",
            "8b07efa4-32d8-4a08-a0d6-0fbe472b790e",
            "b3e20cb4-161e-4035-b3f4-a2e1aa9b6cea",
            "57600f95-7bdc-421f-bf78-4210417406a3",
            "e4394361-9cf9-4670-bef0-41a3e449a2ac",
            "5593809b-049c-494b-b0a2-5c6707673c05",
            "bc295231-5905-4b02-918f-67e4f729b77a",
            "f76265a6-cb5e-402a-964d-bcce48d716f7",
            "9b0045e4-aa34-4678-af0d-0f7b77502c6a",
            "28344b30-3834-4ff3-ac03-8c3823fa73db",
            "c77b6ae0-e12f-4aad-9775-49720699a61a",
            "6216ff27-9e47-4255-815d-a036d1c5fa22",
            "bed35808-3583-42eb-8138-f5da8cd5c1a8",
            "1c59466a-1f79-4bd5-b335-c3f2171ddf13",
            "857e5a1e-3ee5-431e-a2bc-61f75abe4f23",
            "765eb91e-63fb-463e-aa77-c161ae518e0c",
            "27d33a11-b211-415d-ab57-98fbb19dab80",
            "a598a06b-a103-45d3-8778-b90917609ec3",
            "5309af23-7bcc-4371-9686-c60ffe1d94a9",
            "1336b6fb-cacb-487b-b902-588641b357d1",
            "4a12f53a-46ed-4928-9d66-3b54bbef1f47",
            "398258cf-6db1-4dc3-821b-87b886c96fed",
            "5afe9b1a-c9c0-4253-91f2-5707a664f921",
            "c3c515b6-c478-49f8-947f-1a7ea5233892",
            "63dad189-6dc1-45cf-808f-d28a67a3019e",
            "1f6423c5-feb2-4136-afb0-0254d50e9a70",
            "a5f8e66e-b486-4f75-a64f-c1ca249bacee",
            "3e4493b6-aaba-41ab-b433-ba6ecb9628fe",
            "b3fc43c9-c4ed-4d98-b98e-040e8fe267df",
            "c88ab9e1-b591-4834-99ca-534a11ae9ed9",
            "7ecd60f8-b602-4cd5-adb6-de3eba951039",
            "018675ba-d99d-40fa-8efc-df7535ec622a",
            "d0564e54-5020-46d8-b304-8bb18f72ce9f",
            "8d143702-fd9a-4d97-acf3-0a1cc5ec6c99",
            "2492c7ed-57b0-47dc-8ee1-9e12aaccaeac",
            "5a5078da-3793-4fd0-ab93-6e12c244372e",
            "6aaffdab-c299-49bf-844a-29f786b0a208",
            "30dc4501-a84d-4754-8f16-7016a5cfec30",
            "098ad941-967d-47a2-96cb-ef69bb5a0f55",
            "cd31dff0-6374-4bbb-bcd6-253177bcbf97",
            "2e3f4a71-df5e-4118-8b3b-44987e7dc0d0",
            "55cb7cf5-715c-4b9a-aea9-c9bcb994466f",
            "5f243f86-3f2c-4148-9ee3-93a341ab9520",
            "e7edf3a4-973d-4a01-abe1-28137a42e4ca",
            "a0705753-0cb2-4d70-ac40-1e7204c1eba3",
            "585a0ede-0e33-477e-b683-85c511ebbdae",
            "53c1528c-f8fa-406c-803d-b728311f1238",
            "44b1521c-70b3-4052-a5fb-42feb6217e72",
            "6b17e258-e9a2-4904-b125-7f986f4d1fa4",
            "0a51e368-c142-4fbc-ad7c-aa57078b2ff1",
            "05731380-7e2e-436a-b481-24b8209446b5",
            "54438ed5-d985-4f50-88bd-fec1d563fa5d",
            "2ec5bb77-8a1b-414d-8fb9-0b30e306e1dd",
            "638640b3-5aae-47f7-8bbf-3484fcbc7313",
            "0ca93804-f093-4686-9b5e-8db384d86ea1",
            "1c3e380e-91da-4fed-8c11-86a9186d7f3b",
            "8c2cdb3f-dbc1-470e-b5cb-314c079d1685",
            "81350bd2-54be-43e4-b769-107fff6f5f6d",
            "230d7e17-0c8c-42df-a982-b2c11a6a7b10",
            "afd851f7-8c38-4998-b257-f81931984bfc",
            "71e7c146-84ae-4a39-8494-326c5a590a7c",
            "4f0e00ac-1faf-4b8f-a7cb-dd23db4c4963",
            "a8bebb48-508a-4da2-b9ea-f595587f5d0e",
            "12e7b09d-6926-40a8-99a7-9c4710d41e2c",
            "3d478f86-0dbc-4c08-ba7e-33ad33adeef4",
            "015f68cf-1375-40a4-a833-746f1fcbaa90",
            "14e45d34-2266-4d9c-ae0f-142ab54f9658",
            "c95590a4-79e2-4b35-b684-e841af0c67d5",
            "7fdb92e4-5c32-4456-bb74-a4c57efeee8a",
            "626cb2f4-b9a4-411f-889a-fe5ed23d9b94",
            "e052d0d4-6272-4993-8dec-0cfd412fbcfc",
            "a6a34037-0351-40e4-b76a-0a8d97e84b1f",
            "9aea167f-3b84-46af-949e-6ee8d908494f",
            "88b8b2b7-ddaa-4885-b29d-f95669d0e6c8",
            "002966e8-7c66-456e-9501-4951d2c7e610",
            "7513ff0c-8846-4334-8db7-4a43d5009e87",
            "957b01f6-fb9d-4fc7-9462-44ae0d45ff45",
            "392b2945-c0ca-4f18-a3bb-f807ab185a25",
            "29a641f3-f489-4957-b46d-a83447c14dee",
            "6b66a9d8-e8dc-45f4-af49-51951fef56ed",
            "bdaea449-247f-42e9-b052-be0a1cd53657",
            "61ebc4f0-7c34-4d8d-b251-e9a0cf0cb72b",
            "90ce67f8-0c82-46c5-b316-e1499d7d22aa",
            "5652a086-2511-4bcb-9d16-a4cd3b77fba7",
            "8e1eb345-6236-45a7-a595-49c6bfdc6f9a",
            "4715c04a-245e-45dd-bd83-14b917dd32c7",
            "29921817-25fa-424b-80bd-ad4ddf020e09",
            "029f4a1a-7b7c-49ac-b92b-726cce8091c0",
            "a82f83a0-6d61-4673-9fb3-edb113d481e7",
            "b35f1262-c37c-460b-9084-0f1cd9f2fb4f",
            "b778243a-0cad-4e27-8d33-1e6a90a45534",
            "ea038421-5a6a-4807-8894-0c1e38734a37",
            "d5d48af3-8c83-462e-b2c9-c9532f55822d"
        };
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

        public IEnumerable<ResizeInputElements> UseCaseC(CustomizableInputGenerator generator, Random rand, int count)
        {
            DD<int, int> diff;
            CC<int, int> dict;
            ResizeInputElements inputElement;

            int initCount = HashHelpers.ExpandPrime(count);
            int removeCount = 0;// count;
            int initCapacity = count;

            diff = generator.ZombiesAreScatteredDiff(rand, initCount, removeCount, initCapacity);
            dict = generator.ZombiesAreScattered(rand, initCount, removeCount, initCapacity);
            int resizeTo = HashHelpers.ExpandPrime(dict.EnsureCapacity(0));//dict.Count;
            inputElement = new ResizeInputElements(
                GetName(nameof(generator.ZombiesAreScattered), initCount, dict.EnsureCapacity(0), dict.Count, initCapacity),
                SerializeJobData(diff),
                SerializeJobData(dict),
                initCount,
                resizeTo);
            /*if (_generator.TrimWillResize(dict, resizeTo) && _generator.TrimWillResize(diff, resizeTo))*/ yield return inputElement;
        }

        public IEnumerable<ResizeInputElements> InParameters()
        {
            var rand = new Random(42);
            var generator = new CustomizableInputGenerator(1000000);
            int[] counts = { 1000 };
            float[] initCapacityPercentages = { 0.0f };//, 1.0f, 2.0f };//, 1.0f};
            //DD<int,int> diff;
            //CC<int, int> dict;
            //ResizeInputElements inputElement;

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
                    //diff = generator.ZombiesAreScatteredChangeCapDiff(rand, HashHelpers.ExpandPrime(count), count, count, 3 * HashHelpers.ExpandPrime(count));
                    //dict = generator.ZombiesAreScatteredChangeCap(rand, HashHelpers.ExpandPrime(count), count, count, 3 * HashHelpers.ExpandPrime(count));
                    //inputElement = new ResizeInputElements(GetName(nameof(generator.ZombiesAreScattered), HashHelpers.ExpandPrime(count), dict.EnsureCapacity(0), dict.Count, count), SerializeJobData(diff), SerializeJobData(dict), HashHelpers.ExpandPrime(count), 4 * HashHelpers.ExpandPrime(count));
                    //if (_generator.TrimWillResize(dict, 2 * HashHelpers.ExpandPrime(count)) && _generator.TrimWillResize(diff, 2 * HashHelpers.ExpandPrime(count))) yield return inputElement;

                    // UC C-prime - no manual resize
                    foreach (var item in UseCaseC(generator, rand, count))
                    {
                        yield return item;
                    } 

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
                    //diff = generator.DictionaryFullDiff(rand, count, 2 * HashHelpers.ExpandPrime(count));
                    //dict = generator.DictionaryFull(rand, count, 2 * HashHelpers.ExpandPrime(count));
                    //inputElement = new ResizeInputElements(GetName(nameof(generator.DictionaryFull), count, dict.EnsureCapacity(0), dict.Count, 2 * HashHelpers.ExpandPrime(count)), SerializeJobData(diff), SerializeJobData(dict), count, HashHelpers.ExpandPrime(dict.Count));
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

        public static string GetName(string name, int originalSize, int curCapacity, int newSize, int startCapacity)
        { return $"{name} startCapacity:{startCapacity}, originalSize:{originalSize}, curCapacity:{curCapacity}, newSize:{newSize}, ResizeTo:"; }

        private CustomizableInputGenerator _generator;

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

        [Benchmark]
        public void ResizeNewOnlyDes()
        {
            var d = DeserializeDataDiff(Field.diffstring);
            HashHelpers.GetPrime(Field.addOrResizeSize);
        }

        [Benchmark]
        public void ResizeOldOnlyDes()
        {
            var d = DeserializeData(Field.dictstring);
            HashHelpers.GetPrime(Field.addOrResizeSize);
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
        public void TestBothResizeApisOnDefaultInput()
        {
            CC<int,int> _dictionary;
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