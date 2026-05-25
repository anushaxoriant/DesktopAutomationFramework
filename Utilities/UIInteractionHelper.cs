using DesktopAutomationFramework.Utilities;

using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;

namespace DesktopAutomationFramework.Utilities
{
    public static class UIInteractionHelper
    {
        // =========================
        // CLICK ELEMENT
        // =========================

        public static void ClickElement(
            AutomationElement? element)
        {
            try
            {
                if (element == null)
                {
                    throw new Exception(
                        "Element is null");
                }

                WaitHelper.WaitForElement(
                    element);

                if (!element.IsEnabled)
                {
                    throw new Exception(
                        "Element is disabled");
                }

                if (element.Patterns.Invoke.IsSupported)
                {
                    element.Patterns
                        .Invoke.Pattern
                        .Invoke();
                }
                else
                {
                    var point =
                        element.GetClickablePoint();

                    Mouse.MoveTo(
                        point);

                    WaitHelper.ApplyDelay();

                    Mouse.Click(
                        point);
                }

                WaitHelper.ApplyDelay();
            }
            catch (Exception ex)
            {
                string elementName =
                    element?.Name
                    ?? "Unknown Element";

                throw new Exception(
                    $"Element click failed for '{elementName}'. {ex.Message}");
            }
        }

        // =========================
        // ENTER TEXT INTO TEXTBOX
        // =========================

        public static void EnterTextIntoTextBox(
            FlaUI.Core.AutomationElements.TextBox textBox,
            string text,
            string controlName)
        {
            try
            {
                if (textBox == null)
                {
                    throw new Exception(
                        $"{controlName} textbox is null.");
                }

                if (string.IsNullOrWhiteSpace(text))
                {
                    throw new Exception(
                        $"{controlName} text is null or empty.");
                }

                textBox.Focus();

                WaitHelper.ApplyDelay();

                textBox.Text =
                    string.Empty;

                WaitHelper.ApplyDelay();

                textBox.Enter(
                    text);

                WaitHelper.ApplyDelay();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Failed to enter text into {controlName}. {ex.Message}");
            }
        }
    }
}