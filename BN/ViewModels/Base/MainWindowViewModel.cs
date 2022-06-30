using BN.Infrostructure.Commands;
using BN.Models;
using EasyModbus;
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

        #region Фон для лэйбла
        private double _backgroundLabel;
        /// <summary></summary>
        public double BackgroundLabel
        {
            get => _backgroundLabel;
            set => Set(ref _backgroundLabel, value);
        }
        #endregion

        #region Три параметра из регистра

        private double _firstParmReg;
        /// <summary></summary>
        public double FirstParmReg
        {
            get => _firstParmReg;
            set => Set(ref _firstParmReg, value);
        }

        private double _secondParmReg;
        /// <summary></summary>
        public double SecondParmReg
        {
            get => _secondParmReg;
            set => Set(ref _secondParmReg, value);
        }

        private double _thirdParmReg;
        /// <summary></summary>
        public double ThirdParmReg
        {
            get => _thirdParmReg;
            set => Set(ref _thirdParmReg, value);
        }
        #endregion


        Sum calculator = new Sum();

        #region PROPERTIES

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
        #endregion

        private DateTime _datVrem;
        /// <summary></summary>
        public DateTime DatVrem
        {
            get => _datVrem;
            set => Set(ref _datVrem, value);
        }

        private int _t;
        /// <summary></summary>
        public int T
        {
            get => _t;
            set => Set(ref _t, value);
        }
        #region Комманды
        public RelayCommand CalculateButton { get; set; }
        public RelayCommand Day { get; set; }
        public RelayCommand ConnectToRackSteamTurbine { get; set; }
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

            //ConnectToRackSteamTurbine = new RelayCommand(o =>
            //{

            //    DispatcherTimer _timer = new DispatcherTimer(DispatcherPriority.Render);
            //    _timer.Interval = TimeSpan.FromSeconds(1);
            //    _timer.Tick += (sender, args) =>
            //    {

            //        BNRack SteamTurbine = new BNRack();
            //        int[] ArrayRegistr = SteamTurbine.ConnectToRack(AdressIP);
            //        FirstParmReg = ArrayRegistr[0];
            //    };
            //    _timer.Start();
  
            //});




            ConnectToRackSteamTurbine = new RelayCommand(o =>
            {
                ModbusClient modbusClient = new ModbusClient(AdressIP, 502);
                try
                {
                    modbusClient.Connect();
                }
                catch (Exception )
                { 
                
                    MessageBox.Show("Устройство не отвечает");
                    return;
                }
                
                          
                    DispatcherTimer _timer = new DispatcherTimer(DispatcherPriority.Render);
                    _timer.Interval = TimeSpan.FromSeconds(1);
                    _timer.Tick += (sender, args) =>
                    {
                        int[] readHoldingRegisters = modbusClient.ReadHoldingRegisters(0, 3);
                        //ushort testvalue = (ushort)readHoldingRegisters[0];
                        BNRack bnrk = new BNRack();
                        double[] retva = bnrk.Scale(ref readHoldingRegisters);
                        FirstParmReg = retva[0];
                        SecondParmReg = retva[1];
                        ThirdParmReg = retva[2];
                    };
                    _timer.Start();
                

                


            });

           // ModbusClient modbusClient = new ModbusClient(AdressIP, 502);
            //modbusClient.Connect();
            //while (true)
            //{
            //    int[] readHoldingRegisters = modbusClient.ReadHoldingRegisters(0, 3);

            _firstParmReg = 12.2664564;
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
