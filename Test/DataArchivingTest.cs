using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class DataArchivingTest
    {
        public bool CheckFreeSpaceOnDisk()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            string CurDir = Directory.GetCurrentDirectory();
            string DiskName = CurDir.Substring(0, 3);
            int AllowedFreeSpace = 1000;
            bool IsCkecked = false;

            foreach (var item in allDrives)
            {
                if (item.IsReady == true) //Проверяем готов ли диск
                {
                    if (item.Name == DiskName) // Проверяем кол-во свободного места на диске 
                    {
                        long FreeSpaceInMB = (item.TotalFreeSpace / 1024) / 1024;
                        if (FreeSpaceInMB > AllowedFreeSpace)
                        {
                            Console.WriteLine("Диск " + item.Name + " Свободно " + FreeSpaceInMB + " Мб");
                            Console.WriteLine("Все хорошо места достаточно");
                            IsCkecked = true;
                        }
                        else
                        {
                            Console.WriteLine("Не достаточно места на диске");
                            IsCkecked = false;
                        }
                    }
                }
            }
            return IsCkecked;
        }
        public bool CreateDirectory()
        {
            string CurDir = Directory.GetCurrentDirectory();
            string NewDir = CurDir + "\\Archive";
            bool IsCreated = false;

            try
            {
                // Проверяем существет ли директория
                if (Directory.Exists(NewDir))
                {
                    Console.WriteLine("Папка уже существует создовать не надо");
                    IsCreated = true;
                }
                else
                {
                    // Пытаемся создать директорию
                    Directory.CreateDirectory(NewDir);
                    IsCreated = true;
                    Console.WriteLine("папка успешно создана");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Косяк");
                IsCreated = false;
            }
            return IsCreated;
        }
    }
}
