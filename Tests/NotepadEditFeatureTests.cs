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

    public class NotepadEditFeatureTests
        : BaseTest
    {
        [Test]

[TestCase(
    @"C:\Temp\TestFiles\Undo_FlaUI.txt",
    " FlaUI and C#")]

[Category("P2")]
[Order(1)]

public void VerifyUndoFeature(
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
        // UNDO
        // =========================

        notepadPage.PerformUndo();

        // =========================
        // SAVE
        // =========================

        notepadPage.SaveExistingFile();

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
            "VerifyUndoFeature failed.",
            ex);
    }
}
    }
}