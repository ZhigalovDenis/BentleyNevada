using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BN.Models
{
    internal class VisualEffects
    {

        public string LimitBrush(double Parameter,
                         double WH_Y_0, double WH_Y_1, double WL_Y_0, double WL_Y_1,
                         double AH_R_0, double AH_R_1, double AL_R_0, double AL_R_1,
                         double Fault_0, double Fault_1)
        {
            string BackgroundBorder;
            if ((Parameter >= WH_Y_0 && Parameter <= WH_Y_1) || (Parameter <= WL_Y_0 && Parameter >= WL_Y_1))
            {
                BackgroundBorder = "Yellow";
            }
            else
            {
                BackgroundBorder = "#e5e5e5";
            }

            if ((Parameter >= AH_R_0 && Parameter <= AH_R_1) || (Parameter <= AL_R_0 && Parameter >= AL_R_1))
            {
                BackgroundBorder = "Red";
            }

            if ((Parameter > Fault_0) || (Parameter < Fault_1))
            {
                BackgroundBorder = "Blue";
            }

            return BackgroundBorder;
        }
    }
}
