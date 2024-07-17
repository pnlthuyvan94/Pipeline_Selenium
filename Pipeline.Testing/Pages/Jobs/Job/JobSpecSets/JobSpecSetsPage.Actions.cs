using Pipeline.Common.Controls;
using Pipeline.Common.Enums;


namespace Pipeline.Testing.Pages.Jobs.Job.JobSpecSets
{
    public partial class JobSpecSetsPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator GridFilterOperator, string value)
        {
            JobSpecSetsPage_Grid.FilterByColumn(columnName, GridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_rgSets']/div[1]");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return JobSpecSetsPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public JobSpecSetsPage ClickEditItemInGrid(string columnName, string value)
        {
            JobSpecSetsPage_Grid.ClickEditItemInGrid(columnName, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_rgSets']/div[1]");
            return this;
        }

        public void SelectItemInJobSetOverride(string value)
        {
            DropdownList Item_select = new DropdownList(FindType.XPath, $"//*[contains(text(),'{value}')]//ancestor::tr/td[3]/select");
            Item_select.SelectItem(value);
        }

        /// <summary>
        /// Update Job Override Item in Grid , return string ToastMessage()
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string UpdateJobOverrideItemInGrid(string value)
        {
            SelectItemInJobSetOverride(value);
            Button UpdateItem_btn = new Button(FindType.XPath, $"//*[contains(text(),'{value}')]//ancestor::tr//td[4]//input[@title='Update']");
            UpdateItem_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_rgSets']/div[1]");
            return GetLastestToastMessage();
        }

        public void ChangeJobSpecSetPageSize(int size)
        {
            JobSpecSetsPage_Grid.ChangePageSize(size);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_rgSets']/div[1]");
        }

        public void NavigateToPage(int pagenumber)
        {
            JobSpecSetsPage_Grid.NavigateToPage(pagenumber);
            JobSpecSetsPage_Grid.WaitGridLoad();
        }
    }
}
