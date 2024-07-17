using System.Linq;

namespace Pipeline.Testing.Pages.UserMenu.User.AddUser
{
    public partial class AddUserDetailPage
    {
        public AddUserDetailPage EnterUserName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                UserName_txt.SetText(name);
            return this;
        }

        public AddUserDetailPage EnterPassword(string pass)
        {
            if (!string.IsNullOrEmpty(pass))
                Password_txt.SetText(pass);
            return this;
        }

        public AddUserDetailPage EnterConfirmPass(string confirmPass)
        {
            if (!string.IsNullOrEmpty(confirmPass))
                ConfirmPass_txt.SetAttribute("value", confirmPass);
            return this;
        }

        public AddUserDetailPage EnterEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
                Email_txt.SetText(email);
            return this;
        }

        public AddUserDetailPage EnterRole(string role)
        {
            string[] cars = { "customer", "prospect", "vendor", "admin" };
            if (!string.IsNullOrEmpty(role) && cars.Contains(role.ToLower()))
                Role_ddl.SelectItem(role, true);
            else
                Role_ddl.SelectItem("Admin", true);
            return this;
        }

        public AddUserDetailPage EnterActive(string active)
        {
            if (!string.IsNullOrEmpty(active) && active.ToUpper().Equals("TRUE"))
                Active_cbk.SetCheck(true);
            else
                Active_cbk.SetCheck(false);
            return this;
        }

        public AddUserDetailPage EnterFistName(string firstName)
        {
            if (!string.IsNullOrEmpty(firstName))
                FirstName_txt.SetText(firstName);
            return this;
        }

        public AddUserDetailPage EnterLastName(string lastName)
        {
            if (!string.IsNullOrEmpty(lastName))
                LastName_txt.SetText(lastName);
            return this;
        }

        public AddUserDetailPage EnterPhone(string phone)
        {
            if (!string.IsNullOrEmpty(phone))
                Phone_txt.SetText(phone);
            return this;
        }

        public AddUserDetailPage EnterExt(string ext)
        {
            if (!string.IsNullOrEmpty(ext))
                Ext_txt.SetText(ext);
            return this;
        }

        public AddUserDetailPage EnterCell(string cell)
        {
            if (!string.IsNullOrEmpty(cell))
                Cell_txt.SetText(cell);
            return this;
        }

        public AddUserDetailPage EnterFax(string fax)
        {
            if (!string.IsNullOrEmpty(fax))
                Fax_txt.SetText(fax);
            return this;
        }

        public AddUserDetailPage EnterAddress1(string address1)
        {
            if (!string.IsNullOrEmpty(address1))
                Address1_txt.SetText(address1);
            return this;
        }

        public AddUserDetailPage EnterAddress2(string address2)
        {
            if (!string.IsNullOrEmpty(address2))
                Address2_txt.SetText(address2);
            return this;
        }

        public AddUserDetailPage EnterCity(string city)
        {
            if (!string.IsNullOrEmpty(city))
                City_txt.SetText(city);
            return this;
        }

        public AddUserDetailPage EnterState(string state)
        {
            if (!string.IsNullOrEmpty(state))
                State_txt.SetText(state);
            return this;
        }

        public AddUserDetailPage EnterZip(string zip)
        {
            if (!string.IsNullOrEmpty(zip))
                Zip_txt.SetText(zip);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_user1_LoadingPanel1ctl00_CPH_Content_user1_lbLoadingAnimation']/div[1]");
            PageLoad();
        }


        public void CreateNewUser(UserData data)
        {
            EnterUserName(data.UserName)//.EnterPassword(data.Password)//.EnterConfirmPass(data.ConfirmPass)
                 .EnterEmail(data.Email).EnterRole(data.Role).EnterActive(data.Active)
                 .EnterFistName(data.FirstName).EnterLastName(data.LastName).EnterPhone(data.Phone)
                 .EnterExt(data.Ext).EnterCell(data.Cell).EnterFax(data.Fax)
                 .EnterAddress1(data.Address1).EnterAddress2(data.Address2).EnterCity(data.City)
                 .EnterState(data.State).EnterZip(data.Zip);
        }
    }
}
