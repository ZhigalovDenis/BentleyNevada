using BN.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace BN.ViewModels.Base
{
    internal class JurnalWindowViewModel : ViewModel
    {

        #region Журнал 
        private ICollectionView _jurnal_st6;
        public ICollectionView Jurnal_st6
        {
            get => _jurnal_st6;
            set => Set(ref _jurnal_st6, value);        
        }
        #endregion

        #region Строка фильтра по KKS 
        private string _kksFilter_st6;
        public string KKSFilter_st6
        {
            get => _kksFilter_st6;
            set
            {
                Set(ref _kksFilter_st6, value);
                Jurnal_st6.Refresh();
            }
        }
        #endregion

        public JurnalWindowViewModel()
        {
            string Path = Directory.GetCurrentDirectory() + "\\Archive\\Jurnal\\10CJJ20_CJJ\\";
            ReadJurnal readJurnal = new ReadJurnal();
            Jurnal_st6 = readJurnal.GetJurnal(Path);
            Jurnal_st6.Filter = FilterKKS;
        }

        private bool FilterKKS(object o)
        {
            if (!string.IsNullOrEmpty(KKSFilter_st6))
            {
                var detail = o as ColumnsToReadJurnal;
                return detail != null && detail.KKS.Contains(KKSFilter_st6);
            }
            return true;
        }
    }
}



