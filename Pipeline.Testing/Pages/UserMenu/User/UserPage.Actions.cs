using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.UserMenu.User.AddUser;

namespace Pipeline.Testing.Pages.UserMenu.User
{
    public partial class UserPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            User_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            User_Grid.WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return User_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            User_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            User_Grid.WaitGridLoad();
        }

        public void ClickAddUserButton()
        {
            PageLoad();
            GetItemOnHeader(DashboardContentItems.Add).Click();
            // Open Detail User page
            AddUserDetail = new AddUserDetailPage();
        }

        public void CreateNewUser(UserData _userdata)
        {
            // Input data to field and click save button     
            AddUserDetail.CreateNewUser(_userdata);
            AddUserDetail.Save();
            // There is no loading icon after saving a new user
            System.Threading.Thread.Sleep(5000);
        }
    }

}
