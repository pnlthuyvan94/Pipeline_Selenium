using AventStack.ExtentReports;
using NPOI.SS.Formula.Functions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Pipeline.Common.Enums;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;

namespace Pipeline.Common.BaseClass
{
    internal static class BaseValues
    { 
		public const string TARGET_APP = "Pipeline";
	
        internal static bool MaximizeBrowser { get; set; }

        internal static int BrowserWidth { get; set; }

        internal static int BrowserHeight { get; set; }

        internal static string BrowserTarget { get; set; }

        internal static string Protocol { get; set; }

        internal static string PathReportFile { get; set; }

        internal static string ApplicationDomain { get; set; }

        internal static string ApplicationRoot { get; set; }

        internal static bool IsAutoLogin { get; set; }

        internal static bool IsCaptureEverything { get; set; }

        internal static int PageloadTimeouts { get; set; }

        internal static bool SaveImageAsBase64 { get; set; }

        internal static IWebDriver DriverSession = null;

        internal static string DashboardURL { get; set; }

        internal static string BaselineFilesDir { get; set; }

        internal static string BaseApiUrl { get; set; }
        internal static string BasePath { get; set; }

        internal static IWebDriver InitDriverSession
        {
            get
            {
                int.TryParse(Utils.ConfigurationManager.GetValue("PageloadTimeouts"), out int timeOut);
                
                if (BrowserTarget.Equals(BrowserType.Chrome.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    //Define Chrome browser options
                    ChromeOptions options = new ChromeOptions();

                    //Prevent Selenium from enabling Chrome's automation mode (comment these lines out if tests are behaving unexpectedly)
                    //options.AddExcludedArgument("enable-automation");
                    //options.AddAdditionalChromeOption("useAutomationExtension", false);

                    options.AddArguments(new string[]{
                        "no-sandbox",  //Using this argument to prevent occasional timeout errors
                        "window-position=0,0",
                        $"window-size={BaseValues.BrowserWidth},{BaseValues.BrowserHeight}",
                        "start-maximized",
                        "ignore-certificate-errors",
                        "allow-running-insecure-content",
                        "allow-insecure-localhost",
                        "disable-dev-shm-usage",
                        "headless=new"
                    });

                    //Set default download location for saving file
                    string downloadFolder = PathReportFile + "Download\\";
                    Directory.CreateDirectory(downloadFolder);
                    options.AddUserProfilePreference("download.default_directory", downloadFolder);
                    options.AddUserProfilePreference("savefile.default_directory", downloadFolder);
                    options.AddUserProfilePreference("safebrowsing.enabled", true);


                    // Headless=mew has an issue with maximized argument. Just applying this fix
                    options.AddArgument("--window-size=1920,1080");
                    options.AddArgument("force-device-scale-factor=1");
                    options.AddArgument("--start-maximized");

                    //Get chromedriver executable from the filesystem
                    ChromeDriverService service = ChromeDriverService.CreateDefaultService();

                    //Create the driver instance
                    return new ChromeDriver(service, options, TimeSpan.FromSeconds(timeOut));
                }
                else if (BrowserTarget.Equals(BrowserType.Firefox.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    FirefoxOptions driver = new FirefoxOptions();
                    return new FirefoxDriver(driver);
                }
                else if (BrowserTarget.Equals(BrowserType.InternetExplorer.ToString(), StringComparison.InvariantCultureIgnoreCase))
                { // For IE

                    InternetExplorerOptions driver = new InternetExplorerOptions();
                    return new InternetExplorerDriver(driver);
                }
                else
                    throw new ArgumentException("You need to set a valid browser type. It should Chrome/Firefox/InternetExplorer.");

            }

        }

        private static readonly Lazy<ExtentReports> _lazy = new Lazy<ExtentReports>(() => new ExtentReports());

        internal static ExtentReports ExtentReports { get { return _lazy.Value; } }

        //internal static ExtentKlovReporter KlovReport { get { return _lazyKlov.Value; } }

        internal static string ProjectName
        {
            get
            {
                string app_server = Utils.CommonHelper.get_cased_name(Utils.ConfigurationManager.GetValue("ApplicationDomain"));
                return $"{TARGET_APP} Automation - pipeline-dev45";
            }
        }

        internal static void UpdateConnectionString(string newDb, string dataSource = "10.193.34.110\\SQLEXPRESS2008", string user = "pcms", string password = "1[cgvisions")
        {
            //Assembly.GetExecutingAssembly().Location
            Configuration configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStringsSection = (ConnectionStringsSection)configuration.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings["MainConnectionString"].ConnectionString = $@"data source={dataSource};Initial Catalog={newDb}; User Id={user}; Password={password}";
            configuration.Save();
            System.Configuration.ConfigurationManager.RefreshSection("connectionStrings");
        }


        internal static void ExecuteResetIndexSqlScript()
        {
            ConnectionStringSettings connection = ConfigurationManager.ConnectionStrings["MainConnectionString"];
            string connectionString = connection.ConnectionString;
            string script = File.ReadAllText(@"SqlScript/reset-index.sql");

            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand(script, conn))
                    {
                        command.CommandTimeout = 300;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error occurred: " + e.Message);
            }
        }

    }
}
