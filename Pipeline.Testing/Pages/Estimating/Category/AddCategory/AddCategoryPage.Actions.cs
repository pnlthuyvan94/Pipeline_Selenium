using OpenQA.Selenium;

namespace Pipeline.Testing.Pages.Estimating.Category.AddCategory
{
    public partial class AddCategoryPage
    {
        public AddCategoryPage AddName(string name)
        {
            CategoryName_txt.SetText(name);
            return this;
        }

        public AddCategoryPage SetCategoryParent(string parent)
        {
            if (string.IsNullOrEmpty(parent) is false)
                CategoryParent_ddl.SelectItemByValueOrIndex(parent, 1);
            return this;
        }

        public void Save()
        {
            CategorySave_btn.Click();
            WaitGridLoad();
        }

        public void CloseModal()
        {
            CategoryClose_btn.Click();
            System.Threading.Thread.Sleep(500);
        }
    }
}
