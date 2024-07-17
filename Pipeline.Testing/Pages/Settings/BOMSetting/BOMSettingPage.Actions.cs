using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Settings.BOMSetting
{
    public partial class BOMSettingPage
    {
        /// <summary>
        /// Job BOM Setting
        /// </summary>
        /// <param name="value"></param>
        public void SelectJobBOMShowZeroQuantities(bool value)
        {
            string showZeroQuantities_Xpath = "//*[@id='ctl00_CPH_Content_ddlBOMShowZeroQuantities']";
            DropdownList showZeroQuantities_ddl = new DropdownList(FindType.XPath, showZeroQuantities_Xpath);

            // Save button is hidden because user menu opens
            CommonHelper.MoveToElementWithoutCapture(SaveJobBOMSetting);

            if (showZeroQuantities_ddl.IsExisted(false) is true && SaveJobBOMSetting.IsDisplayed(false) is true)
            {
                showZeroQuantities_ddl.SelectItem(value.ToString(), false, false);
                SaveJobBOMSetting.Click(false);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_secJob']/div[1]");

                string actualToastMess = GetLastestToastMessage();
                string expectedMess = "Job BOM Settings Updated";
                if (actualToastMess.Contains(expectedMess))
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Job BOM Setting updated successfully. Toast message's same as the expectation.</b></font>");
                else
                    ExtentReportsHelper.LogWarning(null, $"<font color='yellow'>Job BOM Setting updated unsuccessfully. Toast message's NOT same as the expectation.</font>" +
                        $"<br>The expectation: {expectedMess}" +
                        $"<br>The actual result: {actualToastMess}");
            }
            else
                ExtentReportsHelper.LogWarning($"Can't find 'Job BOM Show Zero Quantities' drop down list or 'Save Job BOM Settings' button to update.");
        }

        /// <summary>
        /// House BOM Setting
        /// </summary>
        /// <param name="value"></param>
        public void SelectHouseBOMShowZeroQuantities(bool value)
        {
            string showZeroQuantities_Xpath = "//*[@id='ctl00_CPH_Content_ddlHouseBOMShowZeroQuantities']";
            DropdownList showZeroQuantities_ddl = new DropdownList(FindType.XPath, showZeroQuantities_Xpath);

            // Save button is hidden because user menu opens
            CommonHelper.MoveToElementWithoutCapture(SaveHouseBOMSetting);

            if (showZeroQuantities_ddl.IsExisted(false) is true && SaveHouseBOMSetting.IsDisplayed(false) is true)
            {
                showZeroQuantities_ddl.SelectItem(value.ToString(), false, false);
                SaveHouseBOMSetting.Click(false);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_secHouse']/div[1]");

                string actualToastMess = GetLastestToastMessage();
                string expectedMess = "House BOM Settings Updated";
                if (actualToastMess.Contains(expectedMess))
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>House BOM Setting updated successfully. Toast message's same as the expectation.</b></font>");
                else
                    ExtentReportsHelper.LogWarning(null, $"<font color='yellow'>House BOM Setting updated unsuccessfully. Toast message's NOT same as the expectation.</font>" +
                        $"<br>The expectation: {expectedMess}" +
                        $"<br>The actual result: {actualToastMess}");
            }
            else
                ExtentReportsHelper.LogWarning($"Can't find 'House BOM Show Zero Quantities' drop down list or 'Save Job BOM Settings' button to update.");
        }

        /// <summary>
        /// House BOM Setting
        /// </summary>
        /// <param name="value"></param>
        public void SelectGroupByParameter(bool value, string paramter)
        {
            Button selectedItem;
            if(value is true)
                selectedItem =  new Button(FindType.XPath, "//*[@for='ctl00_CPH_Content_rbGroupbyParameter_0']");
            else
                selectedItem = new Button(FindType.XPath, "//*[@for='ctl00_CPH_Content_rbGroupbyParameter_1']");

            if (selectedItem.IsExisted(false) is true && SaveHouseBOMSetting.IsDisplayed(false) is true)
            {
                selectedItem.JavaScriptClick();
                if(value is true)
                {
                    Paramter_txt.SetText(paramter);
                }
                SaveHouseBOMSetting.Click();
                CommonHelper.WaitUntilElementInvisible("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_secHouse']/div[1]", 5, false);

                string actualToastMess = GetLastestToastMessage();
                string expectedMess = "House BOM Settings Updated";
                if (actualToastMess.Contains(expectedMess))
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>House BOM Setting updated successfully. Toast message's same as the expectation.</b></font>");
                else
                    ExtentReportsHelper.LogWarning(null, $"<font color='red'>House BOM Setting updated unsuccessfully. Toast message's NOT same as the expectation.</font>" +
                        $"<br>The expectation: {expectedMess}" +
                        $"<br>The actual result: {actualToastMess}");
            }
            else
                ExtentReportsHelper.LogWarning(null, $"Can't find 'Group by Parameter' or 'Save House BOM Settings' button to update.");
        }

        public void Check_House_BOM_Product_Orientation(bool checkbox)
        {
            EnableHouseBOMProductOrientation_chk.SetCheck(checkbox);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlEnableHouseBomProductOrientation']/div[1]");
            SaveGeneralSettings.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbLoadingAnimationGeneral']/div[1]");

            string Save_expected = "General Settings Updated";
            string Save_actual = GetLastestToastMessage();
            if (Save_expected.Equals(Save_actual))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Save General Settings is successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogWarning($"<font color='yellow'>Save General Settings is unsuccessfully. Actual message: <i>{Save_actual}</i></font>");
            }
        }
        /// <summary>
        /// House BOM Setting
        /// </summary>
        /// <param name="value"></param>
        public void SelectDefaultHouseBOMView(bool value)
        {
            Button selectedItem;
            if (value is true)
                selectedItem = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rbAdvancedHouseBOMView_0']");
            else
                selectedItem = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rbAdvancedHouseBOMView_1']");

            if (selectedItem.IsExisted(false) is true && SaveHouseBOMSetting.IsDisplayed(false) is true)
            {
                selectedItem.Click();
                SaveHouseBOMSetting.Click();
                CommonHelper.WaitUntilElementInvisible("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_secHouse']/div[1]", 5, false);

                string actualToastMess = GetLastestToastMessage();
                string expectedMess = "House BOM Settings Updated";
                if (actualToastMess.Contains(expectedMess))
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Job BOM Setting updated successfully. Toast message's same as the expectation.</b></font>");
                else
                    ExtentReportsHelper.LogWarning(null, $"<font color='green'>Job BOM Setting updated unsuccessfully. Toast message's NOT same as the expectation.</font>" +
                        $"<br>The expectation: {expectedMess}" +
                        $"<br>The actual result: {actualToastMess}");
            }
            else
                ExtentReportsHelper.LogWarning(null, $"Can't find 'Default House BOM View' or 'Save House BOM Settings' button to update.");
        }

        public void Check_Prompt_Building_Phase_Add_to_Products(bool checkbox)
        {
            EnablePromptBuildingPhase_chk.SetCheck(checkbox);
            SaveGeneralSettings.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbLoadingAnimationGeneral']/div[1]");

            string Save_expected = "General Settings Updated";
            string Save_actual = GetLastestToastMessage();
            if (Save_expected.Equals(Save_actual))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Save General Settings is successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Save General Settings is unsuccessfully. Actual message: <i>{Save_actual}</i></font>");
            }
        }
        public void RoundNegativeValuesTowardsZero(bool checkbox)
        {
            RoundingNegativeValueTowardsZero_chk.SetCheck(checkbox);
            SaveGeneralSettings.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbLoadingAnimationGeneral']/div[1]");
            string actualMessage = GetLastestToastMessage();
            string expectMessage = "General Settings Updated";
            if (actualMessage.Equals(expectMessage))
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>General Settings is saved successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>General Settings is saved unsuccessfully.</font>");
            }
        }

        public void SelectHouseBOMGroupByUse(bool value)
        {
            DropdownList selectedItem = new DropdownList(FindType.XPath, "//select[@id = 'ctl00_CPH_Content_ddlHouseGroupByUse']");
            Button itemMiddle = new Button(FindType.XPath, "//select[@id = 'ctl00_CPH_Content_ddlHouseGroupByUse']/option[@selected]");
            if (itemMiddle.GetText() == "False" && value == true)
            { 
                selectedItem.SelectItem("True");
                SaveHouseBOMSetting.Click();
                CommonHelper.WaitUntilElementInvisible("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_secHouse']/div[1]", 5, false);
                string actualToastMess = GetLastestToastMessage();
                string expectedMess = "House BOM Settings Updated";
                if (actualToastMess.Contains(expectedMess))
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Job BOM Setting updated successfully. Toast message's same as the expectation.</b></font>");
                else
                    ExtentReportsHelper.LogWarning(null, $"<font color='green'>Job BOM Setting updated unsuccessfully. Toast message's NOT same as the expectation.</font>" +
                        $"<br>The expectation: {expectedMess}" +
                        $"<br>The actual result: {actualToastMess}");
            }
            if(itemMiddle.GetText() == "False" && value == false)
            {
                return;
            }
            if(itemMiddle.GetText() == "True" && value == false)
            {
                selectedItem.SelectItem("False");
                SaveHouseBOMSetting.Click();
                CommonHelper.WaitUntilElementInvisible("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_secHouse']/div[1]", 5, false);
                string actualToastMess = GetLastestToastMessage();
                string expectedMess = "House BOM Settings Updated";
                if (actualToastMess.Contains(expectedMess))
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Job BOM Setting updated successfully. Toast message's same as the expectation.</b></font>");
                else
                    ExtentReportsHelper.LogWarning(null, $"<font color='green'>Job BOM Setting updated unsuccessfully. Toast message's NOT same as the expectation.</font>" +
                        $"<br>The expectation: {expectedMess}" +
                        $"<br>The actual result: {actualToastMess}");
            }
            if(itemMiddle.GetText() == "True" && value == true)
            {
                return;
            }

          
        }

        public void Check_Only_See_Assigned_Options_on_a_HouseBOM(bool checkbox)
        {
            AssignedOptionsOnHouseBOM_chk.SetCheck(checkbox);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_secHouse']/div[1]");
            SaveGeneralSettings.Click();
            CommonHelper.WaitUntilElementInvisible("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_secHouse']/div[1]", 5, false);

            string Save_expected = "General Settings Updated";
            string Save_actual = GetLastestToastMessage();
            if (Save_expected.Equals(Save_actual))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Save General Settings is successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Save General Settings is unsuccessfully. Actual message: <i>{Save_actual}</i></font>");
            }
        }
    }
}
