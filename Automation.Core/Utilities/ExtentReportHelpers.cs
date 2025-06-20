using Automation.Core.Drivers;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Automation.Core.Utilities
{
    public class ExtentReportHelpers
    {
        private static ExtentReports? _extentManager;
        [ThreadStatic]
        private static ExtentTest? _extentTest;
        [ThreadStatic]
        private static ExtentTest? _node;
        public enum ExtentTestStatus
        {
            Passed,
            Failed,
            Skipped,
            Inconclusive
        }

        public static void InitializeReport(string reportPath)
        {
            ExtentSparkReporter htmlReporter = new ExtentSparkReporter(reportPath);
            _extentManager = new ExtentReports();
            _extentManager.AttachReporter(htmlReporter);
            _extentManager.AddSystemInfo("Environment", "Staging");
            _extentManager.AddSystemInfo("Browser", ConfigurationUtils.GetSectionValue("applications", "Browser"));
        }

        public static void CreateTest(string testName)
        {

            _extentTest = _extentManager.CreateTest(testName, testName);

        }

        public static void CreateNode(string nodeName)
        {

            _node = _extentTest.CreateNode(nodeName);

        }


        public static void LogTestStep(string stepName, Status status = Status.Pass)
        {
            if (_extentManager == null)
            {
                Console.WriteLine("[ExtentReportHelpers] ExtentReports not initialized.");
                return;
            }

            if (_extentTest == null)
            {
                Console.WriteLine("[ExtentReportHelpers] ExtentTest not created.");
                return;
            }

            if (_node == null)
            {
                Console.WriteLine("[ExtentReportHelpers] Node not created. Logging to main test.");
                _extentTest.Log(status, stepName);
                return;
            }

            _node.Log(status, stepName);
        }
        public static void LogException(Exception ex)
        {
            if (_node != null)
            {
                _node.Fail(ex);
            }
            else if (_extentTest != null)
            {
                _extentTest.Fail(ex);
            }
        }

        public static void CreateTestResult(ExtentTestStatus status, string stacktrace, string testName)
        {
            Status logStatus;
            switch (status)
            {
                case ExtentTestStatus.Failed:
                    logStatus = Status.Fail;
                    if (BrowserFactory.GetWebDriver() != null)
                    {
                        var mediaEntity = DriverUtil.CaptureScreenShotAndAttachToExtendReport(BrowserFactory.GetWebDriver(), testName);
                        {
                            _node.Fail("#Test Name: " + testName + " #Status: " + logStatus + stacktrace, mediaEntity);

                        }
                    }
                    else
                    {
                        _node.Fail("#Test Name: " + testName + " #Status: " + logStatus + stacktrace);
                    }
                    break;
                case ExtentTestStatus.Passed:
                    logStatus = Status.Pass;
                    if (BrowserFactory.GetWebDriver() != null)
                    {
                        var mediaEntity = DriverUtil.CaptureScreenShotAndAttachToExtendReport(
                            BrowserFactory.GetWebDriver(),
                            testName
                        );

                        _node.Pass($"===> Test Name: {testName} - #Status: {logStatus}", mediaEntity);
                    }
                    else
                    {
                        _node.Pass($"===> Test Name: {testName} - #Status: {logStatus}");
                    }
                    break;
                case ExtentTestStatus.Inconclusive:
                    logStatus = Status.Warning;
                    _node.Warning($"===> Test Name: {testName} - #Status: {logStatus}");
                    break;
                case ExtentTestStatus.Skipped:
                    logStatus = Status.Skip;
                    _node.Skip($"===> Test Name: {testName} - #Status: {logStatus}");
                    break;
                default:
                    logStatus = Status.Pass;
                    _node.Info($"===> Test Name: {testName} - #Status: {logStatus}");
                    break;


            }
        }

        public static void Flush()
        {
            _extentManager.Flush();
        }
    }
}