using System;
using Hash_Table;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HashTableTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void ThreeElementsTest()
        {
            var hashTableTest = new HashTable();
            hashTableTest.CreateHashTable(3);
            hashTableTest.PutPair(1, "one");
            hashTableTest.PutPair("two", "rabbits");
            hashTableTest.PutPair("three", 516);

            if (!(hashTableTest.GetValueByKey(1).Equals("one") &&
                hashTableTest.GetValueByKey("two").Equals("rabbits") &&
                hashTableTest.GetValueByKey("three").Equals(516)))
                throw new Exception();
        }

        [TestMethod]
        public void SameKeyTest()
        {
            var ht = new HashTable();
            ht.CreateHashTable(2);
            ht.PutPair(1, "one");
            ht.PutPair("two", "rabbits");
            ht.PutPair(1, 516);

            if (ht.GetValueByKey(1).Equals("one"))
                throw new Exception();

        }

        [TestMethod]
        public void FindOneIn10000Test()
        {
            var ht = new HashTable();
            ht.CreateHashTable(10000);
            for (var i = 0; i < 9999; i++)
            {
                Random rnd = new Random(DateTime.Now.Millisecond);

                var key = rnd.Next(1, 100000);
                var element = rnd.Next(1, 100000);
                ht.PutPair(key, element);
            }

            ht.PutPair(16, 324);

            ht.GetValueByKey(16);
            if (!(ht.GetValueByKey(16).Equals(324)))
                throw new Exception();

        }

        [TestMethod]
        public void Find1000In10000Test()
        {
            var ht = new HashTable();
            ht.CreateHashTable(10000);

            for (var i = 0; i < 9999; i++)
            {
                var key = i;
                var element = i+2;
                ht.PutPair(key, element);
            }

            for (var i = 10000; i < 1999 ; i++)
            {
                if (ht.GetValueByKey(i).Equals(null))
                    continue;
                else throw new Exception();
            }
        }
    }
}
