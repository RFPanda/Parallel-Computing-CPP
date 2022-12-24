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
        
        const int N = 100;//задаем границу диапазона поиска

        static List<int> basePrime = new List<int>();
    
        static int[] Nums = new int[N + 1];
    
    
        static void Подпрога()
        {;
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
            for (int i = (int)(Math.Sqrt(N)); i < N + 1; i++)
                foreach (var item in basePrime)
                {
                    if (i % item == 0)
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
