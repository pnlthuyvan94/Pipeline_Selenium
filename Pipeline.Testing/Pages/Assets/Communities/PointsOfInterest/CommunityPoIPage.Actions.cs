using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Communities.PointsOfInterest
{
    public partial class CommunityPoIPage
    {
        public void SavePOI()
        {
            Save_btn.Click();
            // Wait loading gif load
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbLoadingAnimation']/div[1]");
            // Page reload
            PageLoad();
        }

        public void GoToPoIDetailPage()
        {
            Add_Btn.Click();
            PageLoad();
        }

        public CommunityPoIPage CreateNewPointsOfInterest(PoIData data)
        {
            EnterTitle(data.Title);
            EnterDescription(data.Description);
            EnterLat(data.Lat);
            EnterLong(data.Long);
            CheckPublished(data.Published);
            return this;
        }

        public CommunityPoIPage EnterTitle(string value)
        {
            Title_Txt.SetText(value);
            return this;
        }

        public CommunityPoIPage EnterDescription(string value)
        {
            Description_Txt.SetText(value);
            return this;
        }

        public CommunityPoIPage EnterLat(string value)
        {
            Lat_Txt.SetText(value);
            return this;
        }

        public CommunityPoIPage EnterLong(string value)
        {
            Long_Txt.SetText(value);
            return this;
        }

        public CommunityPoIPage CheckPublished(bool value)
        {
            Published_chk.SetCheck(value);
            return this;
        }

        public void FilterOnGrid(string columnName, GridFilterOperator gridFilterOperator, string valueToFind)
        {
            POI_Grid.FilterByColumn(columnName, gridFilterOperator, valueToFind);
            POI_Grid.WaitGridLoad();
        }

        public void DeleteItemOnGrid(string columnName, string valueToFind)
        {
            POI_Grid.ClickDeleteItemInGrid(columnName, valueToFind);
            ConfirmDialog(ConfirmType.OK);
            POI_Grid.WaitGridLoad();
        }

        public void EditItemInGrid(string columnName, string valueToFind)
        {
            POI_Grid.ClickEditItemInGrid(columnName, valueToFind);
            PageLoad();
        }
    }
}
