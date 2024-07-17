using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Purchasing.CutoffPhase.AddCutoffPhase;

namespace Pipeline.Testing.Pages.Purchasing.CutoffPhase
{
    public partial class CutoffPhasePage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            CutOffPhase_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgCutOffPhases']/div[1]");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return CutOffPhase_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void ClickAnItemOnGrid(string columnName, string value)
        {
            CutOffPhase_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            CutOffPhase_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            CutOffPhase_Grid.WaitGridLoad();
        }

        public void ClickAddToOpenCutoffPhaseModal()
        {
            PageLoad();
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddCutoffPhaseModal = new AddCutoffPhaseModal();
            System.Threading.Thread.Sleep(500);
        }

        public void CloseModal()
        {
            FindElementHelper.FindElement(FindType.XPath, "//*[@id='rg-modal']/section/header/a").Click();
            System.Threading.Thread.Sleep(500);
        }

        public void DeleteCutOffPhase(CutoffPhaseData _data)
        {
            DeleteItemInGrid("Name", _data.Name);
            WaitGridLoad();
            string successfulMess = $"Cutoff Phase deleted successfully!";
            if (successfulMess == GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Cutoff Phase deleted successfully!</b></font>");
                CloseToastMessage();
            }
            else
            {
                if (IsItemInGrid("Name", _data.Name))
                    ExtentReportsHelper.LogFail("<font color = 'red'>Cutoff Phase could not be deleted!</font>");
                else
                    ExtentReportsHelper.LogPass(null, "<font color = 'green'><b>Cutoff Phase deleted successfully!</b></font>");
            }
        }
    
        /// <summary>
        /// Create a new Cutoff Phase
        /// </summary>
        /// <param name="data"></param>
        public void CreateNewCutoffPhase(CutoffPhaseData data)
        {
            // Click on "+" Add button
            ClickAddToOpenCutoffPhaseModal();
            if (AddCutoffPhaseModal.IsModalDisplayed is false)
                ExtentReportsHelper.LogFail($"<font color='red'>Could not open Create Cutoff Phase modal or the title is incorrect</font>.");

            AddCutoffPhaseModal.CreateNewCutoffPhase(data);
            string actualMessage = GetLastestToastMessage();
            string expectedMessage = $"Cutoff Phase {data.Name} created successfully!";

            // Verify toast message
            if (!string.IsNullOrEmpty(actualMessage) && actualMessage != expectedMessage )
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Could not create a new Cutoff Phase with name <b>{data.Name}</b>.</font>");
                CloseToastMessage();
            }
            else
            {
                // Verify item on the grid if can't get toast message
                FilterItemInGrid("Name", GridFilterOperator.Contains, data.Name);
                if (IsItemInGrid("Name", data.Name) is true)
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Create a new Cutoff Phase with name {data.Name} successfully.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find Cutoff Phase with name {data.Name} on the grid view." +
                        $"<br>Failed to create Cost Type.</br></font>");
            }
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            CutOffPhase_Grid.ClickItemInGrid(columnName, value);
        }
    }

}
