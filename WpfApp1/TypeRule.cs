using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1
{
    class TypeRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ((int)value < 0)
            {
                return new ValidationResult(false, "No selection");
            }

            return ValidationResult.ValidResult;
        }
    }
}
