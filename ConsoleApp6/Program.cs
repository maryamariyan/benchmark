using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Code;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Running;
using hwapp;
using MyBenchmarks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using static MyBenchmarks.IntroIParam;

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
                var summary = BenchmarkRunner.Run<IntroIParam>(new MainConfig());

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
    }
}

namespace MyBenchmarks
{
    //[MySuperJob]
    //[SimpleJob(RunStrategy.Monitoring, launchCount: 1, warmupCount: 1, targetCount: 10)]
    [SimpleJob(RunStrategy.Monitoring, targetCount: 30)]
    [AllStatisticsColumn]
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
            public readonly string befstring;
            public readonly string name;
            public readonly int origSize;
            public readonly int addOrResizeSize;

            public ResizeInputElements(string nam, string diffdictstring, string dictionarystring, string beffstring, int orig, int trim)
            {
                name = nam;
                diffstring = diffdictstring;
                dictstring = dictionarystring;
                befstring = beffstring;
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
            public string ToSourceCode() => $"new ResizeInputElements(\"{value.name}\", \"{value.diffstring}\", \"{value.dictstring}\", \"{value.befstring}\", {value.origSize}, {value.addOrResizeSize})";
        }

        //[ParamsSource(nameof(Parameters))]
        //public ResizeInputElements Field;

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
            BB<int, int> bef;
            ResizeInputElements inputElement;

            int initCount = HashHelpers.ExpandPrime(count);
            int removeCount = 0;// count;
            int initCapacity = count;

            diff = generator.ZombiesAreScatteredDiff(rand, initCount, removeCount, initCapacity);
            dict = generator.ZombiesAreScattered(rand, initCount, removeCount, initCapacity);
            bef = generator.ZombiesAreScatteredBef(rand, initCount, removeCount, initCapacity);
            int resizeTo = HashHelpers.ExpandPrime(dict.EnsureCapacity(0));//dict.Count;
            inputElement = new ResizeInputElements(
                GetName(nameof(generator.ZombiesAreScattered), initCount, dict.EnsureCapacity(0), dict.Count, initCapacity),
                Serializer.SerializeJobData(diff),
                Serializer.SerializeJobData(dict),
                Serializer.SerializeJobData(bef),
                initCount,
                resizeTo);
            /*if (_generator.TrimWillResize(dict, resizeTo) && _generator.TrimWillResize(diff, resizeTo))*/ yield return inputElement;
        }

        public IEnumerable<ResizeInputElements> InParameters()
        {
            var generator = new CustomizableInputGenerator(1000000);
            int[] counts = { 10000 };
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
                    foreach (var item in UseCaseC(generator, _random, count))
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
        
        DD<int, int> d;
        CC<int, int> c;
        BB<int, int> b;
        int prime;
        private static Random _random = new Random(42);

        [IterationSetup(Target = nameof(Approach2))]
        public void IterationSetupApproach2()
        {
            int count = 10000;

            int initCount = HashHelpers.ExpandPrime(count);
            int removeCount = 0;// count;
            int initCapacity = count;

            var diff = _generator.ZombiesAreScatteredDiff(_random, initCount, removeCount, initCapacity);
            var resizeTo = HashHelpers.ExpandPrime(diff.EnsureCapacity(0));//dict.Count;
            
            /*if (_generator.TrimWillResize(dict, resizeTo) && _generator.TrimWillResize(diff, resizeTo))*/
            //yield return inputElement;

            d = diff;
            prime = HashHelpers.GetPrime(resizeTo);
        }

        [IterationSetup(Target = nameof(Approach1))]
        public void IterationSetupApproach1()
        {
            int count = 10000;

            int initCount = HashHelpers.ExpandPrime(count);
            int removeCount = 0;// count;
            int initCapacity = count;
            
            var dict = _generator.ZombiesAreScattered(_random, initCount, removeCount, initCapacity);
            var resizeTo = HashHelpers.ExpandPrime(dict.EnsureCapacity(0));//dict.Count;

            /*if (_generator.TrimWillResize(dict, resizeTo) && _generator.TrimWillResize(diff, resizeTo))*/
            //yield return inputElement;

            c = dict;
            prime = HashHelpers.GetPrime(resizeTo);
        }

        [IterationSetup(Target = nameof(Before))]
        public void IterationSetupBefore()
        {
            int count = 10000;

            int initCount = HashHelpers.ExpandPrime(count);
            int removeCount = 0;// count;
            int initCapacity = count;
            
            var bef = _generator.ZombiesAreScatteredBef(_random, initCount, removeCount, initCapacity);
            var resizeTo = HashHelpers.ExpandPrime(bef.EnsureCapacity(0));//dict.Count;

            /*if (_generator.TrimWillResize(dict, resizeTo) && _generator.TrimWillResize(diff, resizeTo))*/
            //yield return inputElement;

            b = bef;
            prime = HashHelpers.GetPrime(resizeTo);
        }

        [Benchmark]
        public void Approach2() => d.Resize(prime, false);

        [Benchmark]
        public void Approach1() => c.Resize(prime, false);

        [Benchmark]
        public void Before() => b.Resize(prime, false);
    }
}