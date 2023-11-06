using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using EasyCaptcha.Wpf;
using Microsoft.EntityFrameworkCore;
using UCH_PR_1.Context;
using UCH_PR_1.Entities;
using UCH_PR_1.ViewModels.EntititiesViewModels;
using UCH_PR_1.ViewModels.Framework;

namespace UCH_PR_1.ViewModels;

public class AuthorizationViewModel : DialogScreen<User>, INotifyPropertyChanged
{
    private readonly ApplicationContext _applicationContext;
    private readonly DialogManager _dialogManager;
    private readonly IViewModelFactory _viewModelFactory;

    public UserLoginViewModel UserLoginViewModel { get; set; } = new UserLoginViewModel();

    private string _captchaText;

    public string CaptchaText
    {
        get => _captchaText;
        set
        {
            _captchaText = value;
            OnPropertyChanged(nameof(CaptchaText));
        }
    }
    
    /*public static readonly DependencyProperty CanExecuteProperty =
        DependencyProperty.Register(
            nameof(CanExecute),
            typeof(bool),
            typeof(AuthorizationViewModel),
            new PropertyMetadata(true));
    
    public bool CanExecute
    {
        get => (bool)GetValue(CanExecuteProperty);
        set => SetValue(CanExecuteProperty, value);
    }*/

    private bool _canExecute = true;
    
    public bool CanExecute
    {
        get => _canExecute;
        set
        {
            _canExecute = value;
            OnPropertyChanged(nameof(CanExecute));
        }
    }

    public AuthorizationViewModel(ApplicationContext applicationContext, DialogManager dialogManager, IViewModelFactory viewModelFactory)
    {
        _applicationContext = applicationContext;
        _dialogManager = dialogManager;
        _viewModelFactory = viewModelFactory;
    }
    
    
    public async void OnViewFullyLoaded()
    {
    }

    public async void Login()
    {
        try
        {
            if (!CaptchaText.ToUpper().Equals(UserLoginViewModel.Captcha?.ToUpper()))
            {
                var messageBoxDialog = _viewModelFactory.CreateMessageBoxViewModel(
                    title: "Введена неверная капча",
                    message: $@"Пожалуйста, повторите попытку через 10 секунд".Trim(),
                    okButtonText: "OK",
                    cancelButtonText: null
                );
                await _dialogManager.ShowDialogAsync(messageBoxDialog);

                await Task.Run(async () =>
                {
                    CanExecute = false;
                    await Task.Delay(new TimeSpan(0, 0, 10));
                    CanExecute = true;
                });
                
                return;
            }

            var user = await _applicationContext.Users
                .Include(u => u.Roles)
                .Include(u => u.Genders)
                .FirstOrDefaultAsync(u => u.UserId == UserLoginViewModel.UserId && u.Password == UserLoginViewModel.Password);

            if (user is null)
            {
                var messageBoxDialog = _viewModelFactory.CreateMessageBoxViewModel(
                    title: "Пользователь не найден",
                    message: $@"Пожалуйста, повторите попытку".Trim(),
                    okButtonText: "OK",
                    cancelButtonText: null
                );
                await _dialogManager.ShowDialogAsync(messageBoxDialog);
                return;
            }
            
            Close(user);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    public void CloseView()
    {
        Close(null!);
    }
}