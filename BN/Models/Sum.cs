﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BN.Models
{
    internal class Sum
    {   
        public int Calculate(string first, string second)
        {
            return Convert.ToInt32(first) + Convert.ToInt32(second);
        }

    }
}
