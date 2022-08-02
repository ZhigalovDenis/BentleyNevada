
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace BN.Models
{
    internal class ReadJurnal
    {
        /// <summary>
        /// Получение последнего созданного файла
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        private string GetLastFile(string Path)
        {
            DirectoryInfo directory = new DirectoryInfo(Path);
            FileInfo LastFile = directory.GetFiles()
                         .OrderByDescending(f => f.LastWriteTime)
                        .First();
            string PathLastFile = Path + LastFile;
            return PathLastFile;
        }
        /// <summary>
        /// Чтение последнего созданного файла
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public  List<RJString> ReadLastFile(string Path)
        {
            string LastPath = GetLastFile(Path);
            string[] file = File.ReadAllLines(LastPath);
            List<RJString> fileaslist = new List<RJString>();  
                for (int i = 1; i < file.Length; i++)
                {
                    string[] lineoffile = file[i].Split(';');
                    var rjstring = new RJString() 
                    {
                        DT = lineoffile[0],
                        KKS = lineoffile[1],
                        STS = lineoffile[2]
                    };

                fileaslist.Add(rjstring);          
                }
                return fileaslist;
        }

    }
}
