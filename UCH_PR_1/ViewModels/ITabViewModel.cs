using UCH_PR_1.Entities;
using UCH_PR_1.Models;

namespace UCH_PR_1.ViewModels;

public interface ITabViewModel
{
    public string Title { get; }
    
    public ViewType ViewType { get; }
    
    public User CurrentUser { get; set; }

    public void OnViewFullyLoaded();
}