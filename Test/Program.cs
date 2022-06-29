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

            
                ModbusClient modbusClient = new ModbusClient("127.0.0.1", 502);    //Ip-Address and Port of Modbus-TCP-Server
                modbusClient.Connect();                                                    //Connect to Server
               
                int[] readHoldingRegisters = modbusClient.ReadHoldingRegisters(0, 3);    //Read 10 Holding Registers from Server, starting with Address 1

                

                for (int i = 0; i < readHoldingRegisters.Length; i++)
                    Console.WriteLine("Value of HoldingRegister " + (i + 1) + " " + readHoldingRegisters[i].ToString());
                modbusClient.Disconnect();                                                //Disconnect from Server
                Console.Write("Press any key to continue . . . ");
                Console.ReadKey(true);
            
        }
    }
}
