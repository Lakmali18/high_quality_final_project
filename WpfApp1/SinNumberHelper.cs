using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class SinNumberHelper
    {
        public static string ObscureCreditCardNumber(string creditCardNumber)
        {
            char[] obscuredChars = creditCardNumber.ToCharArray();
            var formattedOutput = new System.Text.StringBuilder();
            for (int i = 0; i < obscuredChars.Length; i++)
            {
                obscuredChars[i] = ((i > 3) && (i < creditCardNumber.Length - 4)) ? 'X' : obscuredChars[i];
                formattedOutput.Append(obscuredChars[i]);
                if ((i + 1) % 4 == 0)
                {
                    // adds space to obscured credit number
                    formattedOutput.Append(' ');
                }
            }
            return formattedOutput.ToString();
        }

        public static bool IsSinNumberValid(string creditCardNumber)
        {
            if (creditCardNumber.Trim().Length == 16)
            {
                bool isValid = false;
                foreach (char c in creditCardNumber)
                {
                    isValid = int.TryParse(c.ToString(), out int tempInt);
                    if (!isValid)
                    {
                        break;
                    }
                }
                return isValid;
            }
            return false;
        }
    }
}
