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
        bool intOnly = false;

       // public int Min { get => min; set => min = value; }
        public int Max { get => max; set => max = value; }

        public bool IntOnly { get => intOnly; set => intOnly = value; }
        public string Id { get => id; set => id = value; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = (string)value;

            if ((value == null))
            {
                return new ValidationResult(false, "Invalid");
            }

            if (IntOnly && !StringRule.IsNumericString(input))
            {
                return new ValidationResult(false, "Invalid");
            }

            if (Id != "CourseName" && input.Trim().Length != Max)
            {
                return new ValidationResult(false, "Invalid");
            }

            return ValidationResult.ValidResult;
        }

        public static bool IsNumericString(string str)
        {
            bool isValid = true;
            foreach (char c in str)
            {
                isValid = int.TryParse(c.ToString(), out int tempInt);
                if (!isValid)
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }
    }
}
