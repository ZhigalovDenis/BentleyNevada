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

        #region Status : string - Статус подключения ПТ-6

        /// <summary>Статус программы</summary>
        private string _StatusST6 = "Отключено"; // поле

        /// <summary>Статус программы</summary>
        public string StatusST6
        {
            get => _StatusST6; //возвращает значение поля
            set => Set(ref _StatusST6, value);
        }
        #endregion

        #region IP адрес подключения ПТ-6
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

        #region Активность ввода IP адреса 
        private bool _tbIPAdrrActST6 = true;
        /// <summary></summary>
        public bool TbIPAdrrActST6
        {
            get => _tbIPAdrrActST6;
            set => Set(ref _tbIPAdrrActST6, value);
        }
        #endregion

        #region Активность кнопки подключения для ПТ-6 
        private bool _btConST6 = true;
        /// <summary></summary>
        public bool BtConST6
        {
            get => _btConST6;
            set => Set(ref _btConST6, value);
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
        public RelayCommand DisconnectToRackST6 { get; set; }
        #endregion

        public MainWindowViewModel()
        {

            ConnectToRackST6 = new RelayCommand(o =>
            {
                ModbusClient modbusClientST6 = new ModbusClient(AdressIPST6, 502);
           
                var Match = Regex.IsMatch(AdressIPST6, "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
                if (Match == false)
                {
                    MessageBox.Show("IP адрес не соответствуте формату IPV4");
                    return;
                }

                try
                {
                    modbusClientST6.Connect();
                }
                catch (Exception )
                { 
                
                    MessageBox.Show("Устройство не отвечает");
                    return;
                }    
                    StatusST6 = "Подключено";
                    BackgroundStatusBarST6 = "LightGreen";
                    BtConST6 = false;
                    TbIPAdrrActST6 = false;    

                    BNRack bnRackST6 = new BNRack();
                    DispatcherTimer _timerST6 = new DispatcherTimer(DispatcherPriority.Render);

                    _timerST6.Interval = TimeSpan.FromSeconds(1);
                    _timerST6.Tick += (sender, args) =>
                    {
                        DisconnectToRackST6 = new RelayCommand(o1 =>
                        {
                            MessageBox.Show("Устройство не отвечает");
                        });

                        int[] readHoldingRegisters = modbusClientST6.ReadHoldingRegisters(0, 3);
                        double[] retva = bnRackST6.Scale(ref readHoldingRegisters);
                        FirstParmReg = retva[0];
                        SecondParmReg = retva[1];
                        ThirdParmReg = retva[2];
                    };
                _timerST6.Start();

            });

            DisconnectToRackST6 = new RelayCommand(o =>
            {


            });




                BackgroundBorder = "#e5e5e5";
        }
    }
}
