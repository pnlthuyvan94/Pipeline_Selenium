using System;

namespace Pipeline.Testing.Pages.UserMenu.User.AddUser
{
    public partial class AddUserDetailPage
    {
        public bool IsModalDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (Email_txt == null || Email_txt.IsDisplayed() == false)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("The \"Create Users\" modal is not displayed.");
                    }
                }
                return (Email_txt.GetText() == "Users");
            }
        }

        public bool IsSaveHouseSuccessful(string username)
        {
            System.Threading.Thread.Sleep(1000);
            string expectedUserName = $"{username}";
            return SubHeadTitle().Equals(expectedUserName);
        }


    }
}
