using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.BuildingPhaseDetail;
using Pipeline.Testing.Pages.Estimating.BuildingPhaseType;
using Pipeline.Testing.Pages.Purchasing.BuildingPhases;

namespace Pipeline.Testing.Script.Section_III
{
    class B06_B_PIPE_35658 : BaseTestScript
    {
        BuildingPhaseTypeData BuildingPhaseTypeData;
        BuildingPhaseData BuildingPhaseData;
        BuildingPhaseData BuildingPhaseData_Update;

        private readonly string BUILDINGPHASETYPE_DEFAULT = "Bid";

        bool Flag = true;
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        [SetUp]
        public void GetTestData()
        {
            BuildingPhaseData = new BuildingPhaseData()
            {
                Code = "BP01",
                Name = "QA_RT_BuildingPhase001_Auto",
                AbbName = "Regression QA_RT_BuildingPhase001_Auto",
                Description = "Regression QA_RT_BuildingPhase Auto Only",
                BuildingGroupCode = "R",
                BuildingGroupName = "QA Only Building Group",
                Type = "NONE",
                Parent = "NONE",
                PercentBilled = "100",
                Taxable = true
            };

            BuildingPhaseData_Update = new BuildingPhaseData()
            {
                Code = "BP02",
                Name = "QA_RT_BuildingPhase002_Auto",
                AbbName = "Regression QA_RT_BuildingPhase002_Auto",
                Description = "Regression QA_RT_BuildingPhase Auto Only",
                BuildingGroupCode = "R",
                BuildingGroupName = "QA Only Building Group",
                Type = "NONE",
                Parent = "NONE",
                PercentBilled = "100",
                Taxable = true
            };

            BuildingPhaseTypeData = new BuildingPhaseTypeData()
            {
                TypeName = "QA_RT_BuildingPhaseType_Automation"
            };

            // Prepare a new Building Group 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Building Group to import Product.</font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "R",
                Name = "QA Only Building Group"
            };
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, buildingGroupData.Name);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", buildingGroupData.Name) is false)
            {
                // Open a new tab and create a new Category
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            BuildingPhaseTypePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhaseTypes);

            BuildingPhaseTypePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, BUILDINGPHASETYPE_DEFAULT);
            if (BuildingPhaseTypePage.Instance.IsItemInGrid("Name", BUILDINGPHASETYPE_DEFAULT) is false)
            {
                BuildingPhaseTypePage.Instance.ClickAddToShowBuildingPhaseTypeModal();

                if (BuildingPhaseTypePage.Instance.AddTypeModal.IsModalDisplayed() is true)
                {
                    ExtentReportsHelper.LogPass("Add Building Phase Type modal is displayed.");
                }
                else
                {
                    ExtentReportsHelper.LogFail("Add Building Phase Type modal is not displayed.");
                }
           
                BuildingPhaseTypePage.Instance.AddTypeModal
                                          .EnterBuildingPhaseTypeName(BUILDINGPHASETYPE_DEFAULT);
                BuildingPhaseTypePage.Instance.AddTypeModal.Save();
            }

        }


        [Test]
        [Category("Section_III")]
        public void B06_B_Estimating_BuildingPhaseTypes_Create_Edit_function_doesnot_work_properly()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Products/BuildingPhases/Types.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Products/BuildingPhases/Types.aspx.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_PHASES_URL);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, BuildingPhaseData.Code);
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", BuildingPhaseData.Code) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Code", BuildingPhaseData.Code);
                BuildingPhasePage.Instance.WaitGridLoad();
            }

            // Step 2: Navigate to Estimating > Building Phases page then click on the :plus: icon. Verify if Add Building Phase modal is displayed
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 2: Navigate to Estimating > Building Phases page then click on the :plus: icon. Verify if Add Building Phase modal is displayed.</font>");
            BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
            if (BuildingPhasePage.Instance.AddBuildingPhaseModal.IsModalDisplayed() is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Add Building Phase modal is not displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Add Building Phase modal is displayed.</b></font>");
            }

            //Step 3: Add a new building phase by adding data on the required fields: Code, Building Group then click on Save button.Verify if able to add a new building phase with a correct success toast message: Building Phase xyz added successfully.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3: Add a new building phase by adding data on the required fields: Code, Building Group then click on Save button.Verify if able to add a new building phase with a correct success toast message: Building Phase xyz added successfully.</font>");

            // Verify the modal is displayed with default value ()
            if (!BuildingPhasePage.Instance.AddBuildingPhaseModal.IsDefaultValues)
            {
                ExtentReportsHelper.LogWarning("The modal of Add Building Phase is not displayed with default values.");
                Flag = false;
            }

            BuildingPhasePage.Instance.AddBuildingPhaseModal
                                      .EnterPhaseCode(BuildingPhaseData.Code)
                                      .EnterPhaseName(BuildingPhaseData.Name)
                                      .EnterAbbName(BuildingPhaseData.AbbName)
                                      .EnterDescription(BuildingPhaseData.Description);
            BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(BuildingPhaseData.BuildingGroup);
            BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectType(BuildingPhaseData.Type);
            BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectParent(BuildingPhaseData.Parent);

            // Select the 'Save' button on the modal;
            BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();

            // Verify successful save and appropriate success message.
            string _expectedMessage = "Building Phase " + BuildingPhaseData.Code + " " + BuildingPhaseData.Name + " added successfully!";
            string _actualMessage = BuildingPhasePage.Instance.AddBuildingPhaseModal.GetLastestToastMessage();
            if (_expectedMessage == _actualMessage)
            {
                ExtentReportsHelper.LogPass("The message is displayed as expected. Actual results: " + _actualMessage);
            }
            else
            {
                if (BuildingPhasePage.Instance.IsItemInGrid("Code", BuildingPhaseData.Code) is true)
                {
                    ExtentReportsHelper.LogPass(null, $"Building Phase " + BuildingPhaseData.Code + " " + BuildingPhaseData.Name + "is created successfully");
                }
                else
                {
                    ExtentReportsHelper.LogWarning($"<font color = 'red'>Failed to create Building Phase {BuildingPhaseData.Code} {BuildingPhaseData.Name}</font>");
                    Flag = false;
                }
            }
            //BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseToastMessage();

            try
            {
                BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
            }
            catch (System.Exception exc)
            {
                ExtentReportsHelper.LogWarning("Unable to close modal - Is it already closed?");
            }

            //Step 4: Filter the building phase table grid and search for the newly created building phase
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4: Filter the building phase table grid and search for the newly created building phase.</font>");
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, BuildingPhaseData.Code);
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", BuildingPhaseData.Code) is true)
            {
                ExtentReportsHelper.LogPass($"Building Phase Name {BuildingPhaseData.Name} is displayed in grid");
            }
            else
            {
                ExtentReportsHelper.LogFail($"Building Phase Name {BuildingPhaseData.Name} is not displayed in grid");
            }

            //Step 5: Navigate to Purchasing > Building Phases page and filter the newly created building phase. Verify if it is also shown in this page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 5: Navigate to Purchasing > Building Phases page and filter the newly created building phase. Verify if it is also shown in this page.</font>");
            BuildingPhasesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasesPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, BuildingPhaseData.Code);
            if (BuildingPhasesPage.Instance.IsItemInGrid("Code", BuildingPhaseData.Code) is true)
            {
                ExtentReportsHelper.LogPass($"Building Phase Name {BuildingPhaseData.Name} is displayed in grid");
            }
            else
            {
                ExtentReportsHelper.LogFail($"Building Phase Name {BuildingPhaseData.Name} is not displayed in grid");
            }

            //Step 6: Navigate back to Estimating > Building Phases > newly created building phase then click on the Edit pen.Update the building phase details then click on Save button. Verify if new data are saved successfully.Verify the same updated details in Purchasing > Building Phases > newly created building phase
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 6: Navigate back to Estimating > Building Phases > newly created building phase then click on the Edit pen.Update the building phase details then click on Save button. Verify if new data are saved successfully.Verify the same updated details in Purchasing > Building Phases > newly created building phase.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_PHASES_URL);
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, BuildingPhaseData.Code);
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", BuildingPhaseData.Code) is true)
            {
                BuildingPhasePage.Instance.ClickEditItemInGrid("Code", BuildingPhaseData.Code);
                UpdateBuildingPhase(BuildingPhaseData_Update);

            }

            BuildingPhasesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasesPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, BuildingPhaseData_Update.Code);
            if (BuildingPhasesPage.Instance.IsItemInGrid("Code", BuildingPhaseData_Update.Code) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Building Phase Name {BuildingPhaseData_Update.Name} is displayed in grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Building Phase Name {BuildingPhaseData_Update.Name} is not displayed in grid</font>");
            }

            //Step 7: Navigate to Estimating > Building Phases > Building Phase Types then click the: plus: icon.Verify if Add Type modal is displayed
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 7: Navigate to Estimating > Building Phases > Building Phase Types then click the: plus: icon.Verify if Add Type modal is displayed.</font>");
            BuildingPhaseTypePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhaseTypes);
            BuildingPhaseTypePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, BuildingPhaseTypeData.TypeName);
            if (BuildingPhaseTypePage.Instance.IsItemInGrid("Name", BuildingPhaseTypeData.TypeName) is true)
            {
                BuildingPhaseTypePage.Instance.DeleteItemInGrid("Name", BuildingPhaseTypeData.TypeName);
                BuildingPhaseTypePage.Instance.WaitGridLoad();
            }

            BuildingPhaseTypePage.Instance.ClickAddToShowBuildingPhaseTypeModal();

            if (BuildingPhaseTypePage.Instance.AddTypeModal.IsModalDisplayed() is true)
            {
                ExtentReportsHelper.LogPass("Add Building Phase Type modal is displayed.");
            }
            else
            {
                ExtentReportsHelper.LogFail("Add Building Phase Type modal is not displayed.");
            }

            //Step 8: Type in new building phase type then click on Save button. Verify if able to create new building phase type with correct toast notification message: Building Phase Type xyz created successfully!
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 8: Type in new building phase type then click on Save button. Verify if able to create new building phase type with correct toast notification message: Building Phase Type xyz created successfully!.</font>");
            BuildingPhaseTypePage.Instance.AddTypeModal
                                      .EnterBuildingPhaseTypeName(BuildingPhaseTypeData.TypeName);
            BuildingPhaseTypePage.Instance.AddTypeModal.Save();

            // Verify successful save and appropriate success message.
             _expectedMessage = $"Building Phase Type {BuildingPhaseTypeData.TypeName} created successfully!";
             _actualMessage = BuildingPhaseTypePage.Instance.AddTypeModal.GetLastestToastMessage();
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

            //Step 9: Search for that newly created building phase type then click on Edit pen. Change the name with an existing building phase type name then click on Save button. Verify if there is an error validation message and will not be able to save the changes: Building Phase Type xyz already exists!
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 9: Search for that newly created building phase type then click on Edit pen. Change the name with an existing building phase type name then click on Save button. Verify if there is an error validation message and will not be able to save the changes: Building Phase Type xyz already exists!.</font>");
            BuildingPhaseTypePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, BuildingPhaseTypeData.TypeName);
            if (BuildingPhaseTypePage.Instance.IsItemInGrid("Name", BuildingPhaseTypeData.TypeName) is true)
            {
                BuildingPhaseTypePage.Instance.ClickEditItemInGrid("Name", BuildingPhaseTypeData.TypeName);
                BuildingPhaseTypePage.Instance.Update_BuildingPhaseTypes(BUILDINGPHASETYPE_DEFAULT);

                // Verify Building Phase Type already exists!
                _expectedMessage = $"Building Phase Type {BUILDINGPHASETYPE_DEFAULT} already exists";
                _actualMessage = BuildingPhaseTypePage.Instance.AddTypeModal.GetLastestToastMessage();
                if (_expectedMessage == _actualMessage)
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b>The message is displayed as expected.</b></font> Actual results: " + _actualMessage);
                    BuildingPhaseTypePage.Instance.CloseToastMessage();
                }
                else 
                {
                    ExtentReportsHelper.LogFail("<font color='red'>The message is not displayed as expected.</font> Actual results: " + _actualMessage);
                    BuildingPhaseTypePage.Instance.CloseToastMessage();
                }
            }
        }


        [TearDown]
        public void DeleteData()
        {
            //Delete data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete data.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_PHASES_URL);
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, BuildingPhaseData_Update.Code);
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", BuildingPhaseData_Update.Code) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Code", BuildingPhaseData_Update.Code);
                BuildingPhasePage.Instance.WaitGridLoad();
            }
        }

        private void UpdateBuildingPhase(BuildingPhaseData data)
        {
            BuildingPhaseDetailPage.Instance.EnterPhaseName(data.Name).EnterPhaseCode(data.Code).EnterAbbName(data.AbbName).Save();
            string actualMsg = BuildingPhaseDetailPage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(actualMsg))
            {
                ExtentReportsHelper.LogInformation(actualMsg);
                BuildingPhaseDetailPage.Instance.CloseToastMessage();
            }
        }


    }
}
