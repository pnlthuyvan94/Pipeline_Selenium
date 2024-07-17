using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Costing.TaxGroup;
using Pipeline.Testing.Pages.Costing.TaxGroup.AddTaxGroup;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class D03_RT_01093 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        TaxGroupData _taxgroup;

        [SetUp]
        public void GetData()
        {
            _taxgroup = new TaxGroupData()
            {
                Name = "RT-Auto_New-TaxGroup"
            };
        }
        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void D03_Costing_AddATaxGroup()
        {
            // Step 1: navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Costing/Taxes/CommunityTaxes.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 1: navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Costing/Taxes/CommunityTaxes.aspx </font>");
            TaxGroupPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.TaxGroups);

            // Find item in grid
            TaxGroupPage.Instance.FindItemInGridWithTextContains("Tax Group", _taxgroup.Name);
            if (TaxGroupPage.Instance.IsItemInGrid(_taxgroup.Name) is true)
            {
                // Delete
                string expectedMsg = "Tax Group deleted successfully";
                string actualMsg = TaxGroupPage.Instance.DeleteItemInGrid("Tax Group", _taxgroup.Name);
                if (expectedMsg.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogPass("<font color ='green'><b>Successful delete a new Tax Group</b></font>");
                }
                else
                {
                    if (TaxGroupPage.Instance.IsItemInGrid(_taxgroup.Name) is true)
                        ExtentReportsHelper.LogFail($"<font color ='red'>Tax Group could not be deleted!</font> Actual message: <i>{actualMsg}</i>");
                    else
                        ExtentReportsHelper.LogPass("<font color ='green'><b>Successful delete a Tax Group</b></font>");
                }
            }

            // Step 2: Create new tax group
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 2: Create new tax group </font>");
            TaxGroupPage.Instance.ClickAddToOpenTaxGroupModal();
            AddTaxGroupModal.Instance.AddTaxGroupModal.EnterTaxGroupName(_taxgroup.Name).Save();
            // Find item in grid
            TaxGroupPage.Instance.FindItemInGridWithTextContains("Tax Group", _taxgroup.Name);

            // Step 3: Verify new CommunitySalesTax in header
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3: Verify new CommunitySalesTax in header</font>");
            if (TaxGroupPage.Instance.IsItemInGrid(_taxgroup.Name) is true)
            {
                
                ExtentReportsHelper.LogPass("<font color ='green'><b>Create a new Tax Group successfully</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color ='red'>Create a new Tax Group unsuccessfully.</font>");
            }

            TaxGroupPage.Instance.RefreshPage();

            // Step 4: Change page size
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4: Change page size</font>");
            TaxGroupPage.Instance.ChangePageSize(500);
            CommonHelper.ScrollToEndOfPage();

            // Step 5: Find item in grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 5: Find item in grid</font>");
            TaxGroupPage.Instance.FindItemInGridWithTextContains("Tax Group", _taxgroup.Name);

            if (TaxGroupPage.Instance.IsItemInGrid(_taxgroup.Name) is true)
            {
                //Delete
                string expectedMsg = "Tax Group deleted successfully";
                string actualMsg = TaxGroupPage.Instance.DeleteItemInGrid("Tax Group", _taxgroup.Name);
                if (expectedMsg.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogPass("<font color ='green'><b>Successful delete a new Tax Group</b></font>");
                }
                else
                {
                    if (TaxGroupPage.Instance.IsItemInGrid(_taxgroup.Name) is true)
                        ExtentReportsHelper.LogFail($"<font color ='red'>Tax Group could not be deleted!</font> Actual message: <i>{actualMsg}</i>");
                    else
                        ExtentReportsHelper.LogPass("<font color ='green'><b>Successful delete a new Tax Group</b></font>");
                }
            }
            
        }
        #endregion

    }
}