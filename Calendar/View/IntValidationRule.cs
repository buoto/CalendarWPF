using System;
using System.Globalization;
using System.Windows.Controls;

namespace Calendar.View
{
    class IntValidationRule : ValidationRule
    {
        private int _min;
        private int _max;

        public IntValidationRule()
        {
        }

        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int intValue = 0;

            try
            {
                intValue = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal characters or " + e.Message);
            }

            if ((intValue < Min) || (intValue > Max))
            {
                return new ValidationResult(false,
                  "Please enter an intValue in the range: " + Min + " - " + Max + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }

    }
}
