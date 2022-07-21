using EasyModbus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {

        static void Main(string[] args)
        {

            string CurDir = Directory.GetCurrentDirectory();
            string NewDir = CurDir + "\\Archive";
            DateTime dt0 = DateTime.Now;
            string dt1 = Convert.ToString(dt0);
            string dt2 = dt1.Replace(':','_');
            string Path = NewDir + "\\" + dt2 + ".txt";

            using (var sw = new StreamWriter(Path))
            {
                sw.WriteLine("Дата/Время;Параметр1;Параметр2;Параметр3;Параметр4");
                int[] array = new int[4];
                Random rand = new Random();

                //FileInfo info = new FileInfo(Path);
                //long SizeOfFile = info.Length;

                while (true)
                {
                    FileInfo info = new FileInfo(Path);
                    long SizeOfFile = info.Length;
                    sw.Write(DateTime.Now);
                    for (int y = 0; y < 4; y++)
                    {
                        array[y] = rand.Next(1, 21);
                        sw.Write(";" + array[y]);
                    }
                    sw.WriteLine();
                    Thread.Sleep(1000);
                    sw.Flush();
                }  
            }
            //DataArchiving test  = new DataArchiving();
            //bool test1 = test.CheckFreeSpaceOnDisk();
            //if (test1)
            //{
            //    bool test2 = test.CreateDirectory();
            //    if (test2)
            //    {

            //    }
            //}
            //test.CreateDirectory();
        }
    }
}
