using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp1
{
    class IntRule : ValidationRule
    {
        int min;
        int max;

        public int Min { get => min; set => min = value; }
        public int Max { get => max; set => max = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int numVal;

            if (value != null)
            {
                if (!int.TryParse(value.ToString(), out numVal))
                {
                    return new ValidationResult(false, "Wrong data");
                }

                if (numVal < min || numVal > max)
                {
                    return new ValidationResult(false, "Out of Range");
                }
            } else
            {
                return new ValidationResult(false, "No value provided");
            }


            return ValidationResult.ValidResult;
        }
    }
}
