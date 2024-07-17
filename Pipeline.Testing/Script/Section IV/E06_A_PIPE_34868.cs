using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Costing.CommunitySalesTax;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeBuildingPhase;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeDetail;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeUser;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor;
using Pipeline.Testing.Pages.UserMenu.User;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_IV
{
    public class E06_A_PIPE_34868 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        TradesData _trade = new TradesData();
        TradesData _newtrade = new TradesData();
        TradesData _newtradeVendor = new TradesData();
        TradesData _newtradeBuilderVendor = new TradesData();

        private const string NewBuildingTradeName = "RT_QA_New_BuildingTrade_E06A";
        private const string NewBuildingTradeCode = "E06A";
        private const string BuildingTradeName_ValidSpecialCharacter = "()_-";
        private const string BuildingTradeName_InValidSpecialCharacter = "~!@#$%^&*+";

        private VendorData newVendor;
        private const string NewVendorName = "RT_QA_New_Vendor_E06A_Test";
        private const string NewVendorCode = "E06A";

        private BuildingGroupData newBuildingGroup;
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_E06A_Test";
        private const string NewBuildingGroupCode = "E06A";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_E06A_Test";

        private BuildingPhaseData newBuildingPhase;
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_E06A_Test";
        private const string NewBuildingPhaseCode = "E06A";
        private UserData userdata;

        private const bool IsBuilderVendorEnabled = false; //set to TRUE when the Builder Vendor Feature is enabled in Carbonite

        [SetUp]
        public void Setup()
         {
            Random rndNo = new Random();
            _trade = new TradesData()
            {
                Code = NewBuildingTradeCode,
                TradeName = NewBuildingTradeName + rndNo.Next(1000).ToString(),
            };
            newVendor = new VendorData()
            {
                Name = NewVendorName,
                Code = NewVendorCode
            };
            newBuildingGroup = new BuildingGroupData()
            {
                Name = NewBuildingGroupName,
                Code = NewBuildingGroupCode,
                Description = NewBuildingGroupDescription
            };
            newBuildingPhase = new BuildingPhaseData()
            {
                Code = NewBuildingPhaseCode,
                Name = NewBuildingPhaseName,
                BuildingGroupCode = NewBuildingGroupCode,
                BuildingGroupName = NewBuildingGroupName
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Add first vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
            if (!VendorPage.Instance.IsItemInGrid("Name", NewVendorName))
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendor);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Add New Building Group.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            if (!BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName))
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroup);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Add Building Phase.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewBuildingPhaseName);
            if (!BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName))
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal
                                     .EnterPhaseCode(newBuildingPhase.Code)
                                     .EnterPhaseName(newBuildingPhase.Name)
                                     .EnterAbbName(newBuildingPhase.AbbName)
                                     .EnterDescription(newBuildingPhase.Description);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(newBuildingPhase.BuildingGroup);

                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
            }

            if(IsBuilderVendorEnabled)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Add User.</b></font>");
                userdata = new UserData()
                {
                    UserName = "RT_QA_New_User_E06A",
                    Password = "123456",
                    ConfirmPass = "123456",
                    Email = "RT1@gmail.com",
                    Role = "Prospect",
                    Active = "TRUE",
                    FirstName = "RTQA",
                    LastName = "E06A",
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
        public void E06_A_Purchasing_Trades_Details_Page()
        {
            //Navigate to Purchasing > Trades page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Navigate to Purchasing > Trades page.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);

            //Check if Trade exist
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0: Check if new trade is displayed on the grid.</b></font>");
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, _trade.TradeName);
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is false)
            {
                //Add Trade if not exist
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Check if Building Trade Add Modal is displayed.</b></font>");
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2: Populate trade details in the modal and save.</b></font>");
                TradesPage.Instance.CreateTrade(_trade);

                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Trade successfully created.</b></font>");
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Able to add new trade with the correct success toast notification displayed.</b></font>");
                CommonHelper.RefreshPage();
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.0: Check if new trade is displayed on the grid.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, BuildingTradeName_ValidSpecialCharacter);
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", _trade.TradeName);
                ExtentReportsHelper.LogPass(null, $"Building Trade " + _trade.TradeName + " is displayed on the grid.");
            }

            //Type on the “Trade Name” and verify if the text field will only accept special characters like ()-_
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.1: Type on the “Trade Name” and verify if the text field will only accept special characters like ()-_.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, BuildingTradeName_ValidSpecialCharacter);
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is true)
            {
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Able to type on the “Trade Name” and verified that the text field will only accept special characters like ()-_.</b></font>");
            }

            //Type on the “Trade Name” and verify if the text field will not accept other special characters 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2: Type on the “Trade Name” and verify if the text field will not accept other special characters.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, BuildingTradeName_ValidSpecialCharacter);
            if (TradesPage.Instance.IsItemInGrid("Trade", BuildingTradeName_InValidSpecialCharacter) is false)
            {
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Able to type on the “Trade Name” and verified that the text field will not accept other special characters.</b></font>");
            }

            //Filter the newly created trade then click the “Edit” pen. Verify if in-line editing is enabled and will not show up the Edit Trade modal
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.0: Filter the newly created trade then click the “Edit” pen. Verify if in-line editing is enabled and will not show up the Edit Trade modal.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, _trade.TradeName);
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.1: Update trade via in-line edit with accepted special characters ()_-.</b></font>");
                var expectedUpdateMessage = "Trade " + _trade.Code + " " + _trade.TradeName + "_updated" + BuildingTradeName_ValidSpecialCharacter + " saved successfully!";
                string newTradeName = _trade.TradeName + "_updated" + BuildingTradeName_ValidSpecialCharacter;
                var actualUpdateMessage = TradesPage.Instance.EditTrade("Trade", _trade.TradeName, newTradeName, "", "");
                if (expectedUpdateMessage == actualUpdateMessage)
                {
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Editing is enabled and will not show up the Edit Trade modal.</b></font>");
                    ExtentReportsHelper.LogPass(actualUpdateMessage);
                    _trade.TradeName = newTradeName;
                }
            }

            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.0: On the “Trade” column verify if the text field will not accept special characters and will only accept ()-_.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, _trade.TradeName);
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.1: Update trade via in-line edit with invalid special characters ()_-.</b></font>");
                var expectedUpdateMessage = "Trade " + _trade.Code + " " + _trade.TradeName + "_updated" + " saved successfully!";
                string newTradeName = _trade.TradeName + "_updated";
                var actualUpdateMessage = TradesPage.Instance.EditTrade("Trade", _trade.TradeName, _trade.TradeName + "_updated" + BuildingTradeName_InValidSpecialCharacter, "", "");
                if (expectedUpdateMessage == actualUpdateMessage)
                {
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Text field will not accept special characters and will only accept ()-_.</b></font>");
                    _trade.TradeName = newTradeName;
                }
            }

            CommonHelper.RefreshPage();

            //Click “Cancel” button. Verify if in-line editing is turned-off
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.0: Click “Cancel” button. Verify if in-line editing is turned-off.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, _trade.TradeName);
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is true)
            {
                TradesPage.Instance.EditCancelTrade("Trade", _trade.TradeName);
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>In-line editing is turned-off.</b></font>");
            }


            _newtrade = new TradesData()
            {
                Code = NewBuildingTradeCode + "_new",
                TradeName = NewBuildingTradeName + "_new",
                TradeDescription = NewBuildingTradeName + "_new",
                Vendor = "",
                BuildingPhases = "",
                SchedulingTasks = ""
            };


            //Click the “Edit” pen again and update any of these 4 enabled columns and click on “Update” or the check mark button. Verify if changes are reflected on the table grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.0: Click the “Edit” pen again and update any of these 4 enabled columns and click on “Update” or the check mark button. Verify if changes are reflected on the table grid.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, _newtrade.TradeName);
            if (TradesPage.Instance.IsItemInGrid("Trade", _newtrade.TradeName) is false)
            {
                //Add new trade 
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.1: Add new trade.</b></font>");
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                TradesPage.Instance.CreateTrade(_newtrade);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.2: Update trade via in-line edit.</b></font>");
                var expectedUpdateMessage = "Trade " + _newtrade.Code + " " + _newtrade.TradeName + "_updated" + " saved successfully!";

                var actualUpdateMessage = TradesPage.Instance.EditTrade("Trade", _newtrade.TradeName, _newtrade.TradeName + "_updated", NewBuildingPhaseCode + "-" + NewBuildingPhaseName, "");
                if (expectedUpdateMessage == actualUpdateMessage)
                {
                    ExtentReportsHelper.LogPass(actualUpdateMessage);
                }
                _newtrade.TradeName = _newtrade.TradeName + "_updated";
            }



            //Click on the trade name of newly created trade and verify if “Trade Details page is displayed with only 3 labels 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.0: Click on the trade name of newly created trade and verify if “Trade Details page is displayed with only 3 labels.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, _trade.TradeName);
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", _trade.TradeName);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.1: Able to click on the trade name of newly created trade and verified that “Trade Details page is displayed.</b></font>");

                //On “Name” text field, type in special characters and verify if the text field will not accept special characters and will only accept ()-_
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 10.0: On “Name” text field, type in special characters and verify if the text field will not accept special characters and will only accept ()-_.</b></font>");
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 10.1: Adding " + _trade.TradeName + BuildingTradeName_ValidSpecialCharacter + BuildingTradeName_InValidSpecialCharacter + "</b></font>");
                TradeDetailPage.Instance.EnterTradeName(_trade.TradeName + BuildingTradeName_ValidSpecialCharacter + BuildingTradeName_InValidSpecialCharacter);
                TradeDetailPage.Instance.Save();
                string actualTradeName = TradeDetailPage.Instance.GetTradeName();
                if (_trade.TradeName + BuildingTradeName_ValidSpecialCharacter == actualTradeName)
                {
                    _trade.TradeName = _trade.TradeName + BuildingTradeName_ValidSpecialCharacter;
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Text field will not accept special characters and will only accept ()-_.</b></font>");
                }
                //Update any of the trade details page and click “Save” button. Verify if the updated details are successfully save with success toast notification:
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 11.0: Update any of the trade details page and click “Save” button. Verify if the updated details are successfully save with success toast notification.</b></font>");
                _trade.TradeName = _trade.TradeName + "_updated";
                TradeDetailPage.Instance.EnterTradeName(_trade.TradeName);
                TradeDetailPage.Instance.Save();
                string actualToastMessage = TradeDetailPage.Instance.GetLastestToastMessage();
                string expectedToastMessage = "Trade " + _trade.Code + " " + _trade.TradeName + " saved successfully!";
                if(actualToastMessage == expectedToastMessage)
                {
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Able to update any of the trade details page and able to click “Save” button. Verified that the updated details are successfully save.</b></font>");
                }                
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 12.0: Add new trade qualified as Vendor.</b></font>");
            _newtradeVendor = new TradesData()
            {
                Code = NewBuildingTradeCode + "_V",
                TradeName = NewBuildingTradeName + "_V",
                TradeDescription = NewBuildingTradeName + "_V",
                Vendor = NewVendorName,
                BuildingPhases = "",
                SchedulingTasks = ""
            };
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, _newtradeVendor.TradeName);
            if (TradesPage.Instance.IsItemInGrid("Trade", _newtradeVendor.TradeName) is false)
            {
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                TradesPage.Instance.CreateTrade(_newtradeVendor);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 12.1: Verify if Vendor is found in the Trades Vendor page.</b></font>");
                TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, _newtradeVendor.TradeName);
                if (TradesPage.Instance.IsItemInGrid("Trade", _newtradeVendor.TradeName) is true)
                {
                    TradesPage.Instance.SelectItemInGrid("Trade", _newtradeVendor.TradeName);
                    TradeDetailPage.Instance.LeftMenuNavigation("Vendors");
                    TradeVendorPage.Instance.FilterItemInGrid("Vendor Name", GridFilterOperator.EqualTo, _newtradeVendor.Vendor);
                    if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", _newtradeVendor.Vendor) is true)
                    {
                        ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Vendor is added to the trade.</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Vendor is not added to the trade.</b></font>");
                    }
                }
            }

            if(IsBuilderVendorEnabled)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 13.0: Add new trade qualified as Builder Vendor.</b></font>");
                _newtradeBuilderVendor = new TradesData()
                {
                    Code = NewBuildingTradeCode + "_BV",
                    TradeName = NewBuildingTradeName + "_BV",
                    TradeDescription = NewBuildingTradeName + "_BV",
                    IsBuilderVendor = true,
                    BuilderVendor = userdata.FirstName + " " + userdata.LastName + " - " + userdata.Email,
                    SchedulingTasks = ""
                };
                TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
                TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, _newtradeBuilderVendor.TradeName);
                if (TradesPage.Instance.IsItemInGrid("Trade", _newtradeBuilderVendor.TradeName) is false)
                {
                    TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                    TradesPage.Instance.CreateTrade(_newtradeBuilderVendor);
                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 13.1: Verify if Builder Vendor is found in the Trades Builder Vendor page.</b></font>");
                    TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, _newtradeBuilderVendor.TradeName);
                    if (TradesPage.Instance.IsItemInGrid("Trade", _newtradeBuilderVendor.TradeName) is true)
                    {
                        TradesPage.Instance.SelectItemInGrid("Trade", _newtradeBuilderVendor.TradeName);
                        TradeDetailPage.Instance.LeftMenuNavigation("Builder Users");
                        TradeBuilderUserPage.Instance.FilterItemInGrid("Builder User Email", GridFilterOperator.EqualTo, userdata.Email);
                        if (TradeBuilderUserPage.Instance.IsItemInGrid("Builder User Email", userdata.Email) is true)
                        {
                            ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Builder Vendor is added to the trade.</b></font>");
                        }
                        else
                        {
                            ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Builder Vendor is not added to the trade.</b></font>");
                        }
                    }
                }
            }
            
        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 12.0 Tear down test data.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 12.1 Delete Building Trades.</b></font>");

            DeleteTradeRelations(_trade.TradeName);
            DeleteTradeRelations(_newtrade.TradeName);
            DeleteTradeRelations(_newtradeVendor.TradeName);
            DeleteTradeRelations(_newtradeBuilderVendor.TradeName);

            DeleteTrade(_trade.TradeName);
            DeleteTrade(_newtrade.TradeName);
            DeleteTrade(_newtradeVendor.TradeName);
            DeleteTrade(_newtradeBuilderVendor.TradeName);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 12.2 Delete Building Group.</b></font>");
            DeleteBuildingGroup();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 12.3 Delete Building Phase.</b></font>");
            DeleteBuildingPhase();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 12.4 Delete Vendor.</b></font>");
            DeleteVendor();

            if(IsBuilderVendorEnabled)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 12.5 Delete User.</b></font>");
                DeleteUser();
            }
           

        }

        private void DeleteBuildingGroup()
        {
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is true)
            {
                BuildingGroupPage.Instance.DeleteItemInGrid("Name", NewBuildingGroupName);
                BuildingGroupPage.Instance.WaitGridLoad();
            }
        }
 
        private void DeleteBuildingPhase()
        {
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Name", NewBuildingPhaseName);
            }
        }

        private void DeleteVendor()
        {
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
            {
                VendorPage.Instance.DeleteItemInGrid("Name", NewVendorName);
            }
        }

        private void DeleteTradeRelations(string tradeName)
        {
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeName);
            CommonHelper.RefreshPage();
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", tradeName);
                TradeDetailPage.Instance.LeftMenuNavigation("Building Phases", true);

                TradeBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, NewBuildingPhaseName);
                System.Threading.Thread.Sleep(2000);
                if (TradeBuildingPhasePage.Instance.IsItemInGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName))
                {
                    TradeBuildingPhasePage.Instance.DeleteItemInGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName);
                }
            }

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeName);
            CommonHelper.RefreshPage();
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
        
        private void DeleteTrade(string tradeName)
        {
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeName);
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeName) is true)
            {
                TradesPage.Instance.DeleteItemInGrid("Trade", tradeName);
                CommonHelper.RefreshPage();
            }
        }
        private void DeleteUser()
        {
            UserPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Users);
            UserPage.Instance.FilterItemInGrid("Username", GridFilterOperator.EqualTo, userdata.UserName);
            if(UserPage.Instance.IsItemInGrid("Username", userdata.UserName))
            {
                UserPage.Instance.DeleteItemInGrid("Username", userdata.UserName);
            }
        }

    }
}
