// NOTEPADDIALOGVALIDATIONTESTS.CS

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

    public class NotepadDialogValidationTests : BaseTest
    {
        [Test]
        [Category("P1")]
        [Order(1)]

        [Description(
            "Verify Find dialog opens successfully")]
        public void VerifyFindDialog()
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
                    "Opening Find dialog");

                FlaUI.Core.Input.Keyboard.Press(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL);

                FlaUI.Core.Input.Keyboard.Press(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_F);

                FlaUI.Core.Input.Keyboard.Release(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_F);

                FlaUI.Core.Input.Keyboard.Release(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL);

                WaitHelper.WaitUntil(
                    () =>
                    notepadPage.Window.ModalWindows.Length > 0,
                    "Find dialog did not appear.");

                var findDialog =
                    notepadPage.Window.ModalWindows
                        .FirstOrDefault();

                Assert.That(
                    findDialog,
                    Is.Not.Null,
                    "Find dialog was not displayed.");

                LoggerHelper.Log(
                    "Find dialog validation completed");
            }
            catch (Exception ex)
            {
                FrameworkExceptionHandler.HandleFailure(
                    "VerifyFindDialog failed.",
                    ex);
            }
        }

        [Test]
        [Category("P1")]
        [Order(2)]

        [Description(
            "Verify Replace dialog opens successfully")]
        public void VerifyReplaceDialog()
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
                    "Opening Replace dialog");

                FlaUI.Core.Input.Keyboard.Press(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL);

                FlaUI.Core.Input.Keyboard.Press(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_H);

                FlaUI.Core.Input.Keyboard.Release(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_H);

                FlaUI.Core.Input.Keyboard.Release(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL);

                WaitHelper.WaitUntil(
                    () =>
                    notepadPage.Window.ModalWindows.Length > 0,
                    "Replace dialog did not appear.");

                var replaceDialog =
                    notepadPage.Window.ModalWindows
                        .FirstOrDefault();

                Assert.That(
                    replaceDialog,
                    Is.Not.Null,
                    "Replace dialog was not displayed.");

                LoggerHelper.Log(
                    "Replace dialog validation completed");
            }
            catch (Exception ex)
            {
                FrameworkExceptionHandler.HandleFailure(
                    "VerifyReplaceDialog failed.",
                    ex);
            }
        }
    }
}