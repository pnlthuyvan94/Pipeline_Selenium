namespace Pipeline.Testing.Pages.Settings.Users.UsersDetail
{
    public partial class UserDetailPage
    {
        public UserDetailPage InputUsername(string username)
        {
            Username_txt.SetText(username);
            return this;
        }

        public UserDetailPage InputEmail(string email)
        {
            Email_txt.SetText(email);
            return this;
        }

        public UserDetailPage EditInputUsername(string email)
        {
            EditUsername_txt.SetText(email);
            return this;
        }

        public UserDetailPage EditInputEmail(string email)
        {
            EditEmail_txt.SetText(email);
            return this;
        }

        public UserDetailPage SelectRole(string role)
        {
            Role_ddl.SelectItem(role);
            return this;
        }

        public UserDetailPage InputFirstName(string firstname)
        {
            Firstname_txt.SetText(firstname);
            return this;
        }

        public UserDetailPage InputLastName(string lastname)
        {
            Lastname_txt.SetText(lastname);
            return this;
        }

        public void  Save()
        {
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_user1_LoadingPanel1ctl00_CPH_Content_user1_lbLoadingAnimation']/div[1]");
            PageLoad();
        }
        public void UpdateSave()
        {
            UpdateSave_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_user1_LoadingPanel1ctl00_CPH_Content_user1_lbLoadingAnimation']/div[1]");
            PageLoad();
        }

        public void CreateNewUsername(UserData userdata)
        {
            InputUsername(userdata.Username).InputEmail(userdata.Email).
            SelectRole(userdata.Role).InputFirstName(userdata.firstname).InputLastName(userdata.lastname);
            Save();

        }
    }
}
