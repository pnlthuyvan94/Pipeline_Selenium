using System.Collections.Generic;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Settings.BuilderMT;
using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.BuildingPhaseDetail;
using Pipeline.Common.Constants;
using Pipeline.Testing.Pages.Import;

namespace Pipeline.Testing.Script.Section_III
{
    class B03_C_PIPE_49992 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        private readonly List<string> _fourCharsItems = new List<string> { "-120", "-123", "-7", "-67", "-679" };
        private readonly List<string> _sixCharsItems = new List<string> { "-0012", "-01235", "-12345", "-67", "-67954" };
        private const string CODE_COLUMN_NAME = "Code";
        private const string NAME_COLUMN_NAME = "Name";
        private const string IMPORTED_4_CHARS_SUCCESS_ADDED_CODE = "-120";
        private const string IMPORTED_4_CHARS_SUCCESS_ADDED_NAME = "Exact 4";
        private const string IMPORTED_LESS_THAN_4_CHARS_SUCCESS_ADDED_CODE = "-67";
        private const string IMPORTED_LESS_THAN_4_CHARS_SUCCESS_ADDED_NAME = "Less than 4";
        private const string IMPORTED_6_CHARS_SUCCESS_ADDED_CODE = "-12345";
        private const string IMPORTED_LESS_THAN_6_CHARS_SUCCESS_ADDED_CODE = "-67";
        private const string TEXT_PARAGRAPH_CONTENT_ID = "ctl00_CPH_Content_lblPhases";
        private const string ERROR_MESSAGE_4_CHARS_FILE_DIR = @"\DataInputFiles\Import\PIPE_49992\ErrorMessage4Chars.txt";
        private const string ERROR_MESSAGE_6_CHARS_FILE_DIR = @"\DataInputFiles\Import\PIPE_49992\ErrorMessage6Chars.txt";
        private const string TOAST_MESSAGE_ADD_4_CHARS_FAILED = "cannot be greater than 4 characters";
        private const string TOAST_MESSAGE_ADD_6_CHARS_FAILED = "cannot be greater than 6 characters";
        public const string FiveBuildingPhaseDeletedToastMessage = "5 of 5 selected Phases deleted.";
        BuildingPhaseData _buildingPhaseDataFourChars;
        BuildingPhaseData _buildingPhaseDataLessThanFourChars;
        BuildingPhaseData _buildingPhaseDataMoreThanFourChars;
        BuildingPhaseData _buildingPhaseDataEditFourToTwoChars;
        BuildingPhaseData _buildingPhaseDataEditTwoToFourChars;
        BuildingPhaseData _buildingPhaseDataFourCharsSetToEdit;
        BuildingPhaseData _buildingPhaseDataSixChars;
        BuildingPhaseData _buildingPhaseDataLessThanSixChars;
        BuildingPhaseData _buildingPhaseDataMoreThanSixChars;
        BuildingPhaseData _buildingPhaseDataEditSixToFiveChars;
        BuildingPhaseData _buildingPhaseDataEditFourToSixChars;
        BuildingPhaseData _buildingPhaseDataSixCharsSetToEdit;
        BuildingGroupData _buildingGroupData;
        [SetUp]
        public void SetupData()
        {
            _buildingGroupData = new BuildingGroupData()
            {
                Code = "1999",
                Name = "49992_bDGr_Measure_Container"
            };
            _buildingPhaseDataEditFourToTwoChars = new BuildingPhaseData()
            {
                Code = "-7"
            };
            _buildingPhaseDataEditTwoToFourChars = new BuildingPhaseData()
            {
                Code = "-123"
            };
            _buildingPhaseDataFourChars = new BuildingPhaseData()
            {
                Code = "-678",
                Name = "BuildingPhaseExactFourChars"
            };
            _buildingPhaseDataLessThanFourChars = new BuildingPhaseData()
            {
                Code = "-4",
                Name = "BuildingPhaseLessThanFourChars"
            };
            _buildingPhaseDataMoreThanFourChars = new BuildingPhaseData()
            {
                Code = "-4567",
                Name = "BuildingPhaseGreaterThanFourChars"
            };
            _buildingPhaseDataFourCharsSetToEdit = new BuildingPhaseData()
            {
                Code = "-679",
                Name = "ForEditFourChars"
            };
            _buildingPhaseDataEditSixToFiveChars = new BuildingPhaseData()
            {
                Code = "-0012"
            };
            _buildingPhaseDataEditFourToSixChars = new BuildingPhaseData()
            {
                Code = "-01235"
            };
            _buildingPhaseDataSixChars = new BuildingPhaseData()
            {
                Code = "-00123",
                Name = "BuildingPhaseExactSixChars"
            };
            _buildingPhaseDataLessThanSixChars = new BuildingPhaseData()
            {
                Code = "-012",
                Name = "BuildingPhaseLessThan6Chars"
            };
            _buildingPhaseDataMoreThanSixChars = new BuildingPhaseData()
            {
                Code = "-1234567",
                Name = "BuildingPhaseGreaterThanSixChars"
            };
            _buildingPhaseDataSixCharsSetToEdit = new BuildingPhaseData()
            {
                Code = "-67954",
                Name = "ForEditSixChar"
            };
        }
        private void SetCodeLimit(int inputNumber)
        {
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuilderMTPage.Instance.NavigateURL("/BuilderBom/Settings/Default.aspx");
            BuilderMTPage.Instance.SetBuildingPhaseCodeLimit(inputNumber);
        }
        private void CreateBuildingGroup(BuildingGroupData buildingGroupData)
        {
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid(NAME_COLUMN_NAME, GridFilterOperator.Contains,
                buildingGroupData.Name);
            if (BuildingGroupPage.Instance.IsItemInGrid(NAME_COLUMN_NAME, buildingGroupData.Name))
            {
                ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'>Building group has been existed</font>");
            }
            else
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }
        }
        private void CreateBuildingPhase(BuildingPhaseData buildingPhaseData, BuildingGroupData buildingGroupData)
        {
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
            BuildingPhasePage.Instance.AddBuildingPhaseModal.EnterPhaseCode(buildingPhaseData.Code)
                .EnterPhaseName(buildingPhaseData.Name);
            BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup
                ($"{buildingGroupData.Code}-{buildingGroupData.Name}");
            BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
        }
        private void EditBuildingPhase(BuildingPhaseData currentBuildingPhase, BuildingPhaseData editedBuildingPhase)
        {
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid(CODE_COLUMN_NAME, GridFilterOperator.Contains,
                currentBuildingPhase.Code);
            BuildingPhasePage.Instance.ClickEditItemInGrid(CODE_COLUMN_NAME, currentBuildingPhase.Code);
            BuildingPhaseDetailPage.Instance.EnterPhaseCode(editedBuildingPhase.Code).Save();
        }
        private void LogBasedOnToastMessage(string toastMessageToCompare, int codeLength, string make)
        {
            if (BuildingPhasePage.Instance.GetLastestToastMessage().Contains(toastMessageToCompare))
            {
                ExtentReportsHelper.LogPass(
                    $"<font color='green'> Unable to {make} a Building Phase with its code exceeding {codeLength} characters </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(
                    $"<font color='red'> Issue found when able to {make} a Building Phase with {codeLength} chars settings </font>");
            }
        }
        private void LogBasedOnDeleteToastMessage(string deleteToastMessage)
        {
            if (BuildingPhasePage.Instance.GetLastestToastMessage().Equals(deleteToastMessage))
            {
                ExtentReportsHelper.LogPass("<font color='green'> Deleting Building Phases is a success</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'> Deleting Building Phases is not a success</font>");
            }
        }
        private void LogBasedEdit(bool condition1, bool condition2, int codeLength)
        {
            if (condition1 && condition2)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Edit the Building Phases with setting {codeLength} chars code successfully</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Cannot edit the Building Phases with setting {codeLength} chars code</font>");
            }
        }
        private void LogBasedCreate(bool condition1, bool condition2, int codeLength)
        {
            if (condition1 && condition2)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Create the Building Phases with setting {codeLength} chars code successfully</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Cannot create the Building Phases with setting {codeLength} chars code</font>");
            }
        }
        private void LogBasedImport(bool condition, string buildingphaseName)
        {
            if (condition)
            {
                ExtentReportsHelper.LogPass(
                    $"<font color='green'> The Building Phase {buildingphaseName} with valid code is imported </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(
                    $"<font color='red'> The Building Phase {buildingphaseName} with valid code cannot be imported </font>");
            }
        }
        private void LogBasedImportToastMessage(bool condition)
        {
            if (condition)
            {
                ExtentReportsHelper.LogPass(
                    "<font color='green'> The error message is shown correctly while importing Building Phase file has exact 4-character code </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(
                    "<font color='red'> The error message is shown incorrectly while importing Building Phase file has exact 4-character code </font>");
            }
        }
        [Test]
        [Category("Section_III")]
        public void B03_C_Import_Building_Phase_Over_Four_Or_Six_Chars_No_ErrorMessage()
        {
            //Step 0: Check Building Phase Code character requirement as 4 chars
            ExtentReportsHelper.LogInformation(
                "<font color='lavender'>Step 1.0: Check Building Phase Code character requirement as 4</font>");
            SetCodeLimit(4);
            //Step 1: Create Building Group 
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'>Step 1: Create a building group</font>");
            CreateBuildingGroup(_buildingGroupData);
            //Step 2: Go to Building Phases, create new Building Phase with exactly 4 chars code
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 2.0: Create new Building Phase with exactly 4 chars.</font>");
            CreateBuildingPhase(_buildingPhaseDataFourChars, _buildingGroupData);
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 2.1: Create new Building Phase with exactly 4 chars to be edited.</font>");
            CreateBuildingPhase(_buildingPhaseDataFourCharsSetToEdit, _buildingGroupData);
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 2.2: Create new Building Phase with less than 4 chars.</font>");
            CreateBuildingPhase(_buildingPhaseDataLessThanFourChars, _buildingGroupData);
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 2.3: Create new Building Phase with more than 4 chars.</font>");
            CreateBuildingPhase(_buildingPhaseDataMoreThanFourChars, _buildingGroupData);
            BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
            BuildingPhasePage.Instance.CloseToastMessage();
            ExtentReportsHelper.LogInformation(null,
               "<font color='lavender'> Step 2.4: Verify if Building Phase can be created with its code less than or equal to 4 characters</font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid(CODE_COLUMN_NAME, GridFilterOperator.EqualTo,
                _buildingPhaseDataFourChars.Code);
            bool isCreateFourCharsBuildingPhaseSuccess = BuildingPhasePage.Instance.IsItemInGrid(CODE_COLUMN_NAME,
                _buildingPhaseDataFourChars.Code);
            BuildingPhasePage.Instance.FilterItemInGrid(CODE_COLUMN_NAME, GridFilterOperator.EqualTo,
                _buildingPhaseDataLessThanFourChars.Code);
            bool isCreateLessThanFourCharsBuildingPhaseSuccess = BuildingPhasePage.Instance.IsItemInGrid(CODE_COLUMN_NAME,
                _buildingPhaseDataLessThanFourChars.Code);
            LogBasedCreate(isCreateFourCharsBuildingPhaseSuccess, isCreateLessThanFourCharsBuildingPhaseSuccess, 4);
            //Step 3: Edit the Building Phases
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 3.0: Edit the newly created 4 chars Building Phase to less than 4 ones.</font>");
            EditBuildingPhase(_buildingPhaseDataFourChars, _buildingPhaseDataEditFourToTwoChars);
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 3.1: Edit the newly created Building Phase with less than 4-character code to exactly 4-character one.</font>");
            EditBuildingPhase(_buildingPhaseDataLessThanFourChars, _buildingPhaseDataEditTwoToFourChars);
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 3.2: Edit the current Building Phase has 4-character code to the new one having more than 4-character code.</font>");
            EditBuildingPhase(_buildingPhaseDataFourCharsSetToEdit, _buildingPhaseDataMoreThanFourChars);
            LogBasedOnToastMessage(TOAST_MESSAGE_ADD_4_CHARS_FAILED, 4, "edit");
            BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 3.3: Verify the edited code now updated</font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid(CODE_COLUMN_NAME, GridFilterOperator.EqualTo,
                _buildingPhaseDataEditFourToTwoChars.Code);
            bool isEditFourCharsBuildingPhaseSuccess = BuildingPhasePage.Instance.IsItemInGrid(CODE_COLUMN_NAME,
                _buildingPhaseDataEditFourToTwoChars.Code);
            BuildingPhasePage.Instance.FilterItemInGrid(CODE_COLUMN_NAME, GridFilterOperator.EqualTo,
                _buildingPhaseDataEditTwoToFourChars.Code);
            bool isEditLessThanFourCharsBuildingPhaseSuccess = BuildingPhasePage.Instance.IsItemInGrid(CODE_COLUMN_NAME,
                _buildingPhaseDataEditTwoToFourChars.Code);
            LogBasedEdit(isEditFourCharsBuildingPhaseSuccess, isEditLessThanFourCharsBuildingPhaseSuccess, 4);
            //Step 4: Import Building Phase with setting 4 chars
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 4: Importing Building Phase with 4-character code.</font>");
            CommonHelper.OpenURL($"{BaseDashboardUrl}{BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE}");
            string fourCharsBuildingPhaseImportFile = @"\DataInputFiles\Import\PIPE_49992\Pipeline_BuildingPhases_4_Chars.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, fourCharsBuildingPhaseImportFile);
            bool isFourCharsBuildingPhaseErrorMessageMatched = CommonHelper.
                CompareContentElementAndFile(TEXT_PARAGRAPH_CONTENT_ID, ERROR_MESSAGE_4_CHARS_FILE_DIR);
            LogBasedImportToastMessage(isFourCharsBuildingPhaseErrorMessageMatched);
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 4.1: Verify if the valid 4-character Building Phase code is imported successfully.</font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid(CODE_COLUMN_NAME,
                GridFilterOperator.Contains, IMPORTED_4_CHARS_SUCCESS_ADDED_CODE);
            bool isImportingFourCharacterBuildingPhaseSuccessful = BuildingPhasePage.Instance.
                IsItemInGrid(NAME_COLUMN_NAME, IMPORTED_4_CHARS_SUCCESS_ADDED_NAME);
            LogBasedImport(isImportingFourCharacterBuildingPhaseSuccessful, IMPORTED_4_CHARS_SUCCESS_ADDED_NAME);
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid(CODE_COLUMN_NAME, GridFilterOperator.EqualTo,
                IMPORTED_LESS_THAN_4_CHARS_SUCCESS_ADDED_CODE);
            bool isImportBuildingPhaseLessThanFourCharSuccess = BuildingPhasePage.Instance.
                IsItemInGrid(NAME_COLUMN_NAME, IMPORTED_LESS_THAN_4_CHARS_SUCCESS_ADDED_NAME);
            LogBasedImport(isImportBuildingPhaseLessThanFourCharSuccess, IMPORTED_LESS_THAN_4_CHARS_SUCCESS_ADDED_NAME);
            //Step 5: Delete the selected 
            ExtentReportsHelper.LogInformation("<font color='lavender'>Step 5: Delete the selected Building Phases</font>");
            BuildingPhasePage.Instance.FilterItemInGrid(CODE_COLUMN_NAME, GridFilterOperator.NoFilter,
                IMPORTED_LESS_THAN_4_CHARS_SUCCESS_ADDED_CODE);
            BuildingPhasePage.Instance.DeleteItemsViaCheckbox(CODE_COLUMN_NAME, _fourCharsItems);
            ExtentReportsHelper.LogInformation(
                "<font color='lavender'>Step 5.1: Verify toast message from deleting Building Phases</font>");
            LogBasedOnDeleteToastMessage(FiveBuildingPhaseDeletedToastMessage);
            //Step 6: Change Building Phase require code to 6-char limit
            ExtentReportsHelper.LogInformation(
                "<font color='lavender'>Step 6: Change Building Phase require code to 6-char limit</font>");
            SetCodeLimit(6);
            //Step 7: Go to Building Phases, create new Building Phase with exactly 6 chars code
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 7.1: Create new Building Phase with exactly 6 chars.</font>");
            CreateBuildingPhase(_buildingPhaseDataSixChars, _buildingGroupData);
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 7.2: Create new Building Phase with exactly 6 chars to be edited.</font>");
            CreateBuildingPhase(_buildingPhaseDataSixCharsSetToEdit, _buildingGroupData);
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 7.3: Create new Building Phase with less than 6 chars.</font>");
            CreateBuildingPhase(_buildingPhaseDataLessThanSixChars, _buildingGroupData);
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 7.4: Create new Building Phase with more than 6 chars.</font>");
            CreateBuildingPhase(_buildingPhaseDataMoreThanSixChars, _buildingGroupData);
            LogBasedOnToastMessage(TOAST_MESSAGE_ADD_6_CHARS_FAILED, 6, "create");
            BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
            BuildingPhasePage.Instance.CloseToastMessage();
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 7.5: Verify if Building Phase can be created with its code less than or equal to 6 characters</font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid(CODE_COLUMN_NAME, GridFilterOperator.EqualTo,
                _buildingPhaseDataSixChars.Code);
            bool isCreateSixCharsBuildingPhaseSuccess = BuildingPhasePage.Instance.IsItemInGrid(CODE_COLUMN_NAME,
                _buildingPhaseDataSixChars.Code);
            BuildingPhasePage.Instance.FilterItemInGrid(CODE_COLUMN_NAME, GridFilterOperator.EqualTo,
                _buildingPhaseDataLessThanSixChars.Code);
            bool isCreateLessThanSixCharsBuildingPhaseSuccess = BuildingPhasePage.Instance.IsItemInGrid(CODE_COLUMN_NAME,
                _buildingPhaseDataLessThanSixChars.Code);
            LogBasedCreate(isCreateSixCharsBuildingPhaseSuccess, isCreateLessThanSixCharsBuildingPhaseSuccess, 6);
            //Step 8: Edit
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 8.0: Edit the newly created 6 chars Building Phase to become 5 chars.</font>");
            EditBuildingPhase(_buildingPhaseDataSixChars, _buildingPhaseDataEditSixToFiveChars);
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 8.1: Edit the newly created less than 6 chars Building Phase to exactly 6 ones.</font>");
            EditBuildingPhase(_buildingPhaseDataLessThanSixChars, _buildingPhaseDataEditFourToSixChars);
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 8.2: Edit a 6 chars Building Phase to new one having more than 6 chars.</font>");
            EditBuildingPhase(_buildingPhaseDataSixCharsSetToEdit, _buildingPhaseDataMoreThanSixChars);
            LogBasedOnToastMessage(TOAST_MESSAGE_ADD_6_CHARS_FAILED, 6, "edit");
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 8.3: Verify the edited code now updated</font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid(CODE_COLUMN_NAME, GridFilterOperator.EqualTo,
                _buildingPhaseDataEditSixToFiveChars.Code);
            bool isEdit6CharSuccess = BuildingPhasePage.Instance.IsItemInGrid(CODE_COLUMN_NAME,
                _buildingPhaseDataEditSixToFiveChars.Code);
            BuildingPhasePage.Instance.FilterItemInGrid(CODE_COLUMN_NAME, GridFilterOperator.EqualTo,
                _buildingPhaseDataEditFourToSixChars.Code);
            bool isEditLessThan6CharSuccess = BuildingPhasePage.Instance.IsItemInGrid(CODE_COLUMN_NAME,
                _buildingPhaseDataEditFourToSixChars.Code);
            LogBasedEdit(isEdit6CharSuccess, isEditLessThan6CharSuccess, 6);
            //Step 9: Import Building Phase with setting 6 chars
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 9: Import Building Phase with setting 6 chars.</font>");
            CommonHelper.OpenURL($"{BaseDashboardUrl}{BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE}");
            string sixCharsBuildingPhaseImportFile = @"\DataInputFiles\Import\PIPE_49992\Pipeline_BuildingPhases_6_Chars.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, sixCharsBuildingPhaseImportFile);
            bool isSixCharBuildingPhaseErrorMessageMatched = CommonHelper.CompareContentElementAndFile(TEXT_PARAGRAPH_CONTENT_ID,
                ERROR_MESSAGE_6_CHARS_FILE_DIR);
            LogBasedImportToastMessage(isSixCharBuildingPhaseErrorMessageMatched);
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 9.1: Verify the valid 6 chars Building Phase in the import file imported succeeded.</font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid(CODE_COLUMN_NAME, GridFilterOperator.Contains,
                IMPORTED_6_CHARS_SUCCESS_ADDED_CODE);
            bool isImporting6CharsBuildingPhaseSuccessful = BuildingPhasePage.Instance.IsItemInGrid(CODE_COLUMN_NAME,
                IMPORTED_6_CHARS_SUCCESS_ADDED_CODE);
            LogBasedImport(isImporting6CharsBuildingPhaseSuccessful, IMPORTED_6_CHARS_SUCCESS_ADDED_CODE);
            BuildingPhasePage.Instance.FilterItemInGrid(CODE_COLUMN_NAME, GridFilterOperator.EqualTo,
                IMPORTED_LESS_THAN_6_CHARS_SUCCESS_ADDED_CODE);
            bool isImportingLessThan6CharsBuildingPhaseSuccessful = BuildingPhasePage.Instance.IsItemInGrid(CODE_COLUMN_NAME,
                IMPORTED_LESS_THAN_6_CHARS_SUCCESS_ADDED_CODE);
            LogBasedImport(isImportingLessThan6CharsBuildingPhaseSuccessful, IMPORTED_LESS_THAN_6_CHARS_SUCCESS_ADDED_CODE);
            //Step 10: Delete the selected via Bulk action
            ExtentReportsHelper.LogInformation("<font color='lavender'>Step 10: Delete the selected via Bulk action</font>");
            BuildingPhasePage.Instance.FilterItemInGrid(CODE_COLUMN_NAME, GridFilterOperator.NoFilter,
                IMPORTED_LESS_THAN_6_CHARS_SUCCESS_ADDED_CODE);
            BuildingPhasePage.Instance.DeleteItemsViaCheckbox(CODE_COLUMN_NAME, _sixCharsItems);
            ExtentReportsHelper.LogInformation("<font color='lavender'>Step 10.1: Verify toast message</font>");
            LogBasedOnDeleteToastMessage(FiveBuildingPhaseDeletedToastMessage);
        }
    }
}
