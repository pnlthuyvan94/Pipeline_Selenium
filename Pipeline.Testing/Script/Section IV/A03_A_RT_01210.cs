using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Assets.Series.SeriesDetail;

namespace Pipeline.Testing.Script.Section_IV
{
    public class A03_A_RT_01210 : BaseTestScript
    {
        // Set up the Test Section Name for each Test case
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        SeriesData OldData;
        SeriesData NewData;
        // Add new Series (already did on previous testcase) => skip
        [SetUp] // Pre-condition
        public void GetOldTestData()
        {
            // Old data
            OldData = new SeriesData()
            {
                Name = "RegressionTest_Series",
                Code = "0001",
                Description = "RegressionTest_Series_Description"
            };

            // New data
            NewData = new SeriesData()
            {
                Name = "RT_Series-Update",
                Code = "10203",
                Description = "Update this series",
                HousePlanNumber = "0011",
                HouseName = "hai nguyen house"
            };

            // Delete series default page and find the series
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);

            // Filter and find the updated series
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, OldData.Name);

            if (SeriesPage.Instance.IsItemInGrid("Name", OldData.Name) is true)
            {
                // delete this series
                SeriesPage.Instance.DeleteItemInGrid("Name", OldData.Name);

                // verify the success toast messsage is displayed
                string expectedMsg = $"Series {NewData.Name} deleted successfully!";
                string actualMsg = SeriesPage.Instance.GetLastestToastMessage();
                if (expectedMsg.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogPass($"The Series '<font color='green'><b>{OldData.Name}</b></font>' is deleted successfully.");
                    SeriesPage.Instance.CloseToastMessage();
                }
                else if (!string.IsNullOrEmpty(actualMsg))
                    ExtentReportsHelper.LogFail($"The Series \"{ OldData.Code} - { OldData.Name}\" deleted unsuccessfully!. Actual message: <i>{actualMsg}</i>");
            }
            else
                ExtentReportsHelper.LogInformation($"The Series \"{ OldData.Code} - {OldData.Name}\" is NOT display on grid.");
        }

        [Test]
        [Category("Section_IV")]
        public void A03_A_Assets_DetailPage_Series_SeriesDetails()
        {
            // Go to the Series default page
            //SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL);

            // Filter the created series 
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, OldData.Name);

            // Verify the item is display in list
            if (SeriesPage.Instance.IsItemInGrid("Name", OldData.Name) is false)
            {
                // Create new series to test
                SeriesPage.Instance.ClickAddToSeriesModal();

                //Assert.That(SeriesPage.Instance.AddSeriesModal.IsModalDisplayed(), "Add Series modal is not displayed.");
                if (SeriesPage.Instance.AddSeriesModal.IsModalDisplayed())
                {
                    ExtentReportsHelper.LogPass("Add Series modal is displayed.");
                }
                else
                {
                    ExtentReportsHelper.LogFail("Add Series modal is not displayed.");
                }

                SeriesPage.Instance.AddSeriesModal
                                         .EnterSeriesName(OldData.Name)
                                         .EnterSeriesCode(OldData.Code)
                                         .EnterSeriesDescription(OldData.Description);


                // Step 4. Select the 'Save' button on the modal;
                SeriesPage.Instance.AddSeriesModal.Save();
                string _actualMessage = SeriesPage.Instance.GetLastestToastMessage();
                

                // Verify successful save and appropriate success message.
                string _expectedMessage = "Series " + OldData.Name + " created successfully!";
                if (_expectedMessage.Equals(_actualMessage))
                {
                    ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
                    SeriesPage.Instance.CloseToastMessage();
                }
                else 
                    ExtentReportsHelper.LogFail($"The message does not as expected. \nActual results: {_actualMessage}\nExpected results: {_expectedMessage} ");

                // Close modal
                //SeriesPage.Instance.AddSeriesModal.CloseModal();
            }

            // Go to the detail page
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, OldData.Name);
            SeriesPage.Instance.SelectItemAndOpenDetailPage("Name", OldData.Name);

            // Verify the details page is displayed successfully
            if (!SeriesDetailPage.Instance.VerifySeriesDetailPageDisplay(OldData.Name))
            {
                // The Detail page is not open
                ExtentReportsHelper.LogFail($"The Series Detail page with name <font color='green'><b>{OldData.Name}</b></font> is not display.");
                //Assert.Fail();
            }

            // Click add button on the House Section
            if (!SeriesDetailPage.Instance.ShowAddHouseModal())
            {
                // Verify the "Add House" modal is displayed
                ExtentReportsHelper.LogFail($"the 'Add House' modal is NOT display");
               // Assert.Fail();
            }

            // Select a House in list and click add button
            SeriesDetailPage.Instance
                .SelectHouses(NewData.HousePlanNumber + "-")
                .AddHouseToSeries();

            // Verify the success toast message
            string expectedMsg = "House(s) added to Series successfully";
            if (expectedMsg.Equals(SeriesDetailPage.Instance.GetLastestToastMessage()))
            {
                ExtentReportsHelper.LogPass($"The House <font color='green'><b>{NewData.HouseName}</b></font> is added to the Series {OldData.Name} successfully.");
                SeriesDetailPage.Instance.CloseToastMessage();
            }
            // Sometime, the toast message is displayed in the short period time => We will verify the item in list again.
            // Close modal
            //SeriesDetailPage.Instance.CloseModal();

            // Verify the house in list
            if (!SeriesDetailPage.Instance.VerifyHouseInList("Name", NewData.HouseName))
            {
                // The Detail page is not open
                ExtentReportsHelper.LogFail($"The House <font color='green'><b>{NewData.HouseName}</b></font> is NOT added to the Series {OldData.Name}.");
              //  Assert.Fail();
            }
            else
            {
                ExtentReportsHelper.LogPass($"The House <font color='green'><b>{NewData.HouseName}</b></font> is added to the Series {OldData.Name} successfully.");
            }

            ///////////*************** UPDATE ********************
            // Update title + code + description and hit save button
            SeriesDetailPage.Instance.UpdateSeries(NewData);

            // Verify the success msg
            expectedMsg = $"Series {NewData.Name} saved successfully!";
            if (expectedMsg.Equals(SeriesDetailPage.Instance.GetLastestToastMessage()))
            {
                ExtentReportsHelper.LogPass($"The Series '<font color='green'><b>{NewData.Name}</b></font>' is updated successfully.");
                SeriesDetailPage.Instance.CloseToastMessage();
            }

            // Go back to Series default page
            //SeriesDetailPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL);

            // Filter and find the updated series
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewData.Name);

            // Verify the updated series is displayed on grid
            if (!SeriesPage.Instance.IsItemInGrid("Name", NewData.Name))
            {
                // The item does not exist in list
                ExtentReportsHelper.LogFail($"The Series with name <font color='green'><b>{NewData.Name}</b></font> is not exist in list.");
                //Assert.Fail();
            }
            ExtentReportsHelper.LogPass($"The Series with name '<font color='green'><b>{OldData.Name}</b></font>' is updated to name '<font color='green'><b>{NewData.Name}</b></font>' successfully.");

            // Attempt to delete this series
            SeriesPage.Instance.DeleteItemInGrid("Name", NewData.Name);

            // Verify the error message (cannot be delete because this series contain house)
            expectedMsg = $"Not able to delete Series {NewData.Name}.";
            string actualMsg = SeriesPage.Instance.GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"The Series '<font color='green'><b>{NewData.Name}</b></font>' is deleted unsuccessfully as expected.");
                SeriesPage.Instance.CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(actualMsg))
                ExtentReportsHelper.LogFail($"The Series with name <font color='green'><b>{NewData.Name}</b></font> is deleted successfully althought contains a House. Actual result: <i>{actualMsg}</i>");
          
            // Go to the detail page
            SeriesPage.Instance.SelectItemAndOpenDetailPage("Name", NewData.Name);

            // Remove the House
            SeriesDetailPage.Instance.RemoveHouse(NewData.HouseName, "Visions");
        }

        [TearDown]
        public void DeleteData()
        {
            // Back to series default page and find the series
           // SeriesDetailPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL);

            // Filter and find the updated series
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewData.Name);

            if (SeriesPage.Instance.IsItemInGrid("Name", NewData.Name))
            {
                // delete this series
                SeriesPage.Instance.DeleteItemInGrid("Name", NewData.Name);

                // verify the success toast messsage is displayed
                string expectedMsg = $"Series {NewData.Name} deleted successfully!";
                string actualMsg = SeriesPage.Instance.GetLastestToastMessage();
                if (expectedMsg.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogPass($"The Series '<font color='green'><b>{NewData.Name}</b></font>' is deleted successfully.");
                    SeriesPage.Instance.CloseToastMessage();
                }
                else if (!string.IsNullOrEmpty(actualMsg))
                    ExtentReportsHelper.LogFail($"The Series \"{ NewData.Code} - { NewData.Name}\" deleted unsuccessfully!. Actual message: <i>{actualMsg}</i>");
            }
            else
                ExtentReportsHelper.LogInformation($"The Series \"{ NewData.Code} - {NewData.Name}\" is NOT display on grid.");
        }
    }
}
