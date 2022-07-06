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
        //Замена если вышло за границы
        private const double FaultReplace_0 = 999.999; 
        private const double FaultReplace_1 = 999.9;

        #region Параметры для шкалирования ПТ-6
        private const ushort GatewayFullScaleValue_st6 = 16383;

        private const short LowerMonitorRange_st6_gr0 = 0;
        private const short UpperMonitorRange_st6_gr0 = 200;

        private const short LowerMonitorRange_st6_gr1 = -1;
        private const short UpperMonitorRange_st6_gr1 = 1;

        private const short LowerMonitorRange_st6_gr5 = -6;
        private const short UpperMonitorRange_st6_gr5 = 4;
        #endregion

        #region Аварийные и предупредительные границы ПТ-6
        private const double WH_Y_0_st6_gr0 = 60;
        private const double WH_Y_1_st6_gr0 = 79.99;
        private const double AH_R_0_st6_gr0 = 80;
        private const double AH_R_1_st6_gr0 = 200;


        private const double WH_Y_0_st6_gr1 = 0.5;
        private const double WH_Y_1_st6_gr1 = 0.79;
        private const double WL_Y_0_st6_gr1 = -0.5;
        private const double WL_Y_1_st6_gr1 = -0.79;
        private const double AH_R_0_st6_gr1 = 0.8;
        private const double AH_R_1_st6_gr1 = 1;
        private const double AL_R_0_st6_gr1 = -0.8;
        private const double AL_R_1_st6_gr1 = -1;

        private const double WH_Y_0_st6_gr5 = 0.5;
        private const double WH_Y_1_st6_gr5 = 1.99;
        private const double WL_Y_0_st6_gr5 = -2.5;
        private const double WL_Y_1_st6_gr5 = -3.99;
        private const double AH_R_0_st6_gr5 = 2;
        private const double AH_R_1_st6_gr5 = 4;
        private const double AL_R_0_st6_gr5 = -4.0;
        private const double AL_R_1_st6_gr5 = -6;
        #endregion

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
        private string _tbl_Status_st6 = "Отключено"; // поле
        public string tbl_Status_st6
        {
            get => _tbl_Status_st6; //возвращает значение поля
            set => Set(ref _tbl_Status_st6, value);
        }
        #endregion

        #region IP адрес подключения ПТ-6
        private string _tb_AdressIP_st6 = "127.0.0.1";
        public string tb_AdressIP_st6
        {
            get => _tb_AdressIP_st6;
            set => Set(ref _tb_AdressIP_st6, value);
        }
        #endregion

        #region Фон для параметров ПТ-6
        private string _bckgrd_10MAD10CG010;
        public string bckgrd_10MAD10CG010
        {
            get => _bckgrd_10MAD10CG010;
            set => Set(ref _bckgrd_10MAD10CG010, value);
        }

        private string _bckgrd_10MAD10CG011;
        public string bckgrd_10MAD10CG011
        {
            get => _bckgrd_10MAD10CG011;
            set => Set(ref _bckgrd_10MAD10CG011, value);
        }

        private string _bckgrd_10MAD10CG012;
        public string bckgrd_10MAD10CG012
        {
            get => _bckgrd_10MAD10CG012;
            set => Set(ref _bckgrd_10MAD10CG012, value);
        }

        private string _bckgrd_10MAD20CG010;
        public string bckgrd_10MAD20CG010
        {
            get => _bckgrd_10MAD20CG010;
            set => Set(ref _bckgrd_10MAD20CG010, value);
        }

        private string _bckgrd_10MAD10CY011;
        public string bckgrd_10MAD10CY011
        {
            get => _bckgrd_10MAD10CY011;
            set => Set(ref _bckgrd_10MAD10CY011, value);
        }

        private string _bckgrd_10MAD10CY012;
        public string bckgrd_10MAD10CY012
        {
            get => _bckgrd_10MAD10CY012;
            set => Set(ref _bckgrd_10MAD10CY012, value);
        }
        #endregion

        #region Фон для StatusBarst6 
        private string _sb_Bckgrnd_st6 = "Coral";
        public string sb_Bckgrnd_st6
        {
            get => _sb_Bckgrnd_st6;
            set => Set(ref _sb_Bckgrnd_st6, value);
        }
        #endregion

        #region Активность ввода адреса для ПТ-6 
        private bool _tb_IPAdrrAct_st6 = true;
        public bool tb_IPAdrrAct_st6
        {
            get => _tb_IPAdrrAct_st6;
            set => Set(ref _tb_IPAdrrAct_st6, value);
        }
        #endregion

        #region Активность кнопки подключить для ПТ-6 
        private bool _bt_ConAct_st6 = true;
        public bool bt_ConAct_st6
        {
            get => _bt_ConAct_st6;
            set => Set(ref _bt_ConAct_st6, value);
        }
        #endregion

        #region Кнопка отключить для ПТ-6
        private bool _bt_Discon_st6;
        public bool bt_Discon_st6
        {
            get => _bt_Discon_st6;
            set => Set(ref _bt_Discon_st6, value);
        }
        #endregion

        #region Активность кнопки отключить для ПТ-6 
        private bool _bt_DisconAct_st6 = false;
        public bool bt_DisconAct_st6
        {
            get => _bt_DisconAct_st6;
            set => Set(ref _bt_DisconAct_st6, value);
        }
        #endregion

        #region Измеряемые велечины ПТ-6
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

        private double _prm_10MAD20CG010;
        public double prm_10MAD20CG010
        {
            get => _prm_10MAD20CG010;
            set => Set(ref _prm_10MAD20CG010, value);
        }

        private double _prm_10MAD10CY011;
        public double prm_10MAD10CY011
        {
            get => _prm_10MAD10CY011;
            set => Set(ref _prm_10MAD10CY011, value);
        }

        private double _prm_10MAD10CY012;
        public double prm_10MAD10CY012
        {
            get => _prm_10MAD10CY012;
            set => Set(ref _prm_10MAD10CY012, value);
        }



        #endregion

        #region Комманды
        public RelayCommand cmd_ConToRack_st6 { get; set; }
        public RelayCommand cmd_DisconFromRack_st6 { get; set; }
        #endregion

        public MainWindowViewModel()
        {
            cmd_ConToRack_st6 = new RelayCommand(o =>
            {
                bt_Discon_st6 = false;

                var bnRack_st6 = new BNRack();

                if (bnRack_st6.ValidIPV4(tb_AdressIP_st6) == true)
                {
                    if (bnRack_st6.Connection(tb_AdressIP_st6, 502) == true)
                    { 
                        tbl_Status_st6 = "Подключено";
                        sb_Bckgrnd_st6 = "LightGreen";
                        bt_ConAct_st6 = false;
                        tb_IPAdrrAct_st6 = false;
                        bt_DisconAct_st6 = true;

                        var timer_st6 = new DispatcherTimer(DispatcherPriority.Render);
                        timer_st6.Interval = TimeSpan.FromSeconds(1);
                        timer_st6.Tick += (sender, args) =>
                        {

                            if (bt_Discon_st6 == false)
                            { 
                               int[] prm_st6_gr0 = bnRack_st6.ReadData(5000, 23);
                               double[] rtrn_prm_st6_gr0 = bnRack_st6.Scale(prm_st6_gr0, GatewayFullScaleValue_st6,
                                                                            LowerMonitorRange_st6_gr0, UpperMonitorRange_st6_gr0,
                                                                            FaultReplace_1);
                               int[] prm_st6_gr1 = bnRack_st6.ReadData(5024, 2);
                               double[] rtrn_prm_st6_gr1 = bnRack_st6.Scale(prm_st6_gr1, GatewayFullScaleValue_st6,
                                                                            LowerMonitorRange_st6_gr1, UpperMonitorRange_st6_gr1,
                                                                            FaultReplace_0);
                               int[] prm_st6_gr5 = bnRack_st6.ReadData(5042, 1);
                               double[] rtrn_prm_st6_gr5 = bnRack_st6.Scale(prm_st6_gr5, GatewayFullScaleValue_st6,
                                                                            LowerMonitorRange_st6_gr5, UpperMonitorRange_st6_gr5,
                                                                            FaultReplace_0);
                               int[] prm_st6_gr7 = bnRack_st6.ReadData(5047, 1);
                               double[] rtrn_prm_st6_gr7 = bnRack_st6.Scale(prm_st6_gr7, GatewayFullScaleValue_st6,
                                                                            LowerMonitorRange_st6_gr1, UpperMonitorRange_st6_gr1,
                                                                            FaultReplace_0);

                               var visualEffects_st6 = new VisualEffects();
                               string[] bckgrd_st6_gr0 = visualEffects_st6.LimitBrush_1(rtrn_prm_st6_gr0, WH_Y_0_st6_gr0, WH_Y_1_st6_gr0,
                                                                                        AH_R_0_st6_gr0, AH_R_1_st6_gr0,
                                                                                        UpperMonitorRange_st6_gr0, LowerMonitorRange_st6_gr0);
                               string[] bckgrd_st6_gr1 = visualEffects_st6.LimitBrush_0(rtrn_prm_st6_gr1, WH_Y_0_st6_gr1, WH_Y_1_st6_gr1,
                                                                                        WL_Y_0_st6_gr1, WL_Y_1_st6_gr1, AH_R_0_st6_gr1,
                                                                                        AH_R_1_st6_gr1, AL_R_0_st6_gr1, AL_R_1_st6_gr1,
                                                                                        UpperMonitorRange_st6_gr1, LowerMonitorRange_st6_gr1);
                               string[] bckgrd_st6_gr5 = visualEffects_st6.LimitBrush_0(rtrn_prm_st6_gr5, WH_Y_0_st6_gr5, WH_Y_1_st6_gr5,
                                                                                        WL_Y_0_st6_gr5, WL_Y_1_st6_gr5, AH_R_0_st6_gr5,
                                                                                        AH_R_1_st6_gr5, AL_R_0_st6_gr5, AL_R_1_st6_gr5,
                                                                                        UpperMonitorRange_st6_gr5, LowerMonitorRange_st6_gr5);
                               string[] bckgrd_st6_gr7 = visualEffects_st6.LimitBrush_0(rtrn_prm_st6_gr7, WH_Y_0_st6_gr1, WH_Y_1_st6_gr1,
                                                                                        WL_Y_0_st6_gr1, WL_Y_1_st6_gr1, AH_R_0_st6_gr1,
                                                                                        AH_R_1_st6_gr1, AL_R_0_st6_gr1, AL_R_1_st6_gr1,
                                                                                        UpperMonitorRange_st6_gr1, LowerMonitorRange_st6_gr1);
                               prm_10MAD10CY011 = rtrn_prm_st6_gr0[0];
                               prm_10MAD10CY012 = rtrn_prm_st6_gr0[2];
                               prm_10MAD10CG010 = rtrn_prm_st6_gr1[0];
                               prm_10MAD10CG011 = rtrn_prm_st6_gr1[1];
                               prm_10MAD20CG010 = rtrn_prm_st6_gr5[0];
                               prm_10MAD10CG012 = rtrn_prm_st6_gr7[0];
                               
                               bckgrd_10MAD10CY011 = bckgrd_st6_gr0[0];
                               bckgrd_10MAD10CY012 = bckgrd_st6_gr0[2];
                               bckgrd_10MAD10CG010 = bckgrd_st6_gr1[0];
                               bckgrd_10MAD10CG011 = bckgrd_st6_gr1[1];
                               bckgrd_10MAD20CG010 = bckgrd_st6_gr5[0];
                               bckgrd_10MAD10CG012 = bckgrd_st6_gr7[0];

                            }
                            else
                            {
                               bnRack_st6.Disconnection();
                               timer_st6.Stop();
                               tbl_Status_st6 = "Отключено";
                               sb_Bckgrnd_st6 = "Coral";
                               bt_ConAct_st6 = true;
                               tb_IPAdrrAct_st6 = true;
                               bt_DisconAct_st6 = false;
                            }
                        };
                        timer_st6.Start();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            });

            cmd_DisconFromRack_st6 = new RelayCommand(o =>
            {
                bt_Discon_st6 = true;

            });

        }
    }
}
