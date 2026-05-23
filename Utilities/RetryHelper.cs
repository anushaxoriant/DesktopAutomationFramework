using System;
using System.Threading;

namespace DesktopAutomationFramework.Utilities
{
    public static class RetryHelper
    {
        public static void RetryAction(
            Action action,
            string failureMessage)
        {
            Exception? lastException = null;

            for (int attempt = 1;
                 attempt <= FrameworkConstants.RetryCount;
                 attempt++)
            {
                try
                {
                    action();

                    return;
                }
                catch (Exception ex)
                {
                    lastException = ex;

                    LoggerHelper.Log(
                        $"Retry {attempt} failed: {ex.Message}");

                    Thread.Sleep(
                        FrameworkConstants.RetryDelay);
                }
            }

            throw new Exception(
                failureMessage,
                lastException);
        }
    }
}