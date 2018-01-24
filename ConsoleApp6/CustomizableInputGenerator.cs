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

        public int[] PickNumbers(DD<int, int> input)
        {
            if (input.Count == 0)
            {
                return _firstHalf;
            }
            return _secondHalf;
        }

        public int[] PickNumbers(BB<int, int> input)
        {
            if (input.Count == 0)
            {
                return _firstHalf;
            }
            return _secondHalf;
        }

        public int[] PickNumbers(CC<int, int> input)
        {
            if (input.Count == 0)
            {
                return _firstHalf;
            }
            return _secondHalf;
        }

        public CC<int, int> AddThenRemoveAtFront(CC<int, int> input, Random rand, int addCount, int removeCount)
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

        public DD<int, int> AddThenRemoveAtFront(DD<int, int> input, Random rand, int addCount, int removeCount)
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
        public CC<int, int> AddThenRemoveAtEnd(CC<int, int> input, Random rand, int addCount, int removeCount)
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

        public DD<int, int> AddThenRemoveAtEnd(DD<int, int> input, Random rand, int addCount, int removeCount)
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

        public bool TrimWillResize(CC<int, int> input, int capacity)
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

        public bool TrimWillResize(DD<int, int> input, int capacity)
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

        public BB<int, int> AddThenRemoveAtRandom(BB<int, int> input, Random rand, int addCount, int removeCount, int nextCapacity)
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

            input.EnsureCapacity(nextCapacity);

            for (int i = 0; i < removeCount; i++)
            {
                input.Remove(keys[i]);
            }

            return input;
        }

        public CC<int, int> AddThenRemoveAtRandom(CC<int, int> input, Random rand, int addCount, int removeCount, int nextCapacity)
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

            input.EnsureCapacity(nextCapacity);

            for (int i = 0; i < removeCount; i++)
            {
                input.Remove(keys[i]);
            }

            return input;
        }

        public DD<int, int> AddThenRemoveAtRandom(DD<int, int> input, Random rand, int addCount, int removeCount, int nextCapacity)
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

            input.EnsureCapacity(nextCapacity);

            for (int i = 0; i < removeCount; i++)
            {
                input.Remove(keys[i]);
            }

            return input;
        }

        public CC<int, int> AddThenRemoveAtRandom(CC<int, int> input, Random rand, int addCount, int removeCount)
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

        public BB<int, int> AddThenRemoveAtRandom(BB<int, int> input, Random rand, int addCount, int removeCount)
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

        public DD<int, int> AddThenRemoveAtRandom(DD<int, int> input, Random rand, int addCount, int removeCount)
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

        public DD<int, int> AddThenRemoveAtRandomThenAddAgain(DD<int, int> input, Random rand, int addCount, int removeCount, int addAgainCount)
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

        public CC<int,int> AddThenRemoveAtRandomThenAddAgain(CC<int, int> input, Random rand, int addCount, int removeCount, int addAgainCount)
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

        public DD<int, int> ZombiesAreScatteredChangeCapDiff(Random rand, int size, int removeCount, int initCapacity, int nextCapacity)
        {
            DD<int, int> input;
            int newCount = size - removeCount;
            if (initCapacity == 0.0f)
            {
                input = new DD<int, int>();
            }
            else
            {
                input = new DD<int, int>(initCapacity);
            }

            return AddThenRemoveAtRandom(input, rand, size, size - newCount, nextCapacity);
        }

        public DD<int, int> ZombiesAreScatteredDiff(Random rand, int size, int removeCount, int initCapacity)
        {
            DD<int, int> input;
            int newCount = size - removeCount;
            if (initCapacity == 0.0f)
            {
                input = new DD<int, int>();
            }
            else
            {
                input = new DD<int, int>(initCapacity);
            }

            return AddThenRemoveAtRandom(input, rand, size, size - newCount);
        }

        public DD<int, int> ZombiesAreScatteredDiff(Random rand, int size, float percentageToRemove, float initCapacityPercentage)
        {
            DD<int, int> input;
            int newCount = (int)(size * (1.0f - percentageToRemove));
            if (initCapacityPercentage == 0.0f)
            {
                input = new DD<int, int>();
            }
            else
            {
                input = new DD<int, int>((int)(size * initCapacityPercentage));
            }
            
            return AddThenRemoveAtRandom(input, rand, size, size - newCount);
        }
        
        /// <summary>
        ///  zombies at front
        /// </summary>
        public DD<int,int> ZombiesAtFrontDiff(Random rand, int size, int removeCount, float initCapacityPercentage)
        {
            DD<int,int> input;
            if (initCapacityPercentage == 0.0f)
            {
                input = new DD<int, int>();
            }
            else
            {
                input = new DD<int, int>((int)(size * initCapacityPercentage));
            }
            
            return AddThenRemoveAtFront(input, rand, size, removeCount); ;
        }

        /// <summary>
        ///  zombies at front
        /// </summary>
        public DD<int, int> ZombiesAtEndDiff(Random rand, int size, int removeCount, float initCapacityPercentage)
        {
            DD<int, int> input;
            if (initCapacityPercentage == 0.0f)
            {
                input = new DD<int, int>();
            }
            else
            {
                input = new DD<int, int>((int)(size * initCapacityPercentage));
            }

            return AddThenRemoveAtEnd(input, rand, size, removeCount);
        }

        public DD<int, int> DictionaryAllEntriesRemovedAddAgainDiff(Random rand, int size, int addAgainCount, float initCapacityPercentage)
        {
            DD<int, int> input;
            if (initCapacityPercentage == 0.0f)
            {
                input = new DD<int, int>();
            }
            else
            {
                input = new DD<int, int>((int)(size * initCapacityPercentage));
            }

            return AddThenRemoveAtRandomThenAddAgain(input, rand, size, size, addAgainCount);
        }

        public DD<int, int> DictionaryAllEntriesRemovedAddAgainDiff(Random rand, int size, int addAgainCount, int initCapacity)
        {
            DD<int, int> input;
            if (initCapacity == 0.0f)
            {
                input = new DD<int, int>();
            }
            else
            {
                input = new DD<int, int>(initCapacity);
            }

            return AddThenRemoveAtRandomThenAddAgain(input, rand, size, size, addAgainCount);
        }

        public DD<int, int> DictionaryFullDiff(Random rand, int size, float initCapacityPercentage)
        {
            DD<int, int> input;
            if (initCapacityPercentage == 0.0f)
            {
                input = new DD<int, int>();
            }
            else
            {
                input = new DD<int, int>((int)(size * initCapacityPercentage));
            }

            return AddThenRemoveAtEnd(input, rand, size, 0);
        }

        public DD<int, int> DictionaryFullDiff(Random rand, int size, int initCapacity)
        {
            DD<int, int> input;
            if (initCapacity == 0.0)
            {
                input = new DD<int, int>();
            }
            else
            {
                input = new DD<int, int>(initCapacity);
            }

            return AddThenRemoveAtEnd(input, rand, size, 0);
        }

        public CC<int, int> ZombiesAreScatteredChangeCap(Random rand, int size, int removeCount, int initCapacity, int nextCapacity)
        {
            CC<int, int> input;
            int newCount = size - removeCount;
            if (initCapacity == 0.0f)
            {
                input = new CC<int, int>();
            }
            else
            {
                input = new CC<int, int>(initCapacity);
            }

            return AddThenRemoveAtRandom(input, rand, size, size - newCount, nextCapacity);
        }

        public CC<int, int> ZombiesAreScattered(Random rand, int size, int removeCount, int initCapacity)
        {
            CC<int, int> input;
            int newCount = size - removeCount;
            if (initCapacity == 0.0f)
            {
                input = new CC<int, int>();
            }
            else
            {
                input = new CC<int, int>(initCapacity);
            }

            return AddThenRemoveAtRandom(input, rand, size, size - newCount);
        }



        public BB<int, int> ZombiesAreScatteredBef(Random rand, int size, int removeCount, int initCapacity)
        {
            BB<int, int> input;
            int newCount = size - removeCount;
            if (initCapacity == 0.0f)
            {
                input = new BB<int, int>();
            }
            else
            {
                input = new BB<int, int>(initCapacity);
            }

            return AddThenRemoveAtRandom(input, rand, size, size - newCount);
        }

        public CC<int, int> ZombiesAreScattered(Random rand, int size, float percentageToRemove, float initCapacityPercentage)
        {
            CC<int, int> input;
            int newCount = (int)(size * (1.0f - percentageToRemove));
            if (initCapacityPercentage == 0.0f)
            {
                input = new CC<int, int>();
            }
            else
            {
                input = new CC<int, int>((int)(size * initCapacityPercentage));
            }
            return AddThenRemoveAtRandom(input, rand, size, size - newCount);
        }


        /// <summary>
        ///  zombies at front
        /// </summary>
        public CC<int, int> ZombiesAtFront(Random rand, int size, int removeCount, float initCapacityPercentage)
        {
            CC<int, int> input;
            if (initCapacityPercentage == 0.0f)
            {
                input = new CC<int, int>();
            }
            else
            {
                input = new CC<int, int>((int)(size * initCapacityPercentage));
            }
            
            return AddThenRemoveAtFront(input, rand, size, removeCount);
        }

        /// <summary>
        ///  zombies at front
        /// </summary>
        public CC<int, int> ZombiesAtEnd(Random rand, int size, int removeCount, float initCapacityPercentage)
        {
            CC<int, int> input;
            if (initCapacityPercentage == 0.0f)
            {
                input = new CC<int, int>();
            }
            else
            {
                input = new CC<int, int>((int)(size * initCapacityPercentage));
            }
            
            return AddThenRemoveAtEnd(input, rand, size, removeCount);
        }

        public CC<int, int> DictionaryAllEntriesRemovedAddAgain(Random rand, int size, int addAgainCount, float initCapacityPercentage)
        {
            CC<int, int> input;
            if (initCapacityPercentage == 0.0f)
            {
                input = new CC<int, int>();
            }
            else
            {
                input = new CC<int, int>((int)(size * initCapacityPercentage));
            }
            
            return AddThenRemoveAtRandomThenAddAgain(input, rand, size, size, addAgainCount);
        }

        public CC<int, int> DictionaryAllEntriesRemovedAddAgain(Random rand, int size, int addAgainCount, int initCapacity)
        {
            CC<int, int> input;
            if (initCapacity == 0.0f)
            {
                input = new CC<int, int>();
            }
            else
            {
                input = new CC<int, int>(initCapacity);
            }

            return AddThenRemoveAtRandomThenAddAgain(input, rand, size, size, addAgainCount);
        }

        public CC<int, int> DictionaryFull(Random rand, int size, float initCapacityPercentage)
        {
            CC<int, int> input;
            if (initCapacityPercentage == 0.0f)
            {
                input = new CC<int, int>();
            }
            else
            {
                input = new CC<int, int>((int)(size * initCapacityPercentage));
            }
            
            return AddThenRemoveAtEnd(input, rand, size, 0);
        }

        public CC<int, int> DictionaryFull(Random rand, int size, int initCapacity)
        {
            CC<int, int> input;
            if (initCapacity == 0)
            {
                input = new CC<int, int>();
            }
            else
            {
                input = new CC<int, int>(initCapacity);
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
