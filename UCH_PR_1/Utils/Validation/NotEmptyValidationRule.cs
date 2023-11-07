using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace UCH_PR_1.Utils.Validation;

public class NotEmptyValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        return string.IsNullOrWhiteSpace((value ?? "").ToString())
            ? new ValidationResult(false, "Поле не должно быть пустым.")
            : ValidationResult.ValidResult;
    }
}