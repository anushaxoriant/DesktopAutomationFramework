using Allure.Net.Commons;
using NUnit.Framework;

namespace DesktopAutomationFramework.Utilities
{
    public static class FrameworkExceptionHandler
    {
        public static void HandleFailure(
            string message,
            Exception ex)
        {
            LoggerHelper.Log(
                $"FAILURE: {message}");

            LoggerHelper.Log(
                $"EXCEPTION: {ex.Message}");

            throw new Exception(
                message,
                ex);
        }
    }
}