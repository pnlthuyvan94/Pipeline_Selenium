using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.UserMenu.Role;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class I03_RT_01107 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        RoleData _roledata;
        [SetUp]
        public void CreateTestData()
        {
            _roledata = new RoleData()
            {
              Name= "RT-QA_Role"
            };
        }
        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void I03_UserMenu_AddRole()
        {
            // 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Admin/Users/Roles.aspx
            RolePage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Roles);

            string _expectedMessage = "Role added";
            AddRole(_roledata.Name, _expectedMessage, false);

            // Close Add Role modal
           // RolePage.Instance.AddRoleModal.Close();

            // 6: Verify the new Contract create successfully
            RolePage.Instance.FilterItemInGrid("Role", GridFilterOperator.Contains, _roledata.Name);
            bool isFound = RolePage.Instance.IsItemInGrid("Role", _roledata.Name);
            //Assert.That(isFound, string.Format("New Role \"{0} \" was not display on grid.", _roledata.Name));
            if (isFound)
            {
                ExtentReportsHelper.LogPass(string.Format("New Role \"{0}\" was displayed on grid.", _roledata.Name));
            }
            else
            {
                ExtentReportsHelper.LogFail(string.Format("New Role \"{0}\" was not displayed on grid.", _roledata.Name));
            }

            // 7: Add new Role with duplicate name and verify it
            string _expectedDuplicateMessage = "An error occurred";
            AddRole(_roledata.Name, _expectedDuplicateMessage, true);

            // Close modal
            RolePage.Instance.AddRoleModal.Close();

            // 8: Delete Contract Document
            DeleteRole(_roledata.Name);
        }

        public void AddRole(string roleName, string _expectedMess, bool isDuplicate)
        {
            RolePage.Instance.OpenRoleModal();
            //Assert.That(RolePage.Instance.AddRoleModal.IsModalDisplayed, "Role modal isn't displayed");
            if (RolePage.Instance.AddRoleModal.IsModalDisplayed)
            {
                ExtentReportsHelper.LogPass("Role modal is displayed");
            }
            else
            {
                ExtentReportsHelper.LogFail("Role modal isn't displayed");
            }

            // 2: Populate all values - Click 'Save' Button
            RolePage.Instance.AddRoleModal.EnterName(roleName).Save();

            // Verify message
            string _actualDuplicateMessage = RolePage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_actualDuplicateMessage))
            {
                if (_actualDuplicateMessage != _expectedMess & isDuplicate)
                {
                    ExtentReportsHelper.LogFail($"Create new role with name {roleName} successfully. It should be fail.");
                    ExtentReportsHelper.LogFail($"Create new Role with duplicate name successfully.");
                }
                else if (_actualDuplicateMessage != _expectedMess & !isDuplicate)
                {
                    ExtentReportsHelper.LogFail($"Could not create new role with name {roleName}");
                    ExtentReportsHelper.LogFail($"Could not create new user role.");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Create new role with name {roleName} successfully. / Can't create a new role with duplicate name successfully");
                }
            }
            else
            {
                ExtentReportsHelper.LogFail($"Don't display any message");
                ExtentReportsHelper.LogFail($"Fail to create new role.");
            }

        }

        public void DeleteRole(string roleName)
        {
            // Select OK to confirm; verify successful delete and appropriate success message.
            RolePage.Instance.DeleteItemInGrid("Role", roleName);

            string expectedMess = $"Role was successfully deleted.";
            if (expectedMess == RolePage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass("New Role deleted successfully!");
                RolePage.Instance.CloseToastMessage();
            }
            else
            {
                if (RolePage.Instance.IsItemInGrid("Role", roleName))
                    ExtentReportsHelper.LogFail("Role could not be deleted!");
                else
                    ExtentReportsHelper.LogPass("Role successfully!");
            }
        }
        #endregion
    }
}
