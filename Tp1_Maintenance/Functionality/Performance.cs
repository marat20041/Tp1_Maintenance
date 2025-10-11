using SchoolManager;

/// <summary>
/// Fournit des méthodes pour afficher les performances des étudiants.
/// </summary>
public static class Performance
{

    /// <summary>
    /// Calcule et affiche la moyenne des notes des étudiants de manière asynchrone.
    /// </summary>
    public static async Task ShowPerformance()
    {
        await Task.Run(() =>
        {
            double average = Student.AverageGrade();
            Console.WriteLine(ReferenceText.Format("StudentAverage", new Dictionary<string, string>
            {
                { "average", average.ToString() }
            }));
        });
    }
}
