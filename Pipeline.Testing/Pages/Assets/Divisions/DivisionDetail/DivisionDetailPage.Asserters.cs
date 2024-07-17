
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.Divisions.DivisionDetail
{
    public partial class DivisionDetailPage
    {
        /// <summary>
        /// Verify head title and id of new manufacturer 
        /// </summary>
        /// <param name="divisionName"></param>
        /// <returns></returns>
        public bool IsSaveDivisionSuccessful(string divisionName)
        {
            System.Threading.Thread.Sleep(2000);
            return SubHeadTitle().Equals(divisionName) && !CurrentURL.EndsWith("did=0");
        }

        /// <summary>
        /// Verify the Division save data correct or not
        /// </summary>
        /// <param name="divisionData"></param>
        /// <returns></returns>
        public bool IsSaveDivisionData(DivisionData divisionData)
        {
            bool result = true;
            if (divisionData.Name != DivisionName_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {divisionData.Name}.Actual result: {DivisionName_txt.GetValue()}");
                result =  false;
            }
            if (divisionData.Address != DivisionAddress_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {divisionData.Address}.Actual result: {DivisionAddress_txt.GetValue()}");
                result = false;
            }
            if (divisionData.City != City_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {divisionData.City}.Actual result: {City_txt.GetValue()}");
                result = false;
            }
            if (divisionData.State != State_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {divisionData.State}.Actual result: {State_txt.GetValue()}");
                result = false;
            }
            if (divisionData.Zip != Zip_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {divisionData.Zip}.Actual result: {Zip_txt.GetValue()}");
                result = false;
            }
            if (divisionData.Phone != Phone_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {divisionData.Phone}.Actual result: {Phone_txt.GetValue()}");
                result = false;
            }
            if (divisionData.Fax != Fax_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {divisionData.Fax}.Actual result: {Fax_txt.GetValue()}");
                result = false;
            }
            if (divisionData.MainEmail != MainEmail_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {divisionData.MainEmail}.Actual result: {MainEmail_txt.GetValue()}");
                result = false;
            }
            if (divisionData.ServicesEmail != ServiceEmail_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {divisionData.ServicesEmail}.Actual result: {ServiceEmail_txt.GetValue()}");
                result = false;
            }
            if (divisionData.Slug != Slug_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {divisionData.Slug}.Actual result: {Slug_txt.GetValue()}");
                result = false;
            }
            if (divisionData.Description != Description_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {divisionData.Description}.Actual result: {Description_txt.GetValue()}");
                result = false;
            }
            return result;
        }
    }
}
