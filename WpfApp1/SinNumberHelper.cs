using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class SinNumberHelper
    {
        public static string ObscureSINNumber(string sinNumber)
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

    }
}
