using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using System;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B03_RT_01025 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        BuildingPhaseData BuildingPhaseData1;
        BuildingPhaseData BuildingPhaseData2;
        BuildingPhaseData BuildingPhaseData3;
        BuildingPhaseData BuildingPhaseData4;
        List<BuildingPhaseData> BuildingPhaseData;
        bool Flag = true;
        [SetUp]
        public void CreateTestData()
        {

            BuildingPhaseData1 = new BuildingPhaseData()
            {
                Code = "R0BP",
                Name = "R-QA Building Phase",
                AbbName = "Regression QA Only-Building Phase",
                Description = "Regression QA Building Phase Only",
                BuildingGroupCode = "R",
                BuildingGroupName = "QA Only Building Group",
                Type= "NONE",
                Parent = "NONE",
                PercentBilled = "100",
                Taxable = true
            };

            BuildingPhaseData2 = new BuildingPhaseData()
            {
                Code = "RPBP",
                Name = "R-QA Parent Building Phase",
                AbbName = "Regression Test -Parent Building Phase",
                Description = "Regression QA Only-Parent Building Phase",
                BuildingGroupCode = "R",
                BuildingGroupName = "QA Only Building Group",
                Type = "NONE",
                Parent = "NONE",
                PercentBilled = "50",
                Taxable = true
            };

            BuildingPhaseData3 = new BuildingPhaseData()
            {
                Code = "RCP1",
                Name = "R-QA Child Building Phase 1",
                AbbName = "Regression Test -Child Building Phase 1",
                Description = "Regression QA Only-Child Building Phase 1",
                BuildingGroupCode = "R",
                BuildingGroupName = "QA Only Building Group",
                Type = "NONE",
                Parent = "NONE",
                PercentBilled = "40",
                Taxable = true
            };

            BuildingPhaseData4 = new BuildingPhaseData()
            {
                Code = "RCP2",
                Name = "R-QA Child Building Phase 1",
                AbbName = "Regression Test -Child Building Phase 2",
                Description = "Regression QA Only-Child Building Phase 2",
                BuildingGroupCode = "R",
                BuildingGroupName = "QA Only Building Group",
                Type = "NONE",
                Parent = "RPBP-R-QA Parent Building Phase",
                PercentBilled= "10",
                Taxable = true
            };

            BuildingPhaseData = new List<BuildingPhaseData> { BuildingPhaseData1, BuildingPhaseData2 , BuildingPhaseData3, BuildingPhaseData4};
            
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

        }
        #region"Test case"
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [Category("Section_III")]
        public void B03_Estimating_AddABuildingPhase(int sequence)
        {

            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Products/BuildingPhases/Default.aspx
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_PHASES_URL);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, BuildingPhaseData[sequence].Code);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", BuildingPhaseData[sequence].Code) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Code", BuildingPhaseData[sequence].Code);
            }

            // Step 2: click on "+" Add button
            BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
            if (BuildingPhasePage.Instance.AddBuildingPhaseModal.IsModalDisplayed() is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Add Building Phase modal is not displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Add Building Phase modal is displayed.</b></font>");
            }

            // Verify the modal is displayed with default value ()
            if (!BuildingPhasePage.Instance.AddBuildingPhaseModal.IsDefaultValues)
            {
                ExtentReportsHelper.LogWarning("The modal of Add Building Phase is not displayed with default values.");
                Flag = false;
            }      

            BuildingPhasePage.Instance.AddBuildingPhaseModal
                                      .EnterPhaseCode(BuildingPhaseData[sequence].Code)
                                      .EnterPhaseName(BuildingPhaseData[sequence].Name)
                                      .EnterAbbName(BuildingPhaseData[sequence].AbbName)
                                      .EnterDescription(BuildingPhaseData[sequence].Description);
            BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(BuildingPhaseData[sequence].BuildingGroup);
            BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectType(BuildingPhaseData[sequence].Type);
            BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectParent(BuildingPhaseData[sequence].Parent);
                                      //.EnterPercentBilled(TestData["Percent Billed"])
                                      //.IsTaxable(IsTaxable);

            // 3. Select the 'Save' button on the modal;
            BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();

            // Verify successful save and appropriate success message.
            string _expectedMessage = "Building Phase " + BuildingPhaseData[sequence].Code + " " + BuildingPhaseData[sequence].Name + " added successfully!";
            string _actualMessage = BuildingPhasePage.Instance.AddBuildingPhaseModal.GetLastestToastMessage();
            if (_expectedMessage == _actualMessage)
            {
                ExtentReportsHelper.LogPass("The message is displayed as expected. Actual results: " + _actualMessage);
            }
            else
            {
                if (BuildingPhasePage.Instance.IsItemInGrid("Code", BuildingPhaseData[sequence].Code) is true)
                {
                    ExtentReportsHelper.LogPass(null, $"Building Phase " + BuildingPhaseData[sequence].Code + " " + BuildingPhaseData[sequence].Name + "is created successfully");
                }
                else
                {
                    ExtentReportsHelper.LogWarning($"<font color = 'red'>Failed to create Building Phase {BuildingPhaseData[sequence].Code} {BuildingPhaseData[sequence].Name}</font>");
                    Flag = false;
                }
            }
            //BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseToastMessage();

            try
            {
                BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
            } catch (Exception exc)
            {
                ExtentReportsHelper.LogWarning("Unable to close modal - Is it already closed?");
            }

            //4. Enter input values from step 3 again as listed in the Input table above;
            //verify unsuccessful save and appropriate failure message.
            System.Threading.Thread.Sleep(5000);
            if (sequence == 2)
            {
                if (!BuildingPhasePage.Instance.AddBuildingPhaseModal.IsModalDisplayed())
                {
                    BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                    ExtentReportsHelper.LogInformation("Clicked Add Building Phase button");
                }
                else
                {
                    ExtentReportsHelper.LogWarning("Building Phase modal already displayed");
                }

                System.Threading.Thread.Sleep(2500);

                BuildingPhasePage.Instance.AddBuildingPhaseModal
                                          .EnterPhaseCode(BuildingPhaseData[sequence].Code)
                                          .EnterPhaseName(BuildingPhaseData[sequence].Name)
                                          .EnterAbbName(BuildingPhaseData[sequence].AbbName)
                                          .EnterDescription(BuildingPhaseData[sequence].Description);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(BuildingPhaseData[sequence].BuildingGroup);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectType(BuildingPhaseData[sequence].Type);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectParent(BuildingPhaseData[sequence].Parent);
                                          //.EnterPercentBilled(TestData["Percent Billed"])
                                          //.IsTaxable(IsTaxable);

                // 4. Select the 'Save' button on the modal;
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();

                // Verify successful save and appropriate success message.
                _expectedMessage = "Not able to create Building Phase " + BuildingPhaseData[sequence].Code + " " + BuildingPhaseData[sequence].Name;
                _actualMessage = BuildingPhasePage.Instance.AddBuildingPhaseModal.GetLastestToastMessage();
                if (_actualMessage.Contains(_expectedMessage))
                {
                    ExtentReportsHelper.LogPass("The message is displayed as expected. Actual results: " + _actualMessage);

                    BuildingPhasePage.Instance.CloseToastMessage();
                    BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
                }
            }

            // Verify the new Building Phase create successfully
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, BuildingPhaseData[sequence].Code);
            if(BuildingPhasePage.Instance.IsItemInGrid("Code", BuildingPhaseData[sequence].Code) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>New Building Phase {BuildingPhaseData[sequence].Code}  {BuildingPhaseData[sequence].Name} was not display on grid.</font>");
            }
           

            // 6. Select the trash can to delete the new Building Phase; 
            // select OK to confirm; verify successful delete and appropriate success message.
            if (sequence != 1)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Code", BuildingPhaseData[sequence].Code);
                BuildingPhasePage.Instance.CloseToastMessage();

                if (BuildingPhasePage.Instance.IsItemInGrid("Code", BuildingPhaseData1.Code))
                    ExtentReportsHelper.LogFail($"Not able to delete Building Phase " + BuildingPhaseData[sequence].Code + " " + BuildingPhaseData[sequence].Name);
                else
                    ExtentReportsHelper.LogPass($"Deleted Building Phase {BuildingPhaseData[sequence].Code} {BuildingPhaseData[sequence].Name} successfully!");
            }

            //* After complete the 4th test case. Will delete test data of sequence 2 *//
            if (sequence == 3)
            {
                BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, BuildingPhaseData[1].Code);
                System.Threading.Thread.Sleep(2000);
                BuildingPhasePage.Instance.DeleteItemInGrid("Code", BuildingPhaseData[1].Code);
                BuildingPhasePage.Instance.CloseToastMessage();

                if (BuildingPhasePage.Instance.IsItemInGrid("Code", BuildingPhaseData[1].Code))
                    ExtentReportsHelper.LogFail($"Not able to delete Building Phase " + BuildingPhaseData[1].Code + " " + BuildingPhaseData[1].Name);
                else
                    ExtentReportsHelper.LogPass($"Deleted Building Phase {BuildingPhaseData[1].Code} {BuildingPhaseData[1].Name} successfully!");
            }

            if (Flag == false)
            {
                ExtentReportsHelper.LogFail("There are some error while running this test. Please review the details as above.");
            }  
        }
        #endregion

    }
}
