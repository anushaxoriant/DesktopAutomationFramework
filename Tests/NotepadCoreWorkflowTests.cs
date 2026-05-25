using Allure.NUnit;
using Allure.Net.Commons;
using DesktopAutomationFramework.Pages;
using DesktopAutomationFramework.Utilities;
using NUnit.Framework;
using DesktopAutomationFramework.Base;

namespace DesktopAutomationFramework.Tests
{
    [TestFixture]
    [AllureNUnit]

    public class NotepadCoreWorkflowTests
        : BaseTest
    {
        [Test]

[Category("P0")]
[Order(1)]

public void VerifySaveFile()
{
    string filePath =
        @"C:\Temp\TestFiles\SaveFile.txt";

    try
    {
        // =========================
        // BEFORE VALIDATION
        // =========================

        ValidateBeforeSaveTest(
            filePath);

        // =========================
        // LAUNCH EMPTY NOTEPAD
        // =========================

        var notepadPage =
            LaunchEmptyNotepad();

        // =========================
        // ENTER TEXT
        // =========================

        notepadPage.EnterText(
            "Hello World");

        // =========================
        // SAVE FILE
        // =========================

        notepadPage.SaveFile(
            filePath);

        // =========================
        // AFTER VALIDATION
        // =========================

        ValidateAfterTest(
            filePath,
            "Hello World");
    }
    catch (Exception ex)
    {
        FrameworkExceptionHandler.HandleFailure(
            "VerifySaveFile failed.",
            ex);
    }
}

    [Test]
    [Category("P0")]
[Order(2)]

[TestCase(
    @"C:\Temp\TestFiles\AppendText_FlaUI.txt",
    " FlaUI and C#")]

public void VerifyAppendText(
    string filePath,
    string appendText)
{
    try
    {
        // =========================
        // BEFORE VALIDATION
        // =========================

        ValidateBeforeTest(
            filePath,
            "Hello World");

        // =========================
        // LAUNCH
        // =========================

        var notepadPage =
            LaunchNotepad(
                filePath);

        // =========================
        // APPEND
        // =========================

        notepadPage.AppendText(
            appendText);

        // =========================
        // SAVE
        // =========================

        notepadPage.SaveExistingFile();

        // =========================
        // AFTER VALIDATION
        // =========================

        ValidateAfterTest(
            filePath,
            $"Hello World{appendText}");
    }
    catch (Exception ex)
    {
        FrameworkExceptionHandler.HandleFailure(
            "VerifyAppendText failed.",
            ex);
    }
}

        [Test]
        [Category("P0")]
[Order(3)]

public void VerifyClearText()
{
    string filePath =
        @"C:\Temp\TestFiles\ClearText.txt";

    try
    {
        // =========================
        // BEFORE VALIDATION
        // =========================

        ValidateBeforeTest(
            filePath,
            "Hello World");

        // =========================
        // LAUNCH
        // =========================

        var notepadPage =
            LaunchNotepad(
                filePath);

        // =========================
        // CLEAR
        // =========================

        notepadPage.SelectAllText();

        notepadPage.DeleteSelectedText();

        // =========================
        // SAVE
        // =========================

        notepadPage.SaveExistingFile();

        // =========================
        // AFTER VALIDATION
        // =========================

        ValidateAfterTest(
            filePath,
            string.Empty);
    }
    catch (Exception ex)
    {
        FrameworkExceptionHandler.HandleFailure(
            "VerifyClearText failed.",
            ex);
    }
}
    }
}