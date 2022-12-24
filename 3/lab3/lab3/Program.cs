using System;
using System.Collections.Generic;
using System.Threading;
namespace Lab3
{
    class Program
    {
        static DateTime dt1, dt2;
        static int R = 2; 
        static int Writee = 2; 
        static int n = 100000; 
        static string buffer;
        static Thread[] Writers = new Thread[Writee];
        static Thread[] Readers = new Thread[R];

        static List<string[]> ResultWri = new List<string[]>();
        static List<List<string>> ResultRea = new List<List<string>>();

        static bool bEmpty = true;
        static bool finish = false;
        static void Read()
        {
            List<string> Ридер = new List<string>();
            while (!finish)
                if (!bEmpty)
                {
                    Ридер.Add(buffer);
                    bEmpty = true;
                }
                   ResultRea.Add(Ридер);
        }
        static void Write()
        {
            string[] Райтер = new string[n];
            for (int j = 0; j < n; j++)
            {
                Райтер[j] = j.ToString();
       
            }
            int i = 0;
            while (i < n)
                if (bEmpty)
                {
                    buffer = Райтер[i++];
                    bEmpty = false;
                }
                 ResultWri.Add(Райтер);
        }
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("|-----------------------------------------|");
            Console.WriteLine("| Made by Khasanov A. | 2022 | rfpanda.ml |");
            Console.WriteLine("|-----------------------------------------|");
            Console.WriteLine(); Console.ResetColor();

            for (int i = 0; i < Writee; i++)
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
            for (int i = 0; i < Writee; i++)
                Writers[i].Join();
            finish = true;
            for (int i = 0; i < R; i++)
                Readers[i].Join();

                     
                      int sms = 0;
                      for (int i = 0; i < ResultWri.Count; i++)
                      {
                              sms += ResultWri[i].GetLength(0);
                      }
                      Console.WriteLine("Всего сообщений отправлено: {0}", sms);
                        sms = 0;
                      for (int i = 0; i < ResultRea.Count; i++)
                      {
                          if (ResultRea[i] != null)
                         sms += ResultRea[i].Count;

                      }
                      Console.WriteLine("Получено сообщений: {0}", sms);
            Console.ReadKey();
            Console.WriteLine("| End | bye-bye |");
        }
    }
}