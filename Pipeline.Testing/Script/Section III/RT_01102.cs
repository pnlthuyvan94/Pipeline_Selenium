using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.UserMenu.User.AddUser;
using Pipeline.Testing.Pages.UserMenu.User;
using Pipeline.Testing.Pages.Sales.Prospect;
using Pipeline.Testing.Based;
using Pipeline.Common.Constants;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class G01_RT_01102 : BaseTestScript
    {
        private const string valueToFindColumn = "Value To Find";
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
                Role = "Prospect",
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
        public void G01_Sales_DeleteProspect()
        {
            // 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Admin/Users/Users.aspx
            UserPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Users);

            // 2: click on "+" Add button
            UserPage.Instance.ClickAddUserButton();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.ADMIN_USERS_URL;
            Assert.That(AddUserDetailPage.Instance.IsPageDisplayed(expectedURL), "User detail page isn't displayed");

            // 3: Populate all values
            Row TestData = ExcelFactory.GetRow(UserPage.Instance.TestData_Data_User_Input, 1);
            UserPage.Instance.CreateNewUser(_userdata);

            // 4: Verify new user in header
            Assert.That(AddUserDetailPage.Instance.IsSaveHouseSuccessful(TestData["Username"]), "Create new user with role Prospect unsuccessfully");
            ExtentReportsHelper.LogPass("Create User successful");

            // 5: Back to User page
            UserPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Users);

            // 6: Verify the new uer create successfully
            UserPage.Instance.FilterItemInGrid("Username", GridFilterOperator.EqualTo, TestData["Username"]);
            bool isFound = UserPage.Instance.IsItemInGrid("Username", TestData["Username"]);
            Assert.That(isFound, string.Format("New User \"{0} \" was not display on grid.", TestData["Username"]));

            // 7: Delete Prospect - open Prospect page
            UserPage.Instance.SelectMenu(MenuItems.SALES).SelectItem(SalesMenu.Prospects);

            // 8: Filter username which have been created
            ProspectPage.Instance.FilterItemInGrid("UserName", GridFilterOperator.EqualTo, TestData["Username"]);
            bool isFoundProspect = ProspectPage.Instance.IsItemInGrid("UserName", TestData["Username"]);
            Assert.That(isFoundProspect, string.Format("New Prospect \"{0} \" was not display on grid.", TestData["Username"]));

            // 9. Select the trash can to delete the prospect; 
            // Select OK to confirm; verify successful delete and appropriate success message.
            ProspectPage.Instance.DeleteItemInGrid("UserName", TestData["Username"]);
            ProspectPage.Instance.WaitGridLoad();

            string expectedMess = $"Prospect {TestData["Username"]} deleted successfully!";
            if (expectedMess == ProspectPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass("New Prospect deleted successfully!");

                ProspectPage.Instance.CloseToastMessage();
            }
            else
            {
                if (ProspectPage.Instance.IsItemInGrid("UserName", TestData["Username"]))
                    ExtentReportsHelper.LogFail("New Prospect could not be deleted!");
                else
                    ExtentReportsHelper.LogPass("New Prospect deleted successfully!");
            }
        }
        #endregion

    }
}
