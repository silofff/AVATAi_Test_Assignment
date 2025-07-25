using AVATAi.Core.Models;

namespace AVATAi.Core.ViewModels;

public class StudentItemViewModel(Student student) : BaseViewModel
{
    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    public int Id => Student.Id;
    public string Name => Student.Name;
    public string Info => Student.Info;

    public Student Student { get; } = student;
}