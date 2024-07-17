using NUnit.Framework;
using NUnit.Framework.Legacy;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingPhaseType;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B06_RT_01023 : BaseTestScript
    {
        bool Flag = true;
        BuildingPhaseTypeData BuildingPhaseTypeData;
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        [SetUp]
        public void GetTestData()
        {
            BuildingPhaseTypeData = new BuildingPhaseTypeData()
            {
                TypeName = "R-QA Auto Test Phase Type"
            };
        }

        [Test]
        [Category("Section_III")]
        public void B06_Estimating_AddABuildingPhaseType()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Products/BuildingPhases/Types.aspx
            BuildingPhaseTypePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhaseTypes);

            // Step 1.1: Filter the created data and delete it before creating a new one

            BuildingPhaseTypePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, BuildingPhaseTypeData.TypeName);
            if (BuildingPhaseTypePage.Instance.IsItemInGrid("Name", BuildingPhaseTypeData.TypeName) is true)
            {
                BuildingPhaseTypePage.Instance.DeleteItemInGrid("Name", BuildingPhaseTypeData.TypeName);
                BuildingPhaseTypePage.Instance.WaitGridLoad();
            }


            // Step 2: click on "+" Add button
            BuildingPhaseTypePage.Instance.ClickAddToShowBuildingPhaseTypeModal();

            Assert.That(BuildingPhaseTypePage.Instance.AddTypeModal.IsModalDisplayed(), "Add Building Phase Type modal is not displayed.");

            // Step 3: Populate all values


            BuildingPhaseTypePage.Instance.AddTypeModal
                                      .EnterBuildingPhaseTypeName(BuildingPhaseTypeData.TypeName);

            // Step 4. Select the 'Save' button on the modal;
            BuildingPhaseTypePage.Instance.AddTypeModal.Save();

            // Verify successful save and appropriate success message.
            string _expectedMessage = $"Building Phase Type {BuildingPhaseTypeData.TypeName} created successfully!";
            string _actualMessage = BuildingPhaseTypePage.Instance.AddTypeModal.GetLastestToastMessage();
            if (_expectedMessage == _actualMessage)
            {
                ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
                BuildingPhaseTypePage.Instance.CloseToastMessage();
            }
            // Verify the modal is displayed with default value ()
            if (BuildingPhaseTypePage.Instance.AddTypeModal.IsDefaultValues is false)
            {
                ExtentReportsHelper.LogWarning("The modal of Add Building Group is not displayed with default values.");
                Flag = false;
            }

            // Step 5. The blocked ability to add multiples of a single value (along with the appropriate toast message to indicate the failure)
            BuildingPhaseTypePage.Instance.ClickAddToShowBuildingPhaseTypeModal();
            System.Threading.Thread.Sleep(3000);
            BuildingPhaseTypePage.Instance.AddTypeModal
                                      .EnterBuildingPhaseTypeName(BuildingPhaseTypeData.TypeName).Save();

            _expectedMessage = $"Building Phase Type {BuildingPhaseTypeData.TypeName} already exists";
            _actualMessage = BuildingPhaseTypePage.Instance.AddTypeModal.GetLastestToastMessage();
            ClassicAssert.AreEqual(_expectedMessage, _actualMessage, "The error message is not displayed after create a Building Phase Type invalid.");

            BuildingPhaseTypePage.Instance.CloseToastMessage();

            // Close modal
            BuildingPhaseTypePage.Instance.AddTypeModal.CloseModal();

            // Verify the new Building Phase Type create successfully
            BuildingPhaseTypePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, BuildingPhaseTypeData.TypeName);
            System.Threading.Thread.Sleep(2000);
            if(BuildingPhaseTypePage.Instance.IsItemInGrid("Name", BuildingPhaseTypeData.TypeName) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>New Building Phase Typ {BuildingPhaseTypeData.TypeName} was not display on grid.</font>");
            }


            // 5. Select the trash can to delete the new Building Phase; 
            BuildingPhaseTypePage.Instance.DeleteItemInGrid("Name", BuildingPhaseTypeData.TypeName);
            BuildingPhaseTypePage.Instance.WaitGridLoad();
            if ($"Building Phase Type {BuildingPhaseTypeData.TypeName} deleted successfully!" == BuildingPhaseTypePage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass("Building Phase Type deleted successfully!");
                BuildingPhaseTypePage.Instance.CloseToastMessage();
            }
            else
            {
                if (BuildingPhaseTypePage.Instance.IsItemInGrid("Name", BuildingPhaseTypeData.TypeName))
                    ExtentReportsHelper.LogFail("Building Phase Type could not be deleted!");
                else
                    ExtentReportsHelper.LogPass("Building Phase Type deleted successfully!");

            }

            if (Flag == false)
            {
                ExtentReportsHelper.LogInformation("There are some error while running this test. Please review the details as above.");
            }


        }

    }
}
