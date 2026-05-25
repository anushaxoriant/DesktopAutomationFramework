using System;

namespace DesktopAutomationFramework.Utilities
{
    public static class FileHelper
    {
        // =========================
        // PREPARE TEST FILES
        // =========================

        public static void PrepareTestFiles()
        {
            try
            {
                LoggerHelper.Log(
                    "Preparing test files");

                string testFilesFolder =
                    @"C:\Temp\TestFiles";


                 string negativeFolder = @"C:\Temp\TestFiles\Negative";

                 if (Directory.Exists(
            testFilesFolder))
        {
            LoggerHelper.Log(
                "Deleting existing TestFiles directory");

            Directory.Delete(
                testFilesFolder,
                true);

            WaitHelper.ApplyDelay(
                1000);
        }
                    Directory.CreateDirectory(
                        testFilesFolder);

                    LoggerHelper.Log(
                        $"Created folder: {testFilesFolder}");

                    Directory.CreateDirectory(
    negativeFolder);

LoggerHelper.Log(
    "Negative folder created");

                // =========================
                // CREATE BASELINE FILES
                // =========================

                CreateFile(
                    @"C:\Temp\TestFiles\AppendText_FlaUI.txt",
                    "Hello World");

                CreateFile(
                    @"C:\Temp\TestFiles\ClearText.txt",
                    "Hello World");

                CreateFile(
                    @"C:\Temp\TestFiles\Find_World.txt",
                    "Hello World FlaUI");

                CreateFile(
                    @"C:\Temp\TestFiles\Replace_World.txt",
                    "Hello World");

                CreateFile(
                    @"C:\Temp\TestFiles\Undo_FlaUI.txt",
                    "Hello World");

                CreateFile(
    @"C:\Temp\TestFiles\Negative\AppendText_Empty.txt",
    "Hello World");

    CreateFile(
    @"C:\Temp\TestFiles\Negative\ClearText_Empty.txt",
    "Hello World");

    CreateFile(
    @"C:\Temp\TestFiles\Negative\FindText_Invalid.txt",
    "Hello World");

    CreateFile(
    @"C:\Temp\TestFiles\Negative\ReplaceText_Invalid.txt",
    "Hello World");

    CreateFile(
    @"C:\Temp\TestFiles\Negative\UndoText_Empty.txt",
    "Hello World");

                LoggerHelper.Log(
                    "Test files prepared successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"PrepareTestFiles failed. {ex.Message}");
            }
        }

        // =========================
        // CREATE FILE
        // =========================

        private static void CreateFile(
            string filePath,
            string content)
        {
            try
            {
                File.WriteAllText(
                    filePath,
                    content);

                LoggerHelper.Log(
                    $"Created file: {filePath}");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"CreateFile failed for '{filePath}'. {ex.Message}");
            }
        }

        // =========================
        // CLEANUP TEST FILES
        // =========================

        public static void CleanupTestFiles()
        {
            try
            {
                string testFilesFolder =
                    @"C:\Temp\TestFiles";

                if (Directory.Exists(
                    testFilesFolder))
                {
                    Directory.Delete(
                        testFilesFolder,
                        true);

                    LoggerHelper.Log(
                        $"Deleted folder: {testFilesFolder}");
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"CleanupTestFiles failed: {ex.Message}");
            }
        }
    }
}