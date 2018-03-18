using System;

namespace Binary_search
{
    class Program
    {
        static void Main(string[] args)
        {
            TestFiveElementArray();
            TestNegativeNumbers();
            TestNonExistentElement();
            TestRepeatingElement();
            TestEmptyArray();
            TestBigArray();

            Console.ReadKey();
        }

        public static int BinarySearch(int[] array, int value)
        {
            int middleIndex, firstIndex, lastIndex;
            firstIndex = 0;
            lastIndex = array.Length;
            int answer = -1;

            while (firstIndex < lastIndex)
            {
                middleIndex = (lastIndex + firstIndex) / 2;

                if (array[middleIndex] == value)
                    answer = middleIndex;

                if (array[middleIndex] < value)
                    firstIndex = middleIndex + 1;

                if (array[middleIndex] >= value)
                    lastIndex = middleIndex;
            }

            return answer;

        }

        private static void TestFiveElementArray()
        {
            //Тестирование поиска среди 5 элементов
            int[] fiveNumbers = new[] { 1, 3, 5, 6, 9 };
            if (BinarySearch(fiveNumbers, 6) != 3)
                Console.WriteLine("! Поиск не нашёл число 6 среди чисел { 1, 3, 5, 6, 9 }");
            else
                Console.WriteLine("Поиск в массиве из 5 элементов работает корректно");
        }
        private static void TestNegativeNumbers()
        {
            //Тестирование поиска в отрицательных числах
            int[] negativeNumbers = new[] { -5, -4, -3, -2 };
            if (BinarySearch(negativeNumbers, -3) != 2)
                Console.WriteLine("! Поиск не нашёл число -3 среди чисел {-5, -4, -3, -2}");
            else
                Console.WriteLine("Поиск среди отрицательных чисел работает корректно");
        }
        private static void TestNonExistentElement()
        {
            //Тестирование поиска отсутствующего элемента
            int[] negativeNumbers = new[] { -5, -4, -3, -2 };
            if (BinarySearch(negativeNumbers, -1) >= 0)
                Console.WriteLine("! Поиск нашёл число -1 среди чисел {-5, -4, -3, -2}");
            else
                Console.WriteLine("Поиск отсутствующего элемента вернул корректный результат");
        }
        private static void TestRepeatingElement()
        {
            //Тестирование поиска с повторяющимися элементами
            int[] justNumbers = new[] { 1, 1, 3, 6, 9 };
            if (BinarySearch(justNumbers, 1) != 0)
                Console.WriteLine("! Поиск не нашёл число 1 среди чисел { 1, 1, 3, 6, 9 }.");
            else
                Console.WriteLine("Поиск повторяющегося элемента работает корректно");

        }
        private static void TestEmptyArray()
        {
            //Тестирование поиска в пустом массиве
            int[] emptyArray = new int[0];
            if (BinarySearch(emptyArray, 1) != -1)
                Console.WriteLine("! Поиск нашёл число в пустом массиве");
            else
                Console.WriteLine("Поиск в пустом массиве работает корректно");
        }
        private static void TestBigArray()
        {
            //Тестирование поиска среди 100001 элемента
            int[] manyNumbers = new int[100001];
            for (int i = 0; i < 100001; i++)
                manyNumbers[i] = i;
            if (BinarySearch(manyNumbers, 1000) != 1000)
                Console.WriteLine("! Поиск в большом массиве работает некорректно");
            else
                Console.WriteLine("Поиск в большом массиве работает корректно");
        }
    }
}