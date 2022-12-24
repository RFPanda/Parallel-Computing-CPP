using System;
using System.Collections.Generic;
using System.Threading;
namespace Lab3
{
    class Program
    {
        static DateTime dt1, dt2;
        static int R = 10;
        static int W = 10;
        static int n = 10000; 
        static string buffer;
        static Thread[] Writers = new Thread[W];
        static Thread[] Readers = new Thread[R];

        static List<List<string>> Выводчитателей = new List<List<string>>();
        static List<string[]> Листридеров = new List<string[]>();
     
        

        static bool bEmpty = true;
        static bool finish = false;
        static void Read()
        {
            List<string> MyMessagesRead = new List<string>();//локальный массив читателя
            while (!finish)
                if (!bEmpty)
                {
                    lock ("read")
                    {
                        if (!bEmpty)
                        {
                            bEmpty = true;
                            MyMessagesRead.Add(buffer);
                        }
                    }
                }
            //заносим в статический список, чтобы проверить содержимое
            Выводчитателей.Add(MyMessagesRead);
        }
        static void Write()
        {
            string[] MyMessagesWri = new string[n];//локальный массив писателя
            for (int j = 0; j < n; j++)
                MyMessagesWri[j] = j.ToString();
            int i = 0;
            while (i < n)
                lock ("write")
                {
                    if (bEmpty)
                    {
                        buffer = MyMessagesWri[i++];
                        bEmpty = false;
                    }
                }
                Листридеров.Add(MyMessagesWri);
        }
        static void Main()
        {

            for (int i = 0; i < W; i++)
            {
                Writers[i] = new Thread(Write);
                Writers[i].Start();
            }
            for (int i = 0; i < R; i++)
            {
                Readers[i] = new Thread(Read);
                Readers[i].Start();
            }
            for (int i = 0; i < W; i++)
                Writers[i].Join();
            finish = true;
            for (int i = 0; i < R; i++)
                Readers[i].Join();
       
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("|-----------------------------------------|");
            Console.WriteLine("| Made by Khasanov A. | 2022 | rfpanda.ml |");
            Console.WriteLine("|-----------------------------------------|");
            Console.WriteLine(); Console.ResetColor();

            int cnt = 0;
                   for (int i = 0; i < Листридеров.Count; i++)
                   {
                           cnt += Листридеров[i].GetLength(0);
                   }
                   Console.WriteLine("Всего сообщений отправлено:{0}", cnt);
                   cnt = 0;
                   for (int i = 0; i < Выводчитателей.Count; i++)
                   {
                       if (Выводчитателей[i] != null)
                           cnt+= Выводчитателей[i].Count;

                   }
                   Console.WriteLine("Получено сообщений: {0}",cnt);
            Console.ReadKey();
        }
    }
}