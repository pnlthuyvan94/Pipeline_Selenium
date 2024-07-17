using AventStack.ExtentReports.Reporter;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;

namespace Pipeline.Common
{
    /// <summary>
    /// This class is called automatically by NUnit, allows to run setup and/or teardown code once for all tests under the same namespace.
    /// </summary>
    public abstract class InitialSetting
    {
        public static string pathReportFolder;
        private static log4net.ILog Log;
        // Initial Extent reports
        static InitialSetting()
        {
            // Init Dashboard url from app config file
            BaseValues.Protocol = ConfigurationManager.GetValue(BaseConstants.ApplicationProtocol);
            BaseValues.ApplicationDomain = ConfigurationManager.GetValue(BaseConstants.ApplicationDomain);
            BaseValues.ApplicationRoot = ConfigurationManager.GetValue(BaseConstants.ApplicationRoot);
            BaseValues.BaselineFilesDir = ConfigurationManager.GetValue(BaseConstants.BaselineFilesDir);

            BaseValues.DashboardURL = BaseValues.Protocol + "://" + BaseValues.ApplicationDomain
                + "." + BaseValues.ApplicationRoot;

            // The default report labb folder for all tenants
            string applicationLabb = "develop";

            string BuildCounter = Environment.GetEnvironmentVariable("BUILD_NUMBER");
            if (string.IsNullOrEmpty(BuildCounter))
            {
                BuildCounter = ConfigurationManager.GetValue("BuildCounter");
                if (string.IsNullOrEmpty(BuildCounter))
                    BaseValues.PathReportFile = $"{ConfigurationManager.GetValue("ReportSaveLocation")}\\{BaseValues.ProjectName}\\{applicationLabb}\\Report {DateTime.Now:ddd dd-MMM-yyyy HH-mm-ss}\\";
                else
                    BaseValues.PathReportFile = $"{ConfigurationManager.GetValue("ReportSaveLocation")}\\{BaseValues.ProjectName}\\{applicationLabb}\\Report Build No.{BuildCounter}\\";
            }
            else
                BaseValues.PathReportFile = $"{ConfigurationManager.GetValue("ReportSaveLocation")}\\{BaseValues.ProjectName}\\{applicationLabb}\\Report Build No.{BuildCounter}\\";

            // log file path
            Directory.CreateDirectory(BaseValues.PathReportFile);

            GlobalContext.Properties["LogFileName"] = $@"{BaseValues.PathReportFile}myLog.log";
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\log4net.config"));
            Log = LogManager.GetLogger(typeof(InitialSetting));

            Log.Debug("Loading report config at path: " + BaseValues.PathReportFile);

            var htmlReporter = new ExtentHtmlReporter(BaseValues.PathReportFile);
            htmlReporter.LoadConfig(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\extent-config.xml");
            BaseValues.ExtentReports.AttachReporter(htmlReporter);

            Log.Debug("Creating report file: " + htmlReporter.ReporterName);

            bool.TryParse(ConfigurationManager.GetValue(BaseConstants.IsCaptureEverything), out bool capture);
            BaseValues.IsCaptureEverything = capture;
            pathReportFolder = BaseValues.PathReportFile;

            //Setup Rest API Url
            var temp = BaseValues.ApplicationRoot.Split('/');
            BaseValues.BaseApiUrl = BaseValues.Protocol + "://" + BaseValues.ApplicationDomain
                + "." + temp[0];
            BaseValues.BasePath = temp[1];

            CommonHelper.InitLog();
        }

        public static void RunBeforeAnyTests()
        {
            string domain = ConfigurationManager.GetValue(BaseConstants.ApplicationDomain);
            string root = ConfigurationManager.GetValue(BaseConstants.ApplicationRoot);
            string protocol = ConfigurationManager.GetValue(BaseConstants.ApplicationProtocol);
            string browser = ConfigurationManager.GetValue(BaseConstants.BrowserTarget);

            bool.TryParse(ConfigurationManager.GetValue(BaseConstants.MaximizeBrowser), out bool isBrowserMaximized);
            int.TryParse(ConfigurationManager.GetValue(BaseConstants.BrowserWidth), out int browser_width);
            int.TryParse(ConfigurationManager.GetValue(BaseConstants.BrowserHeight), out int browser_height);
            bool.TryParse(ConfigurationManager.GetValue(BaseConstants.IsAutoLogin), out bool isAutoLogin);

            if (domain == "release" || domain == "packaged_release")
                domain = string.Empty;

            // Initial driver
            BaseValues.ApplicationDomain = domain;
            BaseValues.ApplicationRoot = root;
            BaseValues.Protocol = protocol;
            BaseValues.BrowserTarget = browser;
            BaseValues.MaximizeBrowser = isBrowserMaximized;
            BaseValues.BrowserWidth = browser_width;
            BaseValues.BrowserHeight = browser_height;
            BaseValues.IsAutoLogin = isAutoLogin;

            // Invoke the browser
            BaseValues.DriverSession = BaseValues.InitDriverSession;

            // Setting the driver will wait 120s when invoke browser then navigate to URL
            int.TryParse(ConfigurationManager.GetValue(BaseConstants.PageloadTimeouts), out int timeOut);
            if (timeOut == 0)
                BaseValues.PageloadTimeouts = 60;
            else
                BaseValues.PageloadTimeouts = timeOut;

            //Set the timeout defined in config
            BaseValues.DriverSession.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(BaseValues.PageloadTimeouts);

            // Navigate to the URL
            BaseValues.DriverSession.Url = BaseValues.DashboardURL;

            BasePage.PageLoad();
            ((ChromeDriver)BaseValues.DriverSession).Manage().Window.Maximize();
            CommonHelper.FitViewportToContentByJavascript();
            CommonHelper.EmulateDevice(BaseValues.BrowserWidth, BaseValues.BrowserHeight);

            //Release the user license for Automation if user limit page is shown
            if (BaseValues.DriverSession.Url.ToLower().Contains("userlimit"))
            {
                try
                {
                    ReadOnlyCollection<IWebElement> release_licenses_usernames = BaseValues.DriverSession.FindElements(By.XPath("//*/div/p/strong"));
                    int index = -1;
                    for (int i = 0; i < release_licenses_usernames.Count; i++)
                    {
                        string username_str = release_licenses_usernames[i].Text;
                        if (username_str.ToLower().Contains("automation"))
                            index = i;
                    }
                    if (index != -1)
                    {
                        string index_str = (index >= 10) ? index.ToString() : $"0{index}";
                        IWebElement release_license_btn = BaseValues.DriverSession.FindElement(By.CssSelector($"a[id*='ctl{index_str}_lbBumpUser']"));
                        if (release_license_btn != null)
                            CommonHelper.click_element(release_license_btn);
                    }
                }
                catch { }
            }

            // Get current title to get the name of db
            // Default is db for estimating site: pipeline_dev_estimating
            if (ConfigurationManager.GetValue("ApplicationDomain").ToLower().StartsWith("beta")
                 || ConfigurationManager.GetValue("ApplicationDomain") == string.Empty)
            {
                var dbVersion = "pipeline_dev_" + ConfigurationManager.GetValue("dbNameVersion");
                if (!string.IsNullOrEmpty(dbVersion))
                    BaseValues.UpdateConnectionString(dbVersion);
                else
                {
                    //Version number is not always in Page title (login page for example). So just use '20220' here for now in this case, but it should already have a default value of the correct db version in App.config and in TeamCity parameters.
                    string db_version = "pipeline_dev_20220";
                    BaseValues.UpdateConnectionString(db_version);
                }
            }
            else if (ConfigurationManager.GetValue("ApplicationDomain").ToLower().StartsWith("estimating"))
            {
                var dbVersion = "pipeline_dev_estimating_09";
                BaseValues.UpdateConnectionString(dbVersion);
            }
            else if (ConfigurationManager.GetValue("ApplicationDomain").ToLower().StartsWith("pipeline-dev45")
                 || ConfigurationManager.GetValue("ApplicationDomain").ToLower().StartsWith("pipeline-dev46"))
            {
                string dbVersion = string.Empty;
                string applicationDomain = BaseValues.ApplicationDomain.ToLower();
                string applicationRoot = BaseValues.ApplicationRoot.ToLower();

                if (applicationDomain.Equals("pipeline-dev45"))

                {

                    if (applicationRoot.Contains("/release1/"))

                        dbVersion = "293-sql-pipeline-release1";

                    else if (applicationRoot.Contains("/release2/"))

                        dbVersion = "293-SQL-PIPELINE-release2";

                    else if (applicationRoot.Contains("/release3/"))

                        dbVersion = "293-SQL-PIPELINE-release3";

                    else if (applicationRoot.Contains("/dev1/"))

                        dbVersion = "293-sql-pipeline-dev1";

                    else if (applicationRoot.Contains("/dev2/"))

                        dbVersion = "293-sql-pipeline-dev2";

                    else if (applicationRoot.Contains("/dev3/"))

                        dbVersion = "293-sql-pipeline-dev3";

                    else if (applicationRoot.Contains("/dev4/"))

                        dbVersion = "293-sql-pipeline-dev4";

                    else if (applicationRoot.Contains("/dev5/"))

                        dbVersion = "293-sql-pipeline-dev5";

                    else if (applicationRoot.Contains("/dev6/"))

                        dbVersion = "293-sql-pipeline-dev6";

                    else if (applicationRoot.Contains("/dev7/"))

                        dbVersion = "293-sql-pipeline-dev7";

                    else if (applicationRoot.Contains("/dev8/"))

                        dbVersion = "293-sql-pipeline-dev8";

                    else if (applicationRoot.Contains("/dev9/"))

                        dbVersion = "293-sql-pipeline-dev9";

                    else if (applicationRoot.Contains("/dev10/"))

                        dbVersion = "293-sql-pipeline-dev10";

                    else if (applicationRoot.Contains("/develop/"))

                        dbVersion = "293-sql-pipeline-develop";

                    else if (applicationRoot.Contains("/pur1/"))

                        dbVersion = "293-sql-pipeline-pur1";

                    else if (applicationRoot.Contains("/pur2/"))

                        dbVersion = "293-sql-pipeline-pur2";

                    else if (applicationRoot.Contains("/pur3/"))

                        dbVersion = "293-sql-pipeline-pur3";

                    else if (applicationRoot.Contains("/pur4/"))

                        dbVersion = "293-sql-pipeline-pur4";

                    else if (applicationRoot.Contains("/pur5/"))

                        dbVersion = "293-sql-pipeline-pur5";

                    else if (applicationRoot.Contains("/pur6/"))

                        dbVersion = "293-sql-pipeline-pur6";

                    else if (applicationRoot.Contains("/pur7/"))

                        dbVersion = "293-sql-pipeline-pur7";

                    else if (applicationRoot.Contains("/sale1/"))

                        dbVersion = "293-sql-pipeline-sale1";

                    else if (applicationRoot.Contains("/integration1/"))

                        dbVersion = "293-sql-pipeline-integration1";

                    else if (applicationRoot.Contains("/integration2/"))

                        dbVersion = "293-sql-pipeline-integration2";

                    else if (applicationRoot.Contains("/lennarportlandinternal/"))

                        dbVersion = "293-sql-pipeline-lennarportlandinternal";

                }

                else if (applicationDomain.Equals("pipeline-dev46"))

                {

                    if (applicationRoot.Contains("/simpsonqa20223/"))

                        dbVersion = "293-SQL-PIPELINE-simpsonqa20223";

                    else if (applicationRoot.Contains("/simpsonqa202300/"))

                        dbVersion = "293-SQL-PIPELINE-simpsonqa202300";

                    else if (applicationRoot.Contains("/simpsonqa202301/"))

                        dbVersion = "293-SQL-PIPELINE-simpsonqa202301";

                    else if (applicationRoot.Contains("/simpsonqabeta/"))

                        dbVersion = "293-SQL-PIPELINE-simpsonqabeta";

                    else if (applicationRoot.Contains("/simpsonqamaster/"))

                        dbVersion = "293-SQL-PIPELINE-simpsonqamaster";

                    else if (applicationRoot.Contains("/simpsonqastaging/"))

                        dbVersion = "293-SQL-PIPELINE-simpsonqastaging";

                }

                var dataSource = "tcp:293-sql-dev-04.database.windows.net,1433";
                var userName = "pcms";
                var password = "1[cgvisions";

                BaseValues.UpdateConnectionString(dbVersion, dataSource, userName, password);
                BaseValues.ExecuteResetIndexSqlScript();
            }
            FindElementHelper.Instance().SetDefaultWait();
        }

        public static void RunAfterAnyTests()
        {
            CleanUpAllDriver();
            BaseValues.ExtentReports.Flush();
        }

        public static void CloseCurrentDriver()
        {
            if (BaseValues.DriverSession != null)
            {
                BaseValues.DriverSession.Quit();
            }
            BaseValues.DriverSession = null;
        }

        public static void CleanUpAllDriver()
        {
            InitialSetting.CloseCurrentDriver();

            if (BaseValues.BrowserTarget.Equals(BrowserType.Chrome.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                KillProcessAndChildren("chromedriver.exe");
            }
            else if (BaseValues.BrowserTarget.Equals(BrowserType.Firefox.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                KillProcessAndChildren("geckodriver.exe");
            }
            else if (BaseValues.BrowserTarget.Equals(BrowserType.InternetExplorer.ToString(), StringComparison.InvariantCultureIgnoreCase))
            {
                // For IE
                KillProcessAndChildren("IEDriverServer.exe");
            }
        }

        public static string AssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        private static void KillProcessAndChildren(string p_name)
        {
            var assemblyDirector = AssemblyDirectory();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
             ("Select * From Win32_Process Where Name = '" + p_name + "'");
            var moc = searcher.Get().Cast<ManagementObject>().ToList();
            var currentDriverProcess = moc.Where(mo => mo["ExecutablePath"].ToString().Contains(assemblyDirector)).FirstOrDefault();
            if (null != currentDriverProcess) KillProcessAndChildren(Convert.ToInt32(currentDriverProcess["ProcessID"]));
        }

        private static void KillProcessAndChildren(int pid)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher
             ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {

                try
                {
                    KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
                }
                catch
                {
                    break;
                }
            }

            try
            {
                Process proc = Process.GetProcessById(pid);

                proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
        }

    }
}
