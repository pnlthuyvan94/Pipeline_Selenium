using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Settings.Saberis;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Pipeline.Testing.Script.Integrations
{
    public class RT_01182 : BaseTestScript
    {

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Integrations);
        }

        #region"Test case"
        [Test]
        [Category("Integrations")]
        [Ignore("Integration test scripts will be ignored at this time and be implemented in the future.")]
        public void IT01_Pipeline_Saberis()
        {
            // super admin
            string _superUserName = "sysadmin";
            string _superPass = "shoreline";

            string _url = "http://dev2.saberis.com";
            string _username = "Strongtie";
            string _password = "Steve1234$";
            string _jobName = "Pre-RT-Option-Bid-05";
            // Step 1: navigate to this setting page
            SaberisPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);

            // Step 2: click on "+" Add button
            SaberisPage.Instance.LeftMenuNavigation("Saberis");

            // Log out and re-log in with super admin user to modified
            ExtentReportsHelper.LogInformation("Log out and re-log in with super admin user to update the system information");
            CommonFuncs.LogOut();
            CommonFuncs.LoginToPipeline(_superUserName, _superPass);
            SaberisPage.Instance.EnterInformation(_url, _username, _password)
                .RunningSaberis(true).SaveSetting();

            // Verify successful save and appropriate success message.
            string _expectedMessage = "Changes saved!";
            string _actualMessage = SaberisPage.Instance.GetLastestToastMessage();
            if (_expectedMessage == _actualMessage)
            {
                ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
                SaberisPage.Instance.CloseToastMessage();
            }

            // Log out and re-log in with normal user
            ExtentReportsHelper.LogInformation("Log out and re-log in with normal user");
            CommonFuncs.LogOut();
            CommonFuncs.LoginToPipeline(ConfigurationManager.GetValue("UserName"), ConfigurationManager.GetValue("Password"));

            // Go to the Job page
            SaberisPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.EnterJobNameToFilter("Job Number", _jobName);


            // Verify the new OptionSelection create successfully

            bool isFound = JobPage.Instance.IsItemInGrid("Job Number", _jobName);
            Assert.That(isFound, string.Format("The job for Saberis testing \"{0} \" was not display on grid.", _jobName));

            JobPage.Instance.OpenJobDetailPage("Job Number", _jobName);
            // Go to Job BOM
            JobDetailPage.Instance.LeftMenuNavigation("Job BOM");

            // Asert the Saberis Sync is displayed on screen
            if (JobDetailPage.Instance.IsSaberisDisplayed)
                ExtentReportsHelper.LogPass("The button Saberis is displayed");
            else
            {
                ExtentReportsHelper.LogFail("The button Saberis is NOT displayed");
                Assert.Fail("The button Saberis is NOT displayed.");
            }

            // Genarate BOM
            JobDetailPage.Instance.GenerateBOM();

            ViewByData viewby = new ViewByData();
            JobDetailPage.Instance.ViewBomPageBy(viewby.Getdata()[0].Pipeline).SyncSerberisWithPhaseName("Pre-RT-Option-Bid-Phase-05");
            // TODO: User this for local dev remember to -11 hrs
            string fileName = $"StrongTieStrongTie{DateTime.Now:yyyyMMddHHmm}.xml";

            string _expectedContent = $"<SaberisOrderDocument><DocumentVersion>1.0</DocumentVersion><Order><Source>Simpson</Source><Date>{ DateTime.Now:yyyy.MM.dd}";
            _expectedMessage = "Job BOM sent to Saberis";
            _actualMessage = JobDetailPage.Instance.GetLastestToastMessage();
            if (_expectedMessage == _actualMessage)
            {
                ExtentReportsHelper.LogPass($"The data with view by <font color='green'><b>{viewby.Getdata()[0].Pipeline}</b></font> sync successfully with message <font color='green'><b>{_actualMessage}</b></font>");
                JobDetailPage.Instance.CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail(
                    $"The file could not be uploaded correctly, please check it manually. View By {viewby.Getdata()[0].Pipeline}." +
                    $" Actual message:<font color='green'><b> {_actualMessage}</b></font>");
                Assert.Fail($"The file could not be uploaded correctly, please check it manually. View By {viewby.Getdata()[0].Pipeline}." +
                    $" Actual message:{_actualMessage}");
            }

            GoToSaverisPage(_url, _username, _password);
            isFound = SaberisPage.Instance.IsProcessedFileExisted(fileName);
            if (!isFound)
            {
                ExtentReportsHelper.LogFail($"The xml file with name {fileName} could not be found in the grid.");
                Assert.Fail($"The xml file with name {fileName} could not be found in the grid.");
            }
            // Download file
            DownloadFile(SaberisPage.Instance.ProcessedFileHref, pathReportFolder + fileName);
            System.Threading.Thread.Sleep(2000);
            // Verified the file is download with name {filename}
            string content = File.ReadAllText(pathReportFolder + fileName, Encoding.UTF8);
            string _content = CommonHelper.PrettyXml(content);
            if (!content.StartsWith(_expectedContent) || string.IsNullOrEmpty(content))
            {
                ExtentReportsHelper.LogFail($@"The file could not be downloaded or the content does not correctly, please check it manually.
                                           <br><textarea rows='10' cols='40' style='border:none;'>{_content}</textarea>");
                Assert.Fail($@"The file could not be downloaded or the content does not correctly, please check it manually.{_content}");
            }
            else
            {
                ExtentReportsHelper.LogPass($@"The file is downloaded successfully with name: <font color='green'><b>{fileName}</b></font>, and content:
                                          <br><textarea rows='10' cols='40' style='border:none;'>{_content}</textarea>");
            }
            // Go back to pipeline
            GoBackPipelinePage();

            JobDetailPage.Instance.ViewBomPageBy(viewby.Getdata()[1].Pipeline).SyncSerberisWithPhaseName("Pre-RT-Option-Bid-Phase-05");
            // TODO: User this for local dev remember -11 hrs
            string fileName1 = $"StrongTieStrongTie{DateTime.Now:yyyyMMddHHmm}.xml";
            string _date1 = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            _actualMessage = JobDetailPage.Instance.GetLastestToastMessage();
            if (_expectedMessage == _actualMessage)
            {
                ExtentReportsHelper.LogPass($"The data with view by <font color='green'><b>{viewby.Getdata()[1].Pipeline}</b></font> sync successfully with message <font color='green'><b>{_actualMessage}</b></font>");
                JobDetailPage.Instance.CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail(
                    $"The file could not be uploaded correctly, please check it manually. View By {viewby.Getdata()[1].Pipeline}." +
                    $" Actual message:<font color='green'><b> {_actualMessage}</b></font>");
                Assert.Fail($"The file could not be uploaded correctly, please check it manually. View By {viewby.Getdata()[1].Pipeline}." +
                    $" Actual message: {_actualMessage}");
            }

            JobDetailPage.Instance.ViewBomPageBy(viewby.Getdata()[2].Pipeline).SyncSerberisWithOptionName("RT-Option-Bid-Cost_05");
            // TODO: User this for local dev remember to -11 hrs
            string fileName2 = $"StrongTieStrongTie{DateTime.Now:yyyyMMddHHmm}.xml";
            string _date2 = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            _actualMessage = JobDetailPage.Instance.GetLastestToastMessage();
            if (_expectedMessage == _actualMessage)
            {
                ExtentReportsHelper.LogPass($"The data with view by <font color='green'><b>{viewby.Getdata()[2].Pipeline}</b></font> sync successfully with message <font color='green'><b>{_actualMessage}</b></font>");
                JobDetailPage.Instance.CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail(
                    $"The file could not be uploaded correctly, please check it manually. View By {viewby.Getdata()[2].Pipeline}." +
                    $" Actual message:<font color='green'><b> {_actualMessage}</b></font>");
                Assert.Fail($"The file could not be uploaded correctly, please check it manually. View By {viewby.Getdata()[2].Pipeline}." +
                    $" Actual message: {_actualMessage}");
            }

            JobDetailPage.Instance.ViewBomPageBy(viewby.Getdata()[3].Pipeline).SyncSerberisWithOptionName("RT-Option-Bid-Cost_05");
            // TODO: User this for local dev remember to -11 hrs
            string fileName3 = $"StrongTieStrongTie{DateTime.Now:yyyyMMddHHmm}.xml";
            string _date3 = DateTime.Now.ToString("MM/dd/yyyy hh:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            _actualMessage = JobDetailPage.Instance.GetLastestToastMessage();
            if (_expectedMessage == _actualMessage)
            {
                ExtentReportsHelper.LogPass($"The data with view by <font color='green'><b>{viewby.Getdata()[3].Pipeline}</b></font> sync successfully with message <font color='green'><b>{_actualMessage}</b></font>");
                JobDetailPage.Instance.CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail(
                     $"The file could not be uploaded correctly, please check it manually. View By {viewby.Getdata()[3].Pipeline}." +
                     $" Actual message:<font color='green'><b> {_actualMessage}</b></font>");
                Assert.Fail($"The file could not be uploaded correctly, please check it manually. View By {viewby.Getdata()[3].Pipeline}." +
                     $" Actual message: {_actualMessage}");
            }

            // Go to saberis page and verify
            GoToSaverisPage(_url, _username, _password);
            isFound = SaberisPage.Instance.IsFileUploaded(_date1, viewby.Getdata()[1].Saberis);
            if (!isFound)
            {
                ExtentReportsHelper.LogFail($"The file could not be uploaded correctly, please check it manually. View By {viewby.Getdata()[1].Pipeline}. File Name: {fileName1}");
            }
            isFound = SaberisPage.Instance.IsFileUploaded(_date2, viewby.Getdata()[2].Saberis);
            if (!isFound)
            {
                ExtentReportsHelper.LogFail($"The file could not be uploaded correctly, please check it manually. View By {viewby.Getdata()[2].Pipeline}. File Name: {fileName2}");
            }
            isFound = SaberisPage.Instance.IsFileUploaded(_date3, viewby.Getdata()[3].Saberis);
            if (!isFound)
            {
                ExtentReportsHelper.LogFail($"The file could not be uploaded correctly, please check it manually. View By {viewby.Getdata()[2].Pipeline}. File Name: {fileName3}");
            }

            // Close current page
            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);

            // Go back to the setting page
            SaberisPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SaberisPage.Instance.LeftMenuNavigation("Saberis");
            SaberisPage.Instance.RunningSaberis(false).SaveSetting();

            // Verify successful save and appropriate success message.
            _expectedMessage = "Changes saved!";
            _actualMessage = SaberisPage.Instance.GetLastestToastMessage();
            if (_expectedMessage == _actualMessage)
            {
                ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
                SaberisPage.Instance.CloseToastMessage();
            }
            else
            {
                SaberisPage.Instance.CloseToastMessage();
            }

            // Go to the Job BOM and verified the button gone
            SaberisPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.EnterJobNameToFilter("Job Number", _jobName);

            // Verify the new OptionSelection create successfully

            isFound = JobPage.Instance.IsItemInGrid("Job Number", _jobName);
            Assert.That(isFound, string.Format("The job for Saberis testing \"{0} \" was not display on grid.", _jobName));

            JobPage.Instance.OpenJobDetailPage("Job Number", _jobName);

            // Go to Job BOM
            JobDetailPage.Instance.LeftMenuNavigation("Job BOM");

            // Asert the Saberis Sync is displayed on screen
            if (!JobDetailPage.Instance.IsSaberisDisplayed)
                ExtentReportsHelper.LogPass("The button Saberis is NOT displayed");
            else
            {
                ExtentReportsHelper.LogFail("The button Saberis is displayed");
                Assert.Fail("The button Saberis is still displayed");
            }
        }

        private void GoToSaverisPage(string urlSaberis, string username, string password)
        {
            CommonHelper.SwitchLastestTab();
            if (!CommonHelper.GetCurrentDriverURL.Contains(urlSaberis.Replace("http://", "")))
            {
                CommonHelper.OpenLinkInNewTab(urlSaberis);
                CommonHelper.SwitchLastestTab();
                // Do log in
                SaberisPage.Instance.LoginSaberis(username, password);
            }
            else if (!CommonHelper.GetCurrentDriverURL.EndsWith("Secured/Index"))
            {
                // Do log in
                SaberisPage.Instance.LoginSaberis(username, password);
            }
            else
            {
                CommonHelper.RefreshPage();
            }
        }

        private void GoBackPipelinePage()
        {
            if (!CommonHelper.GetCurrentDriverURL.Contains("bimpipeline"))
            {
                CommonHelper.SwitchTab(0);
            }
        }
        private void DownloadFile(string href, string folderPath)
        {

            using (WebClient wc = new WebClient())
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

        #endregion
    }

    public class ViewByData
    {
        private ViewByObject[] data1;

        public ViewByObject[] Getdata()
        {
            return data1;
        }

        private void Setdata(ViewByObject[] value)
        {
            data1 = value;
        }

        public ViewByData()
        {
            this.Setdata(new ViewByObject[]
             {
                new ViewByObject("Phase/Product","PhasesView"),
                new ViewByObject("Phase/Product/Use","PhasesUsesView"),
                new ViewByObject("Option/Phase/Product","OptionsView"),
                new ViewByObject("Option/Phase/Product/Use","OptionsUsesView")
             });
        }

        public class ViewByObject
        {
            public string Pipeline { get; private set; }
            public string Saberis { get; private set; }

            public ViewByObject(string pipeline, string saberis)
            {
                Pipeline = pipeline;
                Saberis = saberis;
            }
        }

    }
}
