using FlaUI.Core.AutomationElements;

namespace DesktopAutomationFramework.Utilities
{
    public static class WaitHelper
    {
        public static void WaitUntil(
            Func<bool> condition,
            int timeoutInSeconds = 10,
            string errorMessage = "Condition not met")
        {
            var startTime = DateTime.Now;

            while (
                DateTime.Now - startTime <
                TimeSpan.FromSeconds(timeoutInSeconds))
            {
                if (condition())
                {
                    return;
                }

                Task.Delay(100).Wait();
            }

            throw new Exception(errorMessage);
        }

        public static void WaitForElement(
            AutomationElement element,
            int timeoutInSeconds = 10)
        {
            WaitUntil(
                () => element != null && element.IsEnabled,
                timeoutInSeconds,
                "Element not enabled");
        }
    }
}