//=========================================
// BaseTest.cs
//=========================================

using Allure.Net.Commons;
using FlaUI.UIA3;
using NUnit.Framework;
using DesktopAutomationFramework.Utilities;

namespace DesktopAutomationFramework.Tests
{
    public class BaseTest
    {
        //---------------------------------
        // Application
        //---------------------------------

        protected FlaUI.Core.Application? App;

        //---------------------------------
        // Automation
        //---------------------------------

        protected UIA3Automation? Automation;

        //---------------------------------
        // Before All Tests
        //---------------------------------

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            try
            {
                //---------------------------------
                // Initialize Automation
                //---------------------------------

                Automation =
                    new UIA3Automation();

                //---------------------------------
                // Prepare Files
                //---------------------------------

                FileHelper.PrepareTestFiles();

                //---------------------------------
                // Logging
                //---------------------------------

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

        //---------------------------------
        // Before Every Test
        //---------------------------------

        [SetUp]
        public void Setup()
        {
            try
            {
                //---------------------------------
                // Logging
                //---------------------------------

                LoggerHelper.Log(
                    $"Starting Test: " +
                    $"{TestContext.CurrentContext.Test.Name}");

                //---------------------------------
                // Reporting
                //---------------------------------

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

        //---------------------------------
        // After Every Test
        //---------------------------------

        [TearDown]
        public void TearDown()
        {
            try
            {
                //---------------------------------
                // Kill Application
                //---------------------------------

                App?.Kill();

                //---------------------------------
                // Logging
                //---------------------------------

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

        //---------------------------------
        // After All Tests
        //---------------------------------

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            try
            {
                //---------------------------------
                // Dispose Automation
                //---------------------------------

                Automation?.Dispose();

                //---------------------------------
                // Cleanup Files
                //---------------------------------

                FileHelper.CleanupTestFiles();

                //---------------------------------
                // Logging
                //---------------------------------

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