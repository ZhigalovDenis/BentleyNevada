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
       // public ushort ProportionalValue { get; set; } // Это та переменная которую получаем из устройства

        private  ushort GatewayFullScaleValue = 16383;
        private byte UpperMonitorRange = 1;
        private sbyte LowerMonitorRange = -1;

        public double[] Scale(int[] _proportionalValue)
        {
            double[] result = new double[_proportionalValue.Length];
            for (int i = 0; i < _proportionalValue.Length; i++)
            {
                if (_proportionalValue[i] >= 0 && _proportionalValue[i] <= GatewayFullScaleValue)
                {
                    result[i] = ((double)_proportionalValue[i] / GatewayFullScaleValue) * ((double)UpperMonitorRange - LowerMonitorRange) + (double)LowerMonitorRange;
                }
                else
                {
                    result[i] = 999.0;
                }
            }

            return result;
        }

    }
}
