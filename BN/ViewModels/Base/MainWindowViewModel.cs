using BN.Infrostructure.Commands;
using BN.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BN.ViewModels.Base
{
    internal class MainWindowViewModel : ViewModel
    {




        #region Мой тест

        #endregion
        private int _TestSum1;
        /// <summary></summary>
        public int TestSum1
        {
            get => _TestSum1;
            set => Set(ref _TestSum1, value);
        }


        private string _LCB;
        /// <summary></summary>
        public string LCB
        {
            get => _LCB;
            set => Set(ref _LCB, value);
        }


        #region Заголовок окна
        private string _Title = "АСКВД BN";
        /// <summary>Заголовок окна</summary>
        public string Title
        {
            get => _Title;
            //{
            //    //if (Equals(_Title, value)) return;
            //    //_Title = value; 
            //    //OnPropertyChanged();

            //    Set(ref _Title, value); 
            //}
            set => Set(ref _Title, value);
        }
        #endregion

        #region Status : string - Статус программы

        /// <summary>Статус программы</summary>
        private string _Status = "Отключено"; // поле

        /// <summary>Статус программы</summary>
        public string Status
        {
            get => _Status; //возвращает значение поля
            set => Set(ref _Status, value);
        }
        #endregion

        #region Команды

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommanExecute(object p) => true;
        private void OnCloseApplicationCommanExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #endregion
        public MainWindowViewModel()
        {
            #region Команды

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommanExecuted, CanCloseApplicationCommanExecute);

            #endregion




            Sum ss = new Sum();
            // ss.Number1 = 5;
            //ss.Number2 = 9;
            // ss.Calc(ss.Number1 = 6, );
            _TestSum1 = ss.Calc(5, 9);

            //if (_TestSum1 > 10)
            //{

            //    _LCB = "Yellow";
            //}




        }
    }
}
