using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Lab3.Tests")]

public class Program
{
    private static readonly string InputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Files", "INPUT.txt");
    private static readonly string OutputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Files", "OUTPUT.txt");

    public static int Main()
    {
        try
        {
            var input = File.ReadAllLines(InputPath).ToList();

            if (!IsValidInput(input))
                throw new FormatException("Файл INPUT.txt повинен містити 8 рядків по 8 символів W або B.");

            var result = CalculateBuilders(input);
            File.WriteAllText(OutputPath, result.ToString());

            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"ПОМИЛКА: {ex.Message}");
            return 1;
        }
    }

    private static bool IsValidInput(List<string> input)
    {
        return input.Count == 8 && input.All(line => line.Length == 8 && line.All(c => c == 'W' || c == 'B'));
    }

    internal static int CalculateBuilders(List<string> input)
    {
        int errorsPattern1 = 0; // Помилки для патерну, що починається з 'W'.
        int errorsPattern2 = 0; // Помилки для патерну, що починається з 'B'.

        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                char expectedPattern1 = ((row + col) % 2 == 0) ? 'W' : 'B';
                char expectedPattern2 = ((row + col) % 2 == 0) ? 'B' : 'W';

                if (input[row][col] != expectedPattern1) errorsPattern1++;
                if (input[row][col] != expectedPattern2) errorsPattern2++;
            }
        }

        return Math.Min(errorsPattern1, errorsPattern2);
    }
}
