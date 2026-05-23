using Allure.NUnit;
using DesktopAutomationFramework.Pages;
using DesktopAutomationFramework.Utilities;
using NUnit.Framework;

namespace DesktopAutomationFramework.Tests
{
    [TestFixture]
    [AllureNUnit]
    public class NotepadEditFeatureTests
        : BaseTest
    {
        [Test]
        [TestCase(
            @"C:\Temp\TestFiles\Undo_FlaUI.txt",
            " FlaUI and C#")]

        [TestCase(
            @"C:\Temp\TestFiles\Undo_Framework.txt",
            " Automation Framework")]

        [Category("P1")]
        [Order(1)]
        [Description("TC_6: Verify Undo Feature")]
        public void VerifyUndoFeature(
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

                notepadPage.PerformUndo();

                notepadPage.SaveExistingFile();

                // AFTER TEST

                string content =
                    File.ReadAllText(
                        filePath);

                Assert.That(
                    content.Trim(),
                    Is.EqualTo(
                        "Hello World"));

                LoggerHelper.Log(
                    "Undo validation completed");
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"VerifyUndoFeature failed: {ex.Message}");

                throw;
            }
        }
    }
}