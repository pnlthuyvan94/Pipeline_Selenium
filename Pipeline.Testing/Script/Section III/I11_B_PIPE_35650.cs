using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Settings.Users;
using Pipeline.Testing.Pages.Settings.Users.UsersDetail;
using Pipeline.Testing.Pages.UserMenu.Setting;

namespace Pipeline.Testing.Script.Section_III
{
    class I11_B_PIPE_35650 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        private readonly string UPDATE_USERNAME_INSYSTEM = "sysadmin";
        private readonly string NEW_USERNAME = "QA_RT_Automation123";

        private UserData _userdata;

        [SetUp]
        public void SetUp()
        {
            _userdata = new UserData()
            {
                Username = "QA_RT_Automation",
                Email= "qa_auto@strongtie.com",
                Role="Admin",
                firstname = "QA",
                lastname= "Auto"
            };

            //Prepare data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data.</font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Users);
            UserPage.Instance.FilterItemInGrid("Username", GridFilterOperator.Contains, _userdata.Username);
            if (UserPage.Instance.IsItemInGrid("Username", _userdata.Username) is false)
            {
                //click on "+" Add button and Populate all values and save new username
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>click on " + " Add button and Populate all values and save new username.</font>");
                UserPage.Instance.ClickAddToUserIcon();
                UserDetailPage.Instance.CreateNewUsername(_userdata);
                string actual_Msg = UserDetailPage.Instance.GetLastestToastMessage();
                string expect_Msg = "User created Successfully. User is already a part of the SSO";
                if (actual_Msg.Equals(expect_Msg))
                {
                    ExtentReportsHelper.LogPass(null, "<font color ='green'><b> UserName is created successfully.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"<font color='red'> UserName is created unsuccessfully.</font>");
                }
            }
        }

        [Test]
        [Category("Section_III")]
        public void I11_B_UserProfile_User_can_login_to_other_accounts_even_sysadmin_without_using_password()
        {

            //1. User attempts to change username to an another existing one, then try with sysadmin
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1. User attempts to change username to an another existing one, then try with sysadmin.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.ADMIN_USERS_URL);
            //1.1 Go to the PL site> Click on the “Avatar” icon > Click on “Users”
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.1 Go to the PL site> Click on the “Avatar” icon > Click on “Users”.</font>");
            //1.2 On the Users page: Search the Username of the any account and go to the User detail
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.2 On the Users page: Search the Username of the any account and go to the User detail.</font>");
            UserPage.Instance.FilterItemInGrid("Username", GridFilterOperator.Contains, _userdata.Username);
            //1.3 On the User detail: Change “Username” field to existed Username in the system' and save
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.3 On the User detail: Change “Username” field to existed Username in the system' and save.</font>");
            if (UserPage.Instance.IsItemInGrid("Username", _userdata.Username) is true)
            {
                UserPage.Instance.SelectItemInGrid("Username", _userdata.Username);
                //Update UserName 
                UserDetailPage.Instance.EditInputUsername(UPDATE_USERNAME_INSYSTEM);
                UserDetailPage.Instance.UpdateSave();
                string actual_ErrorMsg = UserDetailPage.Instance.GetLastestToastMessage();
                string expect_ErrorMsg = "Username is not unique. Provide a different username.";
                if (actual_ErrorMsg.Equals(expect_ErrorMsg))
                {
                    ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Actual Message is displayed as expect.</b></font>\nActual results: " + actual_ErrorMsg);
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"<font color='red'>Actual Message is not displayed as expect.</font> \nActual results: {actual_ErrorMsg}\nExpected results: {expect_ErrorMsg} ");
                }
            }

            //2. User attempts to change username to a non-existing one: On the User detail: Change “Username” field to another Username doesn’t belong the PL system and save
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2. User attempts to change username to a non-existing one: On the User detail: Change “Username” field to another Username doesn’t belong the PL system and save.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.ADMIN_USERS_URL);
            UserPage.Instance.FilterItemInGrid("Username", GridFilterOperator.Contains, _userdata.Username);

            if (UserPage.Instance.IsItemInGrid("Username", _userdata.Username) is true)
            {
                UserPage.Instance.SelectItemInGrid("Username", _userdata.Username);

                //Update UserName 
                UserDetailPage.Instance.EditInputUsername(NEW_USERNAME);
                UserDetailPage.Instance.UpdateSave();

                string actual_Msg = UserDetailPage.Instance.GetLastestToastMessage();
                string expect_Msg = "Update Successful";
                if (actual_Msg.Equals(expect_Msg))
                {
                    ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Update UserName is successfully.</b></font>\nActual results: " + actual_Msg);
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"<font color='red'>Update UserName is unsuccessfully.</font>\nActual results: {actual_Msg}\nExpected results: {expect_Msg} ");
                }
            }

        }

        [TearDown]
        public void DeleteProduct()
        {
            // Delete data
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.ADMIN_USERS_URL);
            UserPage.Instance.FilterItemInGrid("Username", GridFilterOperator.Contains, NEW_USERNAME);
            if (UserPage.Instance.IsItemInGrid("Username", NEW_USERNAME) is true)
            {
                UserPage.Instance.DeleteItemInGrid("Username", NEW_USERNAME);
            }
        }

    }
}
