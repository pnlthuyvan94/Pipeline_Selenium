using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.House.HouseDetail
{
    public partial class HouseDetailPage
    {
        static HouseData houseData;
        public bool IsHouseNameDisplaySuccessfullyOnBreadScrumb(string houseName, string planNumber)
        {
            System.Threading.Thread.Sleep(1000);
            string expectedHouseName = $"{houseName} ({planNumber})";
            return SubHeadTitle().Equals(expectedHouseName);
        }

        public bool IsSavedWithCorrectValue(HouseData house)
        {
            bool isPassed = true;
            if (!string.IsNullOrEmpty(house.HouseName) && !house.HouseName.Equals(HouseName_txt.GetValue()))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(HouseName_txt), $"The house name is not save with correct value.<br>Actual:<font color='green'><b>{HouseName_txt.GetValue()}</b></font>.<br>Expected:<font color='green'><b>{house.HouseName}</b></font>.");
            }
            if (!string.IsNullOrEmpty(house.SaleHouseName) && !house.SaleHouseName.Equals(SaleHouseName_txt.GetValue()))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(SaleHouseName_txt), $"The Sale house name is not save with correct value.<br>Actual:<font color='green'><b>{SaleHouseName_txt.GetValue()}</b></font>.<br>Expected:<font color='green'><b>{house.SaleHouseName}</b></font>.");
            }
            if (!string.IsNullOrEmpty(house.Series) && !house.Series.Equals(Series_ddl.SelectedItemName))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(Series_ddl), $"The house Series is not save with correct value.<br>Actual:<font color='green'><b>{Series_ddl.SelectedItemName}</b></font>.<br>Expected:<font color='green'><b>{house.Series}</b></font>.");
            }
            if (!string.IsNullOrEmpty(house.PlanNumber) && !house.PlanNumber.Equals(PlanNumber_txt.GetValue()))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(PlanNumber_txt), $"The house Plan Number is not save with correct value.<br>Actual:<font color='green'><b>{PlanNumber_txt.GetValue()}</b></font>.<br>Expected:<font color='green'><b>{house.PlanNumber}</b></font>.");
            }
            float.TryParse(BasePrice_txt.GetValue(), out float actualPrice);
            float.TryParse(house.BasePrice, out float expectedPrice);

            if (!expectedPrice.Equals(actualPrice))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(HouseName_txt), $"The house Base Price is not save with correct value.<br>Actual:<font color='green'><b>{BasePrice_txt.GetValue()}</b></font>.<br>Expected:<font color='green'><b>{house.BasePrice}</b></font>.");
            }
            if (!string.IsNullOrEmpty(house.SQFTBasement) && !house.SQFTBasement.Equals(SQFTBasement_txt.GetValue()))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(SQFTBasement_txt), $"The house SQFT Basement is not save with correct value.<br>Actual:<font color='green'><b>{SQFTBasement_txt.GetValue()}</b></font>.<br>Expected:<font color='green'><b>{house.SQFTBasement}</b></font>.");
            }
            if (!string.IsNullOrEmpty(house.SQFTFloor1) && !house.SQFTFloor1.Equals(SQFTFloor1_txt.GetValue()))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(SQFTFloor1_txt), $"The house SQFT Floor 1 is not save with correct value.<br>Actual:<font color='green'><b>{SQFTFloor1_txt.GetValue()}</b></font>.<br>Expected:<font color='green'><b>{house.SQFTFloor1}</b></font>.");
            }
            if (!string.IsNullOrEmpty(house.SQFTFloor2) && !house.SQFTFloor2.Equals(SQFTFloor2_txt.GetValue()))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(SQFTFloor2_txt), $"The house SQFT Floor 2 is not save with correct value.<br>Actual:<font color='green'><b>{SQFTFloor2_txt.GetValue()}</b></font>.<br>Expected:<font color='green'><b>{house.SQFTFloor2}</b></font>.");
            }
            if (!string.IsNullOrEmpty(house.SQFTHeated) && !house.SQFTHeated.Equals(SQFTHeated_txt.GetValue()))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(SQFTHeated_txt), $"The house SQFT Heated is not save with correct value.<br>Actual:<font color='green'><b>{SQFTHeated_txt.GetValue()}</b></font>.<br>Expected:<font color='green'><b>{house.SQFTHeated}</b></font>.");
            }
            if (!string.IsNullOrEmpty(house.SQFTTotal) && !house.SQFTTotal.Equals(SQFTTotal_txt.GetValue()))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(SQFTTotal_txt), $"The house SQFT Total is not save with correct value.<br>Actual:<font color='green'><b>{SQFTTotal_txt.GetValue()}</b></font>.<br>Expected:<font color='green'><b>{house.SQFTTotal}</b></font>.");
            }
            if (!string.IsNullOrEmpty(house.Description) && !house.Description.Equals(Description_txt.GetValue()))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(Description_txt), $"The house Description is not save with correct value.<br>Actual:<font color='green'><b>{Description_txt.GetValue()}</b></font>.<br>Expected:<font color='green'><b>{house.Description}</b></font>.");
            }
            if (!string.IsNullOrEmpty(house.Style) && !house.Style.Equals(Style_ddl.SelectedItemName))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(Style_ddl), $"The house Style is not save with correct value.<br>Actual:<font color='green'><b>{Style_ddl.SelectedItemName}</b></font>.<br>Expected:<font color='green'><b>{house.Style}</b></font>.");
            }
            if (!string.IsNullOrEmpty(house.Stories) && !house.Stories.Equals(Stories_ddl.SelectedItemName))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(Stories_ddl), $"The house Stories is not save with correct value.<br>Actual:<font color='green'><b>{Stories_ddl.SelectedItemName}</b></font>.<br>Expected:<font color='green'><b>{house.Stories}</b></font>.");
            }
            if (!string.IsNullOrEmpty(house.Bedrooms) && !house.Bedrooms.Equals(BedRoom_ddl.SelectedItemName))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(BedRoom_ddl), $"The house Bedrooms is not save with correct value.<br>Actual:<font color='green'><b>{BedRoom_ddl.SelectedItemName}</b></font>.<br>Expected:<font color='green'><b>{house.Bedrooms}</b></font>.");
            }
            if (!string.IsNullOrEmpty(house.Bathrooms) && !house.Bathrooms.Equals(BathRoom_ddl.SelectedItemName))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(BathRoom_ddl), $"The house Bathrooms is not save with correct value.<br>Actual:<font color='green'><b>{BathRoom_ddl.SelectedItemName}</b></font>.<br>Expected:<font color='green'><b>{house.Bathrooms}</b></font>.");
            }
            if (!string.IsNullOrEmpty(house.Garage) && !house.Garage.Equals(Garage_ddl.SelectedItemName))
            {
                isPassed = false;
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(Garage_ddl), $"The house Garage is not save with correct value.<br>Actual:<font color='green'><b>{Garage_ddl.SelectedItemName}</b></font>.<br>Expected:<font color='green'><b>{house.Garage}</b></font>.");
            }
            if (isPassed)
                ExtentReportsHelper.LogPass("The house is created/updated with correct values.");
            return isPassed;
        }

        public bool IsCommunityPhaseInGrid(string name, string value)
        {
            return CommunityPhase_Grid.IsItemOnCurrentPage(name, value);
        }
        public bool IsSeriesInList(string series)
        {
            if (Series_ddl.IsItemInList(series))
            {
                ExtentReportsHelper.LogInformation($"Items {series} is displayed in list");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogInformation($"Items {series} is not displayed in list");
                return false;
            }
        }
                
    }
}
