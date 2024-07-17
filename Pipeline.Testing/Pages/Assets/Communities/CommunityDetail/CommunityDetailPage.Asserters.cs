
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.Communities.CommunityDetail
{
    public partial class CommunityDetailPage
    {
        /// <summary>
        /// Verify head title and id of new manufacturer 
        /// </summary>
        /// <param name="CommunityName"></param>
        /// <returns></returns>
        public bool IsSaveCommunitySuccessful(string CommunityName)
        {
            System.Threading.Thread.Sleep(1000);
            return SubHeadTitle().Contains(CommunityName);
        }

        /// <summary>
        /// Verify the Community save data correct or not
        /// </summary>
        /// <param name="CommunityData"></param>
        /// <returns></returns>
        public bool IsSaveCommunityData(CommunityData CommunityData)
        {
            if (CommunityData.Name != CommunityName_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Community result: {CommunityData.Name}.Actual result: {CommunityName_txt.GetValue()}");
                return false;
            }
            if (CommunityData.Division != Division_ddl.SelectedItemName)
            {  
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Division result: {CommunityData.Division}.Actual result: {Division_ddl.SelectedItemName}");
                return false;
            }

            // If inputting Code with an empty value, then automatically fill the code field by name value.
            string expectedCode = CommunityData.Code;
            if (string.IsNullOrEmpty(CommunityData.Code))
                expectedCode = CommunityData.Name;

            if (expectedCode != Code_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Community Code result: {expectedCode}.Actual result: {Code_txt.GetValue()}");
                return false;
            }
            if (CommunityData.City != City_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected City result: {CommunityData.City}.Actual result: {City_txt.GetValue()}");
                return false;
            }
            if (CommunityData.CityLink != CityLink_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected City Link result: {CommunityData.CityLink}.Actual result: {CityLink_txt.GetValue()}");
                return false;
            }
            if (CommunityData.Township != Township_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Township result: {CommunityData.Township}.Actual result: {Township_txt.GetValue()}");
                return false;
            }
            if (CommunityData.County != County_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected County result: {CommunityData.County}.Actual result: {County_txt.GetValue()}");
                return false;
            }
            if (CommunityData.State != State_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected State result: {CommunityData.State}.Actual result: {State_txt.GetValue()}");
                return false;
            }
            if (CommunityData.Zip != Zip_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Zip result: {CommunityData.Zip}.Actual result: {Zip_txt.GetValue()}");
                return false;
            }
            if (CommunityData.SchoolDistrict != SchoolDesstrict_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected School District result: {CommunityData.SchoolDistrict}.Actual result: {SchoolDesstrict_txt.GetValue()}");
                return false;
            }
            if (CommunityData.SchoolDistrictLink != SchoolDesstrictLink_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected School District Link result: {CommunityData.SchoolDistrictLink}.Actual result: {SchoolDesstrictLink_txt.GetValue()}");
                return false;
            }
            if (CommunityData.Slug != Slug_txt.GetValue() && CommunityData.Slug != null)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Slug result: {CommunityData.Slug}.Actual result: {Slug_txt.GetValue()}");
                return false;
            }
            if (CommunityData.Status != Status_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Status result: {CommunityData.Status}.Actual result: {Status_ddl.SelectedItemName}");
                return false;
            }
            if (CommunityData.Description != Description_txt.GetText())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Description result: {CommunityData.Description}.Actual result: {Description_txt.GetText()}");
                return false;
            }
            if (CommunityData.DrivingDirections != DrivingDescription_txt.GetText())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Driving Directions result: {CommunityData.DrivingDirections}.Actual result: {DrivingDescription_txt.GetText()}");
                return false;
            }
            return true;
        }

        public bool IsLotStatusExist(string lotStatusName)
        {
            return PlannerStatus_ddl.IsItemInList(lotStatusName);
        }
    }
}
