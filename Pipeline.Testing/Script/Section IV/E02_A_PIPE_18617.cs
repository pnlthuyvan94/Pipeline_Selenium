using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.BuildingPhaseDetail;
using Pipeline.Testing.Pages.Purchasing.ReleaseGroup;
using Pipeline.Testing.Pages.Purchasing.ReleaseGroup.ReleaseGroupDetail;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class E02_A_PIPE_18617 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private string[] buildingPhaseList;

        private BuildingGroupData newBuildingGroup;
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_E02A";
        private const string NewBuildingGroupCode = "E02A";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_E02A";

        private BuildingPhaseData newBuildingPhase;
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_E02A";
        private const string NewBuildingPhaseCode = "E02A";

        private ReleaseGroupData newReleaseGroup;
        private const string NewReleaseGroupName = "RT_QA_New_ReleaseGroup_E02A";
        // Pre-condition
        [SetUp]
        public void GetTestData()
        {
            newBuildingGroup = new BuildingGroupData()
            {
                Name = NewBuildingGroupName,
                Code = NewBuildingGroupCode,
                Description = NewBuildingGroupDescription
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Add New Building Group.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroup);
            }

            newBuildingPhase = new BuildingPhaseData()
            {
                Code = NewBuildingPhaseCode,
                Name = NewBuildingPhaseName,
                BuildingGroupCode = NewBuildingGroupCode,
                BuildingGroupName = NewBuildingGroupName
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Add new Building Phase.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is false)
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal
                                          .EnterPhaseCode(newBuildingPhase.Code)
                                          .EnterPhaseName(newBuildingPhase.Name)
                                          .EnterAbbName(newBuildingPhase.AbbName)
                                          .EnterDescription(newBuildingPhase.Description);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(newBuildingPhase.BuildingGroup);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPayment("TEST");
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPO("TEST");
                BuildingPhasePage.Instance.AddBuildingPhaseModal.ClickTaxableYes();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3 Add new Release Group.</b></font>");
            newReleaseGroup = new ReleaseGroupData()
            {
                Name = NewReleaseGroupName,
                Description = NewReleaseGroupName,
                SortOrder = "0"
            };
            ReleaseGroupPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.ReleaseGroups);
            ReleaseGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newReleaseGroup.Name);
            if (ReleaseGroupPage.Instance.IsItemInGrid("Name", newReleaseGroup.Name) is false)
            {
                string _expectedMessage = "Release Group successfully added.";
                ReleaseGroupPage.Instance.AddReleaseGroup(newReleaseGroup, _expectedMessage, false);
            }

        }


        [Test]
        [Category("Section_IV")]
        public void E02_A_Purchasing_DetailPage_ReleaseGroups_ReleaseGroup()
        {
            buildingPhaseList = new string[] { NewBuildingPhaseCode + "-" + NewBuildingPhaseName };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Update Release Group with new value.</b></font>");
            ReleaseGroupPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.ReleaseGroups);
            ReleaseGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newReleaseGroup.Name);
            if (ReleaseGroupPage.Instance.IsItemInGrid("Name", newReleaseGroup.Name) is true)
            {
                ReleaseGroupPage.Instance.SelectItemInGrid("Name", newReleaseGroup.Name);
                newReleaseGroup.Name = newReleaseGroup.Name + "_updated";
                
                ReleaseGroupDetailPage.Instance.UpdateReleaseGroup(newReleaseGroup);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Assign Building Phases to current Release Group.</b></font>");
                ReleaseGroupDetailPage.Instance.ClickAddBuildingPhase();
                if (ReleaseGroupDetailPage.Instance.IsAddBuildingPhaseDisplayed() is true)
                    ExtentReportsHelper.LogPass(null, "<font color = 'green'><b>Add Building Phases modal displays correctly.<b></font>");
                else
                    ExtentReportsHelper.LogFail("<font color = 'red'>Add Building Phases modal doesn't display or the title isn't correct.</font>");

                ReleaseGroupDetailPage.Instance.SelectBuildingPhaseByName(buildingPhaseList);


                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Filter these assigned Building Phases.</b></font>");
                foreach (var item in buildingPhaseList)
                {
                    if (ReleaseGroupDetailPage.Instance.IsItemInGrid("Name", item) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Assigned Building Phase with name '{item}' to current Release Group successfully.</b></font>");
                    else
                        ExtentReportsHelper.LogPass(null, $"<font color='red'>Can't find Building Phases with name {item} on the grid view." +
                            $"<br>Failed to assign Building Phase '{item}' to current Release Group.</font>");
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Remove Building Phase from Release Group.</b></font>");
                ReleaseGroupDetailPage.Instance.RemoveAllBuildingPhase();

            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Add Release Group in Building Phase detail page.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is true)
            {
                BuildingPhasePage.Instance.ClickItemInGrid("Code", NewBuildingPhaseCode);
                BuildingPhaseDetailPage.Instance.SelectReleaseGroup(newReleaseGroup.Name, 1);
                BuildingPhaseDetailPage.Instance.Save();

                ReleaseGroupPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.ReleaseGroups);
                ReleaseGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newReleaseGroup.Name);
                if (ReleaseGroupPage.Instance.IsItemInGrid("Name", newReleaseGroup.Name) is true)
                {
                    ReleaseGroupPage.Instance.SelectItemInGrid("Name", newReleaseGroup.Name);
                    if (ReleaseGroupDetailPage.Instance.IsItemInGrid("Name", NewBuildingPhaseCode + "-" + NewBuildingPhaseName) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Assigned Building Phase with name '{NewBuildingPhaseCode + "-" + NewBuildingPhaseName}' to current Release Group successfully.</b></font>");
                    else
                        ExtentReportsHelper.LogPass(null, $"<font color='red'>Can't find Building Phases with name {NewBuildingPhaseCode + "-" + NewBuildingPhaseName} on the grid view." +
                            $"<br>Failed to assign Building Phase '{NewBuildingPhaseCode + "-" + NewBuildingPhaseName}' to current Release Group.</font>");
                }
            }
        }


        [TearDown]
        public void DeleteData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Remove ALL Building Phase from Release Group.</b></font>");
            ReleaseGroupPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.ReleaseGroups);
            ReleaseGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newReleaseGroup.Name);
            if (ReleaseGroupPage.Instance.IsItemInGrid("Name", newReleaseGroup.Name) is true)
            {
                ReleaseGroupPage.Instance.SelectItemInGrid("Name", newReleaseGroup.Name);
                ReleaseGroupDetailPage.Instance.RemoveAllBuildingPhase();
            }

            // Delete Release Group
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2: Back to Release Groups default page and delete it.</b></font>");
            ReleaseGroupPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.ReleaseGroups);

            // Delete the updated data
            ReleaseGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newReleaseGroup.Name);
            if (ReleaseGroupPage.Instance.IsItemInGrid("Name", newReleaseGroup.Name) is true)
            {
                // Create a new Release Group if it doesn't exist
                ReleaseGroupPage.Instance.DeleteReleaseGroupByName(newReleaseGroup.Name);
            }

            //delete bp
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.3 Delete building phase.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Name", NewBuildingPhaseName);
            }
            //delete bg
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.4 Delete building group.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is true)
            {
                BuildingGroupPage.Instance.DeleteBuildingGroup(newBuildingGroup);
            }

        }
    }
}
