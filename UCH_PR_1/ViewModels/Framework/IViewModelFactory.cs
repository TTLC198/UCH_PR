namespace UCH_PR_1.ViewModels.Framework;

public interface IViewModelFactory
{
    AuthorizationViewModel CreateAuthorizationViewModel();
    
    OrganisatorViewModel CreateOrganisatorViewModel();
    
    RegistrationViewModel CreateRegistrationViewModel();
    
    MessageBoxViewModel CreateMessageBoxViewModel();
}