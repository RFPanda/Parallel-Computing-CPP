using System;
using System.Collections.Generic;
using System.Threading;
namespace Lab3
{
    class Program
    {
       
        static int R = 2; 
        static int W = 6; 
        static int n = 20000; 
        static string buffer;
        static Thread[] Writers = new Thread[W];
        static Thread[] Readers = new Thread[R];

        
        static List<string[]> ЛистПисат = new List<string[]>();
        
        static List<List<string>> ЛистЧит = new List<List<string>>();

        static int intFull = 0;
        static int intEmpty = 1; // clear

        static bool bEmpty = true;
        static bool finish = false;
        static void Read()
        {
            List<string> MyMessagesRead = new List<string>();
            while (!finish)

                if (Interlocked.CompareExchange(ref intFull, 0, 1) == 1)
                {
                    MyMessagesRead.Add(buffer);
                    intEmpty = 1;
                }

            ЛистЧит.Add(MyMessagesRead);
        }
        static void Write()
        {
            string[] MyMessagesWri = new string[n];
            for (int j = 0; j < n; j++)
                MyMessagesWri[j] = j.ToString();
        
            int i = 0;
            while (i < n)
                if (Interlocked.CompareExchange(ref intEmpty, 0, 1) == 1)
                {
                    buffer = MyMessagesWri[i++];
                    intFull = 1;
                }
        
            ЛистПисат.Add(MyMessagesWri);
        }
        static void Main()
        {
            int sms = 0;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("|-----------------------------------------|");
            Console.WriteLine("| Made by Khasanov A. | 2022 | rfpanda.ml |");
            Console.WriteLine("|-----------------------------------------|");
            Console.WriteLine(); Console.ResetColor();

            for (int i = 0; i < W; i++)
            {
                Writers[i] = new Thread(Write);
                Writers[i].Name = i.ToString();
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