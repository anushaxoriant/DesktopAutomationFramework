using System.IO;

namespace DesktopAutomationFramework.Utilities
{
    public static class FileHelper
    {
        private static readonly string TestFolder =
            @"C:\Temp\TestFiles";
        public static void PrepareTestFiles()
        {
            try
            {
                Directory.CreateDirectory(
                    TestFolder);

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "AppendText_FlaUI.txt"),
                    "Hello World");

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "AppendText_Automation.txt"),
                    "Hello World");

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "ClearText.txt"),
                    "Hello World");

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "Find_Hello.txt"),
                    "Hello World FlaUI");

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "Find_World.txt"),
                    "Hello World FlaUI");

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "Find_FlaUI.txt"),
                    "Hello World FlaUI");

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "Replace_World.txt"),
                    "Hello World");

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "Replace_Hello.txt"),
                    "Hello World");

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "Undo_FlaUI.txt"),
                    "Hello World");

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "Undo_Framework.txt"),
                    "Hello World");

                LoggerHelper.Log(
                    "Baseline test files prepared");
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"PrepareTestFiles failed: {ex.Message}");

                throw;
            }
        }
        public static void CleanupTestFiles()
        {
            if (Directory.Exists(
                TestFolder))
            {
                Directory.Delete(
                    TestFolder,
                    true);
            }

            string saveFile =
                @"C:\Temp\SaveFile.txt";

            if (File.Exists(
                saveFile))
            {
                File.Delete(
                    saveFile);
            }
        }
    }
}