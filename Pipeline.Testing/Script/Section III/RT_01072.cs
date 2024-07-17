using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Divisions;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionDetail;
using RestSharp.Contrib;
using System;

namespace Pipeline.Testing.Script.Section_III
{
    public class A01_RT_01072 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        DivisionData _division;

        [SetUp]
        public void CreateTestData()
        {
            // Populate all values
            _division = new DivisionData()
            {
                Name = "R-QA Only Division Auto",
                Address = "3990 IN 38",
                City = "Lafayette",
                State = "IN",
                Zip = "47905",
                Description = "Create a new Division",
            };
        }

        [Test]
        [Category("Section_III")]
        public void A01_Assets_AddADivision()
        {
            // Step 1: navigate to division menu
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Navigate to division menu</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);

            // Step 2: Check if division exist
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0: Check if division exist</b></font>");
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.Contains, _division.Name);
            if (DivisionPage.Instance.IsItemInGrid("Division", _division.Name) is false)
            {
                // Step 3: click on "+" Add button
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.0: Click on + Add button</b></font>");
                DivisionPage.Instance.GetItemOnHeader(DashboardContentItems.Add).Click();
                var _expectedCreateDivisionURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_DIVISION_URL;

                if (DivisionDetailPage.Instance.IsPageDisplayed(_expectedCreateDivisionURL) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Division create page isn't displayed.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b>Division create page is displayed</b></font>");
                }

                //Step 4: Create division - Click 'Save' Button
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.0: Create division - Click 'Save' Button</b></font>");
                DivisionDetailPage.Instance.AddDivision(_division);
                string _expectedMessage = $"Could not create division with name {_division.Name}.";
                string actualMsg = DivisionDetailPage.Instance.GetLastestToastMessage();
                if (!_expectedMessage.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogPass($"Created division with name {_division.Name} successfully.");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"Could not create division with name {_division.Name}.");
                    DivisionDetailPage.Instance.CloseToastMessage();
                    Assert.Fail($"Could not create division with name {_division.Name}.");
                }

                // Step 5. Verify new House in header
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.0: Verify new House in header</b></font>");
                if (DivisionDetailPage.Instance.IsSaveDivisionSuccessful(_division.Name) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Create new Division unsuccessfully.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Division is created sucessfully with URL: {DivisionDetailPage.Instance.CurrentURL}");
                }


                // Step 6. Verify data saved successfully
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.0: Verify data saved successfully</b></font>");
                if (DivisionDetailPage.Instance.IsSaveDivisionData(_division) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>New Division created with incorrect data.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b>Division is create sucessfully with valid data");
                }
            }
            else 
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.0: Division exist</b></font>");
                DivisionPage.Instance.SelectItemInGrid("Division", _division.Name);
            }


            //Step 8 Verify if there is no Vendors link at the left navigation under “Costing” or above Community Taxes link
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.0: Verify if there is no Vendors link at the left navigation under “Costing” or above Community Taxes link.</b></font>");
            CommonHelper.CaptureScreen();

            //Step 9 When directly pasting the URL to the browser, the page redirects to Pipeline’s dashboard:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.0: When directly pasting the URL to the browser, the page redirects to Pipeline’s dashboard:.</b></font>");
            string currentURL = BaseFullUrl;
            Uri theUri = new Uri(currentURL);
            string did = HttpUtility.ParseQueryString(theUri.Query).Get("did");
            string view_division_vendor_URL = "/Costing/Builder/Divisions/VendorsToDivision.aspx?did=" + did;
            CommonHelper.OpenURL(BaseDashboardUrl + view_division_vendor_URL);
            CommonHelper.CaptureScreen();
        }

        [TearDown]
        public void DeleteData()
        {
            // Step 6. Back to list of Division and verify new item in grid view
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.DeleteDivision(_division);

        }
    }
}
