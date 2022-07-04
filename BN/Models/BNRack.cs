using EasyModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BN.Models
{
    internal class BNRack
    {

        private const ushort GatewayFullScaleValue = 16383;
        private const byte UpperMonitorRange = 1;
        private const sbyte LowerMonitorRange = -1;
        /// <summary>
        ///  Метод производит шкалирование
        /// </summary>
        /// <param name="_proportionalValue">Массив регистров</param>
        /// <returns>Массив шкаллированных данных</returns>
        public double[] Scale( int[] _proportionalValue)
        {
            double[] ScaledValue = new double[_proportionalValue.Length];
            for (int i = 0; i < _proportionalValue.Length; i++)
            {
                if (_proportionalValue[i] >= 0 && _proportionalValue[i] <= GatewayFullScaleValue)
                {
                    ScaledValue[i] = ((double)_proportionalValue[i] / GatewayFullScaleValue) * ((double)UpperMonitorRange - LowerMonitorRange) + (double)LowerMonitorRange;
                }
                else
                {
                    ScaledValue[i] = 99.999;
                }
            }
            return ScaledValue;
        }

        public string LimitBrush(double _paramValue, 
                                 double _why0, double _why1, double _wly0, double _wly1, 
                                 double _ahy0, double _ahy1, double _aly0, double _aly1,
                                 double _flt0, double _flt1)
        { 
            string BackgroundBorder;
            if ((_paramValue >= _why0 && _paramValue <= _why1) || (_paramValue <= _wly0 && _paramValue >= _wly1))
                {
                  BackgroundBorder = "Yellow";
                }
            else
                {
                  BackgroundBorder = "#e5e5e5";
                }

            if ((_paramValue >= _ahy0 && _paramValue <= _ahy1) || (_paramValue <= _aly0 && _paramValue >= _aly1))
                {
                 BackgroundBorder = "Red";
                }

            if ((_paramValue > _flt0) || (_paramValue < _flt1))
                {
                  BackgroundBorder = "Blue";  
                }   
            
            return BackgroundBorder;
        }

    }
}
