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

        /// <summary>
        ///  Метод производит шкалирование параметров
        /// </summary>
        /// <param name="ProportionalValue">Массив не шкалированных параметров</param>
        /// <returns>Массив шкаллированных данных</returns>
        public double[] Scale(int[] ProportionalValue, ushort GatewayFullScaleValue, short LowerMonitorRange, short UpperMonitorRange, double FaultReplace)
        {
            double[] ScaledValue = new double[ProportionalValue.Length];
            for (int i = 0; i < ProportionalValue.Length; i++)
            {
                if (ProportionalValue[i] >= 0 && ProportionalValue[i] <= GatewayFullScaleValue)
                {
                    ScaledValue[i] = ((double)ProportionalValue[i] / GatewayFullScaleValue) * ((double)UpperMonitorRange - LowerMonitorRange) + (double)LowerMonitorRange;
                }
                else
                {
                    ScaledValue[i] = FaultReplace;
                }
            }
            return ScaledValue;
        }

    }
}
