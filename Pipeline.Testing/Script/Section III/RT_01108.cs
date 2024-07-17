using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.UserMenu.DocumentType;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class I04_RT_01108 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        DocumentTypeData _documentType;
        [SetUp]
        public void CreateTestData()
        {
            _documentType = new DocumentTypeData()
            {
                DocumentName= "RT-QA_Doument Type",
                Category1= "Construction",
                Category2= "Drafting",
                Role1= "Assoc",
                Role2= "API"
            };
        }
        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void I04_UserMenu_AddDocumentType()
        {
            // 1: navigate to this URL: http://beta.bimpipeline.com/Dashboard/DocumentManager/DocumentTypes.aspx?p=1
            DocumentTypePage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Documents);

            // 2: Add new document type
            DocumentTypePage.Instance.OpenDocumentTypeModal();
            //Assert.That(DocumentTypePage.Instance.AddDocumentType.IsModalDisplayed, "Add Document Type modal is not displayed.");
            if (DocumentTypePage.Instance.AddDocumentType.IsModalDisplayed)
            {
                ExtentReportsHelper.LogPass("Add Document Type modal is displayed.");
            }
            else
            {
                ExtentReportsHelper.LogFail("Add Document Type modal is not displayed.");
            }

            // 3: Populate all values - Click 'Insert' Button
            DocumentTypePage.Instance.AddDocumentType.InsertName(_documentType.DocumentName);

            string[] CategoryItems = { _documentType.Category1, _documentType.Category2 };
            int[] listCategoryItemsIndex = { 1, 2, 3 };
            string[] getCategoryItems = DocumentTypePage.Instance.AddDocumentType.SelectCategory(CategoryItems, listCategoryItemsIndex);
            DocumentTypePage.Instance.AddDocumentType.CategoryToLeft();
            //Assert.That(DocumentTypePage.Instance.AddDocumentType.IsExistingCategory(getCategoryItems), "Categogies are assigned to left unsuccessful.");
            if (DocumentTypePage.Instance.AddDocumentType.IsExistingCategory(getCategoryItems))
            {
                ExtentReportsHelper.LogPass("Categogies are assigned to left unsuccessful.");
            }
            else
            {
                ExtentReportsHelper.LogFail("Categogies are assigned to left unsuccessful.");
            }

            int[] listRoleItemsIndex = { 1, 2, 3 };
            string[] RoleItems = { _documentType.Role1, _documentType .Role2};

            string[] getRoleItems = DocumentTypePage.Instance.AddDocumentType.SelectRole(RoleItems, listRoleItemsIndex);
            DocumentTypePage.Instance.AddDocumentType.RoleToLeft();
            //Assert.That(DocumentTypePage.Instance.AddDocumentType.IsExistingRole(getRoleItems), "Roles are assigned to left unsuccessful.");
            if (DocumentTypePage.Instance.AddDocumentType.IsExistingRole(getRoleItems))
            {
                ExtentReportsHelper.LogPass("Roles are assigned to left successful.");
            }
            else
            {
                ExtentReportsHelper.LogFail("Roles are assigned to left unsuccessful.");
            }

            DocumentTypePage.Instance.AddDocumentType.Insert();

            // 4: Verify message
            string _expectedMessage = "Record was succssfully saved!";
            string _actualMessage = DocumentTypePage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_expectedMessage))
            {
                if (_actualMessage.Equals(_expectedMessage))
                {
                    ExtentReportsHelper.LogFail("Fail to insert Document Type");
                    ExtentReportsHelper.LogFail("The actual message isn't same as the expected one.");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Insert Document Type with name {_documentType.DocumentName} successfully");
                }
            }
            else
            {
                ExtentReportsHelper.LogFail("Don't display any message");
                ExtentReportsHelper.LogFail("Fail to insert Document Type.");
            }

            // 6: Verify the new Document Type create successfully
            DocumentTypePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _documentType.DocumentName);

            bool isFound = DocumentTypePage.Instance.IsItemInGrid("Name", _documentType.DocumentName);
            //Assert.That(isFound, string.Format("New Document with name: \"{0} \"was not display on grid.", _documentType.DocumentName));
            if (isFound)
            {
                ExtentReportsHelper.LogPass(string.Format("New Document with name: \"{0} \"was display on grid.", _documentType.DocumentName));
            }
            else
            {
                ExtentReportsHelper.LogFail(string.Format("New Document with name: \"{0} \"was not display on grid.", _documentType.DocumentName));
            }

            // 7: Delete Document Type
            DeleteDocumentType(_documentType.DocumentName);
        }

        public void DeleteDocumentType(string documentName)
        {
            // Select OK to confirm; verify successful delete and appropriate success message.
            DocumentTypePage.Instance.DeleteItemInGrid("Name", documentName);

            // Filter again to find out that deleted item is removed
            bool isFoundDeletedItem = DocumentTypePage.Instance.IsItemInGrid("Name", documentName);

            if (isFoundDeletedItem)
            {
                ExtentReportsHelper.LogFail("Document Type could not be deleted!");
                ExtentReportsHelper.LogFail($"The selected Document Type: \"{documentName}\" was not deleted on grid.");
            }
            else
            {
                ExtentReportsHelper.LogPass("Document Type deleted successfully!");
            }

        }
        #endregion
    }
}
