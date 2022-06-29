using EasyModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //ushort _ProportionalValue = 500;
            //ushort GatewayFullScaleValue = 16383;
            //byte UpperMonitorRange = 1;
            //sbyte LowerMonitorRange = -1;
            //double result;

            ushort _ProportionalValue = 500;
            ushort GatewayFullScaleValue = 16383;
            double UpperMonitorRange = 1;
            double LowerMonitorRange = -1;
            double result;

            int a = 500, b = 23;
            double c = (double) a / b;

            result =  ((double)_ProportionalValue / GatewayFullScaleValue) * ((double) UpperMonitorRange - LowerMonitorRange) + (double) LowerMonitorRange;
            Console.WriteLine(result);
        }
    }
}
