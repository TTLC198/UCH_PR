using System.Windows;
using System.Windows.Controls;
using EasyCaptcha.Wpf;

namespace UCH_PR_1.Views;

public partial class AuthorizationView : UserControl
{
    public AuthorizationView()
    {
        InitializeComponent();
    }

    private void RefreshCaptcha_OnClick(object sender, RoutedEventArgs e)
    {
        MyCaptcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 4);
    }

    private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
    {
        MyCaptcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 4);
    }
}