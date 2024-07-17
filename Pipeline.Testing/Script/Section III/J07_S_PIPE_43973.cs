using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.UserMenu.Role;
using Pipeline.Testing.Pages.UserMenu.Role.RolePermission;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Script.Section_III
{
    public class J07_S_PIPE_43973 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private const string UserRole = "Admin";
        private const string PermissionType = "Purchasing";

        [SetUp]
        public void SetupTest()
        {
        }


        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void J07_S_Settings_Roles_Purchasing_Remove_All_CutOff_Phases_Permissions()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Navigate to Users Menu > Roles.</b></font>");
            RolePage.Instance.SelectMenu(MenuItems.PROFILE, true).SelectItem(ProfileMenu.Roles, true, true);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: \"Switch to new tab to update permission\".</b></font>");
            CommonHelper.SwitchTab(1);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Click Edit Admin permission.</b></font>");
            RolePage.Instance.UpdateRolePermission(UserRole);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.4: Set Permission Type is /'{PermissionType}/'.</b></font>");
            RolePermissionPage.Instance.SelectPermissionType(PermissionType);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.5: Verify if the following settings are removed from the Purchasing permissions list: CutOff Phases - Add,Cutoff Phases - Add Building Phases,Cutoff Phases - Add/Edit on Job,Cutoff Phases - Delete,Cutoff Phases - Edit,Cutoff Phases - Import,Cutoff Phases - Import Option Groups,Cutoff Phases - Import Options,Cutoff Phases - Remove Building Phases.</b></font>");
            string[] permissionRole = { "CutOff Phases - Add","Cutoff Phases - Add Building Phases","Cutoff Phases - Add/Edit on Job","Cutoff Phases - Delete","Cutoff Phases - Edit","Cutoff Phases - Import","Cutoff Phases - Import Option Groups","Cutoff Phases - Import Options","Cutoff Phases - Remove Building Phases" };
            foreach (string role in permissionRole)
            {
                ExtentReportsHelper.LogInformation($"Switch to Permission page and Uncheck {role}.");
                CommonHelper.SwitchTab(1);
                var isFound = RolePage.Instance.FindRolePermision(role);
                if (!isFound)
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Settings {role} is removed from the Purchasing permissions.</b></font>");
            }
        }

        #endregion

        [TearDown]
        public void TearDownTest()
        {

        }
    }
}
