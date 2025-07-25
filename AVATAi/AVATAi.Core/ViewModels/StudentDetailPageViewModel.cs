using AVATAi.Core.Models;

namespace AVATAi.Core.ViewModels;

public class StudentDetailPageViewModel : BaseViewModel, IInitialize<Student>
{
    private Student _student;
    public Student Student
    {
        get => _student;
        set
        {
            if (SetProperty(ref _student, value))
            {
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Info));
            }
        }
    }

    public string Name => Student?.Name;
    public string Info => Student?.Info;

    public void Initialize(Student parameter)
    {
        Student = parameter;
    }
}