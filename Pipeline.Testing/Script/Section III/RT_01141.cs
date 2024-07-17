using NUnit.Framework;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Common.BaseClass;
using Pipeline.Testing.Based;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B14_RT_01141 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

          SpecSetData SpecSetData;
        [SetUp]
        public void GetTestData()
        {
            SpecSetData = new SpecSetData()
            {
                GroupName= "RT-QA_SpecSet",
                UseDefault= "TRUE"
            };
        }

        #region"Test Case"
        [Test]
        [Category("Section_III")]
        public void B14_Estimating_AddSpecSetGroup()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/ProductAssemblies/SpecSets/Default.aspx
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);

            SpecSetPage.Instance.ChangeSpecSetPageSize(100);
            // Verify the created item and delete if it's existing
            if (SpecSetPage.Instance.IsItemInGrid("Name", SpecSetData.GroupName) is true)
            {
                SpecSetPage.Instance.DeleteSpecSet(SpecSetData.GroupName);
            }

            // Step 2: click on "+" Add button
            SpecSetPage.Instance.ClickAddToShowCategoryModal();
            if(SpecSetPage.Instance.AddSpecSetModal.IsModalDisplayed() is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Create Spec Set modal is not displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Create Spec Set modal is displayed</b></font>");
            }

            // Step 3: Populate all values
            SpecSetPage.Instance.AddSpecSetModal.AddGroupName(SpecSetData.GroupName).SetUseDefault(SpecSetData.UseDefault);

            // Create Option Room - Click 'Save' Button
            SpecSetPage.Instance.AddSpecSetModal.Save();
            string _actualMessage = SpecSetPage.Instance.GetLastestToastMessage();
            string _expectedMessage = $"Spec Set Group added successfully!";
            if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail($"Could not create Spec Set Group with name {SpecSetData.GroupName}, Use default {SpecSetData.UseDefault}" +
                    $"<br>Expected message: {_expectedMessage}" +
                    $"<br>Actual message: {_actualMessage}</br>");
                SpecSetPage.Instance.CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogPass(null, $"Create Use with Spec Set Group with name {SpecSetData.GroupName}, Use default {SpecSetData.UseDefault} successfully.");
            }

            // Step 5. Close the Adding modal
           // SpecSetPage.Instance.AddSpecSetModal.CloseModal();

            // Step 6. Insert name to filter and click filter by Contain value
            // SpecSetPage.Instance.FindItemInGridWithTextContains("Name", TestData["Group Name"]);

            // bool isFound = SpecSetPage.Instance.IsItemInGrid("Name", TestData["Group Name"]);
            // Assert.That(isFound, string.Format($"New SpecSet Group \"{TestData["Group Name"]} \" was not display on grid."));

        }
        #endregion "Test case"

        [TearDown]
        public void DeleteData()
        {
            // 7. Select item and click deletete icon
            SpecSetPage.Instance.DeleteSpecSet(SpecSetData.GroupName);
        }
    }
}
