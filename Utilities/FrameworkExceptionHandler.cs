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

            string screenshotPath =
                ScreenshotHelper.CaptureScreenshot(
                    TestContext.CurrentContext.Test.Name);

            AllureApi.AddAttachment(
                "Failure Screenshot",
                "image/png",
                screenshotPath);

            throw new Exception(
                message,
                ex);
        }
    }
}