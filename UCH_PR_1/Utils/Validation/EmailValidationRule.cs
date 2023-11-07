using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace UCH_PR_1.Utils.Validation;

public class EmailValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        return regex.IsMatch(value as string ?? string.Empty)
            ? ValidationResult.ValidResult
            : new ValidationResult(false, "Неправильный формат электронной почты.");
    }
}