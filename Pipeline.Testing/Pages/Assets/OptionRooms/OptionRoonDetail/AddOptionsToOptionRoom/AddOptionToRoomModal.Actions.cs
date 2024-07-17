using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Assets.OptionRooms.OptionRoonDetail.AddOptionsToOptionRoom
{
    public partial class AddOptionToRoomModal
    {
        public AddOptionToRoomModal AddOptionToRoom(IList<string> optionlist)
        {
            CheckBox option;
            foreach (var optionName in optionlist)
            {
                option = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rlbSOs']/div/ul/li/label/span[contains(text(),'{optionName}')]/../input");
                if (option.IsNull())
                {
                    ExtentReportsHelper.LogFail($"The Option with name <font color='green'><b>{optionName}</b></font> is not displayed on Option list.");
                }
                else
                {
                    option.Check();
                }
            }
            Save();

            return this;
        }


        private void Save()
        {
            AddOptionToRoom_btn.Click();
            // Loading grid
            WaitingLoadingGifByXpath(loadingIcon, 5);
        }

        public void CloseModal()
        {
            Cancel_btn.Click();
        }

        public List<string> GetSpecifiedItemInOptionList(int numberOfProduct)
        {
            return OptionList_lst.GetSpecifiedItemsName(numberOfProduct);
        }
    }
}
