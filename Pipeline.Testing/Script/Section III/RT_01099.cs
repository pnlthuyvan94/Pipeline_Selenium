using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Pathway.AssetGroup;
using Pipeline.Testing.Pages.Pathway.AssetGroup.AddAssetGroup;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class F03_RT_01099 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        #region"Test case"
        [Test]
        [Category("Section_III")]
        [Ignore("Pathway menu was removed from Pipeline, so this test sript will be ignored.")]
        public void F03_Pathway_AddAssetGroup()
        {

            // 1. Navigate to this URL:  http://dev.bimpipeline.com/Dashboard/eHome/AssetGroups/Default.aspx
            AssetGroupPage.Instance.SelectMenu(MenuItems.PATHWAY).SelectItem(PathWayMenu.AssetGroups);

            // 2. Click on "+" Add button
            AssetGroupPage.Instance.ClickAddToShowAssetGroupModal();
            Assert.That(AssetGroupPage.Instance.AddAssetGroupModal.IsModalDisplayed, "Add Asset Group modal is not displayed.");

            // 3. Populate all values
            Row TestData = ExcelFactory.GetRow(AssetGroupPage.Instance.AddAssetGroupModal.TestData_RT01099, 1);
            AssetGroupPage.Instance.AddAssetGroupModal.EnterName(TestData["Name"]);

            // 4. Select the 'Save' button on the modal and close
            AssetGroupPage.Instance.AddAssetGroupModal.Save();
            AssetGroupPage.Instance.GetLastestToastMessage();
            AssetGroupPage.Instance.AddAssetGroupModal.CloseModal();

            // 5. Insert name to filter and click filter by Contain value
            AssetGroupPage.Instance.FilterItemInGrid("Group Name", GridFilterOperator.EqualTo, TestData["Name"]);
            bool isFound = AssetGroupPage.Instance.IsItemInGrid("Group Name", TestData["Name"]);
            if (isFound)
            {
                ExtentReportsHelper.LogPass("Asset Group created successfully");
            }
            else
            {
                ExtentReportsHelper.LogFail("Asset Group isn't created");
            }
            Assert.That(isFound, string.Format("New Asset Group \"{0} \" was not display on grid.", TestData["Name"]));

            // 6. Select item and click deletete icon
            AssetGroupPage.Instance.DeleteItemInGrid("Group Name", TestData["Name"]);

            // 7. Filter again to find out that deleted item is removed
            bool isFoundDeletedItem = AssetGroupPage.Instance.IsItemInGrid("Group Name", TestData["Name"]);
            if (isFoundDeletedItem)
            {
                ExtentReportsHelper.LogFail("Asset Group could not be deleted!");
            }
            else
            {
                ExtentReportsHelper.LogPass("Asset Group deleted successfully!");
            }
            Assert.That(!isFoundDeletedItem, string.Format($"The selected Asset Group: \"{TestData["Name"]} \" was not deleted on grid."));
        }
        #endregion

    }
}
