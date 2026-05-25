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

    public class NotepadDialogValidationTests
        : BaseTest
    {
        [Test]

[TestCase(
    @"C:\Temp\TestFiles\Find_World.txt",
    "World")]

[Category("P1")]
[Order(1)]

public void VerifyFindFeature(
    string filePath,
    string searchText)
{
    try
    {
        // =========================
        // BEFORE VALIDATION
        // =========================

        ValidateBeforeTest(
            filePath,
            "Hello World FlaUI");

        // =========================
        // LAUNCH
        // =========================

        var notepadPage =
            LaunchNotepad(
                filePath);

        // =========================
        // FIND
        // =========================

        var findWindow =
            notepadPage.OpenFindDialog();

        notepadPage.ValidateFindDialogDefaults();

        notepadPage.PerformFind(
            findWindow,
            searchText);

        // =========================
        // AFTER VALIDATION
        // =========================

        notepadPage.ValidateFindExecution(
    searchText);

        ValidateAfterTest(
            filePath,
            "Hello World FlaUI");
    }
    catch (Exception ex)
    {
        FrameworkExceptionHandler.HandleFailure(
            "VerifyFindFeature failed.",
            ex);
    }
}

        [Test]
        [Category("P1")]
[Order(2)]

[TestCase(
    @"C:\Temp\TestFiles\Replace_World.txt",
    "World",
    "FlaUI")]
public void VerifyReplaceFeature(
    string filePath,
    string findText,
    string replaceText)
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
        // REPLACE
        // =========================

        var replaceWindow =
            notepadPage.OpenReplaceDialog();

        notepadPage.PerformReplace(
            replaceWindow,
            findText,
            replaceText);

        // =========================
        // SAVE
        // =========================

        notepadPage.SaveExistingFile();

        // =========================
// AFTER VALIDATION
// =========================

string expectedContent =
    "Hello World"
        .Replace(
            findText,
            replaceText);

ValidateAfterTest(
    filePath,
    expectedContent);
    }
    catch (Exception ex)
    {
        FrameworkExceptionHandler.HandleFailure(
            "VerifyReplaceFeature failed.",
            ex);
    }
}
    }
}