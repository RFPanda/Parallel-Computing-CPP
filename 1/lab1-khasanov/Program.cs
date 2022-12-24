//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Diagnostics;
//using System.Text;
//using System.Threading;


//namespace Laba1_Khasanov
//{
//    class Program
//    {

//        static double Massiv(int count, int size)
//        {
//            float[] a = new float[size];
//            Random rand = new Random();
//            for (int i = 0; i < a.Length; i++)
//            {
//                a[i] = rand.Next(-200, 100);
//            }

//            int[] arr = new int[300];
//            int l = -200;
//            int[] countArr = new int[300];
//            for (int i = 0; i < 300; i++)
//            {
//                arr[i] = l;
//                l++;
//                countArr[i] = 0;
//            }

//            int k = a.Length / count;
//            int countInc = a.Length % count;
//            DateTime dt1, dt2;
//            dt1 = DateTime.Now;


//            int iM = 0;
//            Thread[] threads = new Thread[count];
//            for (int i = 0; i < count; i++)
//            {

//                int iB, iE;
//                if (i < countInc)
//                {
//                    iB = iM;
//                    iE = iM + k + 1;

//                    iM += k + 1;
//                }
//                else
//                {
//                    iB = iM;
//                    iE = iM + k;

//                    iM += k; // ya 
//                }

//                threads[i] = new Thread((obj) =>
//                {
//                    for (int index = 0; index < 300; index++)
//                    {

//                        for (int j = iB; j < iE; j++)
//                        {
//                            if (arr[index] == a[j])
//                            {
//                                countArr[index]++;
//                            }// y
//                        }
//                    }
//                    int tmp = countArr[0];
//                    int indexOfDig = 0;
//                    for (int index = 0; index < 300; index++)
//                    {
//                        if (tmp < countArr[index])
//                        {
//                            tmp = countArr[index];
//                            indexOfDig = arr[index];
//                        }
//                    }
//                });// mami

//                threads[i].Start();
//            }


//            for (int i = 0; i < count; i++)
//                threads[i].Join();
//            dt2 = DateTime.Now;
//            TimeSpan sw = dt2 - dt1;
//            return sw.TotalMilliseconds;
//        }
//        static void Main(string[] args)
//        {
//            Console.ForegroundColor = ConsoleColor.Green;
//            Console.WriteLine("|-----------------------------------------|");
//            Console.WriteLine("| Made by Khasanov A. | 2022 | rfpanda.ml |");
//            Console.WriteLine("|-----------------------------------------|");
//            Console.WriteLine(); Console.ResetColor();


//            Console.ForegroundColor = ConsoleColor.Blue;
//            Console.WriteLine("Ввод M: ");
//            int M = Convert.ToInt32(Console.ReadLine());
//            Console.WriteLine("Ввод N: ");
//            int N = Convert.ToInt32(Console.ReadLine());
//            Console.WriteLine(); Console.ResetColor();
//            Console.ForegroundColor = ConsoleColor.Red;
//            for (int m = 1; m <= M; m++)
//            {
//                Random rand = new Random();
//                float value = (float)rand.NextDouble();
//                // injener
//                Console.WriteLine("Кол-во потоков: {0}. Время работы: {1} мс", m, Massiv(m, N));
//            }
//            Console.ReadKey();
//        }
//    }
//}
using System;
using System.Threading;

namespace Question2342377
{
    class Program
    {

        private static int[] vector;
        private static int k = 20;
        private static int m = 6;
        private static Thread[] threads;

        static void Main(string[] args)
        {
            InitVector();
            ShowVector();

            Job();

            ShowVector();
            Console.ReadKey();
        }

        private static void InitVector()
        {
            Random random = new Random();
            vector = new int[k];
            for (int i = 0; i < k; i++)
            {
                vector[i] = random.Next(1, 10);
            }
        }

        private static void ShowVector()
        {
            for (int i = 0; i < k; i++)
            {
                Console.Write($"{vector[i],3}");
            }
            Console.WriteLine("\n");
        }

        /// <summary>
        /// Для упрощения все M потоков должны охватить N элементов вектора
        /// </summary>
        private static void Job()
        {
            threads = new Thread[m];

            var range = (int)Math.Ceiling((double)k / m);

            for (int i = 0; i < m; i++)
            {
                int start = i * range,
                    end = start + range - 1;

                if (start > k - 1)
                    break;
                if (end > k - 1)
                    end = k - 1;

                threads[i] = new Thread(() => { MulRange(start, end); });
                threads[i].Start();
                threads[i].Join();
            }
        }

        private static void MulRange(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                vector[i] *= 10;
            }
        }
    }
}