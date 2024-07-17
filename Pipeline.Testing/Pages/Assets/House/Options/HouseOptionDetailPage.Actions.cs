using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.IO;
using System.Runtime.CompilerServices;

namespace Pipeline.Testing.Pages.Assets.House.Options
{
    public partial class HouseOptionDetailPage
    {
        public HouseOptionDetailPage ClickAddElevationToShowModal()
        {
            if (AddElevation_btn.WaitForElementIsVisible(10)==true)
                AddElevation_btn.Click();
            if (!AddElevationModalTitle_Lbl.WaitForElementIsVisible(5))
                ExtentReportsHelper.LogFail("The Add Elevation modal is NOT display after 5s.");
            return this;
        }

        public HouseOptionDetailPage ClickAddOptionToShowModal()
        {
            if (AddOption_Btn.WaitForElementIsVisible(10)==true)
                AddOption_Btn.Click();
            if (!AddOptionModalTitle_Lbl.WaitForElementIsVisible(5))
                ExtentReportsHelper.LogFail("The Add Option modal is NOT display after 5s.");
            return this;
        }

        public void InsertElevationToHouse(params string[] elevations)
        {
            string elevationXpath = "//*[@id='ctl00_CPH_Content_rlbElevations']/div/ul/li/label[./span[contains(.,'{0}')]]/input";
            CheckBox temp = new CheckBox(FindType.XPath, string.Empty);
            foreach (var item in elevations)
            {
                temp.UpdateValueToFind(string.Format(elevationXpath, item));
                temp.RefreshWrappedControl();
                if (!temp.IsNull())
                    temp.Check();
                else
                    ExtentReportsHelper.LogFail($"The elevation with name <font color='green'><b>{item}</b></font> is NOT exist.");
            }
            InsertElevationToHouse_Btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgElevations']/div[1]");
        }

        public void InsertOptionToHouse(params string[] options)
        {
            string elevationXpath = "//*[@id='ctl00_CPH_Content_rlbOptions']/div/ul/li/label[./span[contains(.,'{0}')]]/input";
            CheckBox temp = new CheckBox(FindType.XPath, string.Empty);
            foreach (var item in options)
            {
                temp.UpdateValueToFind(string.Format(elevationXpath, item));
                temp.RefreshWrappedControl();
                if (!temp.IsNull())
                    temp.Check();
                else
                    ExtentReportsHelper.LogFail($"The Option with name <font color='green'><b>{item}</b></font> is NOT exist.");
            }
            InsertOptionToHouse_Btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");
        }

        public void FilterItemInElevationGrid(string name, GridFilterOperator gridFilterOperator, string valueToFind)
        {
            Elevation_Grid.FilterByColumn(name, gridFilterOperator, valueToFind);
            Elevation_Grid.WaitGridLoad();
        }

        public void FilterItemInOptionnGrid(string name, GridFilterOperator gridFilterOperator, string valueToFind)
        {
            Option_Grid.FilterByColumn(name, gridFilterOperator, valueToFind);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");
        }

        public void ChangePageSizeInOptionnGrid(int pageSize)
        {
            Option_Grid.ChangePageSize(pageSize);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");
        }

        public void DeleteItemInElevation(string name, string valueToFind)
        {
            System.Threading.Thread.Sleep(3000);
            Elevation_Grid.ClickDeleteItemInGrid(name, valueToFind);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgElevations']/div[1]");
        }

        public void DeleteItemInOption(string name, string valueToFind)
        {
            Option_Grid.ClickDeleteItemInGrid(name, valueToFind);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");
        }

        public void CloseElevationModal()
        {
            CloseElevationModal_btn.Click();
            if (!AddElevationModalTitle_Lbl.WaitForElementIsInVisible(5))
                ExtentReportsHelper.LogFail("The Add Elevation modal is NOT hide after 5s.");
        }

        public void CloseOptionModal()
        {
            CloseOptionModal_btn.Click();
            if (!AddOptionModalTitle_Lbl.WaitForElementIsInVisible(5))
                ExtentReportsHelper.LogFail("The Add Option modal is NOT hide after 5s.");
        }

        public void ClickEditOnElevation(string columnName, string value)
        {
            Elevation_Grid.ClickEditItemInGridWithTextContains(columnName, value);
            Elevation_Grid.WaitGridLoad();
            // Currently, on server, Wait grid load works incorrectly, so we have to set a waiting time (On local, it works correctly)
            System.Threading.Thread.Sleep(10000);
        }

        public void ClickEditOnOption(string columnName, string value)
        {
            Option_Grid.ClickEditItemInGridWithTextContains(columnName, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");
            // Currently, on server, Wait grid load works incorrectly, so we have to set a waiting time (On local, it works correctly)
            System.Threading.Thread.Sleep(10000);
        }

        public void EditItemOnElevationGridAndVerify(string columnNameToFind, string valueToFind, string columnNameToUpdate, string valueToUpdate)
        {
            int columnKeyIndex = Elevation_Grid.GetColumnHeaderIndexByName(columnNameToFind) + 1;
            int columnUpdateIndex = Elevation_Grid.GetColumnHeaderIndexByName(columnNameToUpdate) + 1;

            string inputXpath = $"//*[@id='ctl00_CPH_Content_rgElevations_ctl00']/tbody/tr[./td[{columnKeyIndex}]/*[contains(.,'{valueToFind}')]]/td[{columnUpdateIndex}]/input";
            Textbox edit_txt = new Textbox(FindType.XPath, inputXpath);
            if (edit_txt.IsNull())
                ExtentReportsHelper.LogFail($"The field with column name <b>{columnNameToFind}</b> and value <b>{valueToFind}</b> is NOT have the edit textbox at column <b>{columnNameToUpdate}</b>.");
            else
            {
                edit_txt.SetText(valueToUpdate);
                Button insertBtn = new Button(FindType.XPath, "//input[@alt='Update' and @title='Update' and contains(@src,'accept.png')]");
                insertBtn.Click();
                Elevation_Grid.WaitGridLoad();
            }
            string expectedMsg = "Record was successfully updated.";
            if (expectedMsg.Equals(GetLastestToastMessage()))
            {
                ExtentReportsHelper.LogPass($"Record was successfully updated.");
                //CloseToastMessage();
            }
            string xpathAfterUpdate = $"//*[@id='ctl00_CPH_Content_rgElevations_ctl00']/tbody/tr[./td[{columnKeyIndex}]/*[contains(.,'{valueToFind}')]]/td[{columnUpdateIndex}]";
            Label temp = new Label(FindType.XPath, xpathAfterUpdate);
            if (temp.GetText().Equals(valueToUpdate))
                ExtentReportsHelper.LogPass($"The field with column name <b>{columnNameToFind}</b> and value <b>{valueToFind}</b> is updated at column <b>{columnNameToUpdate}</b> successfully.");
            else
                ExtentReportsHelper.LogFail($"The field with column name<b>{ columnNameToFind}</b> and value <b>{ valueToFind}</b> is NOT update at column <b>{ columnNameToUpdate}</b> successfully.");
        }

        public void EditItemOnOptionGridAndVerify(string columnNameToFind, string valueToFind, string columnNameToUpdate, string valueToUpdate)
        {
            int columnKeyIndex = Option_Grid.GetColumnHeaderIndexByName(columnNameToFind) + 1;
            int columnUpdateIndex = Option_Grid.GetColumnHeaderIndexByName(columnNameToUpdate) + 1;

            string inputXpath = $"//*[@id='ctl00_CPH_Content_rgOptions_ctl00']/tbody/tr[./td[{columnKeyIndex}]/*[contains(.,'{valueToFind}')]]/td[{columnUpdateIndex}]/input";
            Textbox edit_txt = new Textbox(FindType.XPath, inputXpath);
            if (edit_txt.IsNull())
                ExtentReportsHelper.LogFail($"The field with column name <b>{columnNameToFind}</b> and value <b>{valueToFind}</b> is NOT have the edit textbox at column <b>{columnNameToUpdate}</b>.");
            else
            {
                edit_txt.SetText(valueToUpdate);
                Button insertBtn = new Button(FindType.XPath, "//input[@alt='Update' and @title='Update' and contains(@src,'accept.png')]");
                insertBtn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");
            }

            string expectedMsg = "Record was successfully updated.";
            if (expectedMsg.Equals(GetLastestToastMessage()))
            {
                ExtentReportsHelper.LogPass($"Record was successfully updated.");
                //CloseToastMessage();
            }
            string xpathAfterUpdate = $"//*[@id='ctl00_CPH_Content_rgOptions_ctl00']/tbody/tr[./td[{columnKeyIndex}]/*[contains(.,'{valueToFind}')]]/td[{columnUpdateIndex}]";
            Label temp = new Label(FindType.XPath, xpathAfterUpdate);
            if (temp.GetText().Equals(valueToUpdate))
                ExtentReportsHelper.LogPass($"The field with column name <b>{columnNameToFind}</b> and value <b>{valueToFind}</b> is updated at column <b>{columnNameToUpdate}</b> successfully.");
            else
                ExtentReportsHelper.LogFail($"The field with column name<b>{ columnNameToFind}</b> and value <b>{ valueToFind}</b> is NOT update at column <b>{ columnNameToUpdate}</b> successfully.");
        }

        public HouseOptionDetailPage ClickImgButtonToShowUploadSection(string elevationName)
        {
            string uploadXpath = $"//*[@id='ctl00_CPH_Content_rgElevations_ctl00']/tbody/tr[./td[2]/*[contains(.,'{elevationName}')]]/td/input[@title='Upload Image']";
            Button imgUpload_btn = new Button(FindType.XPath, uploadXpath);
            imgUpload_btn.Click();
            Elevation_Grid.WaitGridLoad();
            Textbox upload_Txt = new Textbox(FindType.Id, "ctl00_CPH_Content_fuElevationfakeInput0");
            if (!upload_Txt.WaitForElementIsVisible(10))
                ExtentReportsHelper.LogFail("The upload section is NOT display after 10s.");
            return this;
        }

        public void UploadImgForElevationAndVerify(string path)
        {
            var fileNameWithoutExtention = Path.GetFileNameWithoutExtension(path);
            string imgNameXpath = $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_lblElevationImagePanel']/span[text()='{fileNameWithoutExtention}']";
            Label fileName_lbl = new Label(FindType.XPath, imgNameXpath);
            Textbox upload_Txt = new Textbox(FindType.Id, "ctl00_CPH_Content_fuElevationfakeInput0");
            if (!upload_Txt.WaitForElementIsVisible(10))
                ExtentReportsHelper.LogFail("The upload section is NOT display after 10s.");
            else
            {
                upload_Txt.UpdateValueToFind("ctl00_CPH_Content_fuElevationfile0");
                upload_Txt.RefreshWrappedControl();
                upload_Txt.AppendKeys(path);
                // Wait process bar exist then hide
                Label temp = new Label(FindType.XPath, "//span[@class='ruFileProgress ruProgressStarted']");
                temp.WaitUntilExist(1);
                temp.WaitForElementIsInVisible(60);

                // Wait the name of file upload success
                fileName_lbl.WaitForElementIsVisible(60);

                if (!ImageThumbnail.WaitForElementIsVisible(60))
                    ExtentReportsHelper.LogFail("The file is not upload successfully after 60s.");
                else
                {
                    if (fileNameWithoutExtention.Equals(fileName_lbl.GetText()))
                        ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(fileName_lbl), "The file is uploaded successfully.");
                    else
                        ExtentReportsHelper.LogFail("The file is NOT upload successfully.");
                }
            }
        }

        public bool IsItemInHouseOptionGrid(string columnName, string valueToFind)
        {
            return OptionHouse_Grid.IsItemOnCurrentPage(columnName, valueToFind);
        }

        public void DeleteItemInOptionGrid(string columnName, string valueToFind)
        {
            OptionHouse_Grid.ClickDeleteItemInGrid(columnName, valueToFind);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunities']/div[1]");
            System.Threading.Thread.Sleep(5000);
            // AssignmentCommunity_Grid.WaitGridLoad();
        }

        public void OpenElevationInNewTabAndVerify(string elevationName)
        {
            OpenItemInNewTabAndVerify(Elevation_Grid, elevationName);
        }

        public void OpenOptionInNewTabAndVerify(string optionName)
        {
            OpenItemInNewTabAndVerify(Option_Grid, optionName);
        }

        private void OpenItemInNewTabAndVerify(IGrid grid, string itemName)
        {
            var item = grid.GetItemWithTextContainsOnCurrentPage("Name", itemName);
            var href = item.FindElement(By.TagName("a")).GetAttribute("href");
            CommonHelper.OpenLinkInNewTab(href);
            CommonHelper.SwitchTab(1);
            ExtentReportsHelper.LogInformation($"Open item with name <b>{itemName}</b> with link on the new tab:<br>{href}");
            PageLoad();
            var optionItem = FindElementHelper.FindElement(FindType.Id, "ctl00_CPH_Content_txtName");
            if (!optionItem.GetAttribute("value").Equals(itemName))
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(optionItem), $"The Option page is not display with expected name <b>{itemName}</b>, actual name: <b>{optionItem.GetAttribute("value")}</b>");
            else
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(optionItem), $"The Option page is displayed with expected name <b>{itemName}</b>, actual name: <b>{optionItem.GetAttribute("value")}</b>");
            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
        }
    }
}
