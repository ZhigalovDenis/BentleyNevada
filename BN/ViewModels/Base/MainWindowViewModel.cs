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
        private const ushort GatewayFullScaleValue_ST6 = 16383;
        private const sbyte UpperMonitorRange_ST6_gr0 = 1;
        private const sbyte LowerMonitorRange_ST6_gr0 = -1;
        private const double FaultReplace_ST6_gr0 = 99.999;

        private const double WH_Y_0_ST6_gr0 = 0.5;
        private const double WH_Y_1_ST6_gr0 = 0.79;
        private const double WL_Y_0_ST6_gr0 = -0.5;
        private const double WL_Y_1_ST6_gr0 = -0.79;
        private const double AH_R_0_ST6_gr0 = 0.8;
        private const double AH_R_1_ST6_gr0 = 0.99;
        private const double AL_R_0_ST6_gr0 = -0.8;
        private const double AL_R_1_ST6_gr0 = -0.99;
        private const double Fault_0_ST6_gr0 = 1;
        private const double Fault_1_ST6_gr0 = -1;


        #region Заголовок окна
        private string _wnd_Title = "АСКВД BN";
        public string wnd_Title
        {
            get => _wnd_Title;
            //{
            //    //if (Equals(_Title, value)) return;
            //    //_Title = value; 
            //    //OnPropertyChanged();

            //    Set(ref _Title, value); 
            //}
            set => Set(ref _wnd_Title, value);
        }
        #endregion

        #region Статус подключения ПТ-6
        private string _tbl_Status_ST6 = "Отключено"; // поле
        public string tbl_Status_ST6
        {
            get => _tbl_Status_ST6; //возвращает значение поля
            set => Set(ref _tbl_Status_ST6, value);
        }
        #endregion

        #region IP адрес подключения ПТ-6
        private string _tb_AdressIP_ST6 = "127.0.0.1";
        public string tb_AdressIP_ST6
        {
            get => _tb_AdressIP_ST6;
            set => Set(ref _tb_AdressIP_ST6, value);
        }
        #endregion

        #region Фон для параметров ПТ-6 надо делать
        private string _bckgrd_10MAD10CG010;
        public string bckgrd_10MAD10CG010
        {
            get => _bckgrd_10MAD10CG010;
            set => Set(ref _bckgrd_10MAD10CG010, value);
        }
        #endregion

        #region Фон для StatusBarST6 
        private string _sb_Bckgrnd_ST6 = "Coral";
        public string sb_Bckgrnd_ST6
        {
            get => _sb_Bckgrnd_ST6;
            set => Set(ref _sb_Bckgrnd_ST6, value);
        }
        #endregion

        #region Активность ввода адреса для ПТ-6 
        private bool _tb_IPAdrrAct_ST6 = true;
        public bool tb_IPAdrrAct_ST6
        {
            get => _tb_IPAdrrAct_ST6;
            set => Set(ref _tb_IPAdrrAct_ST6, value);
        }
        #endregion

        #region Активность кнопки подключить для ПТ-6 
        private bool _bt_ConAct_ST6 = true;
        public bool bt_ConAct_ST6
        {
            get => _bt_ConAct_ST6;
            set => Set(ref _bt_ConAct_ST6, value);
        }
        #endregion

        #region Кнопка отключить для ПТ-6
        private bool _bt_Discon_ST6;
        public bool bt_Discon_ST6
        {
            get => _bt_Discon_ST6;
            set => Set(ref _bt_Discon_ST6, value);
        }
        #endregion

        #region Активность кнопки отключить для ПТ-6 
        private bool _bt_DisconAct_ST6 = false;
        public bool bt_DisconAct_ST6
        {
            get => _bt_DisconAct_ST6;
            set => Set(ref _bt_DisconAct_ST6, value);
        }
        #endregion

        #region Измеряемые велечины
        private double _prm_10MAD10CG010;
        public double prm_10MAD10CG010
        {
            get => _prm_10MAD10CG010;
            set => Set(ref _prm_10MAD10CG010, value);
        }

        private double _prm_10MAD10CG011;
        public double prm_10MAD10CG011
        {
            get => _prm_10MAD10CG011;
            set => Set(ref _prm_10MAD10CG011, value);
        }

        private double _prm_10MAD10CG012;
        public double prm_10MAD10CG012
        {
            get => _prm_10MAD10CG012;
            set => Set(ref _prm_10MAD10CG012, value);
        }
        #endregion






        #region Комманды
        public RelayCommand cmd_ConToRack_ST6 { get; set; }
        public RelayCommand cmd_DisconFromRack_ST6 { get; set; }
        #endregion

        public MainWindowViewModel()
        {

            cmd_ConToRack_ST6 = new RelayCommand(o =>
            {
                bt_Discon_ST6 = false;
                bt_DisconAct_ST6 = true;

                 var modbusClient_ST6 = new ModbusClient(tb_AdressIP_ST6, 502);

                  // Проверка валидности IP адреса
                  var Match = Regex.IsMatch(tb_AdressIP_ST6, "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
                  if (Match == false)
                  {
                      MessageBox.Show("IP адрес не соответствуте формату IPV4");
                      return;
                  }

                  try
                  {
                      modbusClient_ST6.Connect();
                  }
                  catch (Exception )
                  { 
                      MessageBox.Show("Устройство не отвечает");
                      return;
                  }    

                      tbl_Status_ST6 = "Подключено";
                      sb_Bckgrnd_ST6 = "LightGreen";
                      bt_ConAct_ST6 = false;
                      tb_IPAdrrAct_ST6 = false;    

                      var bnRack_ST6 = new BNRack();
                      var timer_ST6 = new DispatcherTimer(DispatcherPriority.Render);              
                       timer_ST6.Interval = TimeSpan.FromSeconds(1);
                       timer_ST6.Tick += (sender, args) =>
                      {
                          if (bt_Discon_ST6 == false)
                          {
                              int[] prm_st6_gr0 = modbusClient_ST6.ReadHoldingRegisters(0, 3);
                              double [] rtrn_prm_st6_gr0 = bnRack_ST6.Scale(prm_st6_gr0, GatewayFullScaleValue_ST6, 
                                                                            LowerMonitorRange_ST6_gr0, UpperMonitorRange_ST6_gr0,
                                                                            FaultReplace_ST6_gr0);

                              prm_10MAD10CG010 = rtrn_prm_st6_gr0[0];
                              prm_10MAD10CG011 = rtrn_prm_st6_gr0[1];
                              prm_10MAD10CG012 = rtrn_prm_st6_gr0[2];

                              var visualEffects = new VisualEffects();
                              string [] bckgrd_gr0 = visualEffects.LimitBrush(rtrn_prm_st6_gr0, WH_Y_0_ST6_gr0, WH_Y_1_ST6_gr0,
                                                                              WL_Y_0_ST6_gr0, WL_Y_1_ST6_gr0, AH_R_0_ST6_gr0,
                                                                              AH_R_1_ST6_gr0, AL_R_0_ST6_gr0, AL_R_1_ST6_gr0,
                                                                              Fault_0_ST6_gr0, Fault_1_ST6_gr0);

                              bckgrd_10MAD10CG010  = bckgrd_gr0[0];


    }
                          else
                          {
                              modbusClient_ST6.Disconnect();
                              timer_ST6.Stop();
                              tbl_Status_ST6 = "Отключено";
                              sb_Bckgrnd_ST6 = "Coral";
                              bt_ConAct_ST6 = true;
                              tb_IPAdrrAct_ST6 = true;
                              bt_DisconAct_ST6 = false;
                          }
                      };
                   timer_ST6.Start();

            });

            cmd_DisconFromRack_ST6 = new RelayCommand(o =>
            {
                bt_Discon_ST6 = true;

            });





         //   BackgroundBorder = "#e5e5e5";



        }
    }
}
