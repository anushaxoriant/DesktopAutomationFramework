using System;
using System.Threading;

namespace DesktopAutomationFramework.Utilities
{
    public static class WaitHelper
    {
        public static void WaitUntil(
            Func<bool> condition,
            string timeoutMessage,
            int timeout = FrameworkConstants.DefaultTimeout)
        {
            var endTime =
                DateTime.Now.AddMilliseconds(timeout);

            while (DateTime.Now < endTime)
            {
                try
                {
                    if (condition())
                    {
                        return;
                    }
                }
                catch
                {
                }

                Thread.Sleep(500);
            }

            throw new TimeoutException(
                timeoutMessage);
        }
    }
}