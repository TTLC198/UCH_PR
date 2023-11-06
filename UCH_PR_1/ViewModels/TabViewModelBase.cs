using Stylet;
using UCH_PR_1.Entities;
using UCH_PR_1.Models;

namespace UCH_PR_1.ViewModels;

public abstract class TabViewModelBase : PropertyChangedBase, ITabViewModel
{
    public string Title { get; }
    
    public ViewType ViewType { get; }

    public User CurrentUser { get; set; } = new User();
    public virtual void OnViewFullyLoaded()
    {
    }

    protected TabViewModelBase(string title, ViewType viewType)
    {
        Title = title;
        ViewType = viewType;
    }
}