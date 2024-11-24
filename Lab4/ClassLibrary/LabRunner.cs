using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ClassLibrary
{
    public class LabRunner
    {
        public void RunLab1(string inputFile, string outputFile)
        {
            string[] input = File.ReadAllLines(inputFile);
            string[] data = input[0].Split(' ');
            int n = int.Parse(data[0]), k = int.Parse(data[1]);
            string[] result = Lab1.Calculate(n, k);
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                writer.Write(result[0]);
                if (result[0] != "NO SOLUTION") writer.Write('\n' + result[1]);
            }
        }

        public void RunLab2(string inputFile, string outputFile)
        {
            try
            {
                if (!File.Exists(inputFile))
                    throw new Exception($"Файл {inputFile} не знайдено.");
                var (n, k, l, queries) = Lab2.ReadInput(inputFile);
                var deletionTimes = Lab2.CalculateDeletionTimes(n, k);
                Lab2.WriteOutput(outputFile, queries, deletionTimes);
                Console.WriteLine($"Результат записано в файл {outputFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ПОМИЛКА: {ex.Message}");
            }
        }

        public void RunLab3(string inputFile, string outputFile)
        {
            List<string> input = new List<string>();
            try
            {
                input = File.ReadAllLines(inputFile).ToList();
                if (!Lab3.InputFileIsCorrect(input))
                    throw new FormatException("Файл INPUT.txt повинен мати 8 рядків по 8 символів");
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"ПОМИЛКА: {e.Message}");
                return;
            }
            File.WriteAllText(outputFile, Lab3.CalculateWorkers(input).ToString());
        }
    }

    class Lab1
    {
        private static readonly int[] segments = { 6, 2, 5, 5, 4, 5, 6, 3, 7, 6 };

        public static string[] Calculate(int n, int k)
        {
            return new string[] { FindMinNumber(n, k), FindMaxNumber(n, k) };
        }

        private static string FindMinNumber(int n, int k)
        {
            string solution = "NO SOLUTION";
            int i = 1;
            if (k >= n * 2)
            {
                solution = new string('1', n);
                k -= n * 2;

                while (i <= n && k >= 4)
                {
                    solution = solution.Remove(i, 1).Insert(i, "0");
                    k -= 4;
                    i++;
                }

                i = n;
                while (i >= 1 && k > 0)
                {
                    switch (solution[i - 1])
                    {
                        case '1':
                            if (k == 1)
                            {
                                solution = solution.Remove(i - 1, 1).Insert(i - 1, "7");
                                k = 0;
                            }
                            else if (k == 2)
                            {
                                solution = solution.Remove(i - 1, 1).Insert(i - 1, "4");
                                k = 0;
                            }
                            else if (k == 3)
                            {
                                solution = solution.Remove(i - 1, 1).Insert(i - 1, "2");
                                k = 0;
                            }
                            else if (k == 4)
                            {
                                solution = solution.Remove(i - 1, 1).Insert(i - 1, "6");
                                k = 0;
                            }
                            else
                            {
                                solution = solution.Remove(i - 1, 1).Insert(i - 1, "8");
                                k -= 5;
                            }
                            break;
                        case '0':
                            solution = solution.Remove(i - 1, 1).Insert(i - 1, "8");
                            k--;
                            break;
                    }
                    i--;
                }

                if (k != 0) solution = "NO SOLUTION";
            }
            return solution;
        }

        public static string FindMaxNumber(int n, int k)
        {
            char[] result = new char[n];
            int totalSegments = k;

            for (int i = 0; i < n; i++)
            {
                for (int digit = 9; digit >= 0; digit--)
                {
                    int neededSegments = segments[digit];
                    int remainingPositions = n - i - 1;

                    if (totalSegments - neededSegments >= remainingPositions * 2)
                    {
                        result[i] = (char)('0' + digit);
                        totalSegments -= neededSegments;
                        break;
                    }
                }
            }

            if (totalSegments != 0)
                return "NO SOLUTION";

            return new string(result);
        }
    }

    class Lab2
    {
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
            var numbers = new List<int>();

            for (int i = 1; i <= n; i++)
                numbers.Add(i);

            int currNumDelTime = 1;

            while (numbers.Count >= k)
            {
                var toRemove = new List<int>();

                for (int i = k - 1; i < numbers.Count; i += k)
                {
                    deletionTimes[numbers[i]] = currNumDelTime++;
                    toRemove.Add(numbers[i]);
                }

                foreach (var num in toRemove)
                    numbers.Remove(num);
            }

            foreach (var num in numbers)
            {
                if (!deletionTimes.ContainsKey(num))
                    deletionTimes[num] = 0;
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
                        writer.Write(deletionTimes[query]);
                    else
                        writer.Write(0); // Якщо символ не було видалено
                }
            }
        }
    }

    class Lab3
    {
        public static bool InputFileIsCorrect(List<string> input)
        {
            if (input.Count != 8) return false;
            foreach (string line in input)
            {
                if (line.Length != 8) return false;
            }
            return true;
        }

        public static int CalculateWorkers(List<string> input)
        {
            int pattern1Errors = 0; // Помилки для еталону 1 (початок з 'W')
            int pattern2Errors = 0; // Помилки для еталону 2 (початок з 'B')

            // Проходимо по кожній клітинці дошки
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // Обчислюємо, яка плитка має бути за шаховим візерунком
                    char expectedPattern1 = ((i + j) % 2 == 0) ? 'W' : 'B';
                    char expectedPattern2 = ((i + j) % 2 == 0) ? 'B' : 'W';

                    // Якщо поточна плитка не збігається з еталоном, це помилка
                    if (input[i][j] != expectedPattern1) pattern1Errors++;
                    if (input[i][j] != expectedPattern2) pattern2Errors++;
                }
            }

            int numOfWorkers = Math.Min(pattern1Errors, pattern2Errors);
            return numOfWorkers > 0 ? numOfWorkers : 1;
        }
    }
}
