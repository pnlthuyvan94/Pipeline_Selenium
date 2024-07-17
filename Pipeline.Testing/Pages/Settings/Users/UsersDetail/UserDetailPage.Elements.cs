
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Settings.Users.UsersDetail
{
    public partial class UserDetailPage : DetailsContentPage<UserDetailPage>
    {
        protected Textbox Username_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_user1_txtNewUserName']");
        protected Textbox EditUsername_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_user1_txtUsernameEdit']");
        protected Textbox Email_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_user1_txtNewUserEmail']");
        protected Textbox EditEmail_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_user1_txtEmailEdit']");
        protected DropdownList Role_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_user1_ddlRolesNew']");
        protected Textbox Firstname_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_user1_txtFName']");
        protected Textbox Lastname_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_user1_txtLName']");
        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_user1_lnkCreate']");
        protected Button UpdateSave_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_user1_lnkSave']");
    }
}

