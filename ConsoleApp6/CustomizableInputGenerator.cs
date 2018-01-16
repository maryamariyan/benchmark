using hwapp;
using System;
using System.Collections.Generic;

namespace MyBenchmarks
{
    public class CustomizableInputGenerator
    {
        public void RemoveOneItem(CustomDictionary<int, int> input)
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

        public CustomDictionary<int, int> WithPercentageAsZombiesAtRandom(Random rand, int size, float percentage)
        {
            var input = new CustomDictionary<int, int>(size);
            var keysToRemove = new HashSet<int>();
            int key;
            int maxRandValue = 100000;
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
        public CustomDictionary<int,int> WithZombiesAtFront(Random rand, int size, int removeCount)
        {
            var input = new CustomDictionary<int, int>(size);
            var keysToRemove = new HashSet<int>();

            int r;
            for (int i = 0; i < size; i++)
            {
                r = rand.Next();
                while (!input.TryAdd(r, rand.Next()))
                    r = rand.Next();
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
        public CustomDictionary<int, int> WithZombiesAtEnd(Random rand, int size, int removeCount)
        {
            var input = new CustomDictionary<int, int>(size);
            var keysToRemove = new HashSet<int>();

            int r;
            for (int i = 0; i < size; i++)
            {
                r = rand.Next();
                while (!input.TryAdd(r, rand.Next()))
                    r = rand.Next();
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

        public CustomDictionary<int, int> WithDictionaryAllEntriesRemovedAddAgain(Random rand, int size, int addAgainCount)
        {
            var input = new CustomDictionary<int, int>(size);
            for (int i = 0; i < size; i++)
            {
                input.TryAdd(rand.Next(), rand.Next());
            }
            input.Clear();

            for (int i = 0; i < addAgainCount; i++)
            {
                while (!input.TryAdd(rand.Next(), rand.Next())) ;
            }

            return input;
        }

        public CustomDictionary<int, int> WithDictionaryFull(Random rand, int size)
        {
            var input = new CustomDictionary<int, int>(size);
            for (int i = 0; i < size; i++)
            {
                input.TryAdd(rand.Next(), rand.Next());
            }
            
            return input;
        }
    }
}
