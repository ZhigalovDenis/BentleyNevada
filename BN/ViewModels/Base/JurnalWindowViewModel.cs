using BN.Models;
using System.Collections.Generic;
using System.IO;

namespace BN.ViewModels.Base
{
    internal class JurnalWindowViewModel : ViewModel
    {
        #region Обработанный журнал
        private List<RJString> _jurnal_st6;
        public List<RJString> Jurnal_st6
        {
            get => _jurnal_st6;
            set => Set(ref _jurnal_st6, value);
        }
        #endregion

        public JurnalWindowViewModel()
        {
            string Path = Directory.GetCurrentDirectory() + "\\Archive\\Jurnal\\";
            ReadJurnal readJurnal = new ReadJurnal();
            Jurnal_st6 = readJurnal.ReadLastFile(Path);
        }

    }
}



