using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MaterialDesignThemes.Wpf;
using Stylet;

namespace UCH_PR_1.ViewModels.Framework;

#pragma warning disable CA1416
public class DialogManager : IDisposable
{
    private readonly IViewManager _viewManager;
    private readonly Dictionary<Type, UIElement> _dialogScreenViewCache = new();
    private readonly SemaphoreSlim _dialogLock = new(1, 2);

    public DialogManager(IViewManager viewManager)
    {
        _viewManager = viewManager;
    }

    public UIElement GetViewForDialogScreen<T>(DialogScreen<T> dialogScreen)
    {
        var dialogScreenType = dialogScreen.GetType();
        if (_dialogScreenViewCache.TryGetValue(dialogScreenType, out var cachedView))
        {
            _viewManager.BindViewToModel(cachedView, dialogScreen);
            return cachedView;
        }
        else
        {
            var view = _viewManager.CreateAndBindViewForModelIfNecessary(dialogScreen);
            view.Arrange(new Rect(0, 0, 500, 500));

            return _dialogScreenViewCache[dialogScreenType] = view;
        }
    }

    public async Task<T?> ShowDialogAsync<T>(DialogScreen<T> dialogScreen)
    {
        var view = GetViewForDialogScreen(dialogScreen);

        void OnDialogOpened(object? openSender, DialogOpenedEventArgs openArgs)
        {
            void OnScreenClosed(object? closeSender, EventArgs args)
            {
                try
                {
                    openArgs.Session.Close();
                }
                catch
                {
                    // ignored
                }

                dialogScreen.Closed -= OnScreenClosed;
            }
            
            dialogScreen.Closed += OnScreenClosed;
        }
        
        if (_dialogScreenViewCache.Count > 1)
        {
            DialogHost.CloseDialogCommand.Execute(null,null);
            dialogScreen.Close(dialogScreen.DialogResult!);
        }

        await _dialogLock.WaitAsync();
        try
        {
            await DialogHost.Show(view, OnDialogOpened);
            return dialogScreen.DialogResult;
        }
        finally
        {
            _dialogLock.Release();
        }
    }

    public void Dispose()
    {
        _dialogLock.Dispose();
    }
}