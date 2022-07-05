using EasyModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace BN.Models
{
    internal class BNData
    {

        /// <summary>
        ///  Метод производит шкалирование параметров
        /// </summary>
        /// <param name="ProportionalValue">Массив не шкалированных параметров</param>
        /// <returns>Массив шкаллированных данных</returns>
        /// 
        public bool ValidIPV4(string IPAdress)
        {
            
            var Match = Regex.IsMatch(IPAdress, "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
            if (Match == false)
            {
                MessageBox.Show("IP адрес не соответствуте формату IPV4");
            }
            return Match;
        }
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
