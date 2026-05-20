using System.IO;

namespace DesktopAutomationFramework.Utilities
{
    public static class FileHelper
    {
        //---------------------------------
        // Test Folder
        //---------------------------------

        private static readonly string TestFolder =
            @"C:\Temp\TestFiles";

        //---------------------------------
// Prepare Files
//---------------------------------

public static void PrepareTestFiles()
{
    try
    {
        //---------------------------------
        // Create Folder
        //---------------------------------

        Directory.CreateDirectory(
            TestFolder);

        //---------------------------------
        // TC_002_01
        // Append FlaUI
        //---------------------------------

        File.WriteAllText(
            Path.Combine(
                TestFolder,
                "AppendText_FlaUI.txt"),
            "Hello World");

        //---------------------------------
        // TC_002_02
        // Append Automation
        //---------------------------------

        File.WriteAllText(
            Path.Combine(
                TestFolder,
                "AppendText_Automation.txt"),
            "Hello World");

        //---------------------------------
        // TC_003
        // Clear Text
        //---------------------------------

        File.WriteAllText(
            Path.Combine(
                TestFolder,
                "ClearText.txt"),
            "Hello World");

        //---------------------------------
        // TC_004_01
        // Find Hello
        //---------------------------------

        File.WriteAllText(
            Path.Combine(
                TestFolder,
                "Find_Hello.txt"),
            "Hello World FlaUI");

        //---------------------------------
        // TC_004_02
        // Find World
        //---------------------------------

        File.WriteAllText(
            Path.Combine(
                TestFolder,
                "Find_World.txt"),
            "Hello World FlaUI");

        //---------------------------------
        // TC_004_03
        // Find FlaUI
        //---------------------------------

        File.WriteAllText(
            Path.Combine(
                TestFolder,
                "Find_FlaUI.txt"),
            "Hello World FlaUI");

        //---------------------------------
        // TC_005_01
        // Replace World
        //---------------------------------

        File.WriteAllText(
            Path.Combine(
                TestFolder,
                "Replace_World.txt"),
            "Hello World");

        //---------------------------------
        // TC_005_02
        // Replace Hello
        //---------------------------------

        File.WriteAllText(
            Path.Combine(
                TestFolder,
                "Replace_Hello.txt"),
            "Hello World");

        //---------------------------------
        // TC_006_01
        // Undo FlaUI
        //---------------------------------

        File.WriteAllText(
            Path.Combine(
                TestFolder,
                "Undo_FlaUI.txt"),
            "Hello World");

        //---------------------------------
        // TC_006_02
        // Undo Framework
        //---------------------------------

        File.WriteAllText(
            Path.Combine(
                TestFolder,
                "Undo_Framework.txt"),
            "Hello World");

        //---------------------------------
        // Logging
        //---------------------------------

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

        //---------------------------------
        // Cleanup Files
        //---------------------------------

        public static void CleanupTestFiles()
        {
            //---------------------------------
            // Delete Folder
            //---------------------------------

            if (Directory.Exists(
                TestFolder))
            {
                Directory.Delete(
                    TestFolder,
                    true);
            }

            //---------------------------------
            // Delete Save File
            //---------------------------------

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