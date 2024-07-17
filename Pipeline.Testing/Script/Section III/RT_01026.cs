using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Estimating.Styles.DetailStyles;
using Pipeline.Testing.Pages.Estimating.Styles.DetailStyles.SubManufacturerPage;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B07_RT_01026 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private StyleData testData;
        [SetUp]
        public void GetTestData()
        {

            testData = new StyleData()
            {
                Name = "Regression QA Only Style_Auto",
                Manufacturer = "RT Manufacturer",
                Url = "https://strongtie.com",
                Description = "Regression QA Product Style Only - Do Not Use"
            };
        }

        [Test]
        [Category("Section_III")]
        public void B07_Estimating_AddAProductStyle()
        {
            // Step 1: Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Products/Styles/Default.aspx
            ExtentReportsHelper.LogInformation(" Step 1: Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Products/Styles/Default.aspx");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);

            // Step 2: click on "+" Add button
            ExtentReportsHelper.LogInformation(" Step 2: Add new Style by clicking /'+/' button.");
            StylePage.Instance.ClickAddStyleIcon();
            string expectedURl = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_STYLE_URL;
            if (StyleDetailPage.Instance.IsPageDisplayed(expectedURl) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Style detail page isn't displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Style detail page is displayed</b></font>");
            }
            // Step 3: Populate all values
            ExtentReportsHelper.LogInformation(" Step 3: Populate test data and add new Manufacture.");
            bool displayManufacture = StyleDetailPage.Instance.IsManufacturerInList(testData.Manufacturer);
            if (displayManufacture == true)
            {
                ExtentReportsHelper.LogPass($"The Manufacturer {testData.Manufacturer} displayed correctly in list.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"The Manufacturer {testData.Manufacturer} was not display in list.");
                // Add new manufacture
                AddNewManufacturer();
            }
            StyleDetailPage.Instance.CreateStyle(testData);


            // 4. Select the 'Save' button on the modal;
            StyleDetailPage.Instance.Save();


            // 5. Verify new Style
            if (StyleDetailPage.Instance.IsUpdateDataCorrectly(testData))
            {
                ExtentReportsHelper.LogPass("Create product style successful");
            }

            // 6. Back to list of style and verify new item in grid view
            ExtentReportsHelper.LogInformation($" Step 4: Back to Style default page. Filter and delete new Style: {testData.Name}.");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);

            // Insert name to filter and click filter by Contain value
            StylePage.Instance.EnterStyleNameToFilter("Name", testData.Name);
            System.Threading.Thread.Sleep(2000);
            if (StylePage.Instance.IsItemInGrid("Name", testData.Name) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>New product style {testData.Name} was not display on grid.</font>");
            }
        }

        [TearDown]
        public void DeleteStyle()
        {
            // Back to Style page and delete item
            ExtentReportsHelper.LogInformation($" Step 6: Back to Style page and delete style {testData.Name}.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_URL);
            StylePage.Instance.DeleteStyle(testData);
        }

        private void AddNewManufacturer()
        {
            // Step 3.1: Add new manufacture
            StyleDetailPage.Instance.ClickAddManufaturerButton();
            if (SubmanufacturerPage.Instance.IsSubManufacturerDisplayed() is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Menufacturer detail page isn't displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Menufacturer detail page is displayed</b></font>");
            }
            // Step 3.2: populate data to manufacturer page and click save button
            SubmanufacturerPage.Instance.EnterSubManufacturerName(testData.Manufacturer);
            SubmanufacturerPage.Instance.Save();

            // Step 3.3: Verify successful message and close sub page
            string successfulStyleMess = $"Manufacturer {testData.Manufacturer} created successfully!";
            string actualMsg = StyleDetailPage.Instance.GetLastestToastMessage();
            if (successfulStyleMess.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"The Manufacturer {testData.Manufacturer} created successfully!");
                StyleDetailPage.Instance.CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail($"The Manufacturer {testData.Manufacturer} fail to create or wrong successful message.");
                StyleDetailPage.Instance.CloseToastMessage();
            }

            // Step 3.4 Close manufacturer detail page
            // SubmanufacturerPage.Instance.CloseModal();

            // Step 3.4 Verify new manufacturer is selected in drop down list
            bool displayManufacture = StyleDetailPage.Instance.DisplayCorrectManufacturer(testData.Manufacturer);
            if (!displayManufacture)
                ExtentReportsHelper.LogFail($"New Manufacturer {testData.Manufacturer} was not display on grid.");
            else
                ExtentReportsHelper.LogPass($"New Manufacturer {testData.Manufacturer} displayed correctly on grid of Creating Style detail page.");
            SubmanufacturerPage.Instance.CloseModal();
        }

    }
}
