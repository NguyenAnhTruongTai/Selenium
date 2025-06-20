using System.Diagnostics;
using Automation.Core.Drivers;
using Automation.Core.Utilities;

namespace Automation.BuggyCars.Testing.Tests
{
    public class BaseTest
    {
        private static string _reportPath;

        [OneTimeSetUp]
        public void CreateTestForExtendedReports()
        {
            ExtentReportHelpers.CreateTest(TestContext.CurrentContext.Test.ClassName ?? "Test case not found");
            Console.WriteLine("Base Test One Time Set Up");
            var basePath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
            _reportPath = Path.Combine(basePath, "TestResults", "index.html");
            ExtentReportHelpers.InitializeReport(_reportPath);

        }
        [SetUp]
        public void Setup()
        {
            BrowserFactory.CreateBrowser(ConfigurationUtils.GetConfigurationByKey("Browser"));
            ExtentReportHelpers.CreateNode(TestContext.CurrentContext.Test.Name);
            DriverUtil.GoToUrl(ConfigurationUtils.GetConfigurationByKey("TestURL"));
            DriverUtil.MaximizeWindow();
        }


        [TearDown]
        public void Teardown()
        {
            try
            {
                var result = TestContext.CurrentContext.Result;
                var status = result.Outcome.Status;

                var extentStatus = status switch
                {
                    NUnit.Framework.Interfaces.TestStatus.Passed => ExtentReportHelpers.ExtentTestStatus.Passed,
                    NUnit.Framework.Interfaces.TestStatus.Failed => ExtentReportHelpers.ExtentTestStatus.Failed,
                    NUnit.Framework.Interfaces.TestStatus.Skipped => ExtentReportHelpers.ExtentTestStatus.Skipped,
                    NUnit.Framework.Interfaces.TestStatus.Inconclusive => ExtentReportHelpers.ExtentTestStatus.Inconclusive,
                    _ => ExtentReportHelpers.ExtentTestStatus.Inconclusive
                };

                ExtentReportHelpers.CreateTestResult(
                    extentStatus,
                    result.Message + "\n" + result.StackTrace,
                    TestContext.CurrentContext.Test.Name
                );


                if (BrowserFactory.GetWebDriver() != null)
                {
                    BrowserFactory.GetWebDriver().Quit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during teardown: " + ex.Message);
            }
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            ExtentReportHelpers.Flush();
            if (File.Exists(_reportPath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = _reportPath,
                    UseShellExecute = true
                });
            }
        }
    }
}