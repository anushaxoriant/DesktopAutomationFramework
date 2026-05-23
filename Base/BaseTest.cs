using Allure.Net.Commons;
using FlaUI.UIA3;
using NUnit.Framework;
using DesktopAutomationFramework.Utilities;

namespace DesktopAutomationFramework.Tests
{
    public class BaseTest
    {
        protected FlaUI.Core.Application? App;
        protected UIA3Automation? Automation;
        [OneTimeSetUp]
        public void GlobalSetup()
        {
            try
            {
                // Initialize automation

                Automation =
                    new UIA3Automation();

                // Prepare files

                FileHelper.PrepareTestFiles();

                // Logging

                LoggerHelper.Log(
                    "Framework initialized");
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"GlobalSetup failed: {ex.Message}");

                throw;
            }
        }
        [SetUp]
        public void Setup()
        {
            try
            {
                // Logging

                LoggerHelper.Log(
                    $"Starting Test: " +
                    $"{TestContext.CurrentContext.Test.Name}");

                // Reporting

                AllureApi.Step(
                    $"Executing Test: " +
                    $"{TestContext.CurrentContext.Test.Name}");
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"Setup failed: {ex.Message}");

                throw;
            }
        }
        [TearDown]
        public void TearDown()
        {
            try
            {
                // Kill application

                App?.Kill();

                // Logging

                LoggerHelper.Log(
                    $"Completed Test: " +
                    $"{TestContext.CurrentContext.Test.Name}");
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"TearDown failed: {ex.Message}");
            }
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            try
            {

                Automation?.Dispose();

                FileHelper.CleanupTestFiles();
                LoggerHelper.Log(
                    "Framework cleanup completed");
            }
            catch (Exception ex)
            {
                LoggerHelper.Log(
                    $"GlobalTearDown failed: {ex.Message}");
            }
        }
    }
}