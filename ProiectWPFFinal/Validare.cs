using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace ProiectWPFFinal
{
    //validator pentru camp required
    public class StringNotEmpty : ValidationRule
    {
        
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureinfo)
        {
            string aString = value.ToString();
            if (aString == "")
                return new ValidationResult(false, "String cannot be empty");
            return new ValidationResult(true, null);
        }
    }

    public class StringMinLengthValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureinfo)
        {
            string aString = value.ToString();
            if (aString.Length < 3)
                return new ValidationResult(false, "String must have at least 3 characters!");
            return new ValidationResult(true, null);
        }
    }
    public class FirstCapital : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureinfo)
        {
            string aString = value.ToString();
            try
            {
                if (!Char.IsUpper(aString[0]) & aString.Length != 0)
                    return new ValidationResult(false, "Prima litera trebuie sa fie UPPERCASE!");
            }
            catch(Exception ex)
            {
                return new ValidationResult(false, "String empty!");
            }
            return new ValidationResult(true, null);
        }
    }
    public class EmailValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureinfo)
        {
            string aString = value.ToString();
            try
            {
                if (!aString.Contains("@")||aString.Contains(" "))
                    return new ValidationResult(false, "Mail scris gresit!");
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, "Mail scris incorect!");
            }
            return new ValidationResult(true, null);
        }
    }

}
