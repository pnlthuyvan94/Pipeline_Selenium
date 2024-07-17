using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Settings.Builder.Lot;

namespace Pipeline.Testing.Script.Section_III
{
    public class J04_RT_01228 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        LotSettingData _lotData;
        [SetUp]
        public void Setup()
        {
            _lotData = new LotSettingData()
            {
                LotStatus = "RT-Add_LotStatus",
               NewLotStatus = "RT_Update-Status"
            };            
        }


        [Test]
        [Order(1)]
        [Category("Section_III")]
        public void J04_Setting_Builder_AddALotStatusSetting()
        {
            NavigateToBuilderLot();

            // On the Lot status setting, click + button
            // Verify Add Lot Status modal is displayed
            // Input value to lot status then hit save button
            LotSettingPage.Instance.CreateSettingLotStatus(_lotData.LotStatus);


            // And display on the System Lot Status Assignment
            //if (!LotSettingPage.Instance.IsLotStatusExist(originalLotStatusName))
            //{
            //    ExtentReportsHelper.LogFail("Status name does not add.");
            //    Assert.Fail();
            //}

            VerifyTheLotStatusDisplayCorrectlyOnCommunity(_lotData.LotStatus);
        }

        [Test]
        [Order(2)]
        [Category("Section_III")]
        public void J04_Setting_Builder_EditALotStatusSetting()
        {
            NavigateToBuilderLot();

            // Change page size
            LotSettingPage.Instance.ChangePageSize(50);

            // Find item on grid
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Find status with name {_lotData.LotStatus} and create if it doesn't exist.<b></b></font>");
            if (LotSettingPage.Instance.IsItemDisplayOnScreen(_lotData.LotStatus) is false)
            {
                // Create a new lot status
                LotSettingPage.Instance.CreateSettingLotStatus(_lotData.LotStatus);
            }

            // Click on edit button on the detination row
            // update the value
            // Hit save 
            LotSettingPage.Instance.EditLotStatus(_lotData.LotStatus, _lotData.NewLotStatus);

            // and verify the message
            string successfulMess = $"Status successfully updated.";
            if (successfulMess == LotSettingPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Status successfully updated.</b></font>");
                LotSettingPage.Instance.CloseToastMessage();
            }

            // Verify the items update successfully
            if (!LotSettingPage.Instance.IsItemDisplayOnScreen(_lotData.NewLotStatus))
            {
                ExtentReportsHelper.LogFail("<font color='red'>Status name does not update.</font>");
            }

            // Verify the updated Lot status is displayed on the System Lot Status Assignment
            //if (!LotSettingPage.Instance.IsLotStatusExist(newLotStatusName))
            //{
            //    ExtentReportsHelper.LogFail("Status name does not add.");
            //    Assert.Fail();
            //}

            VerifyTheLotStatusDisplayCorrectlyOnCommunity(_lotData.NewLotStatus);
        }

        [Test]
        [Order(3)]
        [Category("Section_III")]
        public void J04_Setting_Builder_DeleteALotStatusSetting()
        {
            NavigateToBuilderLot();

            // Change page size
            LotSettingPage.Instance.ChangePageSize(50);

            // Find updated status on grid
            if (LotSettingPage.Instance.IsItemDisplayOnScreen(_lotData.NewLotStatus) is false)
                ExtentReportsHelper.LogInformation(null, $"Can't find status {_lotData.NewLotStatus} to delete.");
            else
                LotSettingPage.Instance.DeleteSettingStatus(_lotData.NewLotStatus);

            // Find original status on grid
            if (LotSettingPage.Instance.IsItemDisplayOnScreen(_lotData.LotStatus) is false)
                ExtentReportsHelper.LogInformation(null, $"Can't find status {_lotData.LotStatus} to delete.");
            else
                LotSettingPage.Instance.DeleteSettingStatus(_lotData.LotStatus);


            //LotSettingPage.Instance.NavigateBackToFirstPapge();

            //// Verify the item does not exist in grid
            //if (LotSettingPage.Instance.IsItemDisplayOnScreen(newLotStatusName))
            //{
            //    ExtentReportsHelper.LogFail("Status name does not update.");
            //    Assert.Fail();
            //}

            // Verify the item does not exist in dropdown list
            //if (LotSettingPage.Instance.IsLotStatusExist(newLotStatusName))
            //{
            //    ExtentReportsHelper.LogFail("Status name does not delete.");
            //    Assert.Fail();
            //}

            VerifyTheLotStatusDisplayCorrectlyOnCommunity(_lotData.LotStatus, false);
            VerifyTheLotStatusDisplayCorrectlyOnCommunity(_lotData.NewLotStatus, false);

        }

        private void NavigateToBuilderLot()
        {
            // Go to the setting page
            LotSettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);

            // Select Builder on the left nav
            LotSettingPage.Instance.LeftMenuNavigation("Builder");

            // Select Lot view on the dropdown list
            LotSettingPage.Instance.SwichingToLotView();
        }

        private void VerifyTheLotStatusDisplayCorrectlyOnCommunity(string lotStatusName, bool expectExisted = true)
        {
            CommonHelper.ScrollToBeginOfPage();

            // Navigate to community page
            //LotSettingPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);

            // Select a community or add new
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            CommunityPage.Instance.SelectItemInGrid(0, 1);

            // Click on Add Lot Status
            if (CommunityDetailPage.Instance.AddLotStatus_btn.WaitForElementIsVisible(10, false) is false)
            {
                // Can't find add lot status button
                ExtentReportsHelper.LogInformation(null, $"<font color=' yellow'><b>Can't find add lot status button. Return.</b></font>");
                return;
            }

            CommunityDetailPage.Instance.AddNewLotStatus();

            // Verify the new item is displayed on Lot list
            if (expectExisted)
            {
                if (CommunityDetailPage.Instance.IsLotStatusExist(lotStatusName) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'><b>{lotStatusName}</b></font> Status name does not exist in list.");
                }
            }
            else
            {
                if (CommunityDetailPage.Instance.IsLotStatusExist(lotStatusName))
                {
                    ExtentReportsHelper.LogFail($"<font color='red'><b>{lotStatusName}</b></font> Status name is existed in list.");
                }
            }
        }

    }
}
