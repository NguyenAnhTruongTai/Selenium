using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Automation.Core.Utilities;
using Automation.Core.Drivers;
using Automation.Test.Utilities;
namespace Automation.Test.Core.Utilities
{
    public class ExtentReportHelpers
    {
        private static ExtentReports _extentManager;
        [ThreadStatic]
        private static ExtentTest _extentTest;
        [ThreadStatic]
        private static ExtentTest _node;
        public enum TestStatus
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
            _extentManager.AddSystemInfo("Browser", ConfigurationUtils.GetSectionValue("application", "Browser"));
        }

        public static void CreateTest(string testName)
        {

            _extentTest = _extentManager.CreateTest(testName, testName);

        }

        public static void CreateNode(string nodeName)
        {

            _node = _extentTest.CreateNode(nodeName);

        }

        public static void LogTestStep(string stepName)
        {
            if (_node != null)
            {
                _node.Info(stepName);
            }
            else if (_extentTest != null)
            {
                _extentTest.Info(stepName);
            }
        }


        public static void CreateTestResult(TestStatus status, string stacktrace, string testName)
        {
            Status logStatus;
            switch (status)
            {
                case TestStatus.Failed:
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
                case TestStatus.Passed:
                    logStatus = Status.Pass;
                    _node.Pass($"===> Test Name: {testName} - #Status: {logStatus}");
                    break;
                case TestStatus.Inconclusive:
                    logStatus = Status.Warning;
                    _node.Warning($"===> Test Name: {testName} - #Status: {logStatus}");
                    break;
                case TestStatus.Skipped:
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