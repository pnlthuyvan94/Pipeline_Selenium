namespace Pipeline.Testing.Pages.Assets.OptionSelection.AddOptionSelection
{
    public partial class AddOptionSelectionModal
    {
        public AddOptionSelectionModal EnterOptionSelectionName(string optionName)
        {
            OptionSelectionName_txt.WaitForElementIsVisible(3, false);
            OptionSelectionName_txt.SetText(optionName);
            return this;
        }

        public AddOptionSelectionModal SelectOptionSelectionGroup(string optionGroup)
        {
            OptionSelectionGroup_ddl.SelectItem(optionGroup, false);
            return this;
        }

        public AddOptionSelectionModal CheckCustomize(bool checkedValue)
        {
            Customizable_cb.SetCheck(checkedValue);
            return this;
        }

        public void Save()
        {
            OptionSelectionSave_btn.Click();
            WaitGridLoad();
        }

        public void CloseModal()
        {
            if (IsModalClosed())
                return;

            OptionSelectionClose_btn.Click();
            ModalTitle_lbl.WaitForElementIsInVisible(5);
        }

    }
}
