using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2_Khasanov
{
    class Program
    {

        const int N = 1000;

        static List<int> basePrime = new List<int>();

        static int[] Nums = new int[N + 1];
        static int current_index = 0;
        //число потоков
        static int M = 8;

        static void Подпрога()
        {
            ;
            for (int i = 2; i < Math.Sqrt(N); i++)
            {
                if (Nums[i] == 0)
                {
                    for (int j = i + 1; j < Math.Sqrt(N); j++)
                        if (j % i == 0)
                            Nums[j] = 1;
                    basePrime.Add(i);
                }
            }
            Thread[] arrThr = new Thread[M];//массив потоков
            for (int i = 0; i < M; i++)
            {

                arrThr[i] = new Thread(функция);
                arrThr[i].Start();
            }
            for (int i = 0; i < M; i++)
                arrThr[i].Join();
        }

        private static void функция()
        {
            int current_prime;
            int Len = basePrime.Count;
            while (true)
            {
                if (current_index >= Len)
                    break;
                lock ("Конец")
                {
                    current_prime = basePrime[current_index];
                    current_index++;
                }
                // Обработка текущего простого числа
                for (int i = (int)Math.Sqrt(N); i < N + 1; i++)
                    if (i % current_prime == 0)
                        Nums[i] = 1;
            }
        }

        static void Main(string[] args)
        {
            int cntV = 0, cntC = 0;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("|-----------------------------------------|");
            Console.WriteLine("| Made by Khasanov A. | 2022 | rfpanda.ml |");
            Console.WriteLine("|-----------------------------------------|");
            Console.WriteLine(); Console.ResetColor();


            for (int i = 0; i < 10; i++)
            {
                Подпрога();
            }

            for (int i = 0; i < N - 1; i++)
            {
                if (Nums[i] == 0)
                {
                    cntV += i; cntC++;
                }
            }
            Console.WriteLine(cntV.ToString() + " & " + cntC.ToString());
            Console.ReadKey(); Console.ReadKey();
        }
    }

}

