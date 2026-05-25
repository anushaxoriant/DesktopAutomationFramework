using DesktopAutomationFramework.Utilities;
using Allure.NUnit;
using Allure.Net.Commons;
using NUnit.Framework;

namespace DesktopAutomationFramework.Tests.Negative
{
    [TestFixture]
    [AllureNUnit]


    public class NotepadNegativeTests
        : BaseTest
    {
        // =========================
        // NEGATIVE SAVE TEST
        // =========================

        [Test]

[Category("P3")]
[Order(1)]        
public void VerifySaveFile_InvalidPath()
        {
            string invalidPath =
                @"Z:\InvalidFolder\Test.txt";

            try
            {
                // Launch

                var notepadPage =
                    LaunchEmptyNotepad();

                // Enter text

                notepadPage.EnterText(
                    "Hello World");

                // Validate exception

                var ex =
                    Assert.Throws<Exception>(
                        (Action)(() =>
                            notepadPage.SaveFile(
                                invalidPath)));

                Assert.That(
                    ex!.Message,
                    Does.Contain(
                        "Save"));

                // Validate file not created

                Assert.That(
                    File.Exists(
                        invalidPath),
                    Is.False);

                LoggerHelper.Log(
                    "Negative Save validation completed successfully");
            }
            catch (Exception ex)
            {
                FrameworkExceptionHandler.HandleFailure(
                    "VerifySaveFile_InvalidPath failed.",
                    ex);
            }
        }

        // =========================
        // NEGATIVE APPEND TEST
        // =========================

        [Test]

[Category("P3")]
[Order(2)]        
public void VerifyAppendText_EmptyText()
        {
            string filePath =
                @"C:\Temp\TestFiles\Negative\AppendText_Empty.txt";

            try
            {
                // Before validation

                ValidateBeforeTest(
                    filePath,
                    "Hello World");

                // Launch

                var notepadPage =
                    LaunchNotepad(
                        filePath);

                // Validate exception

                var ex =
                    Assert.Throws<Exception>(
                        (Action)(() =>
                            notepadPage.AppendText(
                                string.Empty)));

                Assert.That(
                    ex!.Message,
                    Does.Contain(
                        "empty"));

                // After validation

                ValidateAfterTest(
                    filePath,
                    "Hello World");

                LoggerHelper.Log(
                    "Negative Append validation completed successfully");
            }
            catch (Exception ex)
            {
                FrameworkExceptionHandler.HandleFailure(
                    "VerifyAppendText_EmptyText failed.",
                    ex);
            }
        }

        // =========================
        // NEGATIVE FIND TEST
        // =========================
        [Test]
        [Category("P3")]
[Order(3)]
        [TestCase(
            "INVALID_TEXT")]
        public void VerifyFindFeature_TextNotFound(
            string invalidText)
        {
            string filePath =
                @"C:\Temp\TestFiles\Negative\FindText_Invalid.txt";

            try
            {
                // Before validation

                ValidateBeforeTest(
                    filePath,
                    "Hello World FlaUI");

                // Launch

                var notepadPage =
                    LaunchNotepad(
                        filePath);

                // Open find

                var findWindow =
                    notepadPage.OpenFindDialog();

                // Validate exception

                var ex =
    Assert.Throws<Exception>(
        (Action)(() =>
        {
            notepadPage.PerformFind(
                findWindow,
                invalidText);

            notepadPage.ValidateFindExecution(
                invalidText);
        }));

Assert.That(
    ex!.Message,
    Does.Contain(
        invalidText));

Assert.That(
    ex!.Message,
    Does.Contain(
        "does not exist"));

ValidateAfterTest(
    filePath,
    "Hello World FlaUI");

                // After validation

                ValidateAfterTest(
                    filePath,
                    "Hello World FlaUI");

                LoggerHelper.Log(
                    "Negative Find validation completed successfully");
            }
            catch (Exception ex)
            {
                FrameworkExceptionHandler.HandleFailure(
                    "VerifyFindFeature_TextNotFound failed.",
                    ex);
            }
        }

        // =========================
        // NEGATIVE REPLACE TEST
        // =========================
        [Test]
        [TestCase(
            "INVALID_TEXT")]

        [Category("P3")]
[Order(4)] 

        public void VerifyReplaceFeature_TextNotFound(
            string invalidText)
        {
            string filePath =
                @"C:\Temp\TestFiles\Negative\ReplaceText_Invalid.txt";

            try
            {
                // Before validation

                ValidateBeforeTest(
                    filePath,
                    "Hello World");

                // Launch

                var notepadPage =
                    LaunchNotepad(
                        filePath);

                // Open replace

                var replaceWindow =
                    notepadPage.OpenReplaceDialog();

                // Validate exception

                var ex =
                    Assert.Throws<Exception>(
                        (Action)(() =>
                            notepadPage.PerformReplace(
                                replaceWindow,
                                invalidText,
                                "FlaUI")));

                Assert.That(
                    ex!.Message,
                    Does.Contain(
                        "not found"));

                // After validation

                ValidateAfterTest(
                    filePath,
                    "Hello World");

                LoggerHelper.Log(
                    "Negative Replace validation completed successfully");
            }
            catch (Exception ex)
            {
                FrameworkExceptionHandler.HandleFailure(
                    "VerifyReplaceFeature_TextNotFound failed.",
                    ex);
            }
        }

        // =========================
        // NEGATIVE CLEAR TEST
        // =========================

        [Test]

[Category("P3")]
[Order(5)] 
        public void VerifyClearText_AlreadyEmpty()
        {
            string filePath =
                @"C:\Temp\TestFiles\Negative\ClearText_Empty.txt";

            try
            {
                // Before validation

                ValidateBeforeTest(
                    filePath,
                    string.Empty);

                // Launch

                var notepadPage =
                    LaunchNotepad(
                        filePath);

                // Validate exception

                var ex =
                    Assert.Throws<Exception>(
                        (Action)(() =>
                            notepadPage.DeleteSelectedText()));

                Assert.That(
                    ex!.Message,
                    Does.Contain(
                        "Delete"));

                // After validation

                ValidateAfterTest(
                    filePath,
                    string.Empty);

                LoggerHelper.Log(
                    "Negative Clear validation completed successfully");
            }
            catch (Exception ex)
            {
                FrameworkExceptionHandler.HandleFailure(
                    "VerifyClearText_AlreadyEmpty failed.",
                    ex);
            }
        }

        // =========================
        // NEGATIVE UNDO TEST
        // =========================

        [Test]

[Category("P3")]
[Order(6)] 
        public void VerifyUndoFeature_NoChanges()
        {
            string filePath =
                @"C:\Temp\TestFiles\Negative\UndoText_Empty.txt";

            try
            {
                // Before validation

                ValidateBeforeTest(
                    filePath,
                    "Hello World");

                // Launch

                var notepadPage =
                    LaunchNotepad(
                        filePath);

                // Validate exception

                var ex =
                    Assert.Throws<Exception>(
                        (Action)(() =>
                            notepadPage.PerformUndo()));

                Assert.That(
                    ex!.Message,
                    Does.Contain(
                        "Undo"));

                // After validation

                ValidateAfterTest(
                    filePath,
                    "Hello World");

                LoggerHelper.Log(
                    "Negative Undo validation completed successfully");
            }
            catch (Exception ex)
            {
                FrameworkExceptionHandler.HandleFailure(
                    "VerifyUndoFeature_NoChanges failed.",
                    ex);
            }
        }
    }
}