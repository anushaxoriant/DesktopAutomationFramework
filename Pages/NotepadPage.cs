using Allure.Net.Commons;
using DesktopAutomationFramework.Base;
using DesktopAutomationFramework.Utilities;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA3;

namespace DesktopAutomationFramework.Pages
{
    public class NotepadPage : BasePage
    {
        private readonly FlaUI.Core.Application app;

        private readonly UIA3Automation automation;

        public Window Window { get; }

        public FlaUI.Core.AutomationElements.TextBox? Editor =>
            Window.FindFirstDescendant(
                cf => cf.ByControlType(
                    FlaUI.Core.Definitions.ControlType.Document))
            ?.AsTextBox();

        public NotepadPage(
            FlaUI.Core.Application app,
            UIA3Automation automation)
        {
            this.app = app;

            this.automation = automation;

            Window =
                app.GetMainWindow(
                    automation)

                ?? throw new Exception(
        "Failed to locate Notepad main window.");

            if (Window == null)
            {
                throw new Exception(
                    "Notepad main window not found.");
            }

            if (!Window.IsEnabled)
            {
                throw new Exception(
                    "Notepad window is disabled.");
            }

            LoggerHelper.Log(
                "Notepad window initialized successfully");
        }

        public void EnterText(
            string text)
        {
            try
            {
                AllureApi.Step(
                    "Entering text into Notepad editor");

                if (string.IsNullOrWhiteSpace(text))
                {
                    throw new Exception(
                        "Input text is null or empty.");
                }

                SafeEnterText(
                    Editor,
                    text,
                    "Editor");

                LoggerHelper.Log(
                    $"Entered text: {text}");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Failed to enter text into editor. {ex.Message}");
            }
        }

        public void SaveFile(
            string filePath)
        {
            try
            {
                AllureApi.Step(
                    $"Saving file: {filePath}");

                if (string.IsNullOrWhiteSpace(filePath))
                {
                    throw new Exception(
                        "File path is null or empty.");
                }

                Window.Focus();

                FlaUI.Core.Input.Keyboard.Press(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL);

                FlaUI.Core.Input.Keyboard.Press(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_S);

                FlaUI.Core.Input.Keyboard.Release(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_S);

                FlaUI.Core.Input.Keyboard.Release(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL);

                WaitHelper.WaitUntil(
                    () =>
                    Window.ModalWindows.Length > 0,
                    "Save dialog did not appear.");

                var saveDialog =
                    Window.ModalWindows.FirstOrDefault();

                if (saveDialog == null)
                {
                    throw new Exception(
                        "Save dialog not found.");
                }

                var fileNameBox =
                    saveDialog.FindFirstDescendant(
                        cf => cf.ByAutomationId(
                            "1001"))
                    ?.AsTextBox();

                SafeEnterText(
                    fileNameBox,
                    filePath,
                    "File Name");

                var saveButton =
                    saveDialog.FindFirstDescendant(
                        cf => cf.ByName(
                            "Save"))
                    ?.AsButton();

                SafeInvoke(
                    saveButton,
                    "Save");

                WaitHelper.WaitUntil(
                    () => File.Exists(
                        filePath),
                    $"Saved file not found: {filePath}");

                LoggerHelper.Log(
                    $"File saved successfully: {filePath}");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Save operation failed. {ex.Message}");
            }
        }

        public string ReadEditorText()
        {
            try
            {
                AllureApi.Step(
                    "Reading editor text");

                if (Editor == null)
                {
                    throw new Exception(
                        "Editor textbox not found.");
                }

                string text =
                    Editor.Text;

                LoggerHelper.Log(
                    $"Editor text read successfully");

                return text;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Failed to read editor text. {ex.Message}");
            }
        }

        public void ClearEditor()
        {
            try
            {
                AllureApi.Step(
                    "Clearing editor");

                if (Editor == null)
                {
                    throw new Exception(
                        "Editor textbox not found.");
                }

                Editor.Focus();

                FlaUI.Core.Input.Keyboard.Press(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL);

                FlaUI.Core.Input.Keyboard.Press(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_A);

                FlaUI.Core.Input.Keyboard.Release(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_A);

                FlaUI.Core.Input.Keyboard.Release(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL);

                FlaUI.Core.Input.Keyboard.Type(
                    FlaUI.Core.WindowsAPI.VirtualKeyShort.BACK);

                LoggerHelper.Log(
                    "Editor cleared successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Failed to clear editor. {ex.Message}");
            }
        }
    }
}