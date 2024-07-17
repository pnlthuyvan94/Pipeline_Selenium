using OpenQA.Selenium;

namespace Pipeline.Testing.Pages.Pathway.DesignerView.AddDesignerView
{
    public partial class AddDesignerViewModal
    {
        public AddDesignerViewModal EnterViewName(string name)
        {
            ViewName_txt.SetText(name);
            return this;
        }


        public AddDesignerViewModal EnterViewLocation(string location)
        {
            if (!string.IsNullOrEmpty(location))
                ViewLocation_ddl.SelectItem(location, true);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            WaitGridLoad();
        }


        public void CloseModal()
        {
            Close_btn.Click();
            System.Threading.Thread.Sleep(500);
        }
    }
}
