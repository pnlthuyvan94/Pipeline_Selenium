using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.UserMenu.Role.RolePermission
{
    public partial class RolePermissionPage
    {
        public RolePermissionPage SetStatusSinglePermission(string itemName, bool status)
        {
            CheckBox permission = new CheckBox(FindType.XPath, $"//input/../label[text()='{itemName}']/../input");
            if (permission.IsNull())
            {
                ExtentReportsHelper.LogFail($"The Permission with name <font color='green'><b>{itemName}</b></font> is not displayed on permission list.");
            }
            else
            {
                permission.SetCheck(status);
            }
            return this;
        }

        public RolePermissionPage SelectPermissionType(string type)
        {
            PermissionType_ddl.SelectItem(type, true, false);
            WaitingLoadingGifByXpath(LoadingSelectPermission);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(PermissionType_ddl), $"The value <b>{type}</b> is selected.");
            return this;
        }

        public void SaveNewPermission()
        {
            SavePermission_btn.Click();
            WaitingLoadingGifByXpath(LoadingSave);
        }

        public RolePermissionPage SelectAllPermisionOnTheGrid()
        {
            SelectAll_btn.Click();
            return this;
        }

        public RolePermissionPage UnSelectAllPermisionOnTheGrid()
        {
            SelectNone_btn.Click();
            return this;
        }
    }
}
