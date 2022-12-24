using System;
using System.Collections.Generic;
using System.Threading;
namespace lab3_2
{
    class Program
{
    static DateTime dt1, dt2;
    static int W = 10; 
    static int R = 4; 
    static int n = 9000; 
    static string buffer;
    static Thread[] Readers = new Thread[R];
    static Thread[] Writers = new Thread[W];
    
     static AutoResetEvent evEmpty;
        static AutoResetEvent evFull;
        //список дял проверки массивов писателей
       static List<string[]> Листписателей = new List<string[]>();
        //список для проверки массивов читателей
        static List<List<string>> ЛистЧит = new List<List<string>>();



        static bool bEmpty = true;
    static bool finish = false;
    static void Read(object state)
    {
        var evFull = ((object[])state)[0] as AutoResetEvent;
        var evEmpty = ((object[])state)[1] as AutoResetEvent;
        List<string> MMR = new List<string>();
        while (!finish)
        {
            evFull.WaitOne();
            if (finish)
            {
                evFull.Set();
                break;
            }
                MMR.Add(buffer);
            evEmpty.Set();
        }

            ЛистЧит.Add(MMR);
    }
    static void Write(object state)
    {
        var evFull = ((object[])state)[0] as AutoResetEvent;
        var evEmpty = ((object[])state)[1] as AutoResetEvent;
        string[] MMW = new string[n];
        for (int j = 0; j < n; j++)
                MMW[j] = j.ToString();
        int i = 0;
        while (i < n)
        {
            evEmpty.WaitOne();
            buffer = MMW[i++];
            evFull.Set();
        }

            Листписателей.Add(MMW);
    }
    static void Main()
    {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("|-----------------------------------------|");
            Console.WriteLine("| Made by Khasanov A. | 2022 | rfpanda.ml |");
            Console.WriteLine("|-----------------------------------------|");
            Console.WriteLine(); Console.ResetColor();


            evFull = new AutoResetEvent(false);
        evEmpty = new AutoResetEvent(true);

        for (int i = 0; i < W; i++)
        {
            Writers[i] = new Thread(Write);
            Writers[i].Start(new object[] { evFull, evEmpty });
        }
        for (int i = 0; i < R; i++)
        {
            Readers[i] = new Thread(Read);
            Readers[i].Start(new object[] { evFull, evEmpty });
        }
        for (int i = 0; i < W; i++)
            Writers[i].Join();
        finish = true;
        evFull.Set();
        for (int i = 0; i < R; i++)
            Readers[i].Join();
           
            int sms = 0;
            for (int i = 0; i < Листписателей.Count; i++)
            {
                sms += Листписателей[i].GetLength(0);
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
