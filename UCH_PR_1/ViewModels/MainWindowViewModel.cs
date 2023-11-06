using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using UCH_PR_1.Entities;
using UCH_PR_1.Models;
using UCH_PR_1.ViewModels.Framework;

namespace UCH_PR_1.ViewModels;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly DialogManager _dialogManager;
    private readonly IViewModelFactory _viewModelFactory;
    
    public IReadOnlyList<ITabViewModel> Tabs { get; }

    public ITabViewModel? ActiveTab { get; private set; }

    private User _user;

    public User User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged();
        }
    }

    private string _loginButtonText;

    public string LoginButtonText
    {
        get => _loginButtonText;
        set
        {
            _loginButtonText = value;
            OnPropertyChanged();
        }
    }
    
    public MainWindowViewModel(DialogManager dialogManager, IViewModelFactory viewModelFactory, IReadOnlyList<ITabViewModel> tabs)
    {
        _dialogManager = dialogManager;
        _viewModelFactory = viewModelFactory;
        Tabs = tabs;
    }

    public async void OpenAuthWindow()
    {
        if (User is not null)
        {
            User = new User();
            SetTabsToDefault();
        }
        var result = await _dialogManager.ShowDialogAsync(_viewModelFactory.CreateAuthorizationViewModel());
        if (result is not null)
        {
            User = result;
            var viewType = User.Roles.FirstOrDefault()?.Role1 switch
            {
                "модератор" => ViewType.ModeratorView,
                "организатор" => ViewType.OrganisatorView,
                "жюри" => ViewType.JuryView,
                _ => ViewType.ParticipantView
            };
            ActiveTab = Tabs.FirstOrDefault(t => t.ViewType == viewType);
            if (ActiveTab != null)
            {
                ActiveTab.CurrentUser = User;
                ActiveTab.OnViewFullyLoaded();
            }
            LoginButtonText = "Выйти";
        }
    }

    public async void OnViewFullyLoaded()
    {
        SetTabsToDefault();
    }

    public void SetTabsToDefault()
    {
        ActiveTab = Tabs.FirstOrDefault(t => t.ViewType == ViewType.MainScreen);
        LoginButtonText = "Авторизация";
    }

    public async void SetRegistrationTab()
    {
        ActiveTab = Tabs.FirstOrDefault(t => t.ViewType == ViewType.RegistrationView);
        ActiveTab?.OnViewFullyLoaded();
        LoginButtonText = "Назад";
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