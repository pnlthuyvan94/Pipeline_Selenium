using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using Pipeline.Common.Constants;
using Pipeline.Common.Utils;

namespace Pipeline.Common.BaseClass
{
    [TestFixture]
    
    public abstract class BaseTestScript
    {
        string testcaseCode;
        string testcaseName;
        string testSetName;
        protected string pathReportFolder => BaseValues.PathReportFile;
        protected string BaseDashboardUrl => BaseValues.DashboardURL;
        protected string BaseFullUrl => BaseValues.DriverSession.Url;

        /// <summary>
        /// Set up the Test Set name
        /// </summary>
        public abstract void SetupTestSectionName();

        /// <summary>
        /// Set the test set name
        /// </summary>
        /// <param name="parentname"></param>
        protected void SetupTestSectionName(string parentname)
        {
            // Executes once for the test class.
            if (string.IsNullOrEmpty(parentname))
            {
                testSetName = GetType().Name;
                ExtentReportsHelper.CreateParentTest(GetType().Name);
            }
            else
            {
                testSetName = parentname;
                ExtentReportsHelper.SwitchTestSet(parentname);
            }
        }

        [OneTimeSetUp]
        public void OnSectionStart()
        {
            SetupTestSectionName();
        }

        /// <summary>
        /// Override the test case name
        /// </summary>
        public virtual void SetupTestCaseName()
        {
            SetupTestCaseName(null);
        }

        protected virtual void SetupTestCaseName(string testName)
        {
            testcaseCode = GetType().Name;
            // Executes once for the test class.
            if (string.IsNullOrEmpty(testName))
            {
                testcaseName = TestContext.CurrentContext.Test.Name;
                ExtentReportsHelper.CreateTest(TestContext.CurrentContext.Test.Name);
            }
            else
            {
                testcaseName = testName;
                ExtentReportsHelper.CreateTest(testName);
            }
        }

        [SetUp]
        public void PreConditionSetUpTest()
        {
            SetupTestCaseName();
            // Log in each test script
            if (BaseValues.IsAutoLogin)
            {
                CommonFuncs.LoginToPipeline(ConfigurationManager.GetValue(BaseConstants.UserName), ConfigurationManager.GetValue(BaseConstants.Password));

                BasePage.JQueryLoad();

                if (!CommonFuncs.IsDashboardPageDisplayed() || CommonFuncs.IsLoginOrSSOPageDisplayed())
                {
                    ExtentReportsHelper.LogWarning($"Login failure - Possible causes:  Invalid/expired credentials, SSO / Application may be offline, or landing page / dashboard page may have changed.");
                    Assert.Inconclusive($"Login process did not complete as expected - Current URL: [{BaseValues.DriverSession.Url}], Current Title: [{BaseValues.DriverSession.Title}]");
                }
            }
            else
            { // Continue from dashboard if we're not logged out, otherwise login first
                string dashboard_url = BaseValues.DashboardURL;

                if (!CommonFuncs.IsDashboardPageDisplayed())
                {
                    CommonHelper.OpenURL(dashboard_url);

                    BasePage.JQueryLoad();

                    if (CommonFuncs.IsLoginOrSSOPageDisplayed())
                    {
                        CommonFuncs.LoginToPipeline(ConfigurationManager.GetValue(BaseConstants.UserName), ConfigurationManager.GetValue(BaseConstants.Password));
                        BasePage.JQueryLoad();
                    }

                    if (!CommonFuncs.IsDashboardPageDisplayed())
                    {
                        ExtentReportsHelper.LogWarning($"Init failure - Unable to access Pipeline Dashboard. Application offline or in maintenance.");
                        Assert.Inconclusive($"Init process did not complete as expected - Current URL: [{BaseValues.DriverSession.Url}], Current Title: [{BaseValues.DriverSession.Title}]");
                    }
                }
            }
        }


        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            if (status == TestStatus.Failed && ExtentReportsHelper.IsChildTestNull)
            {
                if (ExtentReportsHelper.IsParentTestNull)
                    SetupTestSectionName();
                SetupTestCaseName();
            }
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message)
                ? ""
                : $"<pre>{TestContext.CurrentContext.Result.Message}</pre>";

            bool isAppTimedOut = CommonFuncs.IsApplicationTimedOut();
            Status logstatus;

            switch (status)
            {
                case TestStatus.Failed:
                    if (isAppTimedOut)
                        //If we're timed out at any point then the test will likely be marked as failed,
                        //in which case we should change it's status to an inconclusive test
                        logstatus = Status.Warning;
                    else
                        logstatus = Status.Fail;
                    break;
                case TestStatus.Inconclusive:
                    logstatus = Status.Warning;
                    break;
                case TestStatus.Skipped:
                    logstatus = Status.Skip;
                    break;
                default:
                    logstatus = Status.Pass;
                    break;
            }

            ExtentReportsHelper.GetTest().Log(logstatus, "Test ended with " + logstatus + stacktrace);

            if (logstatus == Status.Fail)
            {
                try
                {
                    ExtentReportsHelper.LogFail($"Test case with ID <b>{testcaseCode} - {testcaseName}</b> in Testset <b>{testSetName}</b> is failed.<br>The current URL is {CommonHelper.GetCurrentDriverURL}");
                }
                catch (WebDriverException ex)
                {
                    System.Console.WriteLine("Error: " + ex.Message);
                    // Invoke the browser
                    BaseValues.DriverSession = BaseValues.InitDriverSession;
                }
            }

            // Return to dashboard before next test
            string dashboard_url = BaseDashboardUrl;

            // Logout if app is set to autologin before every test
            if (BaseValues.IsAutoLogin)
            {
                if (!BaseValues.DriverSession.Url.ToLower().Contains("account/login"))
                {
                    try
                    {
                        CommonFuncs.LogOut();
                    }
                    catch (System.Exception ex)
                    {
                        System.Console.WriteLine("Exception during log out: " + ex.Message);
                        CommonHelper.OpenURL(dashboard_url);
                    }
                }
            }
            else // Return to dashboard if not autologin
            {
                CommonHelper.OpenURL(dashboard_url + "/Default.aspx");
            }

        }

        [OneTimeTearDown]
        public void ClassCleanup()
        {
            // Runs once after all tests in this class are executed. (Optional)
            // Not guaranteed that it executes instantly after all tests from the class.
        }
    }
}
