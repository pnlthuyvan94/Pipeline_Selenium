using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.PointsOfInterest;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A02_K_RT_01208 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private IList<PoIData> TestData;

        [SetUp]
        public void SetupData()
        {
            // Go to the Community page
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Get the test data
            string oldTestData = "R-QA Only Community Auto";

            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, oldTestData);

            // Select item in grid and navigate to PoI page
            CommunityPage.Instance.ClickItemInGridWithTextContains("Name", oldTestData);

            TestData = new List<PoIData>
            {
                new PoIData
                {
                    Title = "RT_QA-POI",
                    Description = "Create new POI",
                    Lat = "40.4158631344391",
                    Long = "-86.8781445717773",
                    Published = true
                },

                new PoIData
                {
                    Title = "RT_Update-POI",
                    Description = "Update POI",
                    Lat = "40.414033366871",
                    Long = "-86.870162317749",
                    Published = false
                },

                new PoIData
                {
                    Title = "Add a Other POI",
                    Description = "Create another POI for filter",
                    Lat = "40.414033366871",
                    Long = "-86.870162317749",
                    Published = false
                }
            };
        }

        #region"Test Case"
        [Test]
        [Category("Section_IV")]
        [Ignore("The PointsOfInterest was removed from Community detail page, so this test sript will be ignored.")]
        public void A02_K_Assets_DetailPage_Comunities_PointsOfInterest()
        {
            // Go to the Point of interest
            CommunityPage.Instance.LeftMenuNavigation("Points Of Interest");

            // Click add and navigate to point of interest page
            CommunityPoIPage.Instance.GoToPoIDetailPage();

            // Verify point of interest page is displayed
            if (!CommunityPoIPage.Instance.IsPointOfInterestPageDisplayed())
            {
                ExtentReportsHelper.LogFail("Point of Interest detail page is NOT display as expected.");
                Assert.Fail();
            }

            // Update information and save
            CommunityPoIPage.Instance.CreateNewPointsOfInterest(TestData[0]).SavePOI();

            // verify the bread scrumb
            if (!CommunityPoIPage.Instance.IsBreadCrumbDisplayedWithName(TestData[0].Title))
            {
                Assert.Fail();
            }

            // back to point of interest main page
            CommunityPage.Instance.LeftMenuNavigation("Points Of Interest");

            // Create the other POI
            CommunityPoIPage.Instance.GoToPoIDetailPage();
            CommunityPoIPage.Instance.CreateNewPointsOfInterest(TestData[2]).SavePOI();

            // back to point of interest main page
            CommunityPage.Instance.LeftMenuNavigation("Points Of Interest");

            // Filter items in grid
            CommunityPoIPage.Instance.FilterOnGrid("Title", GridFilterOperator.Contains, TestData[0].Title);

            // verify item in list
            if (!CommunityPoIPage.Instance.IsItemOnGrid("Title", TestData[0].Title))
            {
                ExtentReportsHelper.LogPass($"The created Points of Interest page with title <font color='green'><b>{TestData[0].Title}</b></font> is NOT display on grid.");
                Assert.Fail();
            }

            // edit
            CommunityPoIPage.Instance.EditItemInGrid("Title", TestData[0].Title);

            // update value
            CommunityPoIPage.Instance.CreateNewPointsOfInterest(TestData[1]).SavePOI();

            // back to main page and verify
            CommunityPage.Instance.LeftMenuNavigation("Points Of Interest");

            // verify item in list
            if (!CommunityPoIPage.Instance.IsItemOnGrid("Title", TestData[1].Title))
            {
                ExtentReportsHelper.LogPass($"The created Points of Interest page with title <font color='green'><b>{TestData[1].Title}</b></font> is NOT display on grid.");
                Assert.Fail();
            }

            // delete all items
            CommunityPoIPage.Instance.DeleteItemOnGrid("Title", TestData[1].Title);

            string expectedDeleteSuccessfulMess = $"Point of Interest {TestData[1].Title} deleted successfully!";
            // Expected: delete successful
            if (expectedDeleteSuccessfulMess == CommunityPoIPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass($"Point of Interest {TestData[1].Title} deleted successfully!");
                CommunityPoIPage.Instance.CloseToastMessage();
            }
            else
            {
                if (!CommunityPoIPage.Instance.IsItemOnGrid("Title", TestData[1].Title))
                    ExtentReportsHelper.LogPass($"Point of Interest {TestData[1].Title} deleted successfully!");
                else
                    ExtentReportsHelper.LogFail($"Point of Interest {TestData[1].Title} is NOT delete successfully!");
            }

            CommunityPoIPage.Instance.DeleteItemOnGrid("Title", TestData[2].Title);

            // Expected: delete successful
            if (expectedDeleteSuccessfulMess == CommunityPoIPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass($"Point of Interest {TestData[2].Title} deleted successfully!");
                CommunityPoIPage.Instance.CloseToastMessage();
            }
            else
            {
                if (!CommunityPoIPage.Instance.IsItemOnGrid("Title", TestData[2].Title))
                    ExtentReportsHelper.LogPass($"Point of Interest {TestData[2].Title} deleted successfully!");
                else
                    ExtentReportsHelper.LogFail($"Point of Interest {TestData[2].Title} is NOT delete successfully!");
            }
        }
        #endregion

    }
}
