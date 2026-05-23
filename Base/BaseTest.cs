using Allure.Net.Commons;
using DesktopAutomationFramework.Utilities;
using FlaUI.Core;
using FlaUI.UIA3;
using NUnit.Framework;

namespace DesktopAutomationFramework.Base
{
    public class BaseTest
    {
        protected FlaUI.Core.Application? App;

        protected UIA3Automation? Automation;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            LoggerHelper.Log(
                "Initializing UI Automation engine");

            Automation =
                new UIA3Automation();
        }

        [SetUp]
        public void Setup()
        {
            LoggerHelper.Log(
                $"Starting test: {TestContext.CurrentContext.Test.Name}");
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                if (TestContext.CurrentContext.Result.Outcome.Status
                    == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    string screenshotPath =
                        ScreenshotHelper.CaptureScreenshot(
                            TestContext.CurrentContext.Test.Name);

                    AllureApi.AddAttachment(
                        "Failure Screenshot",
                        "image/png",
                        screenshotPath);
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"Screenshot capture failed: {ex.Message}");
            }

            try
            {
                App?.Close();
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"Application close failed: {ex.Message}");
            }
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            Automation?.Dispose();

            LoggerHelper.Log(
                "UI Automation disposed");
        }
    }
}