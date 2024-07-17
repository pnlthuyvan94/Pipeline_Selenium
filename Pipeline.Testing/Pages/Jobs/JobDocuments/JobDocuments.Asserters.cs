using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.JobDocuments
{
    public partial class JobDocuments
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

        public bool IsUploadButtonDisplayed()
        {
            return Upload_btn.IsDisplayed(true);
        }

    }
}
