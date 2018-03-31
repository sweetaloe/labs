using System;
using Quick_sort;

namespace Quick_sort
{
    class Program
    {
        static void Sort(int[] array, int startIndex, int endIndex)
        {

            if (startIndex == endIndex)
                return;

            int suppElement = array[endIndex];
            int i = startIndex;
            int j = endIndex;

            while (i <= j)
            {
                while (array[i] < suppElement) i++;
                while (array[j] > suppElement) j--;

                if (i <= j)
                {
                    int buf = array[i];
                    array[i] = array[j];
                    array[j] = buf;
                    i++;
                    j--;
                }
            }

            if (i < endIndex)
                Sort(array, i, endIndex);
            if (j > startIndex)
                Sort(array, startIndex, j);

        }

        static int[] Sort(int[] array)
        {
            if(array.Length!=0)
            Sort(array, 0, array.Length-1);
            return array;
        }

        static int[] GenerateArray(int size)
        {
            int[] array = new int[size];

            var rand = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < size; i++)
                array[i] = rand.Next(0, 10000);

            return array;
        }
        static void Main(string[] args)
        {
            TestThreeElements();
            TestHundredElements();
            TestThousandElements();
            TestEmptyArray();
            TestManyElements();

            Console.WriteLine("Press any key to exit..");
            Console.ReadKey();
        }

        static void TestThreeElements()
        {
            int[] array = GenerateArray(3);
            array = Sort(array);

            if (array[1] > array[0] && array[2] > array[1]) Console.WriteLine("TestThreeElements...Done");
            else Console.WriteLine("TestThreeElements...Failed");
        }
        static void TestHundredElements()
        {
            int[] array = GenerateArray(100);
            array = Sort(array);
            int check = 1;

            for (int i = 0; i < 99; i++)
                if (array[i] > array[i + 1])
                {
                    check = 0;
                    break;
                }

            if (check == 1) Console.WriteLine("TestHundredElements...Done");
            else Console.WriteLine("TestHundredElements...Failed");
        }

        static void TestThousandElements()
        {
            int[] array = GenerateArray(1000);
            array = Sort(array);
            int check = 1;

            for (int i = 0; i < 999; i++)
                if (array[i] > array[i + 1])
                {
                    check = 0;
                    break;
                }

            if (check == 1) Console.WriteLine("TestThousandElements...Done");
            else Console.WriteLine("TestThousandElements...Failed");

        }
        static void TestEmptyArray()
        {
            int[] array = GenerateArray(0);
            array = Sort(array);

            if (array.Length == 0) Console.WriteLine("TestEmptyElements...Done");
            else Console.WriteLine("TestEmptyElements...Failed");
        }
        static void TestManyElements() 
         {
            int[] array = GenerateArray(150000000);
            array = Sort(array);
            int check = 1;

            for (int i = 0; i < 150000000-1; i++)
                if (array[i] > array[i + 1])
                {
                    check = 0;
                    break;
                }

            if (check == 1) Console.WriteLine("TestManyElements...Done");
            else Console.WriteLine("TestManyElements...Failed");
        }
    }
}