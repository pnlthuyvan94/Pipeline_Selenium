using System;

namespace Pipeline.Testing.Pages.Pathway.DesignerElement.AddElementType
{
    public partial class AddElementTypeModal
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
                        throw new TimeoutException("The \"Add Element Type\" modal is not displayed.");
                    }
                }
                return (ModalTitle_lbl.GetText() == "Add Element Type");
            }
        }

    }
}
