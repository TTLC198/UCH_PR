using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace UCH_PR_1.Utils.Validation;

public class PasswordValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value is string stringValue)
        {
            if (stringValue.Length < 6)
                return new ValidationResult(false, "Поле должно быть длиной больше или равно 6 символов.");
            
            // Проверяем наличие заглавных и строчных букв
            if (!Regex.IsMatch(stringValue, "[A-Z]") || !Regex.IsMatch(stringValue, "[a-z]"))
            {
                return new ValidationResult(false, "Пароль должен содержать как минимум одну заглавную и одну строчную букву.");
            }

            // Проверяем наличие специальных символов
            if (!Regex.IsMatch(stringValue, "[^a-zA-Z0-9]"))
            {
                return new ValidationResult(false, "Пароль должен содержать хотя бы один специальный символ.");
            }

            // Проверяем наличие цифр
            if (!Regex.IsMatch(stringValue, "[0-9]"))
            {
                return new ValidationResult(false, "Пароль должен содержать хотя бы одну цифру.");
            }

            return ValidationResult.ValidResult;
        }
        
        return new ValidationResult(false, "Поле должно быть строкой.");;
    }
}