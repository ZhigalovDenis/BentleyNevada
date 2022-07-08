﻿using BN.Infrostructure.Commands;
using BN.Models;
using System;
using System.Linq;
using System.Windows.Threading;
namespace BN.ViewModels.Base
{
    internal class MainWindowViewModel : ViewModel
    {
        //Замена если вышло за границы
        private const double FaultReplace_0 = 999.999; 
        private const double FaultReplace_1 = 999.9;
        private const double FaultReplace_2 = 999.99;

        #region Параметры для шкалирования ПТ-6
        private const ushort GatewayFullScaleValue_st6 = 16383;

        private const short LowerMonitorRange_st6_gr0 = 0;
        private const short UpperMonitorRange_st6_gr0 = 200;

        private const short LowerMonitorRange_st6_gr1 = -1;
        private const short UpperMonitorRange_st6_gr1 = 1;

        private const short LowerMonitorRange_st6_gr2 = 0;
        private const short UpperMonitorRange_st6_gr2 = 25;

        private const short LowerMonitorRange_st6_gr4 = -6;
        private const short UpperMonitorRange_st6_gr4 = 4;
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

        private const double WH_Y_0_st6_gr2 = 7.1;
        private const double WH_Y_1_st6_gr2 = 10.99;
        private const double AH_R_0_st6_gr2 = 11;
        private const double AH_R_1_st6_gr2 = 25;

        private const double WH_Y_0_st6_gr4 = 0.5;
        private const double WH_Y_1_st6_gr4 = 1.99;
        private const double WL_Y_0_st6_gr4 = -2.5;
        private const double WL_Y_1_st6_gr4 = -3.99;
        private const double AH_R_0_st6_gr4 = 2;
        private const double AH_R_1_st6_gr4 = 4;
        private const double AL_R_0_st6_gr4 = -4.0;
        private const double AL_R_1_st6_gr4 = -6;
        #endregion

        #region Заголовок окна
        private string _wnd_Title = "АСКВД Bentley Nevada";
        public string Wnd_Title
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
        public string Tbl_Status_st6
        {
            get => _tbl_Status_st6; //возвращает значение поля
            set => Set(ref _tbl_Status_st6, value);
        }
        #endregion

        #region IP адрес подключения ПТ-6
        private string _ipfld_AdressIP_st6 = "127.0.0.1";
        public string Ipfld_AdressIP_st6
        {
            get => _ipfld_AdressIP_st6;
            set => Set(ref _ipfld_AdressIP_st6, value);
        }
        #endregion

        #region Фон для параметров ПТ-6
        private string _bckgrd_10MAD10CG010;
        public string Bckgrd_10MAD10CG010
        {
            get => _bckgrd_10MAD10CG010;
            set => Set(ref _bckgrd_10MAD10CG010, value);
        }

        private string _bckgrd_10MAD10CG011;
        public string Bckgrd_10MAD10CG011
        {
            get => _bckgrd_10MAD10CG011;
            set => Set(ref _bckgrd_10MAD10CG011, value);
        }

        private string _bckgrd_10MAD10CG012;
        public string Bckgrd_10MAD10CG012
        {
            get => _bckgrd_10MAD10CG012;
            set => Set(ref _bckgrd_10MAD10CG012, value);
        }

        private string _bckgrd_10MAD20CG010;
        public string Bckgrd_10MAD20CG010
        {
            get => _bckgrd_10MAD20CG010;
            set => Set(ref _bckgrd_10MAD20CG010, value);
        }

        private string _bckgrd_10MAD10CY011;
        public string Bckgrd_10MAD10CY011
        {
            get => _bckgrd_10MAD10CY011;
            set => Set(ref _bckgrd_10MAD10CY011, value);
        }

        private string _bckgrd_10MAD10CY012;
        public string Bckgrd_10MAD10CY012
        {
            get => _bckgrd_10MAD10CY012;
            set => Set(ref _bckgrd_10MAD10CY012, value);
        }

        private string _bckgrd_10MAD20CY011;
        public string Bckgrd_10MAD20CY011
        {
            get => _bckgrd_10MAD20CY011;
            set => Set(ref _bckgrd_10MAD20CY011, value);
        }

        private string _bckgrd_10MAD20CY012;
        public string Bckgrd_10MAD20CY012
        {
            get => _bckgrd_10MAD20CY012;
            set => Set(ref _bckgrd_10MAD20CY012, value);
        }

        private string _bckgrd_10MAD30CY011;
        public string Bckgrd_10MAD30CY011
        {
            get => _bckgrd_10MAD30CY011;
            set => Set(ref _bckgrd_10MAD30CY011, value);
        }

        private string _bckgrd_10MAD30CY012;
        public string Bckgrd_10MAD30CY012
        {
            get => _bckgrd_10MAD30CY012;
            set => Set(ref _bckgrd_10MAD30CY012, value);
        }

        private string _bckgrd_10MAD40CY011;
        public string Bckgrd_10MAD40CY011
        {
            get => _bckgrd_10MAD40CY011;
            set => Set(ref _bckgrd_10MAD40CY011, value);
        }

        private string _bckgrd_10MAD40CY012;
        public string Bckgrd_10MAD40CY012
        {
            get => _bckgrd_10MAD40CY012;
            set => Set(ref _bckgrd_10MAD40CY012, value);
        }

        private string _bckgrd_10MAD50CY011;
        public string Bckgrd_10MAD50CY011
        {
            get => _bckgrd_10MAD50CY011;
            set => Set(ref _bckgrd_10MAD50CY011, value);
        }

        private string _bckgrd_10MAD50CY012;
        public string Bckgrd_10MAD50CY012
        {
            get => _bckgrd_10MAD50CY012;
            set => Set(ref _bckgrd_10MAD50CY012, value);
        }

        private string _bckgrd_10MAD60CY011;
        public string Bckgrd_10MAD60CY011
        {
            get => _bckgrd_10MAD60CY011;
            set => Set(ref _bckgrd_10MAD60CY011, value);
        }

        private string _bckgrd_10MAD60CY012;
        public string Bckgrd_10MAD60CY012
        {
            get => _bckgrd_10MAD60CY012;
            set => Set(ref _bckgrd_10MAD60CY012, value);
        }

        private string _bckgrd_10MKA10CY011;
        public string Bckgrd_10MKA10CY011
        {
            get => _bckgrd_10MKA10CY011;
            set => Set(ref _bckgrd_10MKA10CY011, value);
        }

        private string _bckgrd_10MKA10CY012;
        public string Bckgrd_10MKA10CY012
        {
            get => _bckgrd_10MKA10CY012;
            set => Set(ref _bckgrd_10MKA10CY012, value);
        }

        private string _bckgrd_10MKA20CY011;
        public string Bckgrd_10MKA20CY011
        {
            get => _bckgrd_10MKA20CY011;
            set => Set(ref _bckgrd_10MKA20CY011, value);
        }

        private string _bckgrd_10MKA20CY012;
        public string Bckgrd_10MKA20CY012
        {
            get => _bckgrd_10MKA20CY012;
            set => Set(ref _bckgrd_10MKA20CY012, value);
        }
        #endregion

        #region Фон для StatusBar_st6 
        private string _sb_Bckgrnd_st6 = "Coral";
        public string Sb_Bckgrnd_st6
        {
            get => _sb_Bckgrnd_st6;
            set => Set(ref _sb_Bckgrnd_st6, value);
        }
        #endregion

        #region Фон для TextBlock_st6 
        private string _tbl_Bckgrnd_st6 = "Default";
        public string Tbl_Bckgrnd_st6
        {
            get => _tbl_Bckgrnd_st6;
            set => Set(ref _tbl_Bckgrnd_st6, value);
        }
        #endregion

        #region Активность ввода адреса для ПТ-6 
        private bool _ipfld_IPAdrrAct_st6 = true;
        public bool Ipfld_IPAdrrAct_st6
        {
            get => _ipfld_IPAdrrAct_st6;
            set => Set(ref _ipfld_IPAdrrAct_st6, value);
        }
        #endregion

        #region Активность кнопки подключить для ПТ-6 
        private bool _bt_ConAct_st6 = true;
        public bool Bt_ConAct_st6
        {
            get => _bt_ConAct_st6;
            set => Set(ref _bt_ConAct_st6, value);
        }
        #endregion

        #region Кнопка отключить для ПТ-6
        private bool _bt_Discon_st6;
        public bool Bt_Discon_st6
        {
            get => _bt_Discon_st6;
            set => Set(ref _bt_Discon_st6, value);
        }
        #endregion

        #region Активность кнопки отключить для ПТ-6 
        private bool _bt_DisconAct_st6 = false;
        public bool Bt_DisconAct_st6
        {
            get => _bt_DisconAct_st6;
            set => Set(ref _bt_DisconAct_st6, value);
        }
        #endregion

        #region Измеряемые велечины ПТ-6
        private double _prm_10MAD10CG010;
        public double Prm_10MAD10CG010
        {
            get => _prm_10MAD10CG010;
            set => Set(ref _prm_10MAD10CG010, value);
        }

        private double _prm_10MAD10CG011;
        public double Prm_10MAD10CG011
        {
            get => _prm_10MAD10CG011;
            set => Set(ref _prm_10MAD10CG011, value);
        }

        private double _prm_10MAD10CG012;
        public double Prm_10MAD10CG012
        {
            get => _prm_10MAD10CG012;
            set => Set(ref _prm_10MAD10CG012, value);
        }

        private double _prm_10MAD20CG010;
        public double Prm_10MAD20CG010
        {
            get => _prm_10MAD20CG010;
            set => Set(ref _prm_10MAD20CG010, value);
        }

        private double _prm_10MAD10CY011;
        public double Prm_10MAD10CY011
        {
            get => _prm_10MAD10CY011;
            set => Set(ref _prm_10MAD10CY011, value);
        }

        private double _prm_10MAD10CY012;
        public double Prm_10MAD10CY012
        {
            get => _prm_10MAD10CY012;
            set => Set(ref _prm_10MAD10CY012, value);
        }

        private double _prm_10MAD20CY011;
        public double Prm_10MAD20CY011
        {
            get => _prm_10MAD20CY011;
            set => Set(ref _prm_10MAD20CY011, value);
        }

        private double _prm_10MAD20CY012;
        public double Prm_10MAD20CY012
        {
            get => _prm_10MAD20CY012;
            set => Set(ref _prm_10MAD20CY012, value);
        }

        private double _prm_10MAD30CY011;
        public double Prm_10MAD30CY011
        {
            get => _prm_10MAD30CY011;
            set => Set(ref _prm_10MAD30CY011, value);
        }

        private double _prm_10MAD30CY012;
        public double Prm_10MAD30CY012
        {
            get => _prm_10MAD30CY012;
            set => Set(ref _prm_10MAD30CY012, value);
        }

        private double _prm_10MAD40CY011;
        public double Prm_10MAD40CY011
        {
            get => _prm_10MAD40CY011;
            set => Set(ref _prm_10MAD40CY011, value);
        }

        private double _prm_10MAD40CY012;
        public double Prm_10MAD40CY012
        {
            get => _prm_10MAD40CY012;
            set => Set(ref _prm_10MAD40CY012, value);
        }

        private double _prm_10MAD50CY011;
        public double Prm_10MAD50CY011
        {
            get => _prm_10MAD50CY011;
            set => Set(ref _prm_10MAD50CY011, value);
        }

        private double _prm_10MAD50CY012;
        public double Prm_10MAD50CY012
        {
            get => _prm_10MAD50CY012;
            set => Set(ref _prm_10MAD50CY012, value);
        }

        private double _prm_10MAD60CY011;
        public double Prm_10MAD60CY011
        {
            get => _prm_10MAD60CY011;
            set => Set(ref _prm_10MAD60CY011, value);
        }

        private double _prm_10MAD60CY012;
        public double Prm_10MAD60CY012
        {
            get => _prm_10MAD60CY012;
            set => Set(ref _prm_10MAD60CY012, value);
        }

        private double _prm_10MKA10CY011;
        public double Prm_10MKA10CY011
        {
            get => _prm_10MKA10CY011;
            set => Set(ref _prm_10MKA10CY011, value);
        }

        private double _prm_10MKA10CY012;
        public double Prm_10MKA10CY012
        {
            get => _prm_10MKA10CY012;
            set => Set(ref _prm_10MKA10CY012, value);
        }

        private double _prm_10MKA20CY011;
        public double Prm_10MKA20CY011
        {
            get => _prm_10MKA20CY011;
            set => Set(ref _prm_10MKA20CY011, value);
        }

        private double _prm_10MKA20CY012;
        public double Prm_10MKA20CY012
        {
            get => _prm_10MKA20CY012;
            set => Set(ref _prm_10MKA20CY012, value);
        }
        #endregion

        #region Комманды
        public RelayCommand Cmd_ConToRack_st6 { get; set; }
        public RelayCommand Cmd_DisconFromRack_st6 { get; set; }
        #endregion

        public MainWindowViewModel()
        {
            Cmd_ConToRack_st6 = new RelayCommand(o =>
            {
                Bt_Discon_st6 = false;

                var bnRack_st6 = new BNRack();

                if (bnRack_st6.ValidIPV4(Ipfld_AdressIP_st6) == true) //Проверяем валидность IP адреса.
                {
                    if (bnRack_st6.Connection(Ipfld_AdressIP_st6, 502) == true) //Пытаемся подключиться к устройству.
                    { 
                        Tbl_Status_st6 = "Подключено";
                        Sb_Bckgrnd_st6 = "LightGreen";
                        Bt_ConAct_st6 = false;
                        Ipfld_IPAdrrAct_st6 = false;
                        Bt_DisconAct_st6 = true;

                        var timer_st6 = new DispatcherTimer(DispatcherPriority.Render);
                        timer_st6.Interval = TimeSpan.FromSeconds(1);
                        timer_st6.Tick += (sender, args) =>
                        {

                            if (Bt_Discon_st6 == false) // Проверяем нажата или нет кнопка "Отключить".
                            {
                               int[] full_range = bnRack_st6.ReadData(5000, 100);
                                if (full_range.Length < 101) // Если вернулся массив размерностью 101 то не удалется считать регистры. И переходим к условию else.
                                {
                                    int[] prm_st6_gr_0_0 = full_range.Skip(0).Take(1).ToArray();//5000
                                    int[] prm_st6_gr_0_1 = full_range.Skip(2).Take(2).ToArray();//5002-5003
                                    int[] prm_st6_gr_0_2 = full_range.Skip(5).Take(2).ToArray();//5005-5006
                                    int[] prm_st6_gr_0_3 = full_range.Skip(8).Take(2).ToArray();//5008-5009
                                    int[] prm_st6_gr_0_4 = full_range.Skip(11).Take(2).ToArray();//5011-5012
                                    int[] prm_st6_gr_0_5 = full_range.Skip(14).Take(2).ToArray();//5014-5015
                                    int[] prm_st6_gr_0_6 = full_range.Skip(17).Take(2).ToArray();//5017-5018
                                    int[] prm_st6_gr_0_7 = full_range.Skip(20).Take(2).ToArray();//5020-5021
                                    int[] prm_st6_gr_0_8 = full_range.Skip(23).Take(1).ToArray();//5023
                                    int[] prm_st6_gr0 = prm_st6_gr_0_0.Concat(prm_st6_gr_0_1).Concat(prm_st6_gr_0_2)
                                                                       .Concat(prm_st6_gr_0_3).Concat(prm_st6_gr_0_4)
                                                                       .Concat(prm_st6_gr_0_5).Concat(prm_st6_gr_0_6)
                                                                       .Concat(prm_st6_gr_0_7).Concat(prm_st6_gr_0_8).ToArray();

                                    int[] prm_st6_gr_1_0 = full_range.Skip(24).Take(2).ToArray(); //5024-5025
                                    int[] prm_st6_gr_1_1 = full_range.Skip(47).Take(1).ToArray(); //5047 
                                    int[] prm_st6_gr1 = prm_st6_gr_1_0.Concat(prm_st6_gr_1_1).ToArray();

                                    /////Группа 2

                                    int[] prm_st6_gr4 = full_range.Skip(42).Take(1).ToArray(); //5042


                                    double[] rtrn_prm_st6_gr0 = bnRack_st6.Scale(prm_st6_gr0, GatewayFullScaleValue_st6,
                                                                                 LowerMonitorRange_st6_gr0, UpperMonitorRange_st6_gr0,
                                                                                 FaultReplace_1);
                                    double[] rtrn_prm_st6_gr1 = bnRack_st6.Scale(prm_st6_gr1, GatewayFullScaleValue_st6,
                                                                                 LowerMonitorRange_st6_gr1, UpperMonitorRange_st6_gr1,
                                                                                 FaultReplace_0);
                                    double[] rtrn_prm_st6_gr4 = bnRack_st6.Scale(prm_st6_gr4, GatewayFullScaleValue_st6,
                                                                                 LowerMonitorRange_st6_gr4, UpperMonitorRange_st6_gr4,
                                                                                 FaultReplace_0);

                                    var visualEffects_st6 = new VisualEffects();
                                    string[] bckgrd_st6_gr0 = visualEffects_st6.LimitBrush_1(rtrn_prm_st6_gr0, WH_Y_0_st6_gr0, WH_Y_1_st6_gr0,
                                                                                             AH_R_0_st6_gr0, AH_R_1_st6_gr0,
                                                                                             UpperMonitorRange_st6_gr0, LowerMonitorRange_st6_gr0);
                                    string[] bckgrd_st6_gr1 = visualEffects_st6.LimitBrush_0(rtrn_prm_st6_gr1, WH_Y_0_st6_gr1, WH_Y_1_st6_gr1,
                                                                                             WL_Y_0_st6_gr1, WL_Y_1_st6_gr1, AH_R_0_st6_gr1,
                                                                                             AH_R_1_st6_gr1, AL_R_0_st6_gr1, AL_R_1_st6_gr1,
                                                                                             UpperMonitorRange_st6_gr1, LowerMonitorRange_st6_gr1);
                                    string[] bckgrd_st6_gr4 = visualEffects_st6.LimitBrush_0(rtrn_prm_st6_gr4, WH_Y_0_st6_gr4, WH_Y_1_st6_gr4,
                                                                                             WL_Y_0_st6_gr4, WL_Y_1_st6_gr4, AH_R_0_st6_gr4,
                                                                                             AH_R_1_st6_gr4, AL_R_0_st6_gr4, AL_R_1_st6_gr4,
                                                                                             UpperMonitorRange_st6_gr4, LowerMonitorRange_st6_gr4);

                                    Prm_10MAD10CY011 = rtrn_prm_st6_gr0[0];
                                    Prm_10MAD10CY012 = rtrn_prm_st6_gr0[1];
                                    Prm_10MAD20CY011 = rtrn_prm_st6_gr0[2];
                                    Prm_10MAD20CY012 = rtrn_prm_st6_gr0[3];
                                    Prm_10MAD30CY011 = rtrn_prm_st6_gr0[4];
                                    Prm_10MAD30CY012 = rtrn_prm_st6_gr0[5];
                                    Prm_10MAD40CY011 = rtrn_prm_st6_gr0[6];
                                    Prm_10MAD40CY012 = rtrn_prm_st6_gr0[7];
                                    Prm_10MAD50CY011 = rtrn_prm_st6_gr0[8];
                                    Prm_10MAD50CY012 = rtrn_prm_st6_gr0[9];
                                    Prm_10MAD60CY011 = rtrn_prm_st6_gr0[10];
                                    Prm_10MAD60CY012 = rtrn_prm_st6_gr0[11]; 
                                    Prm_10MKA10CY011 = rtrn_prm_st6_gr0[12];
                                    Prm_10MKA10CY012 = rtrn_prm_st6_gr0[13];
                                    Prm_10MKA20CY011 = rtrn_prm_st6_gr0[14];
                                    Prm_10MKA20CY012 = rtrn_prm_st6_gr0[15];
                                    Prm_10MAD10CG010 = rtrn_prm_st6_gr1[0];
                                    Prm_10MAD10CG011 = rtrn_prm_st6_gr1[1];
                                    Prm_10MAD10CG012 = rtrn_prm_st6_gr1[2];
                                    Prm_10MAD20CG010 = rtrn_prm_st6_gr4[0];
                                    
                                    Bckgrd_10MAD10CY011 = bckgrd_st6_gr0[0];
                                    Bckgrd_10MAD10CY012 = bckgrd_st6_gr0[1];
                                    Bckgrd_10MAD20CY011 = bckgrd_st6_gr0[2];
                                    Bckgrd_10MAD20CY012 = bckgrd_st6_gr0[3];
                                    Bckgrd_10MAD30CY011 = bckgrd_st6_gr0[4];
                                    Bckgrd_10MAD30CY012 = bckgrd_st6_gr0[5];
                                    Bckgrd_10MAD40CY011 = bckgrd_st6_gr0[6];
                                    Bckgrd_10MAD40CY012 = bckgrd_st6_gr0[7];
                                    Bckgrd_10MAD50CY011 = bckgrd_st6_gr0[8];
                                    Bckgrd_10MAD50CY012 = bckgrd_st6_gr0[9];
                                    Bckgrd_10MAD60CY011 = bckgrd_st6_gr0[10];
                                    Bckgrd_10MAD60CY012 = bckgrd_st6_gr0[11];
                                    Bckgrd_10MKA10CY011 = bckgrd_st6_gr0[12];
                                    Bckgrd_10MKA10CY012 = bckgrd_st6_gr0[13];
                                    Bckgrd_10MKA20CY011 = bckgrd_st6_gr0[14];
                                    Bckgrd_10MKA20CY012 = bckgrd_st6_gr0[15];
                                    Bckgrd_10MAD10CG010 = bckgrd_st6_gr1[0];
                                    Bckgrd_10MAD10CG011 = bckgrd_st6_gr1[1];
                                    Bckgrd_10MAD10CG012 = bckgrd_st6_gr1[2];
                                    Bckgrd_10MAD20CG010 = bckgrd_st6_gr4[0];
                                }
                                else//Вернулся массив размерностью 101. Значит не удалось считать регистры.
                                {
                                    bnRack_st6.Disconnection();
                                    Tbl_Status_st6 = "Потеря связи";
                                    Tbl_Bckgrnd_st6 = "Red";
                                    Sb_Bckgrnd_st6 = "BlueViolet";                                
                                    if (bnRack_st6.Reconnection(Ipfld_AdressIP_st6, 502) == true)//Проверка повторного подключения к устройству
                                    {
                                        Tbl_Status_st6 = "Подключено";
                                        Tbl_Bckgrnd_st6 = "Default";
                                        Sb_Bckgrnd_st6 = "LightGreen";
                                    }
                                }
                             
                            }
                            else //Действеи если нажата кнопка "Отключить".
                            {
                               bnRack_st6.Disconnection();
                               timer_st6.Stop();
                               Tbl_Status_st6 = "Отключено";
                               Sb_Bckgrnd_st6 = "Coral";
                               Bt_ConAct_st6 = true;
                               Ipfld_IPAdrrAct_st6 = true;
                               Bt_DisconAct_st6 = false;
                            }
                        };
                        timer_st6.Start();
                    }
                    else //Действие если подключиться к устройству не удалось
                    {
                        return;
                    }
                }
                else //Действие если IP адрес не валиден.
                {
                    return;
                }
            });

            Cmd_DisconFromRack_st6 = new RelayCommand(o =>
            {
                Bt_Discon_st6 = true;

            });

        }
    }
}
