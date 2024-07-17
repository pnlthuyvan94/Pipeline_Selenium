using System;

namespace Pipeline.Testing.Pages.Purchasing.ReleaseGroup.AddReleaseGroup
{
    public partial class AddReleaseGroupModal
    {
        //public bool IsDefaultValues
        //{
        //    get
        //    {
        //        if (!string.IsNullOrEmpty(Description_txt.Text))
        //            return false;
        //        if (!string.IsNullOrEmpty(Name_txt.Text))
        //            return false;
        //        if (!string.IsNullOrEmpty(SortOrder_txt.Text))
        //            return false;
        //        return true;
        //    }
        //}

        public bool IsModalDisplayed()
        {
            ModalTitle_lbl.WaitForElementIsVisible(5);
            return (ModalTitle_lbl.GetText() == "Add Release Group");
        }

    }
}
