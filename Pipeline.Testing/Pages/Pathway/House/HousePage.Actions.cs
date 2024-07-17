using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Pathway.House
{
    public partial class HousePage
    {
        public bool IsItemInGrid(string columnName, string value)
        {
            return HousePage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public HousePage EditHouseProperty(string columnName, string houseName)
        {
            HousePage_Grid.ClickEditItemInGrid(columnName, houseName);
            WaitGridLoad();
            return this;
        }

        public void WaitGridLoad()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgHouses']/div[1]", 5);
            //HousePage_Grid.WaitGridLoad();
        }

        public HousePage EnterHouseNameToFilter(string columnName, string houseName)
        {
            HousePage_Grid.FilterByColumn(columnName, GridFilterOperator.Contains, houseName);
            WaitGridLoad();
            return this;
        }

        public HousePage UpdateHouseValue(string houseName, PathwayHouseProperty houseProperty)
        {
            CheckBox FloorPlan_Chk = new CheckBox(FindType.XPath, string.Format(_floorPlan, houseName));
            CheckBox ExteriorDesigner_Chk = new CheckBox(FindType.XPath, string.Format(_exterior, houseName));
            CheckBox InteriorDesigner_Chk = new CheckBox(FindType.XPath, string.Format(_interior, houseName));
            CheckBox MediaCenter_Chk = new CheckBox(FindType.XPath, string.Format(_media, houseName));

            FloorPlan_Chk.SetCheck(houseProperty.FloorPlan);
            ExteriorDesigner_Chk.SetCheck(houseProperty.Exterior);
            InteriorDesigner_Chk.SetCheck(houseProperty.Interior);
            MediaCenter_Chk.SetCheck(houseProperty.Media);

            return this;
        }

        public void UpdateValue()
        {
            Update_btn.Click();
            WaitGridLoad();
        }
    }
}
