using SchoolManager;
public static class Performance
{
    public static async Task showPerformance()
    {
        double average = Student.AverageGrade();
        Console.WriteLine(ReferenceText.Format("StudentAverage", new Dictionary<string, string>
{
    { "average", average.ToString() }
}));
    }
}