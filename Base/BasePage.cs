using Allure.Net.Commons;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Input;
using FlaUI.Core.WindowsAPI;
using System;
using DesktopAutomationFramework.Utilities;

namespace DesktopAutomationFramework.Base
{
    public class BasePage
    {
        protected void SafeInvoke(
            FlaUI.Core.AutomationElements.Button? button,
            string controlName)
        {
            if (button == null)
            {
                throw new Exception(
                    $"{controlName} button not found.");
            }

            if (!button.IsEnabled)
            {
                throw new Exception(
                    $"{controlName} button disabled.");
            }

            RetryHelper.RetryAction(
                () =>
                {
                    AllureApi.Step(
                        $"Clicking {controlName}");

                    button.Patterns.Invoke.Pattern.Invoke();
                },
                $"Failed to click {controlName} button.");
        }

        protected void SafeEnterText(
            FlaUI.Core.AutomationElements.TextBox? textBox,
            string text,
            string controlName)
        {
            if (textBox == null)
            {
                throw new Exception(
                    $"{controlName} textbox not found.");
            }

            if (!textBox.IsEnabled)
            {
                throw new Exception(
                    $"{controlName} textbox disabled.");
            }

            RetryHelper.RetryAction(
                () =>
                {
                    AllureApi.Step(
                        $"Entering text into {controlName}");

                    textBox.Enter(text);
                },
                $"Failed to enter text into {controlName}.");
        }
    }
}