using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.House.HouseDetail
{
    public partial class HouseDetailPage
    {
        public HouseData CreateUpdateAHouse(HouseData house)
        {
            EnterHouseName(house.HouseName);
            EnterHouseSaleName(house.SaleHouseName);
            house.Series=SelectSeries(house.Series);
            EnterPlanNumber(house.PlanNumber);
            EnterBasePrice(house.BasePrice);
            EnterSQFTBasement(house.SQFTBasement);
            EnterSQFTFloor1(house.SQFTFloor1);
            EnterSQFTFloor2(house.SQFTFloor2);
            EnterHeated(house.SQFTHeated);
            EnterSQFTTotal(house.SQFTTotal);
            house.Style= SelectStyle(house.Style);
            house.Stories= SelectStories(house.Stories);
            house.Bedrooms = SelectBedroom(house.Bedrooms);
            house.Bathrooms = SelectBathroom(house.Bathrooms);
            house.Garage = SelectGarage(house.Garage);
            EnterDescription(house.Description);
            Save();
            HouseData newhouse = new HouseData(house)
            {
                Series=house.Series,
                Style = house.Style,
                Stories= house.Stories,
                Bedrooms= house.Bedrooms,
                Garage= house.Garage
            };

            return newhouse;
        }

        public HouseDetailPage EnterHouseName(string name)
        {
            HouseName_txt.SetText(name);
            return this;
        }

        public HouseDetailPage EnterHouseSaleName(string saleName)
        {
            SaleHouseName_txt.SetText(saleName);
            return this;
        }

        public string SelectSeries(string series)
        {
            return Series_ddl.SelectItemByValueOrIndex(series, 1);
        }

        public HouseDetailPage EnterPlanNumber(string planNumber)
        {
            PlanNumber_txt.SetText(planNumber);
            return this;
        }

        public HouseDetailPage EnterBasePrice(string basePrice)
        {
            BasePrice_txt.SetText(basePrice);
            return this;
        }

        public HouseDetailPage EnterSQFTBasement(string basement)
        {
            SQFTBasement_txt.SetText(basement);
            return this;
        }

        public HouseDetailPage EnterSQFTFloor1(string floor1)
        {
            SQFTFloor1_txt.SetText(floor1);
            return this;
        }

        public HouseDetailPage EnterSQFTFloor2(string floor2)
        {
            SQFTFloor2_txt.SetText(floor2);
            return this;
        }

        public HouseDetailPage EnterHeated(string headted)
        {
            SQFTHeated_txt.SetText(headted);
            return this;
        }

        public HouseDetailPage EnterSQFTTotal(string total)
        {
            SQFTTotal_txt.SetText(total);
            return this;
        }

        public string SelectStyle(string style)
        {
            return Style_ddl.SelectItemByValueOrIndex(style, 1);

        }

        public string SelectStories(string stories)
        {
            return Stories_ddl.SelectItemByValueOrIndex(stories, 1);
        }

        public string SelectBedroom(string bedroom)
        {
            return BedRoom_ddl.SelectItemByValueOrIndex(bedroom, 1);

        }

        public string SelectBathroom(string bathroom)
        {
            return BathRoom_ddl.SelectItemByValueOrIndex(bathroom, 1);
        }

        public string SelectGarage(string garage)
        {
            return Garage_ddl.SelectItemByValueOrIndex(garage, 1);
        }

        public HouseDetailPage EnterDescription(string desription)
        {
            Description_txt.SetText(desription);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbLoadingAnimation']/div[1]");
            PageLoad();
        }

        public HouseDetailPage ClickAddToShowCommunityPhaseModal()
        {
            AddCommunityPhase_btn.Click(false);
            if (!AddCommunityPhase_Title.WaitForElementIsVisible(10))
            {
                ExtentReportsHelper.LogFail("<font color = 'red'>The 'Add Community Phase' modal is NOT display after 5s.</font>");
            }
            return this;
        }

        public HouseDetailPage SelectCommunityPhase(params string[] communityPhaseNames)
        {
            string listPhasesXpath = "//*[@id='ctl00_CPH_Content_rlbCommunityPhaseAvailable']/div/ul/li/label[./span[.='{0}']]/input";
            CheckBox temp = new CheckBox(FindType.XPath, string.Empty);
            foreach (var item in communityPhaseNames)
            {
                listPhasesXpath = string.Format(listPhasesXpath, item);
                temp = new CheckBox(FindType.XPath, listPhasesXpath);
                //temp.UpdateValueToFind(listPhasesXpath);
                if (temp.IsDisplayed() is false)
                    ExtentReportsHelper.LogFail($"Could not find the item with name <font color='green'><b>{item}</b></font> in list Community Phases.");
                else
                    temp.Check();
            }
            return this;
        }

        public HouseDetailPage EnterCommunityPhasePrice(string price)
        {
            CommunityPhasePrice_txt.SetText(price);
            return this;
        }

        public void InsertCommunityPhaseToThisHouse()
        {
            InsertCommunityPhaseToHouse_btn.Click(false);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_txtPrice']");
        }

        public void CloseAddCommunityPhaseModal()
        {
            CloseCommunityPhaseModal_btn.Click();
            if (!AddCommunityPhase_Title.WaitForElementIsInVisible(5))
                ExtentReportsHelper.LogFail("The Add Community Phase modal is not hide after 5s.");
        }

        public void DeleteCommunityPhaseInGrid(string name, string value)
        {
            CommunityPhase_Grid.ClickDeleteItemInGrid(name, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunityPhaseForHouse']/div[1]");
        }

        public void SelectItemNameCommunityPhaseInGrid()
        {
            GridCommunityPhase_CommunityNameItem.Click();
            PageLoad();
        }
        public bool IsItemNameCommunityPhaseInGrid()
        {
            if (GridCommunityPhase_CommunityNameItem == null)
            {
                return false;
            }
            return true;
        }
    }
}

