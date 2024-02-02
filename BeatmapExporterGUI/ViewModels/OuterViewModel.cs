﻿using BeatmapExporterGUI.ViewModels.HomePage;
using BeatmapExporterGUI.ViewModels.List;
using BeatmapExporterGUI.ViewModels.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace BeatmapExporterGUI.ViewModels;

/// <summary>
/// Outer, "wrapping" ViewModel. Always visible, maintains the operation that is displayed to the user within
/// </summary>
public partial class OuterViewModel : ViewModelBase
{
    public OuterViewModel()
    {
        Home = new HomeViewModel();
        MenuRow = new MenuRowViewModel(this);

        CurrentOperation = Home;
    }

    [ObservableProperty]
    private ViewModelBase _CurrentOperation;

    /// <summary>
    /// The home page view model instance
    /// </summary>
    public HomeViewModel Home { get; }

    /// <summary>
    /// The menu button row view model instance
    /// </summary>
    public MenuRowViewModel MenuRow { get; }

    public void NavigateHome() => CurrentOperation = Home;

    public void ListBeatmaps() => CurrentOperation = new BeatmapListViewModel();

    public void ListCollections() => CurrentOperation = new CollectionListViewModel();

    public void EditFilters() => CurrentOperation = new ExportConfigViewModel(this);

    public async Task Export(CancellationToken token)
    {
        var export = new ExportViewModel(this);
        CurrentOperation = export;
        await export.StartExport(token);
    }

    public bool IsExporting => (CurrentOperation as ExportViewModel)?.ActiveExport ?? false;
}
