

using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Assets.Communities.Spec_Sets
{
    public partial class CommunitySpecSetsPage
    {

        public void VerifyHouseOverridesInSpecSetGroup(string SpecSetGroup, List<string> HouseOverrides)
        {
            ExpandSpecSetGroup(SpecSetGroup);
            foreach (string item in HouseOverrides)
            {
                Label SpecSet_lbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgSetsPanel']//tbody/tr/td/b[contains(text(),'{SpecSetGroup}')]//ancestor::tr/following::tr//tbody//td[contains(text(),'{item}')]");
                if (SpecSet_lbl.IsDisplayed() && SpecSet_lbl.GetText().Equals(item) is true)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The SpecSet Group With Name '{SpecSet_lbl.GetText()}' is displayed in '{SpecSetGroup}' SpecSet Groups Grid is correct.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>The SpecSet With Name {SpecSet_lbl.GetText()} is not displayed in {SpecSetGroup} SpecSet Groups Grid.</font>");
                }
            } 
        }
    }
}
