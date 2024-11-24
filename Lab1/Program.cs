using System;

public static class Program
{
    private static readonly int[] BlackSticks = { 6, 2, 5, 5, 4, 5, 6, 3, 7, 6 };

    public static void Main(string[] args)
    {
        Console.WriteLine("Введіть значення n:");
        int n = int.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine("Введіть значення k:");
        int k = int.Parse(Console.ReadLine() ?? "0");

        var result = FindMinMaxNumber(n, k);
        Console.WriteLine($"Мінімальне число: {result.Min}");
        Console.WriteLine($"Максимальне число: {result.Max}");
    }

    public static (string Min, string Max) FindMinMaxNumber(int n, int k)
    {
        if (k < n * 2 || k > n * 7) return ("NO SOLUTION", "NO SOLUTION");

        string minNumber = BuildMinNumber(n, k);
        string maxNumber = BuildMaxNumber(n, k);

        return (minNumber, maxNumber);
    }

    private static string BuildMinNumber(int n, int k)
    {
        char[] result = new char[n];
        k -= 2;
        result[0] = '1';

        for (int i = n - 1; i > 0 && k > 0; i--)
        {
            if (k >= 6)
            {
                result[i] = '8';
                k -= 7;
            }
            else
            {
                result[i] = FindDigitForSticks(k);
                k -= BlackSticks[result[i] - '0'];
            }
        }

        if (k > 0)
        {
            result[0] = FindDigitForSticks(2 + k);
        }

        return new string(result);
    }

    private static string BuildMaxNumber(int n, int k)
    {
        char[] result = new char[n];

        for (int i = 0; i < n; i++)
        {
            if (k >= 7)
            {
                result[i] = '8';
                k -= 7;
            }
            else
            {
                result[i] = FindDigitForSticks(k);
                k -= BlackSticks[result[i] - '0'];
            }
        }

        return new string(result);
    }

    private static char FindDigitForSticks(int sticks)
    {
        for (int digit = 0; digit <= 9; digit++)
        {
            if (BlackSticks[digit] == sticks)
                return (char)('0' + digit);
        }
        throw new ArgumentException("Неможливо знайти цифру для заданої кількості смужок.");
    }
}
