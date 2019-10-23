using System;
using System.Collections.Generic;
using System.Text;

namespace HashMap
{
    public struct KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; internal set; }
        public TValue Value { get; internal set; }

        public KeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    public class HashMap<TKey, TValue>
    {
        public TValue this[TKey key]
        {
            get
            {
                int index = GetHashedIndex(key);
                if (data[index] == null)
                {
                    throw new Exception("This key is not in the hashmap");
                }
                LinkedListNode<KeyValuePair<TKey, TValue>> node = data[index].First;
                while (node != null)
                {
                    if (node.Value.Key.Equals(key))
                    {
                        return node.Value.Value;
                    }
                    node = node.Next;
                }
                throw new Exception("This key is not in the hashmap");
            }
        }

        private LinkedList<KeyValuePair<TKey, TValue>>[] data;
        public int Count { get; private set; }
        public int Capacity { get; private set; }

        public HashMap()
            : this(15) { }
        public HashMap(int capacity)
        {
            data = new LinkedList<KeyValuePair<TKey, TValue>>[capacity];
            Count = 0;
            Capacity = capacity;
        }

        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                throw new Exception("Key is already in this hashmap.");
            }

            int index = GetHashedIndex(key);
            if (data[index] == null)
            {
                data[index] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }
            data[index].AddLast(new KeyValuePair<TKey, TValue>(key, value));
            Count++;

            if (Count == Capacity)
            {
                ReHash(Capacity * 2);
            }
        }
        public bool Remove(KeyValuePair<TKey, TValue> keyValuePair)
        {
            int index = GetHashedIndex(keyValuePair.Key);
            return data[index].Remove(keyValuePair);
        }
        public bool Remove(TKey key, TValue value)
        {
            int index = GetHashedIndex(key);
            LinkedListNode<KeyValuePair<TKey, TValue>> node = data[index].First;
            while (node != null)
            {
                if (node.Value.Key.Equals(key))
                {
                    data[index].Remove(node);
                    Count--;
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        private void ReHash(int newCapacity)
        {
            Capacity = newCapacity;

            LinkedList<KeyValuePair<TKey, TValue>>[] newData = new LinkedList<KeyValuePair<TKey, TValue>>[Capacity];
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == null)
                {
                    continue;
                }
                LinkedListNode<KeyValuePair<TKey, TValue>> node = data[i].First;
                while (node != null)
                {
                    int newIndex = GetHashedIndex(node.Value.Key);
                    if (newData[newIndex] == null)
                    {
                        newData[newIndex] = new LinkedList<KeyValuePair<TKey, TValue>>();
                    }
                    newData[newIndex].AddLast(node.Value);
                    node = node.Next;
                }
            }
            data = newData;
        }

        public bool ContainsKey(TKey key)
        {
            int index = GetHashedIndex(key);
            if (data[index] == null)
            {
                return false;
            }
            LinkedListNode<KeyValuePair<TKey, TValue>> node = data[index].First;
            while (node != null)
            {
                if (node.Value.Key.Equals(key))
                {
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        private int GetHashedIndex(TKey key)
        {
            return key.GetHashCode() % Capacity;
        }
    }
}
