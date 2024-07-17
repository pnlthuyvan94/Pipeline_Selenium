using OpenQA.Selenium;

namespace Pipeline.Testing.Pages.Purchasing.CostCategory.AddCostCategory
{
    public partial class AddCostCategoryPage
    {
        public AddCostCategoryPage AddName(string name)
        {
            Name_txt.SetText(name);
            return this;
        }

        public AddCostCategoryPage AddDescription(string description)
        {
            Description_txt.SetText(description);
            return this;
        }

        public string EnterCostType(string costType)
        {
            if (!string.IsNullOrEmpty(costType) && !costType.ToLower().Equals("none"))
                 CostType_ddl.SelectItemByValueOrIndex(costType, 1);
            return CostType_ddl.SelectedItemName;
        }

        public void Save()
        {
            Save_btn.Click(false);
            WaitGridLoad();
        }


        public void CloseModal()
        {
            Close_btn.Click();
            System.Threading.Thread.Sleep(500);
        }
    }
}
