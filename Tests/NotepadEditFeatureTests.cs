// NOTEPADEDITFEATURETESTS.CS

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

    public class NotepadEditFeatureTests : BaseTest
    {
        [Test]
        [Category("P1")]
        [Order(1)]

        [Description(
            "Verify Undo functionality")]
        public void VerifyUndoOperation()
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

                AllureApi.Step(
                    "Entering initial text");

                notepadPage.EnterText(
                    "Hello Automation");

                AllureApi.Step(
                    "Performing Undo operation");

                FlaUI.Core.Input.Keyboard.Press(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL);

                FlaUI.Core.Input.Keyboard.Press(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_Z);

                FlaUI.Core.Input.Keyboard.Release(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_Z);

                FlaUI.Core.Input.Keyboard.Release(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL);

                WaitHelper.WaitUntil(
                    () =>
                    string.IsNullOrWhiteSpace(
                        notepadPage.ReadEditorText()),
                    "Undo operation did not clear editor.");

                string currentText =
                    notepadPage.ReadEditorText();

                Assert.That(
                    string.IsNullOrWhiteSpace(
                        currentText),
                    Is.True,
                    "Undo operation failed.");

                LoggerHelper.Log(
                    "Undo validation completed");
            }
            catch (Exception ex)
            {
                FrameworkExceptionHandler.HandleFailure(
                    "VerifyUndoOperation failed.",
                    ex);
            }
        }

        [Test]
        [Category("P1")]
        [Order(2)]

        [Description(
            "Verify text replacement functionality")]
        public void VerifyTextReplacement()
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

                AllureApi.Step(
                    "Entering initial text");

                notepadPage.EnterText(
                    "Hello World");

                AllureApi.Step(
                    "Replacing text");

                notepadPage.ClearEditor();

                notepadPage.EnterText(
                    "Hello Enterprise Framework");

                string updatedText =
                    notepadPage.ReadEditorText();

                Assert.That(
                    updatedText,
                    Does.Contain(
                        "Enterprise Framework"),
                    "Text replacement failed.");

                LoggerHelper.Log(
                    "Text replacement validation completed");
            }
            catch (Exception ex)
            {
                FrameworkExceptionHandler.HandleFailure(
                    "VerifyTextReplacement failed.",
                    ex);
            }
        }
    }
}