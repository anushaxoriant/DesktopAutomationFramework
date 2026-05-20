using Allure.NUnit;
using DesktopAutomationFramework.Pages;
using DesktopAutomationFramework.Utilities;
using NUnit.Framework;

namespace DesktopAutomationFramework.Tests
{
    [TestFixture]
    [AllureNUnit]
    public class NotepadCoreWorkflowTests
        : BaseTest
    {
        // TC_001
        // Verify save file

        [Test]
        [Category("P0")]
        [Order(1)]
        [Description("TC_001 - Verify Save File")]
        public void VerifySaveFile()
        {
            string filePath =
                @"C:\Temp\SaveFile.txt";

            try
            {
                // BEFORE TEST

                if (File.Exists(filePath))
                {
                    File.Delete(
                        filePath);

                    LoggerHelper.Log(
                        "Existing file deleted");
                }

                Assert.That(
                    File.Exists(filePath),
                    Is.False);

                // Launch app

                App =
                    FlaUI.Core.Application.Launch(
                        @"C:\Windows\System32\notepad.exe");

                // Page

                var notepadPage =
                    new NotepadPage(
                        App,
                        Automation!);

                // Validate window

                WindowHelper.VerifyWindow(
                    notepadPage.Window);

                // Validate editor

                Assert.That(
                    notepadPage.Editor,
                    Is.Not.Null);

                // TEST EXECUTION

                notepadPage.EnterText(
                    "Hello World");

                notepadPage.SaveFile(
                    filePath);

                // AFTER TEST

                Assert.That(
                    File.Exists(filePath),
                    Is.True);

                string content =
                    File.ReadAllText(
                        filePath);

                Assert.That(
                    content,
                    Does.Contain(
                        "Hello World"));

                LoggerHelper.Log(
                    "Save File validation completed");
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"VerifySaveFile failed: {ex.Message}");

                throw;
            }
        }

        // TC_002
        // Verify append text

        [Test]
        [TestCase(
            @"C:\Temp\TestFiles\AppendText_FlaUI.txt",
            " FlaUI and C#")]

        [TestCase(
            @"C:\Temp\TestFiles\AppendText_Automation.txt",
            " Automation Testing")]

        [Category("P0")]
        [Order(2)]
        [Description("TC_002 - Verify Append Text")]
        public void VerifyAppendText(
            string filePath,
            string appendText)
        {
            try
            {
                // BEFORE TEST

                if (!File.Exists(filePath))
                {
                    LoggerHelper.Log(
                        "Baseline file missing. Recreating.");

                    File.WriteAllText(
                        filePath,
                        "Hello World");
                }

                string baselineContent =
                    File.ReadAllText(
                        filePath);

                if (!baselineContent.Contains(
                    "Hello World"))
                {
                    LoggerHelper.Log(
                        "Invalid baseline detected. Restoring.");

                    File.WriteAllText(
                        filePath,
                        "Hello World");
                }

                Assert.That(
                    File.Exists(filePath),
                    Is.True);

                Assert.That(
                    File.ReadAllText(filePath),
                    Does.Contain(
                        "Hello World"));

                // Launch file

                App =
                    FlaUI.Core.Application.Launch(
                        "notepad.exe",
                        filePath);

                // Page

                var notepadPage =
                    new NotepadPage(
                        App,
                        Automation!);

                // Validate window

                WindowHelper.VerifyWindow(
                    notepadPage.Window);

                // TEST EXECUTION

                notepadPage.AppendText(
                    appendText);

                notepadPage.SaveExistingFile();

                // AFTER TEST

                string content =
                    File.ReadAllText(
                        filePath);

                Assert.That(
                    content,
                    Does.Contain(
                        appendText.Trim()));

                LoggerHelper.Log(
                    "Append validation completed");
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"VerifyAppendText failed: {ex.Message}");

                throw;
            }
        }

        // TC_003
        // Verify clear text

        [Test]
        [Category("P0")]
        [Order(3)]
        [Description("TC_003 - Verify Clear Text")]
        public void VerifyClearText()
        {
            string filePath =
                @"C:\Temp\TestFiles\ClearText.txt";

            try
            {
                // BEFORE TEST

                if (!File.Exists(filePath))
                {
                    LoggerHelper.Log(
                        "Baseline file missing. Recreating.");

                    File.WriteAllText(
                        filePath,
                        "Hello World");
                }

                string baselineContent =
                    File.ReadAllText(
                        filePath);

                if (!baselineContent.Contains(
                    "Hello World"))
                {
                    LoggerHelper.Log(
                        "Invalid baseline detected. Restoring.");

                    File.WriteAllText(
                        filePath,
                        "Hello World");
                }

                Assert.That(
                    File.Exists(filePath),
                    Is.True);

                Assert.That(
                    File.ReadAllText(filePath),
                    Does.Contain(
                        "Hello World"));

                // Launch file

                App =
                    FlaUI.Core.Application.Launch(
                        "notepad.exe",
                        filePath);

                // Page

                var notepadPage =
                    new NotepadPage(
                        App,
                        Automation!);

                // Validate window

                WindowHelper.VerifyWindow(
                    notepadPage.Window);

                // TEST EXECUTION

                notepadPage.SelectAllText();

                notepadPage.DeleteSelectedText();

                notepadPage.SaveExistingFile();

                // AFTER TEST

                string content =
                    File.ReadAllText(
                        filePath);

                Assert.That(
                    string.IsNullOrWhiteSpace(
                        content),
                    Is.True);

                LoggerHelper.Log(
                    "Clear validation completed");
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"VerifyClearText failed: {ex.Message}");

                throw;
            }
        }
    }
}