using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using UCH_PR_1.Entities;

namespace UCH_PR_1.ViewModels.EntititiesViewModels;

public class EventViewModel : INotifyPropertyChanged
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }
    
    public ImageSource ImageResource => new BitmapImage(new Uri($@"pack://application:,,,/UCH_PR_1;component/Resources/Images/EventImages/{Id}.png", UriKind.Absolute));

    public string CourseName { get; set; } = string.Empty;
    
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
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