using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.Uses
{
    public partial class UsesPage
    {
        /// <summary>
        /// Verify import grid displays or not
        /// </summary>
        /// <param name="gridTitle"></param>
        /// <returns></returns>
        public bool IsUseImportGridDisplay(string view, string gridTitle)
        {
            DropdownList view_ddl = new DropdownList(FindType.XPath, "//*[@id='ddlViewType']");
            if(view_ddl.IsDisplayed() is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find 'View' drop down list on the current page.</font>");
                return false;
            }

            if(view.Equals(view_ddl.SelectedItemName) is false)
            {
                // Current view is different from expected one, then re-select it
                view_ddl.SelectItem(view, true);
                PageLoad();
            }


            Label CommunityImport_lbl = new Label(FindType.XPath, $"//h1[text()='{gridTitle}']");
            if (CommunityImport_lbl.IsDisplayed() is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>{gridTitle} grid view displays successfully.</b></font>");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find {gridTitle} grid view on the current page.</font>");
                return false;
            }
        }
    }
}
