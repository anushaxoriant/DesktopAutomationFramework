using System.IO;

namespace DesktopAutomationFramework.Utilities
{
    public static class FileHelper
    {
        // Test folder

        private static readonly string TestFolder =
            @"C:\Temp\TestFiles";

        // Prepare files

        public static void PrepareTestFiles()
        {
            try
            {
                // Create folder

                Directory.CreateDirectory(
                    TestFolder);

                // TC_002_01
                // Append flaUI

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "AppendText_FlaUI.txt"),
                    "Hello World");

                // TC_002_02
                // Append automation

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "AppendText_Automation.txt"),
                    "Hello World");

                // TC_003
                // Clear text

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "ClearText.txt"),
                    "Hello World");

                // TC_004_01
                // Find hello

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "Find_Hello.txt"),
                    "Hello World FlaUI");

                // TC_004_02
                // Find world

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "Find_World.txt"),
                    "Hello World FlaUI");

                // TC_004_03
                // Find flaUI

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "Find_FlaUI.txt"),
                    "Hello World FlaUI");

                // TC_005_01
                // Replace world

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "Replace_World.txt"),
                    "Hello World");

                // TC_005_02
                // Replace hello

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "Replace_Hello.txt"),
                    "Hello World");

                // TC_006_01
                // Undo flaUI

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "Undo_FlaUI.txt"),
                    "Hello World");

                // TC_006_02
                // Undo framework

                File.WriteAllText(
                    Path.Combine(
                        TestFolder,
                        "Undo_Framework.txt"),
                    "Hello World");

                // Logging

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

        // Cleanup files

        public static void CleanupTestFiles()
        {
            // Delete folder

            if (Directory.Exists(
                TestFolder))
            {
                Directory.Delete(
                    TestFolder,
                    true);
            }

            // Delete save file

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