using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Assets.OptionGroupDetail
{
    public partial class OptionGroupDetailPage
    {
        public bool IsTitleOptionGroup(string title)
        {
            if (optionGroupDetail_lbl == null || optionGroupDetail_lbl.IsDisplayed() == false)
            {
                throw new Exception("Not found " + optionGroupDetail_lbl.GetText() + " element");
            }
            return (optionGroupDetail_lbl.GetText() == title);
        }

        public bool IsAddOptionDisplayed()
        {
            if (!addOptionToOption_lbl.WaitForElementIsVisible(5))
            {
                throw new TimeoutException("The \"Add Option to Option Group\" modal is not displayed.");
            }
            return (addOptionToOption_lbl.GetText() == "Add Option to Option Group");
        }

        public bool IsNameOptionGroup(string name)
        {
            if (nameGroupOption_txt.IsNull() || nameGroupOption_txt.IsDisplayed() == false)
            {
                ExtentReportsHelper.LogFail("Not found " + nameGroupOption_txt.GetText() + " element");
            }
            return (nameGroupOption_txt.GetValue() == name);
        }

        //Cutoff Phase is removed from Purchasing menu
        /*public bool iscutoffphaseoptiongroup(string cutoffphase)
        {
            if (cutoffGroupOption_ddl.IsNull() || cutoffGroupOption_ddl.IsDisplayed() == false)
            {
                ExtentReportsHelper.LogFail("Not found " + cutoffGroupOption_ddl.SelectedItemName + " element");
            }
            return (cutoffGroupOption_ddl.SelectedItemName == cutoffPhase);

        }

        */
    }
}
