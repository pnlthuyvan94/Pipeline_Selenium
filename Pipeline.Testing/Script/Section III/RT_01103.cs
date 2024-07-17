using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.UserMenu.User.AddUser;
using Pipeline.Testing.Pages.UserMenu.User;
using Pipeline.Testing.Pages.Sales.Customer;
using Pipeline.Testing.Based;
using Pipeline.Common.Constants;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class G02_RT_01103 : BaseTestScript
    {
        private const string valueToFindColumn = "Value To Find";
        private Row TestData = ExcelFactory.GetRow(UserPage.Instance.TestData_Data_User_Input, 1);
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        UserData _userdata;
        [SetUp]
        public void CreateTestData()
        {
            _userdata = new UserData()
            {
                UserName = "QA-RT1",
                Password = "123456",
                ConfirmPass = "123456",
                Email = "RT1@gmail.com",
                Role = "Customer",
                Active = "TRUE",
                FirstName = "Van",
                LastName = "Pham",
                Phone = "1228706916",
                Ext = "2",
                Cell = "14",
                Fax = "987654321",
                Address1 = "14 Tan Hai",
                Address2 = "14/01 Tan Hai",
                City = "HCM",
                State = "IN",
                Zip = "1"
            };
        }
        #region"Test case"
        [Test]
        [Category("Section_III")]
        [Ignore("Sales menu was removed from Pipeline, so this test sript will be ignored.")]
        public void G02_Sales_DeleteCustomer()
        {
            // 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Admin/Users/Users.aspx
            UserPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Users);

            // 2: click on "+" Add button
            ExtentReportsHelper.LogInformation("<b>Create User.</b>");
            UserPage.Instance.ClickAddUserButton();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.ADMIN_USERS_URL;
            Assert.That(AddUserDetailPage.Instance.IsPageDisplayed(expectedURL), "User detail page isn't displayed");

            // 3: Populate all values
            UserPage.Instance.CreateNewUser(_userdata);

            // 4: Verify new user in header
            Assert.That(AddUserDetailPage.Instance.IsSaveHouseSuccessful(TestData["Username"]), "Create new user unsuccessfully");
            ExtentReportsHelper.LogPass("Create User successful");
   
            // 5: Back to User page
            UserPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Users);

            // 6: Verify the new user create successfully
            UserPage.Instance.FilterItemInGrid("Username", GridFilterOperator.EqualTo, TestData["Username"]);
            bool isFound = UserPage.Instance.IsItemInGrid("Username", TestData["Username"]);
            Assert.That(isFound, string.Format("New User \"{0} \" was not display on grid.", TestData["Username"]));

            /* Sale menu is remove. So these code will be documented  
            // 7: Delete Customer page - open customer page
            UserPage.Instance.SelectMenu(MenuItems.SALES).SelectItem(SalesMenu.Customers);

            // 8: Filter username which have been created
            CustomerPage.Instance.FilterItemInGrid("UserName", GridFilterOperator.EqualTo, TestData["Username"]);
            bool isFoundCustomer = CustomerPage.Instance.IsItemInGrid("UserName", TestData["Username"]);
            Assert.That(isFoundCustomer, string.Format("New Customer \"{0} \" was not display on grid.", TestData["Username"]));

            // 9. Select the trash can to delete the new Building Phase; 
            // Select OK to confirm; verify successful delete and appropriate success message.
            CustomerPage.Instance.DeleteItemInGrid("UserName", TestData["Username"]);
            CustomerPage.Instance.WaitGridLoad();

            string expectedMess = $"Customer {TestData["Username"]} deleted successfully!";
            if (expectedMess == CustomerPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass("New Customer deleted successfully!");

                CustomerPage.Instance.CloseToastMessage();
            }
            else
            {
                if (CustomerPage.Instance.IsItemInGrid("UserName", TestData["Username"]))
                    ExtentReportsHelper.LogFail("New Customer could not be deleted!");
                else
                    ExtentReportsHelper.LogPass("New Customer deleted successfully!");
            }
            */
        }
        #endregion

        [TearDown]
        public void DeleteUser()
        {
            ExtentReportsHelper.LogInformation("<b>Delete User.</b>");

            UserPage.Instance.DeleteItemInGrid("Username", TestData["Username"]);
            System.Threading.Thread.Sleep(5000);
            Assert.That(!UserPage.Instance.IsItemInGrid("Username", TestData["Username"]), "New user {0} isn't deleted.", TestData["Username"]);
        }

    }
}
