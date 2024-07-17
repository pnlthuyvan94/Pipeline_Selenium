using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Resources;

namespace Pipeline.Testing.Pages.Assets.OptionSelection.SelectionDetail
{
    public partial class SelectionDetailPage
    {
        public void UpdateSelection(string selectionName, string selectionGroup, bool customizable)
        {
            SelectionName_Txt.SetText(selectionName);
            SelectionGroup_ddl.SelectItem(selectionGroup);
            if (Customizable_chk.IsChecked)
            {
                if (!customizable)
                    CommonHelper.CoordinateClick(FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlContent']/section[3]/span"));
            }
            else
            {
                if (customizable)
                    CommonHelper.CoordinateClick(FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlContent']/section[3]/span"));
            }
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_lbLoadingAnimation']/div[1]");
        }

        public SelectionDetailPage SwitchResourseType(ResourceTypes type)
        {
            string currentItem = ResourcesType_ddl.SelectedItemName;
            string xpathLoading = string.Empty;
            if (!currentItem.Equals(type.ToString("g")))
            {
                switch (currentItem)
                {
                    case "Link":
                        xpathLoading = "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_ruResourceFile']/div[1] | //*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_ruResourceFile']/div[1]";
                        break;
                    default:
                        xpathLoading = "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_ruResourceFile']/div[1]";
                        break;
                }
                ResourcesType_ddl.SelectItem(type.ToString("g"), true, false);
                System.Threading.Thread.Sleep(700);
                WaitingLoadingGifByXpath(xpathLoading);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ResourcesType_ddl), $"The Resources Type selected value <b>{type.ToString("g")}</b>");
            }
            return this;
        }

        public void UploadImageOrDocument(string path, int timeout = 120)
        {
            Textbox upload_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_ruResourceFile']//input[@type='file']");
            if (upload_txt.WaitUntilExist(10))
            {
                upload_txt.AppendKeys(path);
                Label progressing = new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_ruResourceFile']//span[@class='ruUploadProgress']");
                progressing.WaitUntilExist(1);
                progressing.WaitForElementIsInVisible(timeout);
                Label uploadSucess = new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_ruResourceFile']//span[@class='ruUploadProgress ruUploadSuccess']");
                uploadSucess.WaitUntilExist(1);
                uploadSucess.WaitForElementIsInVisible(timeout);
            }
        }

        public void UploadLink(string title, string source)
        {
            Textbox title_txt = new Textbox(FindType.Id, "ctl00_CPH_Content_txtTitle");
            Textbox source_txt = new Textbox(FindType.Id, "ctl00_CPH_Content_txtSource");
            Button insert_btn = new Button(FindType.Id, "ctl00_CPH_Content_lbInsertResource");
            title_txt.WaitForElementIsVisible(5, false);
            title_txt.SetText(title);
            source_txt.SetText(source);
            insert_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgSelectionResources']/div[1]");
        }

        public void FilterResourceOnGrid(string columnName, GridFilterOperator gridFilterOperator, string valueToFind)
        {
            Resources_Grid.FilterByColumn(columnName, gridFilterOperator, valueToFind);
        }

        public void DeleteItemOnResourceGrid(string columnName, string valueToFind)
        {
            Resources_Grid.ClickDeleteItemInGrid(columnName, valueToFind);
            ConfirmDialog(ConfirmType.OK);
            Resources_Grid.WaitGridLoad();
        }

        public SelectionDetailPage ClickAddToShowAddOptionModal()
        {
            AddOption_Btn.Click();
            if (!AddOptionToSelectionTitleModal_lbl.WaitForElementIsVisible(10))
                ExtentReportsHelper.LogFail("The 'Add Option to Selection' modal is NOT display after 10s.");
            return this;
        }

        public SelectionDetailPage SelectOptionOnOptionList(params string[] optionNames)
        {
            string xpathChk = "//*[@id='ctl00_CPH_Content_rlbSOs']/div/ul/li/label[./span[contains(text(),'{0}')]]/input";
            CheckBox option_chk = new CheckBox(FindType.XPath, string.Empty);
            foreach (var item in optionNames)
            {
                option_chk.UpdateValueToFind(string.Format(xpathChk, item));
                option_chk.RefreshWrappedControl();
                option_chk.Check();
            }
            return this;
        }

        public void InsertOptionToSelection()
        {
            InsertOptionToSelection_Btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rlbSOs']/div[1]");
        }

        public void CloseAddOptionToSelectionModal()
        {
            CloseAddOptionToSelectionModal_btn.Click();
            if (!AddOptionToSelectionTitleModal_lbl.WaitForElementIsInVisible(10))
                ExtentReportsHelper.LogFail("The 'Add Option to Selection' modal is NOT hide after 10s.");
        }

        public void ClickEditOnResoucesGrid(string columnName, string valueToFind)
        {
            Resources_Grid.ClickEditItemInGrid(columnName, valueToFind);
        }

        public void UpdateItemInResoucesGridAndVerify(string columnName, string oldValue, string newValue)
        {
            ClickEditOnResoucesGrid(columnName, oldValue);
            int columnIndex = Resources_Grid.GetColumnHeaderIndexByName(columnName) + 1;
            string xpathInput = "//*[@id='ctl00_CPH_Content_rgSelectionResources_ctl00']/tbody/tr/td[{0}]/input[@value='{1}']";
            Textbox input_txt = new Textbox(FindType.XPath, string.Format(xpathInput, columnIndex, oldValue));
            if (input_txt.IsExisted())
                input_txt.SetText(newValue);
            else
                ExtentReportsHelper.LogFail($"Could not find element with name <b>{oldValue}</b> on column <b>{columnName}</b>");

            // Insert
            Button update_btn = new Button(FindType.XPath, "//input[@title='Update' and @alt='Update']");
            if (update_btn.IsExisted())
                update_btn.Click();

            Resources_Grid.WaitGridLoad();
            string expectedMsg = "Selection Resource successfully updated.";
            string actualMsg = GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass("Selection resource successfully updated.");
                CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail($"The selection is NOT update successfully.<br>Actual result:{actualMsg}<br>Expected result:{expectedMsg}");
                CloseToastMessage();
            }
        }

        public void DeleteItemInOptionGrid(string columnName, string valueToFind)
        {
            Option_Grid.ClickDeleteItemInGrid(columnName, valueToFind);
            ConfirmDialog(ConfirmType.OK);
            Option_Grid.WaitGridLoad();
        }

    }
}
