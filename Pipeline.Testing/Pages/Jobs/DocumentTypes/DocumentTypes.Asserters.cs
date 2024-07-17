using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.DocumentTypes
{
    public partial class DocumentTypes
    {
        public bool IsColumnFoundInGrid(string columnName)
        {
            try
            {
                return DocumentTypes_Grid.IsColumnFoundInGrid(columnName);
            }
            catch
            {
                return false;
            }
        }
    }
}
