using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BN.Models
{
    internal class VisualEffects
    {
        /// <summary>
        /// Изменяет цвет Border у lable
        /// </summary>
        /// <param name="Parameter"></param>
        /// <param name="WH_Y_0"></param>
        /// <param name="WH_Y_1"></param>
        /// <param name="WL_Y_0"></param>
        /// <param name="WL_Y_1"></param>
        /// <param name="AH_R_0"></param>
        /// <param name="AH_R_1"></param>
        /// <param name="AL_R_0"></param>
        /// <param name="AL_R_1"></param>
        /// <param name="Fault_0"></param>
        /// <param name="Fault_1"></param>
        /// <returns></returns>
        public string[] LimitBrush_0(double [] Parameter,
                         double WH_Y_0, double WH_Y_1, double WL_Y_0, double WL_Y_1,
                         double AH_R_0, double AH_R_1, double AL_R_0, double AL_R_1,
                         double Fault_0, double Fault_1)
        {
            string [] BackgroundBorder = new string[Parameter.Length];

            for (int i = 0; i < Parameter.Length; i++)
            {

                if ((Parameter[i] >= WH_Y_0 && Parameter[i] <= WH_Y_1) || (Parameter[i] <= WL_Y_0 && Parameter[i] >= WL_Y_1))
                {
                    BackgroundBorder[i] = "Yellow";
                }
                else
                {
                    BackgroundBorder[i] = "#e5e5e5";
                }

                if ((Parameter[i] >= AH_R_0 && Parameter[i] <= AH_R_1) || (Parameter[i] <= AL_R_0 && Parameter[i] >= AL_R_1))
                {
                    BackgroundBorder[i] = "Red";
                }

                if ((Parameter[i] > Fault_0) || (Parameter[i] < Fault_1))
                {
                    BackgroundBorder[i] = "Blue";
                }

            }
            return BackgroundBorder;
        }
        /// <summary>
        ///  Изменяет цвет Border у lable
        /// </summary>
        /// <param name="Parameter"></param>
        /// <param name="WH_Y_0"></param>
        /// <param name="WH_Y_1"></param>
        /// <param name="AH_R_0"></param>
        /// <param name="AH_R_1"></param>
        /// <param name="Fault_0"></param>
        /// <param name="Fault_1"></param>
        /// <returns></returns>
        public string[] LimitBrush_1(double[] Parameter,
                         double WH_Y_0, double WH_Y_1, 
                         double AH_R_0, double AH_R_1, 
                         double Fault_0, double Fault_1)
        {
            string[] BackgroundBorder = new string[Parameter.Length];

            for (int i = 0; i < Parameter.Length; i++)
            {

                if (Parameter[i] >= WH_Y_0 && Parameter[i] <= WH_Y_1)
                {
                    BackgroundBorder[i] = "Yellow";
                }
                else
                {
                    BackgroundBorder[i] = "#e5e5e5";
                }

                if (Parameter[i] >= AH_R_0 && Parameter[i] <= AH_R_1)
                {
                    BackgroundBorder[i] = "Red";
                }

                if ((Parameter[i] > Fault_0) || (Parameter[i] < Fault_1))
                {
                    BackgroundBorder[i] = "Blue";
                }
            }
            return BackgroundBorder;
        }
    }
}
