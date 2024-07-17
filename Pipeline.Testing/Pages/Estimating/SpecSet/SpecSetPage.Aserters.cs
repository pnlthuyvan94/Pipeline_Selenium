using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.SpecSet
{
    public partial class SpecSetPage
    {
        public void VerifySpecSetInSpecSetGroup(string SpecSetGroups, List<string> listSpecSet)
        {
            Button ExpandSpecSet_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSpecSetGroups_ctl00']//a[contains(text(),'{SpecSetGroups}')]//ancestor::tr/td[@class='rgExpandCol']");
            ExpandSpecSet_btn.Click();

            foreach(string item in listSpecSet)
            {
                Label SpecSet_lbl = new Label(FindType.XPath, $"//a[text()='{SpecSetGroups}']//ancestor::tr/following::tr/td//span[contains(@id, 'ctl00_CPH_Content_rgSpecSetGroups_ctl00') and text() = '{item}']");
                if (SpecSet_lbl.IsDisplayed() && SpecSet_lbl.GetText().Equals(item))
                {
                    ExtentReportsHelper.LogPass(null, $"The SpecSet With Name <font color='green'><b>'{SpecSet_lbl.GetText()}'</b> is displayed in '{SpecSetGroups}' SpecSet Groups Grid is correct.</font>");
                }
                else if(item == string.Empty)
                {
                    ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>No child records to display in '{SpecSetGroups}' SpecSet Groups Grid is correct.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"The SpecSet With Name <font color='red'>'{SpecSet_lbl.GetText()}' is not displayed in {SpecSetGroups} SpecSet Groups Grid is correct.</font>");
                }


            }
        }
       

    }
}
