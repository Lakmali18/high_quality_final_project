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
       // int min;
        int max;
        string id;

       // public int Min { get => min; set => min = value; }
        public int Max { get => max; set => max = value; }
        public string Id { get => id; set => id = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = (string)value;

            if ((value == null))
            {
                return new ValidationResult(false, "Invalid");
            }

            if (Id != "CourseName" && input.Trim().Length != Max)
            {
                return new ValidationResult(false, "Invalid");
            }

            return ValidationResult.ValidResult;
        }
    }
}
