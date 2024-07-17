using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Series;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class A03_RT_01077 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        bool isFoundCreatedSeries = false;

        SeriesData SeriesData;
        [SetUp]
        public void SetUp()
        {
            SeriesData = new SeriesData()
            {
                Name = "QA_RT_Series_1077_Automation",
                Code = "1077",
                Description = "Please do not delete this series, use for the automation purpose"
            };

            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Series/Default.aspx
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            SeriesPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, SeriesData.Code);
            isFoundCreatedSeries = SeriesPage.Instance.IsItemInGrid("Code", SeriesData.Code);

            if (isFoundCreatedSeries)
            {
                // Delete existing series before creating a new one
                SeriesPage.Instance.DeleteSeries(SeriesData.Code, SeriesData.Name);
            }
        }

        [Test]
        [Category("Section_III")]
        public void A03_Assets_AddASeries()
        {
            //SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            SeriesPage.Instance.NavigateURL("Builder/Series/Default.aspx");
            // Step 2: click on "+" Add button
            SeriesPage.Instance.ClickAddToSeriesModal();
            Assert.That(SeriesPage.Instance.AddSeriesModal.IsModalDisplayed(), "Add Series modal is not displayed.");

            // Step 3: Populate all values
            SeriesPage.Instance.AddSeriesModal
                                      .EnterSeriesName(SeriesData.Name)
                                      .EnterSeriesCode(SeriesData.Code)
                                      .EnterSeriesDescription(SeriesData.Description);


            // Step 4. Select the 'Save' button on the modal;
            string _actualMessage = SeriesPage.Instance.AddSeriesModal.Save();

            // Verify successful save and appropriate success message.
            string _expectedMessage = "Series " + SeriesData.Name + " created successfully!";
            if (_expectedMessage.Equals(_actualMessage))
            {
                ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
                SeriesPage.Instance.CloseToastMessage();
            }
            else 
                ExtentReportsHelper.LogWarning($"The message does not as expected. \nActual results: {_actualMessage}\nExpected results: {_expectedMessage} ");

            // Verify the modal is displayed with default value ()
            if (!SeriesPage.Instance.AddSeriesModal.IsDefaultValues)
                ExtentReportsHelper.LogWarning("The modal of Add Series is not displayed with default values.");

            // Close modal if it's open
            SeriesPage.Instance.AddSeriesModal.CloseModal();
        }

        [TearDown]
        public void DeleteSeries()
        {
            // Verify the new Series create successfully
            SeriesPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, SeriesData.Code);
            isFoundCreatedSeries = SeriesPage.Instance.IsItemInGrid("Code", SeriesData.Code);
            Assert.That(isFoundCreatedSeries, string.Format("New Series \"{0} - {1}\" was not display on grid.", SeriesData.Code, SeriesData.Name));

            if (isFoundCreatedSeries)
            {
                SeriesPage.Instance.DeleteSeries(SeriesData.Code, SeriesData.Name);
            }
        }
    }
}
