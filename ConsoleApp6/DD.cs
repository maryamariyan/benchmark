﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace hwapp
{

    [DebuggerDisplay("Count = {Count}")]
    [Serializable]
    public class DD<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IReadOnlyDictionary<TKey, TValue>, ISerializable, IDeserializationCallback
    {
        private struct Entry
        {
            public int hashCode;    // Lower 31 bits of hash code, -1 if unused
            public int next;        // Index of next entry, -1 if last
            public TKey key;           // Key of entry
            public TValue value;         // Value of entry
        }

        private int[] _buckets;
        private Entry[] _entries;
        private int _count;
        private int _freeList;
        private int _freeCount;
        private int _version;
        private IEqualityComparer<TKey> _comparer;
        private KeyCollection _keys;
        private ValueCollection _values;
        private object _syncRoot;

        // constants for serialization
        private const string VersionName = "Version"; // Do not rename (binary serialization)
        private const string HashSizeName = "HashSize"; // Do not rename (binary serialization). Must save buckets.Length
        private const string KeyValuePairsName = "KeyValuePairs"; // Do not rename (binary serialization)
        private const string ComparerName = "Comparer"; // Do not rename (binary serialization)

        public DD() : this(0, null) { }

        public DD(int capacity) : this(capacity, null) { }

        public DD(IEqualityComparer<TKey> comparer) : this(0, comparer) { }

        public DD(int capacity, IEqualityComparer<TKey> comparer)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException();// ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
            if (capacity > 0) Initialize(capacity);
            _comparer = comparer ?? EqualityComparer<TKey>.Default;

            if (_comparer == EqualityComparer<string>.Default)
            {
                //_comparer = (IEqualityComparer<TKey>)NonRandomizedStringEqualityComparer.Default;
            }
        }

        public DD(IDictionary<TKey, TValue> dictionary) : this(dictionary, null) { }

        public DD(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) :
            this(dictionary != null ? dictionary.Count : 0, comparer)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException();
                //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
            }

            // It is likely that the passed-in dictionary is DD<TKey,TValue>. When this is the case,
            // avoid the enumerator allocation and overhead by looping through the entries array directly.
            // We only do this when dictionary is DD<TKey,TValue> and not a subclass, to maintain
            // back-compat  subclasses that may have overridden the enumerator behavior.
            if (dictionary.GetType() == typeof(DD<TKey, TValue>))
            {
                DD<TKey, TValue> d = (DD<TKey, TValue>)dictionary;
                int count = d._count;
                Entry[] entries = d._entries;
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].hashCode >= 0)
                    {
                        Add(entries[i].key, entries[i].value);
                    }
                }
                return;
            }

            foreach (KeyValuePair<TKey, TValue> pair in dictionary)
            {
                Add(pair.Key, pair.Value);
            }
        }

        public DD(IEnumerable<KeyValuePair<TKey, TValue>> collection) : this(collection, null) { }

        public DD(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer) :
            this((collection as ICollection<KeyValuePair<TKey, TValue>>)?.Count ?? 0, comparer)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
                //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.collection);
            }

            foreach (KeyValuePair<TKey, TValue> pair in collection)
            {
                Add(pair.Key, pair.Value);
            }
        }

        protected DD(SerializationInfo info, StreamingContext context)
        {
            // We can't do anything  the keys and values until the entire graph has been deserialized
            // and we have a resonable estimate that GetHashCode is not going to fail.  For the time being,
            // we'll just cache this.  The graph is not valid until OnDeserialization has been called.
            HashHelpers.SerializationInfoTable.Add(this, info);
        }

        public IEqualityComparer<TKey> Comparer
        {
            get
            {
                return _comparer;
            }
        }

        public int Count
        {
            get { return _count - _freeCount; }
        }

        public KeyCollection Keys
        {
            get
            {
                if (_keys == null) _keys = new KeyCollection(this);
                return _keys;
            }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get
            {
                if (_keys == null) _keys = new KeyCollection(this);
                return _keys;
            }
        }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get
            {
                if (_keys == null) _keys = new KeyCollection(this);
                return _keys;
            }
        }

        public ValueCollection Values
        {
            get
            {
                if (_values == null) _values = new ValueCollection(this);
                return _values;
            }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get
            {
                if (_values == null) _values = new ValueCollection(this);
                return _values;
            }
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get
            {
                if (_values == null) _values = new ValueCollection(this);
                return _values;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                int i = FindEntry(key);
                if (i >= 0) return _entries[i].value;
                throw new KeyNotFoundException(); //ThrowHelper.ThrowKeyNotFoundException(key);
                                                  //return default(TValue);
            }
            set
            {
                bool modified = TryInsert(key, value, InsertionBehavior.OverwriteExisting);
                Debug.Assert(modified);
            }
        }

        public void Add(TKey key, TValue value)
        {
            bool modified = TryInsert(key, value, InsertionBehavior.ThrowOnExisting);
            Debug.Assert(modified); // If there was an existing key and the Add failed, an exception will already have been thrown.
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            Add(keyValuePair.Key, keyValuePair.Value);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair)
        {
            int i = FindEntry(keyValuePair.Key);
            if (i >= 0 && EqualityComparer<TValue>.Default.Equals(_entries[i].value, keyValuePair.Value))
            {
                return true;
            }
            return false;
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair)
        {
            int i = FindEntry(keyValuePair.Key);
            if (i >= 0 && EqualityComparer<TValue>.Default.Equals(_entries[i].value, keyValuePair.Value))
            {
                Remove(keyValuePair.Key);
                return true;
            }
            return false;
        }

        public void Clear()
        {
            int count = _count;
            if (count > 0)
            {
                int[] buckets = _buckets;
                for (int i = 0; i < buckets.Length; i++)
                {
                    buckets[i] = -1;
                }

                _count = 0;
                _freeList = -1;
                _freeCount = 0;
                _version++;
                Array.Clear(_entries, 0, count);
            }
        }

        public bool ContainsKey(TKey key)
        {
            return FindEntry(key) >= 0;
        }

        public bool ContainsValue(TValue value)
        {
            if (value == null)
            {
                for (int i = 0; i < _count; i++)
                {
                    if (_entries[i].hashCode >= 0 && _entries[i].value == null) return true;
                }
            }
            else
            {
                EqualityComparer<TValue> c = EqualityComparer<TValue>.Default;
                for (int i = 0; i < _count; i++)
                {
                    if (_entries[i].hashCode >= 0 && c.Equals(_entries[i].value, value)) return true;
                }
            }
            return false;
        }

        private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            if (array == null)
            {
                throw new ArgumentNullException();
                //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            }

            if (index < 0 || index > array.Length)
            {
                throw new IndexOutOfRangeException();
                //ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
            }

            if (array.Length - index < Count)
            {
                throw new ArgumentException();
                //ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
            }

            int count = _count;
            Entry[] entries = _entries;
            for (int i = 0; i < count; i++)
            {
                if (entries[i].hashCode >= 0)
                {
                    array[index++] = new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);
                }
            }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this, Enumerator.KeyValuePair);
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return new Enumerator(this, Enumerator.KeyValuePair);
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException();
                //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
            }

            info.AddValue(VersionName, _version);
            info.AddValue(ComparerName, _comparer, typeof(IEqualityComparer<TKey>));
            info.AddValue(HashSizeName, _buckets == null ? 0 : _buckets.Length); // This is the length of the bucket array

            if (_buckets != null)
            {
                var array = new KeyValuePair<TKey, TValue>[Count];
                CopyTo(array, 0);
                info.AddValue(KeyValuePairsName, array, typeof(KeyValuePair<TKey, TValue>[]));
            }
        }

        private int FindEntry(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
                //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }

            if (_buckets != null)
            {
                int hashCode = _comparer.GetHashCode(key) & 0x7FFFFFFF;
                for (int i = _buckets[hashCode % _buckets.Length]; i >= 0; i = _entries[i].next)
                {
                    if (_entries[i].hashCode == hashCode && _comparer.Equals(_entries[i].key, key)) return i;
                }
            }
            return -1;
        }

        private int Initialize(int capacity)
        {
            int size = HashHelpers.GetPrime(capacity);
            int[] buckets = new int[size];
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = -1;
            }

            _freeList = -1;
            _buckets = buckets;
            _entries = new Entry[size];

            return size;
        }

        private bool TryInsert(TKey key, TValue value, InsertionBehavior behavior)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
                //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }

            if (_buckets == null) Initialize(0);
            int hashCode = _comparer.GetHashCode(key) & 0x7FFFFFFF;
            int targetBucket = hashCode % _buckets.Length;
            int collisionCount = 0;

            for (int i = _buckets[targetBucket]; i >= 0; i = _entries[i].next)
            {
                if (_entries[i].hashCode == hashCode && _comparer.Equals(_entries[i].key, key))
                {
                    if (behavior == InsertionBehavior.OverwriteExisting)
                    {
                        _entries[i].value = value;
                        _version++;
                        return true;
                    }

                    if (behavior == InsertionBehavior.ThrowOnExisting)
                    {
                        throw new ArgumentException();
                        //ThrowHelper.ThrowAddingDuplicateKeyArgumentException(key);
                    }

                    return false;
                }
                collisionCount++;
            }

            int index;
            if (_freeCount > 0)
            {
                index = _freeList;
                _freeList = _entries[index].next;
                _freeCount--;
            }
            else
            {
                if (_count == _entries.Length)
                {
                    Resize();
                    targetBucket = hashCode % _buckets.Length;
                }
                index = _count;
                _count++;
            }

            _entries[index].hashCode = hashCode;
            _entries[index].next = _buckets[targetBucket];
            _entries[index].key = key;
            _entries[index].value = value;
            _buckets[targetBucket] = index;
            _version++;

            // If we hit the collision threshold we'll need to switch to the comparer which is using randomized string hashing
            // i.e. EqualityComparer<string>.Default.

            if (collisionCount > HashHelpers.HashCollisionThreshold)//&& _comparer is NonRandomizedStringEqualityComparer)
            {
                _comparer = (IEqualityComparer<TKey>)EqualityComparer<string>.Default;
                Resize(_entries.Length, true);
            }

            return true;
        }

        public virtual void OnDeserialization(object sender)
        {
            SerializationInfo siInfo;
            HashHelpers.SerializationInfoTable.TryGetValue(this, out siInfo);

            if (siInfo == null)
            {
                // We can return immediately if this function is called twice. 
                // Note we remove the serialization info from the table at the end of this method.
                return;
            }

            int realVersion = siInfo.GetInt32(VersionName);
            int hashsize = siInfo.GetInt32(HashSizeName);
            _comparer = (IEqualityComparer<TKey>)siInfo.GetValue(ComparerName, typeof(IEqualityComparer<TKey>));

            if (hashsize != 0)
            {
                Initialize(hashsize);

                KeyValuePair<TKey, TValue>[] array = (KeyValuePair<TKey, TValue>[])
                    siInfo.GetValue(KeyValuePairsName, typeof(KeyValuePair<TKey, TValue>[]));

                if (array == null)
                {
                    throw new Exception();
                    //ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MissingKeys);
                }

                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Key == null)
                    {
                        throw new Exception();
                        //ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_NullKey);
                    }
                    Add(array[i].Key, array[i].Value);
                }
            }
            else
            {
                _buckets = null;
            }

            _version = realVersion;
            HashHelpers.SerializationInfoTable.Remove(this);
        }

        private void Resize()
        {
            Resize(HashHelpers.ExpandPrime(_count), false);
        }

        public void Resize(int newSize, bool forceNewHashCodes)
        {
            Debug.Assert(Count <= newSize);
            int[] buckets = new int[newSize];
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = -1;
            }
            Entry[] entries = new Entry[newSize];

            int count = _count;
            int copyCount = newSize >= count ? count : newSize;
            Array.Copy(_entries, 0, entries, 0, copyCount);

            if (forceNewHashCodes)
            {
                for (int i = 0; i < copyCount; i++)
                {
                    if (entries[i].hashCode != -1)
                    {
                        entries[i].hashCode = (_comparer.GetHashCode(entries[i].key) & 0x7FFFFFFF);
                    }
                }
            }

            for (int i = 0; i < copyCount; i++)
            {
                if (entries[i].hashCode >= 0)
                {
                    int bucket = entries[i].hashCode % newSize;
                    entries[i].next = buckets[bucket];
                    buckets[bucket] = i;
                }
            }

            if (copyCount == count)
            {
                _buckets = buckets;
                _entries = entries;
                return;
            }
            ref Entry[] entriesRef = ref _entries;
            // update free list entries
            int cur = _freeList;
            int freeList = -1;
            int prev = -1;
            while (cur != -1)
            {
                if (cur < newSize)
                {
                    if (prev == -1)
                        freeList = cur;
                    else
                        entries[prev].next = cur;
                    prev = cur;
                }
                cur = entriesRef[cur].next;
            }
            // update hashcode for entries after index (newSize - 1)
            if (forceNewHashCodes)
            {
                for (int i = newSize; i < count; i++)
                {
                    ref Entry entry = ref entriesRef[i];
                    if (entry.hashCode != -1)
                    {
                        entry.hashCode = (_comparer.GetHashCode(entry.key) & 0x7FFFFFFF);
                    }
                }
            }
            // move valid entries after index (newSize - 1)
            for (int i = newSize; i < count; i++)
            {
                if (entriesRef[i].hashCode >= 0)
                {
                    cur = freeList;
                    freeList = entries[cur].next;
                    ref Entry entry = ref entriesRef[i];
                    int bucket = entry.hashCode % newSize;
                    entry.next = buckets[bucket];
                    buckets[bucket] = cur;
                    entries[cur] = entry;
                }
            }
            _freeList = freeList;
            _freeCount -= count - newSize;
            _count -= count - newSize;
            _version++;
            _buckets = buckets;
            _entries = entries;
        }

        // The overload Remove(TKey key, out TValue value) is a copy of this method  one additional
        // statement to copy the value for entry being removed into the output parameter.
        // Code has been intentionally duplicated for performance reasons.
        public bool Remove(TKey key)
        {
            if (key == null)
            {
                throw new Exception();
                //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }

            if (_buckets != null)
            {
                int hashCode = _comparer.GetHashCode(key) & 0x7FFFFFFF;
                int bucket = hashCode % _buckets.Length;
                int last = -1;
                int i = _buckets[bucket];
                while (i >= 0)
                {
                    ref Entry entry = ref _entries[i];

                    if (entry.hashCode == hashCode && _comparer.Equals(entry.key, key))
                    {
                        if (last < 0)
                        {
                            _buckets[bucket] = entry.next;
                        }
                        else
                        {
                            _entries[last].next = entry.next;
                        }
                        entry.hashCode = -1;
                        entry.next = _freeList;

                        if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
                        {
                            entry.key = default(TKey);
                        }
                        if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
                        {
                            entry.value = default(TValue);
                        }
                        _freeList = i;
                        _freeCount++;
                        _version++;
                        return true;
                    }

                    last = i;
                    i = entry.next;
                }
            }
            return false;
        }

        // This overload is a copy of the overload Remove(TKey key)  one additional
        // statement to copy the value for entry being removed into the output parameter.
        // Code has been intentionally duplicated for performance reasons.
        public bool Remove(TKey key, out TValue value)
        {
            if (key == null)
            {
                throw new Exception();
                //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }

            if (_buckets != null)
            {
                int hashCode = _comparer.GetHashCode(key) & 0x7FFFFFFF;
                int bucket = hashCode % _buckets.Length;
                int last = -1;
                int i = _buckets[bucket];
                while (i >= 0)
                {
                    ref Entry entry = ref _entries[i];

                    if (entry.hashCode == hashCode && _comparer.Equals(entry.key, key))
                    {
                        if (last < 0)
                        {
                            _buckets[bucket] = entry.next;
                        }
                        else
                        {
                            _entries[last].next = entry.next;
                        }

                        value = entry.value;

                        entry.hashCode = -1;
                        entry.next = _freeList;

                        if (RuntimeHelpers.IsReferenceOrContainsReferences<TKey>())
                        {
                            entry.key = default(TKey);
                        }
                        if (RuntimeHelpers.IsReferenceOrContainsReferences<TValue>())
                        {
                            entry.value = default(TValue);
                        }
                        _freeList = i;
                        _freeCount++;
                        _version++;
                        return true;
                    }

                    last = i;
                    i = entry.next;
                }
            }
            value = default(TValue);
            return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int i = FindEntry(key);
            if (i >= 0)
            {
                value = _entries[i].value;
                return true;
            }
            value = default(TValue);
            return false;
        }

        public bool TryAdd(TKey key, TValue value) => TryInsert(key, value, InsertionBehavior.None);

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get { return false; }
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            CopyTo(array, index);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            if (array == null)
            {
                throw new Exception();
                //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            }

            if (array.Rank != 1)
            {
                throw new Exception();
                //ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
            }

            if (array.GetLowerBound(0) != 0)
            {
                throw new Exception();
                //ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
            }

            if (index < 0 || index > array.Length)
            {
                throw new Exception();
                //ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
            }

            if (array.Length - index < Count)
            {
                throw new Exception();
                //ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
            }

            KeyValuePair<TKey, TValue>[] pairs = array as KeyValuePair<TKey, TValue>[];
            if (pairs != null)
            {
                CopyTo(pairs, index);
            }
            else if (array is DictionaryEntry[])
            {
                DictionaryEntry[] dictEntryArray = array as DictionaryEntry[];
                Entry[] entries = _entries;
                for (int i = 0; i < _count; i++)
                {
                    if (entries[i].hashCode >= 0)
                    {
                        dictEntryArray[index++] = new DictionaryEntry(entries[i].key, entries[i].value);
                    }
                }
            }
            else
            {
                object[] objects = array as object[];
                if (objects == null)
                {
                    throw new Exception();
                    //ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
                }

                try
                {
                    int count = _count;
                    Entry[] entries = _entries;
                    for (int i = 0; i < count; i++)
                    {
                        if (entries[i].hashCode >= 0)
                        {
                            objects[index++] = new KeyValuePair<TKey, TValue>(entries[i].key, entries[i].value);
                        }
                    }
                }
                catch (ArrayTypeMismatchException)
                {
                    throw new Exception();
                    //ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this, Enumerator.KeyValuePair);
        }

        /// <summary>
        /// Ensures that the dictionary can hold up to 'capacity' entries out any further expansion of its backing storage
        /// </summary>
        public int EnsureCapacity(int capacity)
        {
            if (capacity < 0)
                throw new Exception(); //ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
            if (_entries != null && _entries.Length >= capacity)
                return _entries.Length;
            if (_buckets == null)
                return Initialize(capacity);
            int newSize = HashHelpers.GetPrime(capacity);
            Resize(newSize, forceNewHashCodes: false);
            return newSize;
        }

        /// <summary>
        /// Sets the capacity of this dictionary to hold up at least all existing entries
        /// 
        /// This method can be used to minimize the memory overhead 
        /// once it is known that no new elements will be added. 
        /// 
        /// To completely clear a dictionary and release all memory 
        /// referenced by the dictionary, execute the following statements:
        /// 
        /// dictionary.Clear();
        /// dictionary.TrimExcess();
        /// </summary>
        public void TrimExcess()
        {
            TrimExcess(HashHelpers.GetPrime(Count));
        }

        /// <summary>
        /// Sets the capacity of this dictionary to hold up at least 'capacity' entries out any further expansion of its backing storage
        /// </summary>
        public void TrimExcess(int capacity)
        {
            if (capacity < Count)
            {
                throw new Exception();
                //ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
            }

            int newSize = HashHelpers.GetPrime(capacity);
            if (_entries == null || newSize > _entries.Length)
            {
                return;
            }

            Resize(newSize, false);
        }

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        object ICollection.SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                {
                    System.Threading.Interlocked.CompareExchange<Object>(ref _syncRoot, new Object(), null);
                }
                return _syncRoot;
            }
        }

        bool IDictionary.IsFixedSize
        {
            get { return false; }
        }

        bool IDictionary.IsReadOnly
        {
            get { return false; }
        }

        ICollection IDictionary.Keys
        {
            get { return (ICollection)Keys; }
        }

        ICollection IDictionary.Values
        {
            get { return (ICollection)Values; }
        }

        object IDictionary.this[object key]
        {
            get
            {
                if (IsCompatibleKey(key))
                {
                    int i = FindEntry((TKey)key);
                    if (i >= 0)
                    {
                        return _entries[i].value;
                    }
                }
                return null;
            }
            set
            {
                if (key == null)
                {
                    throw new Exception();
                    //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
                }
                if (value == null)
                {
                    throw new Exception();
                    //ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
                }

                try
                {
                    TKey tempKey = (TKey)key;
                    try
                    {
                        this[tempKey] = (TValue)value;
                    }
                    catch (InvalidCastException)
                    {
                        throw new Exception();
                        //ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
                    }
                }
                catch (InvalidCastException)
                {
                    throw new Exception();
                    //ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
                }
            }
        }

        private static bool IsCompatibleKey(object key)
        {
            if (key == null)
            {
                throw new Exception();
                //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }
            return (key is TKey);
        }

        void IDictionary.Add(object key, object value)
        {
            if (key == null)
            {
                throw new Exception(); //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            }
            if (value == null) throw new Exception(); //ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);

            try
            {
                TKey tempKey = (TKey)key;

                try
                {
                    Add(tempKey, (TValue)value);
                }
                catch (InvalidCastException)
                {
                    throw new Exception(); //ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
                }
            }
            catch (InvalidCastException)
            {
                throw new Exception(); //ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
            }
        }

        bool IDictionary.Contains(object key)
        {
            if (IsCompatibleKey(key))
            {
                return ContainsKey((TKey)key);
            }

            return false;
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return new Enumerator(this, Enumerator.DictEntry);
        }

        void IDictionary.Remove(object key)
        {
            if (IsCompatibleKey(key))
            {
                Remove((TKey)key);
            }
        }

        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>,
            IDictionaryEnumerator
        {
            private DD<TKey, TValue> _dictionary;
            private int _version;
            private int _index;
            private KeyValuePair<TKey, TValue> _current;
            private int _getEnumeratorRetType;  // What should Enumerator.Current return?

            internal const int DictEntry = 1;
            internal const int KeyValuePair = 2;

            internal Enumerator(DD<TKey, TValue> dictionary, int getEnumeratorRetType)
            {
                _dictionary = dictionary;
                _version = dictionary._version;
                _index = 0;
                _getEnumeratorRetType = getEnumeratorRetType;
                _current = new KeyValuePair<TKey, TValue>();
            }

            public bool MoveNext()
            {
                if (_version != _dictionary._version)
                {
                    throw new Exception(); //ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                }

                // Use unsigned comparison since we set index to dictionary.count+1 when the enumeration ends.
                // dictionary.count+1 could be negative if dictionary.count is Int32.MaxValue
                while ((uint)_index < (uint)_dictionary._count)
                {
                    ref Entry entry = ref _dictionary._entries[_index++];

                    if (entry.hashCode >= 0)
                    {
                        _current = new KeyValuePair<TKey, TValue>(entry.key, entry.value);
                        return true;
                    }
                }

                _index = _dictionary._count + 1;
                _current = new KeyValuePair<TKey, TValue>();
                return false;
            }

            public KeyValuePair<TKey, TValue> Current
            {
                get { return _current; }
            }

            public void Dispose()
            {
            }

            object IEnumerator.Current
            {
                get
                {
                    if (_index == 0 || (_index == _dictionary._count + 1))
                    {
                        throw new Exception(); //ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                    }

                    if (_getEnumeratorRetType == DictEntry)
                    {
                        return new System.Collections.DictionaryEntry(_current.Key, _current.Value);
                    }
                    else
                    {
                        return new KeyValuePair<TKey, TValue>(_current.Key, _current.Value);
                    }
                }
            }

            void IEnumerator.Reset()
            {
                if (_version != _dictionary._version)
                {
                    throw new Exception(); //ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                }

                _index = 0;
                _current = new KeyValuePair<TKey, TValue>();
            }

            DictionaryEntry IDictionaryEnumerator.Entry
            {
                get
                {
                    if (_index == 0 || (_index == _dictionary._count + 1))
                    {
                        throw new Exception(); //ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                    }

                    return new DictionaryEntry(_current.Key, _current.Value);
                }
            }

            object IDictionaryEnumerator.Key
            {
                get
                {
                    if (_index == 0 || (_index == _dictionary._count + 1))
                    {
                        throw new Exception();//ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                    }

                    return _current.Key;
                }
            }

            object IDictionaryEnumerator.Value
            {
                get
                {
                    if (_index == 0 || (_index == _dictionary._count + 1))
                    {
                        throw new Exception();//ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                    }

                    return _current.Value;
                }
            }
        }

        [DebuggerDisplay("Count = {Count}")]
        public sealed class KeyCollection : ICollection<TKey>, ICollection, IReadOnlyCollection<TKey>
        {
            private DD<TKey, TValue> _dictionary;

            public KeyCollection(DD<TKey, TValue> dictionary)
            {
                if (dictionary == null)
                {
                    throw new Exception(); //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
                }
                _dictionary = dictionary;
            }

            public Enumerator GetEnumerator()
            {
                return new Enumerator(_dictionary);
            }

            public void CopyTo(TKey[] array, int index)
            {
                if (array == null)
                {
                    throw new Exception(); //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
                }

                if (index < 0 || index > array.Length)
                {
                    throw new Exception(); //ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
                }

                if (array.Length - index < _dictionary.Count)
                {
                    throw new Exception(); //ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
                }

                int count = _dictionary._count;
                Entry[] entries = _dictionary._entries;
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].hashCode >= 0) array[index++] = entries[i].key;
                }
            }

            public int Count
            {
                get { return _dictionary.Count; }
            }

            bool ICollection<TKey>.IsReadOnly
            {
                get { return true; }
            }

            void ICollection<TKey>.Add(TKey item)
            {
                throw new Exception(); //ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
            }

            void ICollection<TKey>.Clear()
            {
                throw new Exception(); //ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
            }

            bool ICollection<TKey>.Contains(TKey item)
            {
                return _dictionary.ContainsKey(item);
            }

            bool ICollection<TKey>.Remove(TKey item)
            {
                throw new Exception(); //ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
                                       //return false;
            }

            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator()
            {
                return new Enumerator(_dictionary);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(_dictionary);
            }

            void ICollection.CopyTo(Array array, int index)
            {
                if (array == null)
                {
                    throw new Exception(); //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
                }

                if (array.Rank != 1)
                {
                    throw new Exception(); //ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
                }

                if (array.GetLowerBound(0) != 0)
                {
                    throw new Exception(); //ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
                }

                if (index < 0 || index > array.Length)
                {
                    throw new Exception(); //ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
                }

                if (array.Length - index < _dictionary.Count)
                {
                    throw new Exception(); //ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
                }

                TKey[] keys = array as TKey[];
                if (keys != null)
                {
                    CopyTo(keys, index);
                }
                else
                {
                    object[] objects = array as object[];
                    if (objects == null)
                    {
                        throw new Exception(); //ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
                    }

                    int count = _dictionary._count;
                    Entry[] entries = _dictionary._entries;
                    try
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (entries[i].hashCode >= 0) objects[index++] = entries[i].key;
                        }
                    }
                    catch (ArrayTypeMismatchException)
                    {
                        throw new Exception(); //ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
                    }
                }
            }

            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            object ICollection.SyncRoot
            {
                get { return ((ICollection)_dictionary).SyncRoot; }
            }

            public struct Enumerator : IEnumerator<TKey>, System.Collections.IEnumerator
            {
                private DD<TKey, TValue> _dictionary;
                private int _index;
                private int _version;
                private TKey _currentKey;

                internal Enumerator(DD<TKey, TValue> dictionary)
                {
                    _dictionary = dictionary;
                    _version = dictionary._version;
                    _index = 0;
                    _currentKey = default(TKey);
                }

                public void Dispose()
                {
                }

                public bool MoveNext()
                {
                    if (_version != _dictionary._version)
                    {
                        throw new Exception();//ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                    }

                    while ((uint)_index < (uint)_dictionary._count)
                    {
                        ref Entry entry = ref _dictionary._entries[_index++];

                        if (entry.hashCode >= 0)
                        {
                            _currentKey = entry.key;
                            return true;
                        }
                    }

                    _index = _dictionary._count + 1;
                    _currentKey = default(TKey);
                    return false;
                }

                public TKey Current
                {
                    get
                    {
                        return _currentKey;
                    }
                }

                object System.Collections.IEnumerator.Current
                {
                    get
                    {
                        if (_index == 0 || (_index == _dictionary._count + 1))
                        {
                            throw new Exception();//ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                        }

                        return _currentKey;
                    }
                }

                void System.Collections.IEnumerator.Reset()
                {
                    if (_version != _dictionary._version)
                    {
                        throw new Exception();//ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                    }

                    _index = 0;
                    _currentKey = default(TKey);
                }
            }
        }

        [DebuggerDisplay("Count = {Count}")]
        public sealed class ValueCollection : ICollection<TValue>, ICollection, IReadOnlyCollection<TValue>
        {
            private DD<TKey, TValue> _dictionary;

            public ValueCollection(DD<TKey, TValue> dictionary)
            {
                if (dictionary == null)
                {
                    throw new Exception(); //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
                }
                _dictionary = dictionary;
            }

            public Enumerator GetEnumerator()
            {
                return new Enumerator(_dictionary);
            }

            public void CopyTo(TValue[] array, int index)
            {
                if (array == null)
                {
                    throw new Exception(); //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
                }

                if (index < 0 || index > array.Length)
                {
                    throw new Exception(); //ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
                }

                if (array.Length - index < _dictionary.Count)
                {
                    throw new Exception(); //ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
                }

                int count = _dictionary._count;
                Entry[] entries = _dictionary._entries;
                for (int i = 0; i < count; i++)
                {
                    if (entries[i].hashCode >= 0) array[index++] = entries[i].value;
                }
            }

            public int Count
            {
                get { return _dictionary.Count; }
            }

            bool ICollection<TValue>.IsReadOnly
            {
                get { return true; }
            }

            void ICollection<TValue>.Add(TValue item)
            {
                throw new Exception(); //ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
            }

            bool ICollection<TValue>.Remove(TValue item)
            {
                throw new Exception(); //ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
                return false;
            }

            void ICollection<TValue>.Clear()
            {
                throw new Exception(); //ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
            }

            bool ICollection<TValue>.Contains(TValue item)
            {
                return _dictionary.ContainsValue(item);
            }

            IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
            {
                return new Enumerator(_dictionary);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new Enumerator(_dictionary);
            }

            void ICollection.CopyTo(Array array, int index)
            {
                if (array == null)
                {
                    throw new Exception(); //ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
                }

                if (array.Rank != 1)
                {
                    throw new Exception(); //ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
                }

                if (array.GetLowerBound(0) != 0)
                {
                    throw new Exception(); //ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
                }

                if (index < 0 || index > array.Length)
                {
                    throw new Exception(); //ThrowHelper.ThrowIndexArgumentOutOfRange_NeedNonNegNumException();
                }

                if (array.Length - index < _dictionary.Count)
                    throw new Exception(); //ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);

                TValue[] values = array as TValue[];
                if (values != null)
                {
                    CopyTo(values, index);
                }
                else
                {
                    object[] objects = array as object[];
                    if (objects == null)
                    {
                        throw new Exception(); //ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
                    }

                    int count = _dictionary._count;
                    Entry[] entries = _dictionary._entries;
                    try
                    {
                        for (int i = 0; i < count; i++)
                        {
                            if (entries[i].hashCode >= 0) objects[index++] = entries[i].value;
                        }
                    }
                    catch (ArrayTypeMismatchException)
                    {
                        throw new Exception(); //ThrowHelper.ThrowArgumentException_Argument_InvalidArrayType();
                    }
                }
            }

            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            object ICollection.SyncRoot
            {
                get { return ((ICollection)_dictionary).SyncRoot; }
            }

            public struct Enumerator : IEnumerator<TValue>, System.Collections.IEnumerator
            {
                private DD<TKey, TValue> _dictionary;
                private int _index;
                private int _version;
                private TValue _currentValue;

                internal Enumerator(DD<TKey, TValue> dictionary)
                {
                    _dictionary = dictionary;
                    _version = dictionary._version;
                    _index = 0;
                    _currentValue = default(TValue);
                }

                public void Dispose()
                {
                }

                public bool MoveNext()
                {
                    if (_version != _dictionary._version)
                    {
                        throw new Exception(); //ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                    }

                    while ((uint)_index < (uint)_dictionary._count)
                    {
                        ref Entry entry = ref _dictionary._entries[_index++];

                        if (entry.hashCode >= 0)
                        {
                            _currentValue = entry.value;
                            return true;
                        }
                    }
                    _index = _dictionary._count + 1;
                    _currentValue = default(TValue);
                    return false;
                }

                public TValue Current
                {
                    get
                    {
                        return _currentValue;
                    }
                }

                object System.Collections.IEnumerator.Current
                {
                    get
                    {
                        if (_index == 0 || (_index == _dictionary._count + 1))
                        {
                            throw new Exception(); //ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
                        }

                        return _currentValue;
                    }
                }

                void System.Collections.IEnumerator.Reset()
                {
                    if (_version != _dictionary._version)
                    {
                        throw new Exception(); //ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumFailedVersion();
                    }
                    _index = 0;
                    _currentValue = default(TValue);
                }
            }
        }
    }
}
