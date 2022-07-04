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

        #region Фон для Border
        private string _backgroundBorder;
        /// <summary></summary>
        public string BackgroundBorder
        {
            get => _backgroundBorder;
            set => Set(ref _backgroundBorder, value);
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


        #region Комманды
        public RelayCommand ConnectToRackSteamTurbine { get; set; }
        #endregion

        public MainWindowViewModel()
        {

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

            BackgroundBorder = "#e5e5e5";
        }
    }
}
