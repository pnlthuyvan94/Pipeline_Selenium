using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Divisions;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionCommunity;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionDetail;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeDetail;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor;
using Pipeline.Testing.Pages.Purchasing.Trades.VendorAssignments;
using Pipeline.Testing.Pages.UserMenu.User;

namespace Pipeline.Testing.Script.Section_IV
{
    public class E06_H_PIPE_34872 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private const string NewBuildingTradeName = "RT_QA_New_BuildingTradeVendor_E06H";
        private const string NewBuildingTradeCode = "E06H_V";

        private const string NewBuildingTradeName2 = "RT_QA_New_BuildingTradeBuilderVendor_E06H";
        private const string NewBuildingTradeCode2 = "E06H_BV";

        private VendorData newVendor;
        private const string NewVendorName = "RT_QA_New_Vendor_E06H";
        private const string NewVendorCode = "E06H";

        private DivisionData newDivision;
        private const string newDivisionName = "RT_QA_New_Division_E06H";

        private CommunityData newCommunity;
        private const string newCommunityName = "RT_QA_New_Community_E06H";

        private const string editVendorExpectedMessage = "Vendor Assignments have been updated successfully.";

        private UserData userdata;

        private const bool IsBuilderVendorEnabled = false; //set to TRUE when the Builder Vendor Feature is enabled in Carbonite

        [SetUp]
        public void Setup()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Setup test data.</b></font>");
            newVendor = new VendorData()
            {
                Name = NewVendorName,
                Code = NewVendorCode
            };           

            newDivision = new DivisionData()
            {
                Name = newDivisionName
            };

            newCommunity = new CommunityData()
            {
                Name = newCommunityName
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Add vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is false)
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendor);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Add new community.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCommunityName);
            System.Threading.Thread.Sleep(2000);
            if (CommunityPage.Instance.IsItemInGrid("Name", newCommunityName) is false)
            {
                CommunityPage.Instance.CreateCommunity(newCommunity);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4: Add new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivisionName) is false)
            {
                DivisionPage.Instance.CreateDivision(newDivision);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5: Add new community to new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivisionName) is true)
            {
                DivisionPage.Instance.SelectItemInGrid("Division", newDivisionName);
                DivisionDetailPage.Instance.LeftMenuNavigation("Communities", true);

                DivisionCommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCommunityName);
                System.Threading.Thread.Sleep(2000);
                if (DivisionCommunityPage.Instance.IsItemInGrid("Name", newCommunityName) is false)
                {
                    string[] communities = { newCommunityName };
                    DivisionCommunityPage.Instance.OpenDivisionCommunityModal();
                    DivisionCommunityPage.Instance.DivisionCommunityModal.SelectDivisionCommunity(communities);
                    DivisionCommunityPage.Instance.DivisionCommunityModal.Save();
                }
            }

            if (IsBuilderVendorEnabled)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.6: Add test Users.</b></font>");
                userdata = new UserData()
                {
                    UserName = "RT_QA_New_User_E06H",
                    Password = "123456",
                    ConfirmPass = "123456",
                    Email = "RT1@gmail.com",
                    Role = "Prospect",
                    Active = "TRUE",
                    FirstName = "RTQA",
                    LastName = "E06H",
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
                UserPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Users);
                UserPage.Instance.ClickAddUserButton();
                UserPage.Instance.CreateNewUser(userdata);
            }
            
        }

        [Test]
        public void E06_H_Purchasing_Trades_Vendors_Assignment()
        {
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Verify if Vendor Assignments button is visible in Trades Default page.</b></font>");
            if (TradesPage.Instance.IsVendorAssignmentBtnDisplayed is true)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Vendor Assignments button is displayed.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Vendor Assignments button is not displayed.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0.1: Verify if company vendor column is NOT visible in Trades Default grid.</b></font>");
            if (TradesPage.Instance.IsColumnFoundInGrid("Company Vendor") is false)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Company Vendor column is NOT found in the grid.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Company Vendor column is found in the grid.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0.2: Verify if company vendor field is NOT visible in Add Trades Modal.</b></font>");
            TradesPage.Instance.ClickAddToOpenCreateTradeModal();
            if(TradesPage.Instance.AddTradeModal.IsCompanyVendorDisplayed() is false)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Company Vendor field is NOT found in the Add Trade Modal.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Company Vendor column is found in the Add Trade Modal</b></font>");
            TradesPage.Instance.AddTradeModal.Close();



            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Add New Trade qualified as Vendor.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, NewBuildingTradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName) is false)
            {
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();

                TradesData _trade = new TradesData()
                {
                    Code = NewBuildingTradeCode,
                    TradeName = NewBuildingTradeName,
                    TradeDescription = NewBuildingTradeName,
                    IsBuilderVendor = false,
                    Vendor = NewVendorName,
                    BuildingPhases = "",
                    SchedulingTasks = ""
                };

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Populate trade details in the modal.</b></font>");
                TradesPage.Instance.CreateTrade(_trade);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Check if new trade is displayed on the grid.</b></font>");
                TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, _trade.TradeName);
                System.Threading.Thread.Sleep(2000);
                if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName) is true)
                {
                    ExtentReportsHelper.LogPass(null, $"Building Trade " + NewBuildingTradeName + " qualified as Vendor is displayed on the grid.");
                }
                else
                {
                    ExtentReportsHelper.LogWarning($"<font color = 'red'>Building Trade  {NewBuildingTradeName} is not displayed on the grid.</font>");
                }
            }

            if (IsBuilderVendorEnabled)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Add New Trade qualified as Builder Vendor.</b></font>");
                TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
                TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, NewBuildingTradeName2);
                System.Threading.Thread.Sleep(2000);
                if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName2) is false)
                {
                    TradesPage.Instance.ClickAddToOpenCreateTradeModal();

                    TradesData _trade = new TradesData()
                    {
                        Code = NewBuildingTradeCode2,
                        TradeName = NewBuildingTradeName2,
                        TradeDescription = NewBuildingTradeName2,
                        IsBuilderVendor = true,
                        BuilderVendor = userdata.FirstName + " " + userdata.LastName + " - " + userdata.Email,
                        SchedulingTasks = ""
                    };

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2: Populate trade details in the modal.</b></font>");
                    TradesPage.Instance.CreateTrade(_trade);

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.3: Check if new trade is displayed on the grid.</b></font>");
                    TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, NewBuildingTradeName2);
                    System.Threading.Thread.Sleep(2000);
                    if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName2) is true)
                    {
                        ExtentReportsHelper.LogPass(null, $"Building Trade " + NewBuildingTradeName2 + " qualified as Builder Vendor is displayed on the grid.");
                    }
                    else
                    {
                        ExtentReportsHelper.LogWarning($"<font color = 'red'>Building Trade  {NewBuildingTradeName2} is not displayed on the grid.</font>");
                    }
                }

            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1: Go to Trades default page and click on Vendor Assignments button.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.ClickVendorAssignments();
            System.Threading.Thread.Sleep(2000);
          
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2: Select new Division " + newDivisionName + ".</b></font>");
            VendorAssignmentsPage.Instance.SelectDivision(newDivisionName, 1);
            System.Threading.Thread.Sleep(2000);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.3: Select new Community " + newCommunityName + ".</b></font>");
            string[] communities = { newCommunityName };
            VendorAssignmentsPage.Instance.SelectCommunities(communities);
            VendorAssignmentsPage.Instance.WaitGridLoad();
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.4: Load Vendors.</b></font>");
            VendorAssignmentsPage.Instance.ClickLoadVendors();
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();
            VendorAssignmentsPage.Instance.ClickLoadVendors();
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();

            if (VendorAssignmentsPage.Instance.IsColumnFoundInGrid("Company Vendor"))
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Company Vendor column is displayed in the Vendor Assignments page.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Company Vendor column is not displayed in the Vendor Assignments page.</b></font>");


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.0: Filter trades column to show new Trade " + NewBuildingTradeName + " qualified as Vendor.</b></font>");
            VendorAssignmentsPage.Instance.FilterItemInGrid("Trades", GridFilterOperator.EqualTo, NewBuildingTradeName);
            VendorAssignmentsPage.Instance.WaitGridLoad();
            System.Threading.Thread.Sleep(5000);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.1: Update the company vendor to " + NewVendorName + ".</b></font>");
            string editCompanyVendorMsg = VendorAssignmentsPage.Instance.EditCompanyVendor(NewVendorName);
            VendorAssignmentsPage.Instance.WaitGridLoad();
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();
            if (editCompanyVendorMsg == editVendorExpectedMessage)
                ExtentReportsHelper.LogPass(editCompanyVendorMsg);
            else
                ExtentReportsHelper.LogFail(editCompanyVendorMsg);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2: Update the division vendor to " + NewVendorName + ".</b></font>");
            VendorAssignmentsPage.Instance.FilterItemInGrid("Trades", GridFilterOperator.EqualTo, NewBuildingTradeName);
            VendorAssignmentsPage.Instance.WaitGridLoad();
            System.Threading.Thread.Sleep(5000);
            string editDivisionVendorMsg = VendorAssignmentsPage.Instance.EditDivisionVendor(newDivisionName, NewVendorName);
            VendorAssignmentsPage.Instance.WaitGridLoad();
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();
            if (editDivisionVendorMsg == editVendorExpectedMessage)
                ExtentReportsHelper.LogPass(editDivisionVendorMsg);
            else
                ExtentReportsHelper.LogFail(editDivisionVendorMsg);

            VendorAssignmentsPage.Instance.FilterItemInGrid("Trades", GridFilterOperator.EqualTo, NewBuildingTradeName);
            VendorAssignmentsPage.Instance.WaitGridLoad();
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.3: Update the community vendor to " + NewVendorName + ".</b></font>");
            string editCommunityVendorMsg = VendorAssignmentsPage.Instance.EditCommunityVendor(newCommunityName, NewVendorName);
            VendorAssignmentsPage.Instance.WaitGridLoad();
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();
            if (editCommunityVendorMsg == editVendorExpectedMessage)
                ExtentReportsHelper.LogPass(editCommunityVendorMsg);
            else
                ExtentReportsHelper.LogFail(editCommunityVendorMsg);

            if(IsBuilderVendorEnabled)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.0: Filter trades column to show new Trade " + NewBuildingTradeName2 + " qualified as Builder Vendor.</b></font>");
                VendorAssignmentsPage.Instance.FilterItemInGrid("Trades", GridFilterOperator.EqualTo, NewBuildingTradeName2);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.1: Update the company vendor to " + userdata.UserName + ".</b></font>");
                editCompanyVendorMsg = VendorAssignmentsPage.Instance.EditCompanyVendor(userdata.FirstName + " " + userdata.LastName);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
                if (editCompanyVendorMsg == editVendorExpectedMessage)
                    ExtentReportsHelper.LogPass(editCompanyVendorMsg);
                else
                    ExtentReportsHelper.LogFail(editCompanyVendorMsg);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.2: Update the division vendor to " + userdata.UserName + ".</b></font>");
                VendorAssignmentsPage.Instance.FilterItemInGrid("Trades", GridFilterOperator.EqualTo, NewBuildingTradeName2);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                editDivisionVendorMsg = VendorAssignmentsPage.Instance.EditDivisionVendor(newDivisionName, userdata.FirstName + " " + userdata.LastName);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
                if (editDivisionVendorMsg == editVendorExpectedMessage)
                    ExtentReportsHelper.LogPass(editDivisionVendorMsg);
                else
                    ExtentReportsHelper.LogFail(editDivisionVendorMsg);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.3: Update the community vendor to " + userdata.UserName + ".</b></font>");
                VendorAssignmentsPage.Instance.FilterItemInGrid("Trades", GridFilterOperator.EqualTo, NewBuildingTradeName2);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                editCommunityVendorMsg = VendorAssignmentsPage.Instance.EditCommunityVendor(newCommunityName, userdata.FirstName + " " + userdata.LastName);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
                if (editCommunityVendorMsg == editVendorExpectedMessage)
                    ExtentReportsHelper.LogPass(editCommunityVendorMsg);
                else
                    ExtentReportsHelper.LogFail(editCommunityVendorMsg);
            }

            VendorAssignmentsPage.Instance.BackToTradesPage();
        }
        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0 Tear down test data.</b></font>");           
          
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1 Delete Building Trades.</b></font>");
            DeleteTradeRelations(NewBuildingTradeName);
            DeleteTrade(NewBuildingTradeName);
            DeleteTrade(NewBuildingTradeName2);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2 Delete Community.</b></font>");
            DeleteCommunity();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.3 Delete Division.</b></font>");
            DeleteDivision();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.4 Delete Vendor.</b></font>");
            DeleteVendor();
            if(IsBuilderVendorEnabled)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.5 Delete User.</b></font>");
                DeleteUser();
            }
            
        }

        private void DeleteTradeRelations(string tradeName)
        {            
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeName);
            CommonHelper.RefreshPage();
            CommonHelper.CaptureScreen();
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", tradeName);
                TradeVendorPage.Instance.LeftMenuNavigation("Vendors", true);
                TradeVendorPage.Instance.FilterItemInGrid("Vendor Name", GridFilterOperator.EqualTo, NewVendorName);
                if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", NewVendorName))
                {
                    TradeVendorPage.Instance.DeleteItemInGrid("Vendor Name", NewVendorName);
                }
            }
        }

        private void DeleteVendor()
        {
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
            {
                VendorPage.Instance.DeleteItemInGrid("Name", NewVendorName);
            }
        }

        private void DeleteTrade(string tradeName)
        {
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeName) is true)
            {
                System.Threading.Thread.Sleep(2000);
                TradesPage.Instance.DeleteItemInGrid("Trade", tradeName);
                System.Threading.Thread.Sleep(2000);
                CommonHelper.RefreshPage();
            }
        }

        private void DeleteDivision()
        {
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivisionName) is true)
            {
                System.Threading.Thread.Sleep(2000);
                DivisionPage.Instance.DeleteItemInGrid("Division", newDivisionName);
                System.Threading.Thread.Sleep(2000);
                CommonHelper.RefreshPage();
            }
        }

        private void DeleteCommunity()
        {
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCommunityName);
            System.Threading.Thread.Sleep(2000);
            if (CommunityPage.Instance.IsItemInGrid("Name", newCommunityName) is true)
            {
                System.Threading.Thread.Sleep(2000);
                CommunityPage.Instance.DeleteItemInGrid("Name", newCommunityName);
                System.Threading.Thread.Sleep(2000);
                CommonHelper.RefreshPage();
            }
        }
        private void DeleteUser()
        {
            UserPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Users);
            UserPage.Instance.FilterItemInGrid("Username", GridFilterOperator.EqualTo, userdata.UserName);
            if (UserPage.Instance.IsItemInGrid("Username", userdata.UserName))
            {
                UserPage.Instance.DeleteItemInGrid("Username", userdata.UserName);
            }
        }
    }
}
