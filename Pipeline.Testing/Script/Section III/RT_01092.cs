using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Costing.CommunitySalesTax;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Import;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class D02_RT_01092 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        private const string BUILDINGPHASE_NAME_DEFAULT = "000-HN-Building-Phase";
        private const string TAXGROUP_NAME_DEFAULT = "Group #5 (7.000%)";
        private const int index = 1;

        [SetUp]
        public void GetData()
        {

            // Prepare a new Building Group to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Building Group to import Product.</font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "101",
                Name = "Hai Nguyen Building Group",
                Description = "Testing Create a Building Group Lorem ipsum dolor sit amet; consectetur adipiscing elit. Proin facilisis ac augue et accumsan metus."
            };
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData.Code) is false)
            {
                // Open a new tab and create a new Category
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            //Prepare data: Import Building Phase to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data: Import Building Phase to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.BUILDING_GROUP_PHASE_VIEW, ImportGridTitle.BUILDING_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products.</font>");

            string importFile = "\\DataInputFiles\\Import\\RT_01092\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();
        }
        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void D02_Costing_AssignATaxGroupOverride()
        {
            // Step 1: navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Costing/Taxes/CommunityTaxes.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1: navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Costing/Taxes/CommunityTaxes.aspx </font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_SALES_TAX_URL);
            // Step 2: Select Community > Select Building Phase > Edit mode > Select Tax Group Override
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2: Select Community > Select Building Phase > Edit mode > Select Tax Group Override</font>");
            CommunitySalesTaxPage.Instance
                .SelectBuildingPhase(BUILDINGPHASE_NAME_DEFAULT)
                .ClickEditItemInGrid("Building Phase", BUILDINGPHASE_NAME_DEFAULT);
            string getTaxGroup = CommunitySalesTaxPage.Instance.SelectTaxGroupOverride(TAXGROUP_NAME_DEFAULT, index);
            CommunitySalesTaxPage.Instance.Save();

            // Step 3: Verify new CommunitySalesTax in header
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 3: Verify new CommunitySalesTax in header</font>");
            if (CommunitySalesTaxPage.Instance.IsItemInGrid("Tax Group Override", getTaxGroup))
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Successful override Tax Group to this Building Phase</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color ='red'>Cannot override Tax Group to this Building Phase</font>");
            }

            // Remove item
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Remove item</font>");
            CommunitySalesTaxPage.Instance.ClickEditItemInGrid("Building Phase", BUILDINGPHASE_NAME_DEFAULT);
            CommunitySalesTaxPage.Instance.SelectTaxGroupOverride(TAXGROUP_NAME_DEFAULT, 0);
            CommunitySalesTaxPage.Instance.Save();
        }
        #endregion

    }
}