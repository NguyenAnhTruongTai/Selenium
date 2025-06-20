using Automation.Core.Utilities;

namespace Automation.BuggyCars.Testing.Tests
{
    [SetUpFixture]
    public class Hooks
    {
        private string _reportPath;

        [OneTimeSetUp]

        public void OneTimeSetUp()
        {
            TestContext.Progress.WriteLine("=========> Global OneTimeSetup");
            ConfigurationUtils.ReadConfiguration("appsettings.json");

            var basePath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
            _reportPath = Path.Combine(basePath, "TestResults", "index.html");
            ExtentReportHelpers.InitializeReport(_reportPath);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            TestContext.Progress.WriteLine("=========> Global OneTimeTearDown");
            ExtentReportHelpers.Flush();
        }
    }
}