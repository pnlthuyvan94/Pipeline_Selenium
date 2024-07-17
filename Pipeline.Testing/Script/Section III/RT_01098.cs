using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Pathway.Assets;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class F02_RT_01098 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private Row TestData;
        private AssetsData data;

        [SetUp]
        public void SetUp()
        {
            TestData = ExcelFactory.GetRow(AssetsPage.Instance.TestData_RT_01098, 1);
            data = new AssetsData()
            {
                Name = TestData["Name"],
                AssetType = TestData["Asset Type"],
                AssetGroup = TestData["Asset Group"],
                Length = TestData["Length"],
                Width = TestData["Width"],
            };
        }

        #region"Test case"
        [Test]
        [Category("Section_III")]
        [Ignore("Pathway menu was removed from Pipeline, so this test sript will be ignored.")]
        public void F02_Pathway_AddAnAsset()
        {

            // 1. Navigate to this URL:  http://dev.bimpipeline.com/Dashboard/eHome/Assets/Default.aspx
            AssetsPage.Instance.SelectMenu(MenuItems.PATHWAY).SelectItem(PathWayMenu.Assets);

            string createAssetPageUrl = BaseDashboardUrl + BaseMenuUrls.CREATE_PATHWAY_ASSETS_URL;


            // Delete item before creating a new one
            AssetsPage.Instance.FilterItemInGrid("Name ", GridFilterOperator.EqualTo, data.Name);
            if (AssetsPage.Instance.IsItemInGrid("Name ", data.Name) is true)
            {
                DeleteAsset(data);
            }

            // 2. Click on "+" Add button
            AssetsPage.Instance.ClickAddButton();
            Assert.That(AssetsPage.Instance.AssetsDetailPage.IsPageDisplayed(createAssetPageUrl), "Add Asset Group modal is not displayed.");

            // 3. Populate all values and save


            AssetsPage.Instance.CreateAnAsset(data);

            // Verified save successfully
            bool isCreated = AssetsPage.Instance.AssetsDetailPage.IsSaveAssetsSuccessful(data.Name);
            if (isCreated)
                ExtentReportsHelper.LogPass($"Create new Asset '{data.Name}' successfully.");
            else
            {
                ExtentReportsHelper.LogFail($"Could not create the Asset with name {data.Name}");
                Assert.Fail();
            }

            // Back to the Assets page
            AssetsPage.Instance.SelectMenu(MenuItems.PATHWAY).SelectItem(PathWayMenu.Assets);

            // 5. Insert name to filter and click filter by Contain value
            AssetsPage.Instance.FilterItemInGrid("Name ", GridFilterOperator.EqualTo, data.Name);
            bool isFound = AssetsPage.Instance.IsItemInGrid("Name ", data.Name);
            Assert.That(isFound, string.Format("New Asset \"{0} \" was not display on grid.", data.Name));

        }
        #endregion

        [TearDown]
        public void TearDownScript()
        {
            // 6. Select item and click deletete icon
            DeleteAsset(data);
        }

        private void DeleteAsset(AssetsData data)
        {
            AssetsPage.Instance.DeleteItemInGrid("Name ", data.Name);

            string expectedMess = "Asset deleted successfully!";
            if (expectedMess == AssetsPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass("Asset deleted successfully!");
                AssetsPage.Instance.CloseToastMessage();
            }
            else
            {
                if (AssetsPage.Instance.IsItemInGrid("Name ", data.Name))
                {
                    ExtentReportsHelper.LogFail("Asset could not be deleted!");
                    Assert.Fail();
                }
                else
                    ExtentReportsHelper.LogPass("Asset deleted successfully!");
            }
        }

    }
}
