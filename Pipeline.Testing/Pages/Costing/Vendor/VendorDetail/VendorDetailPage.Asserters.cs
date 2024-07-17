using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Costing.Vendor.VendorDetail
{
    public partial class VendorDetailPage
    {
        /*
         * Verify head title and id of new Job
         */
        public bool IsCreateSuccessfully(VendorData vendor)
        {
            bool isPassed = true;
            if (!IsHeaderBreadscrumbCorect(vendor.Name))
            {
                SpecificControls breadscrumb = new SpecificControls(FindElementHelper.FindElement(FindType.XPath, "//*[@id='aspnetForm']/div[3]/section[1]/ul/li[2]"));
                CommonHelper.HighLightElement(breadscrumb);
                isPassed = false;
            }
            if (vendor.Name != Name_txt.GetValue())
            {
                CommonHelper.HighLightElement(Name_txt);
                isPassed = false;
            }
            if (vendor.Code != Code_txt.GetValue())
            {
                CommonHelper.HighLightElement(Code_txt);
                isPassed = false;
            }
            if (vendor.Email != Email_txt.GetValue())
            {
                CommonHelper.HighLightElement(Email_txt);
                isPassed = false;
            }
            if (vendor.City != City_txt.GetValue())
            {
                CommonHelper.HighLightElement(City_txt);
                isPassed = false;
            }
            if (vendor.State != State_txt.GetValue())
            {
                CommonHelper.HighLightElement(State_txt);
                isPassed = false;
            }
            if (vendor.Zip != Zip_txt.GetValue())
            {
                CommonHelper.HighLightElement(Zip_txt);
                isPassed = false;
            }
            if (!isPassed)
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), "The Vendor is created with invalid data.");
            return isPassed;
        }

        public bool IsHeaderBreadscrumbCorect(string _name)
        {
            System.Threading.Thread.Sleep(1000);
            string expectedName = $"{_name}";
            return SubHeadTitle().Equals(expectedName) && !CurrentURL.EndsWith("vid=0");
        }
    }
}
