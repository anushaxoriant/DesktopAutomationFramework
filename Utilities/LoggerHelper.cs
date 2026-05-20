using NUnit.Framework;

namespace DesktopAutomationFramework.Utilities
{
    public static class LoggerHelper
    {
        public static void Log(string message)
        {
            TestContext.Progress.WriteLine(
                $"[{DateTime.Now:HH:mm:ss}] {message}");
        }
    }
}