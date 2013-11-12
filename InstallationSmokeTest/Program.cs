using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using RadicalGeek.Common.Xml;
using RadicalGeek.Testing.Smoke.ConfigurationTests;
using RadicalGeek.Testing.Smoke.ConfigurationTests.Tests;

namespace RadicalGeek.Testing.Smoke.Installation
{
    class Program
    {
        const string Run = "Run";
        const string Create = "Create";
        const string Abort = "Abort";
        const string UnexpectedResponseMessage = "Unexpected response.";
        const string TestPassedMessage = "OK!";
        private const string OverwritePrompt = "Overwrite {0}? [y/N] ";
        private const ConsoleKey OverwriteAffirmativeKey = ConsoleKey.Y;
        private static string outputFile;
        static void Main(string[] args)
        {
            bool runWithUi = true;
            try
            {
                WriteLine("Post-Deployment Smoke Test Tool");
                string operation = Run;

                if (args.Length > 0)
                {
                    runWithUi = false;
                    operation = args[0];
                }

                string file = args.Length > 1 ? args[1] : SelectFile(operation == Run);

                outputFile = args.Length > 2 ? args[2] : null;

                if (file == null) return;
                if (Path.GetExtension(file).ToLower() != ".xml") file += ".xml";

                switch (operation)
                {
                    case Create:
                        CreateConfiguration(file);
                        break;
                    case Run:
                        CheckConfiguration(file);
                        break;
                    default:
                        DisplayUsageHelp();
                        break;
                }
            }
            catch (Exception ex)
            {
                Environment.ExitCode = int.MaxValue;
                if (Environment.UserInteractive)
                {
                    WriteLine(ex.ToString());
                    if (runWithUi && outputFile != null)
                    {
                        Console.Write("Press any key to end . . .");
                        Console.ReadKey(true);
                    }
                }
                else throw;
            }
        }

        private static void DisplayUsageHelp()
        {
            string exeName = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
            Console.WriteLine("Usage:");
            Console.WriteLine();
            Console.WriteLine("{0} {1} <filename> <outputfilename>", exeName, Run);
            Console.WriteLine("\tRun the tests contained in the given filename.");
            Console.WriteLine();
            Console.WriteLine("{0} {1} <filename> <outputfilename>", exeName, Create);
            Console.WriteLine("\tCreate a new XML file with examples of the usage.");
            Console.WriteLine();
            Console.WriteLine("\tThe default mode is Run.");
            Console.WriteLine("\tIf the filename does not end with .xml then .xml will be appended.");
            Console.WriteLine("\tIf the filename is omitted, you will be prompted for it.");
        }

        private static void CreateConfiguration(string file)
        {
            var temp = Console.ForegroundColor;
            try
            {
                if (File.Exists(file))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    WriteLine(OverwritePrompt, file);
                    Console.ForegroundColor = ConsoleColor.White;
                    var overwrite = Console.ReadKey(true);
                    if (overwrite.Key != OverwriteAffirmativeKey)
                    {
                        WriteLine("Not overwriting.");
                        return;
                    }
                }

                Console.ForegroundColor = ConsoleColor.White;
                WriteLine("Preparing example data...");
                var configurationInformation = new ConfigurationTestSuite();
                configurationInformation.CreateExampleData();
                string xmlString = configurationInformation.ToXmlString();
                Console.Write("Writing file...");
                File.WriteAllText(Path.Combine(".", file), xmlString, Encoding.Unicode);
                WriteLine(" Done.");
            }
            finally
            {
                Console.ForegroundColor = temp;
            }
        }

        private static void CheckConfiguration(string file)
        {
            if (!File.Exists(file))
            {
                WriteLine("Could not find file {0}", file);
                Environment.ExitCode = int.MaxValue;
                return;
            }
            ConfigurationTestSuite info;
            try
            {
                string xml = File.ReadAllText(file, Encoding.Unicode);
                info = xml.ToObject<ConfigurationTestSuite>();
            }
            catch (InvalidOperationException ex)
            {
                DisplayError("Unable to read file, check that the file is in Unicode.");
                DisplayError(ex.ToString());
                return;
            }

            WriteLine("Running Tests");
            WriteLine();

            int successfulTests = 0;
            foreach (Test test in info.Tests)
            {
                bool result = RunTest(test);
                if (result) successfulTests++;
            }

            WriteLine();
            WriteLine("Completed Tests");
            int totalTests = info.Tests.Count();
            WriteLine("Tests Run:    {0:#,##0}", totalTests);
            WriteLine("Tests Passed: {0:#,##0}", successfulTests);
            int failedTests = totalTests - successfulTests;
            WriteLine("Tests Failed: {0:#,##0}", failedTests);
            if (failedTests > 0)
                DisplayError("SMOKE TEST FAILED!");
            else
                DisplaySuccess("Smoke test passed successfully");
            Environment.ExitCode = failedTests;
            if (Environment.UserInteractive && outputFile == null)
            {
                Console.WriteLine("Press any key to continue . . .");
                Console.ReadKey(true);
            }
        }

        private static void WriteLine(string text = "", params object[] parameters)
        {
            if (outputFile == null)
                Console.WriteLine(text, parameters);
            else
                File.AppendAllLines(outputFile, new[] { string.Format(text, parameters) });
        }

        private static bool RunTest(Test test)
        {
            var temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            WriteLine("{0}, {1}", test.GetType().Name, test.TestName);
            try
            {
                test.Run();
                Console.ForegroundColor = ConsoleColor.Green;
                WriteLine("\t\t{0}", TestPassedMessage);
                return true;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                WriteLine("\t{0}", ex.Message);
                return false;
            }
            finally
            {
                Console.ForegroundColor = temp;
            }
        }

        private static string SelectFile(bool mustExist)
        {
            string[] files = Directory.GetFiles(".", "*.xml");
            string[] suites = files.Select(f => Path.GetFileName(f).Replace(".xml", "")).Where(f => f.ToUpper() != Abort.ToUpper()).ToArray();
        ChooseFile:
            PresentSelectionOptions(suites, Abort);
            string input = GetInput().Trim();
            if (input == "?")
            {
                DisplayUsageHelp();
                goto ChooseFile;
            }
            if (string.IsNullOrWhiteSpace(input) || input.Equals(Abort, StringComparison.InvariantCultureIgnoreCase) || input == "0") return null;
            string file = null;
            if (suites.Any(e => e == input))
                file = Path.Combine(".", string.Format("{0}.xml", input));
            else
            {
                int fileIndex;
                if (int.TryParse(input, out fileIndex) && fileIndex <= files.Length)
                {
                    if (fileIndex > 0)
                        file = files[fileIndex - 1];
                }
                else
                {
                    if (mustExist)
                    {
                        DisplayError(UnexpectedResponseMessage);
                        goto ChooseFile;
                    }
                    file = input;
                }
            }
            return file;
        }

        private static void PresentSelectionOptions(string[] suites, string abort)
        {
            var temp = Console.ForegroundColor;
            Console.WriteLine();
            Console.WriteLine("Choose a suite or type its name, or type 0 to {0} or ? for CLI help:", abort);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("0: {0}", abort);
            for (int i = 0; i < suites.Length; i++)
                Console.WriteLine("{0}: {1}", i + 1, suites[i]);
            Console.ForegroundColor = temp;
        }

        private static void DisplayError(string errorMessage)
        {
            var temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            WriteLine(errorMessage);
            Console.ForegroundColor = temp;
        }

        private static void DisplaySuccess(string message)
        {
            var temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            WriteLine(message);
            Console.ForegroundColor = temp;
        }

        private static string GetInput()
        {
            var temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("> ");
            Console.ForegroundColor = ConsoleColor.White;
            string input = Console.ReadLine();
            Console.ForegroundColor = temp;
            return input;
        }
    }
}
