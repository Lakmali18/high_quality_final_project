using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class SinNumberHelper
    {
        public static string ObscureCreditCardNumber(string sinNumber)
        {
            char[] obscuredChars = sinNumber.ToCharArray();
            var formattedOutput = new System.Text.StringBuilder();
            for (int i = 0; i < obscuredChars.Length; i++)
            {
                obscuredChars[i] = (i < sinNumber.Length - 4) ? 'X' : obscuredChars[i];
                formattedOutput.Append(obscuredChars[i]);
              
            }
            return formattedOutput.ToString();
        }

        //public static bool IsSinNumberValid(string sinNo)
        //{
        //    if (sinNo.Trim().Length == 16)
        //    {
        //        bool isValid = false;
        //        foreach (char c in sinNo)
        //        {
        //            isValid = int.TryParse(c.ToString(), out int tempInt);
        //            if (!isValid)
        //            {
        //                break;
        //            }
        //        }
        //        return isValid;
        //    }
        //    return false;
        //}
    }
}
