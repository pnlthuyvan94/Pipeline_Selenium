using LinqToExcel;
using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Pipeline.Common.Utils
{
    public class CommonFuncs
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool IsApplicationTimedOut()
        {
            return (BaseValues.DriverSession.Title.ToLower().Contains("timed out"));
        }

        public static bool IsLoginOrSSOPageDisplayed()
        {
            return (BaseValues.DriverSession.Url.ToLower().Contains("login") || BaseValues.DriverSession.Url.ToLower().Contains("alreadyloggedin") || BaseValues.DriverSession.Url.ToLower().Contains("signin"));
        }

        public static bool IsDashboardPageDisplayed()
        {
            //string dashboard_url = ConfigurationManager.GetValue(BaseConstants.ApplicationProtocol) + "://" + ConfigurationManager.GetValue(BaseConstants.ApplicationDomain) + "/dashboard";
            string dashboard_domain = "dashboard";

            return (BaseValues.DriverSession.Url.ToLower().Contains(dashboard_domain) && !BaseValues.DriverSession.Url.ToLower().Contains("userlimit")); //(BaseValues.DriverSession.Url.ToLower().StartsWith(dashboard_url));
        }

        #region Automatic SSO Login Process -- INTEGRATED FROM LBM REGRESSION PROJECT

        public static void GoToLoginPage()
        {
            if (!IsLoginOrSSOPageDisplayed())
            {
                try
                {
                    LogOut();
                } catch (Exception _ex)
                {
                    string dashboard_url = "http://dev.bimpipeline.com".ToString().Replace("dev", ConfigurationManager.GetValue("ApplicationDomain"));
                    //string dashboard_url = "http://dev.bimpipeline.com/Dashboard".ToString().Replace("dev", ConfigurationManager.GetValue("ApplicationDomain"));

                    CommonHelper.OpenURL(dashboard_url);
                }

                //BaseValues.DriverSession.Navigate().GoToUrl($"{BaseValues.ApplicationURL}/{BaseValues.Tenant}");
                CommonHelper.OpenURL(ConfigurationManager.GetValue(BaseConstants.ApplicationProtocol) + "://" + ConfigurationManager.GetValue(BaseConstants.ApplicationDomain));
            }
        }

        public static void AutoLogin(string username, string password)
        {
            if (BaseValues.DriverSession.Url.Contains("Account/Login") || BaseValues.DriverSession.Url.Contains("signin"))
            {
                try
                {
                    Log.Debug($"Attempting AutoLogin...");

                    var userNameTxt = BaseValues.DriverSession.FindElement(By.XPath("//input[contains(@name, 'Email')]"));
                    
                    //var loginBtn = BaseValues.DriverSession.FindElement(By.XPath("/html/body/div/div/div[2]/div/div/form/div[2]/input[3]")); // old
                    var loginBtn = BaseValues.DriverSession.FindElement(By.XPath("//*[contains(@class, 'ssobutton ssoprimarybutton')]"));  
                    
                    userNameTxt.Clear();
                    userNameTxt.SendKeys(username);

                    System.Threading.Thread.Sleep(500);

                    loginBtn.Click();

                    BasePage.PageLoad();

                    System.Threading.Thread.Sleep(1000);

                    var passwordTxt = BaseValues.DriverSession.FindElement(By.XPath("//input[contains(@name, 'Password')]"));
                    loginBtn = BaseValues.DriverSession.FindElement(By.XPath("//*[contains(@class, 'ssobutton ssoprimarybutton')]"));

                    passwordTxt.Clear();
                    passwordTxt.SendKeys(password);

                    System.Threading.Thread.Sleep(500);

                    loginBtn.Click();

                    BasePage.PageLoad();
                }
                catch (Exception exception)
                {
                    Log.Error($"Could not locate elements for AutoLogin: {exception.Message}");
                }

            }
        }

        public static void AutoSSO(string password)
        {
            if (BaseValues.DriverSession.Url.Contains("Account/AlreadyLoggedIn"))
            {
                try
                {
                    Log.Debug($"Attempting AutoSSO...");

                    System.Threading.Thread.Sleep(500);

                    var passwordTxt = BaseValues.DriverSession.FindElement(By.Id("txtPassword"));
                    var loginBtn = BaseValues.DriverSession.FindElement(By.XPath("//input[@id=\"btnForceSignIn\"]"));

                    if (passwordTxt != null && passwordTxt.Displayed)
                    {
                        passwordTxt.Clear();
                        passwordTxt.SendKeys(password);
                    }

                    System.Threading.Thread.Sleep(500);

                    if (loginBtn != null && loginBtn.Displayed)
                    {
                        loginBtn.Click();
                        BasePage.PageLoad();
                    }
                }
                catch (Exception exception)
                {
                    Log.Error($"Could not locate elements for AutoSSO: {exception.Message}");
                }
            }
        }

        public static bool IsSSOPageDisplayed()
        {
            return (BaseValues.DriverSession.Url.Contains("AlreadyLoggedIn"));
        }

        #endregion

        public static void LoginToPipeline(string userName, string password)
        {
            //string pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\LoginParams.xlsx";
            //LoginPage _loginPage = new LoginPage(pathExcelFile);

            GoToLoginPage();
            AutoLogin(userName, password);
            AutoSSO(password);

            BasePage.JQueryLoad();

            System.Threading.Thread.Sleep(5000);

            if (BaseValues.ApplicationDomain == string.Empty) {
                string newUrl = BaseValues.DriverSession.Url.Replace("202211.release.bimpipeline.com", "10.193.34.113:9976");

                CommonHelper.OpenURL(newUrl);
            }

            //_loginPage.LoginWithUser(userName)
            //          .LoginWithPassword(password)
            //          .SignIn();
        }

        public static void LogOut()
        {
            CommonHelper.JavaScriptExecutor("javascript:__doPostBack('ctl00$LoginStatus1$ctl00','')");

            BasePage.JQueryLoad();

            System.Threading.Thread.Sleep(2000);

            var logoutButton = BaseValues.DriverSession.FindElement(By.XPath("//input[contains(@class, 'ssobutton')]"));
            if (logoutButton.Displayed)
            {
                logoutButton.Click();
            }

            System.Threading.Thread.Sleep(2000);

            if (BaseValues.ApplicationDomain == string.Empty) {
                CommonHelper.OpenURL("http://10.193.34.113:9976/Dashboard/signout-oidc/external.aspx");
            }

            BasePage.JQueryLoad();


        }

        private class LoginPage : BasePage
        {
            internal LoginPage(string pathOfExcelFile) : base()
            {
                PageLoad();
                ExcelHelper = new ExcelFactory(pathOfExcelFile);

                // Sheet contains repository of Dashboard
                MetaData = ExcelHelper.GetAllRows("Login_OR");
            }

            static IQueryable<Row> MetaData { get; set; }

            Row _title { get { return ExcelFactory.GetRow(MetaData, 1); } }
            Label Title_lbl => new Label(_title);

            Row _userName { get { return ExcelFactory.GetRow(MetaData, 2); } }
            Textbox UserName_txt => new Textbox(_userName);

            Row _password { get { return ExcelFactory.GetRow(MetaData, 3); } }
            Textbox Password_txt => new Textbox(_password);

            Row _signIn { get { return ExcelFactory.GetRow(MetaData, 4); } }
            Button Login_btn => new Button(_signIn);

            internal LoginPage LoginWithUser(string userName)
            {
                if (Title_lbl.IsNull())
                {
                    BaseValues.InitDriverSession.Url = ConfigurationManager.GetValue(BaseConstants.ApplicationProtocol) + "://" + ConfigurationManager.GetValue(BaseConstants.ApplicationDomain); ;
                }
                //UserName_txt.SetTextWithoutCapture(userName);
                //if (string.IsNullOrEmpty(UserName_txt.GetText()))
                    UserName_txt.SetText(userName);
                return this;
            }

            internal LoginPage LoginWithPassword(string pwd)
            {
                //Password_txt.SetTextWithoutCapture(pwd);
                //if (string.IsNullOrEmpty(Password_txt.GetText()))
                    Password_txt.SetText(pwd);
                return this;
            }

            internal void SignIn()
            {
                Login_btn.GetWrappedControl().Click();
                PageLoad();
            }
        }

        public static void DownloadFile(string href, string folderPath)
        {
            using (var wc = new WebClient())
            {
                string file = Path.GetFileName(href);
                wc.DownloadFile(
                    // Param1 = Link of file
                    new Uri(href),
                   // Param2 = Path to save
                   folderPath
                );
            }
        }

        public static void SwitchToAnotherOne(string anotherOneName)
        {
            try
            {
                CommonHelper.SwitchToAnotherOne(anotherOneName);
            }
            catch (ElementNotInteractableException)
            {

            }
            finally
            {
                CommonHelper.SwitchToAnotherOne(anotherOneName);
            }

        }

    }
}
