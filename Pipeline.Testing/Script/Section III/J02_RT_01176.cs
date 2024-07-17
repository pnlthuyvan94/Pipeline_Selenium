using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.CustomField;
using Pipeline.Testing.Pages.Settings.Builder.House;
using Pipeline.Testing.Pages.Settings.CustomField;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Testing.Script.Section_III
{
    public class J02_RT_01176 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        CustomFieldData _customFieldData0;
        CustomFieldData _customFieldData1;
        CustomFieldData _customFieldData2;
        CustomFieldData _customFieldData3;
        CustomFieldData _customFieldData4;
        CustomFieldData _customFieldData5;
        CustomFieldData _customFieldData6;
        CustomFieldData _customFieldData7;
        CustomFieldData _customFieldData8;
        CustomFieldData _customFieldData9;
        List<CustomFieldData> _listcustomFielData;
        List<CustomFieldData> _listcustomFielData_Delete;

        [SetUp]
        public void SetUp()
        {

            _customFieldData0 = new CustomFieldData()
            {
                Title = "RT_Test-1",
                Description = "Create new 1",
                Tag = "RTOne",
                FieldType = "Text",
                Default = true,
                Note = "Create new 1"
            };
            _customFieldData1 = new CustomFieldData()
            {
                Title = "RT_Test-2",
                Description = null,
                Tag = "RTTwo",
                FieldType = "Text",
                Default = false,
                Note = "Create new 2"
            };
            _customFieldData2 = new CustomFieldData()
            {
                Title = "RT_Test-1",
                Description = "Duplicate title",
                Tag = "RTThree",
                FieldType = "Text",
                Default = true,
                Note = "Duplicate Title",
            };
            _customFieldData3 = new CustomFieldData()
            {
                Title = "RT_Test-3",
                Description = "Duplicate tag",
                Tag = "RTOne",
                FieldType = "Text",
                Default = true,
                Note = "Duplicate Tag"
            };
            _customFieldData4 = new CustomFieldData()
            {
                Title = null,
                Description = "Empty Title",
                Tag = "RTThree",
                FieldType = "Text",
                Default = true,
                Note = "Empty title"
            };
            _customFieldData5 = new CustomFieldData()
            {
                Title = "RT_Test-3",
                Description = "Duplicate tag",
                Tag = "'!@#$%^&*()_",
                FieldType = "Text",
                Default = true,
                Note = "Special Tag"
            };
            _customFieldData6 = new CustomFieldData()
            {
                Title = "RT_Test-1",
                Description = "Update 2 to 1 Title",
                Tag = "RTThree",
                FieldType = "Text",
                Default = true,
                Note = "Update 2 to 1 - Invalid"
            };

            _customFieldData7 = new CustomFieldData()
            {
                Title = "RT_Test-3",
                Description = "Update 2 to 1 Tag",
                Tag = "RTOne",
                FieldType = "Text",
                Default = true,
                Note = "Update 2 to 1 - Invalid"
            };
            _customFieldData8 = new CustomFieldData()
            {
                Title = "RT_Test-3",
                Description = "Empty Tag",
                Tag = null,
                FieldType = "Text",
                Default = false,
                Note = "Invalid with empty tag"
            };
            _customFieldData9 = new CustomFieldData()
            {
                Title = "RT_Test-3",
                Description = "Update 2 to 3",
                Tag = "RTThree",
                FieldType = "Text Area",
                Default = true,
                Note = "Update 2 to 3"
            };
            _listcustomFielData = new List<CustomFieldData>()
            {
                _customFieldData0,_customFieldData1,_customFieldData2,_customFieldData3,_customFieldData4,_customFieldData5,_customFieldData6,_customFieldData7,_customFieldData8,_customFieldData9
            };

            _listcustomFielData_Delete = new List<CustomFieldData>()
            {
                _customFieldData0,_customFieldData1,_customFieldData7
            };

        }
        

        [Test]
        [Order(1)]
        [Category("Section_III")]
        public void J02_Setting_Builder_AddAHouseCustomField()
        {
            GoToTheHouseSettingPage();

            // Verify the House Custom fields section is display
            HouseSettingPage.Instance.VerifyTheCustomFieldSectionIsDisplay();

            foreach(var item in _listcustomFielData_Delete)
            {
                if (HouseSettingPage.Instance.IsItemInGrid("Title", item.Title) is true)
                {
                    //Delete data before create new data
                    HouseSettingPage.Instance.DeleteItemInGrid(item.Title);
                }
            }

            //if (HouseSettingPage.Instance.IsItemInGrid("Title", _customFieldData0.Title) is true)
            //{
            //    //Delete data before create new data
            //    HouseSettingPage.Instance.DeleteItemInGrid(_customFieldData0.Title);
            //}

            //if (HouseSettingPage.Instance.IsItemInGrid("Title", _customFieldData1.Title) is true)
            //{
            //    //Delete data before create new data
            //    HouseSettingPage.Instance.DeleteItemInGrid(_customFieldData1.Title);
            //}

            //if (HouseSettingPage.Instance.IsItemInGrid("Title", _customFieldData7.Title) is true)
            //{
            //    //Delete data before create new data
            //    HouseSettingPage.Instance.DeleteItemInGrid(_customFieldData7.Title);
            //}

            // Click add button and verify the Custom field modal display
            HouseSettingPage.Instance.ShowAddCustomFieldModal();

            if (HouseSettingPage.Instance.IsItemInGrid("Title", _customFieldData0.Title) is false)
            {
                // Input data and save
                HouseSettingPage.Instance.CreateCustomField(_listcustomFielData[0]);
            }
            // Verify the message is displayed
            string expectedMsg = "New Custom Field Type added";
            string actualMsg = HouseSettingPage.Instance.GetLastestToastMessage();
            if (actualMsg.Equals(expectedMsg))
            {
                ExtentReportsHelper.LogPass($"New Custom Field Type added with Title: {_customFieldData0.Title}.");
                HouseSettingPage.Instance.CloseToastMessage();
            }
            else 
            {
                ExtentReportsHelper.LogFail($"Could not create Custom Field with Title: {_customFieldData0.Title}. Actual message: <i>{actualMsg}</i>");
                //Assert.Fail();
            }

            HouseSettingPage.Instance.ShowAddCustomFieldModal();
            // Create duplicate title and verify error message
            HouseSettingPage.Instance.VerifyCannotCreateWithDuplicateValue(_listcustomFielData[2]);

            // Duplicate tag and verify error message
            HouseSettingPage.Instance.VerifyCannotCreateWithDuplicateValue(_listcustomFielData[3]);

            // Empty Title and verify error message
            HouseSettingPage.Instance.EnterDataForCustomField(_listcustomFielData[4]);
            if (!HouseSettingPage.Instance.VerifyTitleFieldWithMessage("Please enter a Title"))
            {
                ExtentReportsHelper.LogFail("Verify message is failed!");
            }
            //Assert.Fail();

            // Empty Tag and verify error message
            HouseSettingPage.Instance.EnterDataForCustomField(_listcustomFielData[8]);
            if (!HouseSettingPage.Instance.VerifyTagFieldWithMessage("Please enter a Tag"))
            {
                ExtentReportsHelper.LogFail("Verify message is failed!");
            }
            //Assert.Fail();

            // Invalid Tag (special charactor !@#$%^&*
            HouseSettingPage.Instance.EnterDataForCustomField(_listcustomFielData[5]);
            if (!HouseSettingPage.Instance.VerifyTagFieldWithMessage("Tags must not contain any special characters, spaces, or numbers"))
            {
                ExtentReportsHelper.LogFail("Verify message is failed!");
            }
            //Assert.Fail();

            // Empty Description and Different type field
            if (HouseSettingPage.Instance.IsItemInGrid("Title", _customFieldData1.Title) is false)
            {
                HouseSettingPage.Instance.CreateCustomField(_listcustomFielData[1]);
            }
            // Verify the message is displayed
            expectedMsg = "New Custom Field Type added";
            actualMsg = HouseSettingPage.Instance.GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"New Custom Field Type added with Title: {_customFieldData1.Title}.");
                HouseSettingPage.Instance.CloseToastMessage();
            }
            else 
            {
                ExtentReportsHelper.LogFail($"Could not create Custom Field with Title: {_customFieldData1.Title}. Actual message <i>{actualMsg}</i>");
                //Assert.Fail();
            }

            // Verify the new Custom field create successfully
           // HouseSettingPage.Instance.CloseModal();

            if (!HouseSettingPage.Instance.VerifyItemsInList(_listcustomFielData[0], _listcustomFielData[1]))
            {
                ExtentReportsHelper.LogFail($"The Custom Field with Title: <font color='green'><b>{_customFieldData0.Title} and {_customFieldData1.Title}</b></font> is not display on the grid.");
                //Assert.Fail();
            }
            ExtentReportsHelper.LogPass($"The Custom Field with Title: <font color='green'><b>{_customFieldData0.Title} and {_customFieldData1.Title}</b></font> is display on the grid as expected.");

        }

        [Test]
        [Order(2)]
        [Category("Section_III")]
        public void J02_Setting_Builder_UpdateAHouseCustomField()
        {
            GoToTheHouseSettingPage();

            // Update existing title(from item 2 to item 1)
            HouseSettingPage.Instance.UpdateCustomField(_customFieldData1.Title, _listcustomFielData[6]);
            // Verify the error toast message
            string actualMsg = HouseSettingPage.Instance.GetLastestToastMessage();
            if (!actualMsg.Equals("Custom Field Type could not be edited. Title and Tag Value must be unique and not empty."))
            {
                ExtentReportsHelper.LogFail("Able to create a Custom Field with duplicate Tag.");
                HouseSettingPage.Instance.CloseToastMessage();
                //Assert.Fail();
            }
            else
            {
                ExtentReportsHelper.LogPass("Custom Field Type could not be added as expectation.");
                HouseSettingPage.Instance.CloseToastMessage();
            }

            // update existing task(from item 2 to item 1)
            HouseSettingPage.Instance.UpdateCustomField(_customFieldData1.Title, _listcustomFielData[7]);
            // Verify the error toast message
            actualMsg = HouseSettingPage.Instance.GetLastestToastMessage();
            if (!actualMsg.Equals("Custom Field Type could not be edited. Title and Tag Value must be unique and not empty."))
            {
                ExtentReportsHelper.LogFail("Able to create a Custom Field with duplicate Tag.");
                HouseSettingPage.Instance.CloseToastMessage();
                //Assert.Fail();
            }
            else
            {
                ExtentReportsHelper.LogPass("Custom Field Type could not be added as expectation.");
                HouseSettingPage.Instance.CloseToastMessage();
            }

            // Update normal case
            HouseSettingPage.Instance.UpdateCustomField(_customFieldData1.Title, _listcustomFielData[9]);

            // Verify Success Toast message
            string expectedMsg = "Custom Field Type changed";
            actualMsg = HouseSettingPage.Instance.GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"Custom Field Type updated with Title: {_customFieldData9.Title}.");
                HouseSettingPage.Instance.CloseToastMessage();
            }
            else 
            {
                ExtentReportsHelper.LogFail($"Could not update Custom Field with Title: {_customFieldData9.Title}.");
                //Assert.Fail();
            }

            // Drag and Drop
            HouseSettingPage.Instance.DragAndDrop();
        }

        [Test]
        [Order(3)]
        [Category("Section_III")]
        public void J02_Setting_Builder_ApplyDefaultAHouseCustomField()
        {
            GoToTheHouseSettingPage();

            // Filter all default item // Collect and clear filter
            IList<CustomFieldData> defaultItems = HouseSettingPage.Instance.GetAllDefaultCustomField();

            // Apply default
            HouseSettingPage.Instance.ApplyDefault();

            // Verify
            VerifyTheCustomFieldDisplayCorrectlyOnHouse(defaultItems.ToArray());

        }

        [Test]
        [Order(4)]
        [Category("Section_III")]
        public void J02_Setting_Builder_DeleteAHouseCustomField()
        {
            GoToTheHouseSettingPage();
            if (HouseSettingPage.Instance.IsItemInGrid("Title", _customFieldData0.Title) is true)
            {
                // Filter item created
                HouseSettingPage.Instance.DeleteItemInGrid(_customFieldData0.Title);
            }
            string expectedMess = "Custom Field Type deleted";
            string actualMsg = HouseSettingPage.Instance.GetLastestToastMessage();
            if (expectedMess.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"Custom Field Type: {_customFieldData0.Title} deleted successfully!");
                HouseSettingPage.Instance.CloseToastMessage();
            }
            else
            {
                if (HouseSettingPage.Instance.IsItemInGrid("Title", _customFieldData0.Title))
                    ExtentReportsHelper.LogFail($"Custom Field Type: {_customFieldData0.Title} could not be deleted!");
                else
                    ExtentReportsHelper.LogPass($"Lot: {_customFieldData0.Title} deleted successfully!");
            }

            // Delete 2 items
            if (HouseSettingPage.Instance.IsItemInGrid("Title", _customFieldData9.Title) is true)
            {
                HouseSettingPage.Instance.DeleteItemInGrid(_customFieldData9.Title);
            }
            actualMsg = HouseSettingPage.Instance.GetLastestToastMessage();
            if (expectedMess.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"Custom Field Type: {_customFieldData9.Title} deleted successfully!");
                HouseSettingPage.Instance.CloseToastMessage();
            }
            else
            {
                if (HouseSettingPage.Instance.IsItemInGrid("Title", _customFieldData9.Title))
                    ExtentReportsHelper.LogFail($"Custom Field Type: {_customFieldData9.Title} could not be deleted!");
                else
                    ExtentReportsHelper.LogPass($"Lot: {_customFieldData9.Title} deleted successfully!");
            }
        }

        private void GoToTheHouseSettingPage()
        {
            // Go to Builder House Page
            HouseSettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);

            HouseSettingPage.Instance.LeftMenuNavigation("Builder");

            HouseSettingPage.Instance.SwichingToHouseView();
        }

        private void VerifyTheCustomFieldDisplayCorrectlyOnHouse(params CustomFieldData[] customFieldNames)
        {
            // Navigate to House page
            //HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            // Select a house
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            HousePage.Instance.SelectItemInGrid(0, 1);

            // Go to customField page
            CustomFieldDetailPage.Instance.LeftMenuNavigation("Custom Fields");

            // Verify item in page
            if (!CustomFieldDetailPage.Instance.VerifyCustomFieldIsDisplayedInPage(customFieldNames))
            {
                ExtentReportsHelper.LogFail("After apply Default, this House Custom Field page is not display enough data.");
                //Assert.Fail();
            }
            ExtentReportsHelper.LogPass("After apply Default, this House Custom Field page is displayed enough data.");
        }
    }
}
