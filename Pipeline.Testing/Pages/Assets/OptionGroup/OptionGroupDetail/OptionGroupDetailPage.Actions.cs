using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.OptionGroupDetail
{
    public partial class OptionGroupDetailPage
    {

        public void ClickAdd()
        {
            addGroupOption_btn.Click();
            System.Threading.Thread.Sleep(500);
        }

        public void ClickSave()
        {
            saveGroupOption_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgSelectionInfo']/div[1]");
        }

        public void ClickClose()
        {
            closeGroupOption_btn.Click();
            System.Threading.Thread.Sleep(500);
        }

        public void FilterItemInGridOption(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            OptionGroupDetail_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            System.Threading.Thread.Sleep(5000);
        }

        public bool IsItemInGridOption(string columnName, string value)
        {
            return OptionGroupDetail_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGridOption(string columnName, string value)
        {
            Button delete = new Button(FindType.XPath, $"//a[contains(text(),'{value}')]/parent::td/following-sibling::td/input[contains(@src,'delete')]");
            delete.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgSelectionInfo']/div[1]",4000);
        }

        public void SelectOptionByName(params string[] optionName)
        {
            CheckBox option;
            foreach (var item in optionName)
            {
                option = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rlbSOs']/div/ul/li//input[../span[contains(text(),'{item}')]]");
                if (option.IsNull())
                {
                    ExtentReportsHelper.LogFail($"The option with name <font color='green'><b>{item}</b></font> is not displayed on option list.");
                }
                else
                {
                    option.Check();
                }
            }
        }
        public void editOptionGroupDetail(string nameOption, string sortOrder)
        {
            nameGroupOption_txt.SetText(nameOption);
            sortOderOption_txt.SetText(sortOrder);
            //cutoffGroupOption_ddl.SelectItem(cutOffPhase);
            saveOptionGroup_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgSelectionInfo']/div[1]");
            PageLoad();
        }

        public bool IsCutOffPhasePanelDisplayed()
        {
            Label import_lbl = new Label(FindType.XPath, $"//*[@id=\"ctl00_CPH_Content_pnlCutOffPhases\"]/section/label");
            if (import_lbl.IsDisplayed() is true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
