using Allure.Net.Commons;

using DesktopAutomationFramework.Pages;
using DesktopAutomationFramework.Utilities;

using FlaUI.UIA3;

using NUnit.Framework;

namespace DesktopAutomationFramework.Tests
{
    public class BaseTest
    {
        protected FlaUI.Core.Application? App;

        protected UIA3Automation? Automation;

        // =========================
        // ONE TIME SETUP
        // =========================

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            try
            {
                // Initialize automation

                Automation =
                    new UIA3Automation();

                // Prepare files

                FileHelper.PrepareTestFiles();

                // Logging

                LoggerHelper.Log(
                    "Framework initialized");
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"GlobalSetup failed: {ex.Message}");

                throw;
            }
        }

        // =========================
        // TEST SETUP
        // =========================

        [SetUp]
        public void Setup()
        {
            try
            {
                // Logging

                LoggerHelper.Log(
                    $"Starting Test: " +
                    $"{TestContext.CurrentContext.Test.Name}");

                // Reporting

                AllureApi.Step(
                    $"Executing Test: " +
                    $"{TestContext.CurrentContext.Test.Name}");
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"Setup failed: {ex.Message}");

                throw;
            }
        }

        // =========================
        // TEST CLEANUP
        // =========================

        [TearDown]
        public void TearDown()
        {
            try
            {
                if (App != null)
                {
                    if (!App.HasExited)
                    {
                        App.Kill();
                    }
                }

                LoggerHelper.Log(
                    $"Completed Test: " +
                    $"{TestContext.CurrentContext.Test.Name}");
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"TearDown failed: {ex.Message}");
            }
        }

        // =========================
        // ONE TIME CLEANUP
        // =========================

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            try
            {
                // Dispose automation

                Automation?.Dispose();

                // Kill stray notepad

                var notepadProcesses =
                    System.Diagnostics.Process
                        .GetProcessesByName(
                            "notepad");

                foreach (var process
                    in notepadProcesses)
                {
                    try
                    {
                        process.Kill();
                    }
                    catch
                    {
                    }
                }

                // Cleanup files

                FileHelper.CleanupTestFiles();

                LoggerHelper.Log(
                    "Framework cleanup completed");
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"GlobalTearDown failed: {ex.Message}");
            }
        }

        // =========================
        // BEFORE SAVE TEST VALIDATION
        // =========================

        protected void ValidateBeforeSaveTest(
            string filePath)
        {
            try
            {
                LoggerHelper.Log(
                    $"Starting save test validation for: {filePath}");

                if (File.Exists(filePath))
                {
                    File.Delete(
                        filePath);

                    LoggerHelper.Log(
                        $"Deleted existing file: {filePath}");
                }

                Assert.That(
                    File.Exists(filePath),
                    Is.False,
                    "File should not exist before Save test.");

                LoggerHelper.Log(
                    "Before Save validation completed successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Before Save validation failed. {ex.Message}");
            }
        }

        // =========================
        // BEFORE VALIDATION
        // =========================

        protected void ValidateBeforeTest(
            string filePath,
            string expectedContent)
        {
            try
            {
                LoggerHelper.Log(
                    $"Starting before validation for: {filePath}");

                if (!File.Exists(filePath))
                {
                    File.WriteAllText(
                        filePath,
                        expectedContent);

                    LoggerHelper.Log(
                        $"Created missing baseline file: {filePath}");
                }

                string actualContent =
                    File.ReadAllText(
                        filePath);

                if (actualContent != expectedContent)
                {
                    LoggerHelper.Log(
                        $"Repairing baseline content for: {filePath}");

                    File.WriteAllText(
                        filePath,
                        expectedContent);

                    actualContent =
                        File.ReadAllText(
                            filePath);
                }

                Assert.That(
                    actualContent,
                    Is.EqualTo(
                        expectedContent),
                    "Before validation failed.");

                LoggerHelper.Log(
                    "Before validation completed successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Before validation failed. {ex.Message}");
            }
        }

        // =========================
        // AFTER VALIDATION
        // =========================

        protected void ValidateAfterTest(
            string filePath,
            string expectedContent)
        {
            try
            {
                LoggerHelper.Log(
                    $"Starting after validation for: {filePath}");

                if (!File.Exists(filePath))
                {
                    throw new Exception(
                        $"After validation failed. File '{filePath}' does not exist.");
                }

                string actualContent =
                    File.ReadAllText(
                        filePath);

                if (actualContent.Trim()
                    != expectedContent.Trim())
                {
                    throw new Exception(
                        $"After validation failed. Expected content '{expectedContent}' but found '{actualContent}'.");
                }

                LoggerHelper.Log(
                    "After validation completed successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"After validation failed. {ex.Message}");
            }
        }

        // =========================
        // LAUNCH EMPTY NOTEPAD
        // =========================

        protected NotepadPage LaunchEmptyNotepad()
        {
            try
            {
                LoggerHelper.Log(
                    "Launching empty Notepad");

                App =
                    FlaUI.Core.Application.Launch(
                        "notepad.exe");

                if (App == null)
                {
                    throw new Exception(
                        "Notepad failed to launch.");
                }

                if (App.HasExited)
                {
                    throw new Exception(
                        "Notepad exited unexpectedly.");
                }

                var notepadPage =
                    new NotepadPage(
                        App,
                        Automation!);

                WindowHelper.VerifyWindow(
                    notepadPage.Window);

                LoggerHelper.Log(
                    "Empty Notepad launched successfully");

                return notepadPage;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"LaunchEmptyNotepad failed. {ex.Message}");
            }
        }

        // =========================
        // LAUNCH NOTEPAD
        // =========================

        protected NotepadPage LaunchNotepad(
            string filePath)
        {
            try
            {
                LoggerHelper.Log(
                    $"Launching Notepad with file: {filePath}");

                App =
                    FlaUI.Core.Application.Launch(
                        "notepad.exe",
                        filePath);

                if (App == null)
                {
                    throw new Exception(
                        "Notepad failed to launch.");
                }

                if (App.HasExited)
                {
                    throw new Exception(
                        "Notepad exited unexpectedly.");
                }

                var notepadPage =
                    new NotepadPage(
                        App,
                        Automation!);

                WindowHelper.VerifyWindow(
                    notepadPage.Window);

                LoggerHelper.Log(
                    "Notepad launched successfully");

                return notepadPage;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"LaunchNotepad failed. {ex.Message}");
            }
        }
    }
}