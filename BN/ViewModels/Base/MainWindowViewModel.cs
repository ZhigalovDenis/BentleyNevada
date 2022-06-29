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
using System.Windows.Threading;

namespace BN.ViewModels.Base
{
    internal class MainWindowViewModel : ViewModel
    {
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

        #region IP адрес подключения
        private string _AdressIP = "127.0.0.1";
        /// <summary>IP адрес подключения</summary>
        public string AdressIP
        {
            get => _AdressIP;
            set => Set(ref _AdressIP, value);
        }
        #endregion


        Sum calculator = new Sum();

        #region PROPERTIES

       // private string _firstNumber;
      //  private string _secondNumber;
      //  private string _result;


        private string _firstNumber;
        /// <summary></summary>
        public string FirstNumber
        {
            get => _firstNumber;
            set => Set(ref _firstNumber, value);
        }

        private string _secondNumber;
        /// <summary></summary>
        public string SecondNumber
        {
            get => _secondNumber;
            set => Set(ref _secondNumber, value);
        }

        private string _result;
        /// <summary></summary>
        public string Result
        {
            get => _result;
            set => Set(ref _result, value);
        }


        private DateTime _datVrem;
        /// <summary></summary>
        public DateTime DatVrem
        {
            get => _datVrem;
            set => Set(ref _datVrem, value);
        }

        public RelayCommand CalculateButton { get; set; }

        public RelayCommand Day { get; set; }
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


        #region Комманда подключить 
        public ICommand ConnectToRack { get; }

        private bool CanConnetcToRack(object p) => true;
        private void OnConnetcToRack(object p)
        {
            //BNRack SteamTurbine = new BNRack();
            //int[] mass = SteamTurbine.ConToRack(AdressIP);
            //_TestSum1 = mass[0];
            // Application.

        }
        #endregion
        public MainWindowViewModel()
        {

            CalculateButton = new RelayCommand(o => {
                Result = calculator.Calculate(FirstNumber, SecondNumber).ToString();
            });

            Day = new RelayCommand(o =>
            {

                DispatcherTimer _timer = new DispatcherTimer(DispatcherPriority.Render);
                _timer.Interval = TimeSpan.FromSeconds(1);
                _timer.Tick += (sender, args) =>
                {
                    Clock cl = new Clock();
                    DatVrem = cl.DT();
                };
                _timer.Start();
            });




            /* BNRack SteamTurbine = new BNRack();
             int[] mass = SteamTurbine.ConToRack(AdressIP);
             TestSum1 = mass[0];*/



            // _TestSum1 = mass

            //  Sum ss = new Sum();
            // ss.Number1 = 5;
            //ss.Number2 = 9;
            // ss.Calc(ss.Number1 = 6, );
            //TestSum1 = ss.Calc(5, 9);

            //if (_TestSum1 > 10)
            //{

            //    _LCB = "Yellow";
            //}



        }
    }
}
