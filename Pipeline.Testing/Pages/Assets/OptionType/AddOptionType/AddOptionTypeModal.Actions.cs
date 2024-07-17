using OpenQA.Selenium;

namespace Pipeline.Testing.Pages.Assets.OptionType.AddOptionType
{
    public partial class AddOptionTypeModal
    {
        private AddOptionTypeModal EnterOptionTypeName(string name)
        {
            OptionTypeName_txt.SetText(name);
            return this;
        }

        private AddOptionTypeModal EnterSortOrder(string sortOrder)
        {
            SortOrder_txt.SetText(sortOrder);
            return this;
        }

        private AddOptionTypeModal EnterDisplayName(string displayName)
        {
            DisplayName_txt.SetText(displayName);
            return this;
        }

        private AddOptionTypeModal IsPathwayVisible(bool isPathwayVisible)
        {
            IsPathwayVisible_ckb.SetCheck(isPathwayVisible);
            return this;
        }

        private AddOptionTypeModal IsFlexPlan(bool isFlexPlan)
        {
            IsFlexPlan_ckb.SetCheck(isFlexPlan);
            return this;
        }

        private void Save()
        {
            OptionTypeSave_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptionTypes']/div[1]");
        }

        public void AddNewOptionType(OptionTypeData data)
        {
            EnterOptionTypeName(data.Name).EnterSortOrder(data.SortOrder);//.EnterDisplayName(data.DisplayName)
                //.IsPathwayVisible(data.IsPathwayVisible).IsFlexPlan(data.IsFlexPlan);
            Save();
        }

        public void CloseModal()
        {
            OptionTypeClose_btn.Click();
            System.Threading.Thread.Sleep(500);
        }

    }
}
