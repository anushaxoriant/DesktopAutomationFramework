using Allure.NUnit;
using Allure.Net.Commons;
using DesktopAutomationFramework.Base;
using DesktopAutomationFramework.Pages;
using DesktopAutomationFramework.Utilities;
using FlaUI.Core;
using NUnit.Framework;

namespace DesktopAutomationFramework.Tests
{
    [TestFixture]
    [AllureNUnit]

    public class NotepadCoreWorkflowTests : BaseTest
    {
        private readonly string testFilePath =
            @"C:\Temp\NotepadTestFile.txt";

        [Test]
        [Category("P0")]
        [Order(1)]

        [Description(
            "Verify user can enter text and save file")]
        public void VerifySaveFile()
        {
            try
            {
                AllureApi.Step(
                    "Deleting existing file");

                if (File.Exists(testFilePath))
                {
                    File.Delete(testFilePath);

                    LoggerHelper.Log(
                        "Existing file deleted");
                }

                AllureApi.Step(
                    "Launching Notepad");

                App =
                    FlaUI.Core.Application.Launch(
                        FrameworkConstants.NotepadPath);

                if (App == null)
                {
                    throw new Exception(
                        "Notepad application failed to launch.");
                }

                AllureApi.Step(
                    "Initializing Notepad Page");

                var notepadPage =
                    new NotepadPage(
                        App,
                        Automation!);

                Assert.That(
                    notepadPage.Window,
                    Is.Not.Null,
                    "Notepad window was not initialized.");

                AllureApi.Step(
                    "Entering text");

                notepadPage.EnterText(
                    "Hello World");

                AllureApi.Step(
                    "Saving file");

                notepadPage.SaveFile(
                    testFilePath);

                AllureApi.Step(
                    "Validating saved file");

                Assert.That(
                    File.Exists(testFilePath),
                    Is.True,
                    "Saved file does not exist.");

                string content =
                    File.ReadAllText(
                        testFilePath);

                Assert.That(
                    content,
                    Does.Contain(
                        "Hello World"),
                    "Expected content not found in saved file.");

                LoggerHelper.Log(
                    "Save file validation completed");
            }
            catch (Exception ex)
            {
                FrameworkExceptionHandler.HandleFailure(
                    "VerifySaveFile failed.",
                    ex);
            }
        }

        [Test]
        [Category("P1")]
        [Order(2)]

        [Description(
            "Verify editor clear functionality")]
        public void VerifyClearEditor()
        {
            try
            {
                AllureApi.Step(
                    "Launching Notepad");

                App =
                    FlaUI.Core.Application.Launch(
                        FrameworkConstants.NotepadPath);

                if (App == null)
                {
                    throw new Exception(
                        "Notepad failed to launch.");
                }

                var notepadPage =
                    new NotepadPage(
                        App,
                        Automation!);

                notepadPage.EnterText(
                    "Automation Testing");

                AllureApi.Step(
                    "Clearing editor");

                notepadPage.ClearEditor();

                string currentText =
                    notepadPage.ReadEditorText();

                Assert.That(
                    string.IsNullOrWhiteSpace(
                        currentText),
                    Is.True,
                    "Editor was not cleared successfully.");

                LoggerHelper.Log(
                    "Editor clear validation completed");
            }
            catch (Exception ex)
            {
                FrameworkExceptionHandler.HandleFailure(
                    "VerifyClearEditor failed.",
                    ex);
            }
        }
    }
}