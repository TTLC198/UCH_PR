using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Stylet;
using UCH_PR_1.Context;
using UCH_PR_1.Entities;
using UCH_PR_1.Models;
using UCH_PR_1.ViewModels.EntititiesViewModels;

namespace UCH_PR_1.ViewModels;

public class EventsDataGridViewModel : TabViewModelBase
{
    private readonly ApplicationContext _applicationContext;
    
    private ObservableCollection<EventViewModel> _events;

    public ObservableCollection<EventViewModel> Events
    {
        get => _events;
        private set
        {
            _events = value;
            OnPropertyChanged(nameof(Events));
        }
    }

    public EventsDataGridViewModel(ApplicationContext applicationContext) : base("Главный экран", ViewType.MainScreen)
    {
        _applicationContext = applicationContext;
    }
    
    public async void OnViewFullyLoaded()
    {
        Events = new BindableCollection<EventViewModel>(
            await _applicationContext.Events
                .Include(e => e.EventToEventTypeToCourses)
                .ThenInclude(et => et.Course)
                .Select(e => new EventViewModel()
                {
                    Id = e.Id,
                    CourseName = (e.EventToEventTypeToCourses.FirstOrDefault() ?? new EventToEventTypeToCourse()).Course.Course1,
                    Name = e.Name,
                    StartDate = e.StartDate
                })
                .ToListAsync()
        );
    }
}