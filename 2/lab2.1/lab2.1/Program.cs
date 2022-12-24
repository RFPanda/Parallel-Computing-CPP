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

        const int N = 1000;//задаем границу диапазона поиска

        static List<int> basePrime = new List<int>();

        static int[] Nums = new int[N + 1];
        
        static int cnt; //количество элементов на поток
        static int M = 4; //число потоков
       


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

                arrThr[i] = new Thread(Поток);
                arrThr[i].Start(i);
            }
            for (int i = 0; i < M; i++)
                arrThr[i].Join();
        }
        static void Поток(object obj)
        {
            int idx = (int)obj;//получаем номер потока
            int end;
            int Sqrt = (int)Math.Sqrt(N);
            cnt = (N - Sqrt) / M;//получаем кол. элементов,обрабат. потоком
            int start = Sqrt + cnt * idx;
            if (idx == M - 1) end = N + 1;//идем до конца массива, захватывая элементы, которые останутся в хвосте(деление 16/3)
            else end = start + cnt;
            for (int i = start; i < end; i++)
                foreach (var item in basePrime)
                {
                    if (i % item == 0)//если i делится нацело на item, оно составное
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



            //контрольная сумма значений чисел, контрольная сумма чисел
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
