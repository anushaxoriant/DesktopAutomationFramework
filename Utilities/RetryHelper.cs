namespace DesktopAutomationFramework.Utilities
{
    public static class RetryHelper
    {
        // =========================
        // RETRY ACTION
        // =========================

        public static void RetryAction(
            Action action,
            string failureMessage,
            int retryCount = 3,
            int delayMilliseconds = 1000)
        {
            Exception? lastException = null;

            for (int attempt = 1;
                 attempt <= retryCount;
                 attempt++)
            {
                try
                {
                    action();

                    LoggerHelper.Log(
                        $"Retry action succeeded on attempt {attempt}");

                    return;
                }
                catch (Exception ex)
                {
                    lastException = ex;

                    LoggerHelper.Log(
                        $"Retry attempt {attempt} failed. {ex.Message}");

                    Task.Delay(
                        delayMilliseconds)
                        .Wait();
                }
            }

            throw new Exception(
                $"{failureMessage} Retry failed after {retryCount} attempts.",
                lastException);
        }

        // =========================
        // SIMPLE RETRY
        // =========================

        public static void Retry(
            Action action,
            int retryCount = 3)
        {
            RetryAction(
                action,
                "Retry operation failed.",
                retryCount);
        }
    }
}