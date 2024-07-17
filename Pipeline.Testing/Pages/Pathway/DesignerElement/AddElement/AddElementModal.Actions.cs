namespace Pipeline.Testing.Pages.Pathway.DesignerElement.AddElement
{
    public partial class AddElementModal
    {
        public AddElementModal EnterElementName(string name)
        {
            ElementName_txt.SetText(name);
            return this;
        }

        public AddElementModal SelectElementType(string type)
        {
            ElementType_ddl.SelectItem(type, true);
            return this;
        }

        public AddElementModal SelectElementStyle(string style)
        {
            Style_ddl.SelectItem(style, true);
            return this;
        }

        public AddElementModal EnterElementDescription(string name)
        {
            Description_txt.SetText(name);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            DesignerElement_grid.WaitGridLoad();
        }

        public void CloseModal()
        {
            Close_btn.Click();
            System.Threading.Thread.Sleep(500);
        }
    }
}
