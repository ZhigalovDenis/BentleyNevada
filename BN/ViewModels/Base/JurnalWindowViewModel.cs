using BN.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;

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

        #region Журнал для фильтра
        private ICollectionView _jurnalFilter_st6;
        public ICollectionView JurnalFilter_st6
        {
            get => _jurnalFilter_st6;
            set => Set(ref _jurnalFilter_st6, value);        
        }
        #endregion

        #region KKS фильтр
        private string _kksFilter_st6;
        public string KKSFilter_st6
        {
            get => _kksFilter_st6;
            set
            {
                Set(ref _kksFilter_st6, value);
                JurnalFilter_st6.Refresh();
            }
        }
        #endregion

        public JurnalWindowViewModel()
        {
            string Path = Directory.GetCurrentDirectory() + "\\Archive\\Jurnal\\";
            ReadJurnal readJurnal = new ReadJurnal();
            Jurnal_st6 = readJurnal.ReadLastFile(Path);
            JurnalFilter_st6 = CollectionViewSource.GetDefaultView(Jurnal_st6);
            JurnalFilter_st6.Filter = FilterKKS;
        }

        public bool FilterKKS(object o)
        {
            if (!string.IsNullOrEmpty(KKSFilter_st6))
            {
                var detail = o as RJString;
                return detail != null && detail.KKS.ToLower().Contains(KKSFilter_st6);
            }
            return true;
        }
    }
}



