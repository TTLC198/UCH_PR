using System.Collections.Generic;
using System.Collections.ObjectModel;
using UCH_PR_1.ViewModels.EntititiesViewModels;

namespace UCH_PR_1.ViewModels;

public class MainWindowViewModel
{
    public ObservableCollection<EventViewModel> Events { get; set; }
    
    
}