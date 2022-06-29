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

        public int[] ConToRack(string AdressIP)
        {

            ModbusClient modbusClient = new ModbusClient( AdressIP , 502);    //Ip-Address and Port of Modbus-TCP-Server
            modbusClient.Connect();                                                    //Connect to Server

            int[] readHoldingRegisters = modbusClient.ReadHoldingRegisters(0, 10);    //Read 10 Holding Registers from Server, starting with Address 1

            return readHoldingRegisters;

           /*for (int i = 0; i < readHoldingRegisters.Length; i++)
                Console.WriteLine("Value of HoldingRegister " + (i + 1) + " " + readHoldingRegisters[i].ToString());
            modbusClient.Disconnect(); */                                              //Disconnect from Server
        }
    }
}
