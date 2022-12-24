using System;
using System.Collections.Generic;
using System.Threading;
namespace Lab3
{
    class Program
    {
  
        static int R = 5;
        static int W = 5; 
        static int n = 1000;
        static string buffer;
        static Thread[] Writers = new Thread[W];
        static Thread[] Readers = new Thread[R];
      
        static SemaphoreSlim ssEmpty;

        static List<string[]> ЛистПисат = new List<string[]>();
   
        static List<List<string>> ЛистЧит = new List<List<string>>();

        static bool bEmpty = true;
        static bool finish = false;
        static void Read(object o)
        {
            var ssRead = o as SemaphoreSlim;
            List<string> MyMessagesRead = new List<string>();
            while (!finish)
                if (!bEmpty)
                {
                    ssRead.Wait();
                    if (!bEmpty)
                    {
                        bEmpty = true;
                        MyMessagesRead.Add(buffer);
                    }
                    ssRead.Release();
                }

            ЛистЧит.Add(MyMessagesRead);
        }
        static void Write(object o)
        {
            var ssWrit = o as SemaphoreSlim;
            string[] MyMessagesWri = new string[n];
            for (int j = 0; j < n; j++)
                MyMessagesWri[j] = j.ToString();
            int i = 0;
            while (i < n)
                if (bEmpty)
                {
                    ssWrit.Wait();
                    if (bEmpty)
                    {
                        buffer = MyMessagesWri[i++];
                        bEmpty = false;
                    }
                    ssWrit.Release();
                }
           
            ЛистПисат.Add(MyMessagesWri);
        }
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("|-----------------------------------------|");
            Console.WriteLine("| Made by Khasanov A. | 2022 | rfpanda.ml |");
            Console.WriteLine("|-----------------------------------------|");
            Console.WriteLine(); Console.ResetColor();

            ssEmpty = new SemaphoreSlim(1);
            for (int i = 0; i < W; i++)
            {
                Writers[i] = new Thread(Write);
                Writers[i].Start(ssEmpty);
            }
            for (int i = 0; i < R; i++)
            {
                Readers[i] = new Thread(Read);
                Readers[i].Start(ssEmpty);
            }
            for (int i = 0; i < W; i++)
                Writers[i].Join();
            finish = true;
            for (int i = 0; i < R; i++)
                Readers[i].Join();
          
                 int sms = 0;
                 for (int i = 0; i < ЛистПисат.Count; i++)
                 {
                sms += ЛистПисат[i].GetLength(0);
                 }
                 Console.WriteLine("Всего сообщений отправлено:{0}", sms);
            sms = 0;
                 for (int i = 0; i < ЛистЧит.Count; i++)
                 {
                     if (ЛистЧит[i] != null)
                    sms += ЛистЧит[i].Count;

                 }
                 Console.WriteLine("Получено сообщений: {0}", sms);
            Console.ReadKey();
        }
    }
}