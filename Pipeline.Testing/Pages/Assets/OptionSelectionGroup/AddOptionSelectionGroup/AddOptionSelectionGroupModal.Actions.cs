namespace Pipeline.Testing.Pages.Assets.OptionSelectionGroup.AddOptionSelectionGroup
{
    public partial class AddOptionSelectionGroupModal
    {
        public AddOptionSelectionGroupModal EnterOptionSelectionGroupName(string groupName)
        {
            OptionSelectionGroupName_txt.SetText(groupName);
            return this;
        }

        public AddOptionSelectionGroupModal EnterSortOrder(string sortOrder)
        {
            SortOrder_txt.SetText(sortOrder);
            return this;
        }

        public void Save()
        {
            OptionSelectionGroupSave_btn.Click();
        }

        public void CloseModal()
        {
            if (IsModalClosed())
                return;

            OptionSelectionGroupClose_btn.Click();
            ModalTitle_lbl.WaitForElementIsInVisible(5);
        }

    }
}
