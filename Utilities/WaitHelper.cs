using FlaUI.Core.AutomationElements;

namespace DesktopAutomationFramework.Utilities
{
    public static class WaitHelper
    {
        // =========================
        // WAIT UNTIL
        // =========================

        public static void WaitUntil(
            Func<bool> condition,
            string failureMessage,
            int timeoutSeconds = 10)
        {
            DateTime startTime =
                DateTime.Now;

            Exception? lastException = null;

            while ((DateTime.Now - startTime)
                .TotalSeconds < timeoutSeconds)
            {
                try
                {
                    if (condition())
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lastException = ex;
                }

                Task.Delay(500)
                    .Wait();
            }

            throw new Exception(
                $"{failureMessage}" +

                $"{(lastException != null ? $" Last error: {lastException.Message}" : "")}");
        }

        // =========================
        // WAIT FOR ELEMENT
        // =========================

        public static void WaitForElement(
            AutomationElement? element,
            int timeoutSeconds = 10)
        {
            WaitUntil(
                () =>
                    element != null
                    &&
                    element.IsEnabled,

                "Element not available",

                timeoutSeconds);
        }

        // =========================
        // APPLY DELAY
        // =========================

        public static void ApplyDelay(
            int milliseconds =
                FrameworkConstants.DefaultDelay)
        {
            Task.Delay(
                milliseconds)
                .Wait();
        }
    }
}