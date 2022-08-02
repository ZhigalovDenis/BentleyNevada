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
        public string DirToCreate { get; set; }



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
            string Dir = DirToCreate;
            string NewDir = CurDir + Dir;
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
            long SizeOfFile = (info.Length/1024)/1024; //Размер в МБ 
            if(SizeOfFile < 1) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Архивирование измеряемых параметров
        /// </summary>
        /// <param name="prm_arch"></param>
        /// <param name="Path"></param>
        public void ArchivingData(double[] ArrOfPrm, string Path)
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
                        sw.WriteLine("Дата/Время;10MAD10CY011;10MAD10CY012;10MAD20CY011;10MAD20CY012;10MAD30CY011;10MAD30CY012;10MAD40CY011;10MAD40CY012;" +
                                     "10MAD50CY011;10MAD50CY012;10MAD60CY011;10MAD60CY012;10MKA10CY011;10MKA10CY012;10MKA20CY011;10MKA20CY012;10MAD10CG010;" +
                                     "10MAD10CG011;10MAD10CG012;10MAD10CY020;10MAD10CY030;10MAD20CY030;10MAD10CY040;10MAD20CY040;10MKA10CY030;10MKA20CY030;" +
                                     "10MAD20CY020;10MKA20CY020;10MKA10CY020;10MKA10CY040;10MAK10CY020;10MAK10CY040;10MKA20CY040;10MAK10CY030;10MAD20CG010");
                    }
                    else // Если файл не пустой заполнять массивом
                    {
                        sw.Write(DateTime.Now);
                        foreach (double item in ArrOfPrm)
                        {
                           sw.Write(";" + item);
                        }
                        sw.WriteLine();
                    }
                }
            }
        }              

        public void AchivingJurnal(string[] ArrOfStatus, string[] NewArrOfStatus, string[] KKS, string Path)
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

                if(FirstLine == null)
                {
                    using (var sw = new StreamWriter(Path, true))  
                    {
                        sw.WriteLine("Дата/Время;Параметр;Статус");
                    }
                }

                for (int i = 0; i < NewArrOfStatus.Length; i++)
                {
                    if (ArrOfStatus[i] != NewArrOfStatus[i])
                    {
                        using (var sw = new StreamWriter(Path, true))
                        {
                            switch (ArrOfStatus[i])
                            {
                                case "#e5e5e5":
                                    sw.WriteLine(DateTime.Now + ";" + KKS[i] + ";" + "Норма");
                                    break;
                                case "Yellow":
                                    sw.WriteLine(DateTime.Now + ";" + KKS[i] + ";" + "Сработала предупредительная граница");
                                    break;
                                case "#FFFF4D39":
                                    sw.WriteLine(DateTime.Now + ";" + KKS[i] + ";" + "Сработала аварийная граница");
                                    break;
                                case "Blue":
                                    sw.WriteLine(DateTime.Now + ";" + KKS[i] + ";" + "Неисправность");
                                    break;
                            }
                        }
                    }
                }
               
            }
        }


    }
}
