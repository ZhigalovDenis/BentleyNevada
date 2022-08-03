
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Data;

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
        private  List<ColumnsToReadJurnal> ReadLastFile(string Path)
        {
            string LastPath = GetLastFile(Path);
            string[] file = File.ReadAllLines(LastPath);
            List<ColumnsToReadJurnal> fileaslist = new List<ColumnsToReadJurnal>();  
                for (int i = 1; i < file.Length; i++)
                {
                    string[] lineoffile = file[i].Split(';');
                    var rjstring = new ColumnsToReadJurnal() 
                    {
                        DT = lineoffile[0],
                        KKS = lineoffile[1],
                        STS = lineoffile[2]
                    };

                fileaslist.Add(rjstring);          
                }
                return fileaslist;
        }

        public ICollectionView GetJurnal(string Path)
        {
            List<ColumnsToReadJurnal> Jurnal = ReadLastFile(Path);
            ICollectionView JurnalToFilter = CollectionViewSource.GetDefaultView(Jurnal);
            return JurnalToFilter;
        }

    }
}
