using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BN.Models
{
    internal class DataArchiving
    {
        private readonly DriveInfo[] allDrives = DriveInfo.GetDrives();
        private readonly string CurDir = Directory.GetCurrentDirectory();
        private const int AllowedFreeSpace = 1000; // Размер в Мб

        /// <summary>
        /// Проверяет кол-во свободного места на диске. 
        /// </summary>
        /// <returns></returns>
        private bool CheckFreeSpaceOnDisk()
        {
            string DiskName = CurDir.Substring(0, 3);
            bool IsCkecked = false;
            foreach (var disk in allDrives)
            {
                if (disk.IsReady == true) //Проверяем готов ли диск
                {
                    if (disk.Name == DiskName) // Проверяем кол-во свободного места на диске 
                    {
                        long FreeSpaceInMB = (disk.TotalFreeSpace / 1024) / 1024;
                        if (FreeSpaceInMB > AllowedFreeSpace)
                        {
                           //Все хорошо места достаточно
                            IsCkecked = true;
                        }
                        else
                        {
                            MessageBox.Show("Архивация не возможна, на диске меньше 1 Гб свободно пространстав");
                            IsCkecked = false;
                        }
                    }
                }
            }
            return IsCkecked;
        }
        /// <summary>
        /// Создает дирректорию для хранения файлов
        /// </summary>
        /// <returns></returns>
        public bool CreateDirectory()
        {
            //string CurDir = Directory.GetCurrentDirectory();
            string NewDir = CurDir + "\\Archive";
            bool IsCreated;
            if(CheckFreeSpaceOnDisk() == true)
            {
                try
                {
                    // Проверяем существет ли директория
                    if (Directory.Exists(NewDir))
                    {
                        //Папка уже существует создовать не надо
                        IsCreated = true;
                    }
                    else
                    {
                        // Пытаемся создать директорию
                        Directory.CreateDirectory(NewDir);
                        IsCreated = true;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Не возможно создать дирректорию");
                    IsCreated = false;
                }
                return IsCreated;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Проверка размера архивного файла
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public bool CheckSizeOfFile(string Path)
        {
            using (var sw = new StreamWriter(Path, true))  // Создаем пустой если не был создан. Иначе не чего будет читать. 
            {
            }

            FileInfo info = new FileInfo(Path);
            long SizeOfFile = info.Length;
            if(SizeOfFile < 500)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Архивирование
        /// </summary>
        /// <param name="prm_arch"></param>
        /// <param name="Path"></param>
        public void Archiving(double[] prm_arch, string Path)
        {
            if (CreateDirectory() == true)
            {
                string FirstLine;

                using (var sw = new StreamWriter(Path, true))  // Создаем пустой если не был создан. Иначе не чего будет читать. 
                {
                }

                using (var reader = new StreamReader(Path))
                {
                    FirstLine = reader.ReadLine();
                }

                using (var sw = new StreamWriter(Path, true))
                {
                    if (FirstLine == null) // Если файл пустой то записать заголовок
                    {
                        sw.WriteLine("Дата/Время;Параметр1;Параметр2;Параметр3;Параметр4");
                    }
                    else // Если файл не пустой заполнять массивом
                    {
                        int[] array = new int[4];
                        Random rand = new Random();
                        sw.Write(DateTime.Now);
                        for (int y = 0; y < 4; y++)
                        {
                            array[y] = rand.Next(1, 21);
                            sw.Write(";" + array[y]);
                        }
                        sw.WriteLine();
                    }
                }
            }
        }              
    }
}
