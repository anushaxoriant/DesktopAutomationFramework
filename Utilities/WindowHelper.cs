using FlaUI.Core.AutomationElements;

namespace DesktopAutomationFramework.Utilities
{
    public static class WindowHelper
    {
        public static void VerifyWindow(
            Window window)
        {
            try
            {
                if (window == null)
                {
                    throw new Exception(
                        "Window not found");
                }

                if (!window.IsEnabled)
                {
                    throw new Exception(
                        "Window not enabled");
                }
                LoggerHelper.Log(
                    "Window validated");
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"Window validation failed: {ex.Message}");

                throw;
            }
        }
    }
}