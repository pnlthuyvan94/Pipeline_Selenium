using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.DocumentTypes.AddDocumentTypes
{
    public partial class AddDocumentTypeModal
    {
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
                        throw new TimeoutException("The \"Add Document Type\" modal is not displayed.");
                    }
                }
                return (ModalTitle_lbl.GetText() == "Add Document Type");
            }
        }
    }
}
