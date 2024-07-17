namespace Pipeline.Testing.Pages.Pathway.DesignerElement.AddElementType
{
    public partial class AddElementTypeModal
    {
        public AddElementTypeModal EnterName(string name)
        {
            TypeName_txt.SetText(name);
            return this;
        }


        public void Save()
        {
            Save_btn.Click();
            ElementType_grid.WaitGridLoad();
        }


        public void CloseModal()
        {
            Close_btn.Click();
            System.Threading.Thread.Sleep(500);
        }
    }
}
