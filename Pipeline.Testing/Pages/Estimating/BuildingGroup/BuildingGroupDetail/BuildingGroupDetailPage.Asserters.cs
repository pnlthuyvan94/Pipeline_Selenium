
using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Estimating.BuildingGroup.BuildingGroupDetail
{
    public partial class BuildingGroupDetailPage
    {
        public bool IsTitleBuildingGroup(string title)
        {
            if (BuildingGroupTitle_lbl == null || BuildingGroupTitle_lbl.IsDisplayed() == false)
            {
                throw new Exception("Not found " + BuildingGroupTitle_lbl.GetText() + " element");
            }
            return (BuildingGroupTitle_lbl.GetText() == "Building Group Details" && SubHeadTitle() == title);
        }

        public bool IsUpdateBuildingGroupSuccessful(BuildingGroupData data)
        {
            System.Threading.Thread.Sleep(1000);
            return SubHeadTitle().Equals(data.Code + "-" + data.Name) && !CurrentURL.EndsWith("cid=0");
        }

        public bool IsUpdateDataCorrectly(BuildingGroupData data)
        {
            if (!SubHeadTitle().Equals(data.Code + "-" + data.Name))
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Sub Head title: {data.Code + "-" + data.Name}. Actual Sub Head title: {SubHeadTitle()}");
                return false;
            }
            if (data.Name != BuildingGroupName_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.Name}. Actual result: {BuildingGroupName_txt.GetValue()}");
                return false;
            }
            if (data.Code != BuildingGroupCode_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.Code}. Actual result: {BuildingGroupCode_txt.GetValue()}");
                return false;
            }

            if (data.Description != BuildingGropDescription_txt.GetText())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {data.Description}. Actual result: {BuildingGropDescription_txt.GetText()}");
                return false;
            }
            return true;
        }
    }
}
