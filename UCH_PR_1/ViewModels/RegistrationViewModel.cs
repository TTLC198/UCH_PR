using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Stylet;
using UCH_PR_1.Context;
using UCH_PR_1.Entities;
using UCH_PR_1.Models;
using UCH_PR_1.ViewModels.EntititiesViewModels;

namespace UCH_PR_1.ViewModels;

public class RegistrationViewModel : TabViewModelBase
{
    private readonly ApplicationContext _applicationContext;
        
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
    
    public RegistrationViewModel(ApplicationContext applicationContext) : base("Регистрация жюри/модератора", ViewType.RegistrationView)
    {
        _applicationContext = applicationContext;
    }

    public override async void OnViewFullyLoaded()
    {
        Genders = new BindableCollection<Gender>(
            await _applicationContext.Genders.ToListAsync());
        
        Roles = new BindableCollection<Role>(
            await _applicationContext.Roles.ToListAsync());
    }
}