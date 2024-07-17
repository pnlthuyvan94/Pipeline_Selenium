using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Pathway.DesignerElement;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class F04_RT_01101 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        #region"Test case"
        [Test]
        [Category("Section_III")]
        [Ignore("Pathway menu was removed from Pipeline, so this test sript will be ignored.")]
        public void F04_Pathway_AddDesignerElement()
        {

            // 1. Navigate to this URL:  http://dev.bimpipeline.com/Dashboard/eHome/AssetGroups/Default.aspx
            DesignerElementPage.Instance.SelectMenu(MenuItems.PATHWAY).SelectItem(PathWayMenu.DesignerElements);

            // 2. Click on "+" Add button
            DesignerElementPage.Instance.ClickAddToShowModal(DesignerElementPage.DesignerElementOption.ElementType);
            Assert.That(DesignerElementPage.Instance.AddElementTypeModal.IsModalDisplayed, "Add Element Type modal is not displayed.");

            // 3. Populate all values
            Row TestData = ExcelFactory.GetRow(DesignerElementPage.Instance.TestData_RT01101, 1);
            DesignerElementPage.Instance.AddElementTypeModal.EnterName(TestData["Type Name"]);

            // 4. Select the 'Save' button on the modal and close
            DesignerElementPage.Instance.AddElementTypeModal.Save();

            string expectedMes = "Element Type successfully added.";
            if (expectedMes == DesignerElementPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(expectedMes);
                DesignerElementPage.Instance.CloseToastMessage();
            }

            // 5. Insert name to filter and click filter by Contain value
            DesignerElementPage.Instance.FilterItemInGrid(DesignerElementPage.DesignerElementOption.ElementType, "Element Type Name", GridFilterOperator.EqualTo, TestData["Type Name"]);
            bool isFound = DesignerElementPage.Instance.IsItemInGrid(DesignerElementPage.DesignerElementOption.ElementType, "Element Type Name", TestData["Type Name"]);
            Assert.That(isFound, string.Format("New Element Type \"{0} \" was not display on grid.", TestData["Type Name"]));

            // ****************************Working with Designer element ******************************
            // 6. Click on "+" Add button
            DesignerElementPage.Instance.ClickAddToShowModal(DesignerElementPage.DesignerElementOption.DesignerElement);
            Assert.That(DesignerElementPage.Instance.AddElementModal.IsModalDisplayed, "Add Element modal is not displayed.");

            // 7. Populate all values
            DesignerElementPage.Instance.AddElementModal.SelectElementType(TestData["Type Name"])
                .EnterElementName(TestData["Name"])
                .EnterElementDescription(TestData["Description"])
                .SelectElementStyle(TestData["Style"])
                .Save();

            // 8. Select the 'Save' button on the modal and close
            expectedMes = "Element successfully added.";
            if (expectedMes == DesignerElementPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(expectedMes);
                DesignerElementPage.Instance.CloseToastMessage();
            }

            // 9. Insert name to filter and click filter by Contain value
            DesignerElementPage.Instance.FilterItemInGrid(DesignerElementPage.DesignerElementOption.DesignerElement, "Element Name", GridFilterOperator.EqualTo, TestData["Name"]);
            isFound = DesignerElementPage.Instance.IsItemInGrid(DesignerElementPage.DesignerElementOption.DesignerElement, "Element Name", TestData["Name"]);
            Assert.That(isFound, string.Format("New Element \"{0} \" was not display on grid.", TestData["Name"]));

            //   ********************* DELETE ITEM ************************************
            // Element
            DesignerElementPage.Instance.DeleteItemInGrid(DesignerElementPage.DesignerElementOption.DesignerElement, "Element Name", TestData["Name"]);
            expectedMes = "Element successfully removed.";
            if (expectedMes == DesignerElementPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(expectedMes);
                DesignerElementPage.Instance.CloseToastMessage();
            }
            else
            {
                // Filter again to find out that deleted item is removed
                bool isFoundDeletedItem = DesignerElementPage.Instance.IsItemInGrid(DesignerElementPage.DesignerElementOption.ElementType, "Element Name", TestData["Name"]);
                Assert.That(!isFoundDeletedItem, string.Format($"The selected Element : \"{TestData["Name"]} \" was not deleted on grid."));
                ExtentReportsHelper.LogPass(expectedMes);
            }

            // TYPE
            DesignerElementPage.Instance.DeleteItemInGrid(DesignerElementPage.DesignerElementOption.ElementType, "Element Type Name", TestData["Type Name"]);
            expectedMes = "Element Type successfully removed.";
            if (expectedMes == DesignerElementPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(expectedMes);
                DesignerElementPage.Instance.CloseToastMessage();
            }
            else
            {
                // Filter again to find out that deleted item is removed
                bool isFoundDeletedItem = DesignerElementPage.Instance.IsItemInGrid(DesignerElementPage.DesignerElementOption.ElementType, "Element Type Name", TestData["Type Name"]);
                Assert.That(!isFoundDeletedItem, string.Format($"The selected Element Type: \"{TestData["Type Name"]} \" was not deleted on grid."));
                ExtentReportsHelper.LogPass(expectedMes);
            }
        }
        #endregion

    }
}
