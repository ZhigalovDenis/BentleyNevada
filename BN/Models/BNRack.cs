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

        public double[] Scale(ref int[] _proportionalValue)
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
                    ScaledValue[i] = 999.0;
                }
            }
            return ScaledValue;
        }

    }
}
