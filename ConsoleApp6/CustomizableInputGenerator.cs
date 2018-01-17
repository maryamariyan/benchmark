using hwapp;
using System;
using System.Collections.Generic;

namespace MyBenchmarks
{
    public class CustomizableInputGenerator
    {
        public void RemoveOneItemDiff(DifferentDictionary<int, int> input)
        {
            int midItem = input.Count / 2;
            int i = 0;
            int keyToRemove = -1;
            foreach (var item in input)
            {
                if (i == midItem)
                {
                    keyToRemove = item.Key;
                    break;
                }
                i++;
            }

            int val;
            if (input.TryGetValue(keyToRemove, out val))
            {
                input.Remove(keyToRemove);
            }
        }

        public DifferentDictionary<int, int> WithPercentageAsZombiesAtRandomDiff(Random rand, int size, float percentage, float initCapacityPercentage)
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
            var keysToRemove = new HashSet<int>();
            int key;
            int maxRandValue = 1073741823;
            int goodRange = (int)(maxRandValue * percentage);
            for (int i = 0; i < size; i++)
            {
                key = rand.Next(maxRandValue);
                while (!input.TryAdd(key, rand.Next()))
                    key = rand.Next(maxRandValue);
                if (key < goodRange)
                {
                    keysToRemove.Add(key);
                }
            }
            foreach (var item in keysToRemove)
            {
                input.Remove(item);
            }
            return input;
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

            var keysToRemove = new HashSet<int>();

            int r;
            for (int i = 0; i < size; i++)
            {
                r = rand.Next(1073741823);
                while (!input.TryAdd(r, rand.Next()))
                    r = rand.Next(1073741823);
                if (i < removeCount)
                {
                    keysToRemove.Add(r);
                }
            }
            foreach (var i in keysToRemove)
            {
                input.Remove(i);
            }
            return input;
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
            var keysToRemove = new HashSet<int>();

            int r;
            for (int i = 0; i < size; i++)
            {
                r = rand.Next(1073741823);
                while (!input.TryAdd(r, rand.Next()))
                    r = rand.Next(1073741823);
                if (i >= size - removeCount)
                {
                    keysToRemove.Add(r);
                }
            }
            foreach (var i in keysToRemove)
            {
                input.Remove(i);
            }
            return input;
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
            for (int i = 0; i < size; i++)
            {
                input.TryAdd(rand.Next(1073741823), rand.Next());
            }
            input.Clear();

            for (int i = 0; i < addAgainCount; i++)
            {
                while (!input.TryAdd(rand.Next(1073741823), rand.Next())) ;
            }

            return input;
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
            for (int i = 0; i < size; i++)
            {
                input.TryAdd(rand.Next(1073741823), rand.Next());
            }
            
            return input;
        }
        
        public void RemoveOneItemCurrent(CustomDictionary<int, int> input)
        {
            int midItem = input.Count / 2;
            int i = 0;
            int keyToRemove = -1;
            foreach (var item in input)
            {
                if (i == midItem)
                {
                    keyToRemove = item.Key;
                    break;
                }
                i++;
            }

            int val;
            if (input.TryGetValue(keyToRemove, out val))
            {
                input.Remove(keyToRemove);
            }
        }

        public CustomDictionary<int, int> WithPercentageAsZombiesAtRandomCurrent(Random rand, int size, float percentage, float initCapacityPercentage)
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
            var keysToRemove = new HashSet<int>();
            int key;
            int maxRandValue = 1073741823;
            int goodRange = (int)(maxRandValue * percentage);
            for (int i = 0; i < size; i++)
            {
                key = rand.Next(maxRandValue);
                while (!input.TryAdd(key, rand.Next()))
                    key = rand.Next(maxRandValue);
                if (key < goodRange)
                {
                    keysToRemove.Add(key);
                }
            }
            foreach (var item in keysToRemove)
            {
                input.Remove(item);
            }
            return input;
        }


        /// <summary>
        /// With zombies at front
        /// </summary>
        public CustomDictionary<int, int> WithZombiesAtFrontCurrent(Random rand, int size, int removeCount, float initCapacityPercentage)
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
            var keysToRemove = new HashSet<int>();

            int r;
            for (int i = 0; i < size; i++)
            {
                r = rand.Next(1073741823);
                while (!input.TryAdd(r, rand.Next()))
                    r = rand.Next(1073741823);
                if (i < removeCount)
                {
                    keysToRemove.Add(r);
                }
            }
            foreach (var i in keysToRemove)
            {
                input.Remove(i);
            }
            return input;
        }

        /// <summary>
        /// With zombies at front
        /// </summary>
        public CustomDictionary<int, int> WithZombiesAtEndCurrent(Random rand, int size, int removeCount, float initCapacityPercentage)
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
            var keysToRemove = new HashSet<int>();

            int r;
            for (int i = 0; i < size; i++)
            {
                r = rand.Next(1073741823);
                while (!input.TryAdd(r, rand.Next()))
                    r = rand.Next(1073741823);
                if (i >= size - removeCount)
                {
                    keysToRemove.Add(r);
                }
            }
            foreach (var i in keysToRemove)
            {
                input.Remove(i);
            }
            return input;
        }

        public CustomDictionary<int, int> WithDictionaryAllEntriesRemovedAddAgainCurrent(Random rand, int size, int addAgainCount, float initCapacityPercentage)
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
            for (int i = 0; i < size; i++)
            {
                input.TryAdd(rand.Next(1073741823), rand.Next());
            }
            input.Clear();

            for (int i = 0; i < addAgainCount; i++)
            {
                while (!input.TryAdd(rand.Next(1073741823), rand.Next())) ;
            }

            return input;
        }

        public CustomDictionary<int, int> WithDictionaryFullCurrent(Random rand, int size, float initCapacityPercentage)
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
            for (int i = 0; i < size; i++)
            {
                input.TryAdd(rand.Next(1073741823), rand.Next());
            }

            return input;
        }
    }
}
