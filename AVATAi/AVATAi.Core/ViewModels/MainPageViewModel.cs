using System.Collections.ObjectModel;
using System.Windows.Input;
using AVATAi.Core.Commands;
using AVATAi.Core.Navigation;
using AVATAi.Core.Services;

namespace AVATAi.Core.ViewModels;

public class MainPageViewModel : BaseViewModel
{
    private readonly StudentService _service = new();
    private readonly INavigationService _navigation;

    public ObservableCollection<StudentItemViewModel> Students { get; } = new();

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }

    private StudentItemViewModel _lastSelected;
    public StudentItemViewModel LastSelected
    {
        get => _lastSelected;
        set => SetProperty(ref _lastSelected, value);
    }

    public ICommand LoadMoreCommand { get; }
    public ICommand SelectStudentCommand { get; }

    public MainPageViewModel(INavigationService navigation)
    {
        _navigation = navigation;

        LoadMoreCommand = new ExtendedCommand(async () => await LoadMoreAsync(), () => !IsLoading);
        SelectStudentCommand = new ExtendedCommand<StudentItemViewModel>(async (vm) => await OpenDetail(vm));
        RegisterCommandDependency(nameof(IsLoading), LoadMoreCommand);
        
        Task.Run(LoadMoreAsync);
    }

    private async Task LoadMoreAsync()
    {
        if (IsLoading) return;
        IsLoading = true;

        var items = await _service.LoadStudentsAsync();
        foreach (var student in items)
        {
            Students.Add(new StudentItemViewModel(student));
        }

        IsLoading = false;
    }

    private async Task OpenDetail(StudentItemViewModel vm)
    {
        if (vm == null) return;

        if (LastSelected != null)
            LastSelected.IsSelected = false;

        vm.IsSelected = true;
        LastSelected = vm;

        await _navigation.NavigateAsync(AppScreens.StudentDetail, vm.Student);
    }
}
