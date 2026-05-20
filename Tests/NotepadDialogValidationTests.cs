//=========================================
// NotepadDialogValidationTests.cs
//=========================================

using Allure.NUnit;
using DesktopAutomationFramework.Pages;
using DesktopAutomationFramework.Utilities;
using NUnit.Framework;

namespace DesktopAutomationFramework.Tests
{
    [TestFixture]
    [AllureNUnit]
    public class NotepadDialogValidationTests
        : BaseTest
    {
        //---------------------------------
        // TC_004
        // Verify Find Feature
        //---------------------------------

        [Test]
[TestCase(
    @"C:\Temp\TestFiles\Find_Hello.txt",
    "Hello")]

[TestCase(
    @"C:\Temp\TestFiles\Find_World.txt",
    "World")]

[TestCase(
    @"C:\Temp\TestFiles\Find_FlaUI.txt",
    "FlaUI")]

[Category("P2")]
[Order(1)]
[Description("TC_004 - Verify Find Feature")]
public void VerifyFindFeature(
    string filePath,
    string searchText)
{
    try
    {
        //---------------------------------
// BEFORE TEST
//---------------------------------

if (!File.Exists(filePath))
{
    LoggerHelper.Log(
        "Baseline file missing. Recreating.");

    File.WriteAllText(
        filePath,
        "Hello World FlaUI");
}

string baselineContent =
    File.ReadAllText(
        filePath);

if (!baselineContent.Contains(
    "Hello World FlaUI"))
{
    LoggerHelper.Log(
        "Invalid baseline detected. Restoring.");

    File.WriteAllText(
        filePath,
        "Hello World FlaUI");
}

Assert.That(
    File.Exists(filePath),
    Is.True);

Assert.That(
    File.ReadAllText(filePath),
    Does.Contain(
        "Hello"));

        //---------------------------------
        // Launch File
        //---------------------------------

        App =
            FlaUI.Core.Application.Launch(
                "notepad.exe",
                filePath);

        //---------------------------------
        // Page
        //---------------------------------

        var notepadPage =
            new NotepadPage(
                App,
                Automation!);

        //---------------------------------
        // Validate Window
        //---------------------------------

        WindowHelper.VerifyWindow(
            notepadPage.Window);

        //---------------------------------
        // TEST EXECUTION
        //---------------------------------

        var findWindow =
            notepadPage.OpenFindDialog();

        notepadPage
            .ValidateFindDialogDefaults();

        notepadPage.PerformFind(
            findWindow,
            searchText);

        notepadPage.CloseFindDialog(
            findWindow);

        //---------------------------------
        // AFTER TEST
        //---------------------------------

        LoggerHelper.Log(
            "Find validation completed");
    }
    catch (Exception ex)
    {
        LoggerHelper.Log(
            $"VerifyFindFeature failed: {ex.Message}");

        throw;
    }
}

        //---------------------------------
        // TC_005
        // Verify Replace Feature
        //---------------------------------

        [Test]
[TestCase(
    @"C:\Temp\TestFiles\Replace_World.txt",
    "World",
    "FlaUI")]

[TestCase(
    @"C:\Temp\TestFiles\Replace_Hello.txt",
    "Hello",
    "Desktop")]

[Category("P2")]
[Order(2)]
[Description("TC_005 - Verify Replace Feature")]
public void VerifyReplaceFeature(
    string filePath,
    string findText,
    string replaceText)
{
    try
    {
        //---------------------------------
// BEFORE TEST
//---------------------------------

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

        //---------------------------------
        // Launch File
        //---------------------------------

        App =
            FlaUI.Core.Application.Launch(
                "notepad.exe",
                filePath);

        //---------------------------------
        // Page
        //---------------------------------

        var notepadPage =
            new NotepadPage(
                App,
                Automation!);

        //---------------------------------
        // Validate Window
        //---------------------------------

        WindowHelper.VerifyWindow(
            notepadPage.Window);

        //---------------------------------
        // TEST EXECUTION
        //---------------------------------

        var replaceWindow =
            notepadPage.OpenReplaceDialog();

        notepadPage
            .ValidateReplaceDialogDefaults();

        notepadPage.PerformReplace(
            replaceWindow,
            findText,
            replaceText);

        notepadPage.CloseReplaceDialog(
            replaceWindow);

        notepadPage.SaveExistingFile();

        //---------------------------------
        // AFTER TEST
        //---------------------------------

        string content =
            File.ReadAllText(
                filePath);

        Assert.That(
            content,
            Does.Contain(
                replaceText));

        LoggerHelper.Log(
            "Replace validation completed");
    }
    catch (Exception ex)
    {
        LoggerHelper.Log(
            $"VerifyReplaceFeature failed: {ex.Message}");

        throw;
    }
}
    }
}