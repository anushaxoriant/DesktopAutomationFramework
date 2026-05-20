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
        // Application

        protected FlaUI.Core.Application? App;

        // Automation

        protected UIA3Automation? Automation;

        // Before all tests

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

        // Before every test

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

        // After every test

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

        // After all tests

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            try
            {
                // Dispose automation

                Automation?.Dispose();

                // Cleanup files

                FileHelper.CleanupTestFiles();

                // Logging

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