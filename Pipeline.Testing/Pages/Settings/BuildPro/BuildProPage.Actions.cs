namespace Pipeline.Testing.Pages.Settings.BuildPro
{
    public partial class BuildProPage
    {
        public BuildProPage UpdateUserName(string username)
        {
            UserName_Txt.SetText(username);
            return this;
        }

        public BuildProPage UpdatePassword(string password)
        {
            Password_Txt.SetText(password);
            return this;
        }

        public BuildProPage UpdateRootURI(string data)
        {
            RootUri_Txt.SetText(data);
            return this;
        }

        public BuildProPage UpdateCompany(string data)
        {
            Company_Txt.SetText(data);
            return this;
        }

        public BuildProPage UpdateDivision(string data)
        {
            Division_Txt.SetText(data);
            return this;
        }

        public BuildProPage UpdateStatus(bool data)
        {
            if (data)
                Running_Btn.JavaScriptClick(false);
            else
                Paused_Btn.JavaScriptClick(false);
            return this;
        }

        public void TestConnection()
        {
            TestConnection_Btn.Click();
            JQueryLoad();
        }

        public void Save()
        {
            Save_Btn.Click();
            JQueryLoad();
        }

        public void SignInToBuildProIfNeeded(string username, string password)
        {
            if (BP_Username_Txt.WaitUntilExist(5))
            {
                BP_Username_Txt.SetText(username);
                BP_Password_Txt.SetText(password);
                BP_Login_Btn.Click();
                PageLoad();
                if (ForceSignIn_chk.WaitForElementIsVisible(5))
                {
                    BP_Username_Txt.SetText(username);
                    BP_Password_Txt.SetText(password);
                    ForceSignIn_chk.Check();
                    BP_Login_Btn.Click();
                    PageLoad();
                }
            }
        }

        public void SearchVendor()
        {
            if (BP_ListItem_ddl.WaitForElementIsVisible(5))
            {
                BP_ListItem_ddl.SelectItemByValue("100");
                BP_SearchVendor_btn.JavaScriptClick();
                PageLoad();
            }
        }

        public BuildProPage FilterAndShowTask(string jobNumber)
        {
            if (!JobName_ddl.IsExisted(false) || !JobName_ddl.IsDisplayed(false))
            {
                ExpandJobFilter.Click();
                System.Threading.Thread.Sleep(1000);
            }
            JobName_ddl.SelectItem(jobNumber, true);
            ShowTask_Btn.Click();
            PageLoad();
            return this;
        }

        public bool GetRunningStatus()
        {
            if (Running_Btn != null )
                return Running_Btn.GetAttribute("checked") != null;
            return false;
        }
    }
}
