using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System;
using System.IO;

namespace Pipeline.Testing.Pages.Assets.House.FloorPlans
{
    public partial class HouseFloorPlanDetailPage : DetailsContentPage<HouseFloorPlanDetailPage>
    {
        public string GetFileNameByLevel(FloorPlanLevel planLevel, bool waitToExist = false)
        {
            string xpathFileName = "//*[@id='myTable']/tbody/tr[./td[1][.='{0}']]/td[2]//span";
            switch (planLevel)
            {
                case FloorPlanLevel.Basement:
                    xpathFileName = string.Format(xpathFileName, "Basement");
                    break;
                case FloorPlanLevel.FirstFloor:
                    xpathFileName = string.Format(xpathFileName, "First Floor");
                    break;
                default:
                    xpathFileName = string.Format(xpathFileName, "Second Floor");
                    break;
            }
            Label fileName_lbl = new Label(FindType.XPath, xpathFileName);
            if (waitToExist)
                fileName_lbl.WaitUntilExist(10);
            fileName_lbl.WaitForElementIsVisible(10);
            if (!fileName_lbl.IsNull())
            {
                return fileName_lbl.RefreshWrappedControl().Text;
            }
            else
                return string.Empty;
        }

        public void UploadImageByLevelAndVerify(FloorPlanLevel planLevel, string path)
        {
            string xpathUpload = "//*[@id='myTable']/tbody/tr[./td[1][.='{0}']]/td[3]//input[@type='file']";
            string xpathProgress = "//*[@id='myTable']/tbody/tr[./td[1][.='{0}']]/td[3]//span[@class='ruUploadProgress']";
            string idImg = string.Empty;
            switch (planLevel)
            {
                case FloorPlanLevel.Basement:
                    xpathUpload = string.Format(xpathUpload, "Basement");
                    xpathProgress = string.Format(xpathProgress, "Basement");
                    idImg = "ctl00_CPH_Content_imgBasement";
                    break;
                case FloorPlanLevel.FirstFloor:
                    xpathUpload = string.Format(xpathUpload, "First Floor");
                    xpathProgress = string.Format(xpathProgress, "First Floor");
                    idImg = "ctl00_CPH_Content_imgFirstFloor";
                    break;
                default:
                    xpathUpload = string.Format(xpathUpload, "Second Floor");
                    xpathProgress = string.Format(xpathProgress, "Second Floor");
                    idImg = "ctl00_CPH_Content_imgSecondFloor";
                    break;
            }
            Label progressBar = new Label(FindType.XPath, xpathProgress);
            Textbox upload_txt = new Textbox(FindType.XPath, xpathUpload);

            string imgName = Path.GetFileName(path);

            ExtentReportsHelper.LogInformation($" [[DEBUG]] Started uploading file '{imgName}' ...");

            try
            {
                upload_txt.AppendKeys(path);
            }
            catch (Exception ex)
            {
                ExtentReportsHelper.LogWarning("Caught null reference while uploading file (ignore if running remotely).");
            }
            ExtentReportsHelper.LogInformation(" [[DEBUG]] Finished sending file...");

            progressBar.WaitUntilExist(1);
            progressBar.WaitForElementIsInVisible(60);

            System.Threading.Thread.Sleep(500);

            string uploadMsg = "Image added successfully.";

            ExtentReportsHelper.LogInformation(" [[DEBUG]] Getting last toast message...");

            string actualMsg = HouseFloorPlanDetailPage.Instance.GetLastestToastMessage(1);
            if (uploadMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"The resources with name <font color='green'><b>{imgName}</b></font> of <b>{planLevel:g}</b> is uploaded successfully after 60s.");
                //CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(actualMsg))
            {
                ExtentReportsHelper.LogFail($"The resources with name <font color='green'><b>{imgName}</b></font> of <b>{planLevel:g}</b> is NOT uploaded successfully after 60s.");
                //CloseToastMessage();
            }

            ExtentReportsHelper.LogInformation(" [[DEBUG]] Closing toast message...");

            HouseFloorPlanDetailPage.Instance.CloseToastMessage();

            var imageUploaded = new Label(FindType.Id, idImg);
            if (!imageUploaded.WaitForElementIsVisible(60))
                ExtentReportsHelper.LogFail($"The resources of <b>{planLevel:g}</b> is NOT upload successfully after 60s.");

            // Verify name
            string fileName = GetFileNameByLevel(planLevel, true);
            if (fileName.Equals(imgName))
                ExtentReportsHelper.LogPass($"The resources of <b>{planLevel:g}</b> is uploaded successfully..");
            else
                ExtentReportsHelper.LogFail($"The resources of <b>{planLevel:g}</b> is NOT upload successfully.<br>Expected: {imgName}<br>Actual: {fileName}");

        }

        public void DeleteResourceByLevelAndVerify(FloorPlanLevel planLevel)
        {
            string xpathDeleteBtn = "//*[@id='myTable']/tbody/tr[./td[1][.='{0}']]/td[4]/input";
            switch (planLevel)
            {
                case FloorPlanLevel.Basement:
                    xpathDeleteBtn = string.Format(xpathDeleteBtn, "Basement");
                    break;
                case FloorPlanLevel.FirstFloor:
                    xpathDeleteBtn = string.Format(xpathDeleteBtn, "First Floor");
                    break;
                default:
                    xpathDeleteBtn = string.Format(xpathDeleteBtn, "Second Floor");
                    break;
            }

            Button delete_Btn = new Button(FindType.XPath, xpathDeleteBtn);
            delete_Btn.Click();
            ConfirmDialog(ConfirmType.OK);
            PageLoad();

            // Verify
            if (string.IsNullOrEmpty(GetFileNameByLevel(planLevel)))
                ExtentReportsHelper.LogPass($"The level with name {planLevel:g} is removed successfully.");
            else
                ExtentReportsHelper.LogFail($"The level with name {planLevel:g} is NOT remove successfully.");

        }


    }
}
