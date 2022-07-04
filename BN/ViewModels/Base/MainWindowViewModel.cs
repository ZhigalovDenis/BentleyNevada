using BN.Infrostructure.Commands;
using BN.Models;
using EasyModbus;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private string _StatusST6 = "Отключено"; // поле

        /// <summary>Статус программы</summary>
        public string StatusST6
        {
            get => _StatusST6; //возвращает значение поля
            set => Set(ref _StatusST6, value);
        }
        #endregion

        #region IP адрес подключения
        private string _AdressIPST6 = "127.0.0.1";
        /// <summary>IP адрес подключения</summary>
        public string AdressIPST6
        {
            get => _AdressIPST6;
            set => Set(ref _AdressIPST6, value);
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

        #region Фон для StatusBarST6 
        private string _backgroundStatusBarST6 = "Coral";
        /// <summary></summary>
        public string BackgroundStatusBarST6
        {
            get => _backgroundStatusBarST6;
            set => Set(ref _backgroundStatusBarST6, value);
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
        public RelayCommand ConnectToRackST6 { get; set; }
        #endregion

        public MainWindowViewModel()
        {

            ConnectToRackST6 = new RelayCommand(o =>
            {
                ModbusClient modbusClient = new ModbusClient(AdressIPST6, 502);

                var Match = Regex.IsMatch(AdressIPST6, "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
                if (Match == false)
                {
                    MessageBox.Show("IP адрес не соответствуте формату IPV4");
                    return;
                }

                try
                {
                    modbusClient.Connect();
                    StatusST6 = "Подключено";
                    BackgroundStatusBarST6 = "LightGreen";
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
