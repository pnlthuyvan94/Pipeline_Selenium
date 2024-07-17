using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Building_Phase;
using Pipeline.Testing.Pages.DashboardPage;
using Pipeline.Testing.Pages.Login;

namespace Pipeline.Testing.Script
{
    [TestFixture]
    [Ignore("Ignore a test because this is script for debug purpose")]
    public class DebugScript : BaseTestScript
    {
        private LoginPage _page;

        readonly string pathExcelFile_1 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\LoginParams.xlsx";
        readonly string pathExcelFile_2 = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\DashboardParams.xlsx";

        [SetUp]
        public void SetupTest()
        {
            _page = new LoginPage(pathExcelFile_1);
        }


        [Test]
        public void Test_Debug()
        {
            _page.LoginWithUser("123")
                 .LoginWithPassword("456");

            //a.FilterByColumn("Name", GridFilterOperator.Contains, "abc");

            //var b = a.FindElement(FindType.XPath, "./div/input");
            //_page.LoginWithUser("hnguyen")
            //           .LoginWithPassword("hnguyen")
            //           .SignIn();

            //DashboardPage _dashboardPage = new DashboardPage(pathExcelFile_2);
            //var a = _dashboardPage.Welcome_lbl;
            //try
            //{
            //    string b = a.Text;
            //}

            //catch (InvalidElementStateException e)
            //{
            //    Console.WriteLine(e.ToString());
            //}

            // //Using Assert helper to verify
            // AssertHelper.AreEqual("1", "2", ref Log);
            // Console.WriteLine(Log);
            // // using assert of Nunit

            // Assert.IsNotEmpty(_dashboardPage.Welcome_lbl.Text);
        }
    }
}
