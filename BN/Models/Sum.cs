using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BN.Models
{
    public class Sum
    {   
        public int Number1 { get; set; }
        public int Number2 { get; set; }

        public int Calc(int in1, int in2)
        {
           // in1 = Number1;
            //in2 = Number2;
            return in1 + in2; 
        }
        


    }
}
