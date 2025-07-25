using AVATAi.Core.Models;

namespace AVATAi.Core.Services;

public class StudentService
{
    private int _current = 0;
    private readonly int _batchSize = 20;

    public async Task<List<Student>> LoadStudentsAsync()
    {
        await Task.Delay(200); // simulate network delay

        var list = new List<Student>();
        for (int i = 0; i < _batchSize; i++)
        {
            _current++;
            list.Add(new Student
            {
                Id = _current,
                Name = $"Student {_current}",
                Info = $"Extended information about Student {_current}"
            });
        }
        return list;
    }
}