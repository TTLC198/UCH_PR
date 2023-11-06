using Microsoft.EntityFrameworkCore;
using Stylet;
using StyletIoC;
using UCH_PR_1.Context;
using UCH_PR_1.ViewModels;
using UCH_PR_1.ViewModels.Framework;

namespace UCH_PR_1.Utils;

public class Bootstrapper : Bootstrapper<MainWindowViewModel>
{
    
    private T GetInstance<T>() => (T) base.GetInstance(typeof(T));

    protected override void ConfigureIoC(IStyletIoCBuilder builder)
    {
        base.ConfigureIoC(builder);
        
        builder.Bind<IViewModelFactory>().ToAbstractFactory();
        
        builder.Bind<DialogManager>().ToSelf().InSingletonScope();
        
        builder.Bind<ApplicationContext>()
            .ToSelf()
            .InSingletonScope();
        
        builder.Bind<ITabViewModel>().ToAllImplementations().InSingletonScope();
        
        builder.Bind<MainWindowViewModel>().ToSelf().InSingletonScope();
        builder.Bind<AuthorizationViewModel>().ToSelf().InSingletonScope();
        builder.Bind<OrganisatorViewModel>().ToSelf().InSingletonScope();
        builder.Bind<RegistrationViewModel>().ToSelf().InSingletonScope();
        builder.Bind<JuryViewModel>().ToSelf().InSingletonScope();
        builder.Bind<ModeratorViewModel>().ToSelf().InSingletonScope();
        builder.Bind<ParticipantViewModel>().ToSelf().InSingletonScope();
    }
}