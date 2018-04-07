using System;
using System.Collections.Generic;

namespace Hash_Table
{
    public class HashTable
    {
        public List<List<Data>> hashTable;
        public List<int> hashCodeList;

        public class Data
        {
            public object Key { get; set; }
            public object Value { get; set;}
        }
        /// <summary>
        /// Конструктор контейнера
        /// summary>
        /// size">Размер хэ-таблицы
        public void CreateHashTable(int size)
        {
            hashCodeList = new List<int>(size);

            hashTable = new List<List<Data>>(size);
            for (int i = 0; i < size; i++)
                hashTable.Add(new List<Data>());
        }

        public int FindIndex(int hashCode)
        {
            return hashCodeList.IndexOf(hashCode);
        }

        /// 
        /// Метод складывающий пару ключь-значение в таблицу
        /// 
        /// key">
        /// value">
        public void PutPair(object key, object value)
        {
            int hashCode = key.GetHashCode();
            var newItem = new Data { Key = key, Value = value };
            int index = FindIndex(hashCode);
            if (index == -1 && hashTable.Count > hashCodeList.Count)
            {
                hashCodeList.Add(hashCode);
                index = FindIndex(hashCode);
                hashTable[index].Add(newItem);
            }
            else
            {
                foreach (var element in hashTable[index])
                {
                    if (element.Key.Equals(key))
                        element.Value = value;
                }
            }
   
        }
        /// <summary>
        /// Поиск значения по ключу
        /// summary>
        /// key">Ключь
        /// <returns>Значение, null если ключ отсутствуетreturns>
        public object GetValueByKey(object key)
        {
            int index = FindIndex(key.GetHashCode());

            if (index != -1)
                foreach (var element in hashTable[index])
                {
                    if (element.Key.Equals(key))
                        return element.Value;
                }

            return null;
        }

        public static void Main()
        {
        }
    }
}
