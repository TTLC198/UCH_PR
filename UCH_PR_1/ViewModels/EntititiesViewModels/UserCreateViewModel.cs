using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UCH_PR_1.Entities;

namespace UCH_PR_1.ViewModels.EntititiesViewModels;

public class UserCreateViewModel : User, INotifyPropertyChanged
{
    public string FormattedPhoneNumber
    {
        get
        {
            if (PhoneNumber == null)
                return string.Empty;

            switch (PhoneNumber.Length)
            { 
                case 7:
                    return Regex.Replace(PhoneNumber, @"(\d{3})(\d{4})", "$1-$2");
                case 10:
                    return Regex.Replace(PhoneNumber, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
                case 11:
                    return Regex.Replace(PhoneNumber, @"(\d{1})(\d{3})(\d{3})(\d{4})", "$1-$2-$3-$4");
                default:
                    return PhoneNumber;
            }
        }
        set
        {
            PhoneNumber = Regex.Replace(value, @"[^\d]", "");
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}