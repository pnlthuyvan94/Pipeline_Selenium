using System;

namespace Pipeline.Testing.Pages.Purchasing.CostCategory.AddCostCategory
{
    public partial class AddCostCategoryPage
    {
        /*
         * Check Adding model is displayed or not
         */
        public bool IsModalDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (ModalTitle_lbl == null || ModalTitle_lbl.IsDisplayed() == false)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("The \"Add Cost Type\" modal is not displayed.");
                    }
                }
                return (ModalTitle_lbl.GetText() == "Add Cost Category");
            }
        }

    }
}
