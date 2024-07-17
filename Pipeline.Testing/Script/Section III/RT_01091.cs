using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class D01_RT_01091 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        VendorData _vendor;

        [SetUp]
        public void GetData()
        {
            _vendor = new VendorData()
            {
                Name= "RT_Vendor-Automation",
                Code= "36995",
                Address1= "Automation address",
                City= "Indianapolis",
                Zip= "46280",
                State= "IN",
                Email= "auto@gmail.com"
            };
        }

        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void D01_Costing_AddAVendor()
        {

            // Step 1: navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Vendors/Vendors/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 1: navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Vendors/Vendors/Default.aspx </font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);

            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _vendor.Name);
            if(VendorPage.Instance.IsItemInGrid("Name", _vendor.Name) is true)
            {

                VendorPage.Instance.DeleteItemInGrid("Name", _vendor.Name);
            }

            // Step 2: click on "+" Add button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2: click on " + " Add button</font>");
            VendorPage.Instance.ClickAddToVendorIcon();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_VENDOR_URL;

            if (VendorDetailPage.Instance.IsPageDisplayed(expectedURL) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Vendor detail page isn't displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Vendor detail page is displayed</b></font>");
            }

            // 3. Select the 'Save' button on the modal;
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step  3. Select the 'Save' button on the modal</font>");
            VendorDetailPage.Instance.CreateOrUpdateAVendor(_vendor);

            // 4. Verify new Vendor in header
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4. Verify new Vendor in header</font>");
            if (VendorDetailPage.Instance.IsCreateSuccessfully(_vendor) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Create new Vendor unsuccessfully.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Create Vendor Product.</b></font>");
            }

            // Create a Duplicate Vendor
            CommonHelper.OpenURL(expectedURL);
            VendorDetailPage.Instance.CreateOrUpdateAVendor(_vendor);
            string actualMsg = VendorPage.Instance.GetLastestToastMessage();
            string expectMsg = "Cannot save duplicate vendor name.";
            if (!actualMsg.Equals(expectMsg))
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The delete message is not as expected.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Cannot save duplicate vendor name.</b></font>");
            }
            VendorPage.Instance.CloseToastMessage();

            // 5. Back to list of Vendor and verify new item in grid view
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 5. Back to list of Vendor and verify new item in grid view</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_VENDORS_URL);

            // Insert name to filter and click filter by Contain value
            VendorPage.Instance.EnterVendorNameToFilter("Name", _vendor.Name);
            if(VendorPage.Instance.IsItemInGrid("Name", _vendor.Name) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>New Vendor {_vendor.Name} was not display on grid.</font>");
            }

            // 6. Select item and click deletete icon
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 6. Select item and click deletete icon</font>");
            VendorPage.Instance.DeleteItemInGrid("Name", _vendor.Name);
            if (VendorPage.Instance.IsItemInGrid("Name", _vendor.Name))
                ExtentReportsHelper.LogFail($"<font color='red'>The vendor {_vendor.Name} could not be deleted!</font>");
            else
                ExtentReportsHelper.LogPass($"<font color ='green'><b>The vendor {_vendor.Name} deleted successfully!</b></font>");

        }
        #endregion

    }
}
