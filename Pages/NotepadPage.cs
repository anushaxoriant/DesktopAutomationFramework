using DesktopAutomationFramework.Utilities;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;

namespace DesktopAutomationFramework.Pages
{
    public class NotepadPage
    {
        // Window

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

        // Public window

        public Window Window =>
            _window;

        // Editor

        public FlaUI.Core.AutomationElements.TextBox Editor =>
            _window.FindFirstDescendant(
                cf => cf.ByAutomationId(
                    "15"))

            ?.AsTextBox()

            ?? _window.FindFirstDescendant(
                cf => cf.ByName(
                    "Text Editor"))

            ?.AsTextBox()

            ?? throw new Exception(
                "Editor not found");

        // Delay

        private void ApplyDemoDelay()
        {
            Task.Delay(700)
                .Wait();
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
                    FlaUI.Core.Definitions.WindowVisualState.Maximized);

            ApplyDemoDelay();

            LoggerHelper.Log(
                "Window maximized");
        }

        // Click element

        private void ClickElement(
            AutomationElement? element)
        {
            if (element == null)
            {
                throw new Exception(
                    "Element is null");
            }

            WaitHelper.WaitForElement(
                element);

            // Invoke preferred

            if (element.Patterns.Invoke.IsSupported)
            {
                element.Patterns
                    .Invoke.Pattern
                    .Invoke();
            }
            else
            {
                // Mouse fallback

                var point =
                    element.GetClickablePoint();

                Mouse.MoveTo(
                    point);

                ApplyDemoDelay();

                Mouse.Click(
                    point);
            }

            ApplyDemoDelay();
        }

        // Click menu

        private void ClickMenu(
            string parentMenu,
            string childMenu)
        {
            LoggerHelper.Log(
                $"Opening menu: {parentMenu}");

            // Parent menu

            var parent =
                _window.FindFirstDescendant(
                    cf => cf.ByName(
                        parentMenu))

                ?.AsMenuItem()

                ?? throw new Exception(
                    $"Parent menu not found: {parentMenu}");

            ClickElement(
                parent);

            // Desktop

            var desktop =
                _window.Automation
                .GetDesktop();

            // Wait child menu

            WaitHelper.WaitUntil(
                () =>
                    desktop.FindFirstDescendant(
                        cf => cf.ByName(
                            childMenu)) != null,
                10,
                $"Child menu not found: {childMenu}");

            // Child menu

            var child =
                desktop.FindFirstDescendant(
                    cf => cf.ByName(
                        childMenu))

                ?.AsMenuItem()

                ?? throw new Exception(
                    $"Child menu not found: {childMenu}");

            ClickElement(
                child);

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

            Editor.Enter(
                text);

            ApplyDemoDelay();

            LoggerHelper.Log(
                "Text entered");
        }

        // Append text

        public void AppendText(
            string text)
        {
            LoggerHelper.Log(
                $"Appending text: {text}");

            Editor.Focus();

            Keyboard.Press(
                VirtualKeyShort.END);

            Keyboard.Release(
                VirtualKeyShort.END);

            ApplyDemoDelay();

            Keyboard.Type(
                text);

            ApplyDemoDelay();

            LoggerHelper.Log(
                "Text appended");
        }

        // Save file

        public void SaveFile(
            string filePath)
        {
            LoggerHelper.Log(
                $"Saving file: {filePath}");

            // File -> Save

            ClickMenu(
                "File",
                "Save");

            // Wait dialog

            WaitHelper.WaitUntil(
                () =>
                    _window.ModalWindows.Length > 0,
                10,
                "Save dialog not found");

            // Save window

            var saveWindow =
                _window.ModalWindows[0];

            // File name textbox

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

            // Enter path

            fileNameBox.Focus();

            ApplyDemoDelay();

            fileNameBox.Text =
                string.Empty;

            ApplyDemoDelay();

            fileNameBox.Enter(
                filePath);

            ApplyDemoDelay();

            // Save button

            var saveButton =
                saveWindow.FindFirstDescendant(
                    cf => cf.ByName(
                        "Save"))

                ?.AsButton()

                ?? throw new Exception(
                    "Save button not found");

            ClickElement(
                saveButton);

            // Validate save

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

            Keyboard.Press(
                VirtualKeyShort.DELETE);

            Keyboard.Release(
                VirtualKeyShort.DELETE);

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

            ClickMenu(
                "Edit",
                "Find...");

            var findWindow =
                GetDialogWindow(
                    "Find");

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

            if (findWindow == null)
            {
                throw new Exception(
                    "Find dialog validation failed");
            }
        }

        // Perform find

        public void PerformFind(
            Window findWindow,
            string textToFind)
        {
            LoggerHelper.Log(
                $"Finding text: {textToFind}");

            // Find textbox

            var findTextBox =
                findWindow.FindFirstDescendant(
                    cf => cf.ByAutomationId(
                        "1152"))

                ?.AsTextBox()

                ?? throw new Exception(
                    "Find textbox not found");

            // Enter text

            findTextBox.Enter(
                textToFind);

            ApplyDemoDelay();

            // Find next button

            var findNextButton =
                findWindow.FindFirstDescendant(
                    cf => cf.ByName(
                        "Find Next"))

                ?.AsButton()

                ?? throw new Exception(
                    "Find Next button not found");

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

            var replaceWindow =
                GetDialogWindow(
                    "Replace");

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

            if (replaceWindow == null)
            {
                throw new Exception(
                    "Replace dialog validation failed");
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

            // Find textbox

            var findTextBox =
                replaceWindow.FindFirstDescendant(
                    cf => cf.ByAutomationId(
                        "1152"))

                ?.AsTextBox()

                ?? throw new Exception(
                    "Find textbox not found");

            // Replace textbox

            var replaceTextBox =
                replaceWindow.FindFirstDescendant(
                    cf => cf.ByAutomationId(
                        "1153"))

                ?.AsTextBox()

                ?? throw new Exception(
                    "Replace textbox not found");

            // Enter find text

            findTextBox.Enter(
                findText);

            ApplyDemoDelay();

            // Enter replace text

            replaceTextBox.Enter(
                replaceText);

            ApplyDemoDelay();

            // Find next button

            var findNextButton =
                replaceWindow.FindFirstDescendant(
                    cf => cf.ByName(
                        "Find Next"))

                ?.AsButton()

                ?? throw new Exception(
                    "Find Next button not found");

            ClickElement(
                findNextButton);

            ApplyDemoDelay();

            // Replace button

            var replaceButton =
                replaceWindow.FindFirstDescendant(
                    cf => cf.ByName(
                        "Replace"))

                ?.AsButton()

                ?? throw new Exception(
                    "Replace button not found");

            ClickElement(
                replaceButton);

            ApplyDemoDelay();

            HandleCannotFindPopup();

            LoggerHelper.Log(
                "Replace completed");
        }

        // Close replace dialog

        public void CloseReplaceDialog(
            Window replaceWindow)
        {
            LoggerHelper.Log(
                "Closing Replace dialog");

            replaceWindow.Close();

            ApplyDemoDelay();

            LoggerHelper.Log(
                "Replace dialog closed");
        }

        // Get dialog window

        private Window GetDialogWindow(
            string dialogName)
        {
            var desktop =
                _window.Automation
                .GetDesktop();

            WaitHelper.WaitUntil(
                () =>
                    desktop.FindFirstDescendant(
                        cf => cf.ByName(
                            dialogName)) != null,
                10,
                $"{dialogName} dialog not found");

            var dialog =
                desktop.FindFirstDescendant(
                    cf => cf.ByName(
                        dialogName))

                ?.AsWindow()

                ?? throw new Exception(
                    $"{dialogName} dialog not found");

            dialog.Focus();

            ApplyDemoDelay();

            return dialog;
        }

        // Handle cannot find popup

        private void HandleCannotFindPopup()
        {
            try
            {
                var desktop =
                    _window.Automation
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

                var okButton =
                    popup.FindFirstDescendant(
                        cf => cf.ByName(
                            "OK"))

                    ?.AsButton();

                if (okButton == null)
                {
                    return;
                }

                ClickElement(
                    okButton);

                ApplyDemoDelay();

                LoggerHelper.Log(
                    "Cannot Find popup closed");
            }
            catch
            {
                // Ignore popup failures
            }
        }
    }
}