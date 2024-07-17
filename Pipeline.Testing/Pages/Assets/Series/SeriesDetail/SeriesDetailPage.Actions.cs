using OpenQA.Selenium;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;

namespace Pipeline.Testing.Pages.Assets.Series.SeriesDetail
{
    public partial class SeriesDetailPage
    {
        // Input value to the Series
        public SeriesDetailPage InputValueOnSeriesDetailPage(SeriesData data)
        {
            Title_Txt.SetText(data.Name);
            if (string.IsNullOrEmpty(Title_Txt.GetValue()))
                Title_Txt.SetText(data.Name);
            Code_Txt.SetText(data.Code);
            Description_Txt.SetText(data.Description);
            return this;
        }

        // Update Series
        public void UpdateSeries(SeriesData data)
        {
            InputValueOnSeriesDetailPage(data);
            Save_Btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlContent']/div[1]");
        }

        // Open Add House modal
        public bool ShowAddHouseModal()
        {
            AddHouse_Btn.Click();
            // Wait until the modal is displayed
            return AddHouseModalTitle_Lbl.WaitForElementIsVisible(5);
        }

        // Select a house or multi house in list
        public SeriesDetailPage SelectHouses(params string[] houseCodeNames)
        {
            House_Lst.Select(GridFilterOperator.StartsWith, houseCodeNames);
            return this;
        }

        // Add house to this Series
        public void AddHouseToSeries()
        {
            AddHouseInModal_Btn.Click();
            // Wait the House added to Series
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rlbHouses']/div[1]");
        }

        // Close Modal
        public void CloseModal()
        {
            CloseModal_Btn.Click();
            // Wait until the modal hide
            AddHouseModalTitle_Lbl.WaitForElementIsInVisible(3);
        }

        // Filter item in houseGrid
        public void FilterItemInHouseTable(string columnName, string valueToFind, GridFilterOperator filterBy)
        {
            House_Grid.FilterByColumn(columnName, filterBy, valueToFind);
        }

        // Remove the house 
        public void RemoveHouse(string house, string newSeries)
        {
            // Get the link of house
            var houseItem = House_Grid.GetItemOnCurrentPage("Name", house);
            string link = houseItem.FindElement(By.TagName("a")).GetAttribute("href");

            // Open the house in new tab
            CommonHelper.OpenLinkInNewTab(link);
            // Switch driver to new tab
            CommonHelper.SwitchLastestTab();
            // Wait page load completed.
            PageLoad();
            // House page select the new series
            HouseDetailPage.Instance.SelectSeries(newSeries);
            HouseDetailPage.Instance.Save();
            // Close this tab and switch to previous page
            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
            // Reload if need
        }
    }
}
