using hwapp;
using System;

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
                input.Add(item, 0);
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
                input.Add(item, 0);
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
                input.Add(item, 0);
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
                input.Add(item, 0);
            }

            for (int i = 0; i < removeCount; i++)
            {
                input.Remove(keys[addCount - i - 1]);
            }
            return input;
        }

        public bool TrimWillResize(CustomDictionary<int, int> input, int capacity)
        {
            if (capacity < input.Count)
            {
                return false;
            }

            int newSize = HashHelpers.GetPrime(capacity);
            if (newSize > input.EnsureCapacity(0))
            {
                return false;
            }
            return true;
        }

        public bool TrimWillResize(DifferentDictionary<int, int> input, int capacity)
        {
            if (capacity < input.Count)
            {
                return false;
            }

            int newSize = HashHelpers.GetPrime(capacity);
            if (newSize > input.EnsureCapacity(0))
            {
                return false;
            }
            return true;
        }

        public CustomDictionary<int, int> AddThenRemoveAtRandom(CustomDictionary<int, int> input, Random rand, int addCount, int removeCount)
        {
            var abc = PickNumbers(input);
            _ran.Shuffle(abc);
            int[] keys = new int[addCount];
            Array.Copy(abc, keys, addCount);

            foreach (var item in keys)
            {
                input.Add(item, 0);
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
                input.Add(item, 0);
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
                input.Add(item, 0);
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
                input.Add(item, 0);
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
                input.Add(item, 0);
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
                input.Add(item, 0);
            }
            return input;
        }

        public DifferentDictionary<int, int> ZombiesAreScatteredDiff(Random rand, int size, float percentageToRemove, float initCapacityPercentage)
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
        ///  zombies at front
        /// </summary>
        public DifferentDictionary<int,int> ZombiesAtFrontDiff(Random rand, int size, int removeCount, float initCapacityPercentage)
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
        ///  zombies at front
        /// </summary>
        public DifferentDictionary<int, int> ZombiesAtEndDiff(Random rand, int size, int removeCount, float initCapacityPercentage)
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

        public DifferentDictionary<int, int> DictionaryAllEntriesRemovedAddAgainDiff(Random rand, int size, int addAgainCount, float initCapacityPercentage)
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

            return AddThenRemoveAtRandomThenAddAgain(input, rand, size, size, addAgainCount);
        }

        public DifferentDictionary<int, int> DictionaryFullDiff(Random rand, int size, float initCapacityPercentage)
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
        

        public CustomDictionary<int, int> ZombiesAreScattered(Random rand, int size, float percentageToRemove, float initCapacityPercentage)
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
            return AddThenRemoveAtRandom(input, rand, size, size - newCount);
        }


        /// <summary>
        ///  zombies at front
        /// </summary>
        public CustomDictionary<int, int> ZombiesAtFront(Random rand, int size, int removeCount, float initCapacityPercentage)
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
        ///  zombies at front
        /// </summary>
        public CustomDictionary<int, int> ZombiesAtEnd(Random rand, int size, int removeCount, float initCapacityPercentage)
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

        public CustomDictionary<int, int> DictionaryAllEntriesRemovedAddAgain(Random rand, int size, int addAgainCount, float initCapacityPercentage)
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
            
            return AddThenRemoveAtRandomThenAddAgain(input, rand, size, size, addAgainCount);
        }

        public CustomDictionary<int, int> DictionaryFull(Random rand, int size, float initCapacityPercentage)
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
