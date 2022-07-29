using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

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
        public string[] ReadLastFile(string Path)
        {
            string LastPath = GetLastFile(Path);
            using (StreamReader reader = new StreamReader(LastPath))
            {
                return reader.ReadToEnd().Split('\n');
            }
        }

        public List<string> ReadLastFile1(string Path)
        {
            string LastPath = GetLastFile(Path);
            using (StreamReader reader = new StreamReader(LastPath))
            {
                string[] strs = reader.ReadToEnd().Split('\n');
                //string[] strs2 = st;   
                List<string> ppp = new List<string>();
                for (int i = 0; i < strs.Length; i++)
                {
                    ppp.Add(strs[i]);
                }
                return ppp;
            }
        }

        public  List<RJString> ReadLastFile2(string Path)
        {
            string LastPath = GetLastFile(Path);
            var lines = File.ReadAllLines(LastPath);
            var list = new List<RJString>();  
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i].Split('\t');
                    var rjs = new RJString() 
                    {
                        DT = line[0],
                        KKS = line[1],
                        STS = line[2]
                    };   
                
                    list.Add(rjs);          
                }
                return list;
        }



    }
}
