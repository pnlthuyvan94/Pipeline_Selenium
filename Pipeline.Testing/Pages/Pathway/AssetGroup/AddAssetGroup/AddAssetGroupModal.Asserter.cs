using System;

namespace Pipeline.Testing.Pages.Pathway.AssetGroup.AddAssetGroup
{
    public partial class AddAssetGroupModal
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
                    System.Threading.Thread.Sleep(300);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("The \"Add Asset Group\" modal is not displayed.");
                    }
                }
                return (ModalTitle_lbl.GetText() == "Add Group");
            }
        }

    }
}
