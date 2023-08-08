using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1
{
    class StringRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string sinNumber = (string)value;

            if ((value == null) || !SinNumberHelper.IsSinNumberValid(sinNumber))
            {
                return new ValidationResult(false, "Invalid");
            }

            return ValidationResult.ValidResult;
        }
    }
}
