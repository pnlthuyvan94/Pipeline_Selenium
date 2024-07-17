using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;


namespace Pipeline.Testing.Pages.Jobs.Job.JobSpecSets
{
    public partial class JobSpecSetsPage
    {
        public bool VerifyJobSpecSetsPageIsDisplayed()
        {
            SpecSetsHeaderTitle_Lbl.WaitForElementIsVisible(5);
            if (SpecSetsHeaderTitle_Lbl.IsDisplayed(false))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(SpecSetsHeaderTitle_Lbl), "<font color='green'><b>The Job Spec Sets Page is displayed correctly</b></font>");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(SpecSetsHeaderTitle_Lbl), "<font color='red'>The Job Spec Sets Page is displayed wrong</font>");
                return false;
            }
        }

        public void VerifyJobSetOverrideIsDisplayedCorrectly(string expectdata)
        {
            Textbox JobSetOverrideItem_txt = new Textbox(FindType.XPath, $"//*[contains(text(),'{expectdata}')]//ancestor::tr//td[3]");
            if (JobSetOverrideItem_txt.IsDisplayed(false) && JobSetOverrideItem_txt.GetText().Equals(expectdata))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(JobSetOverrideItem_txt), $"<font color='green'><b>The Job Set Override In Grid is updated successfully with new Job Set Override: {JobSetOverrideItem_txt.GetText()}</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(JobSetOverrideItem_txt), $"<font color='red'>The Job Set Override In Grid is updated with wrong new Job Set Override: {JobSetOverrideItem_txt.GetText()}</font>");
            }
        }

        public void VerifyFilterItemInGridIsDisplayedCorrectly(string expectdata)
        {
            Textbox FilterItem_txt = new Textbox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl04_lblSpecSetGroups_Name' and contains(text(),'{expectdata}')]");
            if (FilterItem_txt.IsDisplayed(false) && FilterItem_txt.GetText().Equals(expectdata))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(FilterItem_txt), $"<font color='green'><b>The Filter Item In Grid is successfully displayed with Item {FilterItem_txt.GetText()}</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(FilterItem_txt), $"<font color='red'>The Filter Item In Grid is unsuccessfully displayed with Item {FilterItem_txt.GetText()}</font>");
            }
        }
    }
}

