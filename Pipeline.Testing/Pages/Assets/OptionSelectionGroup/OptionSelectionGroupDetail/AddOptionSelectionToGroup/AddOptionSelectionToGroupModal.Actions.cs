using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Assets.OptionSelectionGroup.OptionSelectionGroupDetail.AddOptionSelectionToGroup
{
    public partial class AddOptionSelectionToGroupModal
    {
        public AddOptionSelectionToGroupModal AddOptionSelectionToGroup(int numberOfOptionSelection)
        {
            List<string> optionSelectionGroupList = ListOfOptionSelection_lst.GetSpecifiedItemsName(numberOfOptionSelection);

            CheckBox optionSelection;
            foreach (var optionSelectionName in optionSelectionGroupList)
            {
                optionSelection = new CheckBox(FindType.XPath, $"//*[contains(@id,'ctl00_CPH_Content_rlbSelections_i')]/label/input/../span[text()='{optionSelectionName}']/../input");
                if (optionSelection.IsNull())
                {
                    ExtentReportsHelper.LogFail($"The building group with name <font color='green'><b>{optionSelectionName}</b></font> is not displayed on option list.");
                }
                else
                {
                    optionSelection.Check();
                }
            }
            Save();

            return this;
        }


        private void Save()
        {
            AddOptioNSelectionToGroup_btn.Click();
        }

        public void CloseModal()
        {
            Cancel_btn.Click();
        }

        public List<string> GetSpecifiedItemInOptionSelectionList(int numberOfOptionSelection)
        {
            return ListOfOptionSelection_lst.GetSpecifiedItemsName(numberOfOptionSelection);
        }
    }
}
