using EasyModbus;
using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace BN.Models
{
    internal class BNRack
    {
        ModbusClient modbusClient = new ModbusClient();

        /// <summary>
        /// Валидация IP адреса.
        /// </summary>
        /// <param name="IPAdress"></param>
        /// <returns></returns>
        public bool ValidIPV4(string IPAdress)
        {  
            var Match = Regex.IsMatch(IPAdress, "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
            if (Match == false)
            {
                MessageBox.Show("IP адрес не соответствуте формату IPV4");
            }
            return Match;
        }

        /// <summary>
        /// Подключение к Rack 
        /// </summary>
        /// <param name="IPAdress"></param>
        /// <param name="Port"></param>
        /// <returns></returns>
        public bool Connection(string IPAdress, int Port)
        {
            bool IsEnable = true;
            modbusClient.Port = Port;
            modbusClient.IPAddress = IPAdress;
            try
            {
                modbusClient.Connect();
            }
            catch (Exception)
            {
                MessageBox.Show("Устройство не отвечает");
                IsEnable = false;
            }
            return IsEnable;
        }
        /// <summary>
        /// Повторное подключение к Rack после потери связи.
        /// </summary>
        /// <param name="IPAdress"></param>
        /// <param name="Port"></param>
        public bool Reconnection(string IPAdress, int Port)
        {
            bool IsEnable = true;
            modbusClient.Port = Port;
            modbusClient.IPAddress = IPAdress;
            try
            {
                modbusClient.Connect();
            }
            catch (Exception)
            {
                IsEnable = false;
            }
            return IsEnable;
        }

        /// <summary>
        /// Отключение от Rack
        /// </summary>        
        public void Disconnection()
        {
                modbusClient.Disconnect();  
        }
        /// <summary>
        /// Чтение данных из Rack
        /// </summary>
        /// <param name="StartingAdress"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        public int[] ReadData(int StartingAdress, int Quantity)
        {
            try
            {
                int[] ReturnData = modbusClient.ReadHoldingRegisters(StartingAdress, Quantity);
                return ReturnData;
            }
            catch (Exception)
            {
              
                int[] CleanArray = new int[101];
                return CleanArray;
            }
        }

        /// <summary>
        /// Шкалирование величин
        /// </summary>
        /// <param name="ProportionalValue"></param>
        /// <param name="GatewayFullScaleValue"></param>
        /// <param name="LowerMonitorRange"></param>
        /// <param name="UpperMonitorRange"></param>
        /// <param name="FaultReplace"></param>
        /// <returns></returns>
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
