using System.Linq;
using DesktopAutomationFramework.Utilities;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.UIA3;
using NUnit.Framework;

namespace DesktopAutomationFramework.Pages
{
    public class NotepadPage
    {
        private readonly Window _window;

        // Constructor

        public NotepadPage(
            FlaUI.Core.Application app,
            UIA3Automation automation)
        {
            LoggerHelper.Log(
                "Initializing Notepad page");

            WaitHelper.WaitUntil(
                () =>
                    app.GetMainWindow(
                        automation) != null,
                10,
                "Main window not found");

            _window =
                app.GetMainWindow(
                    automation)

                ?? throw new Exception(
                    "Window not found");

            WindowHelper.VerifyWindow(
                _window);

            InitializeWindow();

            LoggerHelper.Log(
                "Notepad page initialized");
        }

        //---------------------------------
// Public Window
//---------------------------------

public Window Window =>
    _window;

        private void ClickElement(
    AutomationElement? element)
{
    if (element == null)
    {
        throw new Exception(
            "Element is null");
    }

    //---------------------------------
    // Wait For Element
    //---------------------------------

    WaitHelper.WaitForElement(
        element);

    //---------------------------------
    // Get Click Point
    //---------------------------------

    var point =
        element.GetClickablePoint();

    //---------------------------------
    // Move Mouse
    //---------------------------------

    Mouse.MoveTo(
        point);

    ApplyDemoDelay();

    //---------------------------------
    // Click
    //---------------------------------

    Mouse.Click(
        point);

    ApplyDemoDelay();
}

        // Initialize window

        public void InitializeWindow()
        {
            LoggerHelper.Log(
                "Maximizing window");

            _window.Focus();

            _window.Patterns
                .Window.Pattern
                .SetWindowVisualState(
                    WindowVisualState.Maximized);

            ApplyDemoDelay();

            LoggerHelper.Log(
                "Window maximized");
        }

        // Editor

        public FlaUI.Core.AutomationElements.TextBox Editor =>
            _window.FindFirstDescendant(cf =>
                cf.ByControlType(
                    ControlType.Document))

            ?.AsTextBox()

            ?? throw new Exception(
                "Editor not found");

        // Delay

        private void ApplyDemoDelay()
        {
            Task.Delay(700)
                .Wait();
        }

        // Click element

        private void ClickMenu(
    string parentMenu,
    string childMenu)
{
    LoggerHelper.Log(
        $"Opening menu: {parentMenu}");

    //---------------------------------
    // Parent Menu
    //---------------------------------

    var parent =
        _window.FindFirstDescendant(cf =>
            cf.ByName(parentMenu))

        ?.AsMenuItem()

        ?? throw new Exception(
            $"Parent menu not found: {parentMenu}");

    //---------------------------------
    // Parent Click Point
    //---------------------------------

    var parentPoint =
        parent.GetClickablePoint();

    //---------------------------------
    // Click Parent Menu
    //---------------------------------

    Mouse.MoveTo(
        parentPoint);

    ApplyDemoDelay();

    Mouse.Click(
        parentPoint);

    ApplyDemoDelay();

    LoggerHelper.Log(
        $"{parentMenu} menu clicked");

    //---------------------------------
    // Desktop
    //---------------------------------

    var desktop =
        _window.Automation
        .GetDesktop();

    //---------------------------------
    // Wait For Child Menu
    //---------------------------------

    WaitHelper.WaitUntil(
        () =>
            desktop.FindFirstDescendant(cf =>
                cf.ByName(childMenu)) != null,
        10,
        $"Child menu not found: {childMenu}");

    //---------------------------------
    // Child Menu
    //---------------------------------

    var child =
        desktop.FindFirstDescendant(cf =>
            cf.ByName(childMenu))

        ?.AsMenuItem()

        ?? throw new Exception(
            $"Child menu not found: {childMenu}");

    //---------------------------------
    // Invoke Child Menu
    //---------------------------------

    child.Invoke();

    ApplyDemoDelay();

    LoggerHelper.Log(
        $"Clicked menu: {parentMenu} -> {childMenu}");
}

        // Enter text

        public void EnterText(
            string text)
        {
            LoggerHelper.Log(
                $"Entering text: {text}");

            Editor.Focus();

            foreach (char character in text)
            {
                Keyboard.Type(
                    character);

                ApplyDemoDelay();
            }

            LoggerHelper.Log(
                "Text entered");
        }

        private Window GetFindWindow()
{
    //---------------------------------
    // Desktop
    //---------------------------------

    var desktop =
        _window.Automation
        .GetDesktop();

    //---------------------------------
    // Wait For Find Window
    //---------------------------------

    WaitHelper.WaitUntil(
        () =>
            desktop.FindFirstDescendant(cf =>
                cf.ByName("Find")) != null,
        10,
        "Find window not found");

    //---------------------------------
    // Find Window
    //---------------------------------

    return desktop.FindFirstDescendant(cf =>
            cf.ByName("Find"))

        ?.AsWindow()

        ?? throw new Exception(
            "Find window not found");
}

private Window GetReplaceWindow()
{
    //---------------------------------
    // Desktop
    //---------------------------------

    var desktop =
        _window.Automation
        .GetDesktop();

    //---------------------------------
    // Wait For Replace Window
    //---------------------------------

    WaitHelper.WaitUntil(
        () =>
            desktop.FindFirstDescendant(cf =>
                cf.ByName("Replace")) != null,
        10,
        "Replace window not found");

    //---------------------------------
    // Replace Window
    //---------------------------------

    return desktop.FindFirstDescendant(cf =>
            cf.ByName("Replace"))

        ?.AsWindow()

        ?? throw new Exception(
            "Replace window not found");
}

        // Append text

        public void AppendText(
    string text)
{
    LoggerHelper.Log(
        $"Appending text: {text}");

    //---------------------------------
    // Focus Editor
    //---------------------------------

    Editor.Focus();

    ApplyDemoDelay();

    //---------------------------------
    // Move Cursor To End
    //---------------------------------

    Mouse.Click(
        Editor.GetClickablePoint());

    ApplyDemoDelay();

    //---------------------------------
    // Type Text Normally
    //---------------------------------

    foreach (char character in text)
    {
        Keyboard.Type(
            character);

        ApplyDemoDelay();
    }

    LoggerHelper.Log(
        "Text appended");
}

        // Save file

        public void SaveFile(
            string filePath)
        {
            LoggerHelper.Log(
                $"Saving file: {filePath}");

            //---------------------------------
            // File -> Save
            //---------------------------------

            ClickMenu(
                "File",
                "Save");

            //---------------------------------
            // Wait For Save Dialog
            //---------------------------------

            WaitHelper.WaitUntil(
                () => _window.ModalWindows.Length > 0,
                10,
                "Save dialog not found");

            //---------------------------------
            // Save Window
            //---------------------------------

            var saveWindow =
                _window.ModalWindows[0];

            LoggerHelper.Log(
                "Save dialog opened");

            ApplyDemoDelay();

            //---------------------------------
            // File Name TextBox
            //---------------------------------

           //---------------------------------
// File Name TextBox
//---------------------------------

var fileNameBox =
    saveWindow.FindFirstDescendant(cf =>
        cf.ByAutomationId(
            "1001"))

    ?.AsTextBox()

    ?? throw new Exception(
        "File name textbox not found");

LoggerHelper.Log(
    "File name textbox located");

ApplyDemoDelay();

//---------------------------------
// Enter File Path
//---------------------------------

fileNameBox.Focus();

ApplyDemoDelay();

fileNameBox.Text =
    string.Empty;

ApplyDemoDelay();

fileNameBox.Enter(
    filePath);

ApplyDemoDelay();

LoggerHelper.Log(
    $"Entered file path: {filePath}");

            LoggerHelper.Log(
                $"Entered path: {filePath}");

            //---------------------------------
            // Save Button
            //---------------------------------

            var saveButton =
                saveWindow.FindFirstDescendant(cf =>
                    cf.ByName("Save"))

                ?.AsButton()

                ?? throw new Exception(
                    "Save button not found");

            //---------------------------------
            // Click Save
            //---------------------------------

            saveButton.Invoke();

            LoggerHelper.Log(
                "Clicked Save button");

            //---------------------------------
            // Validate Save
            //---------------------------------

            WaitHelper.WaitUntil(
                () => File.Exists(
                    filePath),
                10,
                "File not saved");

            LoggerHelper.Log(
                "File saved successfully");
        }

        // Save existing file

        public void SaveExistingFile()
        {
            LoggerHelper.Log(
                "Saving existing file");

            ClickMenu(
                "File",
                "Save");

            ApplyDemoDelay();

            LoggerHelper.Log(
                "Existing file saved");
        }

        // Select all text

        public void SelectAllText()
        {
            LoggerHelper.Log(
                "Selecting all text");

            ClickMenu(
                "Edit",
                "Select All");

            ApplyDemoDelay();

            LoggerHelper.Log(
                "All text selected");
        }

        // Delete selected text

        public void DeleteSelectedText()
        {
            LoggerHelper.Log(
                "Deleting selected text");

            ClickMenu(
                "Edit",
                "Delete");

            ApplyDemoDelay();

            LoggerHelper.Log(
                "Selected text deleted");
        }

        // Perform undo

        public void PerformUndo()
{
    LoggerHelper.Log(
        "Performing Undo");

    ClickMenu(
        "Edit",
        "Undo");

    ApplyDemoDelay();

    LoggerHelper.Log(
        "Undo completed");
}


        // Open find dialog
public Window OpenFindDialog()
{
    LoggerHelper.Log(
        "Opening Find dialog");

    //---------------------------------
    // Open Menu
    //---------------------------------

    ClickMenu(
        "Edit",
        "Find...");

    //---------------------------------
    // Stabilization Delay
    //---------------------------------

    Task.Delay(2000)
        .Wait();

    //---------------------------------
    // Desktop
    //---------------------------------

    var desktop =
        _window.Automation
        .GetDesktop();

    //---------------------------------
    // Find Window
    //---------------------------------

    var findWindow =
        desktop.FindFirstDescendant(cf =>
            cf.ByName("Find"))

        ?.AsWindow()

        ?? throw new Exception(
            "Find dialog not found");

    //---------------------------------
    // Focus Window
    //---------------------------------

    findWindow.Focus();

    ApplyDemoDelay();

    LoggerHelper.Log(
        "Find dialog opened");

    return findWindow;
}

        // Validate find dialog defaults

        public void ValidateFindDialogDefaults()
{
    var findWindow =
        GetDialogWindow(
            "Find");

    Assert.That(
        findWindow,
        Is.Not.Null);
}

        // Perform find

        public void PerformFind(
    Window findWindow,
    string textToFind)
{
    LoggerHelper.Log(
        $"Finding text: {textToFind}");

    //---------------------------------
    // Edit Controls
    //---------------------------------

    var editControls =
        findWindow.FindAllChildren(cf =>
            cf.ByControlType(
                ControlType.Edit));

    if (editControls.Length == 0)
    {
        throw new Exception(
            "Find textbox not found");
    }

    //---------------------------------
    // Find TextBox
    //---------------------------------

    var findTextBox =
        editControls[0]
        .AsTextBox();

    //---------------------------------
    // Click TextBox
    //---------------------------------

    ClickElement(
        findTextBox);

    //---------------------------------
    // Type Text
    //---------------------------------

    foreach (char character in textToFind)
    {
        Keyboard.Type(
            character);

        ApplyDemoDelay();
    }

    //---------------------------------
    // Buttons
    //---------------------------------

    var buttons =
        findWindow.FindAllChildren(cf =>
            cf.ByControlType(
                ControlType.Button));

    //---------------------------------
    // Find Next Button
    //---------------------------------

    var findNextButton =
        buttons
        .FirstOrDefault(x =>
            x.Name == "Find Next")

        ?.AsButton()

        ?? throw new Exception(
            "Find Next button not found");

    //---------------------------------
    // Click Button
    //---------------------------------

    ClickElement(
        findNextButton);

    ApplyDemoDelay();

    LoggerHelper.Log(
        "Find completed");
}

        // Close find dialog

        public void CloseFindDialog(
    Window findWindow)
{
    LoggerHelper.Log(
        "Closing Find dialog");

    //---------------------------------
    // Close Window
    //---------------------------------

    findWindow.Close();

    ApplyDemoDelay();

    LoggerHelper.Log(
        "Find dialog closed");
}

        // Open replace dialog

        public Window OpenReplaceDialog()
{
    LoggerHelper.Log(
        "Opening Replace dialog");

    ClickMenu(
        "Edit",
        "Replace...");

    Task.Delay(2000)
        .Wait();

    var desktop =
        _window.Automation
        .GetDesktop();

    var replaceWindow =
        desktop.FindFirstDescendant(cf =>
            cf.ByName("Replace"))

        ?.AsWindow()

        ?? throw new Exception(
            "Replace dialog not found");

    replaceWindow.Focus();

    ApplyDemoDelay();

    LoggerHelper.Log(
        "Replace dialog opened");

    return replaceWindow;
}

        // Validate replace dialog defaults

        public void ValidateReplaceDialogDefaults()
{
    var replaceWindow =
        GetDialogWindow(
            "Replace");

    Assert.That(
        replaceWindow,
        Is.Not.Null);
}

private Window GetDialogWindow(
    string dialogName)
{
    //---------------------------------
    // Desktop
    //---------------------------------

    var desktop =
        _window.Automation
        .GetDesktop();

    //---------------------------------
    // Wait For Dialog
    //---------------------------------

    WaitHelper.WaitUntil(
        () =>
            desktop.FindFirstDescendant(cf =>
                cf.ByName(dialogName)) != null,
        10,
        $"{dialogName} dialog not found");

    //---------------------------------
    // Delay
    //---------------------------------

    Task.Delay(2000)
        .Wait();

    //---------------------------------
    // Window
    //---------------------------------

    return desktop.FindFirstDescendant(cf =>
            cf.ByName(dialogName))

        ?.AsWindow()

        ?? throw new Exception(
            $"{dialogName} dialog not found");
}

private void HandleCannotFindPopup()
{
    try
    {
        //---------------------------------
        // Desktop
        //---------------------------------

        var desktop =
            _window.Automation
            .GetDesktop();

        //---------------------------------
        // Wait Briefly
        //---------------------------------

        Task.Delay(1000)
            .Wait();

        //---------------------------------
        // Popup Window
        //---------------------------------

        var popup =
            desktop.FindFirstDescendant(cf =>
                cf.ByName(
                    "Notepad"))

            ?.AsWindow();

        if (popup == null)
        {
            return;
        }

        //---------------------------------
        // OK Button
        //---------------------------------

        var okButton =
            popup.FindFirstDescendant(cf =>
                cf.ByName("OK"))

            ?.AsButton();

        if (okButton == null)
        {
            return;
        }

        //---------------------------------
        // Click OK
        //---------------------------------

        LoggerHelper.Log(
            "Closing Cannot Find popup");

        ClickElement(
            okButton);

        ApplyDemoDelay();

        LoggerHelper.Log(
            "Cannot Find popup closed");
    }
    catch
    {
        //---------------------------------
        // Ignore If Popup Missing
        //---------------------------------
    }
}

        // Perform replace

        public void PerformReplace(
    Window replaceWindow,
    string findText,
    string replaceText)
{
    LoggerHelper.Log(
        $"Replacing {findText} with {replaceText}");

    //---------------------------------
    // Edit Controls
    //---------------------------------

    var editControls =
        replaceWindow.FindAllChildren(cf =>
            cf.ByControlType(
                ControlType.Edit));

    if (editControls.Length < 2)
    {
        throw new Exception(
            "Replace textboxes not found");
    }

    //---------------------------------
    // Find TextBox
    //---------------------------------

    var findTextBox =
        editControls[0]
        .AsTextBox();

    //---------------------------------
    // Replace TextBox
    //---------------------------------

    var replaceTextBox =
        editControls[1]
        .AsTextBox();

    //---------------------------------
    // Enter Find Text
    //---------------------------------

    ClickElement(
        findTextBox);

    foreach (char character in findText)
    {
        Keyboard.Type(
            character);

        ApplyDemoDelay();
    }

    //---------------------------------
    // Enter Replace Text
    //---------------------------------

    ClickElement(
        replaceTextBox);

    foreach (char character in replaceText)
    {
        Keyboard.Type(
            character);

        ApplyDemoDelay();
    }

    //---------------------------------
// Stabilize Focus
//---------------------------------

ClickElement(
    replaceTextBox);

ApplyDemoDelay();

    //---------------------------------
    // Buttons
    //---------------------------------

    //---------------------------------
// Stabilization Delay
//---------------------------------

Task.Delay(1000)
    .Wait();

//---------------------------------
// Find Next Button
//---------------------------------

var findNextButton =
    replaceWindow.FindFirstChild(cf =>
        cf.ByName("Find Next"))

    ?.AsButton()

    ?? throw new Exception(
        "Find Next button not found");

LoggerHelper.Log(
    "Find Next button located");

//---------------------------------
// Click Find Next
//---------------------------------

ClickElement(
    findNextButton);

ApplyDemoDelay();

LoggerHelper.Log(
    "Find Next clicked");

//---------------------------------
// Stabilization Delay
//---------------------------------

Task.Delay(1500)
    .Wait();

//---------------------------------
// Replace Button
//---------------------------------

var replaceButton =
    replaceWindow.FindFirstChild(cf =>
        cf.ByName("Replace"))

    ?.AsButton()

    ?? throw new Exception(
        "Replace button not found");

LoggerHelper.Log(
    "Replace button located");

//---------------------------------
// Wait For Enable
//---------------------------------

WaitHelper.WaitUntil(
    () => replaceButton.IsEnabled,
    10,
    "Replace button not enabled");

//---------------------------------
// Click Replace
//---------------------------------

ClickElement(
    replaceButton);

ApplyDemoDelay();

LoggerHelper.Log(
    "Replace button clicked");

HandleCannotFindPopup();

    ApplyDemoDelay();

    LoggerHelper.Log(
        "Replace completed");
}

private Window WaitForDialog(
    string dialogName)
{
    //---------------------------------
    // Desktop
    //---------------------------------

    var desktop =
        _window.Automation
        .GetDesktop();

    //---------------------------------
    // Wait For Dialog
    //---------------------------------

    WaitHelper.WaitUntil(
        () =>
            desktop.FindFirstDescendant(cf =>
                cf.ByName(dialogName)) != null,
        10,
        $"{dialogName} dialog not found");

    //---------------------------------
    // Stabilization Delay
    //---------------------------------

    Task.Delay(2000)
        .Wait();

    //---------------------------------
    // Dialog Window
    //---------------------------------

    var dialog =
        desktop.FindFirstDescendant(cf =>
            cf.ByName(dialogName))

        ?.AsWindow()

        ?? throw new Exception(
            $"{dialogName} dialog not found");

    //---------------------------------
    // Focus Dialog
    //---------------------------------

    dialog.Focus();

    ApplyDemoDelay();

    return dialog;
}
        // Close replace dialog

        public void CloseReplaceDialog(
    Window replaceWindow)
{
    LoggerHelper.Log(
        "Closing Replace dialog");

    //---------------------------------
    // Close Window
    //---------------------------------

    replaceWindow.Close();

    ApplyDemoDelay();

    LoggerHelper.Log(
        "Replace dialog closed");
}
    }
}