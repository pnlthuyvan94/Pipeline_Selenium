using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.OptionGroup;
using Pipeline.Testing.Pages.Assets.OptionGroupDetail;
using Pipeline.Testing.Pages.Assets.Options;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A06_A_RT_01221 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private OptionGroupData oldData;
        private OptionGroupData newTestData;
        private IList<string> optionList;
        OptionData _option1;
        OptionData _option2;

        // Pre-condition
        [SetUp]
        public void GetTestData()
        {
            var optionType = new List<bool>()
            {
                false, false, false
            };

            _option1 = new OptionData()
            {
                Name = "QA_RT_Option01_Automation",
                Number = "0100",
                SquareFootage = 0,
                Description = "Please do not remove or modify",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 0.00,
                Types = optionType
            };
            _option2 = new OptionData()
            {
                Name = "QA_RT_Option02_Automation",
                Number = "0200",
                SquareFootage = 0,
                Description = "Please do not remove or modify",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 0.00,
                Types = optionType
            };
            oldData = new OptionGroupData()
            {
                Name = "QA_RT_OPTION GROUP AUTO TEST",
                SortOrder = "-1"
                //CutoffPhase = "NONE" // Default none
            };

            newTestData = new OptionGroupData()
            {
                Name = "RT-QA_OPTION GROUP TEST AUTO - Update",
                SortOrder = "0"
            };
            optionList = new List<string> { "QA_RT_Option01_Automation", "QA_RT_Option02_Automation" };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Option Page.</font>");
            // Go to Option default page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            // Filter
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.NoFilter, string.Empty);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option1.Name);

            if (!OptionPage.Instance.IsItemInGrid("Name", _option1.Name))
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                Assert.That(OptionPage.Instance.AddOptionModal.IsModalDisplayed(), "Create Option modal is not displayed.");
                // Create Option - Click 'Save' Button
                OptionPage.Instance.AddOptionModal.AddOption(_option1);
                string _expectedMessage = $"Option Number is duplicated.";
                string actualMsg = OptionPage.Instance.GetLastestToastMessage();
                if (_expectedMessage.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogFail($"Could not create Option with name { _option1.Name} and Number {_option1.Number}.");
                    Assert.Fail($"Could not create Option.");
                }
                BasePage.PageLoad();
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Option with name { _option1.Name} is displayed in grid.</font>");
            }

            // Filter

            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option2.Name);
            // when Run RT need to filter column Number BACK TO Default
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.NoFilter, string.Empty);
            if (!OptionPage.Instance.IsItemInGrid("Name", _option2.Name))
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                if(OptionPage.Instance.AddOptionModal.IsModalDisplayed() is true)
                {
                    ExtentReportsHelper.LogPass("Create Option modal is displayed");
                    // Create Option - Click 'Save' Button
                    OptionPage.Instance.AddOptionModal.AddOption(_option2);
                    string _expectedMessage = $"Option Number is duplicated.";
                    string actualMsg = OptionPage.Instance.GetLastestToastMessage();
                    if (_expectedMessage.Equals(actualMsg))
                    {
                        ExtentReportsHelper.LogFail($"Could not create Option with name { _option2.Name} and Number {_option2.Number}.");
                    }
                    BasePage.PageLoad();
                }

                else
                {
                    ExtentReportsHelper.LogFail("Create Option modal is not displayed ");
                }

            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Option with name { _option2.Name} is displayed in grid.</font>");
            }

            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            ExtentReportsHelper.LogInformation(" Step 1: Navigate Asserts > Option Groups and open scussess Option Group page");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_GROUP_URL);
            OptionGroupPage.Instance.FilterItemInGrid("Option Group Name", GridFilterOperator.Contains, oldData.Name);
            if (!OptionGroupPage.Instance.IsItemInGrid("Option Group Name", oldData.Name))
            {
                // Step 2: click on "+" Add button
                OptionGroupPage.Instance.ClickAddToOptionGroupModal();
                OptionGroupPage.Instance.AddOptionGroup
                                    .EnterOptionGroupName(oldData.Name)
                                    .EnterSortOrder(oldData.SortOrder);

                // Save
                OptionGroupPage.Instance.AddOptionGroup.Save();

                // Verify successful save and appropriate success message.
                string _expectedMessage = "New option group added successfully.";
                string _actualMessage = OptionGroupPage.Instance.AddOptionGroup.GetLastestToastMessage();
                if (_expectedMessage == _actualMessage)
                    ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
                else
                    ExtentReportsHelper.LogWarning($"The message does not as expected. <br>Actual results: {_actualMessage}<br>Expected results: {_expectedMessage} ");

                OptionGroupPage.Instance.CloseToastMessage();
                OptionGroupPage.Instance.FilterItemInGrid("Option Group Name", GridFilterOperator.EqualTo, oldData.Name);
            }
        }


        [Test]
        [Category("Section_IV")]
        public void A06_A_Assets_DetailPage_OptionGroup_OptionGroup()
        {
            // Step 1: Navigate Asserts > Option Groups and open scussess Option Group detail page
            OptionGroupPage.Instance.SelectItemInGrid("Option Group Name", oldData.Name);

            // Verify open option groups page is successful
            if (OptionGroupDetailPage.Instance.IsTitleOptionGroup(oldData.Name) is true)
            {
                ExtentReportsHelper.LogPass("Open successfully Option Group data page");
            }
            else
            {
                ExtentReportsHelper.LogFail("Open unsuccessfully Option Group data page.");
            }

            // Step 2:  In Option Group page, click the '+' button to open the 'Add Option to Option Group' modal
            ExtentReportsHelper.LogInformation(" Step 2: In Option Group page, click the '+' button to open the 'Add Option to Option Group' modal");
            OptionGroupDetailPage.Instance.ClickAdd();
            if (OptionGroupDetailPage.Instance.IsAddOptionDisplayed())
            {
                ExtentReportsHelper.LogPass("Add Option Group to Option Group is displayed");
                OptionGroupDetailPage.Instance.SelectOptionByName(optionList[0]);
                OptionGroupDetailPage.Instance.SelectOptionByName(optionList[1]);
            }
            else
            {
                ExtentReportsHelper.LogFail("Add Option Group to Option Group isn't displayed");
            }
            // Save option to option sucessfully
            OptionGroupDetailPage.Instance.ClickSave();
            string _actualMessage = OptionGroupPage.Instance.GetLastestToastMessage();
            string _expectedMessage = "Option were successfully added.";
            if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail($"Save option unsuccessfully.");
            }
            else
            {
                ExtentReportsHelper.LogPass($"Save option successfully.");
                OptionGroupDetailPage.Instance.CloseToastMessage();
            }
            //OptionGroupDetailPage.Instance.ClickClose();
            // Edit option group detail 
            OptionGroupDetailPage.Instance.editOptionGroupDetail(newTestData.Name, newTestData.SortOrder);

            // Step 3: Update the valid for Option Group; verify the successfully update
            ExtentReportsHelper.LogInformation(" Step 3: Update the valid for Option Group; verify the successfully update");
            if (OptionGroupDetailPage.Instance.IsNameOptionGroup(newTestData.Name) is true)
            {
                ExtentReportsHelper.LogPass("Update the valid for Option Group is successfully");
            }
            else
            {
                ExtentReportsHelper.LogFail("Update the valid for Option Group is unsuccessfully");
            }

            // Step 4: Verify the filter option
            ExtentReportsHelper.LogInformation(" Step 4: Verify the filter option");
            // OptionGroupDetailPage.Instance.FilterItemInGridOption("Name", GridFilterOperator.Contains, optionList[1]);
            if (OptionGroupDetailPage.Instance.IsItemInGridOption("Name", optionList[0]) && OptionGroupDetailPage.Instance.IsItemInGridOption("Name", optionList[1]) is true)
            {
                ExtentReportsHelper.LogPass("Filter option is successfully");
            }
            else
            {
                ExtentReportsHelper.LogFail("Filter option is unsuccessfully");
            }

            CommonHelper.RefreshPage();

            // Step 5: The ability to delete the unassigned newly created item from the UI
            ExtentReportsHelper.LogInformation(" Step 5: The ability to delete the unassigned newly created item from the UI");
            OptionGroupDetailPage.Instance.DeleteItemInGridOption("Name", optionList[0]);
            OptionGroupDetailPage.Instance.DeleteItemInGridOption("Name", optionList[1]);
            if (OptionGroupDetailPage.Instance.IsItemInGridOption("Name", optionList[0]) && OptionGroupDetailPage.Instance.IsItemInGridOption("Name", optionList[1]) is true)
            {
                
                ExtentReportsHelper.LogFail("Remove option is unsuccessfully");
            }
            else
            {
                ExtentReportsHelper.LogPass("Remove option is successfully");
            }
        }

        [TearDown]
        public void Delete()
        {
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_GROUP_URL);
            // 5. Select the trash can to delete the new Option; 
            OptionGroupPage.Instance.FilterItemInGrid("Option Group Name", GridFilterOperator.Contains, newTestData.Name);
            if (OptionGroupPage.Instance.IsItemInGrid("Option Group Name", newTestData.Name))
            {
                OptionGroupPage.Instance.DeleteOptionGroup("Option Group Name", newTestData.Name);
            }
        }

    }
}
