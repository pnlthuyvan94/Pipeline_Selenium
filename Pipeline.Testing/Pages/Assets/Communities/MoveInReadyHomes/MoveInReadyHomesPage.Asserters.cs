
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.Communities.MoveInReadyHomes
{
    public partial class MoveInReadyHomesPage
    {
        public bool IsSaveMoveInReadyHomeSuccessful(MoveInReadyHomesData data)
        {
            if (data.House != HouseAfterSaving_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.House}.Actual result: {HouseBeforeSaving_ddl.GetValue()}");
                return false;
            }
            if (data.Lot != Lot_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.Lot}.Actual result: {Lot_ddl.GetValue()}");
                return false;
            }
            if (data.Status != Status_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.Status}.Actual result: {Status_ddl.GetValue()}");
                return false;
            }
            if (!Price_txt.GetValue().StartsWith(data.Price))
            {
                // 1000.00 start with 1000
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.Price}.Actual result: {Price_txt.GetValue()}");
                return false;
            }
            if (data.Address != Address_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.Address}.Actual result: {Address_txt.GetValue()}");
                return false;
            }
            if (data.Basement != Basement_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.Basement}.Actual result: {Basement_txt.GetValue()}");
                return false;
            }
            if (data.FirstFloor != FistFloor_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.FirstFloor}.Actual result: {FistFloor_txt.GetValue()}");
                return false;
            }
            if (data.SecondFloor != SecondFloor_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.SecondFloor}.Actual result: {SecondFloor_txt.GetValue()}");
                return false;
            }
            if (data.Heated != Heated_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.Heated}.Actual result: {Heated_txt.GetValue()}");
                return false;
            }
            if (data.Total != Total_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.Total}.Actual result: {Total_txt.GetValue()}");
                return false;
            }
            if (data.Style != Style_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.Style}.Actual result: {Style_ddl.GetValue()}");
                return false;
            }
            if (data.Story != Story_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.Story}.Actual result: {Story_ddl.GetValue()}");
                return false;
            }
            if (data.Bedroom != Bedroom_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.Bedroom}.Actual result: {Bedroom_ddl.GetValue()}");
                return false;
            }
            if (data.Bathroom != Bathroom_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.Bathroom}.Actual result: {Bathroom_ddl.GetValue()}");
                return false;
            }
            if (data.Garage != Garage_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.Garage}.Actual result: {Garage_ddl.GetValue()}");
                return false;
            }
            if (data.Note != Note_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.Note}.Actual result: {Note_txt.GetValue()}");
                return false;
            }
            if ((IsModalHome_btn.IsClickable() && data.IsModalHome.ToLower() == "false") || (!IsModalHome_btn.IsClickable() && data.IsModalHome.ToLower() == "true"))
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.IsModalHome}.Actual result: {IsModalHome_btn.GetValue()}");
                return false;
            }

            return true;
        }

        public bool IsAddMoveInReadyHomeGridDisplay()
        {
            // True: Save Move In Ready Home button is display and Add new Ready homw button is hidden
            return !SaveMoveInReadyHomes_btn.IsNull() && AddMoveInReadyHomes_btn.IsNull();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return MoveInReadyHomePage_Grid.IsItemOnCurrentPage(columnName, value);
        }
    }
}
