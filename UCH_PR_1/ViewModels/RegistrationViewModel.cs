using System;
using System.Collections.ObjectModel;
using System.Windows.Automation.Peers;
using Microsoft.EntityFrameworkCore;
using Stylet;
using UCH_PR_1.Context;
using UCH_PR_1.Entities;
using UCH_PR_1.Models;
using UCH_PR_1.ViewModels.EntititiesViewModels;
using UCH_PR_1.ViewModels.Framework;

namespace UCH_PR_1.ViewModels;

public class RegistrationViewModel : TabViewModelBase
{
    private readonly ApplicationContext _applicationContext;
    private readonly DialogManager _dialogManager;
    private readonly IViewModelFactory _viewModelFactory;
        
    private UserCreateViewModel _user = new UserCreateViewModel();

    public UserCreateViewModel User
    {
        get => _user;
        set
        {
            _user = value;
            OnPropertyChanged(nameof(User));
        }
    }

    private ObservableCollection<Gender> _genders;

    public ObservableCollection<Gender> Genders
    {
        get => _genders;
        set
        {
            _genders = value;
            OnPropertyChanged(nameof(Genders));
        }
    }
    
    private ObservableCollection<Role> _roles;

    public ObservableCollection<Role> Roles
    {
        get => _roles;
        set
        {
            _roles = value;
            OnPropertyChanged(nameof(Roles));
        }
    }

    private Gender _selectedGender;

    public Gender SelectedGender
    {
        get => _selectedGender;
        set
        {
            _selectedGender = value;
            OnPropertyChanged(nameof(SelectedGender));
        }
    }
    
    private Role _selectedRole;

    public Role SelectedRole
    {
        get => _selectedRole;
        set
        {
            _selectedRole = value;
            OnPropertyChanged(nameof(SelectedRole));
        }
    }
    
    private ObservableCollection<Event> _events;

    public ObservableCollection<Event> Events
    {
        get => _events;
        set
        {
            _events = value;
            OnPropertyChanged(nameof(Events));
        }
    }
    
    private ObservableCollection<Course> _courses;

    public ObservableCollection<Course> Courses
    {
        get => _courses;
        set
        {
            _courses = value;
            OnPropertyChanged(nameof(Courses));
        }
    }

    private Event _selectedEvent;

    public Event SelectedEvent
    {
        get => _selectedEvent;
        set
        {
            _selectedEvent = value;
            OnPropertyChanged(nameof(SelectedEvent));
        }
    }
    
    private Course _selectedCourse;

    public Course SelectedCourse
    {
        get => _selectedCourse;
        set
        {
            _selectedCourse = value;
            OnPropertyChanged(nameof(SelectedCourse));
        }
    }

    private bool _isPasswordVisible;
    
    public bool IsPasswordVisible
    {
        get => _isPasswordVisible;
        set
        {
            _isPasswordVisible = value;
            OnPropertyChanged(nameof(IsPasswordVisible));
        }
    }
    
    private bool _isAttachToEvent;
    
    public bool IsAttachToEvent
    {
        get => _isAttachToEvent;
        set
        {
            _isAttachToEvent = value;
            OnPropertyChanged(nameof(IsAttachToEvent));
        }
    }
    
    public RegistrationViewModel(ApplicationContext applicationContext, DialogManager dialogManager, IViewModelFactory viewModelFactory) : base("Регистрация жюри/модератора", ViewType.RegistrationView)
    {
        _applicationContext = applicationContext;
        _dialogManager = dialogManager;
        _viewModelFactory = viewModelFactory;
    }

    public override async void OnViewFullyLoaded()
    {
        Genders = new BindableCollection<Gender>(
            await _applicationContext.Genders.ToListAsync());
        
        Roles = new BindableCollection<Role>(
            await _applicationContext.Roles.ToListAsync());
        
        Events = new BindableCollection<Event>(
            await _applicationContext.Events.ToListAsync());
        
        Courses = new BindableCollection<Course>(
            await _applicationContext.Courses.ToListAsync());

        var lastId = await _applicationContext
            .Users.MaxAsync(u => u.UserId);

        User.Id = lastId + 1;
    }

    public async void SaveUser()
    {
        MessageBoxViewModel? messageBoxDialog;
        if (!User.ConfirmPassword.Equals(User.Password))
        {
            messageBoxDialog = _viewModelFactory.CreateMessageBoxViewModel(
                title: "Пароли не совпадают",
                message: $@"Пожалуйста, повторите попытку".Trim(),
                okButtonText: "OK",
                cancelButtonText: null
            );
            await _dialogManager.ShowDialogAsync(messageBoxDialog);
            return;
        }
        
        User.Genders.Add(SelectedGender);
        User.Roles.Add(SelectedRole);
        User.Courses.Add(SelectedCourse);
        if (IsAttachToEvent)
            User.Events.Add(SelectedEvent);

        User.UserId = User.Id;
        
        await _applicationContext.AddAsync(User);
        await _applicationContext.SaveChangesAsync();
        CurrentUser = User;
        
        messageBoxDialog = _viewModelFactory.CreateMessageBoxViewModel(
            title: "Операция выполнена успешно!",
            message: $@"Пользователь создан!".Trim(),
            okButtonText: "OK",
            cancelButtonText: null
        );
        await _dialogManager.ShowDialogAsync(messageBoxDialog);
    }
}