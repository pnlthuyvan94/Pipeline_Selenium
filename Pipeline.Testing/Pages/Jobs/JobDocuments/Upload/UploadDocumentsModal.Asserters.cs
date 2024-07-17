using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.JobDocuments.Upload
{
    public partial class UploadDocumentsModal
    {
        public bool IsColumnFoundInGrid(string columnName)
        {
            try
            {
                return Documents_grid.IsColumnFoundInGrid(columnName);
            }
            catch
            {
                return false;
            }
        }

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
                        return false;
                        //throw new TimeoutException("The \"Upload Document\" modal is not displayed.");
                    }
                }
                return (ModalTitle_lbl.GetText() == "Upload");
            }
        }

    }


}
