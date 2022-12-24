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

        const int N = 10000;

        static List<int> basePrime = new List<int>();

        static int[] Nums = new int[N + 1];

        static int cnt; 
        static int M = 4;



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

                arrThr[i] = new Thread(метод);
                arrThr[i].Start(i);
            }
            for (int i = 0; i < M; i++)
                arrThr[i].Join();

        }

        private static void метод(object obj)
        {
            int idx = (int)obj;
            int end;
            int Sqrt = (int)Math.Sqrt(N);
            int Len = basePrime.Count;
            cnt = Len / M;
            int start = cnt * idx;
            if (idx == M - 1) end = Len;
            else end = start + cnt;
            for (int i = Sqrt; i < N + 1; i++)
                for (int j = start; j < end; j++)
                {
                    if (i % basePrime[j] == 0)
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
