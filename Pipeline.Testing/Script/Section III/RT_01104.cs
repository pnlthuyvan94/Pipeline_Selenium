using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Sales.ContractDocument.AddContractDocument;
using Pipeline.Testing.Pages.Sales.ContractDocument;
using Pipeline.Testing.Based;
using Pipeline.Common.Constants;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class G03_RT_01104 : BaseTestScript
    {
        private const string valueToFindColumn = "Value To Find";
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        #region"Test case"
        [Test]
        [Category("Section_III")]
        [Ignore("Sales menu was removed from Pipeline, so this test sript will be ignored.")]
        public void G03_Sales_AddContractDocument()
        {
            // 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Sales/Contracts/Default.aspx
            ContractDocumentPage.Instance.SelectMenu(MenuItems.SALES).SelectItem(SalesMenu.ContractDocuments);

            // 2: Add new contract
            Row TestData = ExcelFactory.GetRow(ContractDocumentPage.Instance.TestData_RT_01104, 1);
            AddContract();

            // 3: Verify new Contract document in header
            Assert.That(ContractDocumentDetailPage.Instance.IsSaveContractSuccessful(TestData["Name"]), "Create new contract ducument unsuccessfully");
            ExtentReportsHelper.LogPass("Create Contract Document successful");

            // 4: Back to Contract page
            ContractDocumentPage.Instance.SelectMenu(MenuItems.SALES).SelectItem(SalesMenu.ContractDocuments);

            // 6: Verify the new Contract create successfully
            ContractDocumentPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, TestData["Name"]);
            bool isFound = ContractDocumentPage.Instance.IsItemInGrid("Name", TestData["Name"]);
            Assert.That(isFound, string.Format("New Contract Document \"{0} \" was not display on grid.", TestData["Name"]));

            // 7: Add new contract with duplicate name and verify it
            AddContract();
            Assert.That(!ContractDocumentDetailPage.Instance.IsSaveContractSuccessful(TestData["Name"]), "Create new contract ducument successfully");
            ExtentReportsHelper.LogPass("Can't create duplicate Contract Document");
            // Back to contact page
            ContractDocumentPage.Instance.SelectMenu(MenuItems.SALES).SelectItem(SalesMenu.ContractDocuments);

            // 8: Delete Contract Document
            DeleteContract(TestData["Name"]);
        }

        public void AddContract()
        {
            ContractDocumentPage.Instance.ClickAddContractDocumentButton();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_CONTRACT_URL;
            Assert.That(ContractDocumentDetailPage.Instance.IsPageDisplayed(expectedURL), "Contract Document detail page isn't displayed");

            // 3: Populate all values
            ContractDocumentPage.Instance.CreateNewContractDocument();
        }

        public void DeleteContract(string contractName)
        {
            // Select OK to confirm; verify successful delete and appropriate success message.
            ContractDocumentPage.Instance.DeleteItemInGrid("Name", contractName);

            string expectedMess = $"Contract successfully removed.";
            if (expectedMess == ContractDocumentPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass("Contract Document deleted successfully!");
                ContractDocumentPage.Instance.CloseToastMessage();
            }
            else
            {
                if (ContractDocumentPage.Instance.IsItemInGrid("Name", contractName))
                    ExtentReportsHelper.LogFail("Contract Document could not be deleted!");
                else
                    ExtentReportsHelper.LogPass("Contract Document successfully!");
            }
        }
        #endregion

    }
}
