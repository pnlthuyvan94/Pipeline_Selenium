using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Estimating.Styles.DetailStyles;
using Pipeline.Testing.Pages.UserMenu.Role;
using Pipeline.Testing.Pages.UserMenu.Role.RolePermission;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class B03_B_RT_01252 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private StyleData oldData;
        private StyleData updateTestData;

        // Pre-condition
        [SetUp]
        public void GetTestData()
        {
            oldData = new StyleData()
            {
                Name = "Regression QA Only Style_Auto",
                Manufacturer = "RT Manufacturer",
                Url = "https://strongtie.com",
                Description = "Regression QA Product Style Only - Do Not Use"
            };

            updateTestData = new StyleData()
            {
                Name = "Regression QA Only Style_Auto_Update",
                Manufacturer = "Armstrong",
                Url = "https://strongtie.com",
                Description = "Regression QA Product Style Only - Do Not Use - Update"
            };

            // Step 1: Navigate to Estimate menu > Style > Open page with URL http://dev.bimpipeline.com/Dashboard/Products/Styles/Default.aspx.
            ExtentReportsHelper.LogInformation(" Step 1: Navigate to Estimate menu > Style > Open page with URL http://dev.bimpipeline.com/Dashboard/Products/Styles/Default.aspx.");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);

            var _styleURL = BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_URL;
            if (StylePage.Instance.IsPageDisplayed(_styleURL) is true)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Style page is displayed.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Style page isn't displayed.</font>");
            }
            ExtentReportsHelper.LogInformation($"Filter new item {oldData.Name} in the grid view.");
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, oldData.Name);
            if (StylePage.Instance.IsItemInGrid("Name", oldData.Name) is false)
            {
                ExtentReportsHelper.LogInformation($"The item {oldData.Name} is NOT display in the grid view.");
                StylePage.Instance.CreateNewStyle(oldData);
            }
            else
            {
                CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_URL);

                // Insert name to filter and click filter by Contain value
                StylePage.Instance.EnterStyleNameToFilter("Name", oldData.Name);

                // Step 2: Select new item to open Style detail page
                ExtentReportsHelper.LogInformation($" Step 2: Select new item to open Style detail page. Ignore all steps related to create new Style.");
                StylePage.Instance.SelectItemInGrid("Name", oldData.Name);
            }           
        }


        [Test]
        [Category("Section_IV")]
        public void B03_B_Estimating_DetailPage_Styles_Style()
        {

            // Step 3: Update Style detail page
            ExtentReportsHelper.LogInformation(" Step 3: Update Style detail page.");
            UpdateStyle(updateTestData);

            // Step 4: Verify Style permission
            ExtentReportsHelper.LogInformation(" Step 4: Verify Style permission.");
            ExtentReportsHelper.LogInformation(" Role Admin with permission is locked can't edit and save");
            //string[] permissionRole = { "Styles - Add", "Styles - Edit", "Styles - Delete" };
            // UpdateStylePermission("Admin", "Products", permissionRole);

            // Go to the manufacture page and delete the data
            // Using this testcase to delete the testdata created by TC Add A Product Style.
            //ExtentReportsHelper.LogInformation($" Step 5: Open Manufacturer page and delete new manufacturer item: {oldData.Manufacturer}");
            //DeleteManufacturer(oldData.Manufacturer);
        }

        private void UpdateStyle(StyleData updateTestData)
        {
            StyleData newStyte = StyleDetailPage.Instance.UpdateStyle(updateTestData);

            if (StyleDetailPage.Instance.IsUpdateDataCorrectly(newStyte))
                ExtentReportsHelper.LogPass("The updated data displays correctly in the grid view.");
            else
                ExtentReportsHelper.LogFail("The updated data isn't displays correctly in the grid view.");

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_URL);
        }

        private void UpdateStylePermission(string userRole, string permissionType, string[] permissionRole)
        {
            ExtentReportsHelper.LogInformation("Navigate to Role page in new tab to update permission.");

            RolePage.Instance.SelectMenu(MenuItems.PROFILE, true).SelectItem(ProfileMenu.Roles, true, true);

            ExtentReportsHelper.LogInformation("Switch to new tab to update permission");

            CommonHelper.SwitchTab(1);

            ExtentReportsHelper.LogInformation("Click Edit Admin permission.");

            RolePage.Instance.UpdateRolePermission(userRole);

            ExtentReportsHelper.LogInformation($"Set Permission Type is /'{permissionType}/'");

            RolePermissionPage.Instance.SelectPermissionType(permissionType);

            foreach (string role in permissionRole)
            {
                ExtentReportsHelper.LogInformation($"Switch to Permission page and Uncheck {role}.");

                CommonHelper.SwitchTab(1);
                // Uncheck permission
                RolePermissionPage.Instance.SetStatusSinglePermission(role, false).SaveNewPermission();

                ExtentReportsHelper.LogInformation($"Switch to Style DEFAULT page to verify {role} function.");

                CommonHelper.SwitchTab(0);

                //Debug<
                System.Threading.Thread.Sleep(1000);
                ExtentReportsHelper.LogInformation($"[DEBUG]: Capturing screen after set Role and switched back to original tab. Current Role test = '{role}'");
                //>Debug

                StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, updateTestData.Name);

                if (!StylePage.Instance.IsNotDisplayButton(role, updateTestData.Name))
                    ExtentReportsHelper.LogFail("Add Style button in Style default page should be invisible.");
                else
                    ExtentReportsHelper.LogPass("Add Style button in Style default page is hidden.");

                if ("Styles - Add" == role)
                {
                    ExtentReportsHelper.LogInformation("Switch to style DETAIL page to verify add function");

                    StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, updateTestData.Name);
                    StylePage.Instance.SelectItemInGrid("Name", updateTestData.Name);

                    if (StyleDetailPage.Instance.IsDisplayAddButton())
                        ExtentReportsHelper.LogFail("Add style button in style detail page should be invisible.");
                    else
                        ExtentReportsHelper.LogPass("Add style button in style detail page is hidden as expected.");
                }
            }

            ExtentReportsHelper.LogPass($"Reset permission of role {permissionType} to ALL and close tab.");

            //--wd/10_30_2021:  The "All" button still doesn't work so avoid using the 'SelectAllPermissionOnGrid' until this defect is addressed
            //RolePermissionPage.Instance.SelectAllPermisionOnTheGrid().SaveNewPermission();

            CommonHelper.SwitchTab(1);

            foreach (string role in permissionRole)
            {
                RolePermissionPage.Instance.SetStatusSinglePermission(role, true);
            }
            RolePermissionPage.Instance.SaveNewPermission();

            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
            CommonHelper.FocusContent();
            StylePage.Instance.RefreshPage();
        }

        [TearDown]
        public void DeleteStyle()
        {
            // Back to Style page and delete item
            ExtentReportsHelper.LogInformation($" Step 6: Back to Style page and delete style {updateTestData.Name}.");

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_URL);

            // Insert name to filter and click filter by Contain value
            StylePage.Instance.EnterStyleNameToFilter("Name", updateTestData.Name);

            bool isFound = StylePage.Instance.IsItemInGrid("Name", updateTestData.Name);
            if (isFound)
            {
                // 5. Select the trash can to delete the new Selection Group; 
                StylePage.Instance.DeleteItemInGrid("Name", updateTestData.Name);
                StylePage.Instance.WaitGridLoad();

                if ("Style was deleted!" == StylePage.Instance.GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass($"Product Style {updateTestData.Name} deleted successfully.");
                }
                else
                {
                    if (StylePage.Instance.IsItemInGrid("Name", updateTestData.Name))
                        ExtentReportsHelper.LogFail($"Product Style {updateTestData.Name} could not be deleted.");
                    else
                        ExtentReportsHelper.LogPass($"Product Style {updateTestData.Name} deleted successfully.");
                }
            }
        }
    }
}
