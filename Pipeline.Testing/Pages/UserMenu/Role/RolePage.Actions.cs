using OpenQA.Selenium;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.UserMenu.Role.AddRole;
using Pipeline.Testing.Pages.UserMenu.Role.RolePermission;
using System.Configuration;
using System.Reflection;

namespace Pipeline.Testing.Pages.UserMenu.Role
{
    public partial class RolePage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Role_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgRoles']/div[1]");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Role_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Role_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgRoles']/div[1]");
        }

        public void OpenRoleModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddRoleModal = new AddRoleModal();
        }

        public void UpdateRolePermission(string roleName)
        {
            Role_Grid.ClickEditItemInGrid("Role", roleName);
        }

        public bool FindRolePermision(string itemName)
        {
            string xpath = $"//input/../label[text()='{itemName}']/../input";
            var el = FindElementHelper.FindElement(FindType.XPath, xpath);
            if (el == null)
                return false;

            return true;
        }



    }

}
