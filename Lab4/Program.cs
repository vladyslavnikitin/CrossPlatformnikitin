using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using ClassLibrary;

namespace Lab4
{
    [Command(Name = "lab4", Description = "Run labs using ClassLibrary")]
    [HelpOption("--help")]
    class Program
    {
        [Option("-I|--input <FILE>", "Path to the input file", CommandOptionType.SingleValue)]
        public string? InputFile { get; }

        [Option("-o|--output <FILE>", "Path to the output file", CommandOptionType.SingleValue)]
        public string? OutputFile { get; }

        [Option("-p|--path <PATH>", "Set default path for files", CommandOptionType.SingleValue)]
        public string? DefaultPath { get; }

        [Argument(0, Description = "Command to execute (version, run, set-path)")]
        public string? Command { get; }

        [Argument(1, Description = "Lab to run (lab1, lab2, lab3)")]
        public string? Lab { get; }

        static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            switch (Command?.ToLower())
            {
                case "version":
                    ShowVersion();
                    break;
                case "run":
                    RunLab();
                    break;
                case "set-path":
                    SetPath();
                    break;
                default:
                    Console.WriteLine("Invalid command. Use --help for usage information.");
                    break;
            }
        }

        private void ShowVersion()
        {
            Console.WriteLine("Author: Vladyslav_nikitin");
            Console.WriteLine("Version: 1.0.0");
        }

        private void RunLab()
        {
            if (string.IsNullOrEmpty(Lab))
            {
                Console.WriteLine("Please specify a lab to run (lab1, lab2, lab3).");
                return;
            }

            string inputPath = InputFile ?? GetDefaultPath($"input_{Lab}.txt");
            string outputPath = OutputFile ?? GetDefaultPath($"output_{Lab}.txt");

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            try
            {
                var runner = new LabRunner();
                switch (Lab.ToLower())
                {
                    case "lab1":
                        runner.RunLab1(inputPath, outputPath);
                        break;
                    case "lab2":
                        runner.RunLab2(inputPath, outputPath);
                        break;
                    case "lab3":
                        runner.RunLab3(inputPath, outputPath);
                        break;
                    default:
                        Console.WriteLine($"Unknown lab: {Lab}. Use lab1, lab2, or lab3.");
                        return;
                }

                Console.WriteLine($"Lab {Lab} completed. Output saved to {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running {Lab}: {ex.Message}");
            }
        }

        private void SetPath()
        {
            if (string.IsNullOrEmpty(DefaultPath))
            {
                Console.WriteLine("Please specify a path to set.");
                return;
            }

            Environment.SetEnvironmentVariable("LAB_PATH", DefaultPath, EnvironmentVariableTarget.User);
            Console.WriteLine($"Default path set to: {DefaultPath}");
        }

        private string GetDefaultPath(string fileName)
        {
            string? envPath = Environment.GetEnvironmentVariable("LAB_PATH");
            return !string.IsNullOrEmpty(envPath)
                ? Path.Combine(envPath, fileName)
                : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), fileName);
        }
    }
}
