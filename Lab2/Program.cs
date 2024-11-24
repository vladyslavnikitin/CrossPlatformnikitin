using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    static string inputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\INPUT.txt");
    static string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Files\OUTPUT.txt");

    static int Main()
    {
        try
        {
            if (!File.Exists(inputPath))
                throw new Exception($"Файл {inputPath} не знайдено.");

            var (n, k, l, queries) = ReadInput(inputPath);

            var deletionTimes = CalculateDeletionTimes(n, k);

            WriteOutput(outputPath, queries, deletionTimes);

            Console.WriteLine($"Результат записано в файл {outputPath}");
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ПОМИЛКА: {ex.Message}");
            return -1;
        }
    }

    public static (int n, int k, int l, int[] queries) ReadInput(string inputPath)
    {
        var input = File.ReadAllText(inputPath).Split();
        if (input.Length != 4)
            throw new Exception("Файл INPUT.txt має некоректну кількість аргументів");

        int n = int.Parse(input[0]); // Кількість символів
        int k = int.Parse(input[1]); // Період видалення
        int l = int.Parse(input[2]); // Кількість запросів
        int[] queries = input[3].Split(',').Select(int.Parse).ToArray(); // Запити

        if (queries.Length != l)
            throw new Exception("Кількість запросів у файлі менше, ніж вказано в параметрі L");

        return (n, k, l, queries);
    }

    public static Dictionary<int, int> CalculateDeletionTimes(int n, int k)
    {
        var deletionTimes = new Dictionary<int, int>();
        var activeList = new List<int>();
        for (int i = 1; i <= n; i++)
        {
            activeList.Add(i);
        }

        int currentTime = 1;
        while (activeList.Count >= k)
        {
            List<int> toRemove = new List<int>();
            for (int i = k - 1; i < activeList.Count; i += k)
            {
                int removedElement = activeList[i];
                if (!deletionTimes.ContainsKey(removedElement))
                {
                    deletionTimes[removedElement] = currentTime;
                }
                toRemove.Add(removedElement);
                currentTime++;
            }

            foreach (var item in toRemove)
            {
                activeList.Remove(item);
            }
        }

        // Any elements left that weren't removed, set their deletion time to 0.
        foreach (var item in activeList)
        {
            if (!deletionTimes.ContainsKey(item))
            {
                deletionTimes[item] = 0;
            }
        }

        return deletionTimes;
    }

    public static void WriteOutput(string outputPath, int[] queries, Dictionary<int, int> deletionTimes)
    {
        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            foreach (var query in queries)
            {
                if (deletionTimes.ContainsKey(query))
                {
                    writer.Write(deletionTimes[query]);
                }
                else
                {
                    writer.Write(0); // If the character was not deleted
                }
            }
        }
    }
}
