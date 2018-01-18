using hwapp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace MyBenchmarks
{
    public class CustomizableInputGenerator
    {
        private int[] _firstHalf;
        private int[] _secondHalf;
        private Random _ran;

        public CustomizableInputGenerator(int maxValue)
        {
            _ran = new Random(42);
            int x = maxValue;
            int h = (int)(maxValue / 2.0);

            _firstHalf = new int[h];
            for (int i = 0; i < _firstHalf.Length; i++)
            {
                _firstHalf[i] = i;
            }
            _ran.Shuffle(_firstHalf);

            _secondHalf = new int[x - h];
            for (int i = 0; i < _secondHalf.Length; i++)
            {
                _secondHalf[i] = int.MaxValue - i;
            }
            _ran.Shuffle(_secondHalf);
        }

        public int[] PickNumbers(DifferentDictionary<int, int> input)
        {
            if (input.Count == 0)
            {
                return _firstHalf;
            }
            return _secondHalf;
        }

        public int[] PickNumbers(CustomDictionary<int, int> input)
        {
            if (input.Count == 0)
            {
                return _firstHalf;
            }
            return _secondHalf;
        }

        public CustomDictionary<int, int> AddThenRemoveAtFront(CustomDictionary<int, int> input, Random rand, int addCount, int removeCount)
        {
            var abc = PickNumbers(input);
            _ran.Shuffle(abc);
            int[] keys = new int[addCount];
            Array.Copy(abc, keys, addCount);

            foreach (var item in keys)
            {
                input.Add(item, rand.Next());
            }

            for (int i = 0; i < removeCount; i++)
            {
                input.Remove(keys[i]);
            }
            return input;
        }

        public DifferentDictionary<int, int> AddThenRemoveAtFront(DifferentDictionary<int, int> input, Random rand, int addCount, int removeCount)
        {
            var abc = PickNumbers(input);
            _ran.Shuffle(abc);
            int[] keys = new int[addCount];
            Array.Copy(abc, keys, addCount);

            foreach (var item in keys)
            {
                input.Add(item, rand.Next());
            }

            for (int i = 0; i < removeCount; i++)
            {
                input.Remove(keys[i]);
            }
            return input;
        }
        public CustomDictionary<int, int> AddThenRemoveAtEnd(CustomDictionary<int, int> input, Random rand, int addCount, int removeCount)
        {
            var abc = PickNumbers(input);
            _ran.Shuffle(abc);
            int[] keys = new int[addCount];
            Array.Copy(abc, keys, addCount);

            foreach (var item in keys)
            {
                input.Add(item, rand.Next());
            }

            for (int i = 0; i < removeCount; i++)
            {
                input.Remove(keys[addCount - i - 1]);
            }
            return input;
        }

        public DifferentDictionary<int, int> AddThenRemoveAtEnd(DifferentDictionary<int, int> input, Random rand, int addCount, int removeCount)
        {
            var abc = PickNumbers(input);
            _ran.Shuffle(abc);
            int[] keys = new int[addCount];
            Array.Copy(abc, keys, addCount);

            foreach (var item in keys)
            {
                input.Add(item, rand.Next());
            }

            for (int i = 0; i < removeCount; i++)
            {
                input.Remove(keys[addCount - i - 1]);
            }
            return input;
        }

        public CustomDictionary<int, int> AddThenRemoveAtRandom(CustomDictionary<int, int> input, Random rand, int addCount, int removeCount)
        {
            var abc = PickNumbers(input);
            _ran.Shuffle(abc);
            int[] keys = new int[addCount];
            Array.Copy(abc, keys, addCount);

            foreach (var item in keys)
            {
                input.Add(item, rand.Next());
            }

            rand.Shuffle(keys);

            for (int i = 0; i < removeCount; i++)
            {
                input.Remove(keys[i]);
            }

            return input;
        }

        public DifferentDictionary<int, int> AddThenRemoveAtRandom(DifferentDictionary<int, int> input, Random rand, int addCount, int removeCount)
        {
            var abc = PickNumbers(input);
            _ran.Shuffle(abc);
            int[] keys = new int[addCount];
            Array.Copy(abc, keys, addCount);

            foreach (var item in keys)
            {
                input.Add(item, rand.Next());
            }

            rand.Shuffle(keys);

            for (int i = 0; i < removeCount; i++)
            {
                input.Remove(keys[i]);
            }

            return input;
        }

        public DifferentDictionary<int, int> AddThenRemoveAtRandomThenAddAgain(DifferentDictionary<int, int> input, Random rand, int addCount, int removeCount, int addAgainCount)
        {
            var abc = PickNumbers(input);
            _ran.Shuffle(abc);
            int[] keys = new int[addCount];
            Array.Copy(abc, keys, addCount);

            foreach (var item in keys)
            {
                input.Add(item, rand.Next());
            }

            rand.Shuffle(keys);

            for (int i = 0; i < removeCount; i++)
            {
                input.Remove(keys[i]);
            }

            abc = PickNumbers(input);
            keys = new int[addAgainCount];
            Array.Copy(abc, addCount, keys, 0, addAgainCount);

            foreach (var item in keys)
            {
                input.Add(item, rand.Next());
            }
            return input;
        }

        public CustomDictionary<int,int> AddThenRemoveAtRandomThenAddAgain(CustomDictionary<int, int> input, Random rand, int addCount, int removeCount, int addAgainCount)
        {
            var abc = PickNumbers(input);
            _ran.Shuffle(abc);
            int[] keys = new int[addCount];
            Array.Copy(abc, keys, addCount);

            foreach (var item in keys)
            {
                input.Add(item, rand.Next());
            }

            rand.Shuffle(keys);

            for (int i = 0; i < removeCount; i++)
            {
                input.Remove(keys[i]);
            }

            abc = PickNumbers(input);
            keys = new int[addAgainCount];
            Array.Copy(abc, addCount, keys, 0, addAgainCount);

            foreach (var item in keys)
            {
                input.Add(item, rand.Next());
            }
            return input;
        }

        public DifferentDictionary<int, int> WithPercentageAsZombiesAtRandomDiff(Random rand, int size, float percentageToRemove, float initCapacityPercentage)
        {
            DifferentDictionary<int, int> input;
            int newCount = (int)(size * (1.0f - percentageToRemove));
            if (initCapacityPercentage == 0.0f)
            {
                input = new DifferentDictionary<int, int>();
            }
            else
            {
                input = new DifferentDictionary<int, int>((int)(size * initCapacityPercentage));
            }
            
            return AddThenRemoveAtRandom(input, rand, size, size - newCount);
        }
        
        /// <summary>
        /// With zombies at front
        /// </summary>
        public DifferentDictionary<int,int> WithZombiesAtFrontDiff(Random rand, int size, int removeCount, float initCapacityPercentage)
        {
            DifferentDictionary<int,int> input;
            if (initCapacityPercentage == 0.0f)
            {
                input = new DifferentDictionary<int, int>();
            }
            else
            {
                input = new DifferentDictionary<int, int>((int)(size * initCapacityPercentage));
            }
            
            return AddThenRemoveAtFront(input, rand, size, removeCount); ;
        }

        /// <summary>
        /// With zombies at front
        /// </summary>
        public DifferentDictionary<int, int> WithZombiesAtEndDiff(Random rand, int size, int removeCount, float initCapacityPercentage)
        {
            DifferentDictionary<int, int> input;
            if (initCapacityPercentage == 0.0f)
            {
                input = new DifferentDictionary<int, int>();
            }
            else
            {
                input = new DifferentDictionary<int, int>((int)(size * initCapacityPercentage));
            }

            return AddThenRemoveAtEnd(input, rand, size, removeCount);
        }

        public DifferentDictionary<int, int> WithDictionaryAllEntriesRemovedAddAgainDiff(Random rand, int size, int addAgainCount, float initCapacityPercentage)
        {
            DifferentDictionary<int, int> input;
            if (initCapacityPercentage == 0.0f)
            {
                input = new DifferentDictionary<int, int>();
            }
            else
            {
                input = new DifferentDictionary<int, int>((int)(size * initCapacityPercentage));
            }

            return AddThenRemoveAtRandomThenAddAgain(input, rand, size, 0, addAgainCount);
        }

        public DifferentDictionary<int, int> WithDictionaryFullDiff(Random rand, int size, float initCapacityPercentage)
        {
            DifferentDictionary<int, int> input;
            if (initCapacityPercentage == 0.0f)
            {
                input = new DifferentDictionary<int, int>();
            }
            else
            {
                input = new DifferentDictionary<int, int>((int)(size * initCapacityPercentage));
            }

            return AddThenRemoveAtEnd(input, rand, size, 0);
        }
        

        public CustomDictionary<int, int> WithPercentageAsZombiesAtRandom(Random rand, int size, float percentageToRemove, float initCapacityPercentage)
        {
            CustomDictionary<int, int> input;
            int newCount = (int)(size * (1.0f - percentageToRemove));
            if (initCapacityPercentage == 0.0f)
            {
                input = new CustomDictionary<int, int>();
            }
            else
            {
                input = new CustomDictionary<int, int>((int)(size * initCapacityPercentage));
            }
            return AddThenRemoveAtRandom(input, rand, size, 0);
        }


        /// <summary>
        /// With zombies at front
        /// </summary>
        public CustomDictionary<int, int> WithZombiesAtFront(Random rand, int size, int removeCount, float initCapacityPercentage)
        {
            CustomDictionary<int, int> input;
            if (initCapacityPercentage == 0.0f)
            {
                input = new CustomDictionary<int, int>();
            }
            else
            {
                input = new CustomDictionary<int, int>((int)(size * initCapacityPercentage));
            }
            
            return AddThenRemoveAtFront(input, rand, size, removeCount);
        }

        /// <summary>
        /// With zombies at front
        /// </summary>
        public CustomDictionary<int, int> WithZombiesAtEnd(Random rand, int size, int removeCount, float initCapacityPercentage)
        {
            CustomDictionary<int, int> input;
            if (initCapacityPercentage == 0.0f)
            {
                input = new CustomDictionary<int, int>();
            }
            else
            {
                input = new CustomDictionary<int, int>((int)(size * initCapacityPercentage));
            }
            
            return AddThenRemoveAtEnd(input, rand, size, removeCount);
        }

        public CustomDictionary<int, int> WithDictionaryAllEntriesRemovedAddAgain(Random rand, int size, int addAgainCount, float initCapacityPercentage)
        {
            CustomDictionary<int, int> input;
            if (initCapacityPercentage == 0.0f)
            {
                input = new CustomDictionary<int, int>();
            }
            else
            {
                input = new CustomDictionary<int, int>((int)(size * initCapacityPercentage));
            }
            
            return AddThenRemoveAtRandomThenAddAgain(input, rand, size, 0, addAgainCount);
        }

        public CustomDictionary<int, int> WithDictionaryFull(Random rand, int size, float initCapacityPercentage)
        {
            CustomDictionary<int, int> input;
            if (initCapacityPercentage == 0.0f)
            {
                input = new CustomDictionary<int, int>();
            }
            else
            {
                input = new CustomDictionary<int, int>((int)(size * initCapacityPercentage));
            }
            
            return AddThenRemoveAtEnd(input, rand, size, 0);
        }
    }
    internal static class RandomExtensions
    {
        public static void Shuffle<T>(this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}
