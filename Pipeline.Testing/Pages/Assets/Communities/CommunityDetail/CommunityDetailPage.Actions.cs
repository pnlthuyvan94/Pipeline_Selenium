using OpenQA.Selenium;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Pages.Assets.Communities.CommunityDetail
{
    public partial class CommunityDetailPage
    {
        public CommunityDetailPage EnterCommunityName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                CommunityName_txt.SetText(name);
            return this;
        }

        public CommunityDetailPage EnterCommunityCode(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Code_txt.SetText(description);
            return this;
        }

        public string SelectDivision(string description)
        {
                return Division_ddl.SelectItemByValueOrIndex(description, 1);
        }

        public CommunityDetailPage EnterCity(string description)
        {
            if (!string.IsNullOrEmpty(description))
                City_txt.SetText(description);
            return this;
        }

        public CommunityDetailPage EnterCityLink(string description)
        {
            if (!string.IsNullOrEmpty(description))
                CityLink_txt.SetText(description);
            return this;
        }

        public CommunityDetailPage EnterTownship(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Township_txt.SetText(description);
            return this;
        }

        public CommunityDetailPage EnterCounty(string description)
        {
            if (!string.IsNullOrEmpty(description))
                County_txt.SetText(description);
            return this;
        }

        public CommunityDetailPage EnterState(string description)
        {
            if (!string.IsNullOrEmpty(description))
                State_txt.SetText(description);
            return this;
        }

        public CommunityDetailPage EnterZip(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Zip_txt.SetText(description);
            return this;
        }

        public CommunityDetailPage EnterSchool(string description)
        {
            if (!string.IsNullOrEmpty(description))
                SchoolDesstrict_txt.SetText(description);
            return this;
        }
        public CommunityDetailPage EnterSchoolLink(string description)
        {
            if (!string.IsNullOrEmpty(description))
                SchoolDesstrictLink_txt.SetText(description);
            return this;
        }
        public CommunityDetailPage EnterStatus(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Status_ddl.SelectItem(description, true);
            return this;
        }

        public CommunityDetailPage EnterSlug(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Slug_txt.SetText(description);
            return this;
        }

        public CommunityDetailPage EnterCommunityDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Description_txt.SetText(description);
            return this;
        }

        public CommunityDetailPage EnterCommunityDrivingDirection(string description)
        {
            if (!string.IsNullOrEmpty(description))
                DrivingDescription_txt.SetText(description);
            return this;
        }

        public void Save()
        {
            //Scroll to top
            System.Threading.Thread.Sleep(100);
            CommonHelper.JavaScriptExecutor("window.scrollTo(0, 0);");
            System.Threading.Thread.Sleep(300);
            Save_btn.Click();
        }

        public void AddOrUpdateCommunity(CommunityData Community, bool isUpdated = false)
        {
            EnterCommunityName(Community.Name)
                .SelectDivision(Community.Division);
                 EnterCommunityCode(Community.Code)
                .EnterCity(Community.City)
                .EnterCityLink(Community.CityLink)
                .EnterTownship(Community.Township)
                .EnterCounty(Community.County)
                .EnterState(Community.State)
                .EnterZip(Community.Zip)
                .EnterSchool(Community.SchoolDistrict)
                .EnterSchoolLink(Community.SchoolDistrictLink)
                .EnterStatus(Community.Status)
                .EnterCommunityDescription(Community.Description)
                .EnterCommunityDrivingDirection(Community.DrivingDirections)
                .EnterSlug(Community.Slug)
                .Save();

            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbLoadingAnimation']/div[1]", 4000);

            if (!isUpdated)
                PageLoad();                         
        }

        public CommunityDetailPage SelectLot(string lotStatus)
        {
            if (!string.IsNullOrEmpty(lotStatus))
                PlannerStatus_ddl.SelectItem(lotStatus, true);
            return this;
        }

        public void AddNewLotStatus()
        {
            //Scroll to top
            System.Threading.Thread.Sleep(100);
            CommonHelper.MoveToElement(AddLotStatus_btn, true);
            System.Threading.Thread.Sleep(300);
            AddLotStatus_btn.Click();
            AddLotStatusSub_btn.WaitForElementIsVisible(20);
        }

        public void SelectNewLotStatus(string status)
        {
            // Select status and add
            if (!string.IsNullOrEmpty(status))
            {
                PlannerStatus_ddl.SelectItem(status, true);
                AddLotStatusSub_btn.Click();

                // Loading grid
                var addNewStatusloadingGrid = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlUnusedStatus']/div[1]";
                WaitingLoadingGifByXpath(addNewStatusloadingGrid);
            }

        }

        public bool IsAddStatusSuccessfully(string statusItem)
        {
            return SelectedStatus_lst.IsItemExisted(GridFilterOperator.EqualTo, statusItem);
        }

        public bool UploadCommunityImage(string imagePath)
        {
            UploadCommunity_txt.SendKeysWithoutClear(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + imagePath);
            PageLoad();
            IWebElement element = CommonHelper.VisibilityOfAllElementsLocatedBy(50, "//*[@id='ctl00_CPH_Content_imgCommunity' and contains(@src,'FileId=')]");
            if (element != null && element.Displayed)
                return true;
            return false;
        }

        /// <summary>
        /// Add lot status by name
        /// </summary>
        /// <param name="StatusItems"></param>
        public void AddLotStatus(params string[] StatusItems)
        {
            // Step 7. Add lot Status button
            AddNewLotStatus();

            // Waiting to load new grid view
            System.Threading.Thread.Sleep(3000);

            // Verify new status is added to the Grid view
            foreach (string item in StatusItems)
            {
                SelectNewLotStatus(item);

                // Move to the end of page
                CommonHelper.ScrollToEndOfPage();

                // Verify new item is added to the grid view
                if (IsAddStatusSuccessfully(item + ":") is false)
                    ExtentReportsHelper.LogFail($"<font color='green'><b>New lot with name: {item} status is added unsuccessful.</b></font>");
                else
                    ExtentReportsHelper.LogPass($"<font color='red'>New lot with status: {item} is NOT added successful to the grid view.</font>");
            }
        }

    }
}
