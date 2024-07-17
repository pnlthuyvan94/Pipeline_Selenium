using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.UserMenu.User;
using Pipeline.Testing.Pages.UserMenu.User.AddUser;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class I02_RT_01106 : BaseTestScript
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
                UserName = "QA-RT123",
                Password= "123456",
                ConfirmPass= "123456",
                Email= "RT123@gmail.com",
                Role= "Admin",
                Active= "TRUE",
                FirstName= "Van",
                LastName= "Pham",
                Phone= "1228706916",
                Ext="2",
                Cell= "14",
                Fax= "987654321",
                Address1= "14 Tan Hai",
                Address2= "14/01 Tan Hai",
                City= "HCM",
                State= "IN",
                Zip="1"
            };
        }

        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void I02_UserMenu_AddUser()
        {
            // 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Admin/Users/Users.aspx
            UserPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Users);

            // Step 1.1: Filter the created user name and delete if it's existing
            ExtentReportsHelper.LogInformation(null, $"Filter the created user name '{_userdata.UserName}' and delete if it's existing.");
            UserPage.Instance.FilterItemInGrid("Username", GridFilterOperator.EqualTo, _userdata.UserName);
            if (UserPage.Instance.IsItemInGrid("Username", _userdata.UserName) is true)
            {
                // Delete before creating a new one
                UserPage.Instance.DeleteItemInGrid("Username", _userdata.UserName);
            }

            // 2: click on "+" Add button
            UserPage.Instance.ClickAddUserButton();
            //string expectedURL = ExcelFactory.GetRow(UserPage.Instance.MetaData, 23)[valueToFindColumn].ToString().Replace("dev", ConfigurationManager.GetValue("ApplicationDomain"));
            //Assert.That(AddUserDetailPage.Instance.IsPageDisplayed(expectedURL), "User detail page isn't displayed");

            // 3: Populate all values
            UserPage.Instance.CreateNewUser(_userdata);

            // 4: Verify new user in header
            //Assert.That(AddUserDetailPage.Instance.IsSaveHouseSuccessful(_userdata.UserName), "Create new user unsuccessfully");
            if (AddUserDetailPage.Instance.IsSaveHouseSuccessful(_userdata.UserName))
            {
                ExtentReportsHelper.LogPass("Create new user successfully");
            }
            else
            {
                ExtentReportsHelper.LogFail("Create new user unsuccessfully");
            }
            //ExtentReportsHelper.LogPass("Create User successful");

            // 5: Back to User page
            //UserPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Users);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.ADMIN_USERS_URL);

            // 6: Verify the new user create successfully
            UserPage.Instance.FilterItemInGrid("Username", GridFilterOperator.EqualTo, _userdata.UserName);
            System.Threading.Thread.Sleep(5000);
            bool isFound = UserPage.Instance.IsItemInGrid("Username", _userdata.UserName);
            //Assert.That(isFound, string.Format("New User \"{0} \" was not display on grid.", _userdata.UserName));
            if (isFound)
            {
                ExtentReportsHelper.LogPass(string.Format("New User \"{0} \" was display on grid.", _userdata.UserName));
            }
            else
            {
                ExtentReportsHelper.LogFail(string.Format("New User \"{0} \" was not display on grid.", _userdata.UserName));
            }

            // 7. Select the trash can to delete the new Building Phase; 
            // Select OK to confirm; verify successful delete and appropriate success message.
            UserPage.Instance.DeleteItemInGrid("Username", _userdata.UserName);
            System.Threading.Thread.Sleep(5000);
            //Assert.That(!UserPage.Instance.IsItemInGrid("Username", _userdata.UserName), "New user {0} isn't deleted.", _userdata.UserName);
            if (!UserPage.Instance.IsItemInGrid("Username", _userdata.UserName))
            {
                ExtentReportsHelper.LogPass($"New user {_userdata.UserName} is deleted.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"New user {_userdata.UserName} isn't deleted.");
            }
        }
        #endregion
    }
}
