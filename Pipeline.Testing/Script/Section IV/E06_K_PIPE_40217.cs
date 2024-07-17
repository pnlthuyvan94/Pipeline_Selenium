using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeDetail;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeUser;
using Pipeline.Testing.Pages.Settings.Users;
using Pipeline.Testing.Pages.Settings.Users.UsersDetail;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_IV
{
    public class E06_K_PIPE_40217 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private const string NewBuildingTradeName = "RT_QA_New_BuildingTrade_E06K";
        private const string NewBuildingTradeCode = "E06K";

        private const string NewTestUser = "RT_QA_NewUser_E06K";
        UserData userdata;

       [SetUp]
        public void Setup()
        {
            TradesData _trade = new TradesData()
            {
                Code = NewBuildingTradeCode,
                TradeName = NewBuildingTradeName,
                TradeDescription = NewBuildingTradeName,
                Vendor = "",
                BuildingPhases = "",
                SchedulingTasks = ""
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Create Trades test data.</b></font>");

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, _trade.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName) is false)
            {
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                TradesPage.Instance.CreateTrade(_trade);
            }

            userdata = new UserData()
            {
                Username = NewTestUser,
                Email = NewTestUser + "@strongtie.com",
                Role = "Admin",
                firstname = NewTestUser,
                lastname = NewTestUser
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 0.2: Create User test data.</font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Users);
            UserPage.Instance.FilterItemInGrid("Username", GridFilterOperator.Contains, NewTestUser);
            System.Threading.Thread.Sleep(2000);
            if (UserPage.Instance.IsItemInGrid("Username", NewTestUser) is false)
            {
                UserPage.Instance.ClickAddToUserIcon();
                UserDetailPage.Instance.CreateNewUsername(userdata);                
            }
        }

        [Test]
        public void E06K_Purchasing_Trades_Builder_Users()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.1: Go to Trades Detail page.</font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, NewBuildingTradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", NewBuildingTradeName);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.2: Go to Trades Builder Vendor page.</font>");
                bool isBuilderVendorEnabled = true;
                try
                {
                    TradeDetailPage.Instance.LeftMenuNavigation("Builder Vendor", true);
                }
                catch(Exception ex)
                {
                    isBuilderVendorEnabled = false;
                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Builder Vendor is not enabled in Carbonite.</font>");
                }
               
                if(isBuilderVendorEnabled)
                {
                    TradeBuilderUserPage.Instance.FilterItemInGrid("First Name", GridFilterOperator.EqualTo, NewTestUser);
                    if (TradeBuilderUserPage.Instance.IsItemInGrid("First Name", NewTestUser) is true)
                    {
                        ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.2.1: Delete user if already exists on the grid.</font>");
                        TradeBuilderUserPage.Instance.DeleteItemInGrid("First Name", NewTestUser);
                    }

                    TradeBuilderUserPage.Instance.FilterItemInGrid("First Name", GridFilterOperator.EqualTo, NewTestUser);
                    if (TradeBuilderUserPage.Instance.IsItemInGrid("First Name", NewTestUser) is false)
                    {
                        ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.3: Add new user to trade.</font>");
                        TradeBuilderUserPage.Instance.ShowAddUserToTradeModal();
                        if (TradeBuilderUserPage.Instance.AddUserToTradeModal.IsModalDisplayed)
                        {
                            TradeBuilderUserPage.Instance.AddUserToTradeModal.SelectUser(userdata.firstname + " " + userdata.lastname + " - " + userdata.Email);
                            TradeBuilderUserPage.Instance.AddUserToTradeModal.Save();
                            string actualMessage = TradeBuilderUserPage.Instance.GetLastestToastMessage();
                            string expectedMessage = "Builder vendor was added successfully.";
                            if (actualMessage == expectedMessage)
                            {
                                ExtentReportsHelper.LogPass(actualMessage);
                            }
                            else
                            {
                                TradeBuilderUserPage.Instance.FilterItemInGrid("First Name", GridFilterOperator.EqualTo, NewTestUser);
                                if (TradeBuilderUserPage.Instance.IsItemInGrid("First Name", NewTestUser) is true)
                                {
                                    ExtentReportsHelper.LogPass(expectedMessage);
                                }
                                else
                                {
                                    ExtentReportsHelper.LogFail("Failed to add Builder Vendor.");
                                }
                            }
                        }
                    }
                }
            }
        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1 Delete Building Trades.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, NewBuildingTradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName) is true)
            {
                System.Threading.Thread.Sleep(2000);
                TradesPage.Instance.DeleteItemInGrid("Trade", NewBuildingTradeName);
                System.Threading.Thread.Sleep(2000);
                CommonHelper.RefreshPage();
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.2: Delete User test data.</font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Users);
            UserPage.Instance.FilterItemInGrid("Username", GridFilterOperator.Contains, NewTestUser);
            System.Threading.Thread.Sleep(2000);
            if (UserPage.Instance.IsItemInGrid("Username", NewTestUser) is true)
            {
                UserPage.Instance.DeleteItemInGrid("Username", NewTestUser);
            }
        }
    }
}
