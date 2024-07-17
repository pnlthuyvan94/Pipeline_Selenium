using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Manufactures.ManufacturerDetail;
using Pipeline.Testing.Pages.UserMenu.Role;
using Pipeline.Testing.Pages.UserMenu.Role.RolePermission;

namespace Pipeline.Testing.Script.Section_IV
{
    public class B04_RT_01253 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private ManufacturerData oldData;
        private ManufacturerData newTestData;


        [SetUp]
        public void GetData()
        {
            oldData = new ManufacturerData()
            {
                Name = "RT_Manufacturer-QA Only-Auto",
                Url = "https://strongtie.com",
                Description = "Regression Test Manufacturer-QA Only"
            };

            newTestData = new ManufacturerData()
            {
                Name = "RT_Manufacturer-QA Only-Auto",
                Url = "https://strongtie.com",
                Description = "Regression Test Manufacturer-QA Only_Update"
            };

            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);

            // Delete manufacturer with the update name to create a new one on step 3
            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newTestData.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", newTestData.Name) is true)
            {
                // Delete it before creating a new one at step 3
                ManufacturerPage.Instance.DeleteManufacturer(newTestData.Name);
            }

            ExtentReportsHelper.LogInformation($"Filter new item {oldData.Name} in the grid view.");
            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, oldData.Name);

            if (!ManufacturerPage.Instance.IsItemInGrid("Name", oldData.Name))
            {
                ManufacturerPage.Instance.CreateNewManufacturer(oldData);

                // 6. Back to list of manufaturer and verify new item in grid view
                CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_MANUFACTURERS_URL);

                // Insert name to filter and click filter by Contain value
                ManufacturerPage.Instance.EnterManufaturerNameToFilter("Name", oldData.Name);
            }
        }

        [Test]
        [Category("Section_IV")]
        public void B04_A_Estimating_DetailPage_Manufactures_Manufacturer()
        {
            ExtentReportsHelper.LogInformation(" Step 1: Navigate to Estimate menu > Manufacturers > Open page with URL http://dev.bimpipeline.com/Dashboard/Products/Manufacturers/Default.aspx.");

            // Select new item to open Manufacturer detail page
            ExtentReportsHelper.LogInformation($" Select item to open Manufacturer detail page. Ignore all steps related to create new Manufacturer.");
            ManufacturerPage.Instance.SelectItemInGrid("Name", oldData.Name);

            // Verify open Manufacturer detail page display correcly
            if (ManufacturerDetailPage.Instance.IsSaveManufactureSuccessful(oldData.Name) is true)
                ExtentReportsHelper.LogPass($"The Manufacturer detail page of item: {oldData.Name} displays correctly.");
            else
                ExtentReportsHelper.LogFail($"The Manufacturer detail page of item:{oldData.Name} displays with incorrect sub header/ title.");

            // Step 3: Update Manufacturer detail page with valid data
            ExtentReportsHelper.LogInformation(" Step 3: Update Manufacturer detail page.");
            UpdateManufacturer(newTestData);

            // Step 4: Verify Manufacturer permission
            ExtentReportsHelper.LogInformation(" Step 4: Verify Manufacturer permission.");
            ExtentReportsHelper.LogInformation(" Role Admin with permission is locked can't edit and save");
            //string[] permissionRole = { "Manufacturers - Add", "Manufacturers - Delete", "Manufacturers - Edit" };
            //UpdateManufacturePermission("Admin", "Products", permissionRole);


        }

        private void UpdateManufacturer(ManufacturerData newTestData)
        {

            ExtentReportsHelper.LogInformation("Update Manufacturer with valid data.");
            string expectedMessage = $"Manufacturer {newTestData.Name} saved successfully!";

            ManufacturerDetailPage.Instance.UpdateManufacturer(newTestData);


            var actualMessage = ManufacturerDetailPage.Instance.GetLastestToastMessage();
            if (actualMessage == string.Empty)
            {
                ExtentReportsHelper.LogInformation($"Can't get toast message after 10s");
            }
            else if (expectedMessage == actualMessage)
            {
                ExtentReportsHelper.LogPass("Update successfully. The toast message same as expected.");

                if (ManufacturerDetailPage.Instance.IsDisplayDataCorrectly(newTestData))
                    ExtentReportsHelper.LogPass("The updated data displays or reset correctly in the grid view.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"Toast message must be same as expected. Expected: {expectedMessage}");
            }
            ManufacturerDetailPage.Instance.CloseToastMessage();
        }

        private void UpdateManufacturePermission(string userRole, string permissionType, string[] permissionRole)
        {
            ExtentReportsHelper.LogInformation("Navigate to Role page to update permission.");
            RolePage.Instance.SelectMenu(MenuItems.PROFILE, true).SelectItem(ProfileMenu.Roles, true, true);

            ExtentReportsHelper.LogInformation("Switch to new tab to update permission");
            CommonHelper.SwitchTab(1);

            if (RolePage.Instance.IsRoleLocked(userRole))
            {
                ExtentReportsHelper.LogInformation($"Permissions are locked for User '{userRole}', skipping permissions verification...");
                CommonHelper.CloseCurrentTab();
                CommonHelper.SwitchTab(0);
                return;
            }

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

                ExtentReportsHelper.LogInformation($"Switch to Manufacturer DEFAULT page to verify {role} function.");
                CommonHelper.SwitchTab(0);
                CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_MANUFACTURERS_URL);
                ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newTestData.Name);
                if (!ManufacturerPage.Instance.IsNotDisplayButton(role, newTestData.Name))
                    ExtentReportsHelper.LogFail("Add Manufacturer button in Manufacturer default page should be invisible.");
                else
                    ExtentReportsHelper.LogPass("Add Manufacturer button in Manufacturer default page is hidden.");

                if ("Manufacturers - Add" == role)
                {
                    ExtentReportsHelper.LogInformation("Switch to Manufacturer DETAIL page to verify add function");
                    ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newTestData.Name);
                    ManufacturerPage.Instance.SelectItemInGrid("Name", newTestData.Name);
                    if (ManufacturerDetailPage.Instance.IsDisplayAddButton())
                    {
                        ExtentReportsHelper.LogFail("add Manufacturer button in Manufacturer detail page should be invisible.");
                        Assert.Fail();
                    }
                    else
                        ExtentReportsHelper.LogPass("add Manufacturer button in Manufacturer detail page is hidden.");
                }
            }

            ExtentReportsHelper.LogPass($"Reset permission of role {permissionType} to ALL and close tab.");
            CommonHelper.SwitchTab(1);
            // There are bug with "All" button => Set check for single permission
            //RolePage.Instance.RolePermission.SelectAllPermisionOnTheGrid().SaveNewPermission();
            foreach (string role in permissionRole)
            {
                RolePermissionPage.Instance.SetStatusSinglePermission(role, true);
            }
            RolePermissionPage.Instance.SaveNewPermission();
            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);

        }

        [TearDown]
        public void DeleteManufacturer()
        {
            // Back to Manufacturer Default page to delete itm
            ExtentReportsHelper.LogInformation($"Back to Manufacturer default page and delete item {newTestData.Name}");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_MANUFACTURERS_URL);

            // Filter New item
            ExtentReportsHelper.LogInformation($"Filter new item {newTestData.Name} in the grid view.");
            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newTestData.Name);
            System.Threading.Thread.Sleep(2000);

            // 7. Select item and click deletete icon
            if (ManufacturerPage.Instance.IsItemInGrid("Name", newTestData.Name) is true)
                ManufacturerPage.Instance.DeleteManufacturer(newTestData.Name);
        }
    }
}
