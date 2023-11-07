using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using UCH_PR_1.Models;

namespace UCH_PR_1.ViewModels;

public class OrganisatorViewModel : TabViewModelBase
{
    private ImageSource _imageResource;
    
    public ImageSource ImageResource
    {
        get => _imageResource;
        set
        {
            _imageResource = value;
            OnPropertyChanged(nameof(ImageResource));
        }
    }

    private string _timeString;

    public string TimeString
    {
        get => _timeString;
        set
        {
            _timeString = value;
            OnPropertyChanged(nameof(TimeString));
        }
    }

    private string _genderWelcomeMessage;

    public string GenderWelcomeMessage
    {
        get => _genderWelcomeMessage;
        set
        {
            _genderWelcomeMessage = value;
            OnPropertyChanged(nameof(GenderWelcomeMessage));
        }
    }
    
    public OrganisatorViewModel() : base("Окно Организатора", ViewType.OrganisatorView)
    {
        SetTimeWelcomeMessage();
        SetGenderWelcomeMessage();
    }

    public override void OnViewFullyLoaded()
    {
        SetGenderWelcomeMessage();
        SetTimeWelcomeMessage();
        SetUserImage();
    }

    private void SetTimeWelcomeMessage()
    {
        var currentTime = DateTime.Now;
        TimeString = currentTime.TimeOfDay switch
        {
            var f when f > new TimeSpan(9, 0, 0) && f < new TimeSpan(11, 0, 0) => "Доброе утро!",
            var f when f > new TimeSpan(11, 1, 0) && f < new TimeSpan(18, 0, 0) => "Добрый день!",
            var f when f > new TimeSpan(18, 1, 0) && f < new TimeSpan(24, 0, 0) => "Добрый вечер!",
            _ => "Доброй ночи!"
        };
    }

    private void SetGenderWelcomeMessage()
    {
        GenderWelcomeMessage = CurrentUser.Genders.FirstOrDefault()?.Gender1 switch
        {
            "мужской" => "Mr",
            "женский" => "Mrs",
            _ => ""
        };
    }

    private void SetUserImage()
    {
        if (!string.IsNullOrEmpty(CurrentUser.Photo))
            ImageResource = new BitmapImage(new Uri(
                $@"pack://application:,,,/UCH_PR_1;component/Resources/Images/OrganisatorImages/{CurrentUser.Photo}",
                UriKind.Absolute));
    }
}