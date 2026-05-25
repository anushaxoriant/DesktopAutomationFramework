using Allure.Net.Commons;

using DesktopAutomationFramework.Utilities;

using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;

using FlaUI.UIA3;

namespace DesktopAutomationFramework.Pages
{
    public class NotepadPage
    {
        private readonly Window window;

        public NotepadPage(
            FlaUI.Core.Application app,
            UIA3Automation automation)
        {
            try
            {
                LoggerHelper.Log(
                    "Initializing Notepad page");

                if (app == null)
                {
                    throw new Exception(
                        "Application instance is null.");
                }

                if (automation == null)
                {
                    throw new Exception(
                        "Automation instance is null.");
                }

                WaitHelper.WaitUntil(
                    () =>
                        app.GetMainWindow(
                            automation) != null,

                    "Main window not found",

                    10);

                window =
                    app.GetMainWindow(
                        automation)

                    ?? throw new Exception(
                        "Window not found");

                WindowHelper.VerifyWindow(
                    window);

                InitializeWindow();

                LoggerHelper.Log(
                    "Notepad page initialized successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Notepad page initialization failed. {ex.Message}");
            }
        }

        public Window Window =>
            window;

        public FlaUI.Core.AutomationElements.TextBox Editor =>
            FindElement(
                cf => cf.ByAutomationId(
                    "15"),

                "Editor textbox not found")

            .AsTextBox();

        // =========================
        // INITIALIZE WINDOW
        // =========================

        private void InitializeWindow()
        {
            try
            {
                AllureApi.Step(
                    "Initializing window");

                window.Focus();

                window.Patterns
                    .Window.Pattern
                    .SetWindowVisualState(
                        FlaUI.Core.Definitions.WindowVisualState.Maximized);

                WaitHelper.ApplyDelay();

                LoggerHelper.Log(
                    "Window initialized successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Window initialization failed. {ex.Message}");
            }
        }

        // =========================
        // FIND ELEMENT
        // =========================

        private AutomationElement FindElement(
            Func<ConditionFactory, ConditionBase> condition,
            string failureMessage)
        {
            return window.FindFirstDescendant(
                condition)

                ?? throw new Exception(
                    failureMessage);
        }

        // =========================
        // FIND ELEMENT IN WINDOW
        // =========================

        private AutomationElement FindElement(
            Window parentWindow,
            Func<ConditionFactory, ConditionBase> condition,
            string failureMessage)
        {
            return parentWindow.FindFirstDescendant(
                condition)

                ?? throw new Exception(
                    failureMessage);
        }

        // =========================
        // CLICK MENU
        // =========================

        private void ClickMenu(
            string parentMenu,
            string childMenu)
        {
            try
            {
                AllureApi.Step(
                    $"Opening menu {parentMenu} -> {childMenu}");

                var parent =
                    FindElement(
                        cf => cf.ByName(
                            parentMenu),

                        $"Parent menu not found: {parentMenu}")

                    .AsMenuItem();

                UIInteractionHelper.ClickElement(
                    parent);

                var desktop =
                    window.Automation
                    .GetDesktop();

                WaitHelper.WaitUntil(
                    () =>
                        desktop.FindFirstDescendant(
                            cf => cf.ByName(
                                childMenu)) != null,

                    $"Child menu not found: {childMenu}",

                    10);

                var child =
                    desktop.FindFirstDescendant(
                        cf => cf.ByName(
                            childMenu))

                    ?.AsMenuItem()

                    ?? throw new Exception(
                        $"Child menu not found: {childMenu}");

                UIInteractionHelper.ClickElement(
                    child);

                WaitHelper.ApplyDelay();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Menu click failed. {ex.Message}");
            }
        }

        // =========================
        // CLICK BUTTON
        // =========================

        private void ClickButton(
            Window parentWindow,
            string buttonName)
        {
            try
            {
                var button =
                    FindElement(
                        parentWindow,

                        cf => cf.ByName(
                            buttonName),

                        $"{buttonName} button not found")

                    .AsButton();

                UIInteractionHelper.ClickElement(
                    button);

                LoggerHelper.Log(
                    $"{buttonName} button clicked successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Failed to click button {buttonName}. {ex.Message}");
            }
        }

        // =========================
        // ENTER TEXT
        // =========================

        public void EnterText(
            string text)
        {
            try
            {
                AllureApi.Step(
                    "Entering text");

                Editor.Focus();

                WaitHelper.ApplyDelay();

                Editor.Enter(
                    text);

                WaitHelper.ApplyDelay();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Enter text failed. {ex.Message}");
            }
        }

        public void AppendText(
    string text)
{
    try
    {
        // =========================
        // VALIDATE INPUT
        // =========================

        if (string.IsNullOrWhiteSpace(
            text))
        {
            LoggerHelper.Log(
    "Append validation failed. Empty text detected.");

WaitHelper.ApplyDelay();
            throw new Exception(
                "Append text is null or empty.");
        }

        LoggerHelper.Log(
            $"Appending text: {text}");

        // =========================
        // FOCUS EDITOR
        // =========================

        Editor.Focus();

        WaitHelper.ApplyDelay();

        // =========================
        // MOVE CURSOR TO END
        // =========================

        Keyboard.Press(VirtualKeyShort.END);

        WaitHelper.ApplyDelay();

        // =========================
        // APPEND TEXT
        // =========================

        Keyboard.Type(
            text);

        WaitHelper.ApplyDelay();

        // =========================
        // VALIDATE APPEND
        // =========================

        string editorContent =
            Editor.Text;

        if (!editorContent.Contains(
            text))
        {
            throw new Exception(
                $"Append operation failed. Text '{text}' was not appended.");
        }

        LoggerHelper.Log(
            "Append operation completed successfully");
    }
    catch (Exception ex)
    {
        throw new Exception(
            $"AppendText failed. {ex.Message}");
    }
}

        public void SaveFile(
    string filePath)
{
    try
    {
        LoggerHelper.Log(
            $"Saving file: {filePath}");

        // =========================
        // VALIDATE INPUT
        // =========================

        if (string.IsNullOrWhiteSpace(
            filePath))
        {
            throw new Exception(
                "File path is null or empty.");
        }

        // =========================
        // CLICK FILE -> SAVE
        // =========================

        ClickMenu(
            "File",
            "Save");

        // =========================
        // WAIT FOR SAVE DIALOG
        // =========================

        WaitHelper.WaitUntil(
    () =>
        window.ModalWindows.Length > 0,
    "Save dialog not found",
    10);

        // =========================
        // SAVE WINDOW
        // =========================

        var saveWindow =
            window.ModalWindows[0];

        // =========================
        // FILE NAME TEXTBOX
        // =========================

        var fileNameBox =
            saveWindow.FindFirstDescendant(
                cf => cf.ByAutomationId(
                    "1001"))

            ?.AsTextBox()

            ?? saveWindow.FindFirstDescendant(
                cf => cf.ByName(
                    "File name:"))

            ?.AsTextBox()

            ?? throw new Exception(
                "File name textbox not found");

        // =========================
        // ENTER FILE PATH
        // =========================

        fileNameBox.Focus();

        WaitHelper.ApplyDelay();

        fileNameBox.Text =
            string.Empty;

        WaitHelper.ApplyDelay();

        fileNameBox.Enter(
            filePath);

        WaitHelper.ApplyDelay();

        // =========================
        // SAVE BUTTON
        // =========================

        ClickButton(
            saveWindow,
            "Save");

        WaitHelper.ApplyDelay();

        // =========================
        // HANDLE INVALID PATH POPUP
        // =========================

        var invalidPathPopup =
            window.ModalWindows
                .FirstOrDefault(
                    w =>
                        w.FindFirstDescendant(
                            cf => cf.ByText(
                                "Check the file name and try again")) != null);

        if (invalidPathPopup != null)
        {
            LoggerHelper.Log(
                $"Invalid save path detected: {filePath}");

            var okButton =
                invalidPathPopup.FindFirstDescendant(
                    cf => cf.ByName(
                        "OK"))

                ?.AsButton();

            if (okButton != null)
            {
                okButton.Patterns
                    .Invoke.Pattern
                    .Invoke();

                WaitHelper.ApplyDelay();
            }

            throw new Exception(
                $"Save failed. Invalid path: {filePath}");
        }

        // =========================
        // VALIDATE FILE SAVED
        // =========================

        WaitHelper.WaitUntil(
    () => File.Exists(
        filePath),
    $"File was not saved: {filePath}",
    10);

        LoggerHelper.Log(
            "File saved successfully");
    }
    catch (Exception ex)
    {
        throw new Exception(
            $"SaveFile failed. {ex.Message}");
    }
}

        // =========================
        // SAVE EXISTING FILE
        // =========================

        public void SaveExistingFile()
        {
            try
            {
                AllureApi.Step(
                    "Saving existing file");

                ClickMenu(
                    "File",
                    "Save");

                WaitHelper.ApplyDelay();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Save existing file failed. {ex.Message}");
            }
        }

        // =========================
        // SELECT ALL TEXT
        // =========================

        public void SelectAllText()
        {
            try
            {
                AllureApi.Step(
                    "Selecting all text");

                ClickMenu(
                    "Edit",
                    "Select All");

                WaitHelper.ApplyDelay();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Select all text failed. {ex.Message}");
            }
        }

        // =========================
        // DELETE SELECTED TEXT
        // =========================

        public void DeleteSelectedText()
{
    try
    {
        LoggerHelper.Log(
            "Deleting selected text");

        // =========================
        // VALIDATE CONTENT EXISTS
        // =========================

        string editorContent =
            Editor.Text;

        if (string.IsNullOrWhiteSpace(
            editorContent))
        {
            LoggerHelper.Log(
    "Delete validation failed. Editor already empty.");

WaitHelper.ApplyDelay();
            throw new Exception(
                "Editor is already empty.");
        }

        // =========================
        // DELETE TEXT
        // =========================

        ClickMenu(
            "Edit",
            "Delete");

        WaitHelper.ApplyDelay();

        // =========================
        // VALIDATE DELETE
        // =========================

        string updatedContent =
            Editor.Text;

        if (!string.IsNullOrWhiteSpace(
            updatedContent))
        {
            throw new Exception(
                "Delete operation failed. Editor still contains text.");
        }

        LoggerHelper.Log(
            "Delete operation completed successfully");
    }
    catch (Exception ex)
    {
        throw new Exception(
            $"DeleteSelectedText failed. {ex.Message}");
    }
}

        // =========================
        // PERFORM UNDO
        // =========================

        public void PerformUndo()
        {
            try
            {
                AllureApi.Step(
                    "Performing Undo");

                ClickMenu(
                    "Edit",
                    "Undo");

                WaitHelper.ApplyDelay();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Undo operation failed. {ex.Message}");
            }
        }

        // =========================
        // OPEN FIND DIALOG
        // =========================

        public Window OpenFindDialog()
        {
            try
            {
                AllureApi.Step(
                    "Opening Find dialog");

                ClickMenu(
                    "Edit",
                    "Find...");

                return GetDialogWindow(
                    "Find");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Open Find dialog failed. {ex.Message}");
            }
        }

        // =========================
        // VALIDATE FIND DIALOG
        // =========================

        public void ValidateFindDialogDefaults()
        {
            try
            {
                var findWindow =
                    GetDialogWindow(
                        "Find");

                FindElement(
                    findWindow,
                    cf => cf.ByAutomationId(
                        "1152"),
                    "Find textbox not found");

                FindElement(
                    findWindow,
                    cf => cf.ByName(
                        "Find Next"),
                    "Find Next button not found");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Find dialog validation failed. {ex.Message}");
            }
        }

        // =========================
        // PERFORM FIND
        // =========================

        public void PerformFind(
            Window findWindow,
            string textToFind)
        {
            try
            {
                AllureApi.Step(
                    $"Finding text: {textToFind}");

                var findTextBox =
                    FindElement(
                        findWindow,
                        cf => cf.ByAutomationId(
                            "1152"),
                        "Find textbox not found")

                    .AsTextBox();

                UIInteractionHelper.EnterTextIntoTextBox(
                    findTextBox,
                    textToFind,
                    "Find");

                ClickButton(
                    findWindow,
                    "Find Next");

                WaitHelper.ApplyDelay();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Perform Find failed. {ex.Message}");
            }
        }

        // =========================
        // VALIDATE FIND EXECUTION
        // =========================

        public void ValidateFindExecution(
    string searchText)
{
    try
    {
        // =========================
        // VALIDATE INPUT
        // =========================

        if (string.IsNullOrWhiteSpace(
            searchText))
        {
            throw new Exception(
                "Search text is null or empty.");
        }

        // =========================
        // HANDLE CANNOT FIND POPUP
        // =========================

        var cannotFindPopup =
            window.ModalWindows
                .FirstOrDefault(
                    w => w.Title.Contains(
                        "Notepad"));

        if (cannotFindPopup != null)
        {
            LoggerHelper.Log(
                $"Search text not found: {searchText}");

            var okButton =
                cannotFindPopup.FindFirstDescendant(
                    cf => cf.ByName(
                        "OK"))

                ?.AsButton();

            if (okButton != null)
            {
                okButton.Patterns
    .Invoke.Pattern
    .Invoke();
                WaitHelper.ApplyDelay();
            }

            throw new Exception(
                $"Search text '{searchText}' does not exist in editor.");
        }

        // =========================
        // VALIDATE EDITOR CONTENT
        // =========================

        string editorContent =
            Editor.Text;

        if (!editorContent.Contains(
            searchText))
        {
            throw new Exception(
                $"Search text '{searchText}' does not exist in editor.");
        }

        LoggerHelper.Log(
            $"Search validation successful for: {searchText}");
    }
    catch (Exception ex)
    {
        throw new Exception(
            $"Validate Find execution failed. {ex.Message}");
    }
}

        // =========================
        // OPEN REPLACE DIALOG
        // =========================

        public Window OpenReplaceDialog()
        {
            try
            {
                AllureApi.Step(
                    "Opening Replace dialog");

                ClickMenu(
                    "Edit",
                    "Replace...");

                return GetDialogWindow(
                    "Replace");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Open Replace dialog failed. {ex.Message}");
            }
        }

        // =========================
        // VALIDATE REPLACE DIALOG
        // =========================

        public void ValidateReplaceDialogDefaults()
        {
            try
            {
                var replaceWindow =
                    GetDialogWindow(
                        "Replace");

                FindElement(
                    replaceWindow,
                    cf => cf.ByAutomationId(
                        "1152"),
                    "Find textbox not found");

                FindElement(
                    replaceWindow,
                    cf => cf.ByAutomationId(
                        "1153"),
                    "Replace textbox not found");

                FindElement(
                    replaceWindow,
                    cf => cf.ByName(
                        "Replace"),
                    "Replace button not found");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Replace dialog validation failed. {ex.Message}");
            }
        }

        // =========================
        // PERFORM REPLACE
        // =========================

        public void PerformReplace(
            Window replaceWindow,
            string findText,
            string replaceText)
        {try
{
    AllureApi.Step(
        $"Replacing {findText} with {replaceText}");

    var findTextBox =
        FindElement(
            replaceWindow,
            cf => cf.ByAutomationId(
                "1152"),
            "Find textbox not found")

        .AsTextBox();

    var replaceTextBox =
        FindElement(
            replaceWindow,
            cf => cf.ByAutomationId(
                "1153"),
            "Replace textbox not found")

        .AsTextBox();

    UIInteractionHelper.EnterTextIntoTextBox(
        findTextBox,
        findText,
        "Find");

    UIInteractionHelper.EnterTextIntoTextBox(
        replaceTextBox,
        replaceText,
        "Replace");

    // =========================
    // CLICK FIND NEXT
    // =========================

    ClickButton(
        replaceWindow,
        "Find Next");

    WaitHelper.ApplyDelay();

    // =========================
// HANDLE TEXT NOT FOUND
// =========================

var cannotFindPopup =
    window.ModalWindows
        .FirstOrDefault(
            w =>
                w.FindFirstDescendant(
                    cf => cf.ByText(
                        $"Cannot find \"{findText}\"")) != null);

if (cannotFindPopup != null)
{
    LoggerHelper.Log(
        $"Replace text not found: {findText}");

    var okButton =
        cannotFindPopup.FindFirstDescendant(
            cf => cf.ByName(
                "OK"))

        ?.AsButton();

    if (okButton != null)
    {
        okButton.Patterns
            .Invoke.Pattern
            .Invoke();

        WaitHelper.ApplyDelay();
    }

    throw new Exception(
        $"Replace operation failed. Text '{findText}' was not found.");
}

    // =========================
    // CLICK REPLACE
    // =========================

    ClickButton(
        replaceWindow,
        "Replace");

    WaitHelper.ApplyDelay();

    // =========================
    // HANDLE COMPLETION POPUP
    // =========================

    HandleReplaceCompletionPopup();

    // =========================
    // CLOSE DIALOG
    // =========================

    CloseReplaceDialog(
        replaceWindow);
}
catch (Exception ex)
{
    throw new Exception(
        $"Perform Replace failed. {ex.Message}");
}}

        // =========================
        // CLOSE REPLACE DIALOG
        // =========================

        public void CloseReplaceDialog(
            Window replaceWindow)
        {
            try
            {
                replaceWindow.Close();

                WaitHelper.ApplyDelay();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Close Replace dialog failed. {ex.Message}");
            }
        }

        // =========================
        // GET DIALOG WINDOW
        // =========================

        private Window GetDialogWindow(
            string dialogName)
        {
            try
            {
                var desktop =
                    window.Automation
                    .GetDesktop();

                WaitHelper.WaitUntil(
                    () =>
                        desktop.FindFirstDescendant(
                            cf => cf.ByName(
                                dialogName)) != null,

                    $"{dialogName} dialog not found",

                    10);

                var dialog =
                    desktop.FindFirstDescendant(
                        cf => cf.ByName(
                            dialogName))

                    ?.AsWindow()

                    ?? throw new Exception(
                        $"{dialogName} dialog not found");

                dialog.Focus();

                WaitHelper.ApplyDelay();

                return dialog;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Get dialog window failed. {ex.Message}");
            }
        }

        // =========================
// HANDLE REPLACE COMPLETION POPUP
// =========================

private void HandleReplaceCompletionPopup()
{
    try
    {
        var desktop =
            window.Automation
            .GetDesktop();

        var popup =
            desktop.FindFirstDescendant(
                cf => cf.ByName(
                    "Notepad"))

            ?.AsWindow();

        if (popup == null)
        {
            return;
        }

        var popupText =
            popup.FindFirstDescendant(
                cf => cf.ByControlType(
                    FlaUI.Core.Definitions.ControlType.Text))

            ?.Name;

        if (popupText != null
            &&
            popupText.Contains(
                "Cannot find"))
        {
            LoggerHelper.Log(
                "Replace completed successfully. No more matches found.");

            var okButton =
                popup.FindFirstDescendant(
                    cf => cf.ByName(
                        "OK"))

                ?.AsButton();

            okButton?.Patterns
                .Invoke.Pattern
                .Invoke();

            WaitHelper.ApplyDelay();
        }
    }
    catch (Exception ex)
    {
        throw new Exception(
            $"Replace completion popup handling failed. {ex.Message}");
    }
}
    }
}