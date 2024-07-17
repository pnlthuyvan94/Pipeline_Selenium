using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Assets.OptionType.OptionTypeDetail.AddOptionToOptionType
{
    public partial class AddOptionToOptionTypeModal
    {
        public AddOptionToOptionTypeModal AddOptionToOptionType(IList<string> optionList)
        {
            CheckBox optionSelection;
            foreach (var optionName in optionList)
            {
                optionSelection = new CheckBox(FindType.XPath, $"//*[contains(@id,'ctl00_CPH_Content_rlbSOs_i')]/label/input/../span[contains(text(), '{ optionName }')]/../input");
                if (optionSelection.IsNull())
                {
                    ExtentReportsHelper.LogFail($"The Option Type with name <font color='green'><b>{optionName}</b></font> is not displayed on option list.");
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
            AddOptionToOptionType_btn.Click();
        }

        public void CloseModal()
        {
            Cancel_btn.Click();
        }

        public List<string> GetSpecifiedItemInOptionList(int numberOfOptionSelection)
        {
            return ListOfOption_lst.GetSpecifiedItemsName(numberOfOptionSelection);
        }
    }
}
