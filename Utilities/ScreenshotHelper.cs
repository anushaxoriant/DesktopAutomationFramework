using FlaUI.Core.AutomationElements;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace DesktopAutomationFramework.Utilities
{
    public static class ScreenshotHelper
    {
        public static string CaptureScreenshot(
            string testName)
        {
            Directory.CreateDirectory(
                FrameworkConstants.ScreenshotFolder);

            string filePath =
                Path.Combine(
                    FrameworkConstants.ScreenshotFolder,
                    $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

            Rectangle bounds =
                Screen.PrimaryScreen?.Bounds

                ?? throw new Exception(
        "Primary screen not available.");

            using Bitmap bitmap =
                new Bitmap(bounds.Width, bounds.Height);

            using Graphics graphics =
                Graphics.FromImage(bitmap);

            graphics.CopyFromScreen(
                Point.Empty,
                Point.Empty,
                bounds.Size);

            bitmap.Save(
                filePath,
                ImageFormat.Png);

            LoggerHelper.Log(
                $"Screenshot captured: {filePath}");

            return filePath;
        }
    }
}