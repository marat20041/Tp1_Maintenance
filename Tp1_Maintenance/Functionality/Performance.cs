using SchoolManager;
public static class Performance
{
    public static async Task showPerformance()
    {
        double average = await Task.Run(() => Student.averageGrade(Student.Students));
        Console.WriteLine($"The student average performance is: {average}");
    }
}