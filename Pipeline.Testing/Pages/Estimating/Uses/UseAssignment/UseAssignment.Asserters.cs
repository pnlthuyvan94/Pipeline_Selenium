

using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.Uses.UseAssignment
{
    public partial class UseAssignment
    {
        public bool VerifyUsesDetailPage(string title, string Name)
        {
            HeaderUsesTitle_Lbl.WaitForElementIsVisible(5);
            if (HeaderUsesTitle_Lbl.GetText().Trim().Equals(title) || UsesName_Lbl.GetText().Trim().Equals(Name))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(UsesName_Lbl), $"The Uses Detail page is displayed with Title <font color='green'><b>{title}</b></font>");
                return true;
            }
            ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(HeaderUsesTitle_Lbl), $"The Uses Detail page is displayed with wrong Title <font color='green'><b>{title}</b></font>");
            return false;
        }

        public void VerifyAssignmentsPage(List<string> Listtitle, List<string> ListSpecSetConversion)
        {
            foreach (var title in Listtitle)
            {
                Label HeaderAssignmentsTitle_Lbl = new Label(FindType.XPath, $"//th[@class='rgHeader simCardHeader']/a[contains(text(),'{title}')]");
                if (HeaderAssignmentsTitle_Lbl.IsDisplayed() && HeaderAssignmentsTitle_Lbl.GetText().Equals(title))
                {
                    ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(HeaderAssignmentsTitle_Lbl), $"The Assignments page is displayed with Title <font color='green'><b>{HeaderAssignmentsTitle_Lbl.GetText()}</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(HeaderAssignmentsTitle_Lbl), $"The Assignments page is displayed with wrong Title <font color='red'>{HeaderAssignmentsTitle_Lbl.GetText()}</font>");
                }
            }
            foreach (var title in ListSpecSetConversion)
            {
                Label HeaderSpecSet_Lbl = new Label(FindType.XPath, $"//header[@class='card-header clearfix']//span[contains(text(),'{title}')]");
                if (HeaderSpecSet_Lbl.GetText().Equals(title))
                {
                    ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(HeaderSpecSet_Lbl), $"The Spec Set Funtional is displayed with Title <font color='green'><b>{HeaderSpecSet_Lbl.GetText()}</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(HeaderSpecSet_Lbl), $"The Spec Set Funtional is displayed with wrong Title <font color='red'>{HeaderSpecSet_Lbl.GetText()}</font>");
                }
            }
        }
    }
}
